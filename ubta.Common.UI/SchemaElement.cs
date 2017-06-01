#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : SchemaElement.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml;

namespace ubta.Common.UI
{
    public class SchemaElement : TreeViewItem
    {
        private XmlNode myNode;
        private List<XmlNode> myAttributes = new List<XmlNode>();
        private List<XmlNode> myChildren = new List<XmlNode>();
        public SchemaElement(XmlNode node)
        {
            myNode = node;
            myChildren.AddRange(GetChildren(node));
            Init();
            AddAttributes();
            AddChildren();
        }

        public XmlNode Node
        {
            get
            {
                return myNode;
            }
        }

        private List<XmlNode> GetChildren(XmlNode node)
        {
            List<XmlNode> list = new List<XmlNode>();
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
            //    if (node.ChildNodes[i].Name == "xs:complexContent"
            //        || node.ChildNodes[i].Name == "xs:extension"
            //        || node.ChildNodes[i].Name == "xs:schema"
            //        || node.ChildNodes[i].Name == "xs:group"
            //        || node.ChildNodes[i].Name == "xs:choice"
            //        || node.ChildNodes[i].Name == "xs:attribute"
            //        || (node.ChildNodes[i].Name == "xs:complexType" && !HasAttribute(node, "name"))
            //        || node.ChildNodes[i].Name == "xs:sequence" 
            //        //|| (node.Name == "xs:element" && (!HasAttribute(node, "type")))
            //        )
            //    {
            //        list.AddRange(GetChildren(node.ChildNodes[i]));
            //    }
            //    else
                {
                    list.Add(node.ChildNodes[i]);
                }
            }
            return list;
        }

        private bool HasAttribute(XmlNode node, string attName_in)
        {
            if (null != node.Attributes)
            {
                XmlAttribute att = node.Attributes[attName_in];
                return null != att;
            }
            return false;
        }
        private void Init()
        {
            switch (myNode.NodeType)
            {
                case XmlNodeType.Text:
                    {
                        if (!string.IsNullOrEmpty(myNode.InnerText))
                        {
                            Header = myNode.InnerText;
                        }
                        break;
                    }
                case XmlNodeType.Attribute:
                case XmlNodeType.Element:
                    {
                        if (null != myNode.Attributes)
                        {
                            XmlAttribute att = myNode.Attributes["name"];
                            if (null == att)
                            {
                                att = myNode.Attributes["ref"];
                            }
                            if (null != att)
                            {
                                Header = att.Value;
                            }
                            att = myNode.Attributes["type"];
                            if (null != att)
                            {
                                this.ToolTip = att.Value;
                            }
                        }
                        break;
                    }
                //case XmlNodeType.Comment:
                //case XmlNodeType.ProcessingInstruction:
                //case XmlNodeType.XmlDeclaration:
                //case XmlNodeType.CDATA:
                //case XmlNodeType.Document:
                //case XmlNodeType.DocumentFragment:
                //case XmlNodeType.DocumentType:
                //case XmlNodeType.EndEntity:
                //case XmlNodeType.Entity:
                //case XmlNodeType.EntityReference:
                //case XmlNodeType.None:
                //case XmlNodeType.Notation:
                //case XmlNodeType.ProcessingInstruction:
                default:
                    Header = "DKNI";
                    break;
            }
        }

        private void AddChildren()
        {
            for (int i = 0; i < myChildren.Count; i++)
            {
                Items.Add(new SchemaElement(myChildren[i]));
            }
        }

        private void AddAttributes()
        {
            
        }
    }
}