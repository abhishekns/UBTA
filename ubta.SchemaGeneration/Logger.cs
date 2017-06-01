#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : Logger.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System;
using System.IO;

namespace ubta.SchemaGeneration
{
    public static class Logger
    {
        private const string LOGFILENAME = "logFile.txt";
        private static string logFile = LOGFILENAME;

        public static void CreateLogFile(String logFilePath)
        {
            logFile = logFilePath + LOGFILENAME;
            using (StreamWriter sw = File.CreateText(logFile))
            {
                sw.WriteLine("Log of all exceptions");
                sw.Close();
            }
        }


        public static void write(Exception e)
        {
            using (StreamWriter sw = File.AppendText(logFile))
            {
                sw.WriteLine("Exception");
                sw.WriteLine("\tException MemberType : " + e.GetType());
                sw.WriteLine("\tMessage : " + e.Message);
                sw.WriteLine("\tStack Trace : " + e.StackTrace);
                sw.Close();
            }
        }

        public static void write(string ex)
        {
            using (StreamWriter sw = File.AppendText(logFile))
            {
                sw.WriteLine(ex);
                sw.Close();
            }

        }
    }
}