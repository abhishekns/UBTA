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
        ICondition IsEqualTo(object p);
        ICondition IsNotEqualTo(object p);
        ICondition IsGreaterThan<T>(T p) where T : IComparable;
        ICondition IsLessThan<T>(T p) where T : IComparable;
        ICondition AreNotNull();
        ICondition AreNull();
        ICondition Are(Type type);
        ICondition AreEqualTo(object p);
        ICondition AreNotEqualTo(object p);
        ICondition AreGreaterThan<T>(T p) where T : IComparable;
        ICondition AreLessThan<T>(T p) where T : IComparable;

        IValueChecker And(object o);
        IValueChecker Or(object o);
        IValueChecker Nand(object o);
    }
    
    public class Assert
    {
        public static IValueChecker That(object arg_in)
        {
            return new MultiValueChecker(arg_in);
        }
    }

    public class Record
    {
        private List<string> myParameterNames;
        private record[] myRecords;
        public Record(params record[] r)
        {
            myRecords = r;
        }
        
        public string report()
        {
            System.Text.StringBuilder ret = new System.Text.StringBuilder();
            foreach(record r in myRecords)
            {
                try {
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

    public delegate void record();

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
        private System.Text.StringBuilder myStatement = new System.Text.StringBuilder();
        private bool myDelayedEvaluation = false;
        private bool myEvaluationResult = false;
        private ICondition myCondition;
        private IValueCheckerInternal myParent;
        private OpType myOpType;
        internal int myRecursion;

        public MultiValueChecker() { }

        public MultiValueChecker(object arg_in)
        {
            myOpType = OpType.None;
            myValue = arg_in;
            myParent = null;
            myStatement.AppendFormat("obj_{0}", myRecursion);
        }

        private MultiValueChecker(IValueChecker parent_in, object other_in, OpType opType_in)
            : this(other_in)
        {
            myParent = parent_in as IValueCheckerInternal;
            myParent.DelayedEvaluation = true;
            myOpType = opType_in;
            myRecursion = myParent.Recursion;
            myRecursion++;
            if(myOpType != OpType.None)
            {
                myStatement.AppendFormat(" {0} obj_{1}", myOpType.ToString(), myParent.Recursion);
            }           
        }

        public string Statement
        {
            get
            {
                return myStatement.ToString();
            }
        }

        public MultiValueChecker that(object arg_in)
        {
            myOpType = OpType.None;
            myValue = arg_in;
            myParent = null;
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
            myCondition = new Condition(new ConditionDel(delegate()
            {
                myStatement.Append("is null");
                return (myValue != null);
            }
                ));
            return AssertCondition();
        }

        public ICondition IsNull()
        {
            myCondition = new Condition(new ConditionDel(delegate() {
                myStatement.Append("is not null");
                return (myValue == null);
            }
                ));
            return AssertCondition();
        }

        public ICondition Is(Type type)
        {
            myCondition = new Condition(new ConditionDel(delegate()
            {
                myStatement.Append("is not of type ").Append(type.ToString());
                return (null != myValue && myValue.GetType().IsAssignableFrom(type));
            }));
            return AssertCondition();
        }

        public ICondition IsEqualTo(object o)
        {

            myCondition = new Condition(new ConditionDel(delegate()
            {
                myStatement.Append(" o1 is not equal to o2");
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

        public ICondition IsNotEqualTo(object o)
        {
            myCondition = new Condition(new ConditionDel(delegate() 
            {
                myStatement.Append(" o1 is equal to o2");
                return (myValue != o);
            }));
            return AssertCondition();
        }

        public ICondition IsGreaterThan<T>(T o) where T : IComparable
        {
            myCondition = new Condition(new ConditionDel(delegate() 
            {
                myStatement.Append(" o1 is not greater that o2");
                return (o.CompareTo(myValue) < 0);
            }));
            return AssertCondition();
        }

        public ICondition IsLessThan<T>(T p) where T : IComparable
        {
            myCondition = new Condition(new ConditionDel(delegate() 
            {
                myStatement.Append(" o1 is not less than o2");
                return (p.CompareTo(myValue) > 0);
            }));
            return AssertCondition();
        }

        public IValueChecker And(object o)
        {
            return new MultiValueChecker(this, o, OpType.And);
        }

        public IValueChecker Or(object o)
        {
            return new MultiValueChecker(this, o, OpType.Or);
        }

        public IValueChecker Nand(object o)
        {
            return new MultiValueChecker(this, o, OpType.Nand);
        }

        private void dummy()
        {
        }

        public ICondition AreNotNull()
        {
            myStatement.Append(" are null.");
            object t = myParent != null ? myParent.AreNotNull() : null;
            return IsNotNull();
        }

        public ICondition AreNull()
        {
            myStatement.Append(" are not null.");
            object t = myParent != null ? myParent.AreNull() : null;
            return IsNull();
        }

        public ICondition Are(Type type)
        {
            myStatement.AppendFormat(" are not of type {0}", type);
            object t = myParent != null ? myParent.Are(type) : null;
            return Is(type);
        }

        public ICondition AreEqualTo(object o)
        {
            myStatement.AppendFormat(" are not equal to obj_{0}", myRecursion, ((o != null) ? o.ToString() : "null"));
            object t = myParent != null ? myParent.AreEqualTo(o) : null;
            return IsEqualTo(o);
        }

        public ICondition AreNotEqualTo(object o)
        {
            myStatement.AppendFormat(" are equal to obj_{0}", myRecursion, ((o != null) ? o.ToString() : "null"));
            object t = myParent != null ? myParent.AreNotEqualTo(o) : null;
            return IsNotEqualTo(o);
        }

        public ICondition AreGreaterThan<T>(T o) where T : IComparable
        {
            myStatement.AppendFormat(" are not greater than obj_{0} = {1}", myRecursion, ((o!=null) ? o.ToString() : "null"));
            object t = myParent != null ? myParent.AreGreaterThan<T>(o) : null;
            return IsGreaterThan<T>(o);
        }

        public ICondition AreLessThan<T>(T o) where T : IComparable
        {
            myStatement.AppendFormat(" are not less than to obj_{0} = {1}", myRecursion, ((o != null) ? o.ToString() : "null"));
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
        public static ubta.Assert.IValueChecker That(object arg_in)
        {
            return ubta.Assert.Assert.That(arg_in);
        }

        public static string Record(params record[] r)
        {
            return new ubta.Assert.Record(r).report();
        }

    }

    public static class MemberInfoGetting
    {
        public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }
    }
}