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
    public partial class frmNhaCungCap : DevExpress.XtraEditors.XtraForm
    {
        public frmNhaCungCap()
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
            txtMaNcc.Clear();
            txtTenNcc.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();
            txtWebsite.Clear();
        }
        private void LockControl(bool kt)
        {
            if (kt)
            {
                txtMaNcc.Enabled = false;
                txtTenNcc.Enabled = false;
                txtDiaChi.Enabled = false;
                txtDienThoai.Enabled = false;
                txtWebsite.Enabled = false;
                btnThem1.Enabled = true;
                btnSua1.Enabled = true;
                btnXoa1.Enabled = true;
                btnLuu1.Enabled = false;
                btnCapNhat1.Enabled = true;
               
            }
            else
            {
                txtMaNcc.Enabled = true;
                txtTenNcc.Enabled = true;
                txtDiaChi.Enabled = true;
                txtDienThoai.Enabled = true;
                txtWebsite.Enabled = true;
                btnThem1.Enabled = false;
                btnSua1.Enabled = false;
                btnXoa1.Enabled = false;
                btnLuu1.Enabled = true;
                btnCapNhat1.Enabled = true;
               
            }
        }

        private void LoadData()
        {
            gridControl1.DataSource = NhaCungCapDAO.Instance.GetTable();
        }
        void Save()
        {
            if (them)
            {
                string ma = txtMaNcc.Text.Trim();
                string ten = txtTenNcc.Text.Trim();
                string dc = txtDiaChi.Text.Trim();
                string dt = txtDienThoai.Text.Trim();
                string web = txtWebsite.Text.Trim();
                int ktra = NhaCungCapDAO.Instance.CheckMaNCC(ma);
                //  MessageBox.Show($"so cot la: {ktra}");
                if (ktra > 0)
                {
                    MessageBox.Show($" Mã nhà cung cấp {ma} đã tồn tại", "Thông Báo: ");
                }
                else
                {
                    DialogResult kq = MessageBox.Show($" Bạn muốn thêm mã nhà cung cấp {ma} ?", " Thông Báo: ", MessageBoxButtons.YesNo);
                    if (kq == DialogResult.Yes)
                    {
                        NhaCungCapDAO.Instance.InsertTable(ma, ten, dc, dt, web);
                        MessageBox.Show(" Thêm mã nhà cung cấp thành công! ");
                    }
                }
                them = false;

            }
            else
            {
                string ma = txtMaNcc.Text.Trim();
                string ten = txtTenNcc.Text.Trim();
                string dc = txtDiaChi.Text.Trim();
                string dt = txtDienThoai.Text.Trim();
                string web = txtWebsite.Text.Trim();
                DialogResult kq = MessageBox.Show($" Bạn muốn sửa thông tin mã nhà cung cấp {ma} ?", " Thông Báo: ", MessageBoxButtons.YesNo);
                if (kq == DialogResult.Yes)
                {
                    NhaCungCapDAO.Instance.UpdateTable(ma, ten, dc, dt, web);
                    MessageBox.Show(" Sửa mã nhà cung cấp thành công! ");
                }
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            txtMaNcc.Text = gridView1.GetFocusedRowCellValue("MANCC").ToString();
            txtTenNcc.Text = gridView1.GetFocusedRowCellValue("TENNCC").ToString();
            txtDiaChi.Text = gridView1.GetFocusedRowCellValue("DIACHI").ToString();
            txtDienThoai.Text = gridView1.GetFocusedRowCellValue("DIENTHOAI").ToString();
            txtWebsite.Text = gridView1.GetFocusedRowCellValue("WEBSITE").ToString();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnThem1_Click(object sender, EventArgs e)
        {
            LockControl(false);
            them = true;
        }

        private void btnSua1_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaNcc.Enabled = false;
        }

        private void btnXoa1_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaNcc.Enabled = false;
            string ma = txtMaNcc.Text.Trim();
            if (ma == "")
            {
                MessageBox.Show(" Bạn chưa chọn Nhà cung cấp để xóa! ", " Thông Báo");
            }
            else
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa mã nhà cung cấp {ma} ?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    NhaCungCapDAO.Instance.DeleteTable(ma);
                    MessageBox.Show($"Xóa thành công nhà cung cấp mã: {ma}", "Thông Báo ");
                }
            }
            LoadControl();
        }

        private void btnLuu1_Click(object sender, EventArgs e)
        {
            Save();
            LoadControl();
        }

        private void btnCapNhat1_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void btnHuy1_Click(object sender, EventArgs e)
        {
            LoadControl();
        }
    }
}