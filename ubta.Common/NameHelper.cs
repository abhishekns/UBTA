#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : NameHelper.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
/*
 * Created by SharpDevelop.
 * User: inet
 * Date: 2/12/2010
 * Time: 2:15 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace ubta.Common
{

    public static class Extension
    {
        public static Expression<Func<T, R>>
            Expr<T, R>(this Expression<Func<T, R>> f)
        {
            return f;
        }

        public static Expression<Func<T1, T2, R>>
            Expr<T1, T2, R>(this Expression f)
        {
            return (Expression<Func<T1, T2, R>>)f;
        }

        public static Func<T, R>
            Func<T, R>(this Func<T, R> f)
        {
            return f;
        }

        public static Func<T1, T2, R>
            Func<T1, T2, R>(this Func<T1, T2, R> f)
        {
            return f;
        }
    }

    public enum StringPos
    {
        Start,
        End
    }

    public class NameHelper
    {
        public static string Clean(string name_in)
        {
            return Clean(name_in, "`", StringPos.Start);
        }

        public static string Clean(string name_in, string c, StringPos sp)
        {
            int idx = -1;
            if (-1 != (idx = name_in.LastIndexOf(c)))
            {
                if (sp == StringPos.Start)
                {
                    return name_in.Substring(0, idx);
                }
                return name_in.Substring(idx + 1);
            }
            return name_in;
        }

        public static string NameCleanup(string name)
        {
            if (null != name)
            {
                //Console.WriteLine(name);
                int idx = -1;
                if ((idx = name.IndexOf('[')) > -1)
                {
                    name = name.Substring(0, idx);
                }
                string t = name.Replace('+', '.').Replace('`', '_').Replace("&lt;", "").Replace("&gt;", "").Replace("&", "").
                    Replace("<", "").Replace(">", "").Replace("{", "").Replace("}", "").Replace("+", "");
                //Console.WriteLine(t);
                return t;
            }
            return name;
        }


        public static string GetTypeName(XmlNode cNode)
        {
            return cNode.SchemaInfo.SchemaType.QualifiedName.Name;
        }

        public static string GetParamType(XmlNode cNode, ref ParameterModifier modifier_ref)
        {
            //var ta = cNode.Attributes["Type"];
            //if (null != ta)
            //{
            //    int idx = -1;
            //    string type = ta.Value;
            //    if (-1 != (idx = type.IndexOf(':')))
            //    {
            //        type = type.Substring(idx+1);
            //    }
            //    modifier_ref = new ParameterModifier(1);
            //    return type;
            //}
            if (cNode.IsGenericArgumentNode())
            {
                modifier_ref = new ParameterModifier(1);
                return cNode.InnerText;
            }
            XmlSchemaComplexType xsct = cNode.SchemaInfo.SchemaType as XmlSchemaComplexType;
            List<XmlSchemaAttribute> atts = new List<XmlSchemaAttribute>();
            atts.AddRange(xsct.Attributes.Cast<XmlSchemaAttribute>());
            modifier_ref = new ParameterModifier(1);
            modifier_ref[0] = (from att in atts where 
                                    null != att.QualifiedName 
                                    && null != att.QualifiedName.Name 
                                    && att.QualifiedName.Name.EndsWith(Constants.PARAM_CATEGORY) 
                                    && null != att.FixedValue 
                               select att.FixedValue.Equals(ParamValueTypes.Ref.ToString())).FirstOrDefault<bool>();
            return (from att in atts where att.QualifiedName.Name.EndsWith(Constants.PARAM_TYPE) select att.FixedValue).FirstOrDefault<string>();
        }

        public static string GetAttributeValue(XmlNode paramNode, string p)
        {
            if (paramNode.Attributes != null)
            {
                XmlAttribute att = paramNode.Attributes[p];
                if (null != att)
                {
                    return att.Value;
                }
            }
            return null;
        }

        public static string GetMethod(XmlNode node_in)
        {
            StringBuilder sb = new StringBuilder();
            if (node_in.Name.IsConstructorNodeName())
            {
                sb.Append(node_in.ParentNode.Name).Append(".").Append(node_in.Name);
            }
            else
            {
                sb.Append("new ").Append(node_in.ParentNode.Name);
            }
            sb.Append("(");
            for (int i = 0; i < node_in.ChildNodes.Count; i++)
            {
                if (i > 0
                    && (i < node_in.ChildNodes.Count)
                    && node_in.ChildNodes.Count > 1)
                {
                    sb.Append(Constants.PARAM_SEPARATOR);
                }
                XmlNode cNode = node_in.ChildNodes[i];
                string val = GetAttributeValue(cNode, Constants.VALUE_NODE_ATTRIBUTE);
                if ((null != val)
                    && (cNode.Attributes[Constants.VALUE_NODE_ATTRIBUTE].SchemaInfo.SchemaType.
                    QualifiedName.Name.Equals(Constants.CLASS_STRING)))
                {
                    sb.Append("\"").Append(val).Append("\"");
                }
                else
                {
                    sb.Append(GetAttributeValue(cNode, Constants.NAME_NODE_ATTRIBUTE));
                }
            }
            sb.Append(")");
            return sb.ToString();
        }

        public static string ConstructStatement(XmlNode method)
        {
            ClassXmlNodeExtension cxne = new ClassXmlNodeExtension(method.ParentNode);
            MethodXmlNodeExtension mxne = new MethodXmlNodeExtension(cxne, method);
            StringBuilder sb = new StringBuilder();
            if (mxne.IsConstructor)
            {
                sb.AppendFormat("{0} = new {1}({2})", cxne.InstanceName, cxne.Name, mxne.ParamString);
            }
            else if (mxne.IsProperty)
            {
                if (mxne.IsGet)
                {
                    sb.AppendFormat("{0} = {1}.{2}", mxne.ReturnValue.ParamName, cxne.InstanceName, mxne.Name);
                }
                else
                {
                    ParameterXmlNodeExtension pxne = mxne.Parameters[0];
                    sb.AppendFormat("{0}.{1} = {2}", cxne.InstanceName, mxne.Name, pxne.IsName ? pxne.ParamName : pxne.Value);
                }
            }
            else
            {
                if (mxne.ReturnValue != null)
                {
                    sb.AppendFormat("{0} = {1}.{2}({3})", mxne.ReturnValue.ParamName, cxne.InstanceName, mxne.Name, mxne.ParamString);
                }
                else
                {
                    sb.AppendFormat("{0}.{1}({2})", cxne.InstanceName, mxne.Name, mxne.ParamString);
                }
            }
            return sb.ToString();
        }

    }
}