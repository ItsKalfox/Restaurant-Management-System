namespace RestaurantManagementSystem.App
{
    partial class OrderDetails
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.orderitemLabel = new System.Windows.Forms.Label();
            this.orderquantityLabel = new System.Windows.Forms.Label();
            this.doneBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // orderitemLabel
            // 
            this.orderitemLabel.AutoSize = true;
            this.orderitemLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderitemLabel.ForeColor = System.Drawing.Color.White;
            this.orderitemLabel.Location = new System.Drawing.Point(45, 16);
            this.orderitemLabel.Name = "orderitemLabel";
            this.orderitemLabel.Size = new System.Drawing.Size(84, 19);
            this.orderitemLabel.TabIndex = 0;
            this.orderitemLabel.Text = "Order Item";
            // 
            // orderquantityLabel
            // 
            this.orderquantityLabel.AutoSize = true;
            this.orderquantityLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderquantityLabel.ForeColor = System.Drawing.Color.White;
            this.orderquantityLabel.Location = new System.Drawing.Point(466, 19);
            this.orderquantityLabel.Name = "orderquantityLabel";
            this.orderquantityLabel.Size = new System.Drawing.Size(17, 19);
            this.orderquantityLabel.TabIndex = 1;
            this.orderquantityLabel.Text = "0";
            // 
            // doneBtn
            // 
            this.doneBtn.BackColor = System.Drawing.Color.IndianRed;
            this.doneBtn.FlatAppearance.BorderSize = 0;
            this.doneBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.doneBtn.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doneBtn.ForeColor = System.Drawing.Color.Black;
            this.doneBtn.Location = new System.Drawing.Point(874, 15);
            this.doneBtn.Name = "doneBtn";
            this.doneBtn.Size = new System.Drawing.Size(75, 23);
            this.doneBtn.TabIndex = 2;
            this.doneBtn.Text = "Done";
            this.doneBtn.UseVisualStyleBackColor = false;
            // 
            // OrderDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.doneBtn);
            this.Controls.Add(this.orderquantityLabel);
            this.Controls.Add(this.orderitemLabel);
            this.Name = "OrderDetails";
            this.Size = new System.Drawing.Size(981, 52);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label orderitemLabel;
        private System.Windows.Forms.Label orderquantityLabel;
        private System.Windows.Forms.Button doneBtn;
    }
}
