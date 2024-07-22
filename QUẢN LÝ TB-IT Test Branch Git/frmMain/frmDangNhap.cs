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
using CONNECTION;
using ChuoiKetNoi;

namespace frmMain
{
    public partial class frmDangNhap : DevExpress.XtraEditors.XtraForm
    {
        public frmDangNhap()
        {
            InitializeComponent();
            txtMatKhau.UseSystemPasswordChar = true;
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtMatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                txtMatKhau.UseSystemPasswordChar = true;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDnDD1_Click(object sender, EventArgs e)
        {
            DataProvider.Instance.strcon = @"Data Source=.\SQLEXPRESS;Initial Catalog=QlyIT_ALL;Integrated Security=True";
            string tknhap = txtTaiKhoan.Text.Trim();
            string tk = tknhap.ToLower();
            string mkNhap = txtMatKhau.Text.Trim();
            int socot = UserDAO.Instance.CheckTaiKhoanExist(tk);
            if (socot == 1)
            {
                UserDTO userDTO = UserDAO.Instance.GetUserDTO(tk);
                string MaPB = userDTO.PHONGBAN;
              
                    string mk = userDTO.MATKHAU;
                    if (mkNhap == mk)
                    {
                        CommonUser.UserStatic = userDTO;
                        CommonUser.NhaMayDN = "DD1";
                        frmMain f = new frmMain();
                        f.Text = "Quản lý IT Đông Dương 1";
                        f.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show(" Sai mật khẩu! ", "Lỗi:");
                    }             
            }
            else
            {
                MessageBox.Show(" Sai tài khoản! ", "Lỗi:");
            }
        }

        private void btnDnDD2_Click(object sender, EventArgs e)
        {

                //  string strcon = @"server=192.168.0.10;uid=sa;database=QLyIT_DD2;password=12345678;";
                DataProvider.Instance.strcon = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLyIT_DD2;Integrated Security=True";
                string tknhap = txtTaiKhoan.Text.Trim();
                string tk = tknhap.ToLower();
                string mkNhap = txtMatKhau.Text.Trim();
                int socot = UserDAO.Instance.CheckTaiKhoanExist(tk);

                if (socot == 1)
                {
                    UserDTO userDTO = UserDAO.Instance.GetUserDTO(tk);
                    string mk = userDTO.MATKHAU;
                    if (mkNhap == mk)
                    {
                        CommonUser.UserStatic = userDTO;
                        CommonUser.NhaMayDN = "DD2";
                        frmMain f = new frmMain();                       
                        f.Text = "Quản lý IT Nhà máy Đông Dương 2";
                        f.ShowDialog();                        
                    }
                    else
                    {
                        MessageBox.Show(" Sai mật khẩu! ", "Thông Báo");
                    }
                }
                else
                {
                    MessageBox.Show(" Sai tài khoản! ", "Thông Báo");
                }

        }

        private void btnDnDDK_Click(object sender, EventArgs e)
        {
            //  string strcon = @"server=192.168.0.10;uid=sa;database=QLyIT_DDK;password=12345678;";
            //string tknhap = txtTaiKhoan.Text.Trim();
            //string tk = tknhap.ToLower();
            //string mkNhap = txtMatKhau.Text.Trim();
            //int socot = UserDAO.Instance.CheckAccountExist(tk);
            //if (socot == 1)
            //{
            //    UserDTO userDTO = UserDAO.Instance.GetUserDTO(tk);
            //    string mk = userDTO.MATKHAU;
            //    if (mkNhap == mk)
            //    {
            //        CommonUser.UserStatic = userDTO;
            //        frmMain f = new frmMain();
            //        f.Text = "Quản lý IT Nhà máy Đông Dương Khuôn";
            //        f.ShowDialog();
            //    }
            //    else
            //    {
            //        MessageBox.Show(" Sai mật khẩu! ", "Thông Báo:");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show(" Sai tài khoản! ", "Thông Báo");
            //}
        }       
    }
}