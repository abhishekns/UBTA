using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.Reflection;
using System.Workflow.ComponentModel;
using System.CodeDom.Compiler;
using System.Workflow.Activities;
using System.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.IO;

namespace ubta.Reflection.Editable.ClassGenerator
{
    public class ActivityClassGenerator : Generator
    {

        public ActivityClassGenerator(Assembly ass)
            :base(ass)
        {
            BASE_CLASS = "UseCases";
            BASE_NAME_SPACE = "UseCase";
        }

        protected override void GenerateClass(IElement e)
        {
            GenerateClass(typeof(Activity).FullName, e);
            string pc = e.ObjectType.FullName;

            foreach (IElement ce in e.Children)
            {
                GenerateClass(pc, ce);
            }
        }
        List<string> handledMethods = new List<string>();

        protected override CompilerParameters CreateCompileParameters()
        {
            var cp = new CompilerParameters();
            cp.TempFiles.KeepFiles = true;
            cp.OutputAssembly = string.Format("{0}{1}{2}{3}", Constants.ASSEMBLY_DIR,
                myAss.GetName().Name, "Activity", Constants.DLL_EXT);
            cp.ReferencedAssemblies.Add(myAss.GetName().Name + Constants.DLL_EXT);
            cp.ReferencedAssemblies.Add("ubta.UseCase.dll");
            cp.ReferencedAssemblies.Add(Constants.SYSTEM_DLL);
            cp.ReferencedAssemblies.Add(Constants.SYSTEM_CORE_DLL);
            cp.ReferencedAssemblies.Add(Constants.NAME_SPACE + Constants.DLL_EXT);
            cp.ReferencedAssemblies.Add("System.Workflow.Activities.dll");
            cp.ReferencedAssemblies.Add("System.Workflow.ComponentModel.dll");
            cp.ReferencedAssemblies.Add("System.Workflow.Runtime.dll");
            cp.ReferencedAssemblies.Add("System.Drawing.dll");
            cp.CompilerOptions = Environment.ExpandEnvironmentVariables(string.Format("{0} \"/lib:{1};{2};{3};{4}\"",
                Constants.DEFAULT_COMPILER_OPTIONS, Constants.LIB_PATH_FW_2_0, Constants.ASSEMBLY_DIR, Constants.LIB_PATH_EXTERN, Constants.LIB_PATH_FW_3_5));
            cp.IncludeDebugInformation = true;
            foreach (var v in myAss.GetReferencedAssemblies())
            {
                cp.ReferencedAssemblies.Add(v.Name + Constants.DLL_EXT);
            }
            return cp;
        }

        protected override CodeNamespace UpdateImportsAndsNameSpaces(CodeCompileUnit ccu, string newNS)
        {
            var cn = base.UpdateImportsAndsNameSpaces(ccu, newNS);
            cn.Imports.Add(new CodeNamespaceImport("System.Text"));
            cn.Imports.Add(new CodeNamespaceImport("System.Linq"));
            cn.Imports.Add(new CodeNamespaceImport("System.Reflection"));
            cn.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            cn.Imports.Add(new CodeNamespaceImport("System.ComponentModel"));
            cn.Imports.Add(new CodeNamespaceImport("System.Workflow.ComponentModel"));
            cn.Imports.Add(new CodeNamespaceImport("System.Workflow.ComponentModel.Design"));
            return cn;
        }

        protected override void GenerateClass(string pc, IElement e)
        {
            #region local variables
            Type t = e.ObjectType;
            string vcn = t.GetValidTypeNameForXml();
            string newName = vcn+BASE_CLASS;
            string newNS =  "_"+vcn;
            #endregion

            var methods = t.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            var constructors = t.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            var all = new List<MethodBase>();
            all.AddRange(methods);
            all.AddRange(constructors);
            handledMethods.Clear();
            var filteredhm = (from hm in all where !hm.IsOverridden() select hm).ToArray();
            
            foreach (var m in filteredhm)
            {
                string mname = m.Name;
                handledMethods.Add(mname);
                if (m.IsOverloaded())
                {
                    mname += "_" + ((from hm in handledMethods where hm == mname select hm).Count() - 1).ToString();
                }
                string sourceFile = Constants.FILE_OUTPUT_PATH + newNS + "." + mname +  
                    (myProvider.FileExtension[0].Equals('.') ? myProvider.FileExtension : "." + myProvider.FileExtension);
                if (!handledMethods.Contains(sourceFile) && !myOutFileAndContents.ContainsKey(sourceFile))
                {
                    IndentedTextWriter itw = new IndentedTextWriter(new StreamWriter(sourceFile));
                    CodeCompileUnit ccu = new CodeCompileUnit();

                    #region Imports And Namespace
                    var cn = UpdateImportsAndsNameSpaces(ccu, newNS);
                    #endregion

                    #region class
                    CodeTypeDeclaration ctd = CreateCodeClass(pc, newName);

                    #endregion

                    #region members
                    UpdateMembers(ctd, m);
                    #endregion

                    UpdateConstructors(ctd);

                    #region generate class file
                    cn.Types.Add(ctd);

                    myProvider.GenerateCodeFromCompileUnit(ccu, itw, new CodeGeneratorOptions());
                    itw.Close();
                    myOutFileAndContents.Add(sourceFile, File.ReadAllText(sourceFile));
                    Console.WriteLine("Generated file {0}", sourceFile);
                    myCcus.Add(ccu);
                    handledMethods.Add(sourceFile);
                }
            }
            foreach (IElement ce in e.Children)
            {
                GenerateClass(newName, ce);
            }
            #endregion
        }

        protected virtual void UpdateMembers(CodeTypeDeclaration ctdc, MethodBase m)
        {
            if (m.IsOverridden())
            {
                return;
            }
            #region  _Init Method
            CodeMemberMethod cmm = new CodeMemberMethod();

            cmm.Attributes = MemberAttributes.Private;
            cmm.Name = "_Init";
            string mname = m.Name;
            
            if(m.IsOverloaded())
            {
                mname += "_" + ((from hm in handledMethods where hm == mname select hm).Count()-1).ToString();
            }
            ctdc.Name = mname.Replace(".ctor", "Constructor");
            cmm.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(null, "Name"),
                new CodePrimitiveExpression(m.Name)));
            CodeMemberField dic = new CodeMemberField(new CodeTypeReference(typeof(Dictionary<string, Type>)), "myTypeRefs");
            ctdc.Members.Add(dic);
            var ptypes = m.GetParameters();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            int cnt = ptypes.Count();
            int i = 0;
            sb1.AppendLine("myTypeRefs = new Dictionary<string, Type>();");
            sb1.Append("Type[] paramTypes = new Type[] { ");
            if (cnt > 0)
            {
                sb2.AppendFormat("\t\t\tParameterModifier pm = new ParameterModifier({0});", cnt);
                sb2.AppendLine();
                sb2.AppendLine("\t\t\tParameterModifier[] paramModifiers = new ParameterModifier[] { pm };");
            }
            else
            {
                sb2.AppendLine("\t\t\tParameterModifier[] paramModifiers = null;");
            }
            List<string> paramNames = new List<string>();
            #region Parameters
            foreach (var p in ptypes)
            {
                StringBuilder sb3 = new StringBuilder();
                CodeMemberField cmf = new CodeMemberField();
                CodeMemberProperty cstp = new CodeMemberProperty();
                string ods = p.ParameterType.GetCodeDeclarationStatement();
                string ds = ods;
                bool nonSer = !p.ParameterType.IsSerializable;
                if (nonSer)
                {
                    ds = typeof(string).GetCodeDeclarationStatement();
                }

                if (p.ParameterType.IsByRef)
                {
                    sb1.Append("typeof(").Append(ds).Append(").MakeByRefType()");
                }
                else
                {
                    sb1.Append("typeof(").Append(ds).Append(")");
                }
                cstp.Attributes = MemberAttributes.Public;
                cmf.Attributes = MemberAttributes.Private;
                cstp.Name = p.Name;
                cmf.Name = "_" + p.Name;

                cstp.Type = new CodeTypeReference(ds);
                cmf.Type = new CodeTypeReference(ds);
                if (nonSer)
                {
                    
                }
                cstp.HasGet = true;
                cstp.HasSet = true;
                StringBuilder ssb = new StringBuilder();
                ssb.AppendFormat("if({0} != value)\n", cmf.Name);
                ssb.Append("{\n");
                ssb.AppendFormat("object ov = {0};\n", cmf.Name);
                ssb.AppendFormat("{0} = value;\n", cmf.Name);
                ssb.AppendFormat("OnParametersChangedInternal(\"{0}\", ov, value);\n", cstp.Name);
                ssb.Append("}");
                cstp.SetStatements.Add(new CodeSnippetExpression(ssb.ToString()));
                cstp.GetStatements.Add(new CodeSnippetExpression(string.Format("return {0}", cmf.Name)));
                /*
                 * [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("The target object to invoke the action on."),
        Category("Execution"), DefaultValue(null)]
                 */
                cmf.CustomAttributes.Add(new CodeAttributeDeclaration("NonSerialized"));
                cstp.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("Browsable"),
                new CodeAttributeArgument(new CodePrimitiveExpression(true))));
                cstp.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("EditorBrowsable"),
                new CodeAttributeArgument(new CodeFieldReferenceExpression(null, "EditorBrowsableState.Advanced"))));
                cstp.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("Description"),
                    new CodeAttributeArgument(new CodePrimitiveExpression(
                        string.Format("Object of type {0}. Specify a created object reference.", ods)))));
                cstp.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("Category"),
                    new CodeAttributeArgument(new CodePrimitiveExpression("Parameters"))));
                {
                    ctdc.Members.Add(cstp);
                    ctdc.Members.Add(cmf);
                    paramNames.Add(cmf.Name);
                }

                if (cnt > 0)
                {
                    sb2.AppendFormat("pm[{0}] = {1};", i, (p.IsOut | p.ParameterType.IsByRef).ToString().ToLowerInvariant());
                    sb2.AppendLine();
                    sb2.AppendFormat("myTypeRefs.Add(\"{0}\", typeof({1}));", cstp.Name, ods );
                    sb2.AppendLine();
                }
                if (cnt != 1 && i != cnt - 1)
                {
                    sb1.Append(", ");
                }
                i++;
            }
            #endregion

            sb1.Append("};");
            sb2.AppendLine();
            sb1.Append(sb2.ToString());

            CodeSnippetExpression cse = new CodeSnippetExpression(
                sb1.ToString()
                );
            cmm.Statements.Add(cse);

            string mcall = "GetMethod";
            string obj = string.Empty;
            if (m is ConstructorInfo)
            {
                mcall = "GetConstructor";
            }
            else
            {
                obj = "\"" + m.Name + "\",";
            }

            string cdsname = m.DeclaringType.GetCodeDeclarationStatement();
            cmm.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(null, "Action"),
                new CodeSnippetExpression(
                    string.Format("typeof({0}).{1}({2} BindingFlags.Public | BindingFlags.Instance, null, paramTypes, paramModifiers)",
                    cdsname, mcall, obj))));

            cmm.Statements.Add(new CodeSnippetExpression(
                string.Format("InstanceType = typeof({0});", cdsname)
                ));

            #region Generic Params
            if (m.DeclaringType.IsGenericType && m is ConstructorInfo)
            {
                Type[] gas = m.DeclaringType.GetGenericArguments();
                foreach (var ga in gas)
                {
                    string gads = ga.GetCodeDeclarationStatement();
                    CodeMemberField gacmf = new CodeMemberField(typeof(string), "_" + ga.Name);
                    gacmf.InitExpression = new CodePrimitiveExpression(gads);
                    CodeMemberProperty gacmp = new CodeMemberProperty();
                    gacmp.Name = ga.Name;
                    gacmp.Type = new CodeTypeReference(typeof(string));
                    
                    gacmp.HasGet = true;
                    gacmp.HasSet = true;
                    gacmp.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(null, gacmf.Name)));
                    gacmp.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(null, gacmf.Name), new CodePropertySetValueReferenceExpression()));
                    gacmp.Attributes = MemberAttributes.Public;
                    gacmp.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("Browsable"),
                new CodeAttributeArgument(new CodePrimitiveExpression(true))));
                    gacmp.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("EditorBrowsable"),
                    new CodeAttributeArgument(new CodeFieldReferenceExpression(null, "EditorBrowsableState.Advanced"))));
                    gacmp.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("Description"),
                        new CodeAttributeArgument(new CodePrimitiveExpression(
                            string.Format("Specify a generic argument of type {0}", gads)))));
                    gacmp.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("Category"),
                        new CodeAttributeArgument(new CodePrimitiveExpression("GenericParameters"))));
                    ctdc.Members.Add(gacmp);
                    ctdc.Members.Add(gacmf);
                }
            }
            #endregion

            ctdc.Members.Add(cmm);
            #endregion

            #region Return value
            var mi = m as MethodInfo;
            var ci = m as ConstructorInfo;
            Type rt = null;
            string rname = string.Empty;
            if (mi != null && !"System.Void".StartsWith(mi.ReturnParameter.ParameterType.GetCodeDeclarationStatement()))
            {
                rt = mi.ReturnParameter.ParameterType;
                rname = mname + "_rv";
            }
            else if (ci != null)
            {
                rt = m.ReflectedType;
                rname = "_rv";
            }
            if (null != rt)
            {
                var rvf = new CodeMemberField();
                var rvp = new CodeMemberProperty();
                string cds = typeof(string).GetCodeDeclarationStatement();
                rvp.Attributes = MemberAttributes.Public;
                rvf.Attributes = MemberAttributes.Private;
                rvp.Name = rname;
                rvf.Name = "_" + rname;
                rvp.Type = new CodeTypeReference(cds);
                rvf.Type = new CodeTypeReference(cds);
                rvp.HasGet = true;
                rvp.HasSet = true;
                rvp.SetStatements.Add(new CodeSnippetExpression(string.Format("{0} = value", rvf.Name)));
                rvp.GetStatements.Add(new CodeSnippetExpression(string.Format("return {0}", rvf.Name)));
                /*
                 * [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("The target object to invoke the action on."),
        Category("Execution"), DefaultValue(null)]
                 */
                rvf.CustomAttributes.Add(new CodeAttributeDeclaration("NonSerialized"));
                rvp.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("Browsable"),
                new CodeAttributeArgument(new CodePrimitiveExpression(true))));
                rvp.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("EditorBrowsable"),
                new CodeAttributeArgument(new CodeFieldReferenceExpression(null, "EditorBrowsableState.Advanced"))));
                rvp.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("Description"),
                    new CodeAttributeArgument(new CodePrimitiveExpression(string.Format("Object of type {0}. Specify a new or existing object reference.", cds)))));
                rvp.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("Category"),
                    new CodeAttributeArgument(new CodePrimitiveExpression("ReturnValue"))));
                ctdc.Members.Add(rvp);
                ctdc.Members.Add(rvf);
            }
            #endregion

            #region Parameters Property
            CodeMemberProperty paramp = new CodeMemberProperty();
            paramp.Type = new CodeTypeReference(typeof(string));
            paramp.HasGet = true;
            paramp.Name = "Parameters";
            paramp.Attributes = MemberAttributes.Override | MemberAttributes.Public;
            StringBuilder pnsb = new StringBuilder();
            pnsb.AppendLine("StringBuilder sb = new StringBuilder();");
            pnsb.AppendLine("sb.Append(\"(\");");
            cnt = paramNames.Count;
            for(i=0; i<cnt; i++)
            {
                var pn = paramNames[i];
                if (ptypes[i].ParameterType.IsValueType)
                {
                    pnsb.AppendFormat("sb.Append({0}.ToString());", pn);
                }
                else
                {
                    pnsb.AppendFormat("sb.Append(null != {0} ? ( !{0}.ToString().StartsWith(\"&\") && {0}.GetType().Equals(typeof(string)) ? ", pn);
                    pnsb.AppendFormat("\"\\\"\" + {0}.ToString() + \"\\\"\" : {0}.ToString()) : \"null\");", pn);
                }
                pnsb.AppendLine();
                if(cnt > 1 && i < cnt -1)
                {
                    pnsb.AppendFormat("sb.Append(\"{0}\");", Constants.PARAM_SEPARATOR);
                    pnsb.AppendLine();
                }
            }
            pnsb.AppendLine("sb.Append(\")\");");
            pnsb.Append("return sb.ToString()");
            paramp.GetStatements.Add(new CodeSnippetExpression(pnsb.ToString()));
            ctdc.Members.Add(paramp);
            #endregion
        }
         

        protected override void UpdateConstructors(CodeTypeDeclaration ctd)
        {
            CodeConstructor cc = new CodeConstructor();
            cc.Attributes = MemberAttributes.Public;
            cc.Name = ctd.Name;
            cc.Statements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(null, "_Init")));
            ctd.Members.Add(cc);
            cc = new CodeConstructor();
            cc.Name = ctd.Name;
            cc.Attributes = MemberAttributes.Public;
            cc.Parameters.Add(new CodeParameterDeclarationExpression("System.Runtime.Serialization.SerializationInfo", "si"));
            cc.Parameters.Add(new CodeParameterDeclarationExpression("System.Runtime.Serialization.StreamingContext", "sc"));
            cc.Statements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(null, "_Init")));
            ctd.Members.Add(cc);
        }

        protected override CodeTypeDeclaration CreateCodeClass(string pc, string newName)
        {
            var ctd = new CodeTypeDeclaration(newName);
            ctd.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("Serializable")));
            ctd.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("TypeConverter"),
                new CodeAttributeArgument(new CodeTypeOfExpression("ExpandableObjectConverter"))));
            ctd.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("Designer"),
                new CodeAttributeArgument(new CodePrimitiveExpression("ubta.UseCase.Designer.UseCaseDesigner"))));
            ctd.BaseTypes.Add(typeof(ubta.UseCase.DefaultActivity).FullName);
            return ctd;
        }

        protected override void UpdateClassAttributes(CodeTypeDeclaration ctd, Type objType)
        {

        }

    }
}
