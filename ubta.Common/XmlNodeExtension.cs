#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : XmlNodeExtension.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace ubta.Common
{
    public static class XmlNodeExtn
    {
        public static bool IsGenericArgumentNodeName(this string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            if (name.Length >= 12 && (name.StartsWith("GenericArg")))
            {
                return true;
            }
            return false;
        }

        public static bool IsGenericArgumentNode(this XmlNode node)
        {
            if (null == node)
            {
                return false;
            }
            return node.Name.IsGenericArgumentNodeName();
        }

        public static bool IsConstructorNodeName(this string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            if (name.Length >= 11 && (name.StartsWith("Constructor")))
            {
                return true;
            }
            return false;
        }

        public static bool IsConstructorNode(this XmlNode node)
        {
            if (null == node)
            {
                return false;
            }
            return node.Name.IsConstructorNodeName();
        }

        public static bool IsReturnNodeName(this string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            if (name.Length > 3 && (name.EndsWith("_rv") || name.Substring(name.Length - 4, 3).EndsWith("_rv")))
            {
                return true;
            }
            return false;
        }
        public static bool IsReturnNode(this XmlNode node)
        {
            if(null == node)
            {
                return false;
            }
            return node.Name.IsReturnNodeName();
        }
    }
    public class DefaultXmlNodeExtension
    {
        protected XmlNode myNode;
        protected DefaultXmlNodeExtension myParent;
        public DefaultXmlNodeExtension(DefaultXmlNodeExtension parent_in, XmlNode aNode_in)
            :this(aNode_in)
        {
            myParent = parent_in;
        }
        private DefaultXmlNodeExtension(XmlNode aNode_in)
        {
            myNode = aNode_in;
        }

        public XmlNode Node
        {
            get
            {
                return myNode;
            }
        }
    }

    public class ParameterXmlNodeExtension : DefaultXmlNodeExtension
    {
        public ParameterXmlNodeExtension(MethodXmlNodeExtension mxne, XmlNode aNode_in)
            : base(mxne, aNode_in)
        {
        }
        public string Name
        {
            get
            {
                return myNode.Name;
            }
        }

        public bool IsName
        {
            get
            {
                return myNode.Attributes[Constants.NAME_NODE_ATTRIBUTE] != null;
            }
        }

        public XmlAttribute NameAttribute
        {
            get
            {
                return myNode.Attributes[Constants.NAME_NODE_ATTRIBUTE];
            }
        }

        public XmlSchemaAttribute Category
        {
            get
            {
                XmlSchemaAttribute cat = null;
                XmlSchemaComplexType xsct = (myNode.SchemaInfo.SchemaType as XmlSchemaComplexType);
                if (null != xsct)
                {
                    cat = (from x in xsct.Attributes.Cast<XmlSchemaAttribute>() 
                                              where x.QualifiedName.Name.Equals(Constants.PARAM_CATEGORY) 
                                              select x)
                                              .FirstOrDefault<XmlSchemaAttribute>();
                }
                return cat;
            }
        }

        public string ParamName
        {
            get
            {
                XmlSchemaAttribute cat = Category;
                if (null != cat && !cat.FixedValue.Equals(ParamValueTypes.In.ToString()))
                {
                    return cat.FixedValue.ToLowerInvariant() + " " + NameAttribute.Value;
                }
                else
                {
                    return NameAttribute.Value;
                }
            }
        }

        public XmlAttribute ValueAttribute
        {
            get
            {
                return myNode.Attributes[Constants.VALUE_NODE_ATTRIBUTE];
            }
        }

        public string Value
        {
            get
            {
                if (ValueAttribute.SchemaInfo.SchemaType.QualifiedName.Name.Equals(Constants.CLASS_STRING))
                {
                    return "\"" + ValueAttribute.Value + "\"";
                }
                else
                {
                    return ValueAttribute.Value;
                }
            }
        }

        public string Type
        {
            get
            {
                return myNode.Attributes[Constants.PARAM_TYPE].Value;
            }
        }

        public string Modifer
        {
            get
            {
                return myNode.Attributes[Constants.PARAM_CATEGORY].Value;
            }
        }
    }
    public class MethodXmlNodeExtension : DefaultXmlNodeExtension
    {
        protected ParameterXmlNodeExtension[] myParameters;
        protected ParameterXmlNodeExtension myReturnValue;
        
        public MethodXmlNodeExtension(ClassXmlNodeExtension cxne, XmlNode aNode_in)
            : base(cxne, aNode_in)
        {
            Init();
        }

        private void Init()
        {
            myParameters = (from child in myNode.ChildNodes.Cast<XmlNode>() 
                            where !child.Name.Equals(Constants.RETURN_VALUE_COMPLEX_TYPE)
                            select new ParameterXmlNodeExtension(this, child))
                            .ToArray<ParameterXmlNodeExtension>();
            myReturnValue = (from child in myNode.ChildNodes.Cast<XmlNode>()
                             where child.Name.Equals(Constants.RETURN_VALUE_COMPLEX_TYPE)
                             select new ParameterXmlNodeExtension(this, child))
                             .FirstOrDefault<ParameterXmlNodeExtension>();

        }

        public virtual string Name
        {
            get
            {
                if (IsProperty)
                {
                    return myNode.Name.Substring(4);
                }
                else if (IsConstructor)
                {
                    return myNode.ParentNode.Name;
                }
                else
                {
                    return myNode.Name;
                }
            }
        }

        public bool IsProperty
        {
            get
            {
                return myNode.Name.StartsWith(Constants.GET_PROPERTY_PREFIX) 
                    || myNode.Name.StartsWith(Constants.SET_PROPERTY_PREFIX);
            }
        }
        public bool IsGet
        {
            get
            {
                return myNode.Name.StartsWith(Constants.GET_PROPERTY_PREFIX);
            }
        }

        public bool IsConstructor
        {
            get
            {
                return myNode.Name.Equals(Constants.CONSTRUCTOR_NODE_NAME);
            }
        }

        public ParameterXmlNodeExtension[] Parameters
        {
            get
            {
                return myParameters;
            }
        }

        public ParameterXmlNodeExtension ReturnValue
        {
            get
            {
                return myReturnValue;
            }
        }
        public object ParamString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < Parameters.Length; i++)
                {
                    if(Parameters.Length > 1 && i > 0)
                    {
                        sb.Append(Constants.PARAM_SEPARATOR);
                    }
                    sb.Append(Parameters[i].IsName ? Parameters[i].ParamName : Parameters[i].Value);
                }
                return sb.ToString();
            }
        }
    }

    public class ClassXmlNodeExtension : DefaultXmlNodeExtension
    {
        protected MethodXmlNodeExtension[] myMethods;
        public ClassXmlNodeExtension(XmlNode node_in)
            :base(null, node_in)
        {

            myMethods = (from method in myNode.ChildNodes.Cast<XmlNode>()
                         select new MethodXmlNodeExtension(this, method))
                         .ToArray<MethodXmlNodeExtension>();
        }

        public MethodXmlNodeExtension[] Methods
        {
            get
            {
                return myMethods;
            }
        }

        public MethodXmlNodeExtension[] Constructors
        {
            get
            {
                return (from c in myMethods where c.IsConstructor select c).ToArray<MethodXmlNodeExtension>();
            }
        }

        public MethodXmlNodeExtension[] Properties
        {
            get
            {
                return (from c in myMethods where c.IsProperty select c).ToArray<MethodXmlNodeExtension>();
            }
        }

        public string Name
        {
            get
            {
                return myNode.Name;
            }
        }

        public string InstanceName
        {
            get
            {
                return NameHelper.GetAttributeValue(myNode, Constants.NAME_NODE_ATTRIBUTE);
            }
        }
    }
}