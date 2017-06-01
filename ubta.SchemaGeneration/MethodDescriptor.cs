#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : MethodDescriptor.cs
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
using System.Text;
using ubta.Common;
using ubta.Reflection;

namespace ubta.SchemaGeneration
{
    public class MethodDescriptor : Descriptor
    {
        private ParameterInfo[] paramsInfo;
        private bool isConstructor = false;
        //string[] namespaceReference;
        public List<bool> paramsIsResolved = new List<bool>();
        private string[] paramsType;
        private string[] paramsCategory;  // indicates whether the parameter is an "In", "Out" or "Ref" parameter
        //string[] resolvedParamsType;
        private bool m_isOverridden;
        public MethodDescriptor()
        {
        }

        public MethodDescriptor(IAssemblyAnalyzer analyzer, MethodInfo methodInfo)
            : base(analyzer)
        {
            myMember = methodInfo;
            paramsInfo = methodInfo.GetParameters();
            //m_isOverridden = false; 
            //----------------------------------------------------
            // m_isOverridden is automatically initialized to false.
            //----------------------------------------------------
            m_isResolved = true;

        }

        public MethodDescriptor(IAssemblyAnalyzer analyzer, ConstructorInfo constructorInfo)
            : base(analyzer)
        {
            myMember = constructorInfo;
            paramsInfo = ConstructorInfo.GetParameters();
            isConstructor = true;
            m_isResolved = true;
        }

        public void CheckOverriding(Type baseType)
        {
            try
            {
                if (null != baseType)
                {
                    Type[] paramTypes = new Type[paramsInfo.Length];
                    int count = 0;
                    foreach (ParameterInfo parameter in paramsInfo)
                    {

                        paramTypes[count] = parameter.ParameterType;
                        count = count + 1;

                    }
                    if (isConstructor)
                    {
                        ConstructorInfo basecinfo = baseType.GetConstructor(paramTypes);
                        if (basecinfo != null)
                        {
                            m_isOverridden = true;
                        }
                    }
                    else
                    {
                        MethodInfo baseClassMethodInfo = baseType.GetMethod(MethodInformation.Name, paramTypes);
                        if (baseClassMethodInfo != null)
                        {
                            m_isOverridden = MethodInformation.ToString().Equals(baseClassMethodInfo.ToString());
                        }
                    }
                    if (!m_isOverridden)
                    {
                        Type basebaseType = baseType.BaseType;
                        if (basebaseType != typeof(object))
                        {
                            CheckOverriding(basebaseType);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.write(e);
                throw;
            }

        }

        public override void CheckResolveStatus()
        {
            try
            {
                //IAssemblyAnalyzer objAssemblyAnalyzer = new AssemblyAnalyzer();
                //IsResolved = true;
                //resolvedParamsType = new string[paramsInfo.Length + 1];
                paramsType = new string[paramsInfo.Length + 1];
                paramsCategory = new string[paramsInfo.Length];
                int paramCounter = 0;
                foreach (ParameterInfo param in paramsInfo)
                {
                    Type paramType = param.ParameterType;

                    // Determine whether the parameter is passed by reference, or is an In or Out parameter
                    if (paramType.IsByRef)
                    {
                        if (param.IsOut)
                        {
                            paramsCategory[paramCounter] = "Out";
                        }
                        else
                        {
                            paramsCategory[paramCounter] = "Ref";
                        }
                    }
                    else
                    {
                        paramsCategory[paramCounter] = "In";
                    }
                    string validSchemaFullName;
                    //trim the ampersand at the end which exists for parameters passed by reference

                    //if (null != paramType.FullName)
                    {
                        while (paramsIsResolved.Count < paramCounter + 1)
                        {
                            paramsIsResolved.Add(false);
                        }
                        Type[] constraints = null;
                        if (paramType.IsGenericParameter)
                        {
                            constraints = paramType.GetGenericParameterConstraints();
                            if (constraints != null && constraints.Length > 0)
                            {
                                StringBuilder sb = new StringBuilder();
                                sb.Append(paramType.GetCodeDeclarationStatement()).Append(":");
                                for (int ccnt = 0; ccnt < constraints.Length; ccnt++ )
                                {
                                    var c = constraints[ccnt];
                                    sb.Append(c.GetCodeDeclarationStatement());
                                    if (ccnt < constraints.Length - 1 && constraints.Length > 1)
                                    {
                                        sb.Append(":");
                                    }
                                }
                                paramsType[paramCounter] = sb.ToString();
                            }
                            else
                            {
                                paramsType[paramCounter] = paramType.Name + ":object";
                            }
                        }
                        else if (paramType.HasElementType)
                        {
                            Type elType = paramType.GetElementType();
                            if (elType.IsGenericParameter)
                            {
                                constraints = elType.GetGenericParameterConstraints();
                                string pType = GetElementType(elType);
                                if (constraints != null && constraints.Length > 0)
                                {
                                    paramsType[paramCounter] = paramType.GetCodeDeclarationStatement() + ":" + pType + ":" + constraints[0].GetCodeDeclarationStatement();
                                }
                                else
                                {
                                    paramsType[paramCounter] = paramType.GetCodeDeclarationStatement() + ":" + pType + ":object";
                                }
                            }
                            else
                            {
                                paramsType[paramCounter] = "_Array:" + elType.GetCodeDeclarationStatement() ;
                            }
                        }
                        else if (paramType.ContainsGenericParameters)
                        {
                            Type[] ga = paramType.GetGenericArguments();
                            if (null != ga && ga.Length > 0)
                            {
                                StringBuilder sb = new StringBuilder();
                                sb.Append(paramType.GetCodeDeclarationStatement());
                                sb.Append(":");
                                for(int i=0; i<ga.Length; i++)
                                {
                                    if (i > 0 && ga.Length > 1)
                                    {
                                        sb.Append(":");
                                    }
                                    if (ga[i].FullName != null)
                                    {
                                        sb.Append(ga[i].GetCodeDeclarationStatement());
                                    }
                                    else
                                    {
                                        Type[] cnstrts = ga[i].GetGenericParameterConstraints();
                                        if (null != cnstrts && cnstrts.Length > 0)
                                        {
                                            sb.Append(cnstrts[0].GetCodeDeclarationStatement());
                                        }
                                        else
                                        {
                                            sb.Append("System.Object");
                                        }
                                    }
                                }
                                paramsType[paramCounter] = sb.ToString();
                            }
                        }
                        else
                        {
                            paramsType[paramCounter] = new TypeDescriptor(myAnalyzer, paramType).ValidSchemaFullName;
                        }
                        if (!myAnalyzer.isTypeKnown(paramType, out validSchemaFullName))
                        {
                            m_isResolved = false;
                        }
                        else
                        {
                            paramsIsResolved[paramCounter] = true;
                        }
                    }
                    paramCounter++;
                }


                // determining the custom-myType of the return-myType for the method
                string resolvedReturnTypeName;

                if (!isConstructor && MethodInformation.ReturnType.FullName == null)
                {
                    //change done to incorporate the methods retuurning Template types
                    //Because methodInfo.ReturnType.FullName is null and throws an exception
                    if (null != myAnalyzer && myAnalyzer.isTypeKnown(MethodInformation.ReturnType.BaseType, out resolvedReturnTypeName))
                    {
                        paramsType[paramCounter] = MethodInformation.ReturnType.BaseType.FullName;
                    }
                    else
                    {
                        //Whether the myType is resolved or not, it has to be defined in the schema.
                        if (null != MethodInformation.ReturnType.BaseType)
                        {
                            paramsType[paramCounter] = MethodInformation.ReturnType.BaseType.FullName;
                        }
                        m_isResolved = false;
                    }
                }
                else if (!isConstructor && null != myAnalyzer && myAnalyzer.isTypeKnown(MethodInformation.ReturnType, out resolvedReturnTypeName))
                {
                    //resolvedParamsType[paramCounter] = resolvedReturnTypeName;
                    paramsType[paramCounter] = MethodInformation.ReturnType.FullName;
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

        private string GetElementType(Type elType)
        {
            if (elType.IsArray)
            {
                return "Array";
            }
            if (elType.IsPointer)
            {
                return "Pointer";
            }
            return "";
        }

        public MethodInfo MethodInformation
        {
            get
            {
                return myMember as MethodInfo;
            }
        }
        public ConstructorInfo ConstructorInfo
        {
            get
            {
                return myMember as ConstructorInfo;
            }
        }

        public ParameterInfo[] parametersInfo
        {
            get
            {
                if (null == paramsInfo)
                {
                    MethodBase mb = myMember as MethodBase;
                    paramsInfo = mb.GetParameters();
                }
                return paramsInfo;
            }
        }


        public string[] ParametersType
        {
            get
            {
                if (null == paramsType)
                {
                    ParameterModifier[] mods;
                    Type[] ptypes = Descriptor.GetParamTypes<MethodBase>(myMember as MethodBase, out mods);
                    paramsType = new string[ptypes.Length+1];
                    paramsCategory = new string[ptypes.Length+1];
                    for (int i = 0; i < ptypes.Length-1; i++)
                    {
                        paramsType[i] = new TypeDescriptor(myAnalyzer, ptypes[i]).ValidSchemaFullName;
                        paramsCategory[i] = GetParamCategory(ptypes[i]);
                    }
                    if (null != MethodInformation)
                    {
                        paramsType[ptypes.Length - 1] = new TypeDescriptor(myAnalyzer, MethodInformation.ReturnType).ValidSchemaFullName;
                        paramsCategory[ptypes.Length - 1] = GetParamCategory(MethodInformation.ReturnType);
                    }
                }
                return paramsType;
            }
        }

        private string GetParamCategory(Type type)
        {
            TypeCategory typeCategory = TypeCategory.OtherType;
            if (type.IsClass)
            {
                typeCategory = TypeCategory.ClassType;
            }
            else if (type.IsInterface)
            {
                typeCategory = TypeCategory.InterfaceType;
            }
            else if (type.IsEnum)
            {
                typeCategory = TypeCategory.EnumType;
            }
            else if (type.IsValueType)
            {
                typeCategory = TypeCategory.StructType;
            }
            // If the myType is that  defined in C++ or C, it is not handled.
            return typeCategory.ToString();
        }

        public string[] ParametersCategory
        {
            get
            {
                if (null == paramsCategory)
                {
                    object t = ParametersType;
                }
                return paramsCategory;
            }

        }

        public bool IsOverridden
        {
            get
            {
                return m_isOverridden;
            }
        }
        public bool IsConstructor
        {
            get
            {
                return isConstructor;
            }
        }

    }
}