using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WINFROM_CSDL
{
    public partial class fromNhanVien : Form
    {
        private bool isAdding = false;
        private bool isEditing = false;
        public fromNhanVien()
        {
            InitializeComponent();
        }
        MySqlConnection conn = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=quanlybanhang_csdl;Uid=root;Pwd=;");

        private void fromNhanVien_Load(object sender, EventArgs e)
        {
            getData();   // Lấy dữ liệu nhân viên
            bingding();  // Ràng buộc dữ liệu vào các TextBox
            setControlState(false);  // Đặt trạng thái ban đầu cho các nút và TextBox

            // Cấu hình ComboBox giới tính với 3 tùy chọn "Nam", "Nữ", "Khác"
            comboBoxGTnhanvien.Items.Clear();  // Xóa tất cả các mục trước đó
            comboBoxGTnhanvien.Items.Add("Nam");
            comboBoxGTnhanvien.Items.Add("Nữ");
            comboBoxGTnhanvien.Items.Add("Khác");
            comboBoxGTnhanvien.SelectedIndex = -1; // Để không chọn mục nào khi form mới mở
        }

        private void getData()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                MySqlDataAdapter sa = new MySqlDataAdapter("SELECT * FROM nhan_vien", conn);
                DataTable dt = new DataTable();
                sa.Fill(dt);
                dGv_nhanvien.DataBindings.Clear();
                dGv_nhanvien.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void bingding()
        {
            if (dGv_nhanvien.DataSource == null)
            {
                MessageBox.Show("DataSource is null!");
                return;
            }

            txtMaNhanvien.DataBindings.Clear();
            txtMaNhanvien.DataBindings.Add("Text", dGv_nhanvien.DataSource, "ma_nhan_vien");
            txtTennhanvien.DataBindings.Clear();
            txtTennhanvien.DataBindings.Add("Text", dGv_nhanvien.DataSource, "ten_nhan_vien");
            dateTimePickernhanvien.DataBindings.Clear();
            dateTimePickernhanvien.DataBindings.Add("Text", dGv_nhanvien.DataSource, "ngay_sinh");
            txtdiachiNV.DataBindings.Clear();
            txtdiachiNV.DataBindings.Add("Text", dGv_nhanvien.DataSource, "dia_chi");
            txtdienthoaiNV.DataBindings.Clear();
            txtdienthoaiNV.DataBindings.Add("Text", dGv_nhanvien.DataSource, "dien_thoai");
            comboBoxGTnhanvien.DataBindings.Clear();
            comboBoxGTnhanvien.DataBindings.Add("Text", dGv_nhanvien.DataSource, "gioi_tinh");

            txtMaNhanvien.Enabled = false; // Không cho phép chỉnh sửa mã nhân viên
        }

        // Hàm để tự động tăng mã nhân viên
        private void tangma()
        {
            MySqlDataAdapter sa = new MySqlDataAdapter("select * from nhan_vien", conn);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            string ma = dt.Rows[dt.Rows.Count - 1][0].ToString();
            int index = Convert.ToInt32(ma.Trim().Substring(2, 3)) + 1;
            if (index <= 9)
                txtMaNhanvien.Text = "NV00" + index;
            else if (index <= 99)
                txtMaNhanvien.Text = "NV0" + index;
            else
                txtMaNhanvien.Text = "NV" + index;
        }

        private void setControlState(bool enabled)
        {
            txtTennhanvien.Enabled = enabled;
            dateTimePickernhanvien.Enabled = enabled;
            comboBoxGTnhanvien.Enabled = enabled;
            txtdiachiNV.Enabled = enabled;
            txtdienthoaiNV.Enabled = enabled;
            btnLuu.Enabled = enabled;
            btnhuy.Enabled = enabled;

            btnThem.Enabled = !enabled;
            btnsua.Enabled = !enabled;
            btnxoa.Enabled = !enabled;
            txtMaNhanvien.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            isEditing = false;

            tangma(); // Tạo mã nhân viên mới

            // Clear the input fields for adding a new employee
            txtTennhanvien.Text = "";
            dateTimePickernhanvien.MinDate = new DateTime(1900, 1, 1);
            dateTimePickernhanvien.MaxDate = DateTime.Today;
            dateTimePickernhanvien.Value = DateTime.Today;
            comboBoxGTnhanvien.SelectedIndex = -1;
            txtdiachiNV.Text = "";
            txtdienthoaiNV.Text = "";

            setControlState(true); // Enable the form fields for adding a new employee
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            isAdding = false;
            isEditing = true;

            setControlState(true); // Kích hoạt các trường để sửa
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM nhan_vien WHERE ma_nhan_vien = @maNV";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@maNV", txtMaNhanvien.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa nhân viên thành công");
                    getData();
                    bingding();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (isAdding)
                {
                    // Thực hiện thêm nhân viên mới
                    string query = "INSERT INTO nhan_vien(ma_nhan_vien, ten_nhan_vien, ngay_sinh, gioi_tinh, dien_thoai, dia_chi) VALUES  (@maNV, @tenNV, @ngaySinh, @gioiTinh, @dienThoai,@diaChi )";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@maNV", txtMaNhanvien.Text);
                    cmd.Parameters.AddWithValue("@tenNV", txtTennhanvien.Text);
                    cmd.Parameters.AddWithValue("@ngaySinh", dateTimePickernhanvien.Value);
                    cmd.Parameters.AddWithValue("@gioiTinh", comboBoxGTnhanvien.Text);
                    cmd.Parameters.AddWithValue("@dienThoai", txtdienthoaiNV.Text);
                    cmd.Parameters.AddWithValue("@diaChi", txtdiachiNV.Text);
                    cmd.ExecuteNonQuery();
                }
                else if (isEditing)
                {
                    // Thực hiện cập nhật nhân viên
                    string query = "UPDATE nhan_vien SET ten_nhan_vien=@tenNV, ngay_sinh=@ngaySinh, gioi_tinh=@gioiTinh,dien_thoai=@dienThoai, dia_chi=@diaChi  WHERE ma_nhan_vien=@maNV";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenNV", txtTennhanvien.Text);
                    cmd.Parameters.AddWithValue("@ngaySinh", dateTimePickernhanvien.Value);
                    cmd.Parameters.AddWithValue("@gioiTinh", comboBoxGTnhanvien.Text);
                    cmd.Parameters.AddWithValue("@dienThoai", txtdienthoaiNV.Text);
                    cmd.Parameters.AddWithValue("@diaChi", txtdiachiNV.Text);
                    cmd.Parameters.AddWithValue("@maNV", txtMaNhanvien.Text);
                    cmd.ExecuteNonQuery();
                }

                // Reload lại dữ liệu sau khi thêm/sửa
                getData();
                bingding();
                setControlState(false); // Sau khi lưu, khóa lại các trường
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu nhân viên: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            bingding(); // Hủy bỏ thay đổi và hiển thị lại dữ liệu gốc
            setControlState(false); // Vô hiệu hóa các trường nhập liệu
        }

        private void btndong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }

    
    }

