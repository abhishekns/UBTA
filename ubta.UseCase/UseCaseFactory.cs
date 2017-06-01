using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using ubta.Common;
using System.Xml;

namespace ubta.UseCase
{
    public class UseCaseFactory
    {
        public static BasicUseCaseTemplate CreateBasicUseCaseTemplate(XmlSchemaType xst, XmlSchemaObject xso)
        {
            BasicUseCaseTemplate buc = new BasicUseCaseTemplate();
            XmlSchemaElement xse = xso as XmlSchemaElement;
            XmlSchemaSequence xss = xso as XmlSchemaSequence;
            if (null != xse)
            {
                buc.Init(xse.Name, xst.Name, GetParameters(xse, "In"), GetParameters(xse, "Out"));
            }
            else
            {
                buc.Init((((xss.Parent as XmlSchemaChoice).Parent as XmlSchemaComplexType).Parent as XmlSchemaElement).Name,
                    xst.Name, GetParameters(xss, "In"), GetParameters(xss, "Out"));
            }

            return buc;
        }

        public static BasicUseCaseTemplate CreateBasicUseCaseTemplate(XmlSchemaType xst, XmlSchemaElement xse, int index_in)
        {
            BasicUseCaseTemplate buc = new BasicUseCaseTemplate();

            var aXst = xse.SchemaType as XmlSchemaComplexType;
            var xss = aXst.Particle as XmlSchemaSequence;
            var xsc = aXst.Particle as XmlSchemaChoice;
            XmlSchemaObjectCollection xsoc = null;
            if (null != xss)
            {
                xsoc = xss.Items;
            }
            if (null != xsc)
            {
                xsoc = xsc.Items;
            }
            XmlSchemaElement e = xsoc.Cast<XmlSchemaElement>().ToArray()[index_in];
            if (null != e)
            {
                buc.Init(xse.Name, aXst.Name, GetParameters(e, "In"), GetParameters(e, "Out"));
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
            return buc;
        }

        public static BasicUseCaseTemplate[] CreateBasicUseCaseTemplate(XmlSchemaType xst, XmlSchemaElement xse)
        {
            List<BasicUseCaseTemplate> l = new List<BasicUseCaseTemplate>();
            var aXst = xse.SchemaType as XmlSchemaComplexType;
            var xss = aXst.Particle as XmlSchemaSequence;
            var xsc = aXst.Particle as XmlSchemaChoice;
            XmlSchemaObjectCollection xsoc = null;
            if (null != xss)
            {
                xsoc = xss.Items;
            }
            if (null != xsc)
            {
                xsoc = xsc.Items;
            }
            foreach (var e in xsoc.Cast<XmlSchemaElement>())
            {
                BasicUseCaseTemplate buc = new BasicUseCaseTemplate();
                List<UseCaseElement> inputs = new List<UseCaseElement>(GetParameters(e, "In"));
                List<UseCaseElement> outputs = new List<UseCaseElement>(GetParameters(e, "Out"));
                buc.Init(xse.Name, aXst.Name, inputs.ToArray(), outputs.ToArray());
                l.Add(buc);
            }
            return l.ToArray();
        }

        private static UseCaseElement[] GetParameters(XmlSchemaElement xse , string type_in)
        {
            List<UseCaseElement> l = new List<UseCaseElement>();
            var xst = xse.ElementSchemaType as XmlSchemaComplexType;
            var xss = xst.Particle as XmlSchemaSequence;
            var xsc = xst.Particle as XmlSchemaChoice;
            XmlSchemaObjectCollection xsoc = null;
            if (null != xss)
            {
                xsoc = xss.Items;
            }
            if (null != xsc)
            {
                xsoc = xsc.Items;
            }
            ElementType et = (ElementType)Enum.Parse(typeof(ElementType), type_in);

            #region XmlSchemaElement
            if (xsoc.OfType<XmlSchemaElement>().Count() == xsoc.Count)
            {
                foreach (var v in xsoc.Cast<XmlSchemaElement>())
                {
                    var cxsct = v.ElementSchemaType as XmlSchemaComplexType;
                    if (null == cxsct)
                    {
                        cxsct = v.SchemaType as XmlSchemaComplexType;
                    }
                    if (null != cxsct)
                    {
                        string name = cxsct.Name;
                        if (name == null)
                        {
                            name = cxsct.QualifiedName.Name;
                        }
                        if ( (v.Name.EndsWith("_rv") || v.Name.Contains("_rv_")) 
                            && et == ElementType.Out)
                        {
                            var attType = (from a in cxsct.Attributes.Cast<XmlSchemaAttribute>() where a.Name.EndsWith("Type") select a).FirstOrDefault();
                            UseCaseElement uce = new UseCaseElement("retValue", attType.FixedValue.Replace("t:", ""), null);
                            uce.ElemType = et;
                            l.Add(uce);
                        }
                        else
                        {
                            var attType = (from a in cxsct.Attributes.Cast<XmlSchemaAttribute>() where a.RefName.Name.EndsWith(Constants.PARAM_TYPE) select a).FirstOrDefault();
                            var attCat = (from a in cxsct.Attributes.Cast<XmlSchemaAttribute>() where a.RefName.Name.EndsWith(Constants.PARAM_CATEGORY) select a).FirstOrDefault();
                            if (null != attType && null != attCat && attCat.FixedValue.Equals(type_in))
                            {
                                UseCaseElement uce = new UseCaseElement(v.Name, attType.FixedValue, null);
                                uce.ElemType = et;
                                l.Add(uce);
                            }
                        }
                    }
                }
            }
            return l.ToArray();
            #endregion
        }
        private static UseCaseElement[] GetParameters(XmlSchemaSequence xss, string type_in)
        {
            List<UseCaseElement> l = new List<UseCaseElement>();
            XmlSchemaObjectCollection xsoc = null;
            if (null != xss)
            {
                xsoc = xss.Items;
            }
            ElementType et = (ElementType)Enum.Parse(typeof(ElementType), type_in);
            #region XmlSchemaSequence

            foreach (var cxss in xss.Items.Cast<XmlSchemaElement>())
            {
                var cxsct = cxss.ElementSchemaType as XmlSchemaComplexType;
                if (null == cxsct)
                {
                    cxsct = cxss.SchemaType as XmlSchemaComplexType;
                }
                if (null != cxsct)
                {
                    string name = cxsct.Name;
                    if (name == null)
                    {
                        name = cxsct.QualifiedName.Name;
                    }
                    if (name.EndsWith("_rv") && et == ElementType.Out)
                    {
                        UseCaseElement uce = new UseCaseElement(cxss.Name, (cxsct.Parent as XmlSchemaAny).Parent.ToString(), null);
                        uce.ElemType = et;
                        l.Add(uce);
                    }
                    else
                    {
                        var attType = (from a in cxsct.Attributes.Cast<XmlSchemaAttribute>() where a.RefName.Name.EndsWith(Constants.PARAM_TYPE) select a).FirstOrDefault();
                        var attCat = (from a in cxsct.Attributes.Cast<XmlSchemaAttribute>() where a.RefName.Name.EndsWith(Constants.PARAM_CATEGORY) select a).FirstOrDefault();
                        if (null != attType && null != attCat && attCat.FixedValue.Equals(type_in))
                        {
                            UseCaseElement uce = new UseCaseElement(cxss.Name, attType.FixedValue, null);
                            uce.ElemType = et;
                            l.Add(uce);
                        }
                    }
                }
            }
            return l.ToArray();
            #endregion
        }
    }
}
