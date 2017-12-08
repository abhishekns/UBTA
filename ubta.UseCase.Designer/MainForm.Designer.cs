using System;
namespace ubta.UseCase.Designer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewSequential = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewStateMachine = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuValidate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.useCaseDesignerControl1 = new ubta.UseCase.Designer.UseCaseDesignerControl();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.myToolStrip = new System.Windows.Forms.ToolStrip();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(766, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNew,
            this.openToolStripMenuItem,
            this.mnuValidate,
            this.mnuSave,
            this.mnuSaveAs,
            this.mnuExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // menuNew
            // 
            this.menuNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewSequential,
            this.mnuNewStateMachine});
            this.menuNew.Name = "menuNew";
            this.menuNew.Size = new System.Drawing.Size(124, 22);
            this.menuNew.Text = "&New";
            // 
            // mnuNewSequential
            // 
            this.mnuNewSequential.Name = "mnuNewSequential";
            this.mnuNewSequential.Size = new System.Drawing.Size(203, 22);
            this.mnuNewSequential.Text = "Sequential Workflow";
            this.mnuNewSequential.Click += new System.EventHandler(this.mnuNewSequential_Click);
            // 
            // mnuNewStateMachine
            // 
            this.mnuNewStateMachine.Name = "mnuNewStateMachine";
            this.mnuNewStateMachine.Size = new System.Drawing.Size(203, 22);
            this.mnuNewStateMachine.Text = "State Machine Workflow";
            this.mnuNewStateMachine.Click += new System.EventHandler(this.mnuNewStateMachine_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // mnuValidate
            // 
            this.mnuValidate.Name = "mnuValidate";
            this.mnuValidate.Size = new System.Drawing.Size(124, 22);
            this.mnuValidate.Text = "&Validate...";
            this.mnuValidate.Click += new System.EventHandler(this.mnuValidate_Click);
            // 
            // mnuSave
            // 
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.Size = new System.Drawing.Size(124, 22);
            this.mnuSave.Text = "&Save";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // mnuSaveAs
            // 
            this.mnuSaveAs.Name = "mnuSaveAs";
            this.mnuSaveAs.Size = new System.Drawing.Size(124, 22);
            this.mnuSaveAs.Text = "&SaveAs...";
            this.mnuSaveAs.Click += new System.EventHandler(this.mnuSaveAs_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(124, 22);
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.BackColor = System.Drawing.SystemColors.Control;
            this.trackBar1.LargeChange = 50;
            this.trackBar1.Location = new System.Drawing.Point(118, 0);
            this.trackBar1.Maximum = 400;
            this.trackBar1.Minimum = 10;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(147, 24);
            this.trackBar1.TabIndex = 3;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.Value = 100;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(81, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Zoom";
            // 
            // useCaseDesignerControl1
            // 
            this.useCaseDesignerControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.useCaseDesignerControl1.ControlsLayout = "";
            this.useCaseDesignerControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.useCaseDesignerControl1.Location = new System.Drawing.Point(0, -2);
            this.useCaseDesignerControl1.Margin = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.useCaseDesignerControl1.MinimumSize = new System.Drawing.Size(699, 399);
            this.useCaseDesignerControl1.Name = "useCaseDesignerControl1";
            this.useCaseDesignerControl1.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.useCaseDesignerControl1.Size = new System.Drawing.Size(766, 518);
            this.useCaseDesignerControl1.TabIndex = 0;
            this.useCaseDesignerControl1.Xoml = "";
            this.useCaseDesignerControl1.XomlFile = "";
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.useCaseDesignerControl1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(766, 516);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 24);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(766, 541);
            this.toolStripContainer1.TabIndex = 6;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.myToolStrip);
            // 
            // myToolStrip
            // 
            this.myToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.myToolStrip.Location = new System.Drawing.Point(3, 0);
            this.myToolStrip.Name = "myToolStrip";
            this.myToolStrip.Size = new System.Drawing.Size(111, 25);
            this.myToolStrip.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 565);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(578, 410);
            this.Name = "MainForm";
            this.Text = "UseCase Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UseCaseDesignerControl useCaseDesignerControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuNew;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveAs;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuNewSequential;
        private System.Windows.Forms.ToolStripMenuItem mnuNewStateMachine;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem mnuValidate;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip myToolStrip;

    }
}

