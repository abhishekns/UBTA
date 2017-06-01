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

using assert = ubta.Assert.Assert;

using record = ubta.Assert.record;

namespace ubta.AssertTest
{

    
    class Program
    {
        static ubta.Assert.IValueChecker That(object arg_in)
        {
            return assert.That(arg_in);
        }

        static string Record(params record[] r)
        {
            return new ubta.Assert.Record(r).report();
        }


        static void Main(string[] args)
        {
            
            string t = "";
            assert.That (t)     .IsNotNull    ();
            assert.That (null)  .IsNull       ();
            assert.That (t)     .Is           (typeof(System.String));
            assert.That (2)     .IsEqualTo    (2);
            assert.That (2)     .IsNotEqualTo (3);
            assert.That (5)     .IsGreaterThan(4);
            assert.That (2)     .IsLessThan   (5);
            assert.That(new object()).And(new object()).AreNotNull();
            assert.That(null).And(null).AreNull();

            string t1 = null;
            string t2 = null;
            string t3 = null;

            string report = Record(
            new record(() =>
            {
                That(t).And(t1).And(t2).And(t3).AreNull();
            }),
            new record(()=>
            {
                That(t).IsNotNull();
            }));

            System.Console.WriteLine(report);

            System.Console.ReadLine();
            
        }
    }
}