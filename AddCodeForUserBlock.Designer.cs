namespace GuiScratch
{
    partial class AddCodeForUserBlock
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
            this.getCodeButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.blockInfoPanel = new System.Windows.Forms.Panel();
            this.infoPB = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.addInfoIndexPanel = new System.Windows.Forms.Panel();
            this.addInfoIndexNUD = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.addTextPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.codeViewPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.removeCodeItemPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.blockNameTB = new System.Windows.Forms.TextBox();
            this.blockInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infoPB)).BeginInit();
            this.panel2.SuspendLayout();
            this.addInfoIndexPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addInfoIndexNUD)).BeginInit();
            this.addTextPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // getCodeButton
            // 
            this.getCodeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getCodeButton.Location = new System.Drawing.Point(344, 296);
            this.getCodeButton.Name = "getCodeButton";
            this.getCodeButton.Size = new System.Drawing.Size(75, 27);
            this.getCodeButton.TabIndex = 13;
            this.getCodeButton.Text = "Get code";
            this.getCodeButton.UseVisualStyleBackColor = true;
            this.getCodeButton.Click += new System.EventHandler(this.getCodeButton_Click);
            // 
            // addButton
            // 
            this.addButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addButton.Location = new System.Drawing.Point(438, 296);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 27);
            this.addButton.TabIndex = 12;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.addButton_MouseUp);
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(250, 296);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 27);
            this.cancelButton.TabIndex = 14;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // blockInfoPanel
            // 
            this.blockInfoPanel.AutoScroll = true;
            this.blockInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blockInfoPanel.Controls.Add(this.infoPB);
            this.blockInfoPanel.Location = new System.Drawing.Point(12, 12);
            this.blockInfoPanel.Name = "blockInfoPanel";
            this.blockInfoPanel.Size = new System.Drawing.Size(501, 48);
            this.blockInfoPanel.TabIndex = 15;
            // 
            // infoPB
            // 
            this.infoPB.Location = new System.Drawing.Point(3, 3);
            this.infoPB.Name = "infoPB";
            this.infoPB.Size = new System.Drawing.Size(329, 40);
            this.infoPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.infoPB.TabIndex = 0;
            this.infoPB.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Code:";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.addInfoIndexPanel);
            this.panel2.Controls.Add(this.addTextPanel);
            this.panel2.Location = new System.Drawing.Point(12, 154);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(501, 91);
            this.panel2.TabIndex = 17;
            // 
            // addInfoIndexPanel
            // 
            this.addInfoIndexPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.addInfoIndexPanel.Controls.Add(this.addInfoIndexNUD);
            this.addInfoIndexPanel.Controls.Add(this.label2);
            this.addInfoIndexPanel.Location = new System.Drawing.Point(3, 47);
            this.addInfoIndexPanel.Name = "addInfoIndexPanel";
            this.addInfoIndexPanel.Size = new System.Drawing.Size(210, 38);
            this.addInfoIndexPanel.TabIndex = 5;
            // 
            // addInfoIndexNUD
            // 
            this.addInfoIndexNUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addInfoIndexNUD.Location = new System.Drawing.Point(109, 7);
            this.addInfoIndexNUD.Name = "addInfoIndexNUD";
            this.addInfoIndexNUD.Size = new System.Drawing.Size(96, 23);
            this.addInfoIndexNUD.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Add info index";
            // 
            // addTextPanel
            // 
            this.addTextPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.addTextPanel.Controls.Add(this.label4);
            this.addTextPanel.Location = new System.Drawing.Point(3, 3);
            this.addTextPanel.Name = "addTextPanel";
            this.addTextPanel.Size = new System.Drawing.Size(210, 38);
            this.addTextPanel.TabIndex = 4;
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
            // codeViewPanel
            // 
            this.codeViewPanel.AutoSize = true;
            this.codeViewPanel.Location = new System.Drawing.Point(0, 43);
            this.codeViewPanel.Name = "codeViewPanel";
            this.codeViewPanel.Size = new System.Drawing.Size(444, 40);
            this.codeViewPanel.TabIndex = 19;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.removeCodeItemPanel);
            this.panel1.Controls.Add(this.codeViewPanel);
            this.panel1.Location = new System.Drawing.Point(69, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 86);
            this.panel1.TabIndex = 20;
            // 
            // removeCodeItemPanel
            // 
            this.removeCodeItemPanel.AutoSize = true;
            this.removeCodeItemPanel.Location = new System.Drawing.Point(3, 3);
            this.removeCodeItemPanel.Name = "removeCodeItemPanel";
            this.removeCodeItemPanel.Size = new System.Drawing.Size(441, 34);
            this.removeCodeItemPanel.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(247, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Block name:";
            // 
            // blockNameTB
            // 
            this.blockNameTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blockNameTB.Location = new System.Drawing.Point(338, 261);
            this.blockNameTB.Name = "blockNameTB";
            this.blockNameTB.Size = new System.Drawing.Size(160, 23);
            this.blockNameTB.TabIndex = 21;
            // 
            // AddCodeForUserBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 335);
            this.Controls.Add(this.blockNameTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.blockInfoPanel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.getCodeButton);
            this.Controls.Add(this.addButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddCodeForUserBlock";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add code";
            this.Load += new System.EventHandler(this.AddCodeForUserBlock_Load);
            this.blockInfoPanel.ResumeLayout(false);
            this.blockInfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infoPB)).EndInit();
            this.panel2.ResumeLayout(false);
            this.addInfoIndexPanel.ResumeLayout(false);
            this.addInfoIndexPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addInfoIndexNUD)).EndInit();
            this.addTextPanel.ResumeLayout(false);
            this.addTextPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button getCodeButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel blockInfoPanel;
        private System.Windows.Forms.PictureBox infoPB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel addInfoIndexPanel;
        private System.Windows.Forms.NumericUpDown addInfoIndexNUD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel addTextPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel codeViewPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel removeCodeItemPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox blockNameTB;
    }
}