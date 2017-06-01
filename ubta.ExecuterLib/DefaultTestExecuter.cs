#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : DefaultTestExecuter.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System;
using System.Xml;
using ubta.Common;

namespace ubta.ExecuterLib
{
    public class DefaultTestExecuter : DefaultExecuter
    {

        public void ExecuteUseCaseFile(string path)
        {
            XmlDocument doc = SchemaHelper.SetupDoc(path, Constants.DEFAULT_SCHEMA_DIR, new GetRoot(GetRoot));
            ExecuteUseCaseFile(doc);
        }
        
        public void ExecuteUseCaseFile(XmlDocument doc)
        {
            SetupExec();
            ExecuteCalls(doc[Constants.USECASE_FILE_ROOT_NODE_NAME][Constants.SETUP_NODE_NAME][Constants.USECASE_NODE_NAME]);
            ExecuteTestCases(doc[Constants.USECASE_FILE_ROOT_NODE_NAME][Constants.TESTCASES_NODE_NAME]);
        }

        public void ExecuteTestCases(XmlNode testcases)
        {
            foreach (XmlNode x in testcases.ChildNodes)
            {
                ExecuteTestCase(x);
            }
        }

        public void ExecuteTestCase(XmlNode testCase)
        {
            ExecuteCalls(testCase[Constants.PRECOND_NODE_NAME]);
            ExecuteCalls(testCase[Constants.USECASE_NODE_NAME]);
            ExecuteCalls(testCase[Constants.POSTCOND_NODE_NAME]);
        }

        protected override string DefaultConfigFile
        {
            get
            {
                return Environment.ExpandEnvironmentVariables(@"%UBTA_HOME%\ubta.ExecuterLib\Config\TestConfig.txt");
            }
        }

        protected override XmlNode GetRoot(XmlDocument doc)
        {
            return doc[Constants.USECASE_FILE_ROOT_NODE_NAME];
        }

    }
}