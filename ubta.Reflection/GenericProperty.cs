using System;
using System.ComponentModel;
using System.Reflection;

namespace ubta.Reflection
{
    #region Protected / private / internal property helper
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GenericProperty<T> : IGenericHelper
    {
        /// <summary>
        /// The C-Tor
        /// </summary>
        /// <param name="obj">the instance of the object</param>
        /// <param name="name">the name of the property</param>
        /// <param name="method">the methodinfo</param>
        public GenericProperty(Object obj, string name, PropertyInfo propertyInfo)
        {
            myObject = obj;
            myName = name;
            myGetMethod = propertyInfo.GetGetMethod(true);
            mySetMethod = propertyInfo.GetSetMethod(true);
        }

        /// <summary>
        /// Gets the name of the Property
        /// </summary>
        /// <returns>the name</returns>
        public string Name
        {
            get
            {
                return myName;
            }
        }

        /// <summary>
        /// Gets the value of the Property
        /// </summary>
        /// <returns>the value</returns>
        public T Value
        {
            get
            {
                try
                {
                    return (T)myGetMethod.Invoke(myObject, new Object[0]);
                }
                catch (Exception/* ex*/)
                {
                    //return "<" + ex.Message + ">";
                    return default(T);
                }
            }
            set
            {
                try
                {
                    mySetMethod.Invoke(myObject, new Object[1] { value });
                }
                catch
                { }
            }
        }

        /// <summary>
        /// Gets the value of the Property
        /// </summary>
        /// <returns>the value</returns>
        public EditableObject Reference
        {
            get
            {
                T obj = (T)myGetMethod.Invoke(myObject, new Object[0]);
                return EditableObjectManager.Instance.GetEditableObject(obj);
            }
        }


        /// <summary>
        /// Gets a value indicating whether this instance is overridden.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is overridden; otherwise, <c>false</c>.
        /// </value>
        public bool IsOverridden
        {
            get
            {
                Type baseType = myObject.GetType().BaseType;
                bool r = false;
                while (baseType != null)
                {
                    PropertyInfo bp = baseType.GetProperty(Name);
                    if (null != bp)
                    {
                        r = true;
                        break;
                    }
                    baseType = baseType.BaseType;
                }
                return r;
            }
        }

        /// <summary>
        /// Gets the name of the Property
        /// </summary>
        /// <returns>the name</returns>
        public override string ToString()
        {
            return myName;
        }

        /// <summary>
        /// The instance of the object
        /// </summary>
        Object myObject;
        /// <summary>
        /// The name of the property
        /// </summary>
        string myName;
        /// <summary>
        /// The MethodInfo of the property
        /// </summary>
        MethodInfo myGetMethod;
        /// <summary>
        /// The MethodInfo of the property
        /// </summary>
        MethodInfo mySetMethod;
    }
    #endregion
}
