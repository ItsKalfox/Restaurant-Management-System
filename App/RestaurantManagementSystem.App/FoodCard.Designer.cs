namespace RestaurantManagementSystem.App
{
    partial class FoodCard
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
            this.itemnameLabel = new System.Windows.Forms.Label();
            this.priceLabel = new System.Windows.Forms.Label();
            this.ratingLabel = new System.Windows.Forms.Label();
            this.addBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.itempicPicbox = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itempicPicbox)).BeginInit();
            this.SuspendLayout();
            // 
            // itemnameLabel
            // 
            this.itemnameLabel.AutoSize = true;
            this.itemnameLabel.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemnameLabel.ForeColor = System.Drawing.Color.White;
            this.itemnameLabel.Location = new System.Drawing.Point(5, 216);
            this.itemnameLabel.Name = "itemnameLabel";
            this.itemnameLabel.Size = new System.Drawing.Size(80, 26);
            this.itemnameLabel.TabIndex = 1;
            this.itemnameLabel.Text = "Item 01";
            // 
            // priceLabel
            // 
            this.priceLabel.AutoSize = true;
            this.priceLabel.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.priceLabel.ForeColor = System.Drawing.Color.Lime;
            this.priceLabel.Location = new System.Drawing.Point(9, 278);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(61, 18);
            this.priceLabel.TabIndex = 2;
            this.priceLabel.Text = "LKR 2000";
            // 
            // ratingLabel
            // 
            this.ratingLabel.AutoSize = true;
            this.ratingLabel.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ratingLabel.ForeColor = System.Drawing.Color.White;
            this.ratingLabel.Location = new System.Drawing.Point(31, 245);
            this.ratingLabel.Name = "ratingLabel";
            this.ratingLabel.Size = new System.Drawing.Size(26, 18);
            this.ratingLabel.TabIndex = 3;
            this.ratingLabel.Text = "5.0";
            // 
            // addBtn
            // 
            this.addBtn.BackColor = System.Drawing.Color.Lime;
            this.addBtn.FlatAppearance.BorderSize = 0;
            this.addBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addBtn.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addBtn.Location = new System.Drawing.Point(123, 276);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 4;
            this.addBtn.Text = "+  ADD";
            this.addBtn.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.itempicPicbox);
            this.panel1.Controls.Add(this.addBtn);
            this.panel1.Controls.Add(this.itemnameLabel);
            this.panel1.Controls.Add(this.ratingLabel);
            this.panel1.Controls.Add(this.priceLabel);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 308);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::RestaurantManagementSystem.App.Properties.Resources._2b50;
            this.pictureBox1.Location = new System.Drawing.Point(10, 244);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(18, 18);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // itempicPicbox
            // 
            this.itempicPicbox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.itempicPicbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.itempicPicbox.Location = new System.Drawing.Point(0, 0);
            this.itempicPicbox.Name = "itempicPicbox";
            this.itempicPicbox.Size = new System.Drawing.Size(208, 208);
            this.itempicPicbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.itempicPicbox.TabIndex = 0;
            this.itempicPicbox.TabStop = false;
            this.itempicPicbox.Click += new System.EventHandler(this.itempicPicbox_Click);
            // 
            // FoodCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.panel1);
            this.Name = "FoodCard";
            this.Size = new System.Drawing.Size(232, 331);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itempicPicbox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox itempicPicbox;
        private System.Windows.Forms.Label itemnameLabel;
        private System.Windows.Forms.Label priceLabel;
        private System.Windows.Forms.Label ratingLabel;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
