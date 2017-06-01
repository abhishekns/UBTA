#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : IInvokableExtension.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System;
using System.Reflection;

namespace ubta.Reflection
{
    public interface IInvokableExtension
    {
        object Invoke(params object[] args);
        object Invoke(object target, Type[] generic_args, params object[] args);
        string CsharpName { get; }
        string Parameters { get; }
        ParameterModifier Modifiers { get; }
        Type[] ParamTypes { get; }
        MethodBody GetInvokableBody();
    }
}