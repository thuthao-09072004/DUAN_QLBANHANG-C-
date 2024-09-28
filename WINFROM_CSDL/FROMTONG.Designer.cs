namespace WINFROM_CSDL
{
    partial class FROMTONG
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonsv = new System.Windows.Forms.Button();
            this.btnbh = new System.Windows.Forms.Button();
            this.buttonthoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.IndianRed;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(130, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(551, 69);
            this.label1.TabIndex = 0;
            this.label1.Text = "HÃY CHỌN FROM";
            // 
            // buttonsv
            // 
            this.buttonsv.BackColor = System.Drawing.Color.RosyBrown;
            this.buttonsv.Location = new System.Drawing.Point(34, 131);
            this.buttonsv.Name = "buttonsv";
            this.buttonsv.Size = new System.Drawing.Size(164, 111);
            this.buttonsv.TabIndex = 1;
            this.buttonsv.Text = "FROM QUẢN LÝ SINH VIÊN";
            this.buttonsv.UseVisualStyleBackColor = false;
            this.buttonsv.Click += new System.EventHandler(this.buttonsv_Click);
            // 
            // btnbh
            // 
            this.btnbh.BackColor = System.Drawing.Color.RosyBrown;
            this.btnbh.Location = new System.Drawing.Point(454, 144);
            this.btnbh.Name = "btnbh";
            this.btnbh.Size = new System.Drawing.Size(164, 111);
            this.btnbh.TabIndex = 2;
            this.btnbh.Text = "FROM QUẢN LÝ BÁN HÀNG";
            this.btnbh.UseVisualStyleBackColor = false;
            this.btnbh.Click += new System.EventHandler(this.btnbh_Click);
            // 
            // buttonthoat
            // 
            this.buttonthoat.Location = new System.Drawing.Point(300, 326);
            this.buttonthoat.Name = "buttonthoat";
            this.buttonthoat.Size = new System.Drawing.Size(153, 70);
            this.buttonthoat.TabIndex = 3;
            this.buttonthoat.Text = "thoát chương trình";
            this.buttonthoat.UseVisualStyleBackColor = true;
            this.buttonthoat.Click += new System.EventHandler(this.buttonthoat_Click);
            // 
            // FROMTONG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonthoat);
            this.Controls.Add(this.btnbh);
            this.Controls.Add(this.buttonsv);
            this.Controls.Add(this.label1);
            this.Name = "FROMTONG";
            this.Text = "FROMTONG";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonsv;
        private System.Windows.Forms.Button btnbh;
        private System.Windows.Forms.Button buttonthoat;
    }
}