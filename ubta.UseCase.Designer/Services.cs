
namespace ubta.UseCase.Designer
{

    using System;
    using System.IO;
    using System.Text;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Collections;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.ComponentModel.Design.Serialization;
    using System.Workflow.ComponentModel.Compiler;
    using System.Workflow.ComponentModel;
    using System.Workflow.ComponentModel.Design;
    using System.Windows.Forms;
    using System.Drawing;
    using System.Reflection;
    using System.Linq;
    using ubta.Common;
    using ubta.Reflection;

    public class TypeResolutionService : ITypeResolutionService
    {
        private static TypeResolutionService myInstance;
             
        public static ITypeResolutionService Instance
        {
            get
            {
                if (null == myInstance)
                {
                    myInstance = new TypeResolutionService();
                }
                return myInstance;
            }
        }

        #region ITypeResolutionService Members

        private List<Assembly> myWellKnown = new List<Assembly>();
        private TypeResolutionService()
        {
            string config = Constants.CONFIG_DIR + @"\AssemblyLoader.txt";
            string[] dlls = File.ReadAllLines(config);
            var dgbConfig = Assembly.GetExecutingAssembly().CodeBase.Contains(Constants.RELEASE_TYPE_DEBUG);
            foreach (var d in dlls)
            {
                var dPath = d.Trim();
                string conf = Constants.RELEASE_TYPE_RELEASE;
                if (dgbConfig)
                {
                    conf = Constants.RELEASE_TYPE_DEBUG;
                }
                dPath = dPath.Replace("%Configuration%", conf);
                dPath = Environment.ExpandEnvironmentVariables(dPath);
                if (string.IsNullOrEmpty(dPath))
                {
                    continue;
                }
                myWellKnown.Add(Assembly.LoadFile(dPath));
            }
            foreach (var ra in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
            {
                myWellKnown.Add(Assembly.Load(ra));
            }
        }

        public System.Reflection.Assembly GetAssembly(System.Reflection.AssemblyName name, bool throwOnError)
        {
            return Assembly.Load(name.FullName);
        }

        public System.Reflection.Assembly GetAssembly(System.Reflection.AssemblyName name)
        {
            return Assembly.Load(name.FullName);
        }

        public string GetPathOfAssembly(System.Reflection.AssemblyName name)
        {
            var fsi = new DirectoryInfo(ubta.Common.Constants.DEPLOYMENT_DIR);
            var files = fsi.GetFiles(name.Name);
            return files.FirstOrDefault().FullName;
        }

        public Type GetType(string name, bool throwOnError, bool ignoreCase)
        {
            int idx = -1;
            string cname = name;
            if (0 <= (idx = name.IndexOf(',')))
            {
                cname = name.Substring(0, idx);
            }
            foreach(var a in myWellKnown)
            {
                Type[] types = a.GetTypes();
                foreach (var t in types)
                {
                    if (ignoreCase)
                    {
                        if (t.GetValidTypeNameForXml().Equals(cname, StringComparison.InvariantCultureIgnoreCase))
                        {
                            return t;
                        }
                        if (t.FullName.Equals(cname, StringComparison.InvariantCultureIgnoreCase))
                        {
                            return t;
                        }
                    }
                    else if (t.GetValidTypeNameForXml().Equals(cname))
                    {
                        return t;
                    }
                    else if (t.FullName.Equals(cname))
                    {
                        return t;
                    }
                }
            }
            if (throwOnError)
            {
                throw new TypeLoadException(string.Format("Type {0} was not found", name));
            }
            return null;
        }

        public Type GetType(string name, bool throwOnError)
        {
            return GetType(name, throwOnError, false);
        }

        public Type GetType(string name)
        {
            return GetType(name, false, false);
        }

        public void ReferenceAssembly(System.Reflection.AssemblyName name)
        {
            Console.WriteLine("ReferenceAssembly");
        }

        public IEnumerable<Assembly> WellKnownAssemblies
        {
            get
            {
                return myWellKnown.AsEnumerable();
            }
        }

        #endregion
    }

    #region IdentifierCreationService
    internal sealed class IdentifierCreationService : IIdentifierCreationService
    {
        internal IdentifierCreationService(IServiceProvider serviceProvider)
        {
        }

        void IIdentifierCreationService.ValidateIdentifier(Activity activity, string identifier)
        {
            if (identifier == null)
                throw new ArgumentNullException("identifier");
            if (activity == null)
                throw new ArgumentNullException("activity");

            if (string.Compare(activity.Name, identifier, true) == 0)
                return;

            ArrayList identifiers = new ArrayList();
            Activity rootActivity = GetRootActivity(activity);
            identifiers.AddRange(GetIdentifiersInCompositeActivity(rootActivity as CompositeActivity));
            identifiers.Sort();
            if (identifiers.BinarySearch(identifier.ToLower(), StringComparer.OrdinalIgnoreCase) >= 0)
                throw new ArgumentException(string.Format("Duplicate Component Identifier {0}", identifier));
        }

        void IIdentifierCreationService.EnsureUniqueIdentifiers(CompositeActivity parentActivity, ICollection childActivities)
        {
            if (parentActivity == null)
                throw new ArgumentNullException("parentActivity");
            if (childActivities == null)
                throw new ArgumentNullException("childActivities");

            ArrayList allActivities = new ArrayList();

            Queue activities = new Queue(childActivities);
            while (activities.Count > 0)
            {
                Activity activity = (Activity)activities.Dequeue();
                if (activity is CompositeActivity)
                {
                    foreach (Activity child in ((CompositeActivity)activity).Activities)
                        activities.Enqueue(child);
                }

                //If we are moving activities, we need not regenerate their identifiers
                if (((IComponent)activity).Site != null)
                    continue;

                allActivities.Add(activity);
            }

            // get the root activity
            CompositeActivity rootActivity = GetRootActivity(parentActivity) as CompositeActivity;
            ArrayList identifiers = new ArrayList(); // all the identifiers in the workflow
            identifiers.AddRange(GetIdentifiersInCompositeActivity(rootActivity));

            foreach (IUseCase activity in allActivities)
            {
                activity.Name = activity.NewUseCaseName();
                activity.ActionName = activity.GetType().Name;
                activity.InstanceName = activity.GetInstanceName(rootActivity);
                identifiers.Add(activity.Name);
            }
        }

        private static IList GetIdentifiersInCompositeActivity(CompositeActivity compositeActivity)
        {
            ArrayList identifiers = new ArrayList();
            if (compositeActivity != null)
            {
                identifiers.Add(compositeActivity.Name);
                IList<Activity> allChildren = GetAllNestedActivities(compositeActivity);
                foreach (IUseCase activity in allChildren)
                {
                    identifiers.Add(activity.ActionName);
                }
            }
            return ArrayList.ReadOnly(identifiers);
        }

        private static Activity GetRootActivity(Activity activity)
        {
            if (activity == null)
                throw new ArgumentException("activity");

            while (activity.Parent != null)
                activity = activity.Parent;

            return activity;
        }

        private static Activity[] GetAllNestedActivities(CompositeActivity compositeActivity)
        {
            if (compositeActivity == null)
                throw new ArgumentNullException("compositeActivity");

            ArrayList nestedActivities = new ArrayList();
            Queue compositeActivities = new Queue();
            compositeActivities.Enqueue(compositeActivity);
            while (compositeActivities.Count > 0)
            {
                CompositeActivity compositeActivity2 = (CompositeActivity)compositeActivities.Dequeue();

                foreach (Activity activity in compositeActivity2.Activities)
                {
                    nestedActivities.Add(activity);
                    if (activity is CompositeActivity)
                        compositeActivities.Enqueue(activity);
                }

                foreach (Activity activity in compositeActivity2.EnabledActivities)
                {
                    if (!nestedActivities.Contains(activity))
                    {
                        nestedActivities.Add(activity);
                        if (activity is CompositeActivity)
                            compositeActivities.Enqueue(activity);
                    }
                }
            }
            return (Activity[])nestedActivities.ToArray(typeof(Activity));
        }
    }
    #endregion

    #region WorkflowCompilerOptionsService
    internal class UseCaseCompilerOptionsService : IWorkflowCompilerOptionsService
    {
        public UseCaseCompilerOptionsService()
        {
        }

        string IWorkflowCompilerOptionsService.RootNamespace
        {
            get
            {
                return String.Empty;
            }
        }

        string IWorkflowCompilerOptionsService.Language
        {
            get
            {
                return "CSharp";
            }
        }

        #region IWorkflowCompilerOptionsService Members

        public bool CheckTypes
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion
    }
    #endregion

    #region Class EventBindingService
    internal class EventBindingService : IEventBindingService
    {
        public EventBindingService()
        {
        }

        public string CreateUniqueMethodName(IComponent component, EventDescriptor e)
        {
            return e.DisplayName;
        }

        public ICollection GetCompatibleMethods(EventDescriptor e)
        {
            return new ArrayList();
        }

        public EventDescriptor GetEvent(PropertyDescriptor property)
        {
            return (property is EventPropertyDescriptor) ? ((EventPropertyDescriptor)property).EventDescriptor : null;
        }

        public PropertyDescriptorCollection GetEventProperties(EventDescriptorCollection events)
        {
            return new PropertyDescriptorCollection(new PropertyDescriptor[] { }, true);
        }

        public PropertyDescriptor GetEventProperty(EventDescriptor e)
        {
            return new EventPropertyDescriptor(e);
        }

        public bool ShowCode()
        {
            return false;
        }

        public bool ShowCode(int lineNumber)
        {
            return false;
        }

        public bool ShowCode(IComponent component, EventDescriptor e)
        {
            return false;
        }

        private class EventPropertyDescriptor : PropertyDescriptor
        {
            private EventDescriptor eventDescriptor_;

            public EventDescriptor EventDescriptor
            {
                get
                {
                    return eventDescriptor_;
                }
            }

            public EventPropertyDescriptor(EventDescriptor eventDescriptor)
                : base(eventDescriptor, null)
            {
                eventDescriptor_ = eventDescriptor;
            }

            public override Type ComponentType
            {
                get
                {
                    return eventDescriptor_.ComponentType;
                }
            }
            public override Type PropertyType
            {
                get
                {
                    return eventDescriptor_.EventType;
                }
            }

            public override bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            public override bool CanResetValue(object component)
            {
                return false;
            }

            public override object GetValue(object component)
            {
                return null;
            }

            public override void ResetValue(object component)
            {
            }

            public override void SetValue(object component, object value)
            {
            }

            public override bool ShouldSerializeValue(object component)
            {
                return false;
            }
        }
    }
    #endregion

    #region Class MenuCommandService
    internal sealed class UseCaseMenuCommandService : MenuCommandService
    {
        public UseCaseMenuCommandService(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public override void ShowContextMenu(CommandID menuID, int x, int y)
        {
            if (menuID == WorkflowMenuCommands.SelectionMenu)
            {
                ContextMenu contextMenu = new ContextMenu();

                foreach (DesignerVerb verb in Verbs)
                {
                    MenuItem menuItem = new MenuItem(verb.Text, new EventHandler(OnMenuClicked));
                    menuItem.Tag = verb;
                    contextMenu.MenuItems.Add(menuItem);
                }

                MenuItem[] items = GetSelectionMenuItems();
                if (items.Length > 0)
                {
                    contextMenu.MenuItems.Add(new MenuItem("-"));
                    foreach (MenuItem item in items)
                        contextMenu.MenuItems.Add(item);
                }

                WorkflowView workflowView = GetService(typeof(WorkflowView)) as WorkflowView;
                if (workflowView != null)
                    contextMenu.Show(workflowView, workflowView.PointToClient(new Point(x, y)));
            }
        }

        private void OnMenuClicked(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null && menuItem.Tag is MenuCommand)
            {
                MenuCommand command = menuItem.Tag as MenuCommand;
                command.Invoke();
            }
        }

        private MenuItem[] GetSelectionMenuItems()
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            bool addMenuItems = true;
            ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
            if (selectionService != null)
            {
                foreach (object obj in selectionService.GetSelectedComponents())
                {
                    if (!(obj is Activity))
                    {
                        addMenuItems = false;
                        break;
                    }
                }
            }

            if (addMenuItems)
            {
                Dictionary<CommandID, string> selectionCommands = new Dictionary<CommandID, string>();
                selectionCommands.Add(WorkflowMenuCommands.Cut, "Cut");
                selectionCommands.Add(WorkflowMenuCommands.Copy, "Copy");
                selectionCommands.Add(WorkflowMenuCommands.Paste, "Paste");
                selectionCommands.Add(WorkflowMenuCommands.Delete, "Delete");

                foreach (CommandID id in selectionCommands.Keys)
                {
                    MenuCommand command = FindCommand(id);
                    if (command != null)
                    {
                        MenuItem menuItem = new MenuItem(selectionCommands[id], new EventHandler(OnMenuClicked));
                        menuItem.Tag = command;
                        menuItems.Add(menuItem);
                    }
                }
            }

            return menuItems.ToArray();
        }
    }
}
#endregion