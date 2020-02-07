﻿namespace GuiScratch
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.blocksContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.douplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCommentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runButton = new System.Windows.Forms.Button();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.addBlocksPanel = new System.Windows.Forms.Panel();
            this.screenPanel = new System.Windows.Forms.Panel();
            this.screenPB = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.controlsKindsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.addControlButton = new System.Windows.Forms.Button();
            this.controlsFLP = new System.Windows.Forms.FlowLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.propertiesPanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.blocksContextMenu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.screenPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.screenPB)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.propertiesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // blocksContextMenu
            // 
            this.blocksContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.douplicateToolStripMenuItem,
            this.addCommentToolStripMenuItem});
            this.blocksContextMenu.Name = "blocksContextMenu";
            this.blocksContextMenu.Size = new System.Drawing.Size(152, 70);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // douplicateToolStripMenuItem
            // 
            this.douplicateToolStripMenuItem.Name = "douplicateToolStripMenuItem";
            this.douplicateToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.douplicateToolStripMenuItem.Text = "Duplicate";
            // 
            // addCommentToolStripMenuItem
            // 
            this.addCommentToolStripMenuItem.Name = "addCommentToolStripMenuItem";
            this.addCommentToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.addCommentToolStripMenuItem.Text = "Add comment";
            // 
            // runButton
            // 
            this.runButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.runButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.runButton.Image = global::GuiScratch.Properties.Resources.runIcon;
            this.runButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.runButton.Location = new System.Drawing.Point(901, 57);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(119, 44);
            this.runButton.TabIndex = 1;
            this.runButton.Text = "  Run";
            this.runButton.UseVisualStyleBackColor = false;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            this.runButton.Visible = false;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.runToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.runToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.runToolStripMenuItem.Text = "&Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1370, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1370, 600);
            this.tabControl1.TabIndex = 12;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.addBlocksPanel);
            this.tabPage1.Controls.Add(this.screenPanel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1362, 574);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Programing";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // addBlocksPanel
            // 
            this.addBlocksPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.addBlocksPanel.AutoScroll = true;
            this.addBlocksPanel.BackColor = System.Drawing.Color.LightGray;
            this.addBlocksPanel.Location = new System.Drawing.Point(3, 3);
            this.addBlocksPanel.Name = "addBlocksPanel";
            this.addBlocksPanel.Size = new System.Drawing.Size(282, 571);
            this.addBlocksPanel.TabIndex = 10;
            // 
            // screenPanel
            // 
            this.screenPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.screenPanel.AutoScroll = true;
            this.screenPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.screenPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.screenPanel.Controls.Add(this.screenPB);
            this.screenPanel.Controls.Add(this.runButton);
            this.screenPanel.Location = new System.Drawing.Point(279, 3);
            this.screenPanel.Name = "screenPanel";
            this.screenPanel.Size = new System.Drawing.Size(1080, 571);
            this.screenPanel.TabIndex = 9;
            this.screenPanel.SizeChanged += new System.EventHandler(this.screenPanel_SizeChanged);
            // 
            // screenPB
            // 
            this.screenPB.Location = new System.Drawing.Point(0, 0);
            this.screenPB.Name = "screenPB";
            this.screenPB.Size = new System.Drawing.Size(1080, 571);
            this.screenPB.TabIndex = 2;
            this.screenPB.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.propertiesPanel);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1362, 574);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Controls";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.controlsKindsPanel);
            this.panel2.Location = new System.Drawing.Point(241, 9);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(184, 586);
            this.panel2.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Kinds:";
            // 
            // controlsKindsPanel
            // 
            this.controlsKindsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.controlsKindsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.controlsKindsPanel.Location = new System.Drawing.Point(4, 28);
            this.controlsKindsPanel.Name = "controlsKindsPanel";
            this.controlsKindsPanel.Size = new System.Drawing.Size(178, 554);
            this.controlsKindsPanel.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.addControlButton);
            this.panel4.Controls.Add(this.controlsFLP);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Location = new System.Drawing.Point(2, 9);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(239, 586);
            this.panel4.TabIndex = 2;
            // 
            // addControlButton
            // 
            this.addControlButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addControlButton.Location = new System.Drawing.Point(181, 2);
            this.addControlButton.Name = "addControlButton";
            this.addControlButton.Size = new System.Drawing.Size(50, 23);
            this.addControlButton.TabIndex = 4;
            this.addControlButton.Text = "Add";
            this.addControlButton.UseVisualStyleBackColor = true;
            this.addControlButton.Click += new System.EventHandler(this.addControlButton_Click);
            // 
            // controlsFLP
            // 
            this.controlsFLP.AutoScroll = true;
            this.controlsFLP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.controlsFLP.Location = new System.Drawing.Point(4, 27);
            this.controlsFLP.Name = "controlsFLP";
            this.controlsFLP.Size = new System.Drawing.Size(230, 554);
            this.controlsFLP.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 17);
            this.label9.TabIndex = 2;
            this.label9.Text = "Controls:";
            // 
            // propertiesPanel
            // 
            this.propertiesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.propertiesPanel.Controls.Add(this.label5);
            this.propertiesPanel.Location = new System.Drawing.Point(426, 9);
            this.propertiesPanel.Name = "propertiesPanel";
            this.propertiesPanel.Size = new System.Drawing.Size(592, 586);
            this.propertiesPanel.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Properties:";
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1362, 574);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Images";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1370, 629);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.blocksContextMenu.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.screenPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.screenPB)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.propertiesPanel.ResumeLayout(false);
            this.propertiesPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip blocksContextMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem douplicateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCommentToolStripMenuItem;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel propertiesPanel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button addControlButton;
        private System.Windows.Forms.FlowLayoutPanel controlsFLP;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel controlsKindsPanel;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Panel addBlocksPanel;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel screenPanel;
        private System.Windows.Forms.PictureBox screenPB;
    }
}

