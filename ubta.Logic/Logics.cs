using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ubta.Logic
{
    public class Logics
    {
        public void For(int start, int step, int end, System.Func<object> work)
        {
            for (int i = start; i < end; i+=step)
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

        public void If(System.Func<bool> condition, System.Func<object> dowork, System.Func<object> elsework)
        {
            if (condition())
            {
                dowork();
            }
            else
            {
                elsework();
            }
        }
    }
}
