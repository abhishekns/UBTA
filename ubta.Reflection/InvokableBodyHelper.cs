using System;
using System.Collections.Generic;
using System.Reflection;

namespace ubta.Reflection
{
    public class InvokableBodyHelper
    {
        private MethodHelper myMH;
        public InvokableBodyHelper(MethodHelper mh_in)
        {
            myMH = mh_in;
        }

        public MethodBody GetMethodBody(string name_in, params Type[] paramTypes_in)
        {
            IList<IInvokableExtension> methods = myMH.Methods[name_in];
            IInvokableExtension method = null;
            foreach (var m in methods)
            {
                if (m.ParamTypes.Length == paramTypes_in.Length)
                {
                    for (int i = 0; i < paramTypes_in.Length; i++)
                    {
                        if (m.ParamTypes[i] != paramTypes_in[i])
                        {
                            return null;
                        }
                    }
                    method = m;
                }
            }
            if (null != method)
            {
                return method.GetInvokableBody();
            }
            return null;
        }

    }
}
