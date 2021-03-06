//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _SampleLib.Logics {
    using System;
    using System.ComponentModel;
    using ubta.Reflection;
    using System.Text;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using System.Workflow.ComponentModel;
    using System.Workflow.ComponentModel.Design;
    
    
    [Serializable()]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Designer("ubta.UseCase.Designer.UseCaseDesigner")]
    public class For_2 : ubta.UseCase.DefaultActivity {
        
        private System.Collections.Generic.Dictionary<string, System.Type> myTypeRefs;
        
        [NonSerialized()]
        private System.Func<System.Int32> _start;
        
        [NonSerialized()]
        private System.Func<System.Int32> _step;
        
        [NonSerialized()]
        private System.Func<System.Boolean> _condition;
        
        [NonSerialized()]
        private System.Func<System.Object> _work;
        
        public For_2() {
            _Init();
        }
        
        public For_2(System.Runtime.Serialization.SerializationInfo si, System.Runtime.Serialization.StreamingContext sc) {
            _Init();
        }
        
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("Object of type System.Func<System.Int32>. Specify a created object reference.")]
        [Category("Parameters")]
        public virtual System.Func<System.Int32> start {
            get {
                return _start;
            }
            set {
                if(_start != value)
{
object ov = _start;
_start = value;
OnParametersChangedInternal("start", ov, value);
};
            }
        }
        
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("Object of type System.Func<System.Int32>. Specify a created object reference.")]
        [Category("Parameters")]
        public virtual System.Func<System.Int32> step {
            get {
                return _step;
            }
            set {
                if(_step != value)
{
object ov = _step;
_step = value;
OnParametersChangedInternal("step", ov, value);
};
            }
        }
        
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("Object of type System.Func<System.Boolean>. Specify a created object reference.")]
        [Category("Parameters")]
        public virtual System.Func<System.Boolean> condition {
            get {
                return _condition;
            }
            set {
                if(_condition != value)
{
object ov = _condition;
_condition = value;
OnParametersChangedInternal("condition", ov, value);
};
            }
        }
        
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("Object of type System.Func<System.Object>. Specify a created object reference.")]
        [Category("Parameters")]
        public virtual System.Func<System.Object> work {
            get {
                return _work;
            }
            set {
                if(_work != value)
{
object ov = _work;
_work = value;
OnParametersChangedInternal("work", ov, value);
};
            }
        }
        
        public override string Parameters {
            get {
                StringBuilder sb = new StringBuilder();
sb.Append("(");
sb.Append(null != _start ? ( !_start.ToString().StartsWith("&") && _start.GetType().Equals(typeof(string)) ? "\"" + _start.ToString() + "\"" : _start.ToString()) : "null");
sb.Append(", ");
sb.Append(null != _step ? ( !_step.ToString().StartsWith("&") && _step.GetType().Equals(typeof(string)) ? "\"" + _step.ToString() + "\"" : _step.ToString()) : "null");
sb.Append(", ");
sb.Append(null != _condition ? ( !_condition.ToString().StartsWith("&") && _condition.GetType().Equals(typeof(string)) ? "\"" + _condition.ToString() + "\"" : _condition.ToString()) : "null");
sb.Append(", ");
sb.Append(null != _work ? ( !_work.ToString().StartsWith("&") && _work.GetType().Equals(typeof(string)) ? "\"" + _work.ToString() + "\"" : _work.ToString()) : "null");
sb.Append(")");
return sb.ToString();
            }
        }
        
        private void _Init() {
            Name = "For";
            myTypeRefs = new Dictionary<string, Type>();
Type[] paramTypes = new Type[] { typeof(System.Func<System.Int32>), typeof(System.Func<System.Int32>), typeof(System.Func<System.Boolean>), typeof(System.Func<System.Object>)};			ParameterModifier pm = new ParameterModifier(4);
			ParameterModifier[] paramModifiers = new ParameterModifier[] { pm };
pm[0] = false;
myTypeRefs.Add("start", typeof(System.Func<System.Int32>));
pm[1] = false;
myTypeRefs.Add("step", typeof(System.Func<System.Int32>));
pm[2] = false;
myTypeRefs.Add("condition", typeof(System.Func<System.Boolean>));
pm[3] = false;
myTypeRefs.Add("work", typeof(System.Func<System.Object>));

;
            Action = typeof(SampleLib.Logics).GetMethod("For", BindingFlags.Public | BindingFlags.Instance, null, paramTypes, paramModifiers);
            InstanceType = typeof(SampleLib.Logics);;
        }
    }
}
