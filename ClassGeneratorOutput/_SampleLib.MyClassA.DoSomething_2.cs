//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _SampleLib.MyClassA {
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
    public class DoSomething_2 : ubta.UseCase.DefaultActivity {
        
        private System.Collections.Generic.Dictionary<string, System.Type> myTypeRefs;
        
        [NonSerialized()]
        private string _a;
        
        [NonSerialized()]
        private string _b;
        
        public DoSomething_2() {
            _Init();
        }
        
        public DoSomething_2(System.Runtime.Serialization.SerializationInfo si, System.Runtime.Serialization.StreamingContext sc) {
            _Init();
        }
        
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("Object of type System.String. Specify a created object reference.")]
        [Category("Parameters")]
        public virtual string a {
            get {
                return _a;
            }
            set {
                if(_a != value)
{
object ov = _a;
_a = value;
OnParametersChangedInternal("a", ov, value);
};
            }
        }
        
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("Object of type SampleLib.IB. Specify a created object reference.")]
        [Category("Parameters")]
        public virtual string b {
            get {
                return _b;
            }
            set {
                if(_b != value)
{
object ov = _b;
_b = value;
OnParametersChangedInternal("b", ov, value);
};
            }
        }
        
        public override string Parameters {
            get {
                StringBuilder sb = new StringBuilder();
sb.Append("(");
sb.Append(null != _a ? ( !_a.ToString().StartsWith("&") && _a.GetType().Equals(typeof(string)) ? "\"" + _a.ToString() + "\"" : _a.ToString()) : "null");
sb.Append(", ");
sb.Append(null != _b ? ( !_b.ToString().StartsWith("&") && _b.GetType().Equals(typeof(string)) ? "\"" + _b.ToString() + "\"" : _b.ToString()) : "null");
sb.Append(")");
return sb.ToString();
            }
        }
        
        private void _Init() {
            Name = "DoSomething";
            myTypeRefs = new Dictionary<string, Type>();
Type[] paramTypes = new Type[] { typeof(System.String), typeof(System.String)};			ParameterModifier pm = new ParameterModifier(2);
			ParameterModifier[] paramModifiers = new ParameterModifier[] { pm };
pm[0] = false;
myTypeRefs.Add("a", typeof(System.String));
pm[1] = false;
myTypeRefs.Add("b", typeof(SampleLib.IB));

;
            Action = typeof(SampleLib.MyClassA).GetMethod("DoSomething", BindingFlags.Public | BindingFlags.Instance, null, paramTypes, paramModifiers);
            InstanceType = typeof(SampleLib.MyClassA);;
        }
    }
}
