//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _SampleLib.IB {
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
    public class get_Value : ubta.UseCase.DefaultActivity {
        
        private System.Collections.Generic.Dictionary<string, System.Type> myTypeRefs;
        
        [NonSerialized()]
        private string _get_Value_rv;
        
        public get_Value() {
            _Init();
        }
        
        public get_Value(System.Runtime.Serialization.SerializationInfo si, System.Runtime.Serialization.StreamingContext sc) {
            _Init();
        }
        
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("Object of type System.String. Specify a new or existing object reference.")]
        [Category("ReturnValue")]
        public virtual string get_Value_rv {
            get {
                return _get_Value_rv;
            }
            set {
                _get_Value_rv = value;
            }
        }
        
        public override string Parameters {
            get {
                StringBuilder sb = new StringBuilder();
sb.Append("(");
sb.Append(")");
return sb.ToString();
            }
        }
        
        private void _Init() {
            Name = "get_Value";
            myTypeRefs = new Dictionary<string, Type>();
Type[] paramTypes = new Type[] { };			ParameterModifier[] paramModifiers = null;

;
            Action = typeof(SampleLib.IB).GetMethod("get_Value", BindingFlags.Public | BindingFlags.Instance, null, paramTypes, paramModifiers);
            InstanceType = typeof(SampleLib.IB);;
        }
    }
}
