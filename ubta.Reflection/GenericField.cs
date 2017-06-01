using System.ComponentModel;
using System.Reflection;

namespace ubta.Reflection
{
    #region Fields helper
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GenericField<T> : IGenericHelper
    {
        public GenericField(object obj, FieldInfo info)
        {
            if (System.Runtime.Remoting.RemotingServices.IsTransparentProxy(obj))
            {
                System.Runtime.Remoting.Proxies.RealProxy proxy = System.Runtime.Remoting.RemotingServices.GetRealProxy(obj);
                MethodInfo mi = typeof(System.Runtime.Remoting.Proxies.RealProxy).GetMethod("GetUnwrappedServer", BindingFlags.NonPublic | BindingFlags.Instance);
                obj = mi.Invoke(proxy, null);
            }
            myObject = obj;
            myInfo = info;
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public T Value
        {
            get
            {
                return (T)myInfo.GetValue(myObject);
            }
            set
            {
                myInfo.SetValue(myObject, value);
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
                T obj = (T)myInfo.GetValue(myObject);
                return EditableObjectManager.Instance.GetEditableObject(obj);
            }
        }

        public string Name
        {
            get
            {
                return myInfo.Name;
            }
        }

        public override string ToString()
        {
            return Name + " = " + Value;
        }

        object myObject;
        FieldInfo myInfo;
    }
    #endregion
}
