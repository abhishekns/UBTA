#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : Assert.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ubta.Assert
{
    public interface ICondition
    {
        bool Evaluate();

        int recursion { get; }
    }

    class AssertFailedException : Exception
    {
        public AssertFailedException() { }
        public AssertFailedException(String message) : base(message) { }
    }

    public interface IValueChecker
    {
        bool DelayedEvaluation { get; set; }
        bool EvaluationResult { get; }

        ICondition AssertCondition();
        ICondition IsNotNull();
        ICondition IsNull();
        ICondition Is(Type type);
        ICondition IsEqualTo(object p, [CallerArgumentExpression("o")] string objName = null);
        ICondition IsNotEqualTo(object p, [CallerArgumentExpression("o")] string objName = null);
        ICondition IsGreaterThan<T>(T p, [CallerArgumentExpression("o")] string objName = null) where T : IComparable;
        ICondition IsLessThan<T>(T p, [CallerArgumentExpression("o")] string objName = null) where T : IComparable;
        ICondition AreNotNull();
        ICondition AreNull();
        ICondition Are(Type type);
        ICondition AreEqualTo(object p, [CallerArgumentExpression("p")] string objName = null);
        ICondition AreNotEqualTo(object p, [CallerArgumentExpression("p")] string objName = null);
        ICondition AreGreaterThan<T>(T p, [CallerArgumentExpression("p")] string objName = null) where T : IComparable;
        ICondition AreLessThan<T>(T p, [CallerArgumentExpression("p")] string objName = null) where T : IComparable;

        IValueChecker And(object o, [CallerArgumentExpression("o")] string objName = null);
        IValueChecker Or(object o, [CallerArgumentExpression("o")] string objName = null);
        IValueChecker Nand(object o, [CallerArgumentExpression("o")] string objName = null);

        string Name { get; }
    }

    public class Assert
    {
        public static IValueChecker That(object arg_in, string objName)
        {
            return new MultiValueChecker(arg_in, objName);
        }
    }

    public static class ObjectExtension
    {
        public static string Name(this object obj, [CallerArgumentExpression("obj")] string objName = null)
        {
            return objName;
        }
    }

    public delegate void Recorder();

    public class Records
    {
        private List<string> myParameterNames;
        private Recorder[] myRecords;
        public Records(params Recorder[] r)
        {
            myRecords = r;
        }

        public string report()
        {
            System.Text.StringBuilder ret = new System.Text.StringBuilder();
            foreach (Recorder r in myRecords)
            {
                try
                {
                    r.Invoke();
                }
                catch (Exception e)
                {
                    ret.AppendFormat("{0}{1}", e.Message, System.Environment.NewLine);
                }
            }
            return ret.ToString();
        }
    }

    internal enum OpType
    {
        None,
        And,
        Or,
        Nand,
    }

    public delegate IValueChecker ThatDel(object ivc);

    public delegate bool ConditionDel();

    internal class Condition : ICondition
    {
        private ConditionDel myConditionDel;
        private int myRecursion = 0;
        internal Condition(ConditionDel cond_in)
        {
            myConditionDel = cond_in;
        }

        public bool Evaluate()
        {
            myRecursion++;
            return myConditionDel();
        }

        public int recursion
        {
            get
            {
                return myRecursion;
            }
        }

    }

    internal interface IValueCheckerInternal : IValueChecker
    {
        int Recursion { get; set; }
        string Statement { get; }
        /*
        ICondition AreNotNullInternal();
        ICondition AreNullInternal();
        ICondition AreInternal(Type type);
        ICondition AreEqualToInternal(object p);
        ICondition AreNotEqualToInternal(object p);
        ICondition AreGreaterThanInternal<T>(T p) where T : IComparable;
        ICondition AreLessThanInternal<T>(T p) where T : IComparable;
        */
    }

    internal class MultiValueChecker : IValueCheckerInternal
    {
        private object myValue;
        private string myObjName;
        private string mySuffix;
        private System.Text.StringBuilder myStatement = new System.Text.StringBuilder();
        private bool myDelayedEvaluation = false;
        private bool myEvaluationResult = false;
        private ICondition myCondition;
        private IValueCheckerInternal myParent;
        private OpType myOpType;
        internal int myRecursion;

        public MultiValueChecker() { }

        public MultiValueChecker(object arg_in, string objName)
        {
            myOpType = OpType.None;
            myValue = arg_in;
            myParent = null;
            myObjName = objName;
            string suffixFormat = (myRecursion > 0) ? "({0})" : "";
            mySuffix = string.Format(suffixFormat, myRecursion);
            myStatement.AppendFormat("{0}{1}", myObjName, mySuffix);
        }

        private MultiValueChecker(IValueChecker parent_in, object other_in, OpType opType_in, string objName)
            : this(other_in, objName)
        {
            myParent = parent_in as IValueCheckerInternal;
            myParent.DelayedEvaluation = true;
            myOpType = opType_in;
            myRecursion = myParent.Recursion;
            myRecursion++;
            if (myOpType != OpType.None)
            {
                myStatement.AppendFormat(" {0} {1}", myOpType.ToString(), myParent.Name);
            }
        }

        public string Name
        {
            get
            {
                return string.Format("{0}{1}", myObjName, mySuffix);
            }
        }

        public string Statement
        {
            get
            {
                return myStatement.ToString();
            }
        }

        public MultiValueChecker that(object arg_in, string objName)
        {
            myOpType = OpType.None;
            myValue = arg_in;
            myParent = null;
            myObjName = objName;
            return this;
        }

        #region IMultiValueChecker Members

        public bool DelayedEvaluation
        {
            get
            {
                return myDelayedEvaluation;
            }
            set
            {
                myDelayedEvaluation = value;
            }
        }

        public ICondition IsNotNull()
        {
            myCondition = new Condition(new ConditionDel(delegate ()
            {
                myStatement.AppendFormat(" {0} is null.{1}", Name, Environment.NewLine);
                return (myValue != null);
            }
                ));
            return AssertCondition();
        }

        public ICondition IsNull()
        {
            myCondition = new Condition(new ConditionDel(delegate ()
            {
                myStatement.AppendFormat(" {0} is not null.{1}", Name, Environment.NewLine);
                return (myValue == null);
            }
                ));
            return AssertCondition();
        }

        public ICondition Is(Type type)
        {
            myCondition = new Condition(new ConditionDel(delegate ()
            {
                myStatement.AppendFormat("{0} is not of type {1}.{2}", Name, type.ToString(), Environment.NewLine);
                return (null != myValue && myValue.GetType().IsAssignableFrom(type));
            }));
            return AssertCondition();
        }

        public ICondition IsEqualTo(object o, [CallerArgumentExpression("o")] string objName = null)
        {

            myCondition = new Condition(new ConditionDel(delegate ()
            {
                myStatement.AppendFormat(" {0}={1} is not equal to {2}=3.{4}", Name, myValue, objName, o, Environment.NewLine);
                if (null != myValue)
                {
                    Type t = myValue.GetType();
                    if (t.IsValueType)
                    {
                        return myValue.Equals(o);
                    }
                }
                return (myValue != o);
            }
                ));
            return AssertCondition();
        }

        public ICondition IsNotEqualTo(object o, [CallerArgumentExpression("o")] string objName = null)
        {
            myCondition = new Condition(new ConditionDel(delegate ()
            {
                myStatement.AppendFormat(" {0}={1} is equal to {2}={3}.{4}", Name, myValue, objName, o, Environment.NewLine);
                return (myValue != o);
            }));
            return AssertCondition();
        }

        public ICondition IsGreaterThan<T>(T o, [CallerArgumentExpression("o")] string objName = null) where T : IComparable
        {
            myCondition = new Condition(new ConditionDel(delegate ()
            {
                myStatement.AppendFormat(" {0}={1} is not greater that {2}={3}.{4}", Name, myValue, objName, o, Environment.NewLine);
                return (o.CompareTo(myValue) < 0);
            }));
            return AssertCondition();
        }

        public ICondition IsLessThan<T>(T p, [CallerArgumentExpression("p")] string objName = null) where T : IComparable
        {
            myCondition = new Condition(new ConditionDel(delegate ()
            {
                myStatement.AppendFormat(" {0}={1} is not less than {2}={3}.{4}", Name, myValue, objName, p, Environment.NewLine);
                return (p.CompareTo(myValue) > 0);
            }));
            return AssertCondition();
        }

        public IValueChecker And(object o, [CallerArgumentExpression("o")] string objName = null)
        {
            return new MultiValueChecker(this, o, OpType.And, objName);
        }
        
        public IValueChecker Or(object o, [CallerArgumentExpression("o")] string objName = null)
        {
            return new MultiValueChecker(this, o, OpType.Or, objName);
        }

        public IValueChecker Nand(object o, [CallerArgumentExpression("o")] string objName = null)
        {
            return new MultiValueChecker(this, o, OpType.Nand, objName);
        }

        private void dummy()
        {
        }

        public ICondition AreNotNull()
        {
            myStatement.AppendFormat(" are null.{0}", Environment.NewLine);
            object t = myParent != null ? myParent.AreNotNull() : null;
            return IsNotNull();
        }

        public ICondition AreNull()
        {
            myStatement.AppendFormat(" are not null.{0}", Environment.NewLine);
            object t = myParent != null ? myParent.AreNull() : null;
            return IsNull();
        }

        public ICondition Are(Type type)
        {
            myStatement.AppendFormat(" are not of type {0}.{1}", type, Environment.NewLine);
            object t = myParent != null ? myParent.Are(type) : null;
            return Is(type);
        }

        public ICondition AreEqualTo(object o, [CallerArgumentExpression("o")] string objName = null)
        {
            myStatement.AppendFormat(" are not equal to {0}={1}.{2}", objName, o, Environment.NewLine);
            object t = myParent != null ? myParent.AreEqualTo(o) : null;
            return IsEqualTo(o);
        }

        public ICondition AreNotEqualTo(object o, [CallerArgumentExpression("o")] string objName = null)
        {
            myStatement.AppendFormat(" are equal to {0}={1}.{2}", objName, o, Environment.NewLine);
            object t = myParent != null ? myParent.AreNotEqualTo(o) : null;
            return IsNotEqualTo(o);
        }

        public ICondition AreGreaterThan<T>(T o, [CallerArgumentExpression("o")] string objName = null) where T : IComparable
        {
            myStatement.AppendFormat(" are greater than {0}={1}.{2}", objName, o, Environment.NewLine);
            object t = myParent != null ? myParent.AreGreaterThan<T>(o) : null;
            return IsGreaterThan<T>(o);
        }

        public ICondition AreLessThan<T>(T o, [CallerArgumentExpression("o")] string objName = null) where T : IComparable
        {
            myStatement.AppendFormat(" are less than {0}={1}.{2}", objName,  o, Environment.NewLine);
            object t = myParent != null ? myParent.AreLessThan<T>(o) : null;
            return IsLessThan<T>(o);
        }

        #endregion

        public ICondition AssertCondition()
        {
            myEvaluationResult = myCondition.Evaluate();
            if (null != myParent)
            {
                myParent.AssertCondition();
                switch (myOpType)
                {
                    case OpType.And:
                        {
                            myEvaluationResult = myEvaluationResult && myParent.EvaluationResult;
                            break;
                        }
                    case OpType.Or:
                        {
                            myEvaluationResult = myEvaluationResult || myParent.EvaluationResult;
                            break;
                        }
                    case OpType.Nand:
                        {
                            myEvaluationResult = !myEvaluationResult && !myParent.EvaluationResult;
                            break;
                        }
                    default:
                        break;
                }
            }
            if (!myEvaluationResult && !DelayedEvaluation)
            {
                throw new AssertFailedException(myStatement.AppendFormat("{0} {1}", myParent.Statement, myStatement.ToString()).ToString());
            }
            return myCondition;
        }

        #region IMultiValueChecker Members


        public bool EvaluationResult
        {
            get { return myEvaluationResult; }
        }

        public int Recursion
        {
            get;
            set;
        }

        #endregion
    }

    public class TestRecorder
    {
        public static ubta.Assert.IValueChecker That(object arg_in, [CallerArgumentExpression("arg_in")] string objName = null)
        {
            return ubta.Assert.Assert.That(arg_in, objName);
        }

        public static string Record(params Recorder[] r)
        {
            return new ubta.Assert.Records(r).report();
        }

        public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }

    }

}
