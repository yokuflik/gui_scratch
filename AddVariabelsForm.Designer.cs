namespace GuiScratch
{
    partial class AddVariabelsAndListsForm
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
            this.varOrListLabel = new System.Windows.Forms.Label();
            this.nameTB = new System.Windows.Forms.TextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.reasonLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // varOrListLabel
            // 
            this.varOrListLabel.AutoSize = true;
            this.varOrListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.varOrListLabel.Location = new System.Drawing.Point(33, 25);
            this.varOrListLabel.Name = "varOrListLabel";
            this.varOrListLabel.Size = new System.Drawing.Size(99, 16);
            this.varOrListLabel.TabIndex = 4;
            this.varOrListLabel.Text = "Variabel name:";
            // 
            // nameTB
            // 
            this.nameTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTB.Location = new System.Drawing.Point(149, 22);
            this.nameTB.Name = "nameTB";
            this.nameTB.Size = new System.Drawing.Size(278, 24);
            this.nameTB.TabIndex = 5;
            this.nameTB.TextChanged += new System.EventHandler(this.nameTB_TextChanged);
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(244, 85);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 27);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // addButton
            // 
            this.addButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addButton.Location = new System.Drawing.Point(334, 85);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 27);
            this.addButton.TabIndex = 8;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // reasonLabel
            // 
            this.reasonLabel.AutoSize = true;
            this.reasonLabel.BackColor = System.Drawing.Color.Red;
            this.reasonLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reasonLabel.Location = new System.Drawing.Point(9, 56);
            this.reasonLabel.Name = "reasonLabel";
            this.reasonLabel.Size = new System.Drawing.Size(0, 17);
            this.reasonLabel.TabIndex = 10;
            // 
            // AddVariabelsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(439, 137);
            this.ControlBox = false;
            this.Controls.Add(this.reasonLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.nameTB);
            this.Controls.Add(this.varOrListLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddVariabelsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AddVariabels";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label varOrListLabel;
        private System.Windows.Forms.TextBox nameTB;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label reasonLabel;
    }
}