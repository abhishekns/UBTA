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
    public class set_MyEnum : ubta.UseCase.DefaultActivity {
        
        private System.Collections.Generic.Dictionary<string, System.Type> myTypeRefs;
        
        [NonSerialized()]
        private SampleLib.MyEnum _value;
        
        public set_MyEnum() {
            _Init();
        }
        
        public set_MyEnum(System.Runtime.Serialization.SerializationInfo si, System.Runtime.Serialization.StreamingContext sc) {
            _Init();
        }
        
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("Object of type SampleLib.MyEnum. Specify a created object reference.")]
        [Category("Parameters")]
        public virtual SampleLib.MyEnum value {
            get {
                return _value;
            }
            set {
                if(_value != value)
{
object ov = _value;
_value = value;
OnParametersChangedInternal("value", ov, value);
};
            }
        }
        
        public override string Parameters {
            get {
                StringBuilder sb = new StringBuilder();
sb.Append("(");
sb.Append(_value.ToString());
sb.Append(")");
return sb.ToString();
            }
        }
        
        private void _Init() {
            Name = "set_MyEnum";
            myTypeRefs = new Dictionary<string, Type>();
Type[] paramTypes = new Type[] { typeof(SampleLib.MyEnum)};			ParameterModifier pm = new ParameterModifier(1);
			ParameterModifier[] paramModifiers = new ParameterModifier[] { pm };
pm[0] = false;
myTypeRefs.Add("value", typeof(SampleLib.MyEnum));

;
            Action = typeof(SampleLib.MySampleClass).GetMethod("set_MyEnum", BindingFlags.Public | BindingFlags.Instance, null, paramTypes, paramModifiers);
            InstanceType = typeof(SampleLib.MySampleClass);;
        }
    }
}
