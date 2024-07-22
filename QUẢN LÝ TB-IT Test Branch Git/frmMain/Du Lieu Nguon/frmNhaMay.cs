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

namespace frmMain.Du_Lieu_Nguon
{
    public partial class frmNhaMay : DevExpress.XtraEditors.XtraForm
    {

        public frmNhaMay()
        {
            InitializeComponent();
            LoadControl();
        }
        bool them;

        private void LoadControl()
        {
            LoadData();
            LockControl(true);
            CleanText();
        }

        void CleanText()
        {
            txtMaNhaMay.Clear();
            txtTenNhaMay.Clear();
            txtDiaChi.Clear();         
        }
        private void LockControl(bool kt)
        {
            if (kt)
            {
                txtMaNhaMay.Enabled = false;
                txtTenNhaMay.Enabled = false;
                txtDiaChi.Enabled = false;                
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;

            }
            else
            {
                txtMaNhaMay.Enabled = true;
                txtTenNhaMay.Enabled = true;
                txtDiaChi.Enabled = true;                         
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;
            }
        }

        private void LoadData()
        {
            gridControl1.DataSource = NHAMAYDAO.Instance.GetTable();
        }

        void Save()
        {
            if (them)
            {
                string maNM = txtMaNhaMay.Text.Trim();
                string tenNM = txtTenNhaMay.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();
                bool CheckExits = NHAMAYDAO.Instance.CheckExist(maNM);              
                if (CheckExits)
                {
                    MessageBox.Show($" Mã nhà máy {maNM} đã tồn tại", "Lỗi:", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult kq = MessageBox.Show($" Bạn muốn thêm mã nhà máy {maNM} ?", " Thông báo: ", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {
                        NHAMAYDAO.Instance.Insert(maNM, tenNM, diaChi);
                        MessageBox.Show($"Đã thêm mã nhà máy {maNM}","THÀNH CÔNG:");
                    }
                }
                them = false;

            }
            else
            {
                string maNM = txtMaNhaMay.Text.Trim();
                string tenNM = txtTenNhaMay.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();
                DialogResult kq = MessageBox.Show($" Bạn muốn sửa thông tin mã nhà máy {maNM} ?", " Thông Báo: ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    NHAMAYDAO.Instance.Update(maNM, tenNM, diaChi);
                    MessageBox.Show($"Đã sửa mã nhà máy {maNM}", "THÀNH CÔNG:");
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LockControl(false);
            them = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaNhaMay.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Save();
            LoadControl();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaNhaMay.Enabled = false;
         
              string maNM = txtMaNhaMay.Text.Trim();
            if (maNM == "")
            {
                MessageBox.Show(" Bạn chưa chọn mã nhà máy để xóa! ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa mã nhà máy {maNM} ?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    NHAMAYDAO.Instance.Delete(maNM);
                    MessageBox.Show($"Xóa thành công mã nhà máy:{maNM}", "Thông Báo:");
                }
            }
            LoadControl();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                txtMaNhaMay.Text = gridView1.GetFocusedRowCellValue("MANHAMAY").ToString();
                txtTenNhaMay.Text = gridView1.GetFocusedRowCellValue("TENNHAMAY").ToString();
                txtDiaChi.Text = gridView1.GetFocusedRowCellValue("DIACHI").ToString();
            }
            catch
            {

            }
                 
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }
    }
}