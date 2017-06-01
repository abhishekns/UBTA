#region Copyright
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : TypeDescriptor.cs
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
using System.Reflection;

namespace ubta.SchemaGeneration
{
    public enum TypeCategory { ClassType = 1, InterfaceType = 2, EnumType = 3, ArrayType = 4, StructType = 5, OtherType = 6 };

    public class TypeDescriptor : Descriptor
    {

        #region Fields

        private string myFullTypeName;
        private string[] myResolvedBaseTypesName;
        private string[] myFullBaseTypesName;
        private Type myType;
        private TypeCategory myTypeCategory;
        private ConstructorInfo[] myConstructorInfos;
        private MethodDescriptor[] myMethodsDescriptors;
        private EventDescriptor[] myEventsDescriptors;
        private FieldDescriptor[] myFieldDescriptors;
        private PropertyDescriptor[] myPropertiesDescriptors;
        #endregion

        #region Construction/Destruction

        public TypeDescriptor(IAssemblyAnalyzer analyzer, Type t)
            :base(analyzer)
        {
            myType = t;
        }
        #endregion

        public Type Type
        {
            get
            {
                return myType;
            }
        }

        public string ValidSchemaFullName
        {
            get
            {
                string tn = myType.FullName;
                if (myType.IsGenericTypeDefinition)
                {

                }
                if (myType.IsGenericType)
                {
                }
                if (!string.IsNullOrEmpty(tn))
                {
                    int idx = -1;
                    if (-1 != (idx = tn.IndexOf("`") ))
                    {
                        tn = tn.Substring(0, idx);
                    }
                    tn.Replace('+', '.');
                    return tn;
                }
                return string.Empty;
            }
        }

        public string ExtendedFullName
        {
            get
            {
                if (myType.IsGenericTypeDefinition)
                {
                }
                if (myType.IsGenericType)
                {
                }
                return string.Empty;
            }
        }

        public TypeCategory TypesCategory
        {
            get
            {
                return myTypeCategory;
            }
            set
            {
                myTypeCategory = value;
            }
        }


        public string FullTypeName
        {
            get
            {
                if (string.IsNullOrEmpty(myFullTypeName))
                {
                    myFullTypeName = myType.FullName;
                }
                return myFullTypeName;
            }
            set
            {
                myFullTypeName = value;
            }
        }
        public string[] ResolvedBaseTypeNames
        {
            get
            {
                return myResolvedBaseTypesName;
            }
            set
            {
                myResolvedBaseTypesName = value;
            }
        }

        public string[] FullBaseTypeNames
        {
            get
            {
                return myFullBaseTypesName;
            }
            set
            {
                myFullBaseTypesName = value;
            }
        }
        public ConstructorInfo[] ConstructorInfos
        {
            get
            {
                return myConstructorInfos;
            }
            set
            {
                myConstructorInfos = value;
            }
        }
        public MethodDescriptor[] MethodsDescriptors
        {
            get
            {
                return myMethodsDescriptors;
            }
            set
            {
                myMethodsDescriptors = value;
            }
        }
        public PropertyDescriptor[] PropertiesDescriptors
        {
            get
            {
                return myPropertiesDescriptors;
            }
            set
            {
                myPropertiesDescriptors = value;
            }
        }
        public EventDescriptor[] EventsDescriptors
        {
            get
            {
                return myEventsDescriptors;
            }
            set
            {
                myEventsDescriptors = value;
            }
        }
        public FieldDescriptor[] FieldsDescriptors
        {
            get
            {
                return myFieldDescriptors;
            }
            set
            {
                myFieldDescriptors = value;
            }
        }
    }
}
