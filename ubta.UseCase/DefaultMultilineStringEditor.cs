using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Workflow.Activities;
using System.Drawing.Design;
using System.ComponentModel.Design;
using System.Workflow.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Workflow.ComponentModel.Design;
using ubta.Common;

namespace ubta.UseCase
{
    internal class DefaultMultilineStringEditor : UITypeEditor
    {
        // ctor
        internal DefaultMultilineStringEditor(string sep) { sep_ = sep; }

        /// <summary>
        /// API:no EditValue
        /// </summary>
        /// <apiflag>no</apiflag>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (sep_.Length == 0) return stringArrayBasedEditValue(context, provider, (string[])value);
            if (sep_ == "<xml>") return xmlBasedEditValue(context, provider, (string)value);
            if (sep_ == "<task>") return taskBasedEditValue(context, provider, (string)value);
            if (sep_ == "<lbs>") return lbsBasedEditValue(context, provider, (string)value);
            if (sep_ == "<ist>") return istBasedEditValue(context, provider, (string)value);
            if (sep_ == "\n") return stdEditValue(context, provider, (string)value);
            return separatorBasedEditValue(context, provider, (string)value);
        }

        // separatorBasedEditValue
        private string separatorBasedEditValue(ITypeDescriptorContext context, IServiceProvider provider, string value)
        {
            string origValue = value;
            if (origValue != null) value = origValue.Replace(sep_, Environment.NewLine).Trim();
            string newValue = (string)ed_.EditValue(context, provider, value);
            string[] txt = newValue.Trim().Replace("\r", "").Split('\n');
            StringBuilder resValue = new StringBuilder();
            foreach (string lineText in txt)
            {
                if (lineText.Length <= 0) continue;
                if (lineText.IndexOf(sep_) != -1)
                {
                    string msg = string.Format("The line\n  '{0}'\ncontains a character that is used as separation character ('{1}').\n\n" +
                                               "Press OK if this is intended or Cancel to discard the changes.", lineText, sep_);
                    if (MessageBox.Show(msg, "Content Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return origValue;
                }
                if (newValue.Length > 0) resValue.Append(sep_);
                resValue.Append(lineText.Trim());
            }
            newValue = resValue.ToString();
            return (string.IsNullOrEmpty(newValue) ? null : newValue);
        }

        // stringArrayBasedEditValue
        private string[] stringArrayBasedEditValue(ITypeDescriptorContext context, IServiceProvider provider, string[] value)
        {
            string origValue = (value != null) ? string.Join(Environment.NewLine, value) : null;
            string newValue = (string)ed_.EditValue(context, provider, origValue);
            string[] resVal = newValue.Trim().Replace("\r", "").Split('\n');
            if ((resVal.Length <= 0) || ((resVal.Length == 1) && string.IsNullOrEmpty(resVal[0]))) return null;
            return ((resVal.Length <= 0) ? null : resVal);
        }

        // xmlBasedEditValue
        private string xmlBasedEditValue(ITypeDescriptorContext context, IServiceProvider provider, string value)
        {
            XmlDocument doc = new XmlDocument();
            {
                value = "<Value>" + ((value != null) ? value : "") + "</Value>";
                doc.LoadXml(value);
                StringWriter xmlStr = new StringWriter();
                XmlTextWriter tw = new XmlTextWriter(xmlStr);
                tw.Formatting = Formatting.Indented;
                value = xmlStr.ToString();
                string newValue = (string)ed_.EditValue(context, provider, value);
                newValue = "<Value>" + ((newValue != null) ? newValue : "") + "</Value>";
                doc.PreserveWhitespace = false;
                doc.Load(new StringReader(newValue));
                doc.LoadXml(newValue);
                string resStr = "";
                return (string.IsNullOrEmpty(resStr) ? null : resStr);
            }
        }

        // taskBasedEditValue
        private string taskBasedEditValue(ITypeDescriptorContext context, IServiceProvider provider, string value)
        {
            string proposal = null;
            if (string.IsNullOrEmpty(value))
            {
                value = proposal = proposal_;
            }
            string ret = xmlBasedEditValue(context, provider, value);
            if ((proposal != null) && (ret == proposal)) return null;
            return ret;
        }

        // lbsBasedEditValue
        private string lbsBasedEditValue(ITypeDescriptorContext context, IServiceProvider provider, string value)
        {
            string proposal = null;
            if (string.IsNullOrEmpty(value))
            {
                value = proposal = proposal_;
            }
            string ret = xmlBasedEditValue(context, provider, value);
            if ((proposal != null) && (ret == proposal)) return null;
            return ret;
        }

        // istBasedEditValue
        private string istBasedEditValue(ITypeDescriptorContext context, IServiceProvider provider, string value)
        {
            string proposal = null;
            if (string.IsNullOrEmpty(value))
            {
                value = proposal = proposal_;
            }
            string ret = xmlBasedEditValue(context, provider, value);
            if ((proposal != null) && (ret == proposal)) return null;
            return ret;
        }

        // stdEditValue
        private string stdEditValue(ITypeDescriptorContext context, IServiceProvider provider, string value)
        {
            string resStr = (string)ed_.EditValue(context, provider, value);
            return (string.IsNullOrEmpty(resStr) ? null : resStr);
        }

        /// <summary>
        /// API:no Equals
        /// </summary>
        /// <apiflag>no</apiflag>
        public override bool Equals(object obj)
        {
            return ed_.Equals(obj);
        }

        /// <summary>
        /// API:no GetEditStyle
        /// </summary>
        /// <apiflag>no</apiflag>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return ed_.GetEditStyle(context);
        }

        /// <summary>
        /// API:no GetHashCode
        /// </summary>
        /// <apiflag>no</apiflag>
        public override int GetHashCode()
        {
            return ed_.GetHashCode();
        }

        /// <summary>
        /// API:no GetPaintValueSupported
        /// </summary>
        /// <apiflag>no</apiflag>
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return ed_.GetPaintValueSupported(context);
        }

        /// <summary>
        /// API:no IsDropDownResizable
        /// </summary>
        /// <apiflag>no</apiflag>
        public override bool IsDropDownResizable
        {
            get
            {
                return ed_.IsDropDownResizable;
            }
        }

        /// <summary>
        /// API:no PaintValue
        /// </summary>
        /// <apiflag>no</apiflag>
        public override void PaintValue(PaintValueEventArgs e)
        {
            ed_.PaintValue(e);
        }

        /// <summary>
        /// API:no ToString
        /// </summary>
        /// <apiflag>no</apiflag>
        public override string ToString()
        {
            return ed_.ToString();
        }

        // field member
        private string sep_ = "|";
        protected string proposal_;
        MultilineStringEditor ed_ = new MultilineStringEditor();
    }

    internal class DefaultISTEditor : DefaultMultilineStringEditor
    {
        DefaultISTEditor()
            : base("<ist>")
        {
            base.proposal_ = "<TOOL ID=\"\" TYPE=\"\" ASSEMBLY=\"\" />";
        }
    }

    internal class DefaultStringArrayMultilineStringEditor : DefaultMultilineStringEditor
    {
        DefaultStringArrayMultilineStringEditor() : base("") { }
    }

    internal class DefaultCommaMultilineStringEditor : DefaultMultilineStringEditor
    {
        DefaultCommaMultilineStringEditor() : base(",") { }
    }
}
