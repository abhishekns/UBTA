#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : PropertyDescriptor.cs
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
    public class PropertyDescriptor : Descriptor<PropertyInfo>
    {
        public PropertyDescriptor()
        {
        }

        public PropertyDescriptor(IAssemblyAnalyzer analyzer, PropertyInfo propertyInfo)
            : base(analyzer, propertyInfo)
        {
        }

        new public PropertyInfo Information
        {
            get
            {
                return myMember as PropertyInfo;
            }
        }

        public override Type KnownType
        {
            get
            {
                if (Information.PropertyType.FullName == null && null != Information.PropertyType.BaseType)
                {
                    return Information.PropertyType.BaseType;
                }
                else
                {
                    return Information.PropertyType;
                }
            }
        }
    }
}