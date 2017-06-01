#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : MyClass.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion

using System;

namespace SampleLib
{
	public interface IA
	{
		string Name{get;}
        IB B { get; set; }
	}
	public interface IB
	{
		string Value{get;}
	}
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class MyClassA : IA
	{
		private string myName;
		private IB myB;
		public MyClassA(string name, IB b) {
			myName = name;
			myB = b;
		}

        public MyClassA(IB b)
        {
            myB = b;
            if (null != b)
            {
                myName = b.Value;
            }
            else
            {
                myName = string.Empty;
            }
        }
		
		public string Name {
			get {
				return myName;
			}
		}
		
		public IB B {
			get {
				return myB;
			}
            set
            {
                myB = value;
            }
		}
		
		public void DoSomething(string a, string b)
		{
			Console.WriteLine("DoSomething({0}, {1})", a, b);
		}
		
		public void DoSomething(string a)
		{
			Console.WriteLine("DoSomething({0})", a);
		}

        public void DoSomething(string a, IB b)
        {
            Console.WriteLine("DoSomething({0}, {1})", a, b);
        }

        public override string ToString()
        {
            return string.Format("MyClassA : B = {0}, Name = {1}", B, Name);
        }
	}

    public enum MyEnum
    {
        Type1,
        Type2,
        Type3
    }
	
	public class MyClassB : IB
	{
		private string myValue;
		public MyClassB(string value)
		{
			myValue = value;
		}
		public string Value {
			get {
				return myValue;
			}
            set
            {
                myValue = value;
            }
		}

        public override string ToString()
        {
            return string.Format("MyClassB Value = {0}", Value);
        }
	}
	
	public class MyGenericClass<T1, T2> where T1 : MyClassA where T2 : MyClassB
	{
		
	}
	
	public class MyInterfaceGenerics<T1, T2> where T1 : IA where T2 : IB
	{
        public void DoSomethingElse()
        {
        }
	}

    public delegate void MyDelegate(string param1);
	
	public class MySampleClass
	{
        public MyClassB myB = new MyClassB("MySampleClass.MyClassB");

        public MyClassB B
        {
            get
            {
                return myB;
            }
        }

        public MySampleClass()
        {
            MyEvent += new MyDelegate(MySampleClass_MyEvent);
        }

        public event MyDelegate MyEvent;

        private MyEnum myEnum = MyEnum.Type1;

        public MyEnum MyEnum
        {
            get
            {
                return myEnum;
            }
            set
            {
                myEnum = value;
            }
        }

        private int myX;

        public void SetSomeInt(int x)
        {
            myX = x;
        }

        public void AddSomeInt(ref int x)
        {
            x += myX;
        }

        public int GetSomeInt()
        {
            return myX;
        }

        public string GetSomething<T>(T param1) where T : IA
		{
            Console.WriteLine("GetSomething<{0}>({0}, {1})", "IA", param1.ToString());
			return param1.Name;
		}
        public string GetSomething<T>(string param1, T param2) where T : IA
		{
            Console.WriteLine("GetSomething<{0}>({1}, {2})", "IA", param1, param2.ToString());
			return param2.B.Value;
		}
		
		public T2 GetSomething<T1, T2>(T1 param1) where T1 : IA where T2 : IB
		{
            Console.WriteLine("GetSomething<{0}, {1}>({2})", "","", param1.ToString());
			return (T2)param1.B;
		}

        void MySampleClass_MyEvent(string param1)
        {
            Console.WriteLine("MyEvent({0})", param1);
        }

        public override string ToString()
        {
            return string.Format("MySampleClass : B = {0}", myB);
        }
	}

    public class Logics
    {
        public void For(int start, int step, int end, System.Func<object> work)
        {
            for (int i = start; i < end; i += step)
            {
                work();
            }
        }

        public void For(System.Func<int> start, System.Func<int> step, System.Func<int> end, System.Func<object> work)
        {
            for (int i = start(); i < end(); i += step())
            {
                work();
            }
        }

        public void For(System.Func<int> start, System.Func<int> step, System.Func<bool> condition, System.Func<object> work)
        {
            for (int i = start(); condition(); i += step())
            {
                work();
            }
        }

    }
}