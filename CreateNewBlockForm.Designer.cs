namespace GuiScratch
{
    partial class CreateNewBlockForm
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.blockInfoPanel = new System.Windows.Forms.Panel();
            this.infoPB = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.addCBOptionsPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.selectionsCB = new System.Windows.Forms.ComboBox();
            this.addTextPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.addInputPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.inputKindCB = new System.Windows.Forms.ComboBox();
            this.AddCodeButton = new System.Windows.Forms.Button();
            this.removeInfoBtnsPanel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.blockKindCB = new System.Windows.Forms.ComboBox();
            this.valueKindPanel = new System.Windows.Forms.Panel();
            this.valueKindCB = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.blockInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infoPB)).BeginInit();
            this.panel1.SuspendLayout();
            this.addCBOptionsPanel.SuspendLayout();
            this.addTextPanel.SuspendLayout();
            this.addInputPanel.SuspendLayout();
            this.valueKindPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(232, 298);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 27);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // addButton
            // 
            this.addButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addButton.Location = new System.Drawing.Point(434, 298);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 27);
            this.addButton.TabIndex = 10;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.addButton_MouseUp);
            // 
            // blockInfoPanel
            // 
            this.blockInfoPanel.AutoScroll = true;
            this.blockInfoPanel.Controls.Add(this.infoPB);
            this.blockInfoPanel.Controls.Add(this.removeInfoBtnsPanel);
            this.blockInfoPanel.Location = new System.Drawing.Point(31, 2);
            this.blockInfoPanel.Name = "blockInfoPanel";
            this.blockInfoPanel.Size = new System.Drawing.Size(456, 90);
            this.blockInfoPanel.TabIndex = 12;
            // 
            // infoPB
            // 
            this.infoPB.Location = new System.Drawing.Point(3, 32);
            this.infoPB.Name = "infoPB";
            this.infoPB.Size = new System.Drawing.Size(329, 50);
            this.infoPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.infoPB.TabIndex = 0;
            this.infoPB.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.addCBOptionsPanel);
            this.panel1.Controls.Add(this.addTextPanel);
            this.panel1.Controls.Add(this.addInputPanel);
            this.panel1.Location = new System.Drawing.Point(31, 148);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(456, 136);
            this.panel1.TabIndex = 13;
            // 
            // addCBOptionsPanel
            // 
            this.addCBOptionsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.addCBOptionsPanel.Controls.Add(this.label3);
            this.addCBOptionsPanel.Controls.Add(this.label5);
            this.addCBOptionsPanel.Controls.Add(this.selectionsCB);
            this.addCBOptionsPanel.Location = new System.Drawing.Point(3, 91);
            this.addCBOptionsPanel.Name = "addCBOptionsPanel";
            this.addCBOptionsPanel.Size = new System.Drawing.Size(210, 38);
            this.addCBOptionsPanel.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(142, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "selection";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Add";
            // 
            // selectionsCB
            // 
            this.selectionsCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectionsCB.FormattingEnabled = true;
            this.selectionsCB.Items.AddRange(new object[] {
            "controls",
            "vars",
            "lists"});
            this.selectionsCB.Location = new System.Drawing.Point(44, 6);
            this.selectionsCB.Name = "selectionsCB";
            this.selectionsCB.Size = new System.Drawing.Size(90, 24);
            this.selectionsCB.TabIndex = 1;
            // 
            // addTextPanel
            // 
            this.addTextPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.addTextPanel.Controls.Add(this.label4);
            this.addTextPanel.Location = new System.Drawing.Point(3, 3);
            this.addTextPanel.Name = "addTextPanel";
            this.addTextPanel.Size = new System.Drawing.Size(210, 38);
            this.addTextPanel.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Add text";
            // 
            // addInputPanel
            // 
            this.addInputPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.addInputPanel.Controls.Add(this.label2);
            this.addInputPanel.Controls.Add(this.label1);
            this.addInputPanel.Controls.Add(this.inputKindCB);
            this.addInputPanel.Location = new System.Drawing.Point(3, 47);
            this.addInputPanel.Name = "addInputPanel";
            this.addInputPanel.Size = new System.Drawing.Size(210, 38);
            this.addInputPanel.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(142, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "input";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Add";
            // 
            // inputKindCB
            // 
            this.inputKindCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputKindCB.FormattingEnabled = true;
            this.inputKindCB.Items.AddRange(new object[] {
            "number",
            "text",
            "boolean",
            "color"});
            this.inputKindCB.Location = new System.Drawing.Point(44, 6);
            this.inputKindCB.Name = "inputKindCB";
            this.inputKindCB.Size = new System.Drawing.Size(90, 24);
            this.inputKindCB.TabIndex = 1;
            // 
            // AddCodeButton
            // 
            this.AddCodeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddCodeButton.Location = new System.Drawing.Point(326, 298);
            this.AddCodeButton.Name = "AddCodeButton";
            this.AddCodeButton.Size = new System.Drawing.Size(90, 27);
            this.AddCodeButton.TabIndex = 14;
            this.AddCodeButton.Text = "Add code";
            this.AddCodeButton.UseVisualStyleBackColor = true;
            this.AddCodeButton.Click += new System.EventHandler(this.AddCodeButton_Click);
            // 
            // removeInfoBtnsPanel
            // 
            this.removeInfoBtnsPanel.AutoSize = true;
            this.removeInfoBtnsPanel.Location = new System.Drawing.Point(1, 0);
            this.removeInfoBtnsPanel.Name = "removeInfoBtnsPanel";
            this.removeInfoBtnsPanel.Size = new System.Drawing.Size(453, 25);
            this.removeInfoBtnsPanel.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(32, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 17);
            this.label6.TabIndex = 16;
            this.label6.Text = "Block kind: ";
            // 
            // blockKindCB
            // 
            this.blockKindCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blockKindCB.FormattingEnabled = true;
            this.blockKindCB.Location = new System.Drawing.Point(109, 106);
            this.blockKindCB.Name = "blockKindCB";
            this.blockKindCB.Size = new System.Drawing.Size(144, 26);
            this.blockKindCB.TabIndex = 17;
            // 
            // valueKindPanel
            // 
            this.valueKindPanel.Controls.Add(this.valueKindCB);
            this.valueKindPanel.Controls.Add(this.label7);
            this.valueKindPanel.Location = new System.Drawing.Point(254, 98);
            this.valueKindPanel.Name = "valueKindPanel";
            this.valueKindPanel.Size = new System.Drawing.Size(233, 44);
            this.valueKindPanel.TabIndex = 18;
            // 
            // valueKindCB
            // 
            this.valueKindCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueKindCB.FormattingEnabled = true;
            this.valueKindCB.Location = new System.Drawing.Point(82, 7);
            this.valueKindCB.Name = "valueKindCB";
            this.valueKindCB.Size = new System.Drawing.Size(147, 26);
            this.valueKindCB.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(5, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 17);
            this.label7.TabIndex = 18;
            this.label7.Text = "Value kind: ";
            // 
            // CreateNewBlockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 337);
            this.Controls.Add(this.valueKindPanel);
            this.Controls.Add(this.blockKindCB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.AddCodeButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.blockInfoPanel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateNewBlockForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create new block";
            this.Load += new System.EventHandler(this.CreateNewBlockForm_Load);
            this.blockInfoPanel.ResumeLayout(false);
            this.blockInfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infoPB)).EndInit();
            this.panel1.ResumeLayout(false);
            this.addCBOptionsPanel.ResumeLayout(false);
            this.addCBOptionsPanel.PerformLayout();
            this.addTextPanel.ResumeLayout(false);
            this.addTextPanel.PerformLayout();
            this.addInputPanel.ResumeLayout(false);
            this.addInputPanel.PerformLayout();
            this.valueKindPanel.ResumeLayout(false);
            this.valueKindPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Panel blockInfoPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox inputKindCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel addInputPanel;
        private System.Windows.Forms.Panel addTextPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button AddCodeButton;
        private System.Windows.Forms.PictureBox infoPB;
        private System.Windows.Forms.Panel removeInfoBtnsPanel;
        private System.Windows.Forms.Panel addCBOptionsPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox selectionsCB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox blockKindCB;
        private System.Windows.Forms.Panel valueKindPanel;
        private System.Windows.Forms.ComboBox valueKindCB;
        private System.Windows.Forms.Label label7;
    }
}