using System;
using System.Reflection;
using ubta.Reflection.Editable.ClassGenerator;

namespace ubta.Reflection.Editable.ClassGeneratorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly a = Assembly.LoadFile(Constants.ASSEMBLY_DIR + @"\SampleLib.dll");
            Generator gen = new ActivityClassGenerator(a);
            IElement e = gen.BuildTree();
            Console.WriteLine(e.ToString());
            gen.GenerateClasses();
            Console.ReadLine();
        }
    }
}
