using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp;


namespace ubta.Reflection.Editable.ClassGenerator
{
    public class Generator : IGenerator
    {
        protected Assembly myAss = null;
        protected CSharpCodeProvider myProvider = new CSharpCodeProvider();
        protected List<CodeCompileUnit> myCcus = new List<CodeCompileUnit>();
        protected Dictionary<string, string> myOutFileAndContents = new Dictionary<string, string>();
        protected string BASE_NAME_SPACE = "Editable";
        protected string BASE_CLASS = "EditableObject";
        public Generator(Assembly ass_in)
        {
            myAss = ass_in;
            
        }
        private IElement myRoot = null;
        public IElement BuildTree()
        {
            myRoot = new Element();
            Type[] types = myAss.GetExportedTypes();
            foreach (Type t in types)
            {
                IElement e = new Element();
                e.ObjectType = t;
                if (t.IsAbstract)
                {
                    if (!myRoot.AddElement(e))
                    {
                        myRoot.Children.Add(e);
                        e.Parent = myRoot;
                    }
                }
            }
            foreach (Type t in types)
            {
                IElement e = new Element();
                e.ObjectType = t;
                if (!t.IsAbstract)
                {
                    if (!myRoot.AddElement(e))
                    {
                        myRoot.Children.Add(e);
                        e.Parent = myRoot;
                    }
                }
            }
            return myRoot;
        }

        public virtual void GenerateClasses()
        {
            foreach (IElement e in myRoot.Children)
            {
                GenerateClass(e);
            }
            CompilerParameters cp = CreateCompileParameters();
            CompilerResults cr = myProvider.CompileAssemblyFromSource(cp, myOutFileAndContents.Values.ToArray());
            cr.TempFiles.KeepFiles = true;
            foreach (var p in cr.Output)
            {
                Console.WriteLine(p);
            }
            foreach (var p in cr.Errors)
            {
                Console.WriteLine(p);
            }
            
            Console.WriteLine("Assembly available @ {0}", cr.CompiledAssembly.Location);
        }

        protected virtual CompilerParameters CreateCompileParameters()
        {
            var cp = new CompilerParameters();
            cp.TempFiles.KeepFiles = true;
            cp.OutputAssembly = string.Format("{0}{1}{2}{3}", Constants.ASSEMBLY_DIR,
                myAss.GetName().Name, Constants.ASSEMBLY_NAME_EXT, Constants.DLL_EXT);
            cp.ReferencedAssemblies.Add(myAss.GetName().Name + Constants.DLL_EXT);
            cp.ReferencedAssemblies.Add(Constants.SYSTEM_DLL);
            cp.ReferencedAssemblies.Add(Constants.SYSTEM_CORE_DLL);
            cp.ReferencedAssemblies.Add(Constants.NAME_SPACE + Constants.DLL_EXT);
            cp.ReferencedAssemblies.Add(Constants.ASSEMBLY_NAME);
            cp.CompilerOptions = Environment.ExpandEnvironmentVariables(string.Format("{0} \"/lib:{1};{2};{3}\"",
                Constants.DEFAULT_COMPILER_OPTIONS, Constants.LIB_PATH_FW_2_0, Constants.ASSEMBLY_DIR, Constants.LIB_PATH_FW_3_5));
            cp.IncludeDebugInformation = true;
            foreach (var v in myAss.GetReferencedAssemblies())
            {
                cp.ReferencedAssemblies.Add(v.Name + Constants.DLL_EXT);
            }
            return cp;
        }

        protected virtual void GenerateClass(IElement e)
        {
            GenerateClass(typeof(EditableObject).FullName, e);
            string pc = e.ObjectType.FullName;

            foreach (IElement ce in e.Children)
            {
                GenerateClass(pc, ce);
            }
        }

        protected virtual void GenerateClass(string pc, IElement e)
        {
            
            string npc = e.ObjectType.Name + Constants.ASSEMBLY_NAME_EXT;
            string sourceFile = Constants.FILE_OUTPUT_PATH + 
                npc +
                (myProvider.FileExtension[0].Equals('.') ? myProvider.FileExtension : "." + myProvider.FileExtension);

            if (myOutFileAndContents.ContainsKey(sourceFile))
            {
                return;
            }
            #region local variables
            string newName = e.ObjectType.Name + BASE_CLASS;
            string newNS = e.ObjectType.Namespace + "." + BASE_NAME_SPACE;
            string newFullName = newNS + "." + e.ObjectType.Name + BASE_CLASS;
            IndentedTextWriter itw = new IndentedTextWriter(new StreamWriter(sourceFile));
            CodeCompileUnit ccu = new CodeCompileUnit();
            Type t = e.ObjectType;

            #endregion

            #region Imports And Namespace
            var cn = UpdateImportsAndsNameSpaces(ccu, newNS);
            #endregion

            #region class
            CodeTypeDeclaration ctd = CreateCodeClass(pc, newName);

            #endregion

            #region Attribs
            UpdateClassAttributes(ctd, t);
            #endregion

            #region Constructors
            UpdateConstructors(ctd);
            #endregion

            #region members
            UpdateMembers(ctd, t);
            #endregion

            #region generate class file
            cn.Types.Add(ctd);

            myProvider.GenerateCodeFromCompileUnit(ccu, itw, new CodeGeneratorOptions());
            itw.Close();
            myOutFileAndContents.Add(sourceFile, File.ReadAllText(sourceFile));
            Console.WriteLine("Generated file {0}", sourceFile);
            myCcus.Add(ccu);

            foreach (IElement ce in e.Children)
            {
                GenerateClass(newFullName, ce);
            }
            #endregion
        }

        protected virtual void UpdateMembers(CodeTypeDeclaration ctd, Type t)
        {
            PropertyInfo[] properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (var p in properties)
            {
                CodeMemberProperty cmp = new CodeMemberProperty();
                cmp.Attributes = MemberAttributes.Public;
                cmp.Name = p.Name;
                cmp.Type = new CodeTypeReference(p.PropertyType.FullName);
                if (!IsOverridden(p, t))
                {
                    if (cmp.HasSet = p.GetSetMethod() != null)
                    {
                        ParameterInfo[] ps = p.GetSetMethod().GetParameters();
                        if (ps.Length > 1)
                        {
                            foreach (var cp in ps)
                            {
                                cmp.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(cp.ParameterType), cp.Name));
                            }
                            cmp.GetStatements.Add(new CodeAssignStatement(
                                new CodeIndexerExpression(
                                new CodeCastExpression(new CodeTypeReference(t), new CodeFieldReferenceExpression(null, Constants.ACTUAL_OBJECT))),
                                new CodePropertySetValueReferenceExpression()));

                        }
                        cmp.SetStatements.Add(new CodeAssignStatement(
                            new CodePropertyReferenceExpression(
                                new CodeCastExpression(new CodeTypeReference(t), new CodeFieldReferenceExpression(null, Constants.ACTUAL_OBJECT)),
                                p.Name),
                            new CodePropertySetValueReferenceExpression()));
                        cmp.SetStatements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(null, Constants.CHANGED_METHOD)));
                    }
                    if (cmp.HasGet = p.GetGetMethod() != null)
                    {
                        ParameterInfo[] ps = p.GetGetMethod().GetParameters();
                        if (ps.Length > 0)
                        {
                            foreach (var cp in ps)
                            {
                                cmp.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(cp.ParameterType), cp.Name));
                            }
                            cmp.GetStatements.Add(new CodeMethodReturnStatement(new CodeIndexerExpression(
                                new CodeCastExpression(new CodeTypeReference(t), new CodeFieldReferenceExpression(null, Constants.ACTUAL_OBJECT)),
                                    new CodeVariableReferenceExpression(ps[0].Name))));
                        }
                        else
                        {
                            cmp.GetStatements.Add(new CodeMethodReturnStatement(new CodePropertyReferenceExpression(
                                    new CodeCastExpression(new CodeTypeReference(t), new CodeFieldReferenceExpression(null, Constants.ACTUAL_OBJECT)),
                                    p.Name)));
                        }
                    }
                    ctd.Members.Add(cmp);
                }
            }
        }

        protected virtual void UpdateConstructors(CodeTypeDeclaration ctd)
        {
            ConstructorInfo[] constructors = typeof(EditableObject).GetConstructors(BindingFlags.Public | BindingFlags.Instance);

            foreach (var c in constructors)
            {
                CodeConstructor cc = new CodeConstructor();
                foreach (var cp in c.GetParameters())
                {
                    cc.Attributes = MemberAttributes.Public;
                    cc.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(cp.ParameterType), cp.Name));
                    cc.BaseConstructorArgs.Add(new CodeVariableReferenceExpression(cp.Name));
                }
                ctd.Members.Add(cc);
            }
        }

        protected virtual void UpdateClassAttributes(CodeTypeDeclaration ctd, Type objType)
        {
            if (objType.IsAbstract)
            {
                ctd.TypeAttributes |= TypeAttributes.Abstract;
            }
            if (objType.IsSealed)
            {
                ctd.TypeAttributes |= TypeAttributes.Sealed;
            }
            if (objType.IsSerializable)
            {
                ctd.TypeAttributes |= TypeAttributes.Serializable;
            }
            if (objType.IsInterface)
            {
            }
            if (objType.IsEnum)
            {
            }
        }

        protected virtual CodeTypeDeclaration CreateCodeClass(string pc, string newName)
        {
            var ctd = new CodeTypeDeclaration(newName);
            
            ctd.BaseTypes.Add(pc);

            ctd.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("TypeConverter"), 
                new CodeAttributeArgument( new CodeTypeOfExpression("ExpandableObjectConverter"))));
            return ctd;
        }

        protected virtual CodeNamespace UpdateImportsAndsNameSpaces(CodeCompileUnit ccu, string newNS)
        {
            CodeNamespace cn = new CodeNamespace(newNS);
            cn.Imports.Add(new CodeNamespaceImport(Constants.SYSTEM_NS));
            cn.Imports.Add(new CodeNamespaceImport(Constants.SYSTEM_COMPONENTMODEL_NS));
            cn.Imports.Add(new CodeNamespaceImport(Constants.NAME_SPACE));

            ccu.Namespaces.Add(cn);
            return cn;
        }

        protected bool IsOverridden(PropertyInfo p, Type t)
        {
            Type baseType = t.BaseType;
            bool r = false;
            while (baseType != null)
            {
                PropertyInfo bp = baseType.GetProperty(p.Name);
                if (null != bp)
                {
                    r = true;
                    break;
                }
                baseType = baseType.BaseType;
            }
            return r;
        }

        protected bool IsOverridden(MethodInfo p, Type t)
        {
            Type baseType = t.BaseType;
            bool r = false;
            while (baseType != null)
            {
                MethodInfo bp = null;
                Exception e = null;
                try
                {
                    bp = baseType.GetMethod(p.Name);
                }
                catch (AmbiguousMatchException ame)
                {
                    e = ame;
                }
                if (null != bp || e != null)
                {
                    r = true;
                    break;
                }
                baseType = baseType.BaseType;
            }
            return r;
        }
    }
}
