#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta.Reflection
    Name            : TypeExtension.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion

using System;
using System.Text;
using ubta.Common;

namespace ubta.Reflection
{
    public class TypeExtension
    {
        private Type myTargetType;
        private TypeHelper myTypeHelper;
        private MethodHelper myMH;
        private string myCSName = null;
        private string myConstraints = null;

        public TypeExtension(Type t, TypeHelper helper_in)
        {
            myTypeHelper = helper_in;
            myTargetType = t;
            myMH = new MethodHelper(this);
        }

        public string Constraints
        {
            get
            {
                if (myConstraints == null)
                {
                    StringBuilder sb = new StringBuilder();
                    if (myTargetType.IsGenericParameter)
                    {
                        Type[] gpc = myTargetType.GetGenericParameterConstraints();
                        myConstraints = TypeHelper.GetConstraints(gpc, myTypeHelper);
                    }
                    else
                    {
                        myConstraints = myTargetType.FullName;
                    }
                }
                return myConstraints;
            }
        }

        public string CsharpName
        {
            get
            {
                if (null == myCSName)
                {
                    if (myTargetType.IsGenericType)
                    {
                        Type[] ga = myTargetType.GetGenericArguments();
                        string whereClause;
                        StringBuilder sb = new StringBuilder();
                        sb.Append(myTargetType.Namespace).Append(".").Append(NameHelper.Clean(myTargetType.Name));
                        sb.Append(TypeHelper.GetGenericParametersStatement(ga, out whereClause, myTypeHelper));
                        sb.Append(whereClause);
                        myCSName = sb.ToString();
                    }
                    else if (myTargetType.IsGenericParameter)
                    {
                        myCSName = myTargetType.Name;
                    }
                    else
                    {
                        myCSName = myTargetType.FullName;
                    }
                }
                return myCSName;
            }
        }

        public Type T
        {
            get
            {
                return myTargetType;
            }
        }

        public MethodHelper MH
        {
            get
            {
                return myMH;
            }
        }

        public TypeHelper TypeHelper
        {
            get
            {
                return myTypeHelper;
            }
        }


    }
}