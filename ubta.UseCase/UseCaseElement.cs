using System.ComponentModel;
using System.Xml;
using ubta.Common;

namespace ubta.UseCase
{
    public enum ElementType
    {
        Undef,
        In,
        Out,
        Ref
    }

    [DefaultPropertyAttribute("ActualType"), BrowsableAttribute(true)]
    public class UseCaseElement : InstanceElement
    {

        public UseCaseElement(string name_in, string type_in, XmlNode node_in)
            : base(null, name_in)
        {
            myType = type_in;
            myNode = node_in;
        }
        protected XmlNode myNode;
        protected string myType;
        protected ElementType myElemType = ElementType.Out;
        [CategoryAttribute("Type"), DescriptionAttribute("Type of the element")]
        public string Type
        {
            get
            {
                return myType;
            }
        }
        [CategoryAttribute("ActualType"), DescriptionAttribute("ActualType of the element")]
        public string ActualType
        {
            get
            {
                if (myType.EndsWith("_rv") || myType.Contains("_rv_"))
                {
                    return myNode.SchemaInfo.SchemaElement.Name;
                }
                else
                {
                    return myType;
                }
            }
        }
        [CategoryAttribute("ElemType"), DescriptionAttribute("ElementType of the element")]
        public ElementType ElemType
        {
            get
            {
                return myElemType;
            }
            set
            {
                myElemType = value;
            }
        }

        public override bool Equals(object obj)
        {
            UseCaseElement uce = obj as UseCaseElement;
            return myName.Equals(uce.Name);
        }

        public override int GetHashCode()
        {
            return myName.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[ {0}, {1}, {2} ]", myName, myType, myElemType.ToString());
        }
    }
}