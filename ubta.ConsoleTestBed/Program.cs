using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using uAssert = ubta.Assert.Assert;

namespace ubta.ConsoleTestBed
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 5;
            uAssert.That(a).IsEqualTo(5);
            int b = 10;
            uAssert.That(a).IsNotEqualTo(b);
            object o1 = new object();
            object o2 = new object();
            uAssert.That(o1).And(o2).AreNotNull();
            o1 = null;
            uAssert.That(o1).IsNull();
            //o2 = null;
            uAssert.That(o1).And(o2).AreNull();
        }
    }
}
