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

using Recorder = ubta.Assert.Recorder;

namespace ubta.AssertTest
{
    class Program : ubta.Assert.TestRecorder
    {
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
            new Recorder(() =>
            {
                That(t).And(t1).And(t2).And(t3).AreNull();
            }),
            new Recorder(()=>
            {
                That(t).IsNotNull();
            }));

            System.Console.WriteLine(report);

            System.Console.ReadLine();
            
        }
    }
}