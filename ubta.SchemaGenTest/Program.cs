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
            System.Console.WriteLine(Constants.HOME);
            System.Console.WriteLine(Constants.DEPLOYMENT_DIR);
            System.Console.WriteLine(Constants.ASSEMBLY_DIR);
            if(args == null || args.Length == 0)
            {
                WriteSchema(Constants.ASSEMBLY_DIR + string.Format("{0}net7.0{0}SampleLib.dll", Path.DirectorySeparatorChar));
                WriteSchema(Constants.ASSEMBLY_DIR + string.Format("{0}net7.0{0}ubta.Assert.dll", Path.DirectorySeparatorChar));
                //WriteSchema(@"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\system.dll");
                WriteSchema("/usr/share/dotnet/shared/Microsoft.NETCore.App/7.0.14/System.dll");
            }
            else
            {
                if(File.Exists(args[0]))
                {
                    WriteSchema(args[0]);
                }
                else if(File.Exists(Constants.ASSEMBLY_DIR + args[0]))
                {
                    WriteSchema(Constants.ASSEMBLY_DIR + args[0]);
                }
            }
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