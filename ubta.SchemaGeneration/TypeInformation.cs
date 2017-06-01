#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : TypeInformation.cs
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
    public struct TypeInformation
    {
        public string fullTypeName;
        public string ValidSchemaFullName;
        public Type type;
        public string[] resolvedBaseTypesName;
        public string[] fullBaseTypesName;
        public bool m_isResolved;

        public TypeCategory typeCategory;
        public ConstructorInfo[] constuctorInfos;
        public MethodDescriptor[] methodsDescriptor;
        public EventDescriptor[] eventsDescriptor;
        public FieldDescriptor[] fieldsDescriptor;
        public PropertyDescriptor[] propertiesDescriptor;
    }
}