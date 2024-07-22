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
    public partial class frmHangMucBaoDuong : DevExpress.XtraEditors.XtraForm
    {
        public frmHangMucBaoDuong()
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
            txtMaHM.Clear();
            txtChiTietHM.Clear();
            txtGhiChu.Clear();
        }

        private void LockControl(bool kt)
        {
            if (kt)
            {
                txtMaHM.Enabled = false;
                txtChiTietHM.Enabled = false;
                txtGhiChu.Enabled = false;

                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;

            }
            else
            {
                txtMaHM.Enabled = true;
                txtChiTietHM.Enabled = true;
                txtGhiChu.Enabled = true;

                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;
            }
        }

        private void LoadData()
        {
            gridControl1.DataSource = HangMucBaoTriDAO.Instance.GetTable();
        }

        void Save()
        {
            if (them)
            {

                string maHM = txtMaHM.Text.Trim();
                string ChiTietHM = txtChiTietHM.Text.Trim();
                string ghichu = txtGhiChu.Text.Trim();
                bool CheckExits = HangMucBaoTriDAO.Instance.CheckMaHMExist(maHM);
                if (CheckExits)
                {
                    MessageBox.Show($" Hạng mục {maHM} đã tồn tại", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {                  
                    HangMucBaoTriDAO.Instance.Insert(maHM, ChiTietHM, ghichu);
                    MessageBox.Show($"Đã thêm hạng mục {maHM}", "Thành công:");                    
                }
                them = false;

            }
            else
            {
                string maHM = txtMaHM.Text.Trim();
                string ChiTietHM = txtChiTietHM.Text.Trim();
                string ghichu = txtGhiChu.Text.Trim();
               
                HangMucBaoTriDAO.Instance.Update(maHM, ChiTietHM, ghichu);
                MessageBox.Show($"Đã sửa hạng mục {maHM}", "Thành công:");
                
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
            txtMaHM.Enabled = false;
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
            txtMaHM.Enabled = false;
         
            int dem = 0;
            List<string> LsMaHMDcChon = new List<string>();
            foreach (var item in gridView1.GetSelectedRows())
            {
                string MaMT = gridView1.GetRowCellValue(item, "MAHM").ToString();
                LsMaHMDcChon.Add(MaMT);
                dem++;
            }
            if (dem > 0)
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} mã hạng mục được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    foreach (string item in LsMaHMDcChon)
                    {
                        HangMucBaoTriDAO.Instance.Delete(item);
                    }
                    MessageBox.Show($"Đã xóa {dem} mã hạng mục được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn hạng mục sửa chữa cần xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadControl();

        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

            try
            {
                txtMaHM.Text = gridView1.GetFocusedRowCellValue("MAHM").ToString();
                txtChiTietHM.Text = gridView1.GetFocusedRowCellValue("CHITIET").ToString();
                txtGhiChu.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();
            }
            catch
            {

            }

        }
    }
}