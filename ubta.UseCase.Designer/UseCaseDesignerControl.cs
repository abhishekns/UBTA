using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.Activities;
using System.Workflow.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;
using System.CodeDom.Compiler;
using System.Workflow.Runtime;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Schema;
using ubta.Common;
using System.Windows.Forms.Design.Behavior;


namespace ubta.UseCase.Designer
{
    /// <summary>
    /// Control to visualize a MS workflow foundation workflow
    /// </summary>
    /// 
    public partial class UseCaseDesignerControl : UserControl, IDisposable, IServiceProvider
    {
        private WorkflowView myUseCaseView;
        private DesignSurface myDesignSurface;
        private UseCaseDesignerLoader myUCDLoader;
        private WorkflowCompilerResults myCompilerResults;
        private WorkflowRuntime myUseCaseRuntime;
        private DefaultSequentialUseCase myCurrentUseCase;
        private Toolbox myToolBox;
        private const string AdditionalAssembies = "";

        /// <summary>
        /// Event raised if a new usecase is loaded
        /// </summary>
        public event EventHandler UseCaseLoaded;
        /// <summary>
        /// Event raised if a new usecase is saved
        /// </summary>
        public event EventHandler UseCaseSaved;

        /// <summary>
        /// Constructs a new designer control instance
        /// </summary>
        public UseCaseDesignerControl()
        {
            InitializeComponent();

            Toolbox toolbox = new Toolbox(this);
            myToolBox = toolbox;
            this.myPropertyGridSplitter.Panel1.Controls.Add(toolbox);
            toolbox.Dock = DockStyle.Fill;
            toolbox.BackColor = BackColor;
            toolbox.Font = WorkflowTheme.CurrentTheme.AmbientTheme.Font;
            this.Resize += new System.EventHandler(workflowView_Resize);

            WorkflowTheme.CurrentTheme.ReadOnly = false;
            WorkflowTheme.CurrentTheme.AmbientTheme.ShowConfigErrors = false;
            WorkflowTheme.CurrentTheme.ReadOnly = true;

            this.propertyGrid.BackColor = BackColor;
            this.propertyGrid.Font = WorkflowTheme.CurrentTheme.AmbientTheme.Font;
        }

        /// <summary>
        /// Gets or sets the file for the usecase markup language
        /// </summary>
        public string XomlFile
        {
            get
            {
                if (this.myUCDLoader == null) return string.Empty;
                return this.myUCDLoader.XomlFile;
            }
            set
            {
                if ((this.myUCDLoader == null) && (string.IsNullOrEmpty(value))) return;
                this.myUCDLoader.XomlFile = value;
            }
        }

        /// <summary>
        /// Gets or sets the usecase markup language
        /// </summary>
        public string Xoml
        {
            get
            {
                string xoml = string.Empty;
                if (this.myUCDLoader != null)
                {
                    try
                    {
                        this.myUCDLoader.Flush();
                        xoml = this.myUCDLoader.Xoml;
                    }
                    catch (Exception)
                    {
                    }
                }
                return xoml;
            }

            set
            {
                try
                {
                    if (!String.IsNullOrEmpty(value))
                        LoadWorkflow(value);
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// Gets the current usecase name
        /// </summary>
        public string WorkflowName
        {
            get
            {
                return this.myCurrentUseCase == null ? string.Empty : this.myCurrentUseCase.Name;
            }
        }

        /// <summary>
        /// Gets or sets the layout of the controls
        /// </summary>
        public string ControlsLayout
        {
            get
            {
                if (null != myUseCaseView)
                {
                    return string.Format("{0} {1} {2} {3}", myDesignerAndXaml.SplitterDistance,
                                         myPropertyGridSplitter.SplitterDistance, myUseCaseViewSplitter.SplitterDistance, myUseCaseView.Zoom);
                }
                return string.Empty;
            }

            set
            {
                string[] loc = value.Split(' ');
                if (loc.Length >= 4)
                {
                    myDesignerAndXaml.SplitterDistance = Convert.ToInt32(loc[0]);
                    myPropertyGridSplitter.SplitterDistance = Convert.ToInt32(loc[1]);
                    myUseCaseViewSplitter.SplitterDistance = Convert.ToInt32(loc[2]);
                    ProcessZoom(Convert.ToInt32(loc[3]));
                }
            }
        }

        public Activity CurrentUseCase
        {
            get
            {
                return myCurrentUseCase;
            }
        }

        /// <summary>
        /// Disposes the object
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                UnloadWorkflow();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Searches for the service with the specified type
        /// </summary>
        new public object GetService(Type serviceType)
        {
            return (this.myUseCaseView != null) ? ((IServiceProvider)this.myUseCaseView).GetService(serviceType) : null;
        }

        /// <summary>
        /// Loads a new sequential usecase
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadNewSequentialWorkflow();
        }

        /// <summary>
        /// Loads a usecase onto the design surface using the specified usecase type
        /// </summary>
        /// <param name="workflowType">Type to load</param>
        /// <returns>ICollection containing loading errors or null if none </returns>
        private ICollection LoadWorkflow(Type workflowType)
        {
            UseCaseDesignerLoader ld = new UseCaseDesignerLoader();
            ld.WorkflowType = workflowType;
            return LoadWorkflow(ld);
        }

        /// <summary>
        /// Loads a usecase onto the design surface using the specified Xaml
        /// </summary>
        /// <param name="xoml">Xaml to load</param>
        /// <returns>ICollection containing loading errors or null if none </returns>
        private ICollection LoadWorkflow(string xoml)
        {
            UseCaseDesignerLoader loader = new UseCaseDesignerLoader();
            loader.Xoml = xoml;
            return LoadWorkflow(loader);
        }

        private ICollection LoadWorkflow(UseCaseDesignerLoader loader)
        {
            SuspendLayout();

            DesignSurface designSurface = new DesignSurface();
            designSurface.BeginLoad(loader);
            if (designSurface.LoadErrors.Count > 0)
                return designSurface.LoadErrors;

            IDesignerHost designerHost = designSurface.GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (designerHost != null && designerHost.RootComponent != null)
            {
                var dsuc = designerHost.RootComponent as DefaultSequentialUseCase;
                myCurrentUseCase = dsuc;
                IRootDesigner rootDesigner = designerHost.GetDesigner(designerHost.RootComponent) as IRootDesigner;
                if (rootDesigner != null)
                {
                    UnloadWorkflow();

                    this.myDesignSurface = designSurface;
                    this.myUCDLoader = loader;
                    this.myUseCaseView = rootDesigner.GetView(ViewTechnology.Default) as WorkflowView;
                    this.myDesignerAndXaml.Panel1.Controls.Add(this.myUseCaseView);
                    this.myUseCaseView.Dock = DockStyle.Fill;
                    this.myUseCaseView.TabIndex = 1;
                    this.myUseCaseView.TabStop = true;
                    this.myUseCaseView.HScrollBar.TabStop = false;
                    this.myUseCaseView.VScrollBar.TabStop = false;
                    this.myUseCaseView.Focus();
                    dsuc.NewInstanceAvailable +=new NewInstanceAvailableHandler(dsuc_NewInstanceAvailable);
                    dsuc.InstanceNameChanged += new InstanceNameChangedHandler(dsuc_InstanceNameChanged);
                    dsuc.ActiveInstanceChanged += new ActiveInstanceChangedHandler(dsuc_ActiveInstanceChanged);
                    dsuc.NewUseCaseName();
                    ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                    IComponentChangeService changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                    if (selectionService != null)
                    {
                        selectionService.SelectionChanged += new EventHandler(OnSelectionChanged);
                    }

                    if (changeService != null)
                    {
                        changeService.ComponentAdded += new ComponentEventHandler(changeService_ComponentAdded);
                        changeService.ComponentChanged += new ComponentChangedEventHandler(changeService_ComponentChanged);
                        changeService.ComponentRemoved += new ComponentEventHandler(changeService_ComponentRemoved);
                        changeService.ComponentRename += new ComponentRenameEventHandler(changeService_ComponentRename);
                    }
                }
            }

            ResumeLayout(true);

            if (btnAutoSynch.Checked == true)
            {
                this.myXomlView.Text = GetCurrentXoml();
            }

            return null;
        }

        public event ActiveInstanceChangedHandler ActiveInstanceChanged;

        void dsuc_ActiveInstanceChanged(Activity sender, Instance oi, Instance ni)
        {
            if (null != ActiveInstanceChanged)
            {
                ActiveInstanceChanged(sender, oi, ni);
            }
        }

        public event InstanceNameChangedHandler InstanceNameChanged;

        void dsuc_InstanceNameChanged(object sender, string oldName_in, string newName_in)
        {
            if (null != InstanceNameChanged)
            {
                InstanceNameChanged(this, oldName_in, newName_in);
            }
        }

        void changeService_ComponentRename(object sender, ComponentRenameEventArgs e)
        {
            if (btnAutoSynch.Checked == true)
            {
                this.myXomlView.Text = GetCurrentXoml();
            }
        }

        void changeService_ComponentRemoved(object sender, ComponentEventArgs e)
        {
            if (btnAutoSynch.Checked == true)
            {
                this.myXomlView.Text = GetCurrentXoml();
            }
        }

        void changeService_ComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            if (btnAutoSynch.Checked == true)
            {
                this.myXomlView.Text = GetCurrentXoml();
            }
        }

        void changeService_ComponentAdded(object sender, ComponentEventArgs e)
        {
            if (btnAutoSynch.Checked == true)
            {
                this.myXomlView.Text = GetCurrentXoml();
            }
        }

        private void UnloadWorkflow()
        {
            IDesignerHost designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (designerHost != null && designerHost.Container.Components.Count > 0)
                UseCaseDesignerLoader.DestroyObjectGraphFromDesignerHost(designerHost, designerHost.RootComponent as Activity);

            if (this.myDesignSurface != null)
            {
                this.myDesignSurface.Dispose();
                this.myDesignSurface = null;
            }

            if (this.myUseCaseView != null)
            {
                Controls.Remove(this.myUseCaseView);
                this.myUseCaseView.Dispose();
                this.myUseCaseView = null;
            }
        }

        /// <summary>
        /// Loads a new sequential usecase
        /// </summary>
        public void LoadNewSequentialWorkflow()
        {
            myCurrentUseCase = LoadDSUseCase();
            myCurrentUseCase.Name = "UseCase";

            this.LoadUseCase();
            this.myXomlView.Text = GetCurrentXoml();
        }

        private DefaultSequentialUseCase LoadDSUseCase()
        {
            string[] primarySchemaLocations = { string.Format(@"{0}\ubta.Assert.xsd", Constants.DEFAULT_SCHEMA_DIR) };
            string useCaseLocation = string.Format(@"{0}\ExecuteTest.SampleLib.xml", Constants.CONFIG_DIR);
            string schemaLocation = string.Format(@"{0}\SampleLib.xsd", Constants.DEFAULT_SCHEMA_DIR);
            UseCaseMarkupSerializer ucms = new UseCaseMarkupSerializer();
            XmlSchema schema;
            using (FileStream sfs = new FileStream(schemaLocation, FileMode.Open))
            {
                schema = XmlSchema.Read(sfs, new ValidationEventHandler(HandleValEvent));
            }
            XmlSchemaSet xss = new XmlSchemaSet();
            xss.XmlResolver = new DefaultXmlResolver();
            xss.Add(schema);
            for (int i = 0; i < primarySchemaLocations.Length; i++)
            {
                using (FileStream fs = new FileStream(primarySchemaLocations[i], FileMode.Open))
                {
                    xss.Add(XmlSchema.Read(fs, new ValidationEventHandler(HandleValEvent)));
                }
            }
            xss.Compile();

            XmlDocument d = new XmlDocument();
            d.Load(useCaseLocation);
            d.Schemas = xss;
            d.Validate(new ValidationEventHandler(HandleValEvent));
            var dsuc = ucms.Deserialize(d) as DefaultSequentialUseCase;
            return dsuc;
        }

        public event NewInstanceAvailableHandler NewInstanceAvailable;

        void dsuc_NewInstanceAvailable(Activity sender, Instance i)
        {
            if (null != NewInstanceAvailable)
            {
                NewInstanceAvailable(sender, i);
            }
        }

        private void HandleValEvent(object source, ValidationEventArgs vea)
        {
        }

        private void LoadUseCase()
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
                {
                    WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
                    serializer.Serialize(xmlWriter, myCurrentUseCase);
                    this.Xoml = stringWriter.ToString();
                    if (UseCaseLoaded != null) UseCaseLoaded(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Zooms the view to fit the entire page
        /// </summary>
        public void FitToPage()
        {
            this.myUseCaseView.FitToScreenSize();
            this.myUseCaseView.Update();
        }

        /// <summary>
        /// Zooms the view to the specified zoom factor
        /// </summary>
        public void ProcessZoom(int zoomFactor)
        {
            if (null != myUseCaseView)
            {
                this.myUseCaseView.Zoom = zoomFactor;
                this.myUseCaseView.Update();
            }
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;

            if (selectionService != null)
            {
                this.propertyGrid.SelectedObjects = new ArrayList(selectionService.GetSelectedComponents()).ToArray();
                if (this.propertyGrid.SelectedObjects.Length > 0)
                {
                    IUseCase newVariable = propertyGrid.SelectedObjects[0] as IUseCase;
                    var x = (from a in myCurrentUseCase.UserObjectInstances where a.InstanceName == newVariable.InstanceName select a).FirstOrDefault();
                    if (null != x)
                    {
                        myCurrentUseCase.ActiveInstance = x;
                    }
                }
            }
        }

        /// <summary>
        /// Deletes the selected item
        /// </summary>
        public void DeleteSelected()
        {
            ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));

            if (selectionService != null)
            {
                if (selectionService.PrimarySelection is Activity)
                {
                    Activity activity = (Activity)selectionService.PrimarySelection;

                    if (activity.Name != this.WorkflowName)
                    {
                        activity.Parent.Activities.Remove(activity);
                        this.myUseCaseView.Update();
                    }
                }
            }
        }

        /// <summary>
        /// Shows a dialog and loads the selected usecase
        /// </summary>
        public void LoadExistingUseCase()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "xml files (*.xml)|*.xml|Assemblies (*.exe;*.dll)|*.exe;*.dll|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialog.FileName) == ".exe" || Path.GetExtension(openFileDialog.FileName) == ".dll")
                {
                    LoadAssemblyUseCases(openFileDialog.FileName);
                    return;
                }
                bool exo = false;
                using (XmlReader xmlReader = XmlReader.Create(openFileDialog.FileName))
                {
                    UseCaseMarkupSerializer serializer = new UseCaseMarkupSerializer();
                    XmlDocument doc = new XmlDocument();
                    doc.Load(xmlReader);
                    try
                    {
                        this.myCurrentUseCase = serializer.Deserialize(doc) as DefaultSequentialUseCase;
                    }
                    catch
                    {
                        exo = true;
                    }
                }
                if (exo)
                {
                    using (var xr = XmlReader.Create(openFileDialog.FileName))
                    {
                        myCurrentUseCase = new WorkflowMarkupSerializer().Deserialize(xr) as DefaultSequentialUseCase;
                    }
                }
                this.LoadUseCase();

                this.XomlFile = openFileDialog.FileName;
                this.Text = "UseCaseEditor -- [" + openFileDialog.FileName + "]";
                if (UseCaseLoaded != null) UseCaseLoaded(this, new EventArgs());
            }
        }

        private string GetUseCaseXoml(Type useCaseType)
        {
            WorkflowMarkupSerializer ser = new WorkflowMarkupSerializer();
            StringWriter sw = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(sw);

            try
            {
                Activity root = (Activity)Activator.CreateInstance(useCaseType);
                ser.Serialize(xmlWriter, root);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                myUseCaseRuntime.StopRuntime();
                sw.Close();
            }

            return sw.ToString();
        }

        private void LoadAssemblyUseCases(string assemblyPath)
        {
            Assembly asm = Assembly.LoadFile(assemblyPath);
            if (asm == null)
            {
                MessageBox.Show("Cannot load assembly", "UseCaseEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<Type> workflowTypes = new List<Type>();
            foreach (Type t in asm.GetTypes())
            {
                if (t.IsSubclassOf(typeof(SequentialWorkflowActivity)) || t.IsSubclassOf(typeof(StateMachineWorkflowActivity)))
                {
                    workflowTypes.Add(t);
                }
            }

            if (workflowTypes.Count == 0)
            {
                MessageBox.Show("Could not find any workflows in this assembly", "UseCaseEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Type selectedType = workflowTypes[0];

            if (workflowTypes.Count > 1)
            {
                // let user choose which usecase to open
                ChooseUseCase chooseForm = new ChooseUseCase();
                chooseForm.Types = workflowTypes;
                chooseForm.ShowDialog();
                selectedType = chooseForm.SelectedType;
            }

            ICollection errors = LoadWorkflow(selectedType);

            if (errors != null)
            {
                StringBuilder errorList = new StringBuilder();
                errorList.Append("Could not load usecase:\n\n");
                foreach (Exception error in errors)
                {
                    errorList.Append(error.Message);
                }

                MessageBox.Show(errorList.ToString(), "UseCaseEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Saves the current usecase to file
        /// </summary>
        public void SaveFile()
        {
            if (this.XomlFile.Length != 0)
            {
                this.SaveExistingWorkflow(this.XomlFile);
            }
            else
            {
                SaveAs();
            }
        }

        /// <summary>
        /// Shows a dialog for file selection and saves the current usecase to file
        /// </summary>
        public void SaveAs()
        {
            //if (ValidateAndAsk() != DialogResult.Yes) return;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "xoml files (*.xoml)|*.xoml|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.SaveExistingWorkflow(saveFileDialog.FileName);
                this.Text = "Designer Hosting Sample -- [" + saveFileDialog.FileName + "]";
            }
        }

        /// <summary>
        /// Saves the current usecase to file
        /// </summary>
        internal void SaveExistingWorkflow(string filePath)
        {
            if (this.myDesignSurface != null && this.myUCDLoader != null)
            {
                this.XomlFile = filePath;
                this.myUCDLoader.PerformFlush();
                if (UseCaseSaved != null) UseCaseSaved(this, new EventArgs());
            }
        }

        /// <summary>
        /// Return the current usecase in xoml format
        /// </summary>
        public string GetCurrentXoml()
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

            if (host != null && host.RootComponent != null)
            {
                Activity service = host.RootComponent as Activity;

                if (service != null)
                {
                    using (StringWriter sw = new StringWriter())
                    {
                        using (XmlWriter writer = XmlWriter.Create(sw))
                        {
                            WorkflowMarkupSerializer xomlSerializer = new WorkflowMarkupSerializer();
                            xomlSerializer.Serialize(writer, service);
                        }

                        return sw.ToString();
                    }
                }
            }

            return "";
        }

        /// <summary>
        /// Validates the current usecase
        /// </summary>
        public string ValidateWorkflow(bool showResults)
        {
            if (this.myDesignSurface != null && this.myUCDLoader != null)
            {
                string valres = this.myUCDLoader.ValidateWorkflow();
                if (showResults)
                {
                    //ValidationResultDialog dlg = new ValidationResultDialog();
                    //dlg.setResults(valres);
                    //dlg.setNoDecision();
                    //dlg.ShowDialog(this);
                }
                return valres;
            }
            throw new Exception("unable to perform validation check because no loader available");
        }

        /// <summary>
        /// Saves the current usecase to file
        /// </summary>
        public bool Save()
        {
            //if (ValidateAndAsk() != DialogResult.Yes) return false;
            return this.Save(true);
        }

        //private DialogResult ValidateAndAsk()
        //{
        //    string valres = ValidateWorkflow(false);
        //    ValidationResultDialog dlg = new ValidationResultDialog();
        //    dlg.setResults(valres);
        //    return dlg.ShowDialog(this);
        //}

        /// <summary>
        /// Saves the current usecase to file
        /// </summary>
        public bool Save(bool showMessage)
        {
            Cursor cursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            bool saveOK = true;

            try
            {
                // Save the usecase first, and capture the filePath of the usecase
                this.SaveFile();

                XmlDocument doc = new XmlDocument();
                doc.Load(this.XomlFile);
                XmlAttribute attrib = doc.CreateAttribute("x", "Class", "http://schemas.microsoft.com/winfx/2006/xaml");
                attrib.Value = string.Format("{0}.{1}", this.GetType().Namespace, this.WorkflowName);
                doc.DocumentElement.Attributes.Append(attrib);
                doc.Save(this.XomlFile);

                if (showMessage)
                {
                    MessageBox.Show(this, "usecase generated successfully. Generated xoml file:\n" + Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), this.XomlFile), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                saveOK = false;
            }
            finally
            {
                this.Cursor = cursor;
            }

            return saveOK;
        }

        /// <summary>
        /// Compiles the current usecase
        /// </summary>
        public bool Compile()
        {
            return this.Compile(true);
        }

        /// <summary>
        /// Compiles the current usecase
        /// </summary>
        public bool Compile(bool showMessage)
        {
            if (string.IsNullOrEmpty(this.XomlFile))
            {
                this.Save(false);
            }

            if (!File.Exists(this.XomlFile))
            {
                MessageBox.Show(this, "Cannot locate xoml file: " + Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), XomlFile), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            bool compileOK = true;

            Cursor cursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // Compile the usecase
                String[] assemblyNames = { AdditionalAssembies };
                WorkflowCompiler compiler = new WorkflowCompiler();
                WorkflowCompilerParameters parameters = new WorkflowCompilerParameters(assemblyNames);
                parameters.OutputAssembly = string.Format("{0}.dll", this.WorkflowName);
                myCompilerResults = compiler.Compile(parameters, this.XomlFile);

                StringBuilder errors = new StringBuilder();
                foreach (CompilerError compilerError in myCompilerResults.Errors)
                {
                    errors.Append(compilerError.ToString() + '\n');
                }

                if (errors.Length != 0)
                {
                    MessageBox.Show(this, errors.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    compileOK = false;
                }
                else if (showMessage)
                {
                    MessageBox.Show(this, "usecase compiled successfully. Compiled assembly:\n" + myCompilerResults.CompiledAssembly.GetName(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finally
            {
                this.Cursor = cursor;
            }

            return compileOK;
        }

        /// <summary>
        /// Runs the current usecase
        /// </summary>
        public bool Run()
        {
            if (this.myCompilerResults == null)
            {
                if (!this.Compile(false))
                {
                    return false;
                }
            }

            // Start the runtime engine
            if (this.myUseCaseRuntime == null)
            {
                this.myUseCaseRuntime = new WorkflowRuntime();
            }

            myUseCaseRuntime.StartRuntime();
            myUseCaseRuntime.WorkflowCompleted += new EventHandler<WorkflowCompletedEventArgs>(workflowRuntime_WorkflowCompleted);
            myUseCaseRuntime.CreateWorkflow((myCompilerResults.CompiledAssembly.GetType(string.Format("{0}.{1}", this.GetType().Namespace, this.WorkflowName)))).Start();

            return true;
        }

        void workflowRuntime_WorkflowCompleted(object sender, WorkflowCompletedEventArgs e)
        {
            this.myUseCaseRuntime.StopRuntime();
            this.myUseCaseRuntime.Dispose();
            this.myUseCaseRuntime = null;
        }

        private void btnAutoSynch_CheckedChanged(object sender, EventArgs e)
        {
            if (btnAutoSynch.Checked == true)
            {
                btnGetXaml.Enabled = false;
            }
            else
            {
                btnGetXaml.Enabled = true;
            }
        }

        private void btnGetXaml_Click(object sender, EventArgs e)
        {
            this.myXomlView.Text = GetCurrentXoml();
        }

        private void btnApplyXaml_Click(object sender, EventArgs e)
        {
            ICollection errors = LoadWorkflow(this.myXomlView.Text);
            if (errors != null)
            {
                StringBuilder errorList = new StringBuilder();
                errorList.Append("Could not load usecase:\n\n");
                foreach (Exception error in errors)
                {
                    errorList.Append(error.Message);
                }

                MessageBox.Show(errorList.ToString(), "UseCaseEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.myXomlView.Text = GetCurrentXoml();
            }
        }

        private void workflowView_Resize(object sender, System.EventArgs e)
        {
            pnlDesigner.Width = this.Width;
            pnlDesigner.Height = this.Height;
        }

        private void UseCaseDesignerControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.O && e.Modifiers == Keys.Control)
            {

            }
        }


        internal void SetToolItems(List<Type> list)
        {
            myToolBox.Clear();
            myToolBox.AddToolboxEntries(list);
        }

        internal void AdaptSizeTo(Size size)
        {
            this.Size = new Size(size.Width, size.Height - 50);
        }

        internal Instance[] GetInstances()
        {
            DefaultSequentialUseCase duc = myCurrentUseCase as DefaultSequentialUseCase;
            if (null != duc)
            {
                return duc.UserObjectInstances;
            }
            return null;
        }
    }
}
