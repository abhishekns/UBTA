
namespace ubta.UseCase.Designer
{
    partial class UseCaseDesignerControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlDesigner = new System.Windows.Forms.Panel();
            this.myUseCaseViewSplitter = new System.Windows.Forms.SplitContainer();
            this.myDesignerAndXaml = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.myXomlView = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnApplyXaml = new System.Windows.Forms.ToolStripButton();
            this.btnAutoSynch = new System.Windows.Forms.ToolStripButton();
            this.btnGetXaml = new System.Windows.Forms.ToolStripButton();
            this.myPropertyGridSplitter = new System.Windows.Forms.SplitContainer();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.pnlDesigner.SuspendLayout();
            this.myUseCaseViewSplitter.Panel1.SuspendLayout();
            this.myUseCaseViewSplitter.Panel2.SuspendLayout();
            this.myUseCaseViewSplitter.SuspendLayout();
            this.myDesignerAndXaml.Panel2.SuspendLayout();
            this.myDesignerAndXaml.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.myPropertyGridSplitter.Panel2.SuspendLayout();
            this.myPropertyGridSplitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDesigner
            // 
            this.pnlDesigner.Controls.Add(this.myUseCaseViewSplitter);
            this.pnlDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDesigner.Location = new System.Drawing.Point(0, 0);
            this.pnlDesigner.Name = "pnlDesigner";
            this.pnlDesigner.Size = new System.Drawing.Size(800, 600);
            this.pnlDesigner.TabIndex = 1;
            this.pnlDesigner.TabStop = true;
            // 
            // myUseCaseViewSplitter
            // 
            this.myUseCaseViewSplitter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myUseCaseViewSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myUseCaseViewSplitter.Location = new System.Drawing.Point(0, 0);
            this.myUseCaseViewSplitter.Name = "myUseCaseViewSplitter";
            // 
            // myUseCaseViewSplitter.Panel1
            // 
            this.myUseCaseViewSplitter.Panel1.Controls.Add(this.myDesignerAndXaml);
            this.myUseCaseViewSplitter.Panel1MinSize = 300;
            // 
            // myUseCaseViewSplitter.Panel2
            // 
            this.myUseCaseViewSplitter.Panel2.Controls.Add(this.myPropertyGridSplitter);
            this.myUseCaseViewSplitter.Size = new System.Drawing.Size(800, 600);
            this.myUseCaseViewSplitter.Panel2MinSize = 200;
            this.myUseCaseViewSplitter.SplitterDistance = 300;
            this.myUseCaseViewSplitter.TabIndex = 0;
            this.myUseCaseViewSplitter.Text = "splitContainer1";
            // 
            // myDesignerAndXaml
            // 
            this.myDesignerAndXaml.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myDesignerAndXaml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myDesignerAndXaml.Location = new System.Drawing.Point(0, 0);
            this.myDesignerAndXaml.Name = "myDesignerAndXaml";
            this.myDesignerAndXaml.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.myDesignerAndXaml.Panel1MinSize = 150;
            // 
            // myDesignerAndXaml.Panel2
            // 
            this.myDesignerAndXaml.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.myDesignerAndXaml.Size = new System.Drawing.Size(300, 600);
            this.myDesignerAndXaml.Panel2MinSize = 200;
            this.myDesignerAndXaml.SplitterDistance = 368;
            this.myDesignerAndXaml.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.myXomlView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(298, 226);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // myXomlView
            // 
            this.myXomlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myXomlView.Location = new System.Drawing.Point(3, 23);
            this.myXomlView.Multiline = true;
            this.myXomlView.Name = "myXomlView";
            this.myXomlView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.myXomlView.Size = new System.Drawing.Size(292, 200);
            this.myXomlView.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnApplyXaml,
            this.btnAutoSynch,
            this.btnGetXaml});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(298, 20);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnApplyXaml
            // 
            this.btnApplyXaml.Image = global::ubta.UseCase.Designer.Properties.Resources.uparrow;
            this.btnApplyXaml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnApplyXaml.Name = "btnApplyXaml";
            this.btnApplyXaml.Size = new System.Drawing.Size(79, 17);
            this.btnApplyXaml.Text = "Apply Xaml";
            this.btnApplyXaml.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnApplyXaml.ToolTipText = "Loads the Workflow designer with the current Xaml code below";
            this.btnApplyXaml.Click += new System.EventHandler(this.btnApplyXaml_Click);
            // 
            // btnAutoSynch
            // 
            this.btnAutoSynch.Checked = true;
            this.btnAutoSynch.CheckOnClick = true;
            this.btnAutoSynch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnAutoSynch.Image = global::ubta.UseCase.Designer.Properties.Resources.autosynch;
            this.btnAutoSynch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAutoSynch.Name = "btnAutoSynch";
            this.btnAutoSynch.Size = new System.Drawing.Size(124, 17);
            this.btnAutoSynch.Text = "AutoSynch Designer";
            this.btnAutoSynch.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnAutoSynch.ToolTipText = "If this is active, any changes in the designer will automatically update the Xaml" +
                " code below";
            this.btnAutoSynch.CheckedChanged += new System.EventHandler(this.btnAutoSynch_CheckedChanged);
            // 
            // btnGetXaml
            // 
            this.btnGetXaml.Enabled = false;
            this.btnGetXaml.Image = global::ubta.UseCase.Designer.Properties.Resources.downArrow;
            this.btnGetXaml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGetXaml.Name = "btnGetXaml";
            this.btnGetXaml.Size = new System.Drawing.Size(69, 17);
            this.btnGetXaml.Text = "Get Xaml";
            this.btnGetXaml.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnGetXaml.ToolTipText = "Retrieves the Xaml code for the workflow currently in the designer";
            this.btnGetXaml.Click += new System.EventHandler(this.btnGetXaml_Click);
            // 
            // myPropertyGridSplitter
            // 
            this.myPropertyGridSplitter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myPropertyGridSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPropertyGridSplitter.Location = new System.Drawing.Point(0, 0);
            this.myPropertyGridSplitter.MinimumSize = new System.Drawing.Size(150, 150);
            this.myPropertyGridSplitter.Name = "myPropertyGridSplitter";
            this.myPropertyGridSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.myPropertyGridSplitter.Panel1MinSize = 100;
            // 
            // myPropertyGridSplitter.Panel2
            // 
            this.myPropertyGridSplitter.Panel2.Controls.Add(this.propertyGrid);
            this.myPropertyGridSplitter.Size = new System.Drawing.Size(496, 600);
            this.myPropertyGridSplitter.Panel2MinSize = 200;
            this.myPropertyGridSplitter.SplitterDistance = 250;
            this.myPropertyGridSplitter.TabIndex = 1;
            this.myPropertyGridSplitter.Text = "splitContainer2";
            // 
            // propertyGrid
            // 
            this.propertyGrid.CommandsVisibleIfAvailable = false;
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.propertyGrid.LineColor = System.Drawing.Color.LightSteelBlue;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(494, 316);
            this.propertyGrid.TabIndex = 1;
            this.propertyGrid.ToolbarVisible = false;
            // 
            // UseCaseDesignerControl
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnlDesigner);
            this.MinimumSize = new System.Drawing.Size(699, 399);
            this.Name = "UseCaseDesignerControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UseCaseDesignerControl_KeyUp);
            this.pnlDesigner.ResumeLayout(false);
            this.myUseCaseViewSplitter.Panel1.ResumeLayout(false);
            this.myUseCaseViewSplitter.Panel2.ResumeLayout(false);
            this.myUseCaseViewSplitter.ResumeLayout(false);
            this.myDesignerAndXaml.Panel2.ResumeLayout(false);
            this.myDesignerAndXaml.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.myPropertyGridSplitter.Panel2.ResumeLayout(false);
            this.myPropertyGridSplitter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDesigner;
        private System.Windows.Forms.SplitContainer myUseCaseViewSplitter;
        private System.Windows.Forms.SplitContainer myPropertyGridSplitter;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.SplitContainer myDesignerAndXaml;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox myXomlView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnApplyXaml;
        private System.Windows.Forms.ToolStripButton btnAutoSynch;
        private System.Windows.Forms.ToolStripButton btnGetXaml;
    }
}
