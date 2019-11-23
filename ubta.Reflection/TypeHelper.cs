#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : TypeHelper.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using ubta.Common;
using System.IO;

namespace ubta.Reflection
{
    public static class ReflectionHelper
    {
        public static bool IsOverridden(this PropertyInfo p)
        {
            Type baseType = p.ReflectedType.BaseType;
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

        public static bool IsOverridden(this MethodInfo p)
        {
            Type baseType = p.ReflectedType.BaseType;
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

        public static bool IsOverridden(this MethodBase mb)
        {
            var mi = mb as MethodInfo;
            if (null != mi)
            {
                return mi.IsOverridden();
            }
            return false;
        }

        public static string GetValidTypeNameForXml(this Type t)
        {
            if (null == t)
            {
                return string.Empty;
            }
            string tn = t.FullName;
            if (t.IsGenericParameter)
            {
                tn = TypeHelper.GetConstraints(t.GetGenericParameterConstraints(), new TypeHelper());
            }
            if (t.IsGenericType)
            {
            }
            if (!string.IsNullOrEmpty(tn))
            {
                int idx = -1;
                if (-1 != (idx = tn.IndexOf("`")))
                {
                    tn = tn.Substring(0, idx);
                }
                if (-1 != (idx = tn.IndexOf("&")))
                {
                    tn = tn.Substring(0, idx);
                }
                tn.Replace('+', '.');
                return tn;
            }
            return string.Empty;
        }

        public static bool IsOverloaded(this MethodBase mb)
        {
            var mi = mb as MethodInfo;
            if (null != mi)
            {
                return mi.IsOverloaded();
            }
            var ci = mb as ConstructorInfo;
            if (null != ci)
            {
                return ci.IsOverloaded();
            }
            return false;
        }

        public static bool IsOverloaded(this ConstructorInfo ci)
        {
            Type t = ci.ReflectedType;
            bool mbo = ci.IsOverridden();
            var cs = from c in t.GetConstructors(BindingFlags.Public | BindingFlags.Instance) where c.IsOverridden() == false select c;
            return cs.Count() > 1;
        }

        public static bool IsOverloaded(this MethodInfo mi)
        {
            Type t = mi.ReflectedType;
            bool mbo = mi.IsOverridden();
            foreach (var m in t.GetMethods())
            {
                if (m.IsOverridden())
                {
                    continue;
                }
                if (!mi.Equals(m) && mi.Name == m.Name)
                {
                    return true;
                }
            }
            return false;
        }

        public static int Inputs(this MethodBase mb)
        {
            int r = 0;
            if (null != mb)
            {
                foreach (var p in mb.GetParameters())
                {
                    if (p.IsOut)
                    {
                    }
                    else
                    {
                        r++;
                    }
                }
            }
            return r;
        }

        public static int Outputs(this MethodBase mb)
        {
            int r = 0;
            if (null != mb)
            {
                foreach (var p in mb.GetParameters())
                {
                    if (p.IsOut || p.ParameterType.IsByRef)
                    {
                        r++;
                    }
                    else
                    {
                    }
                }
            }
            return r;
        }

        public static string GetCodeDeclarationStatement(this Type t)
        {
            if (t.IsGenericType)
            {
                string name = t.GetValidTypeNameForXml();
                return name + GetGenericDeclarationStatement(t.GetGenericArguments());
            }
            return t.GetValidTypeNameForXml();
        }

        public static string GetGenericDeclarationStatement(Type[] ga)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<");
            for (int i = 0; i < ga.Length; i++)
            {
                TypeExtension te = new TypeExtension(ga[i], new TypeHelper());
                sb.Append(te.Constraints);
                if ((i < ga.Length - 1 && ga.Length > 1))
                {
                    sb.Append(Constants.PARAM_SEPARATOR);
                }
            }
            sb.Append(">");
            return sb.ToString();
        }

        public static string GetConstraints(this Type t)
        {
            StringBuilder sb = new StringBuilder();
            if (t.IsGenericParameter)
            {
                Type[] gpc = t.GetGenericParameterConstraints();
                sb.Append(GetConstraints(gpc));
            }
            else
            {
                sb.Append(t.FullName);
            }
            return sb.ToString();
        }

        public static string GetGenericParametersStatement(Type[] ga, out string whereClause, TypeHelper helper_in)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder whereclause = new StringBuilder();
            sb.Append("<");
            for (int i = 0; i < ga.Length; i++)
            {
                TypeExtension te = new TypeExtension(ga[i], helper_in);
                sb.Append(te.CsharpName);
                whereclause.Append(" where ").Append(te.CsharpName).Append(" : ").Append(te.Constraints);
                if ((i < ga.Length - 1 && ga.Length > 1))
                {
                    sb.Append(Constants.PARAM_SEPARATOR);
                }
            }
            sb.Append(">");
            whereClause = whereclause.ToString();
            return sb.ToString();
        }

        public static string GetConstraints(Type[] gpc)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < gpc.Length; i++)
            {
                TypeExtension te = new TypeExtension(gpc[i], new TypeHelper());
                sb.Append(te.CsharpName);
                if (i < gpc.Length - 1 && gpc.Length > 1)
                {
                    sb.Append(Constants.PARAM_SEPARATOR);
                }
            }
            return sb.ToString();
        }
    }

    public class TypeHelper
    {
        public TypeHelper()
        {
        }

        public static string GetCodeDeclarationStatement(Type t)
        {
            if (t.IsGenericType)
            {
                string name = t.GetValidTypeNameForXml();
                return name + GetGenericDeclarationStatement(t.GetGenericArguments(), new TypeHelper());
            }
            return t.GetValidTypeNameForXml();
        }

        public static string GetGenericDeclarationStatement(Type[] ga, TypeHelper helper_in)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder whereclause = new StringBuilder();
            sb.Append("<");
            for (int i = 0; i < ga.Length; i++)
            {
                TypeExtension te = new TypeExtension(ga[i], helper_in);
                sb.Append(te.Constraints);
                if ((i < ga.Length - 1 && ga.Length > 1))
                {
                    sb.Append(Constants.PARAM_SEPARATOR);
                }
            }
            sb.Append(">");
            return sb.ToString();
        }

        public static string GetGenericParametersStatement(Type[] ga, out string whereClause, TypeHelper helper_in)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder whereclause = new StringBuilder();
            sb.Append("<");
            for (int i = 0; i < ga.Length; i++)
            {
                TypeExtension te = new TypeExtension(ga[i], helper_in);
                sb.Append(te.CsharpName);
                whereclause.Append(" where ").Append(te.CsharpName).Append(" : ").Append(te.Constraints);
                if ((i < ga.Length - 1 && ga.Length > 1))
                {
                    sb.Append(Constants.PARAM_SEPARATOR);
                }
            }
            sb.Append(">");
            whereClause = whereclause.ToString();
            return sb.ToString();
        }

        public static string GetConstraints(Type[] gpc, TypeHelper helper_in)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < gpc.Length; i++)
            {
                TypeExtension te = new TypeExtension(gpc[i], helper_in);
                sb.Append(te.CsharpName);
                if (i < gpc.Length - 1 && gpc.Length > 1)
                {
                    sb.Append(Constants.PARAM_SEPARATOR);
                }
            }
            return sb.ToString();
        }

        private Dictionary<string, TypeExtension> myTypes = new Dictionary<string, TypeExtension>();

        public void Init(IList<string> assemblies_in)
        {
            Assembly ass = null;

            assemblies_in = assemblies_in.Distinct().ToList();
            List<string> loadedAssemblies = new List<string>();
            foreach (string assembly in assemblies_in)
            {
                ass = LoadAssembly(assembly);
                foreach (var a in ass.GetReferencedAssemblies())
                {
                    if (!loadedAssemblies.Contains(a.Name))
                    {
                        loadedAssemblies.Add(a.Name);
                        LoadAssembly(a.Name + ".dll");
                    }
                }
            }

            //// default types
            //myTypes.Add(Constants.CLASS_STRING, new TypeExtension(typeof(System.String), this));
            //myTypes.Add(Constants.CLASS_INT32, new TypeExtension(typeof(System.Int32), this));
            //myTypes.Add(Constants.CLASS_INT64, new TypeExtension(typeof(System.Int64), this));
            //myTypes.Add(Constants.CLASS_SINGLE, new TypeExtension(typeof(System.Single), this));
            //myTypes.Add(Constants.CLASS_DECIMAL, new TypeExtension(typeof(System.Decimal), this));
            //myTypes.Add(Constants.CLASS_DOUBLE, new TypeExtension(typeof(System.Double), this));
            //myTypes.Add(Constants.CLASS_OBJECT, new TypeExtension(typeof(System.Object), this));

        }

        private Assembly LoadAssembly(string assembly)
        {
            Assembly ass = null;
            try
            {
                if (!assembly.Contains(":"))
                {
                    try
                    {
                        ass = Assembly.Load(assembly);
                    }
                    catch(Exception e)
                    {
                        ubta.Logging.Log.Warn("TypeHelper", "Error "+ e.Message +" occurred while loading " + assembly +". The assembly is ignored.");
                        // don't care if all are not loaded.
                    }
                }
                else
                {
                    ass = Assembly.LoadFile(assembly);
                }
            }
            catch (Exception e)
            {
                ubta.Logging.Log.Debug("TypeHelper", e.Message);
                ubta.Logging.Log.Debug("TypeHelper", e.StackTrace);
            }
            if (null != ass)
            {
                Type[] types = ass.GetExportedTypes();
                for (int i = 0; i < types.Length; i++)
                {
                    if (!myTypes.ContainsKey(types[i].GetValidTypeNameForXml()))
                    {
                        TypeExtension te = new TypeExtension(types[i], this);
                        myTypes.Add(te.T.GetValidTypeNameForXml(), te);
                    }
                }
            }
            return ass;
        }

        public IDictionary<string, TypeExtension> Mapping
        {
            get
            {
                return myTypes;
            }
        }

        public TypeExtension GetType(string typeName_in)
        {
            return myTypes[typeName_in];
        }
    }
}