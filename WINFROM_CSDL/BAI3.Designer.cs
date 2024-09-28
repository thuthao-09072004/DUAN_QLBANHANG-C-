namespace WINFROM_CSDL
{
    partial class BAI3
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
            this.txtMakhoa = new System.Windows.Forms.TextBox();
            this.txttenKhoa = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnsua = new System.Windows.Forms.Button();
            this.btnxoa = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dGV_DSKhoa = new System.Windows.Forms.DataGridView();
            this.ma_khoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ten_khoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnthoat = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_DSKhoa)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMakhoa
            // 
            this.txtMakhoa.Location = new System.Drawing.Point(107, 34);
            this.txtMakhoa.Multiline = true;
            this.txtMakhoa.Name = "txtMakhoa";
            this.txtMakhoa.Size = new System.Drawing.Size(188, 30);
            this.txtMakhoa.TabIndex = 0;
            // 
            // txttenKhoa
            // 
            this.txttenKhoa.Location = new System.Drawing.Point(441, 34);
            this.txttenKhoa.Multiline = true;
            this.txttenKhoa.Name = "txttenKhoa";
            this.txttenKhoa.Size = new System.Drawing.Size(188, 30);
            this.txttenKhoa.TabIndex = 1;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(80, 121);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(102, 32);
            this.btnThem.TabIndex = 2;
            this.btnThem.Text = "thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnsua
            // 
            this.btnsua.Location = new System.Drawing.Point(306, 121);
            this.btnsua.Name = "btnsua";
            this.btnsua.Size = new System.Drawing.Size(75, 27);
            this.btnsua.TabIndex = 3;
            this.btnsua.Text = "sửa";
            this.btnsua.UseVisualStyleBackColor = true;
            this.btnsua.Click += new System.EventHandler(this.btnsua_Click);
            // 
            // btnxoa
            // 
            this.btnxoa.Location = new System.Drawing.Point(490, 121);
            this.btnxoa.Name = "btnxoa";
            this.btnxoa.Size = new System.Drawing.Size(75, 27);
            this.btnxoa.TabIndex = 4;
            this.btnxoa.Text = "xóa";
            this.btnxoa.UseVisualStyleBackColor = true;
            this.btnxoa.Click += new System.EventHandler(this.btnxoa_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dGV_DSKhoa);
            this.groupBox1.Location = new System.Drawing.Point(12, 187);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(764, 251);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // dGV_DSKhoa
            // 
            this.dGV_DSKhoa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV_DSKhoa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ma_khoa,
            this.ten_khoa});
            this.dGV_DSKhoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGV_DSKhoa.Location = new System.Drawing.Point(3, 18);
            this.dGV_DSKhoa.Name = "dGV_DSKhoa";
            this.dGV_DSKhoa.RowHeadersWidth = 51;
            this.dGV_DSKhoa.RowTemplate.Height = 24;
            this.dGV_DSKhoa.Size = new System.Drawing.Size(758, 230);
            this.dGV_DSKhoa.TabIndex = 0;
            // 
            // ma_khoa
            // 
            this.ma_khoa.DataPropertyName = "ma_khoa";
            this.ma_khoa.HeaderText = "mã khoa";
            this.ma_khoa.MinimumWidth = 6;
            this.ma_khoa.Name = "ma_khoa";
            this.ma_khoa.Width = 125;
            // 
            // ten_khoa
            // 
            this.ten_khoa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ten_khoa.DataPropertyName = "ten_khoa";
            this.ten_khoa.HeaderText = "tên khoa";
            this.ten_khoa.MinimumWidth = 6;
            this.ten_khoa.Name = "ten_khoa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Mã khoa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(373, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "tên khoa";
            // 
            // btnthoat
            // 
            this.btnthoat.Location = new System.Drawing.Point(672, 121);
            this.btnthoat.Name = "btnthoat";
            this.btnthoat.Size = new System.Drawing.Size(75, 27);
            this.btnthoat.TabIndex = 8;
            this.btnthoat.Text = "thoát";
            this.btnthoat.UseVisualStyleBackColor = true;
            this.btnthoat.Click += new System.EventHandler(this.btnthoat_Click);
            // 
            // BAI3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnthoat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnxoa);
            this.Controls.Add(this.btnsua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.txttenKhoa);
            this.Controls.Add(this.txtMakhoa);
            this.Name = "BAI3";
            this.Text = "BAI3";
            this.Load += new System.EventHandler(this.BAI3_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGV_DSKhoa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMakhoa;
        private System.Windows.Forms.TextBox txttenKhoa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnsua;
        private System.Windows.Forms.Button btnxoa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dGV_DSKhoa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnthoat;
        private System.Windows.Forms.DataGridViewTextBoxColumn ma_khoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn ten_khoa;
    }
}