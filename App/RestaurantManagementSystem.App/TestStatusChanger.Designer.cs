namespace RestaurantManagementSystem.App
{
    partial class TableStatusTestForm
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
            this.tableComboBox = new System.Windows.Forms.ComboBox();
            this.statusComboBox = new System.Windows.Forms.ComboBox();
            this.resultLabel = new System.Windows.Forms.Label();
            this.updateStatusButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tableComboBox
            // 
            this.tableComboBox.BackColor = System.Drawing.Color.Black;
            this.tableComboBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableComboBox.ForeColor = System.Drawing.Color.White;
            this.tableComboBox.FormattingEnabled = true;
            this.tableComboBox.Location = new System.Drawing.Point(38, 37);
            this.tableComboBox.Name = "tableComboBox";
            this.tableComboBox.Size = new System.Drawing.Size(121, 27);
            this.tableComboBox.TabIndex = 0;
            // 
            // statusComboBox
            // 
            this.statusComboBox.BackColor = System.Drawing.Color.Black;
            this.statusComboBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusComboBox.ForeColor = System.Drawing.Color.White;
            this.statusComboBox.FormattingEnabled = true;
            this.statusComboBox.Location = new System.Drawing.Point(209, 37);
            this.statusComboBox.Name = "statusComboBox";
            this.statusComboBox.Size = new System.Drawing.Size(121, 27);
            this.statusComboBox.TabIndex = 1;
            // 
            // resultLabel
            // 
            this.resultLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultLabel.ForeColor = System.Drawing.Color.White;
            this.resultLabel.Location = new System.Drawing.Point(38, 160);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(292, 19);
            this.resultLabel.TabIndex = 2;
            this.resultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.resultLabel.Click += new System.EventHandler(this.resultLabel_Click);
            // 
            // updateStatusButton
            // 
            this.updateStatusButton.BackColor = System.Drawing.Color.Lime;
            this.updateStatusButton.FlatAppearance.BorderSize = 0;
            this.updateStatusButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateStatusButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateStatusButton.ForeColor = System.Drawing.Color.Black;
            this.updateStatusButton.Location = new System.Drawing.Point(113, 102);
            this.updateStatusButton.Name = "updateStatusButton";
            this.updateStatusButton.Size = new System.Drawing.Size(145, 40);
            this.updateStatusButton.TabIndex = 3;
            this.updateStatusButton.Text = "UPDATE";
            this.updateStatusButton.UseVisualStyleBackColor = false;
            this.updateStatusButton.Click += new System.EventHandler(this.updateStatusButton_Click);
            // 
            // TableStatusTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(368, 213);
            this.Controls.Add(this.updateStatusButton);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.statusComboBox);
            this.Controls.Add(this.tableComboBox);
            this.Name = "TableStatusTestForm";
            this.Text = "TestStatusChanger";
            this.Load += new System.EventHandler(this.TableStatusTestForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox tableComboBox;
        private System.Windows.Forms.ComboBox statusComboBox;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.Button updateStatusButton;
    }
}