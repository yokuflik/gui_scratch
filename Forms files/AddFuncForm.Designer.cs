namespace GuiScratch
{
    partial class AddFuncForm
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
            this.removeInfoBtnsPanel = new System.Windows.Forms.Panel();
            this.blockInfoPanel = new System.Windows.Forms.Panel();
            this.infoPB = new System.Windows.Forms.PictureBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.addTextPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.addInputPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.inputKindCB = new System.Windows.Forms.ComboBox();
            this.blockInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infoPB)).BeginInit();
            this.panel1.SuspendLayout();
            this.addTextPanel.SuspendLayout();
            this.addInputPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // removeInfoBtnsPanel
            // 
            this.removeInfoBtnsPanel.AutoSize = true;
            this.removeInfoBtnsPanel.Location = new System.Drawing.Point(4, 53);
            this.removeInfoBtnsPanel.Name = "removeInfoBtnsPanel";
            this.removeInfoBtnsPanel.Size = new System.Drawing.Size(450, 26);
            this.removeInfoBtnsPanel.TabIndex = 19;
            // 
            // blockInfoPanel
            // 
            this.blockInfoPanel.AutoScroll = true;
            this.blockInfoPanel.Controls.Add(this.infoPB);
            this.blockInfoPanel.Controls.Add(this.removeInfoBtnsPanel);
            this.blockInfoPanel.Location = new System.Drawing.Point(22, 9);
            this.blockInfoPanel.Name = "blockInfoPanel";
            this.blockInfoPanel.Size = new System.Drawing.Size(456, 99);
            this.blockInfoPanel.TabIndex = 18;
            // 
            // infoPB
            // 
            this.infoPB.Location = new System.Drawing.Point(3, 3);
            this.infoPB.Name = "infoPB";
            this.infoPB.Size = new System.Drawing.Size(329, 50);
            this.infoPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.infoPB.TabIndex = 0;
            this.infoPB.TabStop = false;
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(311, 221);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 27);
            this.cancelButton.TabIndex = 17;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // addButton
            // 
            this.addButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addButton.Location = new System.Drawing.Point(403, 221);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 27);
            this.addButton.TabIndex = 16;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.addButton_MouseUp);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.addTextPanel);
            this.panel1.Controls.Add(this.addInputPanel);
            this.panel1.Location = new System.Drawing.Point(25, 110);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(456, 90);
            this.panel1.TabIndex = 20;
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
            // AddFuncForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 263);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.blockInfoPanel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddFuncForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add func";
            this.Load += new System.EventHandler(this.AddFuncForm_Load);
            this.blockInfoPanel.ResumeLayout(false);
            this.blockInfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infoPB)).EndInit();
            this.panel1.ResumeLayout(false);
            this.addTextPanel.ResumeLayout(false);
            this.addTextPanel.PerformLayout();
            this.addInputPanel.ResumeLayout(false);
            this.addInputPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel removeInfoBtnsPanel;
        private System.Windows.Forms.Panel blockInfoPanel;
        private System.Windows.Forms.PictureBox infoPB;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel addTextPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel addInputPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox inputKindCB;
    }
}