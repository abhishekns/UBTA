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
    public delegate void NewInstanceAvailableHandler(Activity sender, Instance i);
    public delegate void ActiveInstanceChangedHandler(Activity sender, Instance oi, Instance ni);

    /// <summary>
    /// API: SequentialWorkflow
    /// </summary>
    /// <apiflag>no</apiflag>
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    [Serializable]
    [ToolboxItem(typeof(ActivityToolboxItem))]
    [Designer("ubta.UseCase.Designer.UseCaseSequenceDesigner", typeof(IDesigner))]
    public class DefaultSequentialUseCase : SequentialWorkflowActivity, IUseCase
    {
        /// <summary>
        /// API:no Constructor for SequentialWorkflow class
        /// </summary>
        /// <apiflag>no</apiflag>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public DefaultSequentialUseCase()
            : base()
        {
            var o = UserObjectInstances;
        }

        /// <summary>
        /// API:no DisplayName
        /// </summary>
        /// <apiflag>no</apiflag>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("Specify the name of the template or taskflows created from the template that is shown in user interfaces."),
        Category("Execution"), DefaultValue(null)]
        public string ActionName
        {
            get { return actionName_; }
            set { actionName_ = string.IsNullOrEmpty(value) ? null : value; }
        }

        // member fields
        private string actionName_;

        private List<Instance> myInstances = null;
        /// <summary>
        /// The obj
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("The instances to which this usecase refers to."),
        Category("Execution")]
        public Instance[] UserObjectInstances
        {
            get
            {
                if (null == myInstances)
                {
                    myInstances = new List<Instance>();
                    myInstances.AddRange(GetUserObjectInstances(Activities));
                }
                return myInstances.ToArray();
            }
        }

        private Instance[] GetUserObjectInstances(ActivityCollection activities_in)
        {
            List<Instance> uoi = new List<Instance>();
            foreach (var a in activities_in)
            {
                DefaultActivity da = a as DefaultActivity;
                CompositeActivity ca = a as CompositeActivity;
                if (null != da)
                {
                    int idx = -1;
                    var i = new Instance(da);
                    if (-1 == (idx = uoi.IndexOf(i)))
                    {
                        uoi.Add(i);
                    }
                    else
                    {
                        uoi[idx].UseCases.Add(da);
                    }
                }
                if (null != ca)
                {
                    foreach (var ui in GetUserObjectInstances(ca.Activities))
                    {
                        int idx = -1;
                        var u = new Instance() { InstanceName = ui.InstanceName };
                        if (-1 == (idx = uoi.IndexOf(new Instance() { InstanceName = ui.InstanceName, InstanceType = ui.InstanceType })))
                        {
                            uoi.Add(u);
                        }
                        else
                        {
                            uoi[idx].UseCases.AddRange(ui.UseCases);
                        }
                    }
                }
            }
            return uoi.ToArray();
        }

        private Instance myActiveInstance;
        public Instance ActiveInstance
        {
            get
            {
                return myActiveInstance;
            }
            set
            {
                if (myActiveInstance != value)
                {
                    Instance ov = myActiveInstance;
                    myActiveInstance = value;
                    if (null != ActiveInstanceChanged)
                    {
                        ActiveInstanceChanged(this, ov, value);
                    }
                }
            }
        }

        public event NewInstanceAvailableHandler NewInstanceAvailable;

        protected override void OnActivityChangeAdd(ActivityExecutionContext executionContext, Activity addedActivity)
        {
            base.OnActivityChangeAdd(executionContext, addedActivity);
        }

        protected override void OnListChanged(ActivityCollectionChangeEventArgs e)
        {
            base.OnListChanged(e);
            switch (e.Action)
            {
                case ActivityCollectionChangeAction.Add:
                    {
                        CheckIfNewInstanceAvailable(e.AddedItems);
                        break;
                    }
                default:
                    break;
            }
        }

        private void CheckIfNewInstanceAvailable(IList<Activity> iList)
        {
            foreach (var a in iList)
            {
                IUseCase uc = a as IUseCase;
                if (null != uc)
                {
                    var filter = UserObjectInstances.Where((x,t) => x.InstanceName == uc.InstanceName);
                    if (filter.Any())
                    {
                        foreach (var f in filter)
                        {
                            f.UseCases.Add(uc);
                        }
                    }
                    else
                    {
                        var i = new Instance(uc);
                        i.InstanceNameChanged += new InstanceNameChangedHandler(i_InstanceNameChanged);
                        myInstances.Add(i);
                        if (myActiveInstance == null)
                        {
                            ActiveInstance = i;
                        }
                        if (null != NewInstanceAvailable)
                        {
                            NewInstanceAvailable(this, new Instance(uc));
                        }
                    }
                }
            }
        }

        void i_InstanceNameChanged(object sender, string oldName_in, string newName_in)
        {
            if (null != InstanceNameChanged)
            {
                InstanceNameChanged(this, oldName_in, newName_in);
            }
        }

        protected override void OnActivityChangeRemove(ActivityExecutionContext executionContext, Activity removedActivity)
        {
            base.OnActivityChangeRemove(executionContext, removedActivity);
        }

        #region IUseCase Members


        public string InstanceName {get; set;}

        public Type InstanceType {get; set;}

        public event EventHandler InstanceTypeAvailable;

        public event ParamValueChanged ParameterChanged;

        public string Parameters
        {
            get { return string.Empty; }
        }

        public event InstanceNameChangedHandler InstanceNameChanged;

        #endregion

        public event ActiveInstanceChangedHandler ActiveInstanceChanged;
    }
}
