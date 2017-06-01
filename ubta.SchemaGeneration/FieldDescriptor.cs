#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : FieldDescriptor.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System;
using System.Reflection;

namespace ubta.SchemaGeneration
{
    public class FieldDescriptor : Descriptor<FieldInfo>
    {
        public FieldDescriptor()
        {
        }

        public FieldDescriptor(IAssemblyAnalyzer analyzer, FieldInfo info)
            : base(analyzer, info)
        {
        }
        new public FieldInfo Information
        {
            get
            {
                return myMember as FieldInfo;
            }
        }

        public override Type KnownType
        {
            get
            {
                return Information.FieldType;
            }
        }
    }
}