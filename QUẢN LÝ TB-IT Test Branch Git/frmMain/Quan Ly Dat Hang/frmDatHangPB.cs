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
using DAO;
using DTO;

namespace frmMain
{
    public partial class frmDatHangPB : DevExpress.XtraEditors.XtraForm
    {

        public frmDatHangPB()
        {
            InitializeComponent();
            LoadControl();
        }

        private void LoadControl()
        {
            sglMaPB.Properties.DataSource = PhongBanDAO.Instance.GetLsvPB();
            sglMaPB.Properties.DisplayMember = "MAPB";  
            sglMaPB.Properties.ValueMember = "MAPB";
            txtNhaMay.Enabled = false;
        }

        private void searchLookUpEdit1View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }
      
        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string PBDH = sglMaPB.EditValue.ToString();
                string NhaMay = txtNhaMay.Text;
                string ngaydh = dtpNgayDatHang.Value.ToString("dd/MM/yyyy");
                string TgDH = dtpNgayDatHang.Value.ToString("-ddMMyyyy-") + DateTime.Now.ToString("HHmmss");
                string MaDonHang = PBDH + TgDH;
                string TenHang = txtTenMH.Text;
                string sldat = txtSLDat.Text;
                string DvTinh = txtDonViTinh.Text;
                string mdsd = txtMDSD.Text;

                if (TenHang == "" || sldat == "" || DvTinh == "" || mdsd == "" || PBDH == "")
                {
                    MessageBox.Show($"Chưa chọn phòng ban hoặc chưa nhập đầy đủ thông tin cần thiết.", "Lỗi: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int SolgDat = int.Parse(sldat);
                    QlyDonHangPBDAO.Instance.Insert(MaDonHang, PBDH, ngaydh, TenHang, SolgDat, DvTinh,NhaMay,mdsd, "", 0, "");
                    MessageBox.Show("Cập nhật thành công!", "Thông báo:");
                }
                this.Close();
            }
            catch
            {
                MessageBox.Show($"Chưa chọn phòng ban đặt hàng.", "Lỗi: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void searchLookUpEdit1View_Click(object sender, EventArgs e)
        {
            txtNhaMay.Text=searchLookUpEdit1View.GetFocusedRowCellValue("NHAMAY").ToString();
        }
    }
}