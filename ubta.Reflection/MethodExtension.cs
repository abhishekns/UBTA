#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : MethodExtension.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion

using System;
using System.Reflection;
using System.Text;
using System.Linq;
using ubta.Common;

namespace ubta.Reflection
{
    public class InvokableExtension<T> : IInvokableExtension
    {
        private T myInvokable;
        private TypeHelper myTypeHelper;
        private MethodBase myInvoke;
        private EventInfo myEInfo = null;
        private string myCsharpName = null;
        private string myParameters = null;
        private Type[] myParamTypes = null;
        private ParameterModifier myModifiers;

        public InvokableExtension(T t, TypeHelper helper_in)
        {
            myInvokable = t;
            myInvoke = t as MethodBase;
            myTypeHelper = helper_in;
            myEInfo = t as EventInfo;
        }

        public InvokableExtension(MethodBase mBase_in, TypeHelper helper_in)
        {
            myTypeHelper = helper_in;
            myInvoke = mBase_in;
        }

        public ConstructorInfo C
        {
            get
            {
                return myInvoke as ConstructorInfo;
            }
        }

        public MethodInfo M
        {
            get
            {
                return myInvoke as MethodInfo;
            }
        }

        private Type[] GetParamTypes(string eventMethod)
        {
            ParameterInfo[] ps = myEInfo.EventHandlerType.GetMethod(eventMethod).GetParameters();
            Type[] pTypes = new Type[ps.Length];
            for (int i = 0 ; i < ps.Length; i++)
            {
                pTypes[i] = ps[i].ParameterType;
            }
            return pTypes;
        }

        public ParameterModifier Modifiers
        {
            get
            {
                GetParamType();
                return myModifiers;
            }
        }

        public Type[] ParamTypes
        {
            get
            {
                GetParamType();
                return myParamTypes;
            }
        }

        private void GetParamType()
        {
            if (null == myParamTypes)
            {
                ParameterInfo[] parameters = null;
                if (null != myInvoke)
                {
                    parameters = myInvoke.GetParameters();
                }
                else if (null != myEInfo)
                {
                    parameters = myEInfo.EventHandlerType.GetMethod("Invoke").GetParameters();
                }
                if (null != parameters)
                {
                    myParamTypes = new Type[parameters.Length];
                    if (parameters.Length > 0)
                    {
                        myModifiers = new ParameterModifier(parameters.Length);
                    }
                    else
                    {
                        myModifiers = new ParameterModifier();
                    }
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        myParamTypes[i] = parameters[i].ParameterType;
                        myModifiers[i] = parameters[i].IsRetval | parameters[i].IsOut | myParamTypes[i].IsByRef;
                    }
                }
            }
        }

        public string Parameters
        {
            get
            {
                if (null == myParameters)
                {
                    StringBuilder sb = new StringBuilder();
                    ParameterInfo[] parameters = myInvoke.GetParameters();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        ParameterInfo pinfo = parameters[i];
                        TypeExtension te = new TypeExtension(pinfo.ParameterType, myTypeHelper);
                        sb.Append(te.CsharpName);
                    }
                    myParameters = sb.ToString();
                }
                return myParameters;
            }
        }

        public string CsharpName
        {
            get
            {
                if (null == myCsharpName)
                {
                    if(C != null)
                    {
                        myCsharpName = Constants.CONSTRUCTOR_NODE_NAME;
                    }
                    else if (myInvoke.IsGenericMethod)
                    {
                        Type[] ga = myInvoke.GetGenericArguments();
                        string whereClause;
                        StringBuilder sb = new StringBuilder();
                        sb.Append(TypeHelper.GetGenericParametersStatement(ga, out whereClause, myTypeHelper));
                        sb.Append(whereClause);
                        myCsharpName = sb.ToString();
                    }
                    else
                    {
                        myCsharpName = myInvoke.Name;
                    }
                }
                return myCsharpName;
            }
        }

        #region IInvokableExtension Members

        public object Invoke(params object[] args)
        {
            object robj = null;
            if (null != C)
            {
                robj = C.Invoke(args);
            }
            return robj;
        }

        public object Invoke(object target, Type[] genericArgs, params object[] args)
        {
            object retObj = null;
            if (null != M)
            {
                args = CastArgs(M.GetParameters(), args);
                if (M.IsGenericMethod)
                {
                    MethodInfo m = M.MakeGenericMethod(genericArgs);
                    retObj = m.Invoke(target, args);
                }
                else
                {
                    retObj = M.Invoke(target, args);
                }
            }
            else if (null != myEInfo)
            {
                MulticastDelegate d = target.GetType()
                    .GetField(myEInfo.Name, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                    .GetValue(target) as MulticastDelegate;
                retObj = d.DynamicInvoke(args);
            }
            else if (null != C)
            {
                if (C.ReflectedType.IsGenericType)
                {
                    args = CastArgs(C.GetParameters(), args);
                    Type gt = C.ReflectedType.MakeGenericType(genericArgs);
                    var ptypes = (from pt in C.GetParameters() select pt.ParameterType).ToArray();
                    int pc = 1;
                    if (ptypes.Length > 0)
                    {
                        pc = ptypes.Length;
                    }
                    var pm = new ParameterModifier(pc);
                    int i=0;
                    foreach(var pt in C.GetParameters())
                    {
                        pm[i] = pt.IsOut || pt.ParameterType.IsByRef;
                        i++;
                    }
                    ConstructorInfo c = gt.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, ptypes, new ParameterModifier[] { pm });
                    retObj = c.Invoke(args);
                }
                else
                {
                    args = CastArgs(C.GetParameters(), args);
                    retObj = C.Invoke(args);
                }
            }
            return retObj;
        }

        private object[] CastArgs(ParameterInfo[] parameterInfos, object[] args)
        {
            if (null != parameterInfos)
            {
                for (int i = 0; i < parameterInfos.Length; i++)
                {
                    Type t = parameterInfos[i].ParameterType;
                    if (t.IsEnum)
                    {
                        string[] names = Enum.GetNames(t);
                        string val = args[i] as string;
                        for(int j=0; j<names.Length; j++)
                        {
                            if(names[j].Equals(val))
                            {
                                args[i] = Enum.ToObject(t, j);
                                break;
                            }
                        }                        
                    }
                    else if (t.IsValueType)
                    {
                        MethodInfo parse = t.GetMethod("Parse", new Type[] {typeof(string), typeof(System.IFormatProvider)});
                        args[i] = parse.Invoke(null, new object[] {args[i], System.Globalization.NumberFormatInfo.InvariantInfo });
                    }
                }
            }
            return args;
        }

        public MethodBody GetInvokableBody()
        {
            return myInvoke.GetMethodBody();
        }

        #endregion
    }
}