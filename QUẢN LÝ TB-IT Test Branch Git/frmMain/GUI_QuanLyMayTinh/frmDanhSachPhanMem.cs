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
using QuanLyThietBiIT.GridViewEdit;

namespace QuanLyThietBiIT.GUI_QuanLyMayTinh
{
    public partial class frmDanhSachPhanMem : DevExpress.XtraEditors.XtraForm
    {
        public frmDanhSachPhanMem()
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
            cbNCC.DataSource = NhaCungCapDAO.Instance.GetListNCC();
            cbNCC.DisplayMember = "MANCC";
            cbNCC.ValueMember = "MANCC";
        }

        private void CleanText()
        {
            txtMaPhanMem.Clear();
            txtTenPhanMem.Clear();
            txtLicense.Clear();
            txtGhiChu.Clear();
        }
        
        private void LoadData()
        {

            gridControl1.DataSource = DanhSachPhanMemDAO.Instance.GetTable();
        }

        private void LockControl(bool kt)
        {
            if (kt)
            {
                txtMaPhanMem.Enabled = false;
                txtTenPhanMem.Enabled = false;
                txtLicense.Enabled = false;
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
                txtMaPhanMem.Enabled = true;
                txtTenPhanMem.Enabled = true;
                txtLicense.Enabled = true;
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
            try
            {
                if (them)
                {
                    string maPM = txtMaPhanMem.Text;
                    string tenPM = txtTenPhanMem.Text;
                    string license = txtLicense.Text;
                    string ngaymua = dtpNgayMua.Value.ToString("dd/MM/yyyy");
                    string hansd = dtpHanSuDung.Value.ToString("dd/MM/yyyy");
                    string ghichu = txtGhiChu.Text;
                    string ncc = cbNCC.SelectedValue.ToString();
                    int socot = DanhSachPhanMemDAO.Instance.CheckMaPM(maPM);
                    if (socot > 0)
                    {
                        MessageBox.Show(" Mã phần mềm đã tồn tại!", "Thông Báo");
                    }
                    else
                    {
                        DialogResult kq = MessageBox.Show($"Bạn muốn thêm mã phần mềm {maPM}", "Thông Báo:", MessageBoxButtons.YesNo);
                        if (kq == DialogResult.Yes)
                        {
                            DanhSachPhanMemDAO.Instance.Insert(maPM, tenPM, license, ngaymua, hansd, ncc, ghichu);
                            MessageBox.Show($" Thêm mã phần mềm {maPM} thành công! ");
                        }
                        them = false;
                        LoadControl();

                    }
                }
                else
                { // UPDATE THONG TIN
                    string maPM = txtMaPhanMem.Text;
                    string tenPM = txtTenPhanMem.Text;
                    string license = txtLicense.Text;
                    string ngaymua = dtpNgayMua.Value.ToString("dd/MM/yyyy");
                    string hansd = dtpHanSuDung.Value.ToString("dd/MM/yyyy");
                    string ghichu = txtGhiChu.Text;
                    string ncc = cbNCC.SelectedValue.ToString();
                    if (maPM == "")
                    {
                        MessageBox.Show($" Bạn chưa chọn mã phần mềm để thay đổi thông tin!", "Thông Báo:");
                    }
                    else
                    {
                        DialogResult kq = MessageBox.Show($"Bạn muốn sửa thông tin của mã phần mềm {maPM}", "Thông Báo:", MessageBoxButtons.YesNo);
                        if (kq == DialogResult.Yes)
                        {
                            DanhSachPhanMemDAO.Instance.Update(maPM, tenPM, license, ngaymua, hansd, ncc, ghichu);
                            MessageBox.Show($" Sửa thông tin mã phần mềm {maPM} thành công! ");
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Hãy chọn nhà cung cấp có trong danh sách ", "Thông Báo:");
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
            txtMaPhanMem.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaPhanMem.Enabled = false;
            string maPM = txtMaPhanMem.Text.Trim();
            if (maPM == "")
            {
                MessageBox.Show(" Bạn chưa chọn mã phần mềm để xóa ! ");

            }
            else
            {
                DialogResult kq = MessageBox.Show($" Bạn muốn xóa mã phần mềm {maPM} ", "Thông Báo", MessageBoxButtons.OKCancel);
                if (kq == DialogResult.OK)
                {
                    UserDAO.Instance.DeleteTable(maPM);
                    MessageBox.Show($"Xóa thành công mã phần mềm {maPM} ");
                }

            }
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

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Save();
            LoadControl();
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            LockControl(false);
            them = true;
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaPhanMem.Enabled = false;
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaPhanMem.Enabled = false;
            string maPM = txtMaPhanMem.Text.Trim();
            if (maPM == "")
            {
                MessageBox.Show(" Bạn chưa chọn mã phần mềm để xóa ! ");

            }
            else
            {
                DialogResult kq = MessageBox.Show($" Bạn muốn xóa mã phần mềm {maPM} ", "Thông Báo", MessageBoxButtons.OKCancel);
                if (kq == DialogResult.OK)
                {
                    UserDAO.Instance.DeleteTable(maPM);
                    MessageBox.Show($"Xóa thành công mã phần mềm {maPM} ");
                }

            }
            LoadControl();
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