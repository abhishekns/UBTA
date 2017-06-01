using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ubta.Reflection.Analyze
{
    public class MethodBodyParser
    {
        private static List<OpCode> myOpCodes = new List<OpCode>();
        private static List<OpCode> myOpCodesWSBA = new List<OpCode>();
        private static List<OpCode> myOpCodesW4BA = new List<OpCode>();
        static MethodBodyParser()
        {
            Type ocType = typeof(OpCodes);
            FieldInfo[] fields = ocType.GetFields();
            foreach (var f in fields)
            {
                OpCode oc = (OpCode)f.GetValue(null);
                myOpCodes.Add(oc);
                if (OpCodes.TakesSingleByteArgument(oc))
                {
                    myOpCodesWSBA.Add(oc);
                }
            }
            myOpCodesW4BA.Add(OpCodes.Call);
            myOpCodesW4BA.Add(OpCodes.Calli);
            myOpCodesW4BA.Add(OpCodes.Jmp);
            myOpCodesW4BA.Add(OpCodes.Ldarg);
            myOpCodesW4BA.Add(OpCodes.Callvirt);
            myOpCodesW4BA.Add(OpCodes.Ldstr);
            myOpCodesW4BA.Add(OpCodes.Newobj);
            myOpCodesW4BA.Add(OpCodes.Newarr);
            myOpCodesW4BA.Add(OpCodes.Ldtoken);
        }
        public void Parse(MethodBody mbody)
        {
            IList<LocalVariableInfo> lvis = mbody.LocalVariables;
            for (int i = 0; i < lvis.Count; i++)
            {
                Console.WriteLine(lvis[i].ToString());
            }
            byte[] mb = mbody.GetILAsByteArray();
            Console.WriteLine("Size of code : {0}",mb.Length);
            for (int i = 0; i < mb.Length; i++)
            {
                OpCode oc = GetOpCode(mb[i]);
                Console.Write(oc);
                HandleArgs(oc, myOpCodesWSBA, ref i, mb, 1);
                HandleArgs(oc, myOpCodesW4BA, ref i, mb, 4);
                Console.WriteLine();
            }
        }

        private void HandleArgs(OpCode oc, List<OpCode> category, ref int i, byte[] mb, int step)
        {
            if (category.Contains(oc))
            {
                for (int j = 1; j <= step; j++)
                {
                    Console.Write(" {0:X}", mb[i + j]);
                }
                i += step;
            }
        }

        private OpCode GetOpCode(byte p)
        {
            return (from r in myOpCodes where r.Value.Equals(p) select r).FirstOrDefault();
        }
    }
}
