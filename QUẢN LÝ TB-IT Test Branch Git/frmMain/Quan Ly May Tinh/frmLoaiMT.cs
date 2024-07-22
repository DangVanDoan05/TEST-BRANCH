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

namespace frmMain.Quan_Ly_May_Tinh
{
    public partial class frmLoaiMT : DevExpress.XtraEditors.XtraForm
    {
        public frmLoaiMT()
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
            gridControl1.DataSource = LoaiMayTinhDAO.Instance.GetTable();
        }

        private void LockControl(bool kt)
        {
            if (kt == true)
            {
                txtLoaiMT.Enabled = false;
                txtGhichu.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
            }


            else
            {
                txtLoaiMT.Enabled = true;
                txtGhichu.Enabled = true;
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;

            }
        }
        void CleanText()
        {
            txtLoaiMT.Clear();
            txtGhichu.Clear();
        }
        void Save()
        {
            if (them)
            {
                string LoaiMT = txtLoaiMT.Text.Trim();
                string Ghichu = txtGhichu.Text.Trim();
                int socot = LoaiMayTinhDAO.Instance.CheckLoai(LoaiMT);
                if (socot > 0)
                {
                    MessageBox.Show(" Loại máy tính đã tồn tại ", " Thông Báo");
                }
                else
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn thêm loại máy tính : {LoaiMT}", "Thông Báo", MessageBoxButtons.YesNo);
                    if (kq == DialogResult.Yes)

                    {
                        LoaiMayTinhDAO.Instance.Insert(LoaiMT, Ghichu);
                        MessageBox.Show($"Thêm thành công Loại máy tính :{LoaiMT} ", "Thông Báo");
                        them = false;
                        LoadControl();
                    }
                }
            }
            else
            {
                string LoaiMT = txtLoaiMT.Text.Trim();
                string Ghichu = txtGhichu.Text.Trim();

                DialogResult kq = MessageBox.Show($"Bạn muốn sửa thông tin Loại máy tính : {LoaiMT}", "Thông Báo", MessageBoxButtons.YesNo);
                if (kq == DialogResult.Yes)

                {
                    LoaiMayTinhDAO.Instance.Update(LoaiMT, Ghichu);
                    MessageBox.Show($"Sửa thành công thông tin loại máy tính :{LoaiMT} ", "Thông Báo");

                    LoadControl();
                }
            }
        }


        private void gridControl1_Click(object sender, EventArgs e)
        {
            txtLoaiMT.Text = gridView1.GetFocusedRowCellValue("TENLOAIMT").ToString();
            txtGhichu.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();

        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            LockControl(false);
            them = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtLoaiMT.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtLoaiMT.Enabled = false;
            string loaiMT = txtLoaiMT.Text.Trim();
            string ghichu = txtGhichu.Text.Trim();
            if (loaiMT == "")
            {
                MessageBox.Show($" Bạn chưa chọn Loại máy tính để xóa ");
                LoadControl();
            }

            else
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa loại máy tính : {loaiMT}", "Thông Báo", MessageBoxButtons.YesNo);
                if (kq == DialogResult.Yes)
                {
                    LoaiMayTinhDAO.Instance.Delete(loaiMT);
                    MessageBox.Show($" Xóa thành công loại máy tính: {loaiMT}", " Thông Báo");
                }
                LoadControl();
            }
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
    }
}