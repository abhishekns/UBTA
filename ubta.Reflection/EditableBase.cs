using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace ubta.Reflection
{
    /// <summary>
    /// The delegate for the changed event
    /// </summary>
    public delegate void ObjectChangedDelegate(EditableObject sender);
    /// <summary>
    /// Delegate to add objects
    /// </summary>
    public delegate void AddObjectDelegate(EditableObject creator, EditableObject obj);

    /// <summary>
    /// Base class for all Editable objects
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class EditableObject : IGenericHelper, IDisposable, ICustomTypeDescriptor
    {
        private EditableObjectManager myManager = EditableObjectManager.Instance;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="obj">the object to be Edited</param>
        public EditableObject(object obj)
        {
            myActualObject = obj;
            if (obj != null && !myManager.ObjectTable.ContainsKey(obj))
            {
                myManager.ObjectTable[obj] = this;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="obj">the object to be Edited</param>
        /// <param name="parent">the parent Editable object</param>
        public EditableObject(object obj, EditableObject parent)
        {
            myActualObject = obj;
            myEditableParent = parent;

            if (obj != null && !myManager.ObjectTable.ContainsKey(obj))
            {
                myManager.ObjectTable[obj] = this;
            }

            if (myEditableParent != null)
            {
                OnChanged += new ObjectChangedDelegate(EditableParent.OnChildChanged);
            }
        }

        /// <summary>
        /// Detaches all the Editable objects from the "real" objects (without dispose the real one)
        /// </summary>
        public void Detach()
        {
            if (ActualObject != null)
            {
                myManager.ObjectTable.Remove(ActualObject);
            }

            if (EditableParent != null)
            {
                OnChanged -= new ObjectChangedDelegate(EditableParent.OnChildChanged);
            }

            foreach (EditableObject child in myChildren)
            {
                child.Detach();
            }
        }

        /// <summary>
        /// Implements IDisposable, forwards the call to the object if it is derived from IDisposable.
        /// </summary>
        public virtual void Dispose()
        {
            Detach();

            IDisposable disp = myActualObject as IDisposable;
            if (disp != null)
            {
                disp.Dispose();
            }
        }

        /// <summary>
        /// Registers the object by the delegate
        /// </summary>
        public void AddObject()
        {
            if (myAddObjectDelegate != null)
            {
                myAddObjectDelegate(EditableParent, this);
            }
        }

        /// <summary>
        /// Returns the actual object
        /// </summary>
        [Browsable(false)]
        public object ActualObject
        {
            get
            {
                return myActualObject;
            }
        }

        /// <summary>
        /// Returns the parent object
        /// </summary>
        [Browsable(false)]
        public EditableObject EditableParent
        {
            get
            {
                return myEditableParent;
            }
        }

        /// <summary>
        /// All the collected child objects
        /// </summary>
        [Browsable(false)]
        public IList<EditableObject> Children
        {
            get
            {
                return myChildren;
            }
        }

        /// <summary>
        /// Object was changed, propergate the event
        /// </summary>
        protected virtual void Changed()
        {
            OnSelfChanged();
        }

        /// <summary>
        /// overload this method to recive a changed event
        /// </summary>
        protected virtual void OnSelfChanged()
        {
            // Notify all registered delegates
            if (myChangedDelegate != null)
                myChangedDelegate(this);
        }

        /// <summary>
        /// overload this method to recive a changed event
        /// </summary>
        protected virtual void OnChildChanged(EditableObject sender)
        {
            // Notify all registered delegates
            if (myChangedDelegate != null)
                myChangedDelegate(sender);
        }

        /// <summary>
        /// Is this object read only ? (not editable in PropertyGrid)
        /// </summary>
        [Browsable(false)]
        public bool ReadOnly
        {
            get
            {
                return myReadOnlyFlag;
            }
            set
            {
                myReadOnlyFlag = value;
            }
        }

        /// <summary>
        /// Gets the Editable class name
        /// </summary>
        [Browsable(false)]
        public string EditableClassName
        {
            get
            {
                return this.GetType().Name;
            }
        }

        /// <summary>
        /// The title for the PropertyGrid
        /// </summary>
        /// <returns>string of the class</returns>
        public override string ToString()
        {
            return ActualObject.ToString();
        }

        /// <summary>
        /// The event will be called if the object (or one child) has changed
        /// </summary>
        public event ObjectChangedDelegate OnChanged
        {
            add
            {
                myChangedDelegate += value;
            }
            remove
            {
                myChangedDelegate -= value;
            }
        }

        /// <summary>
        /// Traces the message
        /// </summary>
        /// <param name="str">message</param>
        public void Trace(string str)
        {
            System.Diagnostics.Trace.Write(this.ToString() + ": " + str);
        }

        #region Reflection tricks
        IGenericHelper[] myProtectedPropertyCache = null;
        [CategoryAttribute("Reflection tricks")]
        public IGenericHelper[] ProtectedProperty
        {
            get
            {
                if (myManager.ReflectionEnabled && myProtectedPropertyCache == null && myActualObject != null)
                {
                    Type type = myActualObject.GetType();
                    PropertyInfo[] properties = type.GetProperties(/*BindingFlags.Public | */BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Static);
                    myProtectedPropertyCache = new IGenericHelper[properties.Length];
                    for (int i = 0; i < properties.Length; i++)
                    {
                        Type genType = typeof(GenericProperty<>);
                        Type instType = genType.MakeGenericType(properties[i].PropertyType);
                        ConstructorInfo cinfo = instType.GetConstructor(new Type[] { type, typeof(string), typeof(PropertyInfo) });
                        myProtectedPropertyCache[i] = cinfo.Invoke(new object[] { myActualObject, properties[i].Name, properties[i] }) as IGenericHelper;
                        //myProtectedPropertyCache[i] = new ProtectedProperty<type>(myActualObject, properties[i].Name, getmethod, setmethod);
                    }
                }
                return myProtectedPropertyCache;
            }
        }
        IGenericHelper[] myPublicPropertyCache = null;
        [CategoryAttribute("Reflection tricks")]
        public IGenericHelper[] PublicProperty
        {
            get
            {
                if (myManager.ReflectionEnabled && myPublicPropertyCache == null && myActualObject != null)
                {
                    Type type = myActualObject.GetType();
                    PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Static);
                    myPublicPropertyCache = new IGenericHelper[properties.Length];
                    for (int i = 0; i < properties.Length; i++)
                    {
                        Type genType = typeof(GenericProperty<>);
                        Type instType = genType.MakeGenericType(properties[i].PropertyType);
                        ConstructorInfo cinfo = instType.GetConstructor(new Type[] { type, typeof(string), typeof(PropertyInfo) });
                        myPublicPropertyCache[i] = cinfo.Invoke(new object[] { myActualObject, properties[i].Name, properties[i] }) as IGenericHelper;
                        //myPublicPropertyCache[i] = new ProtectedProperty(myActualObject, properties[i].Name, getmethod, setmethod);
                    }
                }
                return myPublicPropertyCache;
            }
        }

        IGenericHelper[] myObjectFields = null;
        [CategoryAttribute("Reflection tricks")]
        public IGenericHelper[] Fields
        {
            get
            {
                if (myManager.ReflectionEnabled && myObjectFields == null && myActualObject != null)
                {
                    Type type = myActualObject.GetType();
                    FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Static);
                    myObjectFields = new IGenericHelper[fields.Length];
                    for (int i = 0; i < fields.Length; i++)
                    {
                        Type genType = typeof(GenericField<>);
                        Type instType = genType.MakeGenericType(fields[i].FieldType);
                        ConstructorInfo cinfo = instType.GetConstructor(new Type[] { type, typeof(FieldInfo) });
                        myObjectFields[i] = cinfo.Invoke(new object[] { myActualObject, fields[i] }) as IGenericHelper;
                        //myObjectFields[i] = new ObjectFields(myActualObject, objs[i]);
                    }

                }
                return myObjectFields;
            }
        }

        IGenericHelper[] myObjectAttributes = null;
        [CategoryAttribute("Reflection tricks")]
        public IGenericHelper[] Attributes
        {
            get
            {
                if (myManager.ReflectionEnabled && myObjectAttributes == null && myActualObject != null)
                {
                    Type type = myActualObject.GetType();
                    Object[] objs = type.GetCustomAttributes(true);
                    myObjectAttributes = new IGenericHelper[objs.Length];
                    for (int i = 0; i < objs.Length; i++)
                    {
                        myObjectAttributes[i] = new EditableObject(objs[i]);
                    }
                }
                return myObjectAttributes;
            }
        }

        [CategoryAttribute("Reflection tricks"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Type ObjectType
        {
            get
            {
                if (myActualObject == null)
                    return null;

                return myActualObject.GetType();
            }
        }
        #endregion

        /// <summary>
        /// The object to be Edited
        /// </summary>
        protected object myActualObject = null;
        /// <summary>
        /// The parent object
        /// </summary>
        private EditableObject myEditableParent = null;
        /// <summary>
        /// The Child objects
        /// </summary>
        private IList<EditableObject> myChildren = new List<EditableObject>();
        /// <summary>
        /// the list of the registered delegates
        /// </summary>
        private ObjectChangedDelegate myChangedDelegate;
        /// <summary>
        /// Is Object read only ?
        /// </summary>
        private bool myReadOnlyFlag = false;
        /// <summary>
        /// The delegate to add objects
        /// </summary>
        private AddObjectDelegate myAddObjectDelegate = null;

        // Implementation of interface ICustomTypeDescriptor
        #region ICustomTypeDescriptor impl
        public String GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public AttributeCollection GetAttributes()
        {
            AttributeCollection col = TypeDescriptor.GetAttributes(this, true);

            if (myReadOnlyFlag)
            {
                System.Attribute[] attrs = new System.Attribute[col.Count + 1];
                int index = 0;
                foreach (Attribute attr in col)
                {
                    attrs[index] = attr;
                    index++;
                }
                attrs[index] = new ReadOnlyAttribute(true);
                return new AttributeCollection(attrs);
            }

            return col;
        }

        public String GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }


        /// <summary>
        /// Called to get the properties of this type. Returns properties with certain
        /// attributes. this restriction is not implemented here.
        /// </summary>
        /// <param name="attributes"></param>
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        /// <summary>
        /// Called to get the properties of this type.
        /// </summary>
        public PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(this, true);
        }
        #endregion
    }
}
