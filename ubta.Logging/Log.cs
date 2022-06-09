using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;


namespace ubta
{
    public class Util
    {
        public static string GetHomeDir()
        {
            string homeDir = Environment.ExpandEnvironmentVariables(@"%UBTA_HOME%");
            if (homeDir.Contains("%UBTA_HOME%"))
            {
                homeDir = Assembly.GetExecutingAssembly().Location + @"..\..\..\..\..\";
                homeDir = Path.GetFullPath(homeDir);
                Environment.SetEnvironmentVariable("UBTA_HOME", homeDir);
                var sw = File.CreateText(@"c:\temp\ubta.init.log");
                sw.Write("UBTA_HOME : " + homeDir);
                sw.Close();
                sw.Dispose();
            }
            return homeDir;
        }
    }
}


namespace ubta.Logging
{
    internal class Writer
    {
        public static void Write(StreamWriter sw, string format, string domain, string message)
        {
            Write(sw, format, domain, message, null);
        }

        public static void Write(StreamWriter sw, string format, string domain, string message, Exception e)
        {
            string td = DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat);
            if (null == e)
            {
                sw.WriteLine(string.Format("{0} : {1}", td, string.Format(format, domain, message)));
            }
            else
            {
                sw.WriteLine(format, td, domain, string.Format("{0} : {1}", message, e.Message), e.StackTrace);
            }
            sw.Flush();
        }
    }
    public class Log
    {
        private Log()
        {
        }

        public static string LOG_FILE = ubta.Util.GetHomeDir() + @"\ubta.log";
        private static Log log = new Log();
        private StreamWriter myLogWriter = new StreamWriter(LOG_FILE);

        public static void Info(string domain, string message)
        {
            Writer.Write(log.myLogWriter, "Info: {0} : {1}", domain, message);
        }
        public static void Warn(string domain, string message)
        {
            Writer.Write(log.myLogWriter, "Warn: {0} : {1}", domain, message);
        }
        public static void Error(string domain, string message, Exception e)
        {
            Writer.Write(log.myLogWriter, "Error: {0} : {1}", domain, message);
        }
        public static void Debug(string domain, string message)
        {
            Writer.Write(log.myLogWriter, "Debug: {0} : {1}", domain, message);
        }
    }

    public class TraceInOut : IDisposable
    {
        public static string TRACE_FILE = ubta.Util.GetHomeDir() + "ubta.trace";
        protected static StreamWriter myTraceWriter = new StreamWriter(TRACE_FILE);

        private string myDomain;
        private string myMethod;
        public TraceInOut(string domain, string method)
        {
            myDomain = domain;
            myMethod = method;
            Writer.Write(myTraceWriter, "Entering: {0} : {1}", domain, method);
        }

        #region IDisposable Members

        public void Dispose()
        {
            Writer.Write(myTraceWriter, "Leaving: {0} : {1}", myDomain, myMethod);
        }

        #endregion
    }

    public class Trace : TraceInOut
    {
        public Trace()
            :base("", "")
        {
        }
        public static void Info(string domain, string message)
        {
            Writer.Write(myTraceWriter, "Info: {0} : {1}", domain, message);
        }
        public static void Warn(string domain, string message)
        {
            Writer.Write(myTraceWriter, "Warn: {0} : {1}", domain, message);
        }
        public static void Error(string domain, string message, Exception e)
        {
            Writer.Write(myTraceWriter, "Error: {0} : {1}", domain, message, e);
        }
        public static void Debug(string domain, string message)
        {
            Writer.Write(myTraceWriter, "Debug: {0} : {1}", domain, message);
        }
    }
}
