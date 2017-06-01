#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : SchemaWriter.cs
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using ubta.Common;
using ubta.Reflection;

namespace ubta.SchemaGeneration
{
    public class SchemaWriter : ubta.SchemaGeneration.ISchemaWriter
    {
        //private const string xsdFileName = "Schema0.xsd";
        private const string infoFileName = "UnresolvedTypeInfo.txt";
        //private StringBuilder unresolvedTypeInfo = new StringBuilder();
        private Hashtable m_TypesAndMembers;
        private List<string> unresolvedTypes = new List<string>();
        private string m_XsdFile;
        //string xsdFullFileName;
        string infoFullFileName;
        string outputPath;
        private string m_assemblyName;
        private string m_qName;
        private List<XmlSchema> mySchemas = new List<XmlSchema>();
        private Dictionary<NamespaceInfo, XmlSchema> myNamespaceSchemas = new Dictionary<NamespaceInfo, XmlSchema>();
        private XmlSchema mySchema = new XmlSchema();
        private XmlSchemaSet SchemaSet = new XmlSchemaSet();
        private XmlSchema myCommonSchema = new XmlSchema();
        private FileStream filestream;
        private IAssemblyAnalyzer myAa;
        private string myCurrentTypeName;
        private string myOutputDir = string.Empty;

        public XmlSchema Schema
        {
            get
            {
                return GetSchema(myCurrentTypeName);
            }
        }

        private const char myNsPrefix = 'q';
        private int myNsIndex = 0;
        private Dictionary<string, string> myNsPrefixes = new Dictionary<string, string>();

        public XmlQualifiedName GetQName(string name_in)
        {
            if (IsValueType(name_in) || name_in.Equals("System.Object") || name_in.Equals("System.Type"))
            {
                return new XmlQualifiedName(name_in, myCommonSchema.TargetNamespace);
            }
            else
            {
                string defns;
                if (GetDefinedNamespace(name_in, out defns))
                {
                    XmlSchema x = Schema;
                    XmlSchemaImport xi = new XmlSchemaImport();
                    xi.Namespace = Constants.DEFAULT_SCHEMA_NAMESPACE_PREFIX + defns;
                    xi.SchemaLocation = defns + ".xsd";
                    if (!x.Includes.Contains(xi))
                    {
                        string nsp;
                        if (!myNsPrefixes.TryGetValue(defns, out nsp))
                        {
                            myNsPrefixes.Add(defns, myNsPrefix.ToString() + myNsIndex);
                            myNsIndex++;
                        }
                        x.Namespaces.Add(Convert.ToString(myNsPrefixes[defns]), Constants.DEFAULT_SCHEMA_NAMESPACE_PREFIX + defns);
                        x.Includes.Add(xi);
                        WriteAnnotation(x);
                    }
                    return new XmlQualifiedName(name_in, Constants.DEFAULT_SCHEMA_NAMESPACE_PREFIX + defns);
                }
                return new XmlQualifiedName(name_in, Constants.DEFAULT_SCHEMA_NAMESPACE_PREFIX + GetNameSpace());
            }
        }

        private bool GetDefinedNamespace(string name_in, out string defns)
        {
            defns = string.Empty;
            NamespaceInfo nsi = myAa.GetNamespaceInfo(name_in);
            if (nsi.Namespaces != null)
            {
                string ns1 = GetNameSpace();
                string ns2 = nsi.Namespaces[0]; 
                if (!ns1.StartsWith(ns2) || !ns2.StartsWith(ns1))
                {
                    defns = nsi.Namespaces[0];
                    return true;
                }
            }
            return false;
        }

        public XmlQualifiedName GetQName(string name_in, string ns)
        {
            return new XmlQualifiedName(name_in, ns);
        }

        private string GetNameSpace(string typeName)
        {
            NamespaceInfo nsi = myAa.GetNamespaceInfo(typeName);
            string fn = nsi.CommonNamespaceName;
            if (string.IsNullOrEmpty(fn))
            {
                fn = nsi.Namespaces[0];
            }
            return fn;
        }

        private string GetNameSpace()
        {
            return GetNameSpace(myCurrentTypeName);
        }

        public SchemaWriter(IAssemblyAnalyzer iaa, string assemblyName)
        {
            myAa = iaa;
            m_TypesAndMembers = iaa.TypesAndMembers;
            m_assemblyName = assemblyName;
        }

        public SchemaWriter(string xsdFile, Hashtable typesAndMembers, string assemblyName)
        {
            m_XsdFile = xsdFile;
            m_TypesAndMembers = typesAndMembers;
            m_assemblyName = assemblyName;
            int index = xsdFile.LastIndexOf('\\');
            outputPath = xsdFile.Remove(index + 1);
            infoFullFileName = outputPath + infoFileName;
        }

        private void CreateSchemaFile()
        {
            try
            {

                filestream = new FileStream(m_XsdFile, FileMode.Create,
                FileAccess.ReadWrite, FileShare.Read);

                //All types that are not resolved are written to the UnresolvedTypeInfo file.
                using (StreamWriter sw = File.CreateText(infoFullFileName))
                {
                    sw.WriteLine("This file lists all the types and members for which schema is not written");

                }
            }
            catch (Exception e)
            {
                Logger.write(e);
                throw;
            }

        }

        private void AddToUnresolvedTypes(string theTypeName)
        {
            if (!unresolvedTypes.Contains(theTypeName))
            {
                unresolvedTypes.Add(theTypeName);
            }
        }

        private void WriteSchemaForUnresolvedTypes()
        {
            foreach (string unValidSchemaFullName in unresolvedTypes)
            {
                XmlSchemaSimpleType unresolvedType = new XmlSchemaSimpleType();
                unresolvedType.Name = unValidSchemaFullName;
                XmlSchemaSimpleTypeRestriction restrictionBaseString = new XmlSchemaSimpleTypeRestriction();
                unresolvedType.Content = restrictionBaseString;
                restrictionBaseString.BaseTypeName = GetQName(Constants.CLASS_STRING, myCommonSchema.TargetNamespace);
                Schema.Items.Add(unresolvedType);
            }
        }

        /// <summary>
        /// This method writes all the details relating to the fields(member variables) of the 
        /// class to the schema file.
        /// </summary>
        /// <param name="typeObj"></param>
        /// <param name="typeChoice"></param>
        private void WriteSchemaForFields(TypeDescriptor typeObj, XmlSchemaChoice typeChoice)
        {
            try
            {
                FieldDescriptor[] fieldDescriptors = typeObj.FieldsDescriptors;
                foreach (FieldDescriptor fieldDesc in fieldDescriptors)
                {
                    if (fieldDesc.IsResolved && fieldDesc.Information.DeclaringType.FullName.Equals(typeObj.FullTypeName))
                    {

                        XmlSchemaElement fieldElement = new XmlSchemaElement();
                        typeChoice.Items.Add(fieldElement);

                        fieldElement.Name = NameHelper.NameCleanup(fieldDesc.Information.Name);
                        fieldElement.SchemaTypeName = GetQName(NameHelper.NameCleanup(fieldDesc.MemberType));
                    }
                }
            }
            catch (Exception e)
            {
                Logger.write(e);
                throw;
            }
        }

        private void WriteSchemaForProperties(TypeDescriptor typeObj, XmlSchemaChoice typeChoice)
        {
            try
            {
                PropertyDescriptor[] propDescriptors = typeObj.PropertiesDescriptors;
                foreach (PropertyDescriptor propDesc in propDescriptors)
                {
                    if (propDesc.IsResolved)
                    {
                        XmlSchemaElement propertyElement = new XmlSchemaElement();
                        typeChoice.Items.Add(propertyElement);
                        propertyElement.Name = propDesc.Information.Name;
                        propertyElement.SchemaTypeName = GetQName(propDesc.MemberType);
                    }

                    // If the property myType is not resolved, then write it to UnresolvedTypeInfo file.
                    else
                    {
                        using (StreamWriter sw = File.AppendText(infoFullFileName))
                        {
                            sw.WriteLine("\tProperty :" + propDesc.Information.ToString());
                            sw.Close();
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

        private void WriteSchemaForEvents(TypeDescriptor typeObj, XmlSchemaChoice typeChoice)
        {
            try
            {
                EventDescriptor[] eventDescriptors = typeObj.EventsDescriptors;
                foreach (EventDescriptor eventDesc in eventDescriptors)
                {
                    if (eventDesc.IsResolved)
                    {
                        XmlSchemaElement eventElement = new XmlSchemaElement();
                        typeChoice.Items.Add(eventElement);
                        eventElement.Name = NameHelper.NameCleanup(eventDesc.EventInformation.Name);
                        eventElement.SchemaTypeName = GetQName(NameHelper.NameCleanup(eventDesc.MemberType)+".Invoke");
                    }
                }
            }
            catch (Exception e)
            {
                Logger.write(e);
                throw;
            }
        }

        private void WriteSchemaForMethods(TypeDescriptor typeObj, XmlSchemaChoice typeChoice)
        {
            try
            {
                Hashtable m_MethodsInfo = new Hashtable();
                MethodDescriptor[] methodDescriptors = typeObj.MethodsDescriptors;
                Type t = typeObj.Type;
                XmlSchema x = Schema;
                bool areDelegateMethods = typeof(System.Delegate).IsAssignableFrom(t);
                // Each method in the methodDescriptor is fetched and the schema is written if not the method
                // is a property or overrided.
                foreach (MethodDescriptor methodDescriptor in methodDescriptors)
                {
                    MethodInfo method;
                    ConstructorInfo cinfo;
                    bool isOverridden;
                    bool isResolved;
                    method = methodDescriptor.MethodInformation;
                    cinfo = methodDescriptor.ConstructorInfo;
                    isOverridden = null != method ? method.IsOverridden() : (null != cinfo ? cinfo.IsOverridden() : false );
                    isResolved = methodDescriptor.IsResolved;
                    bool isConstructor = methodDescriptor.IsConstructor;
                    

                    string methodPrefix = String.Empty;

                    if (isOverridden == false ||
                        ((typeObj.ResolvedBaseTypeNames == null || typeObj.ResolvedBaseTypeNames.Length == 0) &&
                          isOverridden == true && isResolved == true))
                    {
                        // An Hashtable m_MethodsInfo is built with key corresponding to the method name 
                        // and the value corresponding to an arraylist contaning all the information about 
                        // the method parameters.
                        string name = string.Empty;
                        if (isConstructor)
                        {
                            name = Constants.CONSTRUCTOR_NODE_NAME;
                        }
                        else
                        {
                            name = method.Name;
                        }
                        if (!m_MethodsInfo.ContainsKey(name))
                        {
                            ArrayList methodDescList = new ArrayList();             // create a arraylist 
                            methodDescList.Add(methodDescriptor);                   // add the methodinfo method to it 
                            m_MethodsInfo.Add(name, methodDescList);         // add to the  hashtable
                        }
                        else
                        {
                            // get the corresponding value (arraylist) for the key (method.name)
                            ArrayList methodDescList = (ArrayList)m_MethodsInfo[name];
                            methodDescList.Add(methodDescriptor);               // add the methodinfo to the existing arraylist
                            m_MethodsInfo[name] = methodDescList;        // adding to the hashtable
                        }
                    }
                }

                // Each entry in the hashTable M_methodsInfo is fetched and schema is written for the same.
                // If the entries is arraylist methodDescList is more than one it corresponds to overloaded
                // methods and hence 
                foreach (DictionaryEntry methodname in m_MethodsInfo)
                {
                    ArrayList methodDescList = (ArrayList)m_MethodsInfo[methodname.Key];

                    XmlSchemaElement methodElement = new XmlSchemaElement();
                    typeChoice.Items.Add(methodElement);
                    methodElement.Name = NameHelper.NameCleanup(methodname.Key.ToString());

                    XmlSchemaComplexType methodType = new XmlSchemaComplexType();
                    if (areDelegateMethods)
                    {
                        methodType.Name = t.FullName + "." + methodname.Key.ToString();
                        x.Items.Add(methodType);
                        methodElement.SchemaTypeName = GetQName(methodType.Name);
                    }
                    else
                    {
                        methodElement.SchemaType = methodType;
                    }
                    XmlSchemaChoice methodChoice = new XmlSchemaChoice();
                    // For handling of overloaded methods.
                    if (methodDescList.Count > 1)
                    {
                        methodChoice.MinOccurs = 0;
                        methodChoice.MaxOccurs = 1;
                        methodType.Particle = methodChoice;
                        for (int i = 0 ; i < methodDescList.Count ; i++)
                        {
                            MethodDescriptor methodDesc = (MethodDescriptor)methodDescList[i];
                            MethodInfo methodInfo = methodDesc.MethodInformation;
                            ParameterInfo[] parameters = methodDesc.parametersInfo;
                            // The number of parameters cannot be fixed because different overloaded methods
                            // have different number of parameters. Instead here, "use" is set to "required"

                            XmlSchemaSequence paramSequence = new XmlSchemaSequence();
                            methodChoice.Items.Add(paramSequence);

                            // Write the schema for the parameters for the method
                            if (parameters.Length > 0)
                            {
                                string name;
                                if (methodInfo == null)
                                {
                                    name = Constants.CONSTRUCTOR_NODE_NAME;
                                    if (i == 0)
                                    {
                                        if (typeObj.Type.IsGenericType)
                                        {
                                            Type[] ga = typeObj.Type.GetGenericArguments();
                                            for (int counter = 0; counter < ga.Length; counter++)
                                            {
                                                Type[] gpc = ga[counter].GetGenericParameterConstraints();
                                                for (int jcnt = 0; jcnt < gpc.Length; jcnt++)
                                                {
                                                    XmlSchemaElement xse = new XmlSchemaElement();
                                                    xse.Name = "GenericArg" + counter.ToString() + jcnt.ToString();
                                                    xse.SchemaTypeName = GetQName(Constants.CLASS_STRING, myCommonSchema.TargetNamespace);
                                                    //xse.FixedValue = gpc[jcnt].GetCodeDeclarationStatement();
                                                    paramSequence.Items.Add(xse);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    name = methodInfo.Name;
                                }
                                WriteSchemaForParameters(methodDesc, paramSequence, name, true, i);
                            }
                            if (null != methodInfo 
                                && null != methodInfo.ReturnParameter
                                && null != methodInfo.ReturnParameter.ParameterType
                                && null != methodInfo.ReturnParameter.ParameterType.FullName
                                && !methodInfo.ReturnParameter.ParameterType.FullName.Equals("System.Void"))
                            {
                                XmlSchemaElement retValue = GetRetValueElement(methodInfo.Name, methodInfo.ReturnParameter.ParameterType, i);
                                paramSequence.Items.Add(retValue);
                            }
                        }
                    }
                    // In case there is no overloaded definition but only one single method definition
                    else
                    {
                        //one def
                        MethodDescriptor methodDesc = (MethodDescriptor)methodDescList[0];
                        MethodInfo methodInfo = methodDesc.MethodInformation;
                        ConstructorInfo cinfo = methodDesc.ConstructorInfo;
                        ParameterInfo[] parameters = methodDesc.parametersInfo;

                        if (parameters.Length > 0)
                        {
                            XmlSchemaSequence paramSequence = new XmlSchemaSequence();
                            methodType.Particle = paramSequence;
                            WriteSchemaForParameters(methodDesc, paramSequence, NameHelper.NameCleanup(methodInfo != null ? methodInfo.Name : cinfo.Name), false, 0);
                        }
                        if (typeObj.Type.IsGenericType)
                        {
                            XmlSchemaSequence paramSequence = new XmlSchemaSequence();
                            methodType.Particle = paramSequence;
                            Type[] ga = typeObj.Type.GetGenericArguments();
                            for (int counter = 0; counter < ga.Length; counter++)
                            {
                                Type[] gpc = ga[counter].GetGenericParameterConstraints();
                                for (int jcnt = 0; jcnt < gpc.Length; jcnt++)
                                {
                                    XmlSchemaElement xse = new XmlSchemaElement();
                                    xse.Name = "GenericArg" + counter.ToString()  + jcnt.ToString();
                                    xse.SchemaTypeName = GetQName(Constants.CLASS_STRING, myCommonSchema.TargetNamespace);
                                    //xse.FixedValue = gpc[jcnt].GetCodeDeclarationStatement();
                                    paramSequence.Items.Add(xse);
                                }
                            }
                        }

                        
                        if (null != methodInfo)
                        {
                            if (!methodInfo.ReturnParameter.ParameterType.Name.Equals("Void"))
                            {
                                if (null == methodType.Particle)
                                {
                                    methodType.Particle = methodChoice;
                                    methodChoice.Items.Add(GetRetValueElement(methodInfo.Name, methodInfo.ReturnParameter.ParameterType));
                                }
                                else
                                {
                                    if (methodType.Particle is XmlSchemaSequence)
                                    {
                                        (methodType.Particle as XmlSchemaSequence).Items.Add(GetRetValueElement(methodInfo.Name, methodInfo.ReturnParameter.ParameterType));
                                    }
                                    if (methodType.Particle is XmlSchemaChoice)
                                    {
                                        (methodType.Particle as XmlSchemaChoice).Items.Add(GetRetValueElement(methodInfo.Name, methodInfo.ReturnParameter.ParameterType));
                                    }
                                }
                            }
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

        private XmlSchemaElement GetRetValueElement(string fname, Type retType, int i, bool useIndex)
        {
            XmlSchemaElement returnValue = new XmlSchemaElement();
            if (useIndex)
            {
                returnValue.Name = string.Format("{0}_rv_{1}", fname, i);
            }
            else
            {
                returnValue.Name = string.Format("{0}_rv", fname);
            }
            returnValue.MinOccurs = 0;

            XmlSchemaComplexType retValct = new XmlSchemaComplexType();
            retValct.Attributes.Add(GetNameAttribute());
            XmlSchemaAttribute retTypeAtt = GetNameAttribute();
            retTypeAtt.Name = "Type";
            retTypeAtt.Use = XmlSchemaUse.Optional;
            XmlQualifiedName qn;
            var td = GetTypeDescriptor(retType);
            XmlSchema x = null;
            if (string.IsNullOrEmpty(td.FullTypeName))
            {
                if (retType.IsGenericType)
                {
                    qn = GetQName(retType.GetGenericArguments()[0].ReflectedType.FullName);
                }
                else
                {
                    qn = GetQName("System.Object");
                    x = myCommonSchema;
                }
            }
            else
            {
                qn = GetQName(td.FullTypeName);
                x = GetSchema(td.FullTypeName);
            }
            string ns;
            if (null != x)
            {
                ns = (from n in x.Namespaces.ToArray() where n.Namespace == qn.Namespace select n.Name).FirstOrDefault();
                retTypeAtt.FixedValue = string.Format("{0}:{1}", ns, qn.Name);
            }
            else if (myNsPrefixes.TryGetValue(qn.Namespace, out ns))
            {
                retTypeAtt.FixedValue = string.Format("{0}:{1}", ns, qn.Name);
            }
            else
            {
                retTypeAtt.FixedValue = string.Format("{0}:{1}", qn.Namespace, qn.Name);
            }
            retValct.Attributes.Add(retTypeAtt);
            returnValue.SchemaType = retValct;
            return returnValue;
        }

        private XmlSchemaElement GetRetValueElement(string fname, Type retType, int i)
        {
            return GetRetValueElement(fname, retType, i, true);
        }

        private XmlSchemaElement GetRetValueElement(string fname, Type retType)
        {
            return GetRetValueElement(fname, retType, 0, false);
        }

        private XmlSchemaAttributeGroup GetNameOrValueAttGroup()
        {
            XmlSchemaAttributeGroup xsag = new XmlSchemaAttributeGroup();
            XmlSchemaAttribute nameAtt = GetNameAttribute();
            nameAtt.Use = XmlSchemaUse.Optional;
            XmlSchemaAttribute valueAtt = GetNameAttribute();
            valueAtt.Name = Constants.VALUE_NODE_ATTRIBUTE;
            valueAtt.Use = XmlSchemaUse.Optional;
            xsag.Attributes.Add(nameAtt);
            xsag.Attributes.Add(valueAtt);
            xsag.Name = Constants.NAME_OR_VALUE_ATT_GROUP_NAME;
            return xsag;
        }

        private XmlSchemaAttribute GetNameAttribute()
        {
            XmlSchemaAttribute nameAtt = new XmlSchemaAttribute();
            nameAtt.Name = Constants.NAME_NODE_ATTRIBUTE;
            nameAtt.SchemaTypeName = GetQName(Constants.CLASS_STRING, myCommonSchema.TargetNamespace);
            nameAtt.Use = XmlSchemaUse.Required;
            return nameAtt;
        }

        /// <summary>
        /// This method writes the schema content for the parameters of the methods defined in the Assemblies. 
        /// </summary>
        /// <param name="methodDesc">The method whose parameter details is being written to the schema</param>
        /// <param name="paramSequence"></param>
        /// <param name="modifiedMethodName">The method name is modified based on the number of overloaded 
        ///             definitions that exist for this method. This is to avoid nameclashes in Schema</param>
        private void WriteSchemaForParameters(MethodDescriptor methodDesc, XmlSchemaSequence paramSequence,
            string modifiedMethodName, bool isOverloaded, int index_in)
        {
            try
            {
                MethodInfo methodInfo = methodDesc.MethodInformation;

                ParameterInfo[] parameters = methodDesc.IsConstructor ? methodDesc.ConstructorInfo.GetParameters() : methodInfo.GetParameters();
                string[] paramsType = methodDesc.ParametersType;
                string[] paramsCategory = methodDesc.ParametersCategory;
                List<bool> paramIsResolved = methodDesc.paramsIsResolved;
                int paramCounter = 0;
                string paramName = string.Empty;
                foreach (ParameterInfo parameter in parameters)
                {
                    paramName = NameHelper.NameCleanup(parameter.Name);
                    if (isOverloaded)
                    {
                        paramName = paramName + '_' + index_in;
                    }
                    bool t = true;
                    if (paramIsResolved.Count > paramCounter)
                    {
                        t = paramIsResolved[paramCounter];
                    }
                    XmlSchemaElement paramElement = WriteSchemaForParameterDetails(paramName, paramsType[paramCounter], paramsCategory[paramCounter], t, false);

                    paramSequence.Items.Add(paramElement);
                    paramCounter++;
                }

                //ParameterInfo returnParameter = methodInfo.ReturnParameter;
                #region Return value
                if (!AreEqual(paramsType[paramCounter], "System.Void"))
                {
                    // RetValCondition and the two returnValue elements may appear as a group or may not appear at all.
                    XmlSchemaChoice returnValueChoice = new XmlSchemaChoice();
                    returnValueChoice.MinOccurs = 0;
                    returnValueChoice.MaxOccurs = 1;

                    XmlSchemaSequence returnValueSequence = new XmlSchemaSequence();
                    returnValueChoice.Items.Add(returnValueSequence);

                    // Value of false is passed for IsParamResolved. The value is not applicable in this case because 
                    // for return parameter, paramValueType is not specified.
                    string rname = "RetVal";
                    if (isOverloaded)
                    {
                        rname += "_" + index_in.ToString();
                    }
                    XmlSchemaElement returnValue1 = WriteSchemaForParameterDetails(rname, paramsType[paramCounter], "", false, true);

                    //XmlSchemaElement returnValue1 = WriteSchemaForParameterDetails(modifiedMethodName + "RetVal", paramsType[paramCounter], "", true);

                    returnValueSequence.Items.Add(returnValue1);

                    rname = "RetVal_spec";
                    if (isOverloaded)
                    {
                        rname += "_" + index_in.ToString();
                    }
                    // Return Value2 may or may not appear in the xml.
                    // Value of false is passed for IsParamResolved. The value is not applicable in this case because 
                    // for return parameter, paramValueType is not specified.
                    XmlSchemaElement returnValue2 = WriteSchemaForParameterDetails(rname, paramsType[paramCounter], "", false, true);
                    // Return Value2 may or may not appear in the xml
                    returnValue2.MinOccurs = 0;
                    returnValue2.MaxOccurs = 1;
                    returnValueSequence.Items.Add(returnValue2);
                }
                #endregion
            }
            catch (Exception e)
            {
                Logger.write(e);
                throw;
            }
        }
        private bool AreEqual(string p, string p_2)
        {
            if (null != p)
            {
                return p.Equals(p_2);
            }
            return p == p_2;
        }

        /// <summary>
        /// This method writes the details of the parameters, namely, the parameter name, the parameter myType,
        /// the parameter category(In,Out or Ref) and whether it is a return parameter or not.
        /// </summary>
        /// <param name="paramName">The Name of the parameter</param>
        /// <param name="paramType">The datatype of the parameter</param>
        /// <param name="paramCategory">The Category(In, Out or Ref) of the parameter</param>
        /// <param name="isReturnParameter">Whether the parameter is return parameter or input parameter</param>
        /// <returns></returns>
        private XmlSchemaElement WriteSchemaForParameterDetails(string paramName, string paramType, string paramCategory, bool isResolved, bool isReturnParameter)
        {
            XmlSchemaElement paramElement = new XmlSchemaElement();
            //paramSequence.Items.Add(paramElement);
            paramElement.Name = NameHelper.NameCleanup(paramName);



            // Each parameter of a method is specified as an element (of complex myType) that has
            // "ParamType", "ParamCategory" and "ParamValueType" and Constants.NAME_NODE_ATTRIBUTE attributes

            XmlSchemaComplexType paramComplexType = new XmlSchemaComplexType();
            paramComplexType.IsMixed = true;
            paramElement.SchemaType = paramComplexType;
            // Adding the ParamType attribute to parameter deinition
            XmlSchemaAttribute paramTypeAttribute = new XmlSchemaAttribute();
            paramTypeAttribute.RefName = GetQName(Constants.PARAM_TYPE, myCommonSchema.TargetNamespace);
            paramTypeAttribute.FixedValue = NameHelper.NameCleanup(paramType);
            paramComplexType.Attributes.Add(paramTypeAttribute);
            XmlSchemaAttribute nameAtt = GetNameAttribute();
            paramComplexType.Attributes.Add(nameAtt);

            if (IsValueType(paramTypeAttribute.FixedValue) || IsCustomEnum(paramTypeAttribute.FixedValue))
            {
                nameAtt.Use = XmlSchemaUse.Optional;
                paramComplexType.Attributes.Add(GetValueAttribute(paramTypeAttribute.FixedValue));
            }

            // For parameters other than "return parameter", the ParameterCategory and ParamValueType attrbutes
            // are to be specified.
            if (!isReturnParameter)
            {
                // Adding the ParamCategory attribute to parameter definition
                XmlSchemaAttribute paramCategoryAttribute = new XmlSchemaAttribute();
                paramCategoryAttribute.RefName = GetQName(Constants.PARAM_CATEGORY, myCommonSchema.TargetNamespace);
                paramCategoryAttribute.FixedValue = paramCategory;
                paramComplexType.Attributes.Add(paramCategoryAttribute);

                // Adding the ParamValueType attribute to parameter definition
                XmlSchemaAttribute paramValueTypeAttribute = new XmlSchemaAttribute();
                paramValueTypeAttribute.RefName = GetQName(Constants.PARAM_VALUE_TYPE, myCommonSchema.TargetNamespace);
                paramValueTypeAttribute.Use = XmlSchemaUse.Required;
                paramValueTypeAttribute.DefaultValue = "ByValue";
                paramComplexType.Attributes.Add(paramValueTypeAttribute);
            }
            return paramElement;
        }

        private XmlSchemaObject GetValueAttribute(string type_in)
        {
            XmlSchemaAttribute valueAtt = new XmlSchemaAttribute();
            valueAtt.Name = Constants.VALUE_NODE_ATTRIBUTE;
            valueAtt.SchemaTypeName = GetQName(type_in);
            valueAtt.Use = XmlSchemaUse.Optional;
            return valueAtt;
        }

        private bool IsCustomEnum(string p)
        {
            XmlSchema x = Schema;
            for (int i = 0; i < x.Items.Count; i++)
            {
                XmlSchemaSimpleType xsst = x.Items[i] as XmlSchemaSimpleType;
                if (null != xsst)
                {
                    if (xsst.Name.Equals(p))
                    {
                        return true;
                    }
                }
            }
            IDictionaryEnumerator de = m_TypesAndMembers.GetEnumerator();
            while(de.MoveNext())
            {
                TypeDescriptor td = (de.Value as TypeDescriptor);
                if (td.Type.IsEnum)
                {
                    string name = (de.Key as string);
                    if (null != name && name.Equals(p))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsValueType(string p)
        {
            return new List<string>(Constants.VALUE_TYPE_CLASSES).Contains(p);
        }

        private XmlSchemaObject GetNameOrValueAttGroupRef()
        {
            XmlSchemaAttributeGroupRef xsagr = new XmlSchemaAttributeGroupRef();
            xsagr.RefName = GetQName(Constants.NAME_OR_VALUE_ATT_GROUP_NAME, myCommonSchema.TargetNamespace);
            return xsagr;
        }

        private void WriteSchemaForFields(TypeDescriptor typeObj, XmlSchemaSimpleType enumSimpleType)
        {
            try
            {
                FieldDescriptor[] fieldDescriptors = typeObj.FieldsDescriptors;
                XmlSchemaSimpleTypeRestriction restrictionBase = new XmlSchemaSimpleTypeRestriction();
                enumSimpleType.Content = restrictionBase;
                restrictionBase.BaseTypeName = GetQName(Constants.CLASS_STRING, myCommonSchema.TargetNamespace);
                for (int fieldCounter = 1 ; fieldCounter < fieldDescriptors.Length ; fieldCounter++)
                {
                    if (fieldDescriptors[fieldCounter].IsResolved)
                    {
                        XmlSchemaEnumerationFacet enumerationValue = new XmlSchemaEnumerationFacet();
                        restrictionBase.Facets.Add(enumerationValue);
                        enumerationValue.Value = fieldDescriptors[fieldCounter].Information.Name.ToString();

                    }

                }
            }
            catch (Exception e)
            {
                Logger.write(e);
                throw;
            }

        }

        private void WriteSchemaForMappingDatatypes()
        {
            AddSimpleType(Constants.CLASS_DOUBLE, "double");
            AddSimpleType(Constants.CLASS_INT32, "int");
            AddSimpleType(Constants.CLASS_INT64, "int");
            AddSimpleType(Constants.CLASS_SINGLE, "float");
            AddSimpleType(Constants.CLASS_BOOL, "boolean");
            AddSimpleType(Constants.CLASS_DECIMAL, "decimal");
            AddSimpleType(Constants.CLASS_STRING, "string");
        }

        private void AddSimpleType(string name_in, string restrictionBaseName)
        {
            XmlSchema x = myCommonSchema;
            XmlSchemaSimpleType simpleType = new XmlSchemaSimpleType();
            x.Items.Add(simpleType);
            simpleType.Name = name_in;
            XmlSchemaSimpleTypeRestriction restrictionBase = new XmlSchemaSimpleTypeRestriction();
            simpleType.Content = restrictionBase;
            restrictionBase.BaseTypeName = new XmlQualifiedName(restrictionBaseName, Constants.XMLSCHEMA_NAMESPACE);
        }

        private XmlSchemaSimpleType GetSimpleType(string name_in, string restrictionBaseName, params string[] enumfacets_in)
        {
            XmlSchemaSimpleType simpleType = new XmlSchemaSimpleType();
            XmlSchemaSimpleTypeRestriction restrictionBase = new XmlSchemaSimpleTypeRestriction();
            simpleType.Content = restrictionBase;
            restrictionBase.BaseTypeName = new XmlQualifiedName(restrictionBaseName, Constants.XMLSCHEMA_NAMESPACE);
            if (null != enumfacets_in)
            {
                for (int i = 0 ; i < enumfacets_in.Length ; i++)
                {
                    AddEnumFacet(restrictionBase, enumfacets_in[i]);
                }
            }
            return simpleType;
        }

        private void WriteSchemaForCommonDefinitions()
        {
            XmlSchema x = myCommonSchema;
            XmlSchemaSimpleType retValConditionType = GetSimpleType("RetValCondition", "string", "Equal", "Greater", "Lesser",
                "Not Equal", "Within Bounds", "Out Of Bounds", "Not Applicable");
            retValConditionType.Name = "RetValCondition";
            x.Items.Add(retValConditionType);

            // For defining ParamValueType attribute as an enumeration containing "ByName" and "ByValue" values
            XmlSchemaAttribute paramValueTypeAttribute = new XmlSchemaAttribute();
            paramValueTypeAttribute.Name = "ParamValueType";

            XmlSchemaSimpleType paramValueSimpleType = GetSimpleType("ParamValueAttributeType", "string", "ByName", "ByValue");
            paramValueTypeAttribute.SchemaType = paramValueSimpleType;

            x.Items.Add(paramValueTypeAttribute);

            // For defining ParamCategory attribute as an enumeration containing "In" "Out" and "Ref" values

            XmlSchemaAttribute paramCategoryAttribute = new XmlSchemaAttribute();
            paramCategoryAttribute.Name = "ParamCategory";

            XmlSchemaSimpleType paramCategorySimpleType = GetSimpleType("", "string", new string[] { ParamValueTypes.In.ToString(),
                ParamValueTypes.Out.ToString(), ParamValueTypes.Ref.ToString(), ParamValueTypes.StructType.ToString() });
            paramCategoryAttribute.SchemaType = paramCategorySimpleType;

            x.Items.Add(paramCategoryAttribute);

            // For defining NumParam attribute indicating the number of parameters for a method
            XmlSchemaAttribute numParamAttribute = new XmlSchemaAttribute();
            numParamAttribute.Name = "NumParam";
            numParamAttribute.SchemaTypeName = new XmlQualifiedName("int", Constants.XMLSCHEMA_NAMESPACE);
            x.Items.Add(numParamAttribute);

            // For defining paramType attribute indicating the datatype of the parameter for a method
            XmlSchemaAttribute paramTypeAttribute = new XmlSchemaAttribute();
            paramTypeAttribute.Name = Constants.PARAM_TYPE;
            paramTypeAttribute.SchemaTypeName = GetQName(Constants.CLASS_STRING, myCommonSchema.TargetNamespace);
            x.Items.Add(paramTypeAttribute);

            // For defining returnValueName element indicating the name for the rerurn value for a method.
            // This name would help in passing the output of one method as input to another method.
            //XmlSchemaElement returnValueName = new XmlSchemaElement();
            //returnValueName.Name = Constants.RETURNVALUE_NODE_NAME;
            //returnValueName.SchemaTypeName = GetQName(Constants.CLASS_STRING, myCommonSchema.TargetNamespace);
            //x.Items.Add(returnValueName);

            // For defining ParamFile attribute indicating the path of the meta file where parameter values are 
            // defined. This facilitates parameterization of test cases. This is an optional attribute.
            XmlSchemaAttribute paramFile = new XmlSchemaAttribute();
            paramFile.Name = "ParamFile";
            //paramFile.Use = XmlSchemaUse.Optional;
            paramFile.SchemaTypeName = GetQName(Constants.CLASS_STRING, myCommonSchema.TargetNamespace);
            x.Items.Add(paramFile);

            //XmlSchemaComplexType retValct = new XmlSchemaComplexType();
            //retValct.Name = Constants.RETURN_VALUE_COMPLEX_TYPE;
            //retValct.Attributes.Add(GetNameAttribute());
            //XmlSchemaAttribute retTypeAtt = GetNameAttribute();
            ////retTypeAtt.Name = "Type";
            ////retValct.Attributes.Add(retTypeAtt);
            //x.Items.Add(retValct);
            x.Items.Add(GetNameOrValueAttGroup());
            TypeDescriptor t = GetTypeDescriptor(typeof(object));

            XmlSchemaElement sysTypeName = new XmlSchemaElement();
            sysTypeName.Name = "System.Type";
            sysTypeName.SchemaTypeName = GetQName(Constants.CLASS_STRING, myCommonSchema.TargetNamespace);
            x.Items.Add(sysTypeName);

            WriteTypeSchema(t, null);
          //<xs:element name="RootNode">
          //  <xs:complexType>
          //    <xs:sequence maxOccurs="unbounded">
          //      <xs:any/>
          //    </xs:sequence>
          //  </xs:complexType>
          //</xs:element>
            XmlSchemaElement rootNode = new XmlSchemaElement();
            rootNode.Name = Constants.ROOT_ELEMENT_NODE_NAME.Replace("u:", "");
            XmlSchemaComplexType rnxsct = new XmlSchemaComplexType();
            rootNode.SchemaType = rnxsct;
            XmlSchemaSequence rnseq = new XmlSchemaSequence();
            rnxsct.Particle = rnseq;
            rnseq.MaxOccursString = "unbounded";
            rnseq.Items.Add(new XmlSchemaAny());
            x.Items.Add(rootNode);
        }

        private TypeDescriptor GetTypeDescriptor(Type type)
        {
            TypeDescriptor td = new TypeDescriptor(null, type);
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            td.ConstructorInfos = type.GetConstructors(flags);
            td.MethodsDescriptors = GetDescriptors<MethodDescriptor>(type.GetMethods(flags).Cast<MemberInfo>().ToArray<MemberInfo>());
            td.PropertiesDescriptors = GetDescriptors<PropertyDescriptor>(type.GetProperties(flags).Cast<MemberInfo>().ToArray<MemberInfo>());
            td.EventsDescriptors = GetDescriptors<EventDescriptor>(type.GetEvents(flags).Cast<MemberInfo>().ToArray<MemberInfo>());
            td.FieldsDescriptors = GetDescriptors<FieldDescriptor>(type.GetFields(flags).Cast<MemberInfo>().ToArray<MemberInfo>());
            return td;
        }

        private T1[] GetDescriptors<T1>(MemberInfo[] mInfos) where T1: Descriptor, new() 
        {
            T1[] fds = new T1[mInfos.Length];
            int i=0;
            foreach (MemberInfo mi in mInfos)
            {
                fds[i] = new T1();
                fds[i].Member = mi;
                fds[i].Analyzer = null;
                i++;
            }
            return fds;
        }

        private void AddEnumFacet(XmlSchemaSimpleTypeRestriction retValConditionRestiction, string p)
        {
            XmlSchemaEnumerationFacet enumerationEqual = new XmlSchemaEnumerationFacet();
            enumerationEqual.Value = p;
            retValConditionRestiction.Facets.Add(enumerationEqual);
        }

        public void WriteAnnotation(XmlSchema x)
        {
            XmlSchemaAnnotation schemaAnnotation = new XmlSchemaAnnotation();

            //Write the assemblyName in the appInfo tag.
            XmlSchemaAppInfo appInfo = new XmlSchemaAppInfo();
            appInfo.Markup = TextToNodeArray(m_assemblyName);
            schemaAnnotation.Items.Add(appInfo);
            x.Items.Add(schemaAnnotation);
            m_qName = m_assemblyName.Substring(0, m_assemblyName.Length - 4);
            m_qName = m_qName.Substring(m_qName.LastIndexOf("\\") + 1);

            x.TargetNamespace = Constants.DEFAULT_SCHEMA_NAMESPACE_PREFIX + m_qName;
            x.ElementFormDefault = XmlSchemaForm.Qualified;
            x.AttributeFormDefault = XmlSchemaForm.Unqualified;
        }

        public static XmlNode[] TextToNodeArray(string text)
        {
            XmlDocument doc = new XmlDocument();
            return new XmlNode[1] { doc.CreateTextNode(text) };
        }
        private void UIFixture()
        {
            XmlSchemaElement uiFixture = new XmlSchemaElement();
            uiFixture.Name = "ubta.UIFixture";
            XmlSchemaComplexType uiFixtureType = new XmlSchemaComplexType();
            //uiFixtureType.Name = "ubta.UIFixture";
            uiFixture.SchemaType = uiFixtureType;
            uiFixture.SchemaTypeName = uiFixtureType.QualifiedName;

            //x.Items.Add(uiFixture);
            //x.Items.Add(uiFixtureType);

            /// <xs:attribute name="id" myType="xs:ID" ></xs:attribute>
            /// <xs:attribute name="Explicit" myType="xs:boolean" use="required"></xs:attribute>
            /// <xs:attribute name="Order" myType="xs:int" use="required"></xs:attribute>
            /// <xs:attribute name="X" myType="xs:int"></xs:attribute>
            /// <xs:attribute name="Y" myType="xs:int"></xs:attribute>
            /// <xs:attribute name="Source" myType="string"></xs:attribute>
            /// <xs:attribute name="Destination" myType="string"></xs:attribute>

            AddSchemaAttribute("id", uiFixtureType, "ID");
            AddSchemaAttribute("Explicit", uiFixtureType, "boolean");
            AddSchemaAttribute("Order", uiFixtureType, "int");
            AddSchemaAttribute("X", uiFixtureType, "int");
            AddSchemaAttribute("Y", uiFixtureType, "int");
            AddSchemaAttribute("Connections", uiFixtureType, "string");
        }

        private XmlSchema GetSchema(string name)
        {
            XmlSchema x;
            if(!myNamespaceSchemas.TryGetValue(myAa.GetNamespaceInfo(name), out x))
            {
                return myCommonSchema;                     
            }
            return x;
        }

        public void WriteSchemas()
        {
            IDictionaryEnumerator de = myAa.TypesAndMembers.GetEnumerator();
            while (de.MoveNext())
            {
                string name = de.Key as string;
                Type type = de.Value as Type;
                myCurrentTypeName = name;
                NamespaceInfo ni = myAa.GetNamespaceInfo(name);
                if (!myNamespaceSchemas.ContainsKey(ni))
                {
                    XmlSchema s = new XmlSchema();
                    myNamespaceSchemas.Add(ni, s);
                    string fn = GetNameSpace();
                    s.TargetNamespace = Constants.DEFAULT_SCHEMA_NAMESPACE_PREFIX + fn;
                    s.Namespaces.Add("t", Constants.DEFAULT_SCHEMA_NAMESPACE_PREFIX + fn);
                    s.Namespaces.Add("u", Constants.DEFAULT_SCHEMA_NAMESPACE_PREFIX + "ubta.Schema");
                    s.ElementFormDefault = XmlSchemaForm.Qualified;
                    s.AttributeFormDefault = XmlSchemaForm.Unqualified;
                }
            }
            WriteSchema();
        }

        public void WriteSchema()
        {
            try
            {
                // Create the schema file in the specified path
                //CreateSchemaFile();
                //WriteAnnotation();

                XmlNamespaceManager nsm = new XmlNamespaceManager(new NameTable());
                nsm.AddNamespace("xs", Constants.XMLSCHEMA_NAMESPACE);
                myCommonSchema.TargetNamespace = Constants.DEFAULT_SCHEMA_NAMESPACE_PREFIX + "ubta.Schema";
                myCommonSchema.Namespaces.Add("u", myCommonSchema.TargetNamespace);

                WriteSchemaForMappingDatatypes();
                WriteSchemaForCommonDefinitions();


                XmlSchemaElement rootElement = new XmlSchemaElement();
                //x.Items.Add(rootElement);
                rootElement.Name = Constants.ROOT_ELEMENT_NODE_NAME;

                XmlSchemaComplexType complexTypeRootdef = new XmlSchemaComplexType();
                rootElement.SchemaType = complexTypeRootdef;
                XmlSchemaSequence seq = new XmlSchemaSequence();
                XmlSchemaChoice choice = new XmlSchemaChoice();
                choice.MinOccurs = 0;
                choice.MaxOccursString = "unbounded";

                seq.Items.Add(choice);

                complexTypeRootdef.Particle = seq;

                XmlSchemaAttribute descAttribute = new XmlSchemaAttribute();
                descAttribute.Name = "UseCaseDescription";
                complexTypeRootdef.Attributes.Add(descAttribute);

                // main loop for iterating the hashtable
                foreach (DictionaryEntry de in m_TypesAndMembers)
                {
                    WriteTypeSchema(de.Value as TypeDescriptor, choice);
                }
                using (FileStream fs = new FileStream( myOutputDir + "ubta.Schema.xsd", FileMode.Create))
                {
                    myCommonSchema.Write(fs, nsm);
                }
                XmlSchemaImport xi = new XmlSchemaImport();
                xi.Schema = myCommonSchema;
                xi.Namespace = myCommonSchema.TargetNamespace;
                xi.SchemaLocation = "ubta.Schema.xsd";
                foreach (KeyValuePair<NamespaceInfo, XmlSchema> kvp in myNamespaceSchemas)
                {
                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(new NameTable());
                    nsmgr.AddNamespace("xs", Constants.XMLSCHEMA_NAMESPACE);
                    string fn = kvp.Key.CommonNamespaceName;
                    if (string.IsNullOrEmpty(fn))
                    {
                        fn = kvp.Key.Namespaces[0];
                    }
                    kvp.Value.TargetNamespace = Constants.DEFAULT_SCHEMA_NAMESPACE_PREFIX + fn;
                    nsmgr.AddNamespace("t", kvp.Value.TargetNamespace);
                    nsmgr.AddNamespace("u", myCommonSchema.TargetNamespace);
                    kvp.Value.Includes.Add(xi);
                    using (FileStream fs = new FileStream(myOutputDir + fn + ".xsd", FileMode.Create))
                    {
                        kvp.Value.Write(fs, nsmgr);
                    }
                }

                // Validate
                SchemaSet.ValidationEventHandler += new ValidationEventHandler(ValidationCallbackOne);
                SchemaSet.XmlResolver = new DefaultXmlResolver();
                foreach (KeyValuePair<NamespaceInfo, XmlSchema> kvp in myNamespaceSchemas)
                {
                    SchemaSet.Add(kvp.Value);
                }
                SchemaSet.Add(myCommonSchema);
                SchemaSet.Compile();
            }
            catch (Exception e)
            {
                Logger.write(e);
                throw;
            }
            finally
            {
                if (null != filestream)
                {
                    filestream.Close();
                }
            }
        }

        private void WriteTypeSchema(TypeDescriptor typeObj, XmlSchemaChoice choice)
        {
            myCurrentTypeName = typeObj.Type.FullName;
            XmlSchema x = Schema;
            // Reference element is required only for the base types and not
            // for the derived types.For the derived elements it is automatically implied.
            if (typeObj.ValidSchemaFullName != null && typeObj.ValidSchemaFullName.Length != 0 && null != choice)
            {
                XmlSchemaElement refElement = new XmlSchemaElement();
                choice.Items.Add(refElement);
                refElement.RefName = GetQName(NameHelper.NameCleanup(typeObj.ValidSchemaFullName));
            }

            XmlSchemaElement elementType = new XmlSchemaElement();
            x.Items.Add(elementType);
            elementType.Name = typeObj.ValidSchemaFullName;
 
            if (typeObj.ResolvedBaseTypeNames != null)
            {
                foreach (string baseType in typeObj.ResolvedBaseTypeNames)
                {
                    elementType.SubstitutionGroup = GetQName(typeObj.Type.GetValidTypeNameForXml());
                }
            }
            if (!(typeObj.TypesCategory == TypeCategory.EnumType))
            {
                elementType.SchemaTypeName = GetQName(typeObj.ValidSchemaFullName);
                XmlSchemaChoice typeChoice = new XmlSchemaChoice();
                XmlSchemaComplexType complexType = new XmlSchemaComplexType();
                complexType.Name = typeObj.ValidSchemaFullName;
                x.Items.Add(complexType);

                XmlSchemaAttribute attributeName = new XmlSchemaAttribute();
                XmlSchemaAttribute attributeCreate = new XmlSchemaAttribute();
                XmlSchemaAttribute attributeDispose = new XmlSchemaAttribute();
                XmlSchemaAttribute attributeCreateCode = new XmlSchemaAttribute();
                XmlSchemaAttribute attributeDisposeCode = new XmlSchemaAttribute();
                if (typeObj.ResolvedBaseTypeNames != null && typeObj.ResolvedBaseTypeNames.Length > 0)
                {
                    XmlSchemaComplexContent complexContent = new XmlSchemaComplexContent();
                    complexType.ContentModel = complexContent;
                    XmlSchemaComplexContentExtension extensionBase = new XmlSchemaComplexContentExtension();
                    complexContent.Content = extensionBase;
                    extensionBase.BaseTypeName = GetQName(NameHelper.NameCleanup(typeObj.FullBaseTypeNames[0]));

                    // Attribute 'Name' is specified only for the base myType.This automatically
                    //ensures that this attribute is available for the derived types.
                    typeChoice.MinOccurs = 0;
                    typeChoice.MaxOccursString = "unbounded";
                    extensionBase.Particle = typeChoice;
                }
                else
                {
                    // XmlSchemaChoice typeChoice = new XmlSchemaChoice();
                    attributeName.Name = Constants.NAME_NODE_ATTRIBUTE;
                    complexType.Attributes.Add(attributeName);
                    attributeName.Use = XmlSchemaUse.Required;
                    attributeName.SchemaTypeName = GetQName(Constants.CLASS_STRING, myCommonSchema.TargetNamespace);

                    typeChoice.MinOccurs = 0;
                    typeChoice.MaxOccursString = "unbounded";
                    if (typeObj.Type.IsGenericType)
                    {
                        Type[] ga = typeObj.Type.GetGenericArguments();
                        for (int i = 0; i < ga.Length; i++)
                        {
                            Type[] gpc = ga[i].GetGenericParameterConstraints();
                            for (int j = 0; j < gpc.Length; j++)
                            {
                                XmlSchemaAttribute xsa = new XmlSchemaAttribute();
                                xsa.Name = "GenericArg_" + i + "_" + j;
                                xsa.SchemaTypeName = GetQName(Constants.CLASS_STRING, myCommonSchema.TargetNamespace);
                                xsa.FixedValue = gpc[j].GetCodeDeclarationStatement();
                                complexType.Attributes.Add(xsa);
                            }
                        }
                    }
                    complexType.Particle = typeChoice;
                }
                // Methods
                WriteSchemaForMethods(typeObj, typeChoice);
                WriteSchemaForFields(typeObj, typeChoice);
                WriteSchemaForEvents(typeObj, typeChoice);
            }
            else
            {
                elementType.SchemaTypeName = GetQName(NameHelper.NameCleanup(typeObj.FullTypeName));
                XmlSchemaSimpleType enumSimpleType = new XmlSchemaSimpleType();
                enumSimpleType.Name = NameHelper.NameCleanup(typeObj.FullTypeName);
                x.Items.Add(enumSimpleType);
                WriteSchemaForFields(typeObj, enumSimpleType);
            }
        }

        private void AddSchemaAttribute(string name_in, XmlSchemaComplexType complexType_in, string type_in)
        {
            XmlSchemaAttribute att = new XmlSchemaAttribute();
            att.Name = name_in;
            att.SchemaTypeName = new XmlQualifiedName(type_in, Constants.XMLSCHEMA_NAMESPACE);
            complexType_in.Attributes.Add(att);
        }

        private static void ValidationCallbackOne(object sender, ValidationEventArgs args)
        {
        }

        public string OutputDir
        {
            get
            {
                return myOutputDir;
            }
            set
            {
                myOutputDir = value;
            }
        }

        public string AssemblyLocation
        {
            get
            {
                return m_assemblyName;
            }
            set
            {
                m_assemblyName = value;
            }
        }
    }
}