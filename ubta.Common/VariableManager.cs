#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : VariableManager.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion

using System.Collections;
using System;
namespace ubta.Common
{
    /// <summary>
    /// Description of VariableManager.
    /// </summary>
    public class VariableManager
    {
        public static VariableManager Instance = new VariableManager();

        private Hashtable myVariables = new Hashtable();
        private Hashtable myVariableTypes = new Hashtable();

        private VariableManager()
        {
        }

        public System.Collections.IDictionaryEnumerator Mapping
        {
            get
            {
                return myVariables.GetEnumerator();
            }
        }

        public void SetVarType(string varName, Type vType)
        {
            if (myVariableTypes.ContainsKey(varName))
            {
                Type x = myVariableTypes[varName] as Type;
                if(x.IsGenericType)
                {
                    // TODO : do the validation
                }
                else if (!(x).IsAssignableFrom(vType))
                {
                    throw new InvalidOperationException(string.Format("Variable {0}'s type cannot be changed from {1} to {2}.", varName, myVariableTypes[varName], vType));
                }
            }
            else
            {
                myVariableTypes.Add(varName, vType);
            }
        }

        public Type GetVarType(string varName)
        {
            if (!myVariableTypes.ContainsKey(varName))
            {
                myVariableTypes.Add(varName, typeof(object));
            }
            return myVariableTypes[varName] as Type;
        }

        public object this[string varName]
        {
            get
            {
                string vn = varName;
                string[] props = null;
                if(varName.Contains("."))
                {
                    props = varName.Split('.');
                    vn = props[0];
                }
                object o = GetValue(myVariables[vn], props);
                return o;
            }
            set
            {
                SetValue(varName, value);
                if(null != value && !varName.Contains(".") && !myVariableTypes.ContainsKey(varName))
                {
                    myVariableTypes.Add(varName, value.GetType());
                }
            }
        }

        private T GetValue<T>(string varName)
        {
            string[] split = varName.Split('.');
            if (split.Length > 1)
            {
                object t = myVariables[split[0]];
                return (T)GetValue(t, split);
            }
            else
            {
                return (T)myVariables[split[0]];
            }
        }

        private object GetValue(object t, string[] props)
        {
            object tn = t;
            if (null != props)
            {
                int i = 1;
                while (i < props.Length)
                {
                    tn = GetValue(tn, props[i]);
                    i++;
                }
            }
            return tn;
        }

        private object GetValue(object t, string prop)
        {
            if (null == t)
            {
                return null;
            }
            return t.GetType().GetProperty(prop).GetValue(t, null);
        }

        private void SetValue(string varName, object value)
        {
            string[] split = varName.Split('.');
            if (split.Length > 1)
            {
                object t = myVariables[split[0]];
                SetValue(t, split, value);
            }
            else
            {
                myVariables[split[0]] = value;
            }
        }

        private void SetValue(object t, string[] prop, object value)
        {
            int i = 1;
            object gn = t;
            while (i < prop.Length - 1)
            {
                gn = GetValue(gn, prop[i]);
                i++;
            }
            if (null != gn)
            {
                gn.GetType().GetProperty(prop[i + 1]).SetValue(gn, value, null);
            }
        }

        public void Reset()
        {
            myVariables.Clear();
        }
    }
}