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
using MySql.Data.MySqlClient;

namespace WINFROM_CSDL
{
    public partial class BANGSINHVIEN : Form
    {
        public BANGSINHVIEN()
        {
            InitializeComponent();
        }
        MySqlConnection conn = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=csdl_qlhs;Uid=root;Pwd=;");

        private void BANGSINHVIEN_Load(object sender, EventArgs e)
        {
            getDataComboxkhoa();
            getDataComboxNganh();
            getDataComboxNH();
            getDataComboxlop();
            getDataSV();
            bingding();

            comboBoxKhoa.SelectedIndexChanged += comboBoxKhoa_SelectedIndexChanged;
            comboBoxNganh.SelectedIndexChanged += comboBoxNganh_SelectedIndexChanged;
            comboBoxNamHoc.SelectedIndexChanged += comboBoxNamHoc_SelectedIndexChanged;
            comboBoxLOP.SelectedIndexChanged += comboBoxLOP_SelectedIndexChanged;
        }
        
        private void getDataComboxkhoa()
        {
            MySqlDataAdapter sa = new MySqlDataAdapter("select * from khoa", conn);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            comboBoxKhoa.DataSource = dt;
            comboBoxKhoa.DisplayMember = "ten_khoa";
            comboBoxKhoa.ValueMember = "ma_khoa";
        }
        private void getDataComboxNganh()
        {
            if (comboBoxKhoa.SelectedValue != null)
            {
                string ma_khoa = comboBoxKhoa.SelectedValue.ToString();
                MySqlDataAdapter sa = new MySqlDataAdapter($"SELECT * FROM nganh WHERE ma_khoa = '{ma_khoa}'", conn);
                DataTable dt = new DataTable();
                sa.Fill(dt);
                comboBoxNganh.DataSource = dt;
                comboBoxNganh.DisplayMember = "ten_nganh";
                comboBoxNganh.ValueMember = "ma_nganh";
            }
        }
        
        private void getDataComboxNH()
        {
            MySqlDataAdapter sa = new MySqlDataAdapter("select * from namhoc", conn);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            comboBoxNamHoc.DataSource = dt;
            comboBoxNamHoc.DisplayMember = "ten_nam_hoc";
            comboBoxNamHoc.ValueMember = "ma_nam_hoc";
        }
        private void getDataComboxlop()
        {
            if (comboBoxNganh.SelectedValue != null && comboBoxNamHoc.SelectedValue != null)
            {
                string ma_nganh = comboBoxNganh.SelectedValue.ToString();
                string ma_nam_hoc = comboBoxNamHoc.SelectedValue.ToString();
                MySqlDataAdapter sa = new MySqlDataAdapter($"SELECT * FROM lop WHERE ma_nganh = '{ma_nganh}' AND ma_nam_hoc = '{ma_nam_hoc}'", conn);
                DataTable dt = new DataTable();
                sa.Fill(dt);
                comboBoxLOP.DataSource = dt;
                comboBoxLOP.DisplayMember = "ten_lop";
                comboBoxLOP.ValueMember = "ma_lop";
            }
        }
        private void getDataSV()
        {
            if (comboBoxLOP.SelectedValue != null)
            {
                string ma_lop = comboBoxLOP.SelectedValue.ToString();
                try
                {
                    conn.Open();
                    MySqlDataAdapter sa = new MySqlDataAdapter($"SELECT * FROM sinhvien WHERE ma_lop = '{ma_lop}'", conn);
                    DataTable dt = new DataTable();
                    sa.Fill(dt);
                    dgv_SinhVien.DataSource = dt;
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
        }

        private void bingding()
        {
            txtMaSV.DataBindings.Clear();
            txtMaSV.DataBindings.Add("Text", dgv_SinhVien.DataSource, "ma_sinh_vien");
            txtTenSV.DataBindings.Clear();
            txtTenSV.DataBindings.Add("Text", dgv_SinhVien.DataSource, "ten_sinh_vien");
            dateTimeSV.DataBindings.Clear();
            dateTimeSV.DataBindings.Add("Text", dgv_SinhVien.DataSource, "ngay_sinh");
            txtQue.DataBindings.Clear();
            txtQue.DataBindings.Add("Text", dgv_SinhVien.DataSource, "dia_chi");
            txtSDTSV.DataBindings.Clear();
            txtSDTSV.DataBindings.Add("Text", dgv_SinhVien.DataSource, "sdt");
        }

        private void comboBoxKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            getDataComboxNganh();
        }

        private void comboBoxNganh_SelectedIndexChanged(object sender, EventArgs e)
        {
            getDataComboxlop();
        }

        private void comboBoxNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            getDataComboxlop();
            if (comboBoxNamHoc.SelectedValue != null)
            {
                // Khi chọn năm học, lấy mã năm học và tên năm học từ ComboBox
                string ma_nam_hoc = comboBoxNamHoc.SelectedValue.ToString();
                txtManamhoc.Text = ma_nam_hoc;

                // Lấy tên năm học tương ứng
                DataRowView drv = comboBoxNamHoc.SelectedItem as DataRowView;
                if (drv != null)
                {
                    txtTenNH.Text = drv["ten_nam_hoc"].ToString();
                }
            }
        }

        private void comboBoxLOP_SelectedIndexChanged(object sender, EventArgs e)
        {
            getDataSV();
            if (comboBoxLOP.SelectedValue != null)
            {
                // Khi chọn lớp, lấy mã lớp và tên lớp từ ComboBox
                string ma_lop = comboBoxLOP.SelectedValue.ToString();
                txtMalop.Text = ma_lop;

                // Lấy tên lớp tương ứng
                DataRowView drv = comboBoxLOP.SelectedItem as DataRowView;
                if (drv != null)
                {
                    txtTenLop.Text = drv["ten_lop"].ToString();
                }
            }
        }
        private void themNH()
        {
            try
            {
                string query = "INSERT INTO namhoc (ma_nam_hoc, ten_nam_hoc) " +
                               "VALUES ('" + txtManamhoc.Text + "', N'" + txtTenNH.Text + "')";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm năm học thành công!");
                getDataComboxNH();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void suaNH()
        {
            try
            {
                string query = "UPDATE namhoc SET ten_nam_hoc = N'" + txtTenNH.Text + "' " + "WHERE ma_nam_hoc = '" + txtManamhoc.Text + "'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật năm học thành công!");
                getDataComboxNH() ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dữ liệu suaNH: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private void xoaNH()
        {
            try
            {
                string query = "DELETE FROM namhoc WHERE ma_nam_hoc = '" + txtManamhoc.Text + "'";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa năm học thành công!");
                getDataComboxNH();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu xoaNH: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnThemNamhoc_Click(object sender, EventArgs e)
        {
            themNH();
            getDataComboxNH();
        }

        private void btnSuaNH_Click(object sender, EventArgs e)
        {
            suaNH();
            getDataComboxNH();

        }

        private void btnxoaNH_Click(object sender, EventArgs e)
        {
            xoaNH();
            getDataComboxNH();
        }
        private void them()
        {
            try
            {
                string ngaySinh = DateTime.Parse(dateTimeSV.Text).ToString("yyyy-MM-dd");
                string query = "INSERT INTO sinhvien (ma_sinh_vien, ten_sinh_vien, ngay_sinh, dia_chi, sdt, ma_lop) " +
                               "VALUES ('" + txtMaSV.Text + "', N'" + txtTenSV.Text + "', '" + ngaySinh + "', N'" + txtQue.Text + "', '" + txtSDTSV.Text + "', '" + comboBoxLOP.SelectedValue + "')";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu them: " + ex.Message);
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
                string ngaySinh = DateTime.Parse(dateTimeSV.Text).ToString("yyyy-MM-dd");
                string query = "UPDATE sinhvien SET ten_sinh_vien = N'" + txtTenSV.Text + "', " +
                               "ngay_sinh = '" + ngaySinh + "', " +
                               "dia_chi = N'" + txtQue.Text + "', " +
                               "sdt = '" + txtSDTSV.Text + "', " +
                               "ma_lop = '" + comboBoxLOP.SelectedValue + "' " +
                               "WHERE ma_sinh_vien = '" + txtMaSV.Text + "'";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dữ liệu sua: " + ex.Message);
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
                string query = "DELETE FROM sinhvien WHERE ma_sinh_vien = '" + txtMaSV.Text + "'";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu xoa: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void txtManamhoc_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenNH_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMalop_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenLop_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnthemLop_Click(object sender, EventArgs e)
        {
            string ma_lop = txtMalop.Text;
            string ten_lop = txtTenLop.Text;
            string ma_nganh = comboBoxNganh.SelectedValue.ToString();
            string ma_nam_hoc = comboBoxNamHoc.SelectedValue.ToString();

            if (string.IsNullOrWhiteSpace(ma_lop) || string.IsNullOrWhiteSpace(ten_lop))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin lớp.");
                return;
            }

            try
            {
                conn.Open();
                string query = $"INSERT INTO lop (ma_lop, ten_lop, ma_nganh, ma_nam_hoc) VALUES ('{ma_lop}', '{ten_lop}', '{ma_nganh}', '{ma_nam_hoc}')";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm lớp thành công.");
                getDataComboxlop(); // Load lại dữ liệu cho ComboBox Lớp
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm lớp: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnSuaLOP_Click(object sender, EventArgs e)
        {
            string ma_lop = txtMalop.Text;
            string ten_lop = txtTenLop.Text;
            string ma_nganh = comboBoxNganh.SelectedValue.ToString();
            string ma_nam_hoc = comboBoxNamHoc.SelectedValue.ToString();

            if (string.IsNullOrWhiteSpace(ma_lop) || string.IsNullOrWhiteSpace(ten_lop))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin lớp.");
                return;
            }

            try
            {
                conn.Open();
                string query = $"UPDATE lop SET ten_lop = '{ten_lop}', ma_nganh = '{ma_nganh}', ma_nam_hoc = '{ma_nam_hoc}' WHERE ma_lop = '{ma_lop}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Sửa lớp thành công.");
                getDataComboxlop(); // Load lại dữ liệu cho ComboBox Lớp
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa lớp: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnxoaLop_Click(object sender, EventArgs e)
        {
            string ma_lop = txtMalop.Text;

            if (string.IsNullOrWhiteSpace(ma_lop))
            {
                MessageBox.Show("Vui lòng chọn lớp cần xóa.");
                return;
            }

            try
            {
                conn.Open();
                string query = $"DELETE FROM lop WHERE ma_lop = '{ma_lop}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa lớp thành công.");
                getDataComboxlop(); // Load lại dữ liệu cho ComboBox Lớp
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa lớp: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void khoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BAI3 f1 = new BAI3();
            f1.ShowDialog();
        }

        private void nganhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NGANH f2 = new NGANH();
            f2.ShowDialog();
        }

        private void sinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SinhVien f3 = new SinhVien();
            f3.ShowDialog();
        }

        private void btnthemSV_Click(object sender, EventArgs e)
        {
            them();
            getDataSV();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            sua();
            getDataSV();
        }

        private void btnXoaSV_Click(object sender, EventArgs e)
        {
            xoa();
            getDataSV();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }

    
}
