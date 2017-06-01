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
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    [Serializable]
    public class Instance
    {
        public Instance()
        {
        }
        private IUseCase myUc;
        private string myInstanceName;
        public Instance(IUseCase uc)
        {
            InstanceName = uc.InstanceName;
            InstanceType = uc.InstanceType;
            myUseCases.Inserting += new Change<IUseCase>(myUseCases_Inserting);
            myUseCases.Inserted += new Change<IUseCase>(myUseCases_Inserted);
            if (null == InstanceType)
            {
                uc.InstanceTypeAvailable += new EventHandler(uc_InstanceTypeAvailable);
            }
            UseCases.Add(uc);
            myUc = uc;
        }

        void myUseCases_Inserted(IListEx<IUseCase> source, ListChangeEventArgs<IUseCase> lcea)
        {
            foreach (var i in lcea.Items)
            {
                i.InstanceNameChanged += new InstanceNameChangedHandler(i_InstanceNameChanged);
            }
        }

        void i_InstanceNameChanged(object sender, string oldName_in, string newName_in)
        {
            this.InstanceName = newName_in;
            foreach (var i in myUseCases)
            {
                i.InstanceName = newName_in;
            }
        }

        void myUseCases_Inserting(IListEx<IUseCase> source, ListChangeEventArgs<IUseCase> lcea)
        {

        }

        void uc_InstanceTypeAvailable(object sender, EventArgs e)
        {
            InstanceType = myUc.InstanceType;
        }
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("The reference name of the object on which the activities execute."),
        Category("Execution"), DefaultValue("")]
        public string InstanceName
        {
            get
            {
                return myInstanceName;
            }
            set
            {
                if (myInstanceName != value)
                {
                    string ov = myInstanceName;
                    myInstanceName = value;
                    if (null != InstanceNameChanged)
                    {
                        InstanceNameChanged(this, ov, myInstanceName);
                    }
                }
            }
        }

        [Browsable(false)]
        public event InstanceNameChangedHandler InstanceNameChanged;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("The activities which are associated with the given reference name."),
        Category("Execution")]
        public Type InstanceType = typeof(object);

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("The activities which are associated with the given reference name."),
        Category("Execution")]
        public IListEx<IUseCase> UseCases
        {
            get
            {
                return myUseCases;
            }
        }

        [Browsable(false)]
        private ListEx<IUseCase> myUseCases = new ListEx<IUseCase>();

        [Browsable(false)]
        public override bool Equals(object obj)
        {
            Instance other = obj as Instance;
            if (null != other)
            {
                if (InstanceName == other.InstanceName)
                {
                    return true;
                }
                else
                {
                    return InstanceName.Equals(other.InstanceName);
                }
            }
            return false;
        }

        [Browsable(false)]
        public override int GetHashCode()
        {
            return InstanceName.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(InstanceName).Append(".");
            foreach (var a in UseCases)
            {
                sb.Append(" ");
                sb.Append(a.ActionName);
            }
            return sb.ToString();
        }


    }
}
