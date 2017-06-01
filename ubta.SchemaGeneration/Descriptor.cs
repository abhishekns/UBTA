#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : Descriptor.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System;
using System.Reflection;

namespace ubta.SchemaGeneration
{
    public class Descriptor
    {
        public static Type[] GetParamTypes<T>(T method, out ParameterModifier[] modifiers) where T : MethodBase
        {
            Type[] pt = null;
            modifiers = null;
            if (null != method)
            {
                ParameterInfo[] paramInfos = method.GetParameters();
                if (null != paramInfos)
                {
                    pt = new Type[paramInfos.Length];
                    modifiers = new ParameterModifier[paramInfos.Length];
                    for (int i = 0; i < pt.Length; i++)
                    {
                        pt[i] = paramInfos[i].ParameterType;
                        modifiers[i] = new ParameterModifier(1);
                        modifiers[i][0] = pt[i].IsByRef || paramInfos[i].IsOut;
                    }
                }

            }
            return pt;
        }

        protected string myMemberType;
        protected bool m_isResolved;
        protected MemberInfo myMember;
        protected IAssemblyAnalyzer myAnalyzer;

        public Descriptor()
        {
        }

        public Descriptor(IAssemblyAnalyzer analyzer)
        {
            myAnalyzer = analyzer;
        }

        public virtual MemberInfo Member
        {
            set
            {
                myMember = value;
            }
        }

        public virtual void CheckResolveStatus()
        {
            try
            {
                string resolvedType;
                if (null != myAnalyzer && myAnalyzer.isTypeKnown(KnownType, out resolvedType))
                {
                    myMemberType = KnownType.FullName;
                }
                else
                {
                    m_isResolved = false;
                }
            }
            catch (Exception e)
            {
                Logger.write(e);
                throw;
            }
        }

        public string GetXMLName(Type type)
        {
            string typeName = type.Name;
            if (type.IsGenericType)
            {
                int t;
                typeName = typeName.Remove((t = typeName.LastIndexOf("`")) == -1 ? typeName.Length : t);

                //MemberType[] ga = myType.GetGenericArguments();
                //for (int i = 0; i < ga.Length; i++)
                //{
                //    MemberType[] gpc = ga[i].GetGenericParameterConstraints();
                //    if (null != gpc)
                //    {
                //        for (int j = 0; j < gpc.Length; j++)
                //        {
                //            typeName += "_" + gpc[j].Name;
                //        }
                //    }
                //}
            }
            return typeName;
        }

        public string GetCSharpName(Type type)
        {
            string typeName = type.Name;
            if (type.IsGenericType)
            {
                int t;
                typeName = typeName.Remove((t = typeName.LastIndexOf("`")) == -1 ? typeName.Length : t);

                Type[] ga = type.GetGenericArguments();
                for (int i = 0 ; i < ga.Length ; i++)
                {
                    Type[] gpc = ga[i].GetGenericParameterConstraints();
                    if (null != gpc)
                    {
                        for (int j = 0 ; j < gpc.Length ; j++)
                        {
                            if (j == 0)
                            {
                                typeName += "<" + gpc[j].Name;
                            }
                            else
                            {
                                typeName += "," + gpc[j].Name;
                            }
                            if (j == gpc.Length - 1)
                            {
                                typeName += ">";
                            }
                        }
                    }
                }
            }
            return typeName;
        }

        public IAssemblyAnalyzer Analyzer
        {
            set
            {
                myAnalyzer = value;
            }
        }

        public bool IsResolved
        {
            get
            {
                return m_isResolved;
            }
            set
            {
                m_isResolved = value;
            }
        }

        public string MemberType
        {
            get
            {
                return myMemberType;
            }
            set
            {
                myMemberType = value;
            }
        }

        public object Information
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual Type KnownType
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }

    public class Descriptor<T> : Descriptor where T : MemberInfo
    {
        public Descriptor()
        {
        }

        public Descriptor(IAssemblyAnalyzer analyzer, T memberInfo)
            : base(analyzer)
        {
            myMember = memberInfo;
            m_isResolved = true;
            MemberType = String.Empty;
        }
    }
}