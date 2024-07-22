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
using QuanLyThietBiIT.Common;

namespace QuanLyThietBiIT
{
    public partial class frmDatHang : DevExpress.XtraEditors.XtraForm
    {
        public frmDatHang()
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
            int ngay = dtpNgayDatHang.Value.Day;
            int thang = dtpNgayDatHang.Value.Month;
            int nam = dtpNgayDatHang.Value.Year;
            int sldat = int.Parse(txtSLdat.Text);
            string ngaydathang = $"{ngay}/{thang}/{nam}";
            DialogResult kq = MessageBox.Show($"Bạn chắc chắn đã đặt hàng  cho mã linh kiện {a} ?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                TonLinhKienDAO.Instance.UpdateDATHANG(a, ngaydathang, sldat);
                MessageBox.Show("Cập nhật thông tin đặt hàng thành công!", "Thông Báo:");
            }

            this.Close();
            
        }

    }
}