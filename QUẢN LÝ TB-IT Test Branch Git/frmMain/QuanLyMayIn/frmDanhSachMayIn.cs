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

namespace QuanLyThietBiIT.QuanLyMayIn
{
    public partial class frmDanhSachMayIn : DevExpress.XtraEditors.XtraForm
    {
        public frmDanhSachMayIn()
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
            LoadCBX();
        }

        private void LoadCBX()
        {
            List<PhongBanDTO> listPB = PhongBanDAO.Instance.GetLsvPB();
            cbPhongBan.DataSource = listPB;
            cbPhongBan.DisplayMember = "MAPB";
            cbPhongBan.ValueMember = "MAPB";
           
        }

        private void CleanText()
        {
            txtMamayin.Clear();
            txtTenMin.Clear();
            txtMAC.Clear();
            txtIP.Clear();
            txtGhiChu.Clear();

        }

        private void LoadData()
        {

            gridControl1.DataSource = DanhSachMayInDAO.Instance.GetTable();
        }

        private void LockControl(bool kt)
        {
            if (kt)
            {
                txtMamayin.Enabled = false;
                txtTenMin.Enabled = false;
                txtMAC.Enabled = false;
                txtIP.Enabled = false;
                cbPhongBan.Enabled = false;
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

                txtMamayin.Enabled = true;
                txtTenMin.Enabled = true;
                txtMAC.Enabled =true;
                txtIP.Enabled = true;
                cbPhongBan.Enabled = true;
                txtGhiChu.Enabled = true;
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = false;
                btnHuy.Enabled = true;
               
            }
        }


        void Save()
        {
            if (them)
            {
                string maMI = txtMamayin.Text.Trim();
                string tenMI = txtTenMin.Text.Trim();
                string ip = txtIP.Text.Trim();
                string mac = txtMAC.Text.Trim();
                string phongban = cbPhongBan.SelectedValue.ToString();
                string ngaymua = dtpNgayMua.Value.ToString("dd/MM/yyyy");
                string hanBH = dtpNgayMua.Value.ToString("dd/MM/yyyy");
                string ghichu = txtGhiChu.Text.Trim();

                int socot = DanhSachMayInDAO.Instance.CheckMaMI(maMI);
                if (socot > 0)
                {
                    MessageBox.Show(" Mã máy in đã tồn tại!", "Thông Báo");
                }
                else
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn thêm Mã máy in {maMI}", "Thông Báo:", MessageBoxButtons.YesNo);
                    if (kq == DialogResult.Yes)
                    {
                        DanhSachMayInDAO.Instance.Insert(maMI, tenMI, phongban, ip, mac, ngaymua, hanBH, ghichu);
                        MessageBox.Show($" Thêm mã máy in {maMI} thành công! ");
                    }
                    them = false;
                    LoadControl();

                }
            }
            else
            { // UPDATE THONG TIN
                string maMI = txtMamayin.Text.Trim();
                string tenMI = txtTenMin.Text.Trim();
                string ip = txtIP.Text.Trim();
                string mac = txtMAC.Text.Trim();
                string phongban = cbPhongBan.SelectedValue.ToString();
                string ngaymua = dtpNgayMua.Value.ToString("dd/MM/yyyy");
                string hanBH = dtpNgayMua.Value.ToString("dd/MM/yyyy");
                string ghichu = txtGhiChu.Text.Trim();
                if (maMI == "")
                {
                    MessageBox.Show($"Bạn chưa chọn máy in  ","Thông báo:");
                }
                else
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn sửa thông tin của mã máy in {maMI}", "Thông Báo:", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {
                        DanhSachMayInDAO.Instance.Update(maMI, tenMI, phongban, ip, mac, ngaymua, hanBH, ghichu);
                        MessageBox.Show($" Sửa thông tin mã máy in {maMI} thành công! ");
                    }
                }



            }
        }

       




        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }



        // nut them
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            LockControl(false);
            them = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtMamayin.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtMamayin.Enabled = false;
            string ma = txtMamayin.Text.Trim();
            if (ma == "")
            {
                MessageBox.Show(" Bạn chưa chọn mã máy in để xóa! ", " Thông Báo");
            }
            else
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa mã máy in {ma} ?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    // cho phép xóa nhiều dòng trong gridview
                    foreach (var item in gridView1.GetSelectedRows())
                    {
                        string ma1 = gridView1.GetRowCellValue(item, "MAMI").ToString();
                        DanhSachMayInDAO.Instance.Delete(ma);
                        MessageBox.Show($"Xóa thành công mã máy in : {ma}", "Thông Báo ");
                    }
                    //MaLinhKienDAO.Instance.DeleteTable(ma);
                    //TonLinhKienDAO.Instance.Delete(ma);
                    //MessageBox.Show($"Xóa thành công mã linh kiện: {ma}", "Thông Báo ");
                }
            }
            LoadControl();
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void gridControl1_Click_1(object sender, EventArgs e)
        {
            txtMamayin.Text = gridView1.GetFocusedRowCellValue("MAMAYIN").ToString();
            txtTenMin.Text = gridView1.GetFocusedRowCellValue("TENMAYIN").ToString();
            txtIP.Text = gridView1.GetFocusedRowCellValue("IP").ToString();
            txtMAC.Text = gridView1.GetFocusedRowCellValue("MAC").ToString();
            dtpNgayMua.Value = Convert.ToDateTime(gridView1.GetFocusedRowCellValue("NGAYMUA").ToString());
            dtpHanSuDung.Value = Convert.ToDateTime(gridView1.GetFocusedRowCellValue("HANBH").ToString());
            txtGhiChu.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();
        }
    }
}