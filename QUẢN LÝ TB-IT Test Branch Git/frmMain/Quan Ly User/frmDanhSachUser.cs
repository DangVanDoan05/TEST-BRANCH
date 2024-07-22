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

namespace frmMain.Quan_Ly_User
{
    public partial class frmDanhSachUser : DevExpress.XtraEditors.XtraForm
    {
        public frmDanhSachUser()
        {
            InitializeComponent();
            LoadControl();
        }

         bool them = false;

         int idquyen = CommonUser.Quyen;

        // Phân quyền Load Datagrid control 

        // Phân quyền nút chức năng : đưa ra thông báo bạn chưa đủ thẩm quyền 

        private void LoadControl()
        {
            LockControl(true);
            LoadGridControl();
            LoadDataEditLookup();
            CleanText();
        }

        private void LockControl(bool kt)
        {
            if (kt)
            {
                sglMaNV.Enabled = false;
                txtHoTen.Enabled = false;
                txtBoPhan.Enabled = false;
                txtPhongBan.Enabled = false;
                txtNhom.Enabled = false;
                txtChucVu.Enabled = false;
                txtTaiKhoan.Enabled = false;
                txtMatKhau.Enabled = false;
                             
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
            }
            else
            {
                sglMaNV.Enabled = true;
                txtHoTen.Enabled = false;
                txtBoPhan.Enabled = false;
                txtPhongBan.Enabled = false;
                txtChucVu.Enabled = false;
                txtNhom.Enabled = false;
                txtTaiKhoan.Enabled = false;
                txtMatKhau.Enabled = true;
               
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;
            }
        }

        private void LoadGridControl()
        {
          
            if (idquyen >= 7) // admin + Tổng giám đốc
            {
                gridControl1.DataSource = UserDAO.Instance.GetTable();
            }
            else
            {

                if (idquyen >= 6) // QLCC: Load bộ phận
                {
                    gridControl1.DataSource = UserDAO.Instance.GetUserOfBP(CommonUser.UserStatic.BOPHAN);                 
                }
                else
                {

                    if (idquyen>= 5) // QLTC trở xuống chỉ Load nhân viên của phòng ban
                    {
                        gridControl1.DataSource = UserDAO.Instance.GetUserOfPB(CommonUser.UserStatic.PHONGBAN);                      
                    }
                    else
                    {
                        gridControl1.DataSource = UserDAO.Instance.GetUserOfPB(CommonUser.UserStatic.PHONGBAN);
                        gridColumn3.Visible = false;
                    }

                }
            }

        }

        private void LoadDataEditLookup()
        {
            //*** Load Mã nhân viên theo quyền
            // ADMIN
            // QLCC
            // QLTC

           if(idquyen>=7) // admin + Tổng giám đốc
            {
                sglMaNV.Properties.DataSource = QLNhanVienDAO.Instance.GetTable();
                sglMaNV.Properties.DisplayMember = "MANV";
                sglMaNV.Properties.ValueMember = "MANV";
            }
            else
            {
                if(idquyen>=6) // QLCC: Load bộ phận
                {
                    sglMaNV.Properties.DataSource = QLNhanVienDAO.Instance.GetNVOfBP(CommonUser.UserStatic.BOPHAN);
                    sglMaNV.Properties.DisplayMember = "MANV";
                    sglMaNV.Properties.ValueMember = "MANV";
                }
                else
                {

                    if(idquyen>=1&&idquyen<=5) // QLTC trở xuống chỉ Load nhân viên của phòng ban
                    {
                        sglMaNV.Properties.DataSource = QLNhanVienDAO.Instance.GetNVOfPB(CommonUser.UserStatic.PHONGBAN);
                        sglMaNV.Properties.DisplayMember = "MANV";
                        sglMaNV.Properties.ValueMember = "MANV";
                    }
                    
                }               
            }

        }
       
        private void CleanText()
        {
            txtTaiKhoan.Clear();
            txtMatKhau.Clear();
            txtPhongBan.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(idquyen>=2)
            {
                LockControl(false);
                them = true;
            }
            else
            {
                MessageBox.Show("Bạn chưa được cấp quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void searchLookUpEdit1View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private static readonly string[] VietnameseSigns = new string[]

    {

        "aAeEoOuUiIdDyY",

        "áàạảãâấầậẩẫăắằặẳẵ",

        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

        "éèẹẻẽêếềệểễ",

        "ÉÈẸẺẼÊẾỀỆỂỄ",

        "óòọỏõôốồộổỗơớờợởỡ",

        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

        "úùụủũưứừựửữ",

        "ÚÙỤỦŨƯỨỪỰỬỮ",

        "íìịỉĩ",

        "ÍÌỊỈĨ",

        "đ",

        "Đ",

        "ýỳỵỷỹ",

        "ÝỲỴỶỸ"



    };



        public static string RemoveSign4VietnameseString(string str)

        {

            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

            for (int i = 1; i < VietnameseSigns.Length; i++)

            {

                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

            }

            return str;

        }

        private void sglMaNV_EditValueChanged(object sender, EventArgs e)
        {
            //string MaNV = sglMaNV.EditValue.ToString(); 
            //QLNhanVienDTO NhanVienDTO = QLNhanVienDAO.Instance.GetNhanVienDTO(MaNV);
            
            //string HoTenNV = NhanVienDTO.FULLNAME;
            //txtHoTen.Text = HoTenNV;
            //// Cắt chuỗi lấy ký tự đầu và ký tự cuối
            //string[] arrayHoTen = HoTenNV.Split(' ');
            //int dodai = arrayHoTen.Length;
            //string Ho = arrayHoTen[0];
            //string Ten= arrayHoTen[dodai-1];
            //string Kytudau = Ho.Substring(0, 1).ToLower();
            //string Kytucuoi = Ten.Substring(0, 1).ToLower();
            //string HautoViet = Kytudau + Kytucuoi;
            //string HautoAnh = RemoveSign4VietnameseString(HautoViet);
           
            //txtMatKhau.Text = "1";
            //txtBoPhan.Text = NhanVienDTO.BOPHAN;
            //txtPhongBan.Text = NhanVienDTO.PHONGBAN;
            //txtNhom.Text = NhanVienDTO.NHOM;
            //txtChucVu.Text = NhanVienDTO.CHUCVU;

            //if (txtPhongBan.Text == "ADMIN")
            //{
            //    txtTaiKhoan.Enabled = true;
            //    txtTaiKhoan.Text = MaNV ;
            //}
            //else
            //{
            //    txtTaiKhoan.Text = MaNV + HautoAnh;
            //}
        }

        void Save()
        {
            if(them)
            {
                try
                {
                    string MaNV = sglMaNV.EditValue.ToString();
                    string Hoten = txtHoTen.Text;
                    string BoPhan = txtBoPhan.Text;
                    string PhongBan = txtPhongBan.Text;
                    string Nhom = txtNhom.Text;
                    string ChucVu = txtChucVu.Text;
                    string TaiKhoan = txtTaiKhoan.Text;
                    string MatKhau = txtMatKhau.Text;
                   
                    bool CheckUserExist = UserDAO.Instance.CheckUserExist(MaNV);
                    if (CheckUserExist)
                    {
                       MessageBox.Show($" Nhân viên mã {MaNV} đã đăng ký User.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult kq = MessageBox.Show($"Bạn muốn tạo tài khoản cho nhân viên mã {MaNV}.", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (kq == DialogResult.Yes)
                        {
                            //*** Thêm vào bảng quản lý User
                            // QLUSER(MANV,FULLNAME,BOPHAN,PHONGBAN,NHOM,CHUCVU,TAIKHOAN,MATKHAU,MAQLTT) 

                            UserDAO.Instance.Insert(MaNV,Hoten ,BoPhan,PhongBan,Nhom,ChucVu, TaiKhoan, MatKhau,"");

                            //*** Khi thêm User thì thêm luôn quyền cho User đó

                            //*** Tài khoản thường thì phải phân còn thuộc phòng admin thì là quyền cao nhất

                            UserDTO userDTO = UserDAO.Instance.GetUserDTO1(MaNV);
                            List<QLYCHUCNANGDTO> LsCN = QLYCHUCNANGDAO.Instance.GetLsCN();
                            foreach (QLYCHUCNANGDTO item in LsCN)
                            {
                                string BpUser = userDTO.BOPHAN;
                                if(BpUser == "ADMIN")
                                {
                                    QuanLyQuyenDTO quyenbandau = QuanLyQuyenDAO.Instance.GetChiTietQuyen(8); // IDquyen=8  <=> Toàn quyền ADMIN
                                    QlyPhanQuyenDAO.Instance.Insert(MaNV, item.ID, item.IDPARENT, item.MOTA, quyenbandau.IDQUYEN, quyenbandau.CHITIETQUYEN);
                                }
                                else
                                {
                                    QuanLyQuyenDTO quyenbandau1 = QuanLyQuyenDAO.Instance.GetChiTietQuyen(0); // IDquyen=0  <=> Cấm
                                    QlyPhanQuyenDAO.Instance.Insert(MaNV, item.ID, item.IDPARENT, item.MOTA, quyenbandau1.IDQUYEN, quyenbandau1.CHITIETQUYEN);
                                }                                                            
                            }
                            MessageBox.Show($"Đã tạo tài khoản cho nhân viên mã {MaNV} .", "THÀNH CÔNG!");
                            them = false;
                        }

                    }

                }
                catch
                {
                    MessageBox.Show($"Hãy chọn mã nhân viên có trong danh sách. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    them = false;
                }
            }
            else // Update mật khẩu User
            {
                    string MaNV = sglMaNV.Text;
                    string MatKhau = txtMatKhau.Text;
                                                    
                    DialogResult kq = MessageBox.Show($"Bạn muốn sửa mật khẩu User cho nhân viên mã {MaNV} ?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (kq == DialogResult.Yes)
                    {
                        //*** update mật khẩu
                        UserDAO.Instance.UpdateMKUSER(MaNV, MatKhau);
                        MessageBox.Show($"Đã thêm nhân viên mã {MaNV}.", "THÀNH CÔNG!");
                        them = false;
                    }
                    
            }

        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (idquyen >= 2)
            {
                Save();
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn chưa được cấp quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(idquyen>=2)
            {
                LockControl(false);
                sglMaNV.Enabled = false;
            }
            else
            {
                MessageBox.Show("Bạn chưa được cấp quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }          
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (idquyen >= 2)
            {              
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn chưa được cấp quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (idquyen >= 3)
            {
                LockControl(false);
                sglMaNV.Enabled = false;
                string ma = sglMaNV.Text.Trim();

                if (ma == "")
                {
                    MessageBox.Show(" Bạn chưa chọn mã User để xóa!", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    // cho phép xóa nhiều dòng trong gridview
                    int dem = 0;
                    //  int demloi = 0;
                    List<string> LsMaNVdcChon = new List<string>();
                    foreach (var item in gridView1.GetSelectedRows())
                    {
                        string ma1 = gridView1.GetRowCellValue(item, "MANV").ToString();
                        LsMaNVdcChon.Add(ma1);
                        dem++;
                    }

                    DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} mã User được chọn?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {
                        int demXoa = 0;
                        foreach (string item in LsMaNVdcChon)
                        {
                            if (item != CommonUser.UserStatic.MANV)
                            {

                                UserDAO.Instance.Delete(item);
                                demXoa++;

                            // Xóa trong bảng phân quyền của User đó.

                                QlyPhanQuyenDAO.Instance.Delete(item);

                            }                          
                        }

                        if(demXoa<dem)
                        {
                            MessageBox.Show($"Đã xóa {demXoa} User, {dem-demXoa} User không được xóa.", "THÀNH CÔNG!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        }
                        else
                        {
                            MessageBox.Show($"Đã xóa {dem} User được chọn.", "THÀNH CÔNG!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        }

                        demXoa = 0;
                        dem = 0;
                    }
                }
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn chưa được cấp quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                sglMaNV.EditValue = gridView1.GetFocusedRowCellValue("MANV").ToString();
                txtHoTen.Text = gridView1.GetFocusedRowCellValue("FULLNAME").ToString();
                txtTaiKhoan.Text = gridView1.GetFocusedRowCellValue("TAIKHOAN").ToString();
                txtPhongBan.Text = gridView1.GetFocusedRowCellValue("PHONGBAN").ToString();
                txtMatKhau.Text = gridView1.GetFocusedRowCellValue("MATKHAU").ToString();
                
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