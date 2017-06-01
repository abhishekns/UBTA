using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using ubta.Reflection;

namespace ubta.UseCase.Designer
{
    public partial class MainForm : Form
    {
        private const string RegStorage = "HKEY_CURRENT_USER\\Software\\usecasetests\\ubta\\ubta.UseCase.UseCaseDesigner";
        private UseCaseProvider myUcp = new UseCaseProvider();
        
        public MainForm()
        {
            InitializeComponent();
            useCaseDesignerControl1.UseCaseLoaded += new EventHandler(handleUseCaseLoaded);
            useCaseDesignerControl1.UseCaseSaved += new EventHandler(handleUseCaseSaved);
            useCaseDesignerControl1.ActiveInstanceChanged += new ActiveInstanceChangedHandler(useCaseDesignerControl1_ActiveInstanceChanged);
            //bool fb = true;
            //foreach (var ns in myUcp.NameSpaces)
            //{
            //    ToolStripButton b = new ToolStripButton();
            //    if (ns.StartsWith("_"))
            //    {
            //        b.Text = ns.Substring(1);
            //    }
            //    else
            //    {
            //        b.Text = ns;
            //    }
            //    b.Click += new EventHandler(b_Click);
            //    if (fb)
            //    {
            //        fb = false;
            //        workflowDesignerControl1.UseCaseLoaded +=new EventHandler((object sender, EventArgs e) => b_Click(b, new EventArgs()));
            //    }
            //    myToolStrip.Items.Add(b);
            //}
        }

        void useCaseDesignerControl1_ActiveInstanceChanged(System.Workflow.ComponentModel.Activity sender, Instance oi, Instance ni)
        {
            var button = (from b in myToolStrip.Items.OfType<ToolStripButton>() where b.Text == ni.InstanceName select b).FirstOrDefault();
            if (null != button)
            {
                b_Click(button, new EventArgs());
            }
        }

        void b_Click(object sender, EventArgs e)
        {
            ToolStripButton b = sender as ToolStripButton;
            if (null != b)
            {
                Instance i = b.Tag as Instance;
                string ns = i.InstanceType.GetValidTypeNameForXml();
                var dsuc = useCaseDesignerControl1.CurrentUseCase as DefaultSequentialUseCase;
                dsuc.ActiveInstance = i;
                SetToolsForNameSpace(ns, b);
            }
        }

        private void SetToolsForNameSpace(string ns, ToolStripButton b)
        {
            if (!myUcp.NameSpaces.Contains(ns))
            {
                ns = "_" + ns;
                if (!myUcp.NameSpaces.Contains(ns))
                {
                    MessageBox.Show(string.Format("Namespace {0} or {1} not found.", b.Text, ns));
                }
            }
            useCaseDesignerControl1.SetToolItems(myUcp[ns]);

            Color def = new ToolStripButton().BackColor;
            if (null != b)
            {
                b.BackColor = Color.AliceBlue;
            }
            foreach (var cb in myToolStrip.Items.OfType<ToolStripButton>())
            {
                if (b != cb)
                {
                    cb.BackColor = def;
                }
            }
        }

        private void mnuNewSequential_Click(object sender, EventArgs e)
        {
            this.useCaseDesignerControl1.LoadNewSequentialWorkflow();
        }

        private void mnuNewStateMachine_Click(object sender, EventArgs e)
        {
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.useCaseDesignerControl1.LoadExistingUseCase();
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            this.useCaseDesignerControl1.Save();
        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            this.useCaseDesignerControl1.SaveAs();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.useCaseDesignerControl1.UseCaseLoaded -= new EventHandler(handleUseCaseLoaded);
            this.useCaseDesignerControl1.UseCaseSaved -= new EventHandler(handleUseCaseSaved);
            this.Close();
        }

        private void zoom25_Click(object sender, EventArgs e)
        {
            this.useCaseDesignerControl1.FitToPage();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            this.useCaseDesignerControl1.ProcessZoom(this.trackBar1.Value);
        }

        private void mnuValidate_Click(object sender, EventArgs e)
        {
            this.useCaseDesignerControl1.ValidateWorkflow(true);
        }

        private void handleUseCaseLoaded(object sender, EventArgs e)
        {
            updateName();
            ToolStripComboBox tscb = new ToolStripComboBox();
            tscb.Items.AddRange((from ns in myUcp.NameSpaces where ns.StartsWith("_") select ((object)(ns.Substring(1)))).ToArray());
            tscb.Items.AddRange((from ns in myUcp.NameSpaces where !ns.StartsWith("_") select ((object)(ns))).ToArray());

            myToolStrip.Items.Add(tscb);
            tscb.SelectedIndexChanged += new EventHandler(tscb_SelectedIndexChanged);
            bool fb = true;
            foreach (var i in useCaseDesignerControl1.GetInstances())
            {
                ToolStripButton b = new ToolStripButton();
                b.ToolTipText = "Instance of Type "+ i.InstanceType.GetValidTypeNameForXml();
                b.Text = i.InstanceName;
                b.Tag = i;
                b.Click +=new EventHandler(b_Click);
                if (fb)
                {
                    fb = false;
                    b_Click(b, new EventArgs());
                }
                myToolStrip.Items.Add(b);
            }
            useCaseDesignerControl1.NewInstanceAvailable += new NewInstanceAvailableHandler(useCaseDesignerControl1_NewInstanceAvailable);
            useCaseDesignerControl1.InstanceNameChanged += new InstanceNameChangedHandler(useCaseDesignerControl1_InstanceNameChanged);
        }

        void useCaseDesignerControl1_InstanceNameChanged(object sender, string oldName_in, string newName_in)
        {
            var r = (from x in myToolStrip.Items.OfType<ToolStripButton>() where x.Text == oldName_in select x);
            foreach (var i in r)
            {
                i.Text = newName_in;
            }
        }

        void useCaseDesignerControl1_NewInstanceAvailable(System.Workflow.ComponentModel.Activity sender, Instance i)
        {
            ToolStripButton b = new ToolStripButton();
            b.ToolTipText = "Instance of Type " + i.InstanceType.GetValidTypeNameForXml();
            b.Text = i.InstanceName;
            b.Tag = i;
            b.Click += new EventHandler(b_Click);
            b_Click(b, new EventArgs());
            myToolStrip.Items.Add(b);
        }

        void tscb_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tscb = sender as ToolStripComboBox;
            string ns = tscb.SelectedItem as string;
            SetToolsForNameSpace(ns, null);
        }
        private void handleUseCaseSaved(object sender, EventArgs e)
        {
            updateName();
        }

        // updateName
        private void updateName()
        {
            if (string.IsNullOrEmpty(this.useCaseDesignerControl1.XomlFile))
            {
                Text = "UseCase Editor";
            }
            else
            {
                Text = "UseCase Editor - " + this.useCaseDesignerControl1.XomlFile;
            }
        }

        // MainForm_Load
        private void MainForm_Load(object sender, EventArgs e)
        {
            string val = (string)Registry.GetValue(RegStorage, "Position", null);
            if (!string.IsNullOrEmpty(val))
            {
                string[] loc = val.Split(' ');
                if (loc.Length >= 4)
                {
                    Left = Convert.ToInt32(loc[0]);
                    Top = Convert.ToInt32(loc[1]);
                    Height = Convert.ToInt32(loc[2]);
                    Width = Convert.ToInt32(loc[3]);
                }
            }
            val = (string)Registry.GetValue(RegStorage, "DesignerControlLayout", null);
            if (!string.IsNullOrEmpty(val))
            {
                useCaseDesignerControl1.ControlsLayout = val;
            }
            val = (string)Registry.GetValue(RegStorage, "WindowState", null);
            if (!string.IsNullOrEmpty(val))
            {
                WindowState = (FormWindowState)Enum.Parse(typeof(FormWindowState), val);
            }
        }

        // MainForm_FormClosing
        void MainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                string val = string.Format("{0} {1} {2} {3}", Left, Top, Height, Width);
                Registry.SetValue(RegStorage, "Position", val);
            }
            Registry.SetValue(RegStorage, "WindowState", WindowState.ToString());
            Registry.SetValue(RegStorage, "DesignerControlLayout", useCaseDesignerControl1.ControlsLayout);
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            useCaseDesignerControl1.AdaptSizeTo(this.ClientSize);
            toolStripContainer1.Width = this.ClientSize.Width;
            myToolStrip.Width = this.ClientSize.Width;
        }
    }
}
