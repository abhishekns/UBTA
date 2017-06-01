#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : Program.cs
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
using System.Xml;
using ubta.Common;
using ubta.ExecuterLib;
using ubta.UseCase;

namespace ubta.ExecuteTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region File execution
            DefaultExecuter eb = new DefaultExecuter();
            string path = Constants.CONFIG_DIR + @"\ExecuteTest.SampleLib.xml";
            eb.Assemblies.Add(Constants.ASSEMBLY_DIR+ @"\SampleLib.dll");

            eb.ExecuteXmlFile(path);
            IDictionaryEnumerator de = VariableManager.Instance.Mapping;
            while (de.MoveNext())
            {
                Console.WriteLine("Key = {0}, Value = {1}", de.Key.ToString(), de.Value.ToString());
            }

            #endregion

            #region file Ananlysis

            XmlDocument doc = SchemaHelper.SetupDoc(path, Constants.DEFAULT_SCHEMA_DIR, new GetRoot(GetExecFileRoot));

            UseCaseAnalyzer uca = new UseCaseAnalyzer(null, GetExecFileRoot(doc));
            UseCaseAnalysisResult ucar = uca.AnalyzeUseCase();
            UseCaseElement[] results = ucar.GetElements(ElementType.Undef).ToArray();
            for (int i = 0; i < results.Length; i++)
            {
                Console.WriteLine(results[i].ToString());
            }
            #endregion

            VariableManager.Instance.Reset();

            #region UseCase execution
            DefaultTestExecuter dte = new DefaultTestExecuter();
            path = Constants.CONFIG_DIR + @"\UseCaseFile1.xml";
            dte.Assemblies.Add(Constants.ASSEMBLY_DIR + @"\SampleLib.dll");
            dte.Assemblies.Add(Constants.ASSEMBLY_DIR + @"\ubta.Assert.dll");

            dte.ExecuteUseCaseFile(path);

            de = VariableManager.Instance.Mapping;
            while (de.MoveNext())
            {
                Console.WriteLine("Key = {0}, Value = {1}", de.Key.ToString(), de.Value.ToString());
            }
            #endregion

            #region UseCase analysis
            TestCaseFile tc = new TestCaseFile();
            doc = tc;
            tc = SchemaHelper.SetupDoc(path, Constants.DEFAULT_SCHEMA_DIR, new GetRoot(GetTestCaseFileRoot), ref doc) as TestCaseFile;

            foreach (XmlNode tcs in tc.TestCases.ChildNodes)
            {
                uca = new UseCaseAnalyzer(new List<XmlNode> { 
                tc.Setup, tcs[Constants.PRECOND_NODE_NAME] }, tcs[Constants.USECASE_NODE_NAME]);
                ucar = uca.AnalyzeUseCase();
                results = ucar.GetElements(ElementType.Undef).ToArray();
                Console.WriteLine(tcs[Constants.USECASE_NODE_NAME].Attributes[Constants.NAME_NODE_ATTRIBUTE].Value);
                for (int i = 0; i < results.Length; i++)
                {
                    Console.WriteLine(results[i].ToString());
                }
            }
            #endregion

            Console.ReadLine();
        }

        private static XmlNode GetExecFileRoot(XmlDocument doc_in)
        {
            return doc_in[Constants.ROOT_ELEMENT_NODE_NAME];
        }

        private static XmlNode GetTestCaseFileRoot(XmlDocument doc)
        {
            return doc[Constants.USECASE_FILE_ROOT_NODE_NAME];
        }
    }
}