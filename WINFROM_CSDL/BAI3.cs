using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // Import MySql library

namespace WINFROM_CSDL
{
    public partial class BAI3 : Form
    {
        public BAI3()
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
                MySqlDataAdapter sa = new MySqlDataAdapter("SELECT * FROM khoa", conn);
                DataTable dt = new DataTable();
                sa.Fill(dt);
                dGV_DSKhoa.DataSource = dt;
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
            txtMakhoa.DataBindings.Clear();
            txtMakhoa.DataBindings.Add("Text", dGV_DSKhoa.DataSource, "ma_khoa");
            txttenKhoa.DataBindings.Clear();
            txttenKhoa.DataBindings.Add("Text", dGV_DSKhoa.DataSource, "ten_khoa");
        }
        private void them()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO khoa (ma_khoa, ten_khoa) VALUES (@maKhoa, @tenKhoa)", conn);
                cmd.Parameters.AddWithValue("@maKhoa", txtMakhoa.Text);
                cmd.Parameters.AddWithValue("@tenKhoa", txttenKhoa.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private void xoa()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("DELETE FROM khoa WHERE ma_khoa = @maKhoa", conn);
                cmd.Parameters.AddWithValue("@maKhoa", txtMakhoa.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        private void sua()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE khoa SET ten_khoa = @tenKhoa WHERE ma_khoa = @maKhoa", conn);
                cmd.Parameters.AddWithValue("@maKhoa", txtMakhoa.Text);
                cmd.Parameters.AddWithValue("@tenKhoa", txttenKhoa.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            sua();
            BAI3_Load(sender, e);
        }

        private void BAI3_Load(object sender, EventArgs e)
        {
            getData();
            bingding();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            // Implement your logic if needed
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            them();
            BAI3_Load(sender, e);
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            xoa();
            BAI3_Load(sender, e);
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
