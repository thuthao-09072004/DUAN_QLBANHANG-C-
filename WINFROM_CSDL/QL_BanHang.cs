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
    public partial class QL_BanHang : Form
    {
        public QL_BanHang()
        {
            InitializeComponent();
        }
        MySqlConnection conn = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=quanlybanhang_csdl;Uid=root;Pwd=;");


        private void QL_BanHang_Load(object sender, EventArgs e)
        {
            // Thiết lập các cột cho DataGridView
            dgv_BH.ColumnCount = 7;
            dgv_BH.Columns[0].Name = "STT";
            dgv_BH.Columns[1].Name = "MaHang";
            dgv_BH.Columns[2].Name = "TenHang";
            dgv_BH.Columns[3].Name = "SoLuong";
            dgv_BH.Columns[4].Name = "DonGia";
            dgv_BH.Columns[5].Name = "DVT";
            dgv_BH.Columns[6].Name = "ThanhTien";

            LoadComboBoxData();
            LayMaHoaDonMoi();
            txtTienThua.ReadOnly = true;
            dgv_BH.AllowUserToDeleteRows = true;


        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fromKhachHang f1 = new fromKhachHang();
            f1.ShowDialog();
        }
        private void LoadComboBoxData()
        {
            try
            {
                conn.Open();

                // Load dữ liệu khách hàng
                MySqlDataAdapter daKhachHang = new MySqlDataAdapter("SELECT * FROM khach_hang", conn);
                DataTable dtKhachHang = new DataTable();
                daKhachHang.Fill(dtKhachHang);
                comboBoxKH.DataSource = dtKhachHang;
                comboBoxKH.DisplayMember = "ten_khach_hang";
                comboBoxKH.ValueMember = "ma_khach_hang";

                // Load dữ liệu nhân viên
                MySqlDataAdapter daNhanVien = new MySqlDataAdapter("SELECT * FROM nhan_vien", conn);
                DataTable dtNhanVien = new DataTable();
                daNhanVien.Fill(dtNhanVien);
                comboBoxNV.DataSource = dtNhanVien;
                comboBoxNV.DisplayMember = "ten_nhan_vien";
                comboBoxNV.ValueMember = "ma_nhan_vien";

                // Load dữ liệu hàng hóa
                MySqlDataAdapter daHangHoa = new MySqlDataAdapter("SELECT * FROM hang_hoa", conn);
                DataTable dtHangHoa = new DataTable();
                daHangHoa.Fill(dtHangHoa);
                comboBoxTH.DataSource = dtHangHoa;
                comboBoxTH.DisplayMember = "ten_hang";
                comboBoxTH.ValueMember = "ma_hang";

                txtDG.DataBindings.Add("Text", dtHangHoa, "don_gia");
                txtDVT.DataBindings.Add("Text", dtHangHoa, "don_vi_tinh");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private void LayMaHoaDonMoi()
        {
            try
            {
                conn.Open();
                // Lấy mã hóa đơn lớn nhất hiện tại
                MySqlCommand cmd = new MySqlCommand("SELECT MAX(ma_hoa_don) FROM hoa_don", conn);
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    string maHoaDonCu = result.ToString();
                    int soThuTu = int.Parse(maHoaDonCu.Substring(2)); // Lấy số thứ tự từ mã hóa đơn cũ
                    string maHoaDonMoi = "HD" + (soThuTu + 1).ToString().PadLeft(2, '0'); // Tạo mã hóa đơn mới với định dạng HD01
                    txtMHD.Text = maHoaDonMoi;
                }
                else
                {
                    txtMHD.Text = "HD01"; // Trường hợp không có hóa đơn nào
                }

                txtMHD.ReadOnly = true; // Đảm bảo không chỉnh sửa mã hóa đơn bằng tay
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy mã hóa đơn mới: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fromNhanVien f2 = new fromNhanVien();
            f2.ShowDialog();
        }

        private void hàngHóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fromHanghoa f3 = new fromHanghoa();
            f3.ShowDialog();

        }

        private void btnThemhang_Click(object sender, EventArgs e)
        {
            if (comboBoxTH.SelectedValue != null && int.TryParse(numericUpDownSL.Text, out int soLuong))
            {
                string maHang = comboBoxTH.SelectedValue.ToString();
                string tenHang = comboBoxTH.Text;
                decimal donGia = decimal.Parse(txtDG.Text);
                string dvt = txtDVT.Text;
                decimal thanhTien = donGia * soLuong;

                // Kiểm tra mã hàng và số lượng
                if (!string.IsNullOrEmpty(maHang) && !string.IsNullOrEmpty(tenHang) && soLuong > 0)
                {
                    // Lấy số STT hiện tại dựa trên số hàng có trong DataGridView
                    int stt = dgv_BH.Rows.Count + 1;

                    // Thêm hàng vào DataGridView
                    dgv_BH.Rows.Add(stt, maHang, tenHang, soLuong, donGia, dvt, thanhTien);
                    TinhTongTien();
                }
                else
                {
                    // Hiển thị thông báo nếu mã hàng hoặc thông tin không hợp lệ
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin mặt hàng.");
                }
            }
            else
            {
                // Thông báo nếu số lượng không hợp lệ
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ.");
            }
        }

        private void btnbothang_Click(object sender, EventArgs e)
        {
            if (dgv_BH.SelectedRows.Count > 0) // Kiểm tra xem người dùng đã chọn hàng hay chưa
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa hàng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        // Xóa hàng đã chọn khỏi DataGridView
                        foreach (DataGridViewRow row in dgv_BH.SelectedRows)
                        {
                            // Nếu bạn cần xóa hàng từ cơ sở dữ liệu, thực hiện câu lệnh DELETE tại đây
                            string maHang = row.Cells["MaHang"].Value.ToString();
                            string query = "DELETE FROM chi_tiet_hoa_don WHERE ma_hoa_don = @maHoaDon AND ma_hang = @maHang";

                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@maHoaDon", txtMHD.Text);
                            cmd.Parameters.AddWithValue("@maHang", maHang);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            // Sau khi xóa khỏi CSDL, xóa khỏi DataGridView
                            dgv_BH.Rows.Remove(row);
                        }

                        // Cập nhật lại STT sau khi xóa
                        for (int i = 0; i < dgv_BH.Rows.Count; i++)
                        {
                            dgv_BH.Rows[i].Cells["STT"].Value = i + 1; // Cập nhật STT từ 1 trở đi
                        }

                        MessageBox.Show("Đã xóa hàng thành công.");
                        TinhTongTien();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa hàng: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng để xóa.");
            }
        }
        private void TinhTongTien()
        {
            decimal tongTien = 0;
            foreach (DataGridViewRow row in dgv_BH.Rows)
            {
                tongTien += Convert.ToDecimal(row.Cells["ThanhTien"].Value);
            }
            txtTT.Text = tongTien.ToString();
        }

        private void btnTT_HD_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtkhachdua.Text, out decimal khachDua) && decimal.TryParse(txtTT.Text, out decimal tongTien))
            {
                if (khachDua >= tongTien)
                {
                    decimal tienThua = khachDua - tongTien;
                    txtTienThua.Text = tienThua.ToString();

                    try
                    {
                        conn.Open();

                        // Thêm thông tin hóa đơn vào bảng hoa_don
                        MySqlCommand cmdHoaDon = new MySqlCommand("INSERT INTO hoa_don (ma_hoa_don, ma_khach_hang, ma_nhan_vien, ngay_lap_hoa_don) VALUES (@maHoaDon, @maKhachHang, @maNhanVien, @ngayLap)", conn);
                        cmdHoaDon.Parameters.AddWithValue("@maHoaDon", txtMHD.Text);
                        cmdHoaDon.Parameters.AddWithValue("@maKhachHang", comboBoxKH.SelectedValue);
                        cmdHoaDon.Parameters.AddWithValue("@maNhanVien", comboBoxNV.SelectedValue);
                        cmdHoaDon.Parameters.AddWithValue("@ngayLap", DateTime.Now);
                        cmdHoaDon.ExecuteNonQuery();

                        // Duyệt qua các dòng trong DataGridView và thêm vào chi_tiet_hoa_don
                        foreach (DataGridViewRow row in dgv_BH.Rows)
                        {
                            if (row.Cells["MaHang"].Value != null && row.Cells["SoLuong"].Value != null && !row.IsNewRow)
                            {
                                string maHang = row.Cells["MaHang"].Value.ToString();
                                int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);

                                if (!string.IsNullOrEmpty(maHang) && soLuong > 0)
                                {
                                    MySqlCommand cmdChiTiet = new MySqlCommand("INSERT INTO chi_tiet_hoa_don (ma_hoa_don, ma_hang, so_luong, don_gia) VALUES (@maHoaDon, @maHang, @soLuong, @donGia)", conn);
                                    cmdChiTiet.Parameters.AddWithValue("@maHoaDon", txtMHD.Text);
                                    cmdChiTiet.Parameters.AddWithValue("@maHang", maHang);
                                    cmdChiTiet.Parameters.AddWithValue("@soLuong", soLuong);
                                    cmdChiTiet.Parameters.AddWithValue("@donGia", row.Cells["DonGia"].Value);
                                    cmdChiTiet.ExecuteNonQuery();
                                }
                                else
                                {
                                    MessageBox.Show($"Lỗi: Dòng {row.Index + 1} có mã hàng hoặc số lượng không hợp lệ.");
                                    return; // Dừng quá trình nếu phát hiện lỗi
                                }
                            }
                        }

                        MessageBox.Show("Thanh toán thành công!");

                        // Đảm bảo rằng kết nối được đóng trước khi gọi lại LayMaHoaDonMoi
                        conn.Close();

                        // Xóa dữ liệu trong DataGridView sau khi thanh toán
                        dgv_BH.Rows.Clear();
                        TinhTongTien();

                        // Gọi phương thức để lấy mã hóa đơn mới
                        LayMaHoaDonMoi();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi thanh toán: " + ex.Message);
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Số tiền khách đưa không đủ!");
                }
            }

        }




        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtkhachdua_TextChanged(object sender, EventArgs e)
        {
            decimal tongTien, khachDua;

            // Đảm bảo giá trị tổng tiền luôn được lấy
            if (decimal.TryParse(txtTT.Text, out tongTien) && decimal.TryParse(txtkhachdua.Text, out khachDua))
            {
                decimal tienThua = khachDua - tongTien;
                txtTienThua.Text = tienThua >= 0 ? tienThua.ToString() : "0";
            }
        }
    }
}
