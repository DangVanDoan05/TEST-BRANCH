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
using QuanLyThietBiIT.Common;
using QuanLyThietBiIT.DAOIT;
using QuanLyThietBiIT.DTOIT;
using QuanLyThietBiIT.GridViewEdit;

namespace QuanLyThietBiIT.GUI_QuanLyUser
{
    public partial class frmQuanLyLoaiTaiKhoan : DevExpress.XtraEditors.XtraForm
    {
        public frmQuanLyLoaiTaiKhoan()
        {
            InitializeComponent();
            LoadControl();
        }
        bool them;
        private void LoadControl()
        {
            LockControl(true);
            LoadData();
            CleanText();
        }

        private void LoadData()
        {
            gridControl1.DataSource = LoaiTKDAO.Instance.GetTable();
        }

        private void LockControl(bool kt)
        {
            if (kt == true)
            {
                txtMaLoaiTK.Enabled = false;
                txtTenLoaiTk.Enabled = false;
                txtGhiChu.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
                btnHuy.Enabled = false;
            }
            else
            {
                txtMaLoaiTK.Enabled = true;
                txtTenLoaiTk.Enabled = true;
                txtGhiChu.Enabled = true;
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = false;
                btnHuy.Enabled = true;
            }
        }
        private void CleanText()
        {
            txtMaLoaiTK.Clear();
            txtTenLoaiTk.Clear();
            txtGhiChu.Clear();
        }
        void Save()
        {
            if (them)
            {
                string MaloaiTk = txtMaLoaiTK.Text.Trim();
                string TenloaiTk = txtTenLoaiTk.Text.Trim();
               
              
                string Ghichu = txtGhiChu.Text.Trim();
                int socot = LoaiTKDAO.Instance.CheckMaLoaiTK(MaloaiTk);
                if (socot ==1 )
                {
                    MessageBox.Show(" Mã loại TK đã tồn tại ", " Thông Báo");
                }
                else
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn thêm Mã loại TK : {MaloaiTk}", "Thông Báo", MessageBoxButtons.YesNo);
                    if (kq == DialogResult.Yes)

                    {
                        LoaiTKDAO.Instance.Insert(MaloaiTk, TenloaiTk, Ghichu);
                        MessageBox.Show($"Thêm thành công  Mã Loại tài khoản :{MaloaiTk} ", "Thông Báo");
                        them = false;
                        LoadControl();
                    }
                }
            }
            else
            {
                string MaloaiTk = txtMaLoaiTK.Text.Trim();
                string TenloaiTk = txtTenLoaiTk.Text.Trim();
            
                string Ghichu = txtGhiChu.Text.Trim();
                if (MaloaiTk == "")
                {
                    MessageBox.Show($"Bạn chưa chọn mã loại tài khoản để sửa thông tin. ", "Thông Báo");
                }
                else
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn sửa thông tin mã loại máy tính : {MaloaiTk}", "Thông Báo", MessageBoxButtons.YesNo);
                    if (kq == DialogResult.Yes)

                    {
                        LoaiTKDAO.Instance.Update(MaloaiTk, TenloaiTk, Ghichu);
                        MessageBox.Show($"Sửa thành công thông tin loại máy tính :{MaloaiTk} ", "Thông Báo");
                        LoadControl();

                    }
                }

            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            txtMaLoaiTK.Text = gridView1.GetFocusedRowCellValue("MALOAITK").ToString();
            txtTenLoaiTk.Text = gridView1.GetFocusedRowCellValue("TENLOAITK").ToString();
          
            txtGhiChu.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();
           
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            LockControl(false);
            them = true;
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaLoaiTK.Enabled = false;
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaLoaiTK.Enabled = false;
            string MaloaiTk = txtMaLoaiTK.Text.Trim();
            string TenloaiTk = txtTenLoaiTk.Text.Trim();

            string Ghichu = txtGhiChu.Text.Trim();
            if (MaloaiTk == "")
            {
                MessageBox.Show($" Bạn chưa chọn Mã Loại tài khoản để xóa ");
                LoadControl();
            }

            else
            {

                DialogResult kq = MessageBox.Show($"Bạn muốn xóa mã loại tài khoản: {MaloaiTk}", "Thông Báo", MessageBoxButtons.YesNo);
                if (kq == DialogResult.Yes)
                {
                    LoaiTKDAO.Instance.Delete(MaloaiTk);
                    MessageBox.Show($" Xóa thành công mã loại tài khoản : {MaloaiTk}", " Thông Báo");
                }
                LoadControl();

            }
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            Save();
            LoadControl();
        }

        private void btnCapNhat_Click_1(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            LoadControl();
        }
    }
}