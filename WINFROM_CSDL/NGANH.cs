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
using MySql.Data.MySqlClient; // Import MySql library

namespace WINFROM_CSDL
{
    public partial class NGANH : Form
    {
        public NGANH()
        {
            InitializeComponent();
        }
        // Correct MySQL connection string
        MySqlConnection conn = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=csdl_qlhs;Uid=root;Pwd=;");
        private void getData()
        {
            try
            {
                conn.Open();
                MySqlDataAdapter sa = new MySqlDataAdapter("select *  from nganh   where ma_khoa= '"+comboKhoa.SelectedValue+"'", conn); DataTable dt = new DataTable();
                sa.Fill(dt);
                dgv_nganh.DataSource = dt;
                bingding();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private void bingding()
        {
            txtMaNganh.DataBindings.Clear();
            txtMaNganh.DataBindings.Add("Text", dgv_nganh.DataSource, "ma_nganh");
            txtTenNganh.DataBindings.Clear();
            txtTenNganh.DataBindings.Add("Text", dgv_nganh.DataSource, "ten_nganh");
            //comboKhoa.DataBindings.Clear();
            //comboKhoa.DataBindings.Add("text", dgv_nganh.DataSource, "ma_khoa");
        }
        private void getDataCombox()
        {
            MySqlDataAdapter sa = new MySqlDataAdapter("select * from khoa", conn);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            comboKhoa.DataSource = dt;
            comboKhoa.DisplayMember = "ten_khoa";
            comboKhoa.ValueMember = "ma_khoa";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            getData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            xoa();
            NGANH_Load(sender, e);

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void NGANH_Load(object sender, EventArgs e)
        {
            getDataCombox();
            getData();
           
        }
        private void them()
        {
            MySqlCommand cmd = new MySqlCommand("insert into nganh values('" + txtMaNganh.Text + "',N'" + txtTenNganh.Text + "','"+comboKhoa.SelectedValue+"')", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        private void sua()
        {
            try
            {
                conn.Open();
                string query = "UPDATE nganh SET ten_nganh = @ten_nganh, ma_khoa = @ma_khoa WHERE ma_nganh = @ma_nganh";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // Sử dụng parameterized query để tránh SQL injection
                cmd.Parameters.AddWithValue("@ten_nganh", txtTenNganh.Text);
                cmd.Parameters.AddWithValue("@ma_khoa", comboKhoa.SelectedValue);
                cmd.Parameters.AddWithValue("@ma_nganh", txtMaNganh.Text);

                int rowsAffected = cmd.ExecuteNonQuery(); // Thực thi câu lệnh
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật thành công!");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mã ngành để cập nhật!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
            }
            finally
            {
                conn.Close(); // Đóng kết nối sau khi hoàn thành
                getData(); // Cập nhật lại dữ liệu sau khi sửa
            }
        }
        private void xoa()
        {
            try
            {
                // Hiển thị thông báo xác nhận trước khi xóa
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa ngành này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    conn.Open();
                    string query = "DELETE FROM nganh WHERE ma_nganh = @ma_nganh";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Sử dụng parameterized query để tránh SQL injection
                    cmd.Parameters.AddWithValue("@ma_nganh", txtMaNganh.Text);

                    int rowsAffected = cmd.ExecuteNonQuery(); // Thực thi câu lệnh
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã ngành để xóa!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
            finally
            {
                conn.Close(); // Đóng kết nối sau khi hoàn thành
                getData(); // Cập nhật lại dữ liệu sau khi xóa
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            them();
            getData();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            sua();
            NGANH_Load(sender, e);
        }

        private void dgv_nganh_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
