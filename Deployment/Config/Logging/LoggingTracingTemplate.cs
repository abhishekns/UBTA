using System;
$usings$

namespace $nameSpace$
{

    //public class Log
    //{
    //    public void Info()
    //    {
    //        $logClass$.$logInfoMethod$($logInfoParams$);
    //    }

    //    public void Warning()
    //    {
    //        $logClass$.$logWarningMethod$($logWarnParams$);
    //    }

    //    public void Error()
    //    {
    //        $logClass$.$logErrorMethod$($logErrorParams$);
    //    }

    //}

    //public class TraceInOut
    //{
    //    public void InOut()
    //    {
    //        using($traceInOutClass$ l = new $trace$($traceConstructorArgs$))
    //        {
    //        }
    //    }
    //}

    public class Trace
    {
        public static void Info($traceInfoParams$)
        {
            $traceClass$.$traceInfoMethod$($tip$);
        }

        public static void Warning($traceWarningParams$)
        {
            $traceClass$.$traceWarnMethod$($twp$);
        }

        public static void Error($traceErrorParams$)
        {
            $traceClass$.$traceErrorMethod$($tep$);
        }
    }
}