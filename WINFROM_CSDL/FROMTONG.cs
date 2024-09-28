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
    public partial class FROMTONG : Form
    {
        public FROMTONG()
        {
            InitializeComponent();
        }

        private void buttonsv_Click(object sender, EventArgs e)
        {
            BANGSINHVIEN F1 = new BANGSINHVIEN();
            F1.ShowDialog();
        }

        private void btnbh_Click(object sender, EventArgs e)
        {
            QL_BanHang F2 = new QL_BanHang();
            F2.ShowDialog();
        }

        private void buttonthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
