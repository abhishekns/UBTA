//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _SampleLib.MySampleClass {
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
    public class AddSomeInt : ubta.UseCase.DefaultActivity {
        
        private System.Collections.Generic.Dictionary<string, System.Type> myTypeRefs;
        
        [NonSerialized()]
        private string _x;
        
        public AddSomeInt() {
            _Init();
        }
        
        public AddSomeInt(System.Runtime.Serialization.SerializationInfo si, System.Runtime.Serialization.StreamingContext sc) {
            _Init();
        }
        
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("Object of type System.Int32. Specify a created object reference.")]
        [Category("Parameters")]
        public virtual string x {
            get {
                return _x;
            }
            set {
                if(_x != value)
{
object ov = _x;
_x = value;
OnParametersChangedInternal("x", ov, value);
};
            }
        }
        
        public override string Parameters {
            get {
                StringBuilder sb = new StringBuilder();
sb.Append("(");
sb.Append(null != _x ? ( !_x.ToString().StartsWith("&") && _x.GetType().Equals(typeof(string)) ? "\"" + _x.ToString() + "\"" : _x.ToString()) : "null");
sb.Append(")");
return sb.ToString();
            }
        }
        
        private void _Init() {
            Name = "AddSomeInt";
            myTypeRefs = new Dictionary<string, Type>();
Type[] paramTypes = new Type[] { typeof(System.String).MakeByRefType()};			ParameterModifier pm = new ParameterModifier(1);
			ParameterModifier[] paramModifiers = new ParameterModifier[] { pm };
pm[0] = true;
myTypeRefs.Add("x", typeof(System.Int32));

;
            Action = typeof(SampleLib.MySampleClass).GetMethod("AddSomeInt", BindingFlags.Public | BindingFlags.Instance, null, paramTypes, paramModifiers);
            InstanceType = typeof(SampleLib.MySampleClass);;
        }
    }
}