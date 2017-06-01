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
using System.IO;
using ubta.Common;
using ubta.SchemaGeneration;

namespace ubta.SchemaGenTest
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteSchema(Constants.ASSEMBLY_DIR + @"\SampleLib.dll");
            WriteSchema(Constants.ASSEMBLY_DIR + @"\ubta.Assert.dll");
            WriteSchema(@"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\system.dll");
        }

        private static void WriteSchema(string assName)
        {
            IAssemblyAnalyzer anal = new AssemblyAnalyzer();
            ArrayList newArrayList = new ArrayList();
            newArrayList.Add(assName);
            Hashtable ht = anal.AnalyzeAssembly(newArrayList, true);
            SchemaWriter sw = new SchemaWriter(anal, assName);
            sw.OutputDir = Constants.DEFAULT_SCHEMA_DIR;
            sw.WriteSchemas();
        }
    }
}