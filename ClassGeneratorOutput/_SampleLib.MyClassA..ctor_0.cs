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
    public class Constructor_0 : ubta.UseCase.DefaultActivity {
        
        private System.Collections.Generic.Dictionary<string, System.Type> myTypeRefs;
        
        [NonSerialized()]
        private string _name;
        
        [NonSerialized()]
        private string _b;
        
        [NonSerialized()]
        private string @__rv;
        
        public Constructor_0() {
            _Init();
        }
        
        public Constructor_0(System.Runtime.Serialization.SerializationInfo si, System.Runtime.Serialization.StreamingContext sc) {
            _Init();
        }
        
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("Object of type System.String. Specify a created object reference.")]
        [Category("Parameters")]
        public virtual string name {
            get {
                return _name;
            }
            set {
                if(_name != value)
{
object ov = _name;
_name = value;
OnParametersChangedInternal("name", ov, value);
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
        
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("Object of type System.String. Specify a new or existing object reference.")]
        [Category("ReturnValue")]
        public virtual string _rv {
            get {
                return __rv;
            }
            set {
                __rv = value;
            }
        }
        
        public override string Parameters {
            get {
                StringBuilder sb = new StringBuilder();
sb.Append("(");
sb.Append(null != _name ? ( !_name.ToString().StartsWith("&") && _name.GetType().Equals(typeof(string)) ? "\"" + _name.ToString() + "\"" : _name.ToString()) : "null");
sb.Append(", ");
sb.Append(null != _b ? ( !_b.ToString().StartsWith("&") && _b.GetType().Equals(typeof(string)) ? "\"" + _b.ToString() + "\"" : _b.ToString()) : "null");
sb.Append(")");
return sb.ToString();
            }
        }
        
        private void _Init() {
            Name = ".ctor";
            myTypeRefs = new Dictionary<string, Type>();
Type[] paramTypes = new Type[] { typeof(System.String), typeof(System.String)};			ParameterModifier pm = new ParameterModifier(2);
			ParameterModifier[] paramModifiers = new ParameterModifier[] { pm };
pm[0] = false;
myTypeRefs.Add("name", typeof(System.String));
pm[1] = false;
myTypeRefs.Add("b", typeof(SampleLib.IB));

;
            Action = typeof(SampleLib.MyClassA).GetConstructor( BindingFlags.Public | BindingFlags.Instance, null, paramTypes, paramModifiers);
            InstanceType = typeof(SampleLib.MyClassA);;
        }
    }
}
