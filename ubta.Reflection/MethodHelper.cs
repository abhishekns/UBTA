#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : MethodHelper.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using ubta.Common;

namespace ubta.Reflection
{
    public class MethodHelper
    {
        private TypeHelper myTypeHelper;
        private Dictionary<string, List<IInvokableExtension>> myMethods = new Dictionary<string, List<IInvokableExtension>>();

        public MethodHelper(TypeExtension te)
        {
            myTypeHelper = te.TypeHelper;
            ConstructorInfo[] cinfos = te.T.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
            MethodInfo[] methods = te.T.GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
            EventInfo[] events = te.T.GetEvents(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
            foreach (MethodInfo minfo in methods)
            {
                List<IInvokableExtension> mes;
                if (!myMethods.TryGetValue(minfo.Name, out mes))
                {
                    mes = new List<IInvokableExtension>();
                    myMethods.Add(minfo.Name, mes);
                }
                mes.Add(new InvokableExtension<MethodInfo>(minfo, myTypeHelper));
            }
            List<IInvokableExtension> ces = new List<IInvokableExtension>();
            myMethods.Add(Constants.CONSTRUCTOR_NODE_NAME, ces);
            foreach (ConstructorInfo cinfo in cinfos)
            {
                ces.Add(new InvokableExtension<ConstructorInfo>(cinfo, myTypeHelper));
            }
            foreach (EventInfo einfo in events)
            {
                List<IInvokableExtension> ees;
                if (!myMethods.TryGetValue(einfo.Name, out ees))
                {
                    ees = new List<IInvokableExtension>();
                    myMethods.Add(einfo.Name, ees);
                }
                ees.Add(new InvokableExtension<EventInfo>(einfo, myTypeHelper));
            }
        }

        public Dictionary<string, List<IInvokableExtension>> Methods
        {
            get
            {
                return myMethods;
            }
        }

        public List<IInvokableExtension> this[string name_in]
        {
            get
            {
                return myMethods[name_in];
            }
        }

        public object Invoke(XmlNode anode_in)
        {
            List<IInvokableExtension> ces = myMethods[anode_in.Name];
            Type[] paramTypes = GetParamTypes(anode_in);
            Type[] genericArgs = GetParamTypes(anode_in, true);
            foreach (IInvokableExtension me in ces)
            {
                Type[] generics;
                if (Comparer(paramTypes, me.ParamTypes, out generics))
                {
                    string[] names;
                    object[] parameters = GetParameters(anode_in, out names);
                    return me.Invoke(null, genericArgs, parameters);
                }
            }
            throw new InvalidOperationException("Non-existant constructor called.");
        }

        public object Invoke(object target, XmlNode anode_in)
        {
            List<IInvokableExtension> ces = myMethods[NameHelper.Clean(anode_in.Name, ":", StringPos.End)];
            Type[] paramTypes = GetParamTypes(anode_in);
            foreach (IInvokableExtension me in ces)
            {
                Type[] generics;
                if (Comparer(paramTypes, me.ParamTypes, out generics))
                {
                    string[] names;
                    object[] parameters = GetParameters(anode_in, out names);
                    object r = me.Invoke(target, generics, parameters);
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (me.Modifiers[i])
                        {
                            VariableManager.Instance[names[i]] = parameters[i];
                        }
                    }
                    return r;
                }
            }
            throw new InvalidOperationException("Non-existant method called.");
        }

        private object[] GetParameters(XmlNode anode_in, out string[] names_out)
        {
            List<string> names = new List<string>();
            List<object> pars = new List<object>();
            foreach (XmlNode paramNode in anode_in.ChildNodes)
            {
                string value = NameHelper.GetAttributeValue(paramNode, Constants.VALUE_NODE_ATTRIBUTE);
                bool added = true;
                if (value == null)
                {
                    if (!(paramNode.IsGenericArgumentNode() || paramNode.IsReturnNode()))
                    {
                        value = NameHelper.GetAttributeValue(paramNode, Constants.NAME_NODE_ATTRIBUTE);
                        if (null != value)
                        {
                            pars.Add(VariableManager.Instance[value]);
                        }
                        else
                        {
                            added = false;
                        }
                    }
                    else
                    {
                        added = false;
                    }
                }
                else
                {
                    pars.Add(value);
                }
                if (added)
                {
                    names.Add(value);
                }
            }
            names_out = names.ToArray();
            return pars.ToArray();
        }

        private bool Comparer(Type[] paramTypes, Type[] types, out Type[] generics)
        {
            generics = null;
            if (paramTypes == types)
            {
                return true;
            }
            if (null == paramTypes)
            {
                return false;
            }
            if (paramTypes.Length != types.Length)
            {
                return false;
            }
            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (types[i].IsGenericParameter)
                {
                    generics = types[i].GetGenericParameterConstraints();
                    if (!paramTypes[i].Equals(generics[0]))
                    {
                        return false;
                    }
                }
                else if (!paramTypes[i].Equals(types[i]))
                {
                    return false;
                }
            }
            return true;
        }

        private Type[] GetParamTypes(XmlNode anode_in)
        {
            return GetParamTypes(anode_in, false);
        }

        private Type[] GetParamTypes(XmlNode anode_in , bool genericArg_in)
        {
            List<Type> t = new List<Type>();
            foreach (XmlNode cNode in anode_in.ChildNodes)
            {
                if (!(cNode.IsReturnNode() || (!cNode.IsGenericArgumentNode() == genericArg_in) ))
                {
                    ParameterModifier m = new ParameterModifier();
                    string typeName = NameHelper.GetParamType(cNode, ref m);
                    if (null != typeName)
                    {
                        Type aType = myTypeHelper.GetType(NameHelper.Clean(typeName, ":", StringPos.End)).T;
                        if (m[0])
                        {
                            t.Add(aType.MakeByRefType());
                        }
                        else
                        {
                            t.Add(aType);
                        }
                    }
                }
            }
            return t.ToArray();
        }
    }
}