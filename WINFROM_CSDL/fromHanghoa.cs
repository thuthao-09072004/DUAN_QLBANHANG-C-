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
    public partial class fromHanghoa : Form
    {
        private bool isAdding = false;
        private bool isEditing = false;
        public fromHanghoa()
        {
            InitializeComponent();
        }
        MySqlConnection conn = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=quanlybanhang_csdl;Uid=root;Pwd=;");

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void fromHanghoa_Load(object sender, EventArgs e)
        {
            getData();   // Lấy dữ liệu nhân viên
            bingding();  // Ràng buộc dữ liệu vào các TextBox
            setControlState(false);  // Đặt trạng thái ban đầu cho các nút và TextBox

           
        }
        private void getData()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                MySqlDataAdapter sa = new MySqlDataAdapter("SELECT * FROM hang_hoa", conn);
                DataTable dt = new DataTable();
                sa.Fill(dt);
                dgv_HH.DataBindings.Clear();
                dgv_HH.DataSource = dt;
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
            if (dgv_HH.DataSource == null)
            {
                MessageBox.Show("DataSource is null!");
                return;
            }

            txtMaHH.DataBindings.Clear();
            txtMaHH.DataBindings.Add("Text", dgv_HH.DataSource, "ma_hang");
            txtTenHH.DataBindings.Clear();
            txtTenHH.DataBindings.Add("Text", dgv_HH.DataSource, "ten_hang");
           
            txtDG.DataBindings.Clear();
            txtDG.DataBindings.Add("Text", dgv_HH.DataSource, "don_gia");
            txtdvt.DataBindings.Clear();
            txtdvt.DataBindings.Add("Text", dgv_HH.DataSource, "don_vi_tinh");
            

            txtMaHH.Enabled = false; // Không cho phép chỉnh sửa mã nhân viên
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

                // Lấy tất cả dữ liệu từ bảng hàng hóa
                MySqlDataAdapter sa = new MySqlDataAdapter("SELECT * FROM hang_hoa", conn);
                DataTable dt = new DataTable();
                sa.Fill(dt);

                // Kiểm tra nếu có dữ liệu trong bảng
                if (dt.Rows.Count > 0)
                {
                    string ma = dt.Rows[dt.Rows.Count - 1]["ma_hang"].ToString();  // Lấy mã hàng hóa cuối cùng

                    // Kiểm tra độ dài của mã hàng hóa để tránh lỗi Substring
                    if (ma.Length >= 3)
                    {
                        // Chỉ lấy phần số từ mã hàng hóa (bỏ qua phần 'HH')
                        int index = Convert.ToInt32(ma.Trim().Substring(2)) + 1;

                        // Tạo mã hàng hóa mới với định dạng HH01, HH02,...
                        if (index <= 9)
                            txtMaHH.Text = "HH0" + index;
                        else
                            txtMaHH.Text = "HH" + index;
                    }
                    else
                    {
                        // Xử lý trường hợp mã không đủ dài (ít hơn 3 ký tự)
                        txtMaHH.Text = "HH01";  // Mã đầu tiên nếu dữ liệu không hợp lệ
                    }
                }
                else
                {
                    // Nếu bảng chưa có dữ liệu, tạo mã hàng hóa đầu tiên
                    txtMaHH.Text = "HH01";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo mã hàng hóa mới: " + ex.Message);
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
            txtTenHH.Enabled = enabled;
           
            txtDG.Enabled = enabled;
            txtdvt.Enabled = enabled;
            btnLuu.Enabled = enabled;
            btnhuy.Enabled = enabled;

            btnThem.Enabled = !enabled;
            btnsua.Enabled = !enabled;
            btnxoa.Enabled = !enabled;
            txtMaHH.Enabled = false;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtdvt_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            isEditing = false;

            tangma(); // Tạo mã nhân viên mới

            // Clear the input fields for adding a new employee
            txtTenHH.Text = "";
            
            txtDG.Text = "";
            txtdvt.Text = "";

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
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa hàng  này không?", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM hang_hoa WHERE ma_hang = @maNV";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@maNV", txtMaHH.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa nhân viên thành công");
                    getData();
                    bingding();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa hàng hóa: " + ex.Message);
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
                    string query = "INSERT INTO hang_hoa(ma_hang, ten_hang, don_gia, don_vi_tinh) VALUES  (@maNV, @tenNV,  @dienThoai,@diaChi )";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@maNV", txtMaHH.Text);
                    cmd.Parameters.AddWithValue("@tenNV", txtTenHH.Text);
            
                    cmd.Parameters.AddWithValue("@dienThoai", txtDG.Text);
                    cmd.Parameters.AddWithValue("@diaChi", txtdvt.Text);
                    cmd.ExecuteNonQuery();
                }
                else if (isEditing)
                {
                    // Thực hiện cập nhật nhân viên
                    string query = "UPDATE hang_hoa SET ten_hang=@tenNV,  don_gia=@dienThoai, don_vi_tinh=@diaChi  WHERE ma_hang=@maNV";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenNV", txtTenHH.Text);
                   
                    cmd.Parameters.AddWithValue("@dienThoai", txtDG.Text);
                    cmd.Parameters.AddWithValue("@diaChi", txtdvt.Text);
                    cmd.Parameters.AddWithValue("@maNV", txtMaHH.Text);
                    cmd.ExecuteNonQuery();
                }

                // Reload lại dữ liệu sau khi thêm/sửa
                getData();
                bingding();
                setControlState(false); // Sau khi lưu, khóa lại các trường
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu hàng hóa: " + ex.Message);
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
