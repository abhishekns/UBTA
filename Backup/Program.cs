#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : Program.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     Copyright (C) Siemens AG 2010-2010 All Rights Reserved
-----------------------------------------------------------------------------*/
/*] END */
#endregion

namespace ubta.AssertTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string t = "";
            ubta.Assert.Assert.That (t)     .IsNotNull    ();
            ubta.Assert.Assert.That (null)  .IsNull       ();
            ubta.Assert.Assert.That (t)     .Is           (typeof(System.String));
            ubta.Assert.Assert.That (2)     .IsEqualTo    (2);
            ubta.Assert.Assert.That (2)     .IsNotEqualTo (3);
            ubta.Assert.Assert.That (5)     .IsGreaterThan(4);
            ubta.Assert.Assert.That (2)     .IsLessThan   (5);
            ubta.Assert.Assert.That(new object()).And(new object()).AreNotNull();
            ubta.Assert.Assert.That(null).And(null).AreNull();
        }
    }
}