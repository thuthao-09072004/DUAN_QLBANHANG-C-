using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WINFROM_CSDL
{
    public partial class fromKhachHang : Form
    {
        private bool isAdding = false;
        private bool isEditing = false;
        public fromKhachHang()
        {
            InitializeComponent();
        }
        MySqlConnection conn = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=quanlybanhang_csdl;Uid=root;Pwd=;");

        private void fromKhachHang_Load(object sender, EventArgs e)
        {
            getData();   // Lấy dữ liệu nhân viên
            bingding();  // Ràng buộc dữ liệu vào các TextBox
            setControlState(false);  // Đặt trạng thái ban đầu cho các nút và TextBox

            // Cấu hình ComboBox giới tính với 3 tùy chọn "Nam", "Nữ", "Khác"
            comboBoxGTKH.Items.Clear();  // Xóa tất cả các mục trước đó
            comboBoxGTKH.Items.Add("Nam");
            comboBoxGTKH.Items.Add("Nữ");
            comboBoxGTKH.Items.Add("Khác");
            comboBoxGTKH.SelectedIndex = -1; // Để không chọn mục nào khi form mới mở
        }
        private void getData()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                MySqlDataAdapter sa = new MySqlDataAdapter("SELECT * FROM khach_hang", conn);
                DataTable dt = new DataTable();
                sa.Fill(dt);
                dgv_khachhang.DataBindings.Clear();
                dgv_khachhang.DataSource = dt;
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
            if (dgv_khachhang.DataSource == null)
            {
                MessageBox.Show("DataSource is null!");
                return;
            }

            txtMaKH.DataBindings.Clear();
            txtMaKH.DataBindings.Add("Text", dgv_khachhang.DataSource, "ma_khach_hang");
            txtTenKH.DataBindings.Clear();
            txtTenKH.DataBindings.Add("Text", dgv_khachhang.DataSource, "ten_khach_hang");
            dateTimePickerNgaySinh.DataBindings.Clear();
            dateTimePickerNgaySinh.DataBindings.Add("Text", dgv_khachhang.DataSource, "ngay_sinh");
            txtdiachi.DataBindings.Clear();
            txtdiachi.DataBindings.Add("Text", dgv_khachhang.DataSource, "dia_chi");
            txtdienthoaiKH.DataBindings.Clear();
            txtdienthoaiKH.DataBindings.Add("Text", dgv_khachhang.DataSource, "dien_thoai");
            comboBoxGTKH.DataBindings.Clear();
            comboBoxGTKH.DataBindings.Add("Text", dgv_khachhang.DataSource, "gioi_tinh");

            txtMaKH.Enabled = false; // Không cho phép chỉnh sửa mã nhân viên
        }
        private void tangma()
        {
            try
            {
                // Mở kết nối nếu cần
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // Lấy tất cả dữ liệu từ bảng khách hàng
                MySqlDataAdapter sa = new MySqlDataAdapter("SELECT * FROM khach_hang", conn);
                DataTable dt = new DataTable();
                sa.Fill(dt);

                // Kiểm tra nếu có dữ liệu trong bảng
                if (dt.Rows.Count > 0)
                {
                    string ma = dt.Rows[dt.Rows.Count - 1]["ma_khach_hang"].ToString();  // Lấy mã khách hàng cuối cùng

                    // Kiểm tra độ dài của mã khách hàng để tránh lỗi Substring
                    if (ma.Length >= 3)
                    {
                        // Chỉ lấy phần số từ mã khách hàng (bỏ qua phần 'KH')
                        int index = Convert.ToInt32(ma.Trim().Substring(2)) + 1;

                        // Tạo mã khách hàng mới với định dạng KH01, KH02,...
                        if (index <= 9)
                            txtMaKH.Text = "KH0" + index;
                        else
                            txtMaKH.Text = "KH" + index;
                    }
                    else
                    {
                        // Xử lý trường hợp mã không đủ dài (ít hơn 3 ký tự)
                        txtMaKH.Text = "KH01";  // Mã đầu tiên nếu dữ liệu không hợp lệ
                    }
                }
                else
                {
                    // Nếu bảng chưa có dữ liệu, tạo mã khách hàng đầu tiên
                    txtMaKH.Text = "KH01";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo mã khách hàng mới: " + ex.Message);
            }
            finally
            {
                // Đóng kết nối nếu mở
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void setControlState(bool enabled)
        {
            txtTenKH.Enabled = enabled;
            dateTimePickerNgaySinh.Enabled = enabled;
            comboBoxGTKH.Enabled = enabled;
            txtdiachi.Enabled = enabled;
            txtdienthoaiKH.Enabled = enabled;
            btnLuu.Enabled = enabled;
            btnhuy.Enabled = enabled;

            btnThem.Enabled = !enabled;
            btnsua.Enabled = !enabled;
            btnxoa.Enabled = !enabled;
            txtMaKH.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            isEditing = false;

            tangma(); // Tạo mã nhân viên mới

            // Clear the input fields for adding a new employee
            txtTenKH.Text = "";
            dateTimePickerNgaySinh.MinDate = new DateTime(1900, 1, 1);
            dateTimePickerNgaySinh.MaxDate = DateTime.Today;
            dateTimePickerNgaySinh.Value = DateTime.Today;
            comboBoxGTKH.SelectedIndex = -1;
            txtdiachi.Text = "";
            txtdienthoaiKH.Text = "";

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
                    string query = "DELETE FROM khach_hang WHERE ma_khach_hang = @maNV";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@maNV", txtMaKH.Text);
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
                    string query = "INSERT INTO khach_hang(ma_khach_hang, ten_khach_hang, ngay_sinh, gioi_tinh, dien_thoai, dia_chi) VALUES  (@maNV, @tenNV, @ngaySinh, @gioiTinh, @dienThoai,@diaChi )";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@maNV", txtMaKH.Text);
                    cmd.Parameters.AddWithValue("@tenNV", txtTenKH.Text);
                    cmd.Parameters.AddWithValue("@ngaySinh", dateTimePickerNgaySinh.Value);
                    cmd.Parameters.AddWithValue("@gioiTinh", comboBoxGTKH.Text);
                    cmd.Parameters.AddWithValue("@dienThoai", txtdienthoaiKH.Text);
                    cmd.Parameters.AddWithValue("@diaChi", txtdiachi.Text);
                    cmd.ExecuteNonQuery();
                }
                else if (isEditing)
                {
                    // Thực hiện cập nhật nhân viên
                    string query = "UPDATE khach_hang SET ten_khach_hang=@tenNV, ngay_sinh=@ngaySinh, gioi_tinh=@gioiTinh,dien_thoai=@dienThoai, dia_chi=@diaChi  WHERE ma_khach_hang=@maNV";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenNV", txtTenKH.Text);
                    cmd.Parameters.AddWithValue("@ngaySinh", dateTimePickerNgaySinh.Value);
                    cmd.Parameters.AddWithValue("@gioiTinh", comboBoxGTKH.Text);
                    cmd.Parameters.AddWithValue("@dienThoai", txtdienthoaiKH.Text);
                    cmd.Parameters.AddWithValue("@diaChi", txtdiachi.Text);
                    cmd.Parameters.AddWithValue("@maNV", txtMaKH.Text);
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
    }
}
