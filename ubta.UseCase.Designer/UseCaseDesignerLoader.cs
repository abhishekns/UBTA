/*[ Compilation unit ----------------------------------------------------------

   Component       : syngo - CentralStrategyManager

   Name            : Loader.cs

   Last Author     : A. Schuelke, SW D ArT

   Language        : C#

   Creation Date   : 19-May-2006

   Description     : Provides functionality for workflow designer

-----------------------------------------------------------------------------*/
/*] END */

namespace ubta.UseCase.Designer
{
    using System;
    using System.IO;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.ComponentModel.Design.Serialization;
    using System.Workflow.ComponentModel;
    using System.Workflow.ComponentModel.Compiler;
    using System.Workflow.ComponentModel.Design;
    using System.Workflow.ComponentModel.Serialization;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing.Design;
    using System.Windows.Forms;
    using System.Xml;
    using System.Text;
    using System.Windows.Forms.Design.Behavior;
    using System.Reflection;

    #region WorkflowLoader
    internal sealed class UseCaseDesignerLoader : WorkflowDesignerLoader
    {
        private string xoml = string.Empty;

        internal UseCaseDesignerLoader()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            IDesignerLoaderHost host = LoaderHost;
            if (host != null)
            {
                host.RemoveService(typeof(IIdentifierCreationService));
                host.AddService(typeof(IIdentifierCreationService), new IdentifierCreationService(host));
                host.AddService(typeof(IMenuCommandService), new UseCaseMenuCommandService(host));
                host.AddService(typeof(IToolboxService), new Toolbox(host));
                host.AddService(typeof(ITypeResolutionService), TypeResolutionService.Instance);
                TypeProvider typeProvider = new TypeProvider(host);
                typeProvider.AddAssemblyReference(typeof(string).Assembly.Location);
                host.AddService(typeof(ITypeProvider), typeProvider, true);
                host.AddService(typeof(IEventBindingService), new EventBindingService());
                //host.AddService(typeof(BehaviorService), new BehaviorService());
            }
        }

        public override void Dispose()
        {
            try
            {
                IDesignerLoaderHost host = LoaderHost;
                if (host != null)
                {
                    host.RemoveService(typeof(IIdentifierCreationService));
                    host.RemoveService(typeof(IMenuCommandService));
                    host.RemoveService(typeof(IToolboxService));
                    host.RemoveService(typeof(ITypeProvider), true);
                    host.RemoveService(typeof(IWorkflowCompilerOptionsService));
                    host.RemoveService(typeof(IEventBindingService));
                }
            }
            finally
            {
                base.Dispose();
            }
        }

        public override void ForceReload()
        {
        }

        public string DefaultNamespace
        {
            get
            {
                return "SampleNamespace";
            }
        }

        public string Xoml
        {
            get
            {
                return this.xoml;
            }

            set
            {
                this.xoml = value;
            }
        }

        private Type workflowType;

        public Type WorkflowType
        {
            get { return workflowType; }
            set { workflowType = value; }
        }

        private string xomlFile = string.Empty;

        public string XomlFile
        {
            get
            {
                return this.xomlFile;
            }
            set
            {
                this.xomlFile = value;
            }
        }

        public override string FileName
        {
            get { return string.Empty; }
        }

        public override TextReader GetFileReader(string filePath)
        {
            return new StreamReader(new FileStream(filePath, FileMode.OpenOrCreate));
        }

        public override TextWriter GetFileWriter(string filePath)
        {
            // clsoe the handle
            return new StreamWriter(new FileStream(filePath, FileMode.OpenOrCreate));
        }

        protected override void PerformLoad(IDesignerSerializationManager serializationManager)
        {
            IDesignerHost designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
            Activity rootActivity = null;
            //First see if we have a workflow type
            if (WorkflowType != null)
            {
                rootActivity = (Activity)Activator.CreateInstance(WorkflowType);
            }
            else
            {
                // Create a text reader out of the doc data, and ask
                TextReader reader = new StringReader(this.xoml);
                try
                {
                    using (XmlReader xmlReader = XmlReader.Create(reader))
                    {
                        WorkflowMarkupSerializer xomlSerializer = new WorkflowMarkupSerializer();
                        rootActivity = xomlSerializer.Deserialize(xmlReader) as Activity;
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            if (rootActivity != null && designerHost != null)
            {
                AddObjectGraphToDesignerHost(designerHost, rootActivity);
            }
        }

        protected override void PerformFlush(IDesignerSerializationManager manager)
        {
        }

        public string ValidateWorkflow()
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

            if (host != null && host.RootComponent != null)
            {
                Activity service = host.RootComponent as Activity;

                if (service != null)
                {
                }
            }
            throw new Exception("unable to perform workflow validation check");
        }

        public void PerformFlush()
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

            if (host != null && host.RootComponent != null)
            {
                Activity service = host.RootComponent as Activity;

                if (service != null)
                {
                    StringBuilder sb = new StringBuilder();
                    using (XmlWriter writer = XmlWriter.Create(sb))
                    {
                        WorkflowMarkupSerializer xomlSerializer = new WorkflowMarkupSerializer();
                        xomlSerializer.Serialize(writer, service);
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(sb.ToString());
                        XmlAttribute attrib = doc.CreateAttribute("x", "Class", "http://schemas.microsoft.com/winfx/2006/xaml");
                        attrib.Value = this.GetType().Namespace + ".CSMWF";
                        doc.DocumentElement.Attributes.Append(attrib);
                        doc.Save(new XmlTextWriter(this.xomlFile, Encoding.UTF8));
                    }
                }
            }
        }

        private static void AddObjectGraphToDesignerHost(IDesignerHost designerHost, Activity activity)
        {
            Guid Definitions_Class = new Guid("3FA84B23-B15B-4161-8EB8-37A54EFEEFC7");

            if (designerHost == null)
                throw new ArgumentNullException("designerHost");
            if (activity == null)
                throw new ArgumentNullException("activity");

            string rootSiteName = activity.QualifiedName;
            if (activity.Parent == null)
            {
                string fullClassName = activity.UserData[Definitions_Class] as string;
                if (fullClassName == null)
                    fullClassName = activity.GetType().FullName;
                rootSiteName = (fullClassName.LastIndexOf('.') != -1) ? fullClassName.Substring(fullClassName.LastIndexOf('.') + 1) : fullClassName;
                designerHost.Container.Add(activity, rootSiteName);
            }
            else
            {
                designerHost.Container.Add(activity, activity.QualifiedName);
            }

            if (activity is CompositeActivity)
            {
                foreach (Activity activity2 in GetNestedActivities(activity as CompositeActivity))
                    designerHost.Container.Add(activity2, activity2.QualifiedName);
            }
        }

        private static Activity[] GetNestedActivities(CompositeActivity compositeActivity)
        {
            if (compositeActivity == null)
                throw new ArgumentNullException("compositeActivity");

            IList<Activity> childActivities = null;
            ArrayList nestedActivities = new ArrayList();
            Queue compositeActivities = new Queue();
            compositeActivities.Enqueue(compositeActivity);
            while (compositeActivities.Count > 0)
            {
                CompositeActivity compositeActivity2 = (CompositeActivity)compositeActivities.Dequeue();
                childActivities = compositeActivity2.Activities;

                foreach (Activity activity in childActivities)
                {
                    nestedActivities.Add(activity);
                    if (activity is CompositeActivity)
                        compositeActivities.Enqueue(activity);
                }
            }
            return (Activity[])nestedActivities.ToArray(typeof(Activity));
        }

        internal static void DestroyObjectGraphFromDesignerHost(IDesignerHost designerHost, Activity activity)
        {
            if (designerHost == null)
                throw new ArgumentNullException("designerHost");
            if (activity == null)
                throw new ArgumentNullException("activity");

            designerHost.DestroyComponent(activity);

            if (activity is CompositeActivity)
            {
                foreach (Activity activity2 in GetNestedActivities(activity as CompositeActivity))
                    designerHost.DestroyComponent(activity2);
            }
        }
    }
    #endregion
}
