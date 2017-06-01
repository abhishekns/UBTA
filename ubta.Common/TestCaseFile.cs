#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : TestCaseFile.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System.Xml;

namespace ubta.Common
{
    public class TestCaseFile : XmlDocument
    {
         public XmlNode Setup
        {
            get
            {
                return this[Constants.USECASE_FILE_ROOT_NODE_NAME][Constants.SETUP_NODE_NAME][Constants.USECASE_NODE_NAME];
            }
        }

        public XmlNode Teardown
        {
            get
            {
                return this[Constants.USECASE_FILE_ROOT_NODE_NAME][Constants.TEARDOWN_NODE_NAME][Constants.USECASE_NODE_NAME];
            }
        }

        public XmlNode TestCases
        {
            get
            {
                return this[Constants.USECASE_FILE_ROOT_NODE_NAME][Constants.TESTCASES_NODE_NAME];
            }
        }

        public XmlNode GetPreCondition(string testCase_in)
        {
            return TestCases[testCase_in][Constants.PRECOND_NODE_NAME];
        }

        public XmlNode GetPostCondition(string testCase_in)
        {
            return TestCases[testCase_in][Constants.POSTCOND_NODE_NAME];
        }

        public XmlNode GetUseCase(string testCase_in)
        {
            return TestCases[testCase_in][Constants.USECASE_NODE_NAME];
        }
    }
}