//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _SampleLib.MyInterfaceGenerics {
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
    public class Constructor : ubta.UseCase.DefaultActivity {
        
        private System.Collections.Generic.Dictionary<string, System.Type> myTypeRefs;
        
        private string _T1 = "SampleLib.IA";
        
        private string _T2 = "SampleLib.IB";
        
        [NonSerialized()]
        private string @__rv;
        
        public Constructor() {
            _Init();
        }
        
        public Constructor(System.Runtime.Serialization.SerializationInfo si, System.Runtime.Serialization.StreamingContext sc) {
            _Init();
        }
        
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("Specify a generic argument of type SampleLib.IA")]
        [Category("GenericParameters")]
        public virtual string T1 {
            get {
                return _T1;
            }
            set {
                _T1 = value;
            }
        }
        
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("Specify a generic argument of type SampleLib.IB")]
        [Category("GenericParameters")]
        public virtual string T2 {
            get {
                return _T2;
            }
            set {
                _T2 = value;
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
sb.Append(")");
return sb.ToString();
            }
        }
        
        private void _Init() {
            Name = ".ctor";
            myTypeRefs = new Dictionary<string, Type>();
Type[] paramTypes = new Type[] { };			ParameterModifier[] paramModifiers = null;

;
            Action = typeof(SampleLib.MyInterfaceGenerics<SampleLib.IA, SampleLib.IB>).GetConstructor( BindingFlags.Public | BindingFlags.Instance, null, paramTypes, paramModifiers);
            InstanceType = typeof(SampleLib.MyInterfaceGenerics<SampleLib.IA, SampleLib.IB>);;
        }
    }
}
