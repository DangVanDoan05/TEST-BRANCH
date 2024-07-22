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
    public partial class frmChucVu : DevExpress.XtraEditors.XtraForm
    {
        public frmChucVu()
        {
            InitializeComponent();
            LoadControl();
        }

        bool them;
        int idquyen = CommonUser.Quyen;
        private void LoadControl()
        {
            LockControl(true);
            LoadData();
            CleanText();
        }

        private void LoadData()
        {
            if(idquyen>=8)
            {
                gridControl1.DataSource = ChucVuDAO.Instance.GetLsvCVDaSX();
            }                       
        }

        private void LockControl(bool kt)
        {
            if (kt == true)
            {
                txtMaCV.Enabled = false;
                txtBacCV.Enabled = false;
                txtTenCV.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
            }
            else
            {
                txtMaCV.Enabled = true;
                txtBacCV.Enabled = true;
                txtTenCV.Enabled = true;
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;

            }
        }
        void CleanText()
        {
            txtMaCV.Clear();          
            txtTenCV.Clear();
            txtBacCV.Clear();
        }
        void Save()
        {
            if (them)
            {
                string maCV = txtMaCV.Text.Trim();             
                string TenCV = txtTenCV.Text.Trim();
                try
                {
                    int BacCV = int.Parse(txtBacCV.Text.Trim());
                    if (maCV != "")
                    {
                        bool checkMaCVexist = ChucVuDAO.Instance.CheckMaCVExist(maCV, BacCV);
                        if (checkMaCVexist)
                        {
                            MessageBox.Show(" Mã chức vụ hoặc bậc chức vụ đã tồn tại ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            DialogResult kq = MessageBox.Show($"Bạn muốn thêm mã chức vụ : {maCV}", "Thông Báo", MessageBoxButtons.YesNo);
                            if (kq == DialogResult.Yes)
                            {
                                ChucVuDAO.Instance.Insert(maCV, TenCV, BacCV);
                                MessageBox.Show($"Đã thêm mã chức vụ : {maCV} ", "THÀNH CÔNG:");
                                them = false;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã chức vụ không được phép để trống ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch 
                {
                    MessageBox.Show("Bậc chức vụ phải là dạng số nguyên ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                          
            }
            else
            {
                string maCV = txtMaCV.Text.Trim();
                int BacCV = int.Parse(txtBacCV.Text.Trim());
                string TenCV = txtTenCV.Text.Trim();
                if (maCV == "")
                {
                    MessageBox.Show(" Bạn chưa chọn mã chức vụ để sửa thông tin ! ", " Thông Báo: ");
                }
                else
                {

                    DialogResult kq = MessageBox.Show($"Bạn muốn sửa thông tin mã chức vụ : {maCV}", "Thông Báo:", MessageBoxButtons.YesNo);
                    if (kq == DialogResult.Yes)
                    {
                        ChucVuDAO.Instance.Update(maCV, TenCV,BacCV);
                        MessageBox.Show($"Sửa thành công thông tin mã chức vụ :{maCV} ", "THÀNH CÔNG:");
                        LoadControl();
                    }
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(idquyen>=8)
            {
                LockControl(false);
                them = true;
            }
            else
            {
                MessageBox.Show("Bạn không đủ thẩm quyền cho chức năng này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (idquyen >= 8)
            {
                LockControl(false);
                txtMaCV.Enabled = false;
            }
            else
            {
                MessageBox.Show("Bạn không đủ thẩm quyền cho chức năng này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                  
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (idquyen >= 8)
            {
                LockControl(false);
                txtMaCV.Enabled = false;

                string maCV = txtMaCV.Text.Trim();
                if (maCV == "")
                {
                    MessageBox.Show(" Bạn chưa chọn mã chức vụ cầu cần xóa! ", " Thông Báo: ");
                }
                else
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn xóa mã chức vụ được chọn. ?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {
                        int dem = 0;
                        // cho phép xóa nhiều dòng trong gridview
                        foreach (var item in gridView1.GetSelectedRows())
                        {
                            string MaCV = gridView1.GetRowCellValue(item, "MACHUCVU").ToString();
                            ChucVuDAO.Instance.Delete(MaCV);
                            dem++;

                        }
                        MessageBox.Show($"Xóa thành công {dem} mã chức vụ được chọn.", "Thông Báo: ");
                    }
                }
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn không đủ thẩm quyền cho chức năng này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (idquyen >= 8)
            {
                Save();
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn không đủ thẩm quyền cho chức năng này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (idquyen >= 8)
            {
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn không đủ thẩm quyền cho chức năng này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

            try
            {
                txtMaCV.Text = gridView1.GetFocusedRowCellValue("MACHUCVU").ToString();            
                txtTenCV.Text = gridView1.GetFocusedRowCellValue("TENCHUCVU").ToString();
                txtBacCV.Text = gridView1.GetFocusedRowCellValue("BACCV").ToString();
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