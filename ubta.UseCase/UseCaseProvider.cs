using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ubta.Common;
using System.IO;
using System.Workflow.Activities;

namespace ubta.UseCase
{
    public class UseCaseProvider
    {
        private Dictionary<string, List<Type>> myTypeGroups = new Dictionary<string, List<Type>>();

        public UseCaseProvider()
        {
            LoadUseCaseTypes();
        }
        
        private void LoadUseCaseTypes()
        {
            string config = Constants.CONFIG_DIR + @"\AssemblyLoader.txt";
            string[] dlls = File.ReadAllLines(config);
            List<Assembly> asses = new List<Assembly>();
            foreach (var d in dlls)
            {
                d.Trim();
                var dPath = Environment.ExpandEnvironmentVariables(d);
                if (string.IsNullOrEmpty(dPath))
                {
                    continue;
                }
                asses.Add(Assembly.LoadFile(dPath));
            }
            foreach (var ra in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
            {
                asses.Add(Assembly.Load(ra));
            }
            var dat = typeof(DefaultActivity);
            var swa = typeof(SequentialWorkflowActivity);
            foreach (var a in asses)
            {
                foreach (Type t in a.GetTypes())
                {
                    if (dat.IsAssignableFrom(t) || swa.IsAssignableFrom(t))
                    {
                        if (myTypeGroups.ContainsKey(t.Namespace))
                        {
                            if (!myTypeGroups[t.Namespace].Contains(t))
                            {
                                myTypeGroups[t.Namespace].Add(t);
                            }
                        }
                        else
                        {
                            myTypeGroups.Add(t.Namespace, new List<Type>() { t });
                        }
                    }
                }
            }
        }

        public List<Type> this[string ns]
        {
            get
            {
                List<Type> o;
                if (myTypeGroups.TryGetValue(ns, out o))
                {
                    return new List<Type>(o);
                }
                else
                {
                    o = new List<Type>();
                }
                return o;
            }
        }

        public IEnumerable<string> NameSpaces
        {
            get
            {
                return myTypeGroups.Keys.AsEnumerable();
            }
        }
    }
}
