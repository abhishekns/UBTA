using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.ComponentModel;
using System.Reflection;
using System.ComponentModel;
using System.Xml;

namespace ubta.UseCase
{
    public delegate void ParamValueChanged(string name, object oldValue_in, object newValue_in, params object[] indices);
    public delegate void InstanceNameChangedHandler(object sender, string oldName_in, string newName_in);

    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    [Serializable]
    [Designer("ubta.UseCase.Designer.UseCaseDesigner")]
    public class DefaultActivity : Activity, IUseCase
    {
        [NonSerialized]
        private Type myInstanceType;

        private string myInstanceName = string.Empty;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("The target object's type."),
        Category("Execution"), DefaultValue(typeof(object))]
        public Type InstanceType
        {
            get
            {
                return myInstanceType;
            }
            set
            {
                myInstanceType = value;
            }
        }

        [Browsable(false)]
        public event EventHandler InstanceTypeAvailable;

        [Browsable(false)]
        public event ParamValueChanged ParameterChanged;

        [Browsable(false)]
        public event InstanceNameChangedHandler InstanceNameChanged; 

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("The target object to invoke the action on."),
        Category("Execution"), DefaultValue(null)]
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
                        InstanceNameChanged(this, ov, value);
                    }
                }
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("The name of the method to be invoked on execution"),
        Category("Execution"), DefaultValue(null)]
        public string ActionName { get; set; }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("The xml description."),
        Category("Execution"), DefaultValue(null)]
        public string Xml { get; set; }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("The method to be invoke on execution"),
        Category("Execution"), DefaultValue(null)]
        [NonSerialized]
        public MethodBase Action;

        [Browsable(false)]
        public virtual string Parameters
        {
            get
            {
                return string.Empty;
            }
        }

        [Browsable(false)]
        protected void OnParametersChanged(string nameOfParam_in, object oldValue_in, object newValue_in, params object[] indices)
        {
                OnParametersChangedInternal(nameOfParam_in, oldValue_in, newValue_in, indices);
        }

        [Browsable(false)]
        protected virtual void OnParametersChangedInternal(string nameOfParam_in, object oldValue_in, object newValue_in, params object[] indices)
        {
            if (ParameterChanged != null)
            {
                ParameterChanged(nameOfParam_in, oldValue_in, newValue_in, indices);
            }
        }
    }
}
