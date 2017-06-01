#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : Constants.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : Constants.cs
    
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System;

namespace ubta.Common
{
    public class Constants
    {
        public static string HOME = Environment.ExpandEnvironmentVariables(@"%UBTA_HOME%");
        public static string DEPLOYMENT_DIR = string.Format(@"{0}\Deployment", HOME);
        public static string CONFIG_DIR = string.Format(@"{0}\Config", DEPLOYMENT_DIR);
        public static string DEFAULT_SCHEMA_DIR = string.Format(@"{0}\Schemas\", DEPLOYMENT_DIR);
        public static string ASSEMBLY_DIR = string.Format(@"{0}\bin\Debug\", DEPLOYMENT_DIR);

        public static string HOME_PATH = HOME;
        public static string METHOD_INFO_NODE_NAME_PREFIX = "Methods_";
        public static string PARAM_INFO_NODE_NAME_PREFIX = "ParamInfo_";
        public static string OVERLOADS_NODE_NAME_PREFIX = "Overload_";
        public static string CONSTRAINT_NODE_NAME_PREFIX = "Constraint_";

        public static string NAME_NODE_ATTRIBUTE = "Name";
        public static string VALUE_NODE_ATTRIBUTE = "Value";

        public static string ROOT_ELEMENT_NODE_NAME = "u:RootNode";
        public static string USECASE_FILE_ROOT_NODE_NAME = "p:TestCaseFile";
        public static string TESTCASES_NODE_NAME = "p:TestCases";
        public static string TESTCASE_NODE_NAME = "p:TestCase";
        public static string USECASE_NODE_NAME = "p:UseCase";
        public static string PRECOND_NODE_NAME = "p:PreCondition";
        public static string POSTCOND_NODE_NAME = "p:PostCondition";
        public static string SETUP_NODE_NAME = "p:Setup";
        public static string TEARDOWN_NODE_NAME = "p:Teardown";
        public static string METHODS_NODE_NAME = "Methods";
        public static string PARAMETERS_NODE_NAME = "Parameters";
        public static string PROPERTIES_NODE_NAME = "Properties";
        public static string GENERIC_PARAM_NODE_NAME = "_GenericParameters";
        public static string CLASSES_NODE_NAME = "Classes";
        public static string CONSTRUCTOR_NODE_NAME = "Constructor";
        public static string RETURNVALUE_NODE_NAME = "ReturnValue";
        public static string NAME_OR_VALUE_ATT_GROUP_NAME = "NameOrValueAttGroup";
        public static string CLASS_INT32 = "System.Int32";
        public static string CLASS_INT64 = "System.Int64";
        public static string CLASS_DOUBLE = "System.Double";
        public static string CLASS_SINGLE = "System.Single";
        public static string CLASS_BOOL = "System.Boolean";
        public static string CLASS_DECIMAL = "System.Decimal";
        public static string CLASS_STRING = "System.String";
        public static string CLASS_VOID = "System.Void";
        public static string CLASS_OBJECT = "System.Object";

        public static string[] VALUE_TYPE_CLASSES = new string[] { CLASS_BOOL, CLASS_DECIMAL, CLASS_DOUBLE, CLASS_INT32, CLASS_INT64, CLASS_SINGLE, CLASS_STRING };

        public static string GET_PROPERTY_PREFIX = "get_";
        public static string SET_PROPERTY_PREFIX = "set_";

        public static string NAMESPACE_SYSTEM = "System";
        public static string NAMESPACE_MS = "MS";
        public static string INTERNAL_NAMESPACE_PREFIX = "_";

        public static string DEFAULT_SCHEMA_NAMESPACE_PREFIX = "http://www.usecasetests.com/";
        public static string XMLSCHEMA_NAMESPACE = "http://www.w3.org/2001/XMLSchema";
        public static string XSI_SCHEMALOCATION = "xsi:schemaLocation";

        public static string RETURN_VALUE_COMPLEX_TYPE = "ReturnValue";
        public static string PARAM_VALUE_TYPE = "ParamValueType";
        public static string PARAM_TYPE = "ParamType";
        public static string PARAM_CATEGORY = "ParamCategory";
        public static string PARAM_SEPARATOR = ", ";

        public static string LIB_PATH_FW_2_0 = @"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\";
        public static string LIB_PATH_FW_3_5 = @"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5";
        public static string LIB_PATH_FW_4_0 = @"C:\Program Files\Reference Assemblies\Microsoft\Framework\v4.0";

        public static string SYSTEM_DLL = "System.dll";
        public static string SYSTEM_CORE_DLL = "System.Core.dll";

        public static string SYSTEM_NS = "System";
        public static string SYSTEM_COMPONENTMODEL_NS = "System.ComponentModel";

        public static string DEFAULT_COMPILER_OPTIONS = "/t:library /utf8output";

        public const string DLL_EXT = ".dll";
    }


    public enum ParamValueTypes : int
    {
        In = 0,
        Out = 1,
        Ref = 2,
        StructType = 3
    }
}