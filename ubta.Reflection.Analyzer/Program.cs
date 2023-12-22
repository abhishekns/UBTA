using System;
using ubta.Reflection.Analyze;
using System.Reflection;

namespace ubta.Reflection.Analyzer
{
    class TraceTest
    {
        public void DoSomething()
        {
            Console.WriteLine("I should be called after a trace");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //TraceInserter ti = new TraceInserter();
            //ti.Init();

            Type t = typeof(TraceTest);
            MethodInfo mi = t.GetMethod("DoSomething", BindingFlags.Public | BindingFlags.Instance);
            //ti.InsertTraceInOut(t, mi);
            TraceTest tt = new TraceTest();
            tt.DoSomething();
            //MethodBodyParser mbp = new MethodBodyParser();
            //mbp.Parse(typeof(Program).GetMethod("Main", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).GetMethodBody());
            Console.ReadLine();

        }

        /*
          .entrypoint
          // Code size       59 (0x3b)
          .maxstack  4
          .locals init ([0] class [ubta.Reflection.Analyze]ubta.Reflection.Analyze.MethodBodyParser mbp)
          IL_0000:  nop
          IL_0001:  ldstr      "Hello World!"
          IL_0006:  call       void [mscorlib]System.Console::WriteLine(string)
          IL_000b:  nop
          IL_000c:  newobj     instance void [ubta.Reflection.Analyze]ubta.Reflection.Analyze.MethodBodyParser::.ctor()
          IL_0011:  stloc.0
          IL_0012:  ldloc.0
          IL_0013:  ldtoken    ubta.Reflection.Analyzer.Program
          IL_0018:  call       class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
          IL_001d:  ldstr      "Main"
          IL_0022:  ldc.i4.s   40
          IL_0024:  call       instance class [mscorlib]System.Reflection.MethodInfo [mscorlib]System.Type::GetMethod(string,
                                                                                                                      valuetype [mscorlib]System.Reflection.BindingFlags)
          IL_0029:  callvirt   instance class [mscorlib]System.Reflection.MethodBody [mscorlib]System.Reflection.MethodBase::GetMethodBody()
          IL_002e:  callvirt   instance void [ubta.Reflection.Analyze]ubta.Reflection.Analyze.MethodBodyParser::Parse(class [mscorlib]System.Reflection.MethodBody)
          IL_0033:  nop
          IL_0034:  call       string [mscorlib]System.Console::ReadLine()
          IL_0039:  pop
          IL_003a:  ret

        Hello World!
        ubta.Reflection.Analyze.MethodBodyParser (0)
        nop
        ldstr  break  nop  nop  cpobj
        call   starg.s nop nop stloc.0 
        nop
        newobj  ldloc.s nop nop stloc.0 
        stloc.0
        ldloc.0 
        ldtoken ldarg.0 nop nop ldarg.0
        call ldloca.s nop nop stloc.0 
        ldstr  ldc.i4.5 nop nop cpobj
        ldc.i4.s 
        call call stloc.s nop nop 
        stloc.0
        callvirt ldnull nop nop stloc.0
        callvirt ldc.i4.m1 nop nop stloc.0 
        nop
        call ldc.i4.0 nop nop stloc.0
        pop
        ret
         */
    }
}
