#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta.AssertTest
    Name            : Program.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion

using System;
using System.Linq.Expressions;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.CompilerServices;

using assert = ubta.Assert.Assert;

using Recorder = ubta.Assert.Recorder;

namespace ubta.AssertTest
{
    class Program : ubta.Assert.TestRecorder
    {
        public static ubta.Assert.IValueChecker That1(object arg_in, [CallerArgumentExpression("arg_in")] string objName = null)
        {
            System.Console.WriteLine("Argument Name: "+ objName);
            return ubta.Assert.Assert.That(arg_in, objName);
        }

        public static string GetMemberName1<T>(Expression<Func<T>> anExpression)
        {
            return nameof(anExpression);
        }

        public static void LogParamName()
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            ParameterInfo[] _parameters = methodBase.GetParameters();
            foreach (ParameterInfo parameter in _parameters)
                System.Console.WriteLine (parameter.Name);
        }

        private static void DoSomething(string t, [CallerArgumentExpression("t")] string objName = null)
        {
            System.Console.WriteLine(objName);
            System.Console.WriteLine(GetMemberName(() => t));
        }
        static void Main1(string[] args)
        {
            string testName = "ubta.AssertTest.Program";
            DoSomething(testName);

        }

        static void Main(string[] args)
        {

            string t = "";
            try {
            That (t)     .IsNotNull    ();
            That (null)  .IsNull       ();
            That (t)     .Is           (typeof(System.String));
            That (2)     .IsEqualTo    (2);
            That (2)     .IsNotEqualTo (3);
            That (5)     .IsGreaterThan(4);
            That (2)     .IsLessThan   (5);
            var obj1 = new object();
            var obj2 = new object();
            That(obj1).And(obj2).AreNotNull();
            object obj3 = null;
            object obj4 = null;
            That(obj3).And(obj4).AreNotNull();

            string t1 = null;
            string t2 = null;
            string t3 = null;
            System.Console.WriteLine("t1 == t2 == t3 == null");

            string report = Record(
                new Recorder(() => {That(t).And(t1).And(t2).And(t3).AreNull();}),
                new Recorder(() => {That(t).IsNotNull();}));
            System.Console.WriteLine("Two records...");
            System.Console.WriteLine(report);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            System.Console.ReadLine();

        }
    }
}