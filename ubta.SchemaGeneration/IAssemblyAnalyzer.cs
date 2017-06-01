#region Copyright
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : IAssemblyAnalyzer.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
//------------------------------------------------------------------------
//  
//------------------------------------------------------------------------
//   Project           : USECASE BASED TEST AUTOMATION
//   Author            : <name>, <company>
//   In Charge for Code: <name>, <company>
//------------------------------------------------------------------------
// __cc_history__
//------------------------------------------------------------------------
#endregion
using System;
using System.Collections;

namespace ubta.SchemaGeneration
{
    public interface IAssemblyAnalyzer
    {
        Hashtable AnalyzeAssembly(ArrayList assemblyFiles, bool choice);
        NamespaceInfo GetNamespaceInfo(string name);
        System.Collections.Hashtable TypesAndMembers { get; }
        bool isTypeKnown(Type type, out string validSchemaFullName);
    }
}
#region Copyright

//------------------------------------------------------------------------
//  
//------------------------------------------------------------------------
#endregion