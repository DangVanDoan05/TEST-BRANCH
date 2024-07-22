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



namespace QuanLyThietBiIT
{
    public partial class frmThongTinTaiKhoan : DevExpress.XtraEditors.XtraForm
    {
        public frmThongTinTaiKhoan()
        {
            InitializeComponent();
            LoadControl();
        }
        string matkhaucu = CommonUser.UserStatic.Matkhau;
        string fullname = CommonUser.UserStatic.Fullname;
        string phongban = CommonUser.UserStatic.Phongban;

        private void LoadControl()
        {
            LockControl();
            LoadData();

        }
        
        private void LoadData()
        {
            txtTK.Text = CommonUser.UserStatic.Taikhoan;
            txtMaNV.Text = CommonUser.UserStatic.Manv;
          
           
        }
        private void LockControl()
        {
            txtTK.Enabled = false;
            txtMaNV.Enabled = false;
            txtMKcu.Enabled = true;
            txtMKmoi.Enabled = true;
            txtMKxacnhan.Enabled = true;

        }
        void CleanText()
        {
            txtMKcu.Clear();
            txtMKmoi.Clear();
            txtMKxacnhan.Clear();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
          
            string mkcunhap = txtMKcu.Text.Trim();
            string mkmoi = txtMKmoi.Text.Trim();
            string mkxn = txtMKxacnhan.Text.Trim();

            if (mkcunhap != matkhaucu)
            {
                MessageBox.Show(" Mật khẩu cũ không đúng", " Thông Báo ",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                if(mkmoi!=mkxn)
                {
                    MessageBox.Show("Mật khẩu mới khác mật khẩu xác nhận, Hãy kiểm tra lại!", "Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                  DialogResult kq=MessageBox.Show("Bạn chắc chắn muốn đổi mật khẩu?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(kq==DialogResult.Yes)
                    {
                        UserDAO.Instance.UpdateMKUSER(txtTK.Text, txtMKmoi.Text);
                        MessageBox.Show("Đổi mật khẩu thành công, hãy đăng nhập lại!", "Thông Báo");
                    }
                }
                   
            }



        }
    }
}