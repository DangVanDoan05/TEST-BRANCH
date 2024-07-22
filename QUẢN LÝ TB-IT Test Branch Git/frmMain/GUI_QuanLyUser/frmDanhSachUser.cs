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
using QuanLyThietBiIT.GridViewEdit;

namespace QuanLyThietBiIT
{
    public partial class frmDanhSachUser : DevExpress.XtraEditors.XtraForm
    {
       
        public frmDanhSachUser()
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
            cbLoaiTK.DataSource = LoaiTKDAO.Instance.GetLsvLoaiTk();
            cbLoaiTK.DisplayMember = "MALOAITK";
            cbLoaiTK.ValueMember = "MALOAITK";

        }
     
        private void CleanText()
        {
            txtTaiKhoan.Clear();
            txtMatKhau.Clear();
            txtTenHienThi.Clear();
            txtMaNhanVien.Clear();
           
        }
      
        private void LoadData()
        {
            
            gridControl1.DataSource = UserDAO.Instance.GetTable();
        }

        private void LockControl(bool kt)
        {
            if(kt)
            {
                txtTaiKhoan.Enabled = false;
                txtMatKhau.Enabled = false;
                txtMaNhanVien.Enabled = false;
                txtTenHienThi.Enabled = false;
                cbPhongBan.Enabled = false;
                cbLoaiTK.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
                btnHuy.Enabled = false;
                btnResetMK.Enabled = true;
            }
            else
            {
                txtTaiKhoan.Enabled = true;
                txtMatKhau.Enabled = true;
                txtMaNhanVien.Enabled = true;
                txtTenHienThi.Enabled = true;
                cbPhongBan.Enabled = true;
                cbLoaiTK.Enabled = true;
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = false;
                btnHuy.Enabled = true;
                btnResetMK.Enabled = false;
            }
        }
      

        void Save()
        {
            try
            {
                if (them)
                {
                    string tk = txtTaiKhoan.Text.Trim();
                    string mk = txtMatKhau.Text.Trim();
                    string manv = txtMaNhanVien.Text.Trim();
                    string tenht = txtTenHienThi.Text.Trim();
                    string phongban = cbPhongBan.SelectedValue.ToString();
                    string maloaiTK = cbLoaiTK.SelectedValue.ToString();

                    int socot = UserDAO.Instance.CheckAccountExist(tk);
                    if (socot > 0)
                    {
                        MessageBox.Show(" Tài Khoản đã tồn tại!", "Thông Báo");
                    }
                    else
                    {
                        DialogResult kq = MessageBox.Show($"Bạn muốn thêm Tài Khoản {tk}", "Thông Báo:", MessageBoxButtons.YesNo);
                        if (kq == DialogResult.Yes)
                        {
                            UserDAO.Instance.InsertTable(tk, mk, manv, tenht, phongban, maloaiTK);
                            MessageBox.Show($" Thêm tài khoản {tk} thành công! ");
                        }
                        them = false;
                        LoadControl();

                    }
                }
                else
                { // UPDATE THONG TIN
                    string tk = txtTaiKhoan.Text.Trim();
                    string mk = txtMatKhau.Text.Trim();
                    string manv = txtMaNhanVien.Text.Trim();
                    string tenht = txtTenHienThi.Text.Trim();
                    string maloaiTK = cbLoaiTK.SelectedValue.ToString();
                    LoaiTKDTO loaitkDTO = LoaiTKDAO.Instance.getLoaiTK(maloaiTK);
                    string phongban = cbPhongBan.SelectedValue.ToString();

                    DialogResult kq = MessageBox.Show($"Bạn muốn sửa thông tin của Tài Khoản {tk}", "Thông Báo:", MessageBoxButtons.YesNo);
                    if (kq == DialogResult.Yes)
                    {
                        UserDAO.Instance.UpdateTable(tk, mk, manv, tenht, phongban, maloaiTK);
                        MessageBox.Show($" Sửa thông tin tài khoản {tk} thành công! ");
                    }

                }
            }
            catch
            {
                MessageBox.Show("Hãy chọn mã loại tài khoản và phòng ban trong danh sách!", "Thông Báo:");
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            txtTaiKhoan.Text = gridView1.GetFocusedRowCellValue("TAIKHOAN").ToString();
            txtMatKhau.Text = gridView1.GetFocusedRowCellValue("MATKHAU").ToString();
            txtMaNhanVien.Text= gridView1.GetFocusedRowCellValue("MANV").ToString();
            txtTenHienThi.Text = gridView1.GetFocusedRowCellValue("FULLNAME").ToString();
        }



        private void btnLuu_Click(object sender, EventArgs e)
        {
            Save();
            LoadControl();
        }

       

        private void btnSuaUser_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtTaiKhoan.Enabled = false;
            txtMatKhau.Enabled = false;
        }

       
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            LoadControl();
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
            txtTaiKhoan.Enabled = false;
            txtMatKhau.Enabled = false;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtTaiKhoan.Enabled = false;
            txtMatKhau.Enabled = false;
            string tk = txtTaiKhoan.Text.Trim();
            if (txtTaiKhoan.Text == "")
            {
                MessageBox.Show(" Bạn chưa chọn Tài Khoản để xóa! ");

            }
            else
            {
                DialogResult kq = MessageBox.Show($" Bạn muốn xóa Tài khoản {tk} ", "Thông Báo", MessageBoxButtons.OKCancel);
                if (kq == DialogResult.OK)
                {
                    UserDAO.Instance.DeleteTable(tk);
                    MessageBox.Show($"Xóa thành công tài khoản {tk} ");
                }

            }
            LoadControl();
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
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

        private void btnResetMK_Click_1(object sender, EventArgs e)
        {
            LockControl(false);
            txtTaiKhoan.Enabled = false;
            txtMatKhau.Enabled = false;
            string tk = txtTaiKhoan.Text.Trim();
            string mkreset = "0";
            string manv = txtMaNhanVien.Text.Trim();
            string fullname = txtTenHienThi.Text.Trim();
            string maloaiTK = cbLoaiTK.SelectedValue.ToString(); LoaiTKDTO loaitkDTO = LoaiTKDAO.Instance.getLoaiTK(maloaiTK);
            string phongban = cbPhongBan.SelectedValue.ToString();
            if (txtTaiKhoan.Text == "")
            {
                MessageBox.Show(" Bạn chưa chọn Tài Khoản! ", "Thông Báo");

            }
            else

            {
                DialogResult kq = MessageBox.Show("Bạn chắc chắn muốn Reset mật khẩu về mặc định? ", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    UserDAO.Instance.UpdateMKUSER(tk, mkreset);
                    MessageBox.Show(" Reset mật khẩu thành công, Hãy đăng nhập lại phần mềm! ", " Thông báo: ");

                }

            }
            LoadControl();
        }
    }
}