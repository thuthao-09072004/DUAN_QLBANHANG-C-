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
    public partial class SinhVien : Form
    {
        public SinhVien()
        {
            InitializeComponent();
        }
        MySqlConnection conn = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=csdl_qlhs;Uid=root;Pwd=;");

        private void getData()
        {
            try
            {
                conn.Open();
                MySqlDataAdapter sa = new MySqlDataAdapter("SELECT * FROM sinhvien WHERE ma_lop = @maLop", conn);
                sa.SelectCommand.Parameters.AddWithValue("@maLop", comboLop.SelectedValue);
                DataTable dt = new DataTable();
                sa.Fill(dt);
                dgv_nganh.DataSource = dt;
                bingding(); // Gắn dữ liệu vào các ô nhập liệu
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


        private void getDataCombox()
        {
            MySqlDataAdapter sa = new MySqlDataAdapter("SELECT * FROM lop", conn);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            comboLop.DataSource = dt;
            comboLop.DisplayMember = "ten_lop";
            comboLop.ValueMember = "ma_lop";
        }
        private void bingding()
        {
            txtMSV.DataBindings.Clear();
            txtMSV.DataBindings.Add("Text", dgv_nganh.DataSource, "ma_sinh_vien");
            txtTenSV.DataBindings.Clear();
            txtTenSV.DataBindings.Add("Text", dgv_nganh.DataSource, "ten_sinh_vien");
            txtdiachi.DataBindings.Clear();
            txtdiachi.DataBindings.Add("Text", dgv_nganh.DataSource, "dia_chi");
            txtSDT.DataBindings.Clear();
            txtSDT.DataBindings.Add("Text", dgv_nganh.DataSource, "sdt");
            dateTimePickerNS.DataBindings.Clear();
            dateTimePickerNS.DataBindings.Add("Text", dgv_nganh.DataSource, "ngay_sinh");
        }
        private void SinhVien_Load(object sender, EventArgs e)
        {

            getDataCombox();
            getData();
        }

        private void comboLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            getData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO sinhvien(ma_sinh_vien, ten_sinh_vien, dia_chi, sdt, ngay_sinh, ma_lop) VALUES (@MSV, @TenSV, @DiaChi, @SDT, @NgaySinh, @MaLop)", conn);
                cmd.Parameters.AddWithValue("@MSV", txtMSV.Text);
                cmd.Parameters.AddWithValue("@TenSV", txtTenSV.Text);
                cmd.Parameters.AddWithValue("@DiaChi", txtdiachi.Text);
                cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                cmd.Parameters.AddWithValue("@NgaySinh", dateTimePickerNS.Value);
                cmd.Parameters.AddWithValue("@MaLop", comboLop.SelectedValue);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm sinh viên thành công!");
                getData(); // Cập nhật lại dữ liệu sau khi thêm
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sinh viên: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE sinhvien SET ten_sinh_vien = @TenSV, dia_chi = @DiaChi, sdt = @SDT, ngay_sinh = @NgaySinh, ma_lop = @MaLop WHERE ma_sinh_vien = @MSV", conn);
                cmd.Parameters.AddWithValue("@MSV", txtMSV.Text);
                cmd.Parameters.AddWithValue("@TenSV", txtTenSV.Text);
                cmd.Parameters.AddWithValue("@DiaChi", txtdiachi.Text);
                cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                cmd.Parameters.AddWithValue("@NgaySinh", dateTimePickerNS.Value);
                cmd.Parameters.AddWithValue("@MaLop", comboLop.SelectedValue);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật sinh viên thành công!");
                getData(); // Cập nhật lại dữ liệu sau khi sửa
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa sinh viên: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM sinhvien WHERE ma_sinh_vien = @MSV", conn);
                cmd.Parameters.AddWithValue("@MSV", txtMSV.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa sinh viên thành công!");
                getData(); // Cập nhật lại dữ liệu sau khi xóa
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa sinh viên: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
    }

