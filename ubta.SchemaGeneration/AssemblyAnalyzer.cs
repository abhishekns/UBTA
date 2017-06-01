#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : AssemblyAnalyzer.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ubta.Common;
using System.Text;


namespace ubta.SchemaGeneration
{
    public class AssemblyAnalyzer : IAssemblyAnalyzer
    {
        private const char CHAR_STOP_Constant = '.';
        private const string STR_STOP_Constant = ".";
        #region Fields
        
        private Hashtable m_TypeAndMembers = new Hashtable();
        private Dictionary<Type, NamespaceInfo> m_TypeAndNamespace = new Dictionary<Type, NamespaceInfo>();
        private bool m_IsClassSchema;
        private Assembly m_ObjAssembly;
        
        static List<string> basicDataTypes;
        #endregion

        public NamespaceInfo GetNamespaceInfo(string name)
        {
            var r = (from t in m_TypeAndNamespace
                    where name.Equals(t.Key.FullName)
                    select t.Value).FirstOrDefault<NamespaceInfo>();
            if (null == r)
            {
                r = new NamespaceInfo();
                r.AddNamespace("System");
            }
            return r;
        }
        

        #region Construction/Destruction
        
        public AssemblyAnalyzer()
        {
            
        }

        //~AssemblyAnalyzer()
        //{
        //}
        #endregion


        #region IAssemblyAnalyzer Members

        public Hashtable AnalyzeAssembly(ArrayList assemblyFiles, bool choice)
        {
            try
            {
                m_IsClassSchema = choice;
                m_TypeAndMembers.Clear();
                m_TypeAndNamespace.Clear();
                basicDataTypes = new List<string>(8);
                basicDataTypes.Add("System.Int32");
                basicDataTypes.Add("System.Int64");
                basicDataTypes.Add("System.Double");
                basicDataTypes.Add("System.Single");
                basicDataTypes.Add("System.Boolean");
                basicDataTypes.Add("System.Decimal");
                basicDataTypes.Add("System.String");
                basicDataTypes.Add("System.Void");
                basicDataTypes.Add("Custom.Never.Used.By.Anybody");
                foreach (string assemblyFile in assemblyFiles)
                {
                    LoadAssembly(assemblyFile);
                    IdentifyKnownTypes();
                    AnalyzeAssemblyInDepth();
                    Cleanup();
                }
            }
            catch (Exception e)
            {
                Logger.write(e);
                throw;
            }
            return m_TypeAndMembers;
        }

        public Hashtable TypesAndMembers
        {
            get
            {
                return m_TypeAndMembers;
            }
        }

        private void Cleanup()
        {
            for (int i = 0; i < m_TypeAndMembers.Count; i++)
            {
                //m_TypeAndMembers
            }
        }
        
        #endregion

        
        #region Other Methods
        private void LoadAssembly(string dllfilename)
        {
            try
            {
                m_ObjAssembly = System.Reflection.Assembly.LoadFrom(dllfilename);
                
            }
            catch (Exception e)
            {
                Logger.write(e);
                throw;
            }
        }

        private void IdentifyKnownTypes()
        {
            try
            {
                Type[] arrayOfTypes;
                arrayOfTypes = m_ObjAssembly.GetExportedTypes();
                foreach (Type t in arrayOfTypes)
                {
                    //if ((!t.IsInterface) && m_IsClassSchema || !t.IsClass && !m_IsClassSchema)
                    {
                        
                        if (m_TypeAndNamespace.ContainsKey(t))
                        {
                            NamespaceInfo namespaceInfo = (NamespaceInfo)m_TypeAndNamespace[t];
                            namespaceInfo.AddNamespace(CustomizedNamespace(t));
                            m_TypeAndNamespace[t] = namespaceInfo;
                        }
                        else
                        {
                            NamespaceInfo namespaceInfo = new NamespaceInfo();
                            namespaceInfo.AddNamespace(CustomizedNamespace(t));
                            m_TypeAndNamespace.Add(t, namespaceInfo);
                        }
                    }
                }

                foreach (KeyValuePair<Type, NamespaceInfo> de in m_TypeAndNamespace)
                {
                    de.Value.DetermineCommonHierarchy();
                }
            }
            catch (Exception e)
            {
                Logger.write(e);
                throw;
            }
        }

        public static string NameCleanup(string name)
        {
            if (null != name)
            {
                //Console.WriteLine(name);
                string t = name.Replace('+', '_').Replace('`', '_').Replace("&lt;", "").Replace("&gt;", "").Replace("&", "").
                    Replace("<", "").Replace(">", "").Replace("{", "").Replace("}", "").Replace("+", "");
                //Console.WriteLine(t);
                return t;
            }
            return name;
        }

        public static string CustomizedNamespace(Type t)
        {
            string customNamespace = null;
            try
            {
                string fullName = t.FullName;
                if (null != fullName)
                {
                    fullName = NameHelper.NameCleanup(fullName);
                    int customNamespaceLength = fullName.LastIndexOf(CHAR_STOP_Constant);
                    if ((customNamespaceLength != -1))
                    {
                        customNamespace = fullName.Substring(0, customNamespaceLength);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.write(e);
                throw;
            }
            return customNamespace;
        }

        private void AnalyzeAssemblyInDepth()
        {
            try
            {
                Type[] arrayOfTypes;
                arrayOfTypes = m_ObjAssembly.GetExportedTypes();
                BindingFlags bindFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
                foreach (Type type in arrayOfTypes)
                {
                    //if (m_IsClassSchema || !myType.IsClass && !m_IsClassSchema)
                    {
                        TypeDescriptor typeInfo = new TypeDescriptor(this, type);
                        string typeName = type.Name;
                        if (type.IsGenericType)
                        {
                            int t;
                            typeName = typeName.Remove((t = typeName.LastIndexOf("`")) == -1 ? 0: t);

                            //MemberType[] ga = myType.GetGenericArguments();
                            //for (int i = 0; i < ga.Length; i++)
                            //{
                            //    MemberType[] gpc = ga[i].GetGenericParameterConstraints();
                            //    if (null != gpc)
                            //    {
                            //        for (int j = 0; j < gpc.Length; j++)
                            //        {
                            //            typeName +=  "_"+ gpc[j].Name;
                            //        }
                            //    }
                            //}
                        }
                        NamespaceInfo namespaceInfo = (NamespaceInfo)m_TypeAndNamespace[type];
                        string validSchemaFullName = String.Empty;
                        string CommonNamespace = namespaceInfo.CommonNamespaceName;
                        string typeNamespace = CustomizedNamespace(type);
                        TypeCategory typeCategory;
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
                        else if (type.IsAnsiClass)
                        {
                            continue;
                        }
                        else
                        {
                            typeCategory = TypeCategory.OtherType;
                        }
                        typeInfo.TypesCategory = typeCategory;

                        if (!CommonNamespace.Equals(String.Empty))
                        {
                            if (typeNamespace != CommonNamespace && typeNamespace.Length > CommonNamespace.Length)
                            {
                                validSchemaFullName = typeNamespace.Substring(CommonNamespace.Length + 1);
                            }
                            else
                            {
                                validSchemaFullName = string.Empty;
                            }
                        }
                        else
                        {
                            validSchemaFullName = typeNamespace;
                        }
                        if (validSchemaFullName != String.Empty && validSchemaFullName != null)
                        {
                            validSchemaFullName += STR_STOP_Constant;
                        }

                        validSchemaFullName += typeName;

                        ArrayList baseTypes = new ArrayList();
                        ArrayList fullBaseTypes = new ArrayList();
                        bool isTypeResolved = true;

                        if (type.IsClass || type.IsInterface)
                        {
                            Type baseType = type.BaseType;
                            string resolvedBaseTypeName;
                            if (null != baseType)
                            {
                                string ins = NamespaceInfo.ExtractIndividualNamespace(baseType.FullName, 0);
                                if (baseType.FullName != null)// && !Filter(ins))
                                {
                                    ;
                                    if (isTypeKnown(baseType, out resolvedBaseTypeName))
                                    {
                                        baseTypes.Add(resolvedBaseTypeName);
                                        fullBaseTypes.Add(baseType.FullName);
                                        isTypeResolved = true;
                                    }
                                    else
                                    {
                                        isTypeResolved = false;
                                    }
                                }
                            }
                            Type[] implementedInterfaces = type.GetInterfaces();
                            typeInfo.FullBaseTypeNames = new string[implementedInterfaces.Length];
                            int baseTypeCounter = 0;
                            foreach (Type implementedInterface in implementedInterfaces)
                            {
                                typeInfo.FullBaseTypeNames[baseTypeCounter] = implementedInterface.FullName;
                                baseTypeCounter++;
                                if (!Filter(NamespaceInfo.ExtractIndividualNamespace(implementedInterface.FullName, 0)))
                                {
                                    if (isTypeKnown(implementedInterface, out resolvedBaseTypeName))
                                    {
                                        baseTypes.Add(resolvedBaseTypeName);
                                        fullBaseTypes.Add(implementedInterface.FullName);

                                        isTypeResolved = true;
                                    }
                                    else
                                    {
                                        isTypeResolved = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    // The myType is derived from a .NET framework defined class
                                    // Not clear how to handle it
                                }

                            }
                        }
                        if (isTypeResolved)
                        {
                            typeInfo.ResolvedBaseTypeNames = new string[baseTypes.Count];
                            typeInfo.FullBaseTypeNames = new string[fullBaseTypes.Count];
                            // baseTypes.count and fullBaseTypes.Count must be same
                            for (int listIterator = 0; listIterator < baseTypes.Count; listIterator++)
                            {
                                typeInfo.ResolvedBaseTypeNames[listIterator] = (string)baseTypes[listIterator];
                                typeInfo.FullBaseTypeNames[listIterator] = (string)fullBaseTypes[listIterator];
                            }
                        }

                        MethodInfo[] methodInfos = type.GetMethods(bindFlags);
                        ConstructorInfo[] cinfos = type.GetConstructors(bindFlags);
                        MethodDescriptor[] methodDesc = new MethodDescriptor[methodInfos.Length+cinfos.Length];
                        List<MethodDescriptor> ml = new List<MethodDescriptor>();
                        int methodCounter = 0;
                        foreach (MethodInfo method in methodInfos)
                        {
                            methodDesc[methodCounter] = new MethodDescriptor(this, method);
                            if (!BaseContains<MethodInfo>(type, method))
                            {
                                methodDesc[methodCounter].CheckOverriding(type.BaseType);
                                methodDesc[methodCounter].CheckResolveStatus();
                                ml.Add(methodDesc[methodCounter]);
                            }
                            methodCounter++;
                        }
                        
                        foreach (ConstructorInfo method in cinfos)
                        {
                            methodDesc[methodCounter] = new MethodDescriptor(this, method);
                            if (!BaseContains<ConstructorInfo>(type, method))
                            {
                                methodDesc[methodCounter].CheckOverriding(type.BaseType);
                                methodDesc[methodCounter].CheckResolveStatus();
                                ml.Add(methodDesc[methodCounter]);
                            }
                            methodCounter++;
                        }

                        typeInfo.MethodsDescriptors = ml.ToArray();

                        EventInfo[] eventInfos = type.GetEvents(bindFlags);
                        EventDescriptor[] eventDesc = new EventDescriptor[eventInfos.Length];
                        int eventCounter = 0;
                        foreach (EventInfo eventInfo in eventInfos)
                        {
                            eventDesc[eventCounter] = new EventDescriptor(this, eventInfo);
                            eventDesc[eventCounter].CheckResolveStatus();
                            eventCounter++;
                        }

                        typeInfo.EventsDescriptors = eventDesc;
                        PropertyInfo[] propInfos = type.GetProperties(bindFlags);
                        PropertyDescriptor[] propDesc = new PropertyDescriptor[propInfos.Length];
                        int propCounter = 0;
                        foreach (PropertyInfo propInfo in propInfos)
                        {
                            propDesc[propCounter] = new PropertyDescriptor(this, propInfo);
                            propDesc[propCounter].CheckResolveStatus();
                            propCounter++;
                        }
                        typeInfo.PropertiesDescriptors = propDesc;

                        FieldInfo[] fieldInfos = type.GetFields(bindFlags);
                        FieldDescriptor[] fieldDesc = new FieldDescriptor[fieldInfos.Length];
                        int fieldCounter = 0;
                        foreach (FieldInfo fieldInfo in fieldInfos)
                        {
                            fieldDesc[fieldCounter] = new FieldDescriptor(this, fieldInfo);
                            fieldDesc[fieldCounter].CheckResolveStatus();
                            fieldCounter++;
                        }
                        typeInfo.FieldsDescriptors = fieldDesc;
                        typeInfo.IsResolved = isTypeResolved;

                        if (!m_TypeAndMembers.ContainsKey(type.FullName))
                        {
                            m_TypeAndMembers.Add(type.FullName, typeInfo);
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

        private bool BaseContains<T>(Type type, T method) where T: MethodBase
        {
            MethodInfo methodInfo = null;
            List<Type> baseTypes = new List<Type>();
            if (type.BaseType != null)
            {
                baseTypes.Add(type.BaseType);
            }
            baseTypes.AddRange(type.GetInterfaces());
            foreach (Type bt in baseTypes)
            {
                ParameterModifier[] modifiers;
                Type[] getParamTypes ;
                if (method.IsGenericMethod)
                {
                    try
                    {
                        MethodInfo[] ms = bt.GetMethods(BindingFlags.Public | BindingFlags.Instance);
                        T[] ts = ms.Cast<T>().ToArray<T>();
                        var expr = Extension.Expr((T m) => m.Name.Equals(method.Name));
                        var sms = ts.Where<T>(expr.Compile());
                        int cnt = sms.Count<T>();
                        ParameterModifier[] othermods;
                        Type[] otherPTypes = Descriptor.GetParamTypes<T>(method, out othermods);
                        foreach (T t in sms)
                        {
                            getParamTypes = Descriptor.GetParamTypes<T>(t, out modifiers);
                            if (CompareParamTypes(otherPTypes, getParamTypes))
                            {
                                return true;
                            }
                        }
                    }
                    catch(Exception)
                    {
                        // we got generic overloads..
                    }
                }
                else
                {
                    getParamTypes = Descriptor.GetParamTypes<T>(method, out modifiers);
                    methodInfo = bt.GetMethod(method.Name, BindingFlags.Instance | BindingFlags.Public, null, getParamTypes, modifiers);
                }
                if (null != methodInfo)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CompareParamTypes(Type[] otherPTypes, Type[] getParamTypes)
        {
            bool r = false;
            if (otherPTypes == getParamTypes)
            {
                return true;
            }
            if (null != otherPTypes && getParamTypes != null)
            {
                if (otherPTypes.Length == getParamTypes.Length)
                {
                    r = true;
                    for (int i = 0; i < otherPTypes.Length; i++)
                    {
                        if (!otherPTypes[i].Name.Equals(getParamTypes[i].Name))
                        {
                            return false;
                        }
                    }
                }
            }
            return r;
        }



        private bool Filter(string ins)
        {
            if (null != ins)
            {
                if (ins.StartsWith("System") || ins.StartsWith("MS.") || ins.StartsWith("_"))
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        public bool isTypeKnown(Type type, out string validSchemaFullName)
        {
            try
            {
                if (type == null)
                {
                    validSchemaFullName = "";
                    return false;
                }
                validSchemaFullName = String.Empty;
                if (null != type.FullName && Filter(NamespaceInfo.ExtractIndividualNamespace(type.FullName, 0)))
                {
                    // The myType is built-in myType defined by the .NET framework and is hence 
                    // it is to be checked if the myType is one of the basic datatypes or not.
                    //Also trim the ampersand at the end which exists for parameters passed by reference
                    if (basicDataTypes.Contains(type.FullName.TrimEnd(new char[] { '&' })))
                    {
                        validSchemaFullName = type.FullName;
                        return true;
                    }
                    // The myType is a complex myType that is defined in the .NET framework. Such 
                    // types are treated as not resolved 
                    else
                    {
                        return false;
                    }

                }
                else if (m_TypeAndNamespace.ContainsKey(type))
                {
                    // The myType is user-defined myType and the definition is available 
                    // within the assembly(s) that is being analysed.
                    NamespaceInfo namespaceInfo = (NamespaceInfo)m_TypeAndNamespace[type];
                    if (namespaceInfo.ContainsNamespace(CustomizedNamespace(type)))
                    {
                        string CommonNamespace = namespaceInfo.CommonNamespaceName;
                        string typeNamespace = CustomizedNamespace(type);
                        if (!String.IsNullOrEmpty(CommonNamespace) && !string.IsNullOrEmpty(typeNamespace))
                        {
                            if (typeNamespace != CommonNamespace && typeNamespace.Length > CommonNamespace.Length)
                            {
                                validSchemaFullName = typeNamespace.Substring(CommonNamespace.Length + 1);
                            }
                            else
                            {
                                validSchemaFullName = String.Empty;
                            }

                        }
                        else
                        {
                            validSchemaFullName = typeNamespace;
                        }
                        if (validSchemaFullName != String.Empty && validSchemaFullName != null)
                        {
                            validSchemaFullName += STR_STOP_Constant;
                        }

                        validSchemaFullName += type.Name;


                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
                else
                {
                    // The myType is unknown within the assembly(s) that is being analysed
                    return false;
                }
            }
            catch (Exception e)
            {
                Logger.write(e.Message);
                throw;
            }
        }
        #endregion
    }

}
#region Copyright

//------------------------------------------------------------------------
//  
//------------------------------------------------------------------------
#endregion