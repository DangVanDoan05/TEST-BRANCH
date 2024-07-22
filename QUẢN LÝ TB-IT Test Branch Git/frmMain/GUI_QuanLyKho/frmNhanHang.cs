using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QuanLyThietBiIT.DAOIT;
using QuanLyThietBiIT.DTOIT;


namespace QuanLyThietBiIT
{
    public partial class frmNhanHang : DevExpress.XtraEditors.XtraForm
    {
        public frmNhanHang()
        {
            InitializeComponent();
            LoadCBX();
        }

        private void LoadCBX()
        {
            cbMaLK.DataSource = TonLinhKienDAO.Instance.GetListMaLKTon();
            cbMaLK.DisplayMember = "MALK";
            cbMaLK.ValueMember = "MALK";
        }

        private void cbMaLK_SelectedValueChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox cb = sender as System.Windows.Forms.ComboBox;

            cb.ValueMember = "MALK";
            string a = cb.SelectedValue.ToString();


            TonLinhKienDTO malkton = TonLinhKienDAO.Instance.GetMaLKTon(a);
            MaLinhKienDTO malkDTO = MaLinhKienDAO.Instance.GetRowMaLK(a);
            txtTenLK.Text = malkDTO.TENLK;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string a = cbMaLK.SelectedValue.ToString();
            TonLinhKienDTO malkton = TonLinhKienDAO.Instance.GetMaLKTon(a);
            MaLinhKienDTO malkDTO = MaLinhKienDAO.Instance.GetRowMaLK(a);
            txtTenLK.Text = malkDTO.TENLK;
            int ngay = dtpNgaynhanhang.Value.Day;
            int thang = dtpNgaynhanhang.Value.Month;
            int nam = dtpNgaynhanhang.Value.Year;
            int slnhan = int.Parse(txtSLnhan.Text);
            string ngaynhanhang = $"{ngay}/{thang}/{nam}";
            int slsaunhan = malkton.SLTON + slnhan;
            DialogResult kq = MessageBox.Show($"Bạn muốn cập nhật thông tin nhận mã linh kiện {a} ?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                TonLinhKienDAO.Instance.UpdateNHANHANG(a, ngaynhanhang, slsaunhan);
                MessageBox.Show("Cập nhật thông tin nhận hàng thành công!", "Thông Báo:");
            }

            this.Close();
        }
    }
}