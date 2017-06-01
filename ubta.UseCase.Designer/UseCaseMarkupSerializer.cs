using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.ComponentModel.Serialization;
using System.ComponentModel.Design.Serialization;
using System.Workflow.ComponentModel;
using System.Xml;
using System.Collections;
using System.Reflection;
using ubta.Common;
using ubta.Reflection;
using System.Runtime.Serialization;
using System.Globalization;
using ubta.ExecuterLib;
using System.Text.RegularExpressions;

namespace ubta.UseCase.Designer
{
    public class UseCaseMarkupSerializer 
    {
        VariableManager myVM = VariableManager.Instance;
        private TypeResolutionService myTrs;
        private DefaultExecuter myDe;
        public UseCaseMarkupSerializer()
        {
            myTrs = TypeResolutionService.Instance as TypeResolutionService;
            myDe = new DefaultExecuter();
            foreach (var a in myTrs.WellKnownAssemblies)
            {
                myDe.Assemblies.Add(a.Location);
            }
            myDe.SetupExec();
        }

        //
        // Summary:
        //     Deserializes workflow markup into an System.Object.
        //
        // Parameters:
        //   reader:
        //     An System.Xml.XmlReader that contains the workflow definition.
        //
        // Returns:
        //     An System.Object that contains the definition of the workflow defined in
        //     the workflow markup file or stream.
        public object Deserialize(XmlDocument d)
        {
            DefaultSequentialUseCase duc = new DefaultSequentialUseCase();
            
            XmlNode rn = d[Constants.ROOT_ELEMENT_NODE_NAME];
            foreach (var cn in rn.ChildNodes.Cast<XmlNode>())
            {
                string n = cn.Name;
                Type targetcl = null;
                if (cn.SchemaInfo != null &&
                        cn.SchemaInfo.SchemaType != null &&
                        cn.SchemaInfo.SchemaType.Name != null &&
                        cn.SchemaInfo.SchemaType.Name.EndsWith(".Invoke"))
                {
                    string tn = cn.SchemaInfo.SchemaType.Name;
                    targetcl = myTrs.GetType(tn);
                }
                else
                {
                    targetcl = myTrs.GetType(n);
                }

                string ns = "_"+n;
                foreach (var ccn in cn.ChildNodes.Cast<XmlNode>())
                {
                    string nn = ccn.SchemaInfo.SchemaElement.Name;
                    
                    DefaultActivity a = null;
                    bool iso;
                    int idx = 0;
                    GetOverloadInfo(ccn, out iso, out idx);
                    Type cl = null;
                    
                    if (ccn.SchemaInfo != null &&
                        ccn.SchemaInfo.SchemaType != null &&
                        ccn.SchemaInfo.SchemaType.Name != null &&
                        ccn.SchemaInfo.SchemaType.Name.EndsWith(".Invoke"))
                    {
                        string tn = "_" + ccn.SchemaInfo.SchemaType.Name;
                        cl = myTrs.GetType(tn);
                    }
                    else if (iso)
                    {
                        cl = myTrs.GetType(ns + "." + nn + "_" + idx);
                    }
                    else
                    {
                        cl = myTrs.GetType(ns + "." + nn);
                    }
                    a = cl.GetConstructor(new Type[] { }).Invoke(null) as DefaultActivity;
                    a.Xml = ccn.OuterXml;
                    a.Name = Guid.NewGuid().ToString();
                    a.InstanceType = targetcl;
                    a.ActionName = nn;
                    a.InstanceName = cn.Attributes["Name"].Value;
                    if (nn.IsConstructorNodeName())
                    {
                        myDe.ExecuteMethod(ccn);
                    }
                    myVM.SetVarType(a.InstanceName, targetcl);
                    ReflectProperties(a, cl, ccn, iso, idx, false);
                    duc.Activities.Add(a);
                }
            }
            return duc;
        }

        private Regex myRegEx = new Regex("_([0-9])+");

        private void GetOverloadInfo(XmlNode ccn, out bool iso, out int idx)
        {
            iso = false;
            idx = 0;
            foreach (var p in ccn.ChildNodes.Cast<XmlNode>())
            {
                if (p.Name.Length > 2)
                {
                    Match m = null;
                    if (null != (m = myRegEx.Match(p.Name)) && m.Success)
                    {
                        iso = true;
                        idx = int.Parse(m.Captures[0].Value.Substring(1));
                        break;
                    }
                    if (p.Name[p.Name.Length - 2] == '_')
                    {
                        iso = true;
                        idx = p.Name[p.Name.Length - 1] - '0';
                        break;
                    }
                }
            }
        }

        private Type GetOverloadedType(string p, out bool isOverloaded, out int oIdx)
        {
            isOverloaded = false;
            oIdx = 0;
            Type t = myTrs.GetType(p);
            if (null == t)
            {

                int c = 10;
                for (int i = 0; i < c; i++)
                {
                    string np = p + "_" + i;
                    t = myTrs.GetType(np);
                    if (t != null)
                    {
                        oIdx = i;
                        isOverloaded = true;
                        break;
                    }
                }
            }
            return t;
        }

        private void ReflectProperties(object a, Type cl, XmlNode ccn, bool iso, int idx, bool serializeDeserialize)
        {
            var props = cl.GetProperties();
            if (!serializeDeserialize)
            {
                foreach (var pm in ccn.ChildNodes.Cast<XmlNode>())
                {
                    if (pm.IsReturnNode())
                    {
                        var natt = pm.Attributes["Name"];
                        if (null != natt)
                        {
                            foreach (var p in props)
                            {
                                string pname = p.Name;
                                if (iso)
                                {
                                    pname += "_" + idx;
                                }
                                if ((pname).Equals(pm.Name))
                                {
                                    p.SetValue(a, "&" + natt.Value, null);
                                }
                            }
                        }
                    }
                    else
                    {
                        bool hit = false;
                        foreach (var p in props)
                        {
                            string pname = p.Name;
                            if (iso)
                            {
                                pname += "_" + idx;
                            }
                            if ((pname).Equals(pm.Name))
                            {
                                var na = pm.Attributes["Name"];
                                string pv = string.Empty;
                                if (null == na)
                                {
                                    var va = pm.Attributes["Value"];
                                    if (null != va)
                                    {
                                        pv = va.Value;
                                    }
                                }
                                else
                                {
                                    pv = "&" + na.Value;
                                }
                                object pvo = GetObject(pv, p);
                                p.SetValue(a, pvo, null);
                                hit = true;
                                break;
                            }
                        }
                        if (!hit)
                        {

                        }
                    }
                }
            }
            else
            {
                foreach (var p in props)
                {
                    if (p.IsOverridden())
                    {
                        continue;
                    }
                    string pname = p.Name;
                    if (iso)
                    {
                        pname += "_" + idx;
                    }
                }
            }
        }

        private object GetObject(string pv, PropertyInfo p)
        {
            if (p.PropertyType.IsEnum)
            {
                return Enum.Parse(p.PropertyType, pv);
            }
            Type t = typeof(IConvertible);
            if (t.IsAssignableFrom(p.PropertyType) && !"System.String".Equals(p.PropertyType.FullName))
            {
                var m = p.PropertyType.GetMethod("Parse", new Type[] {typeof(string), typeof(IFormatProvider)});
                return m.Invoke(null, new object[] {pv, CultureInfo.InvariantCulture.NumberFormat });
            }
            return pv;
        }

        public void Serialize(XmlWriter writer, object obj)
        {
        }
    }
}
