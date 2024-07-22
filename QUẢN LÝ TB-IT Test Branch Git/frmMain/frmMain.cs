using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DAO;
using DTO;

using frmMain.YeuCauKyThuat;
using frmMain.Quan_Ly_May_Tinh;
using frmMain.Du_Lieu_Nguon;
using DAO;
using DTO;
using DevExpress.XtraBars.Ribbon;
using PhanQuyenUngDung;
using System.Collections;
using frmMain.Quan_Ly_Kho;
using frmMain.Quan_Ly_User;

// Cho chạy đa luồng dù đang đăng nhập DD1 nhưng vẫn thông báo đc yêu cầu của  DD2, DDK 

namespace frmMain
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private static frmMain instance;

        public static frmMain Instance
        {
            get { if (instance == null) instance = new frmMain(); return frmMain.instance; }
            private set { frmMain.instance = value; }
        }
       
        UserDTO user = CommonUser.UserStatic;

        // Ném hết thông báo chạy vào Form Main

        public frmMain()
        {
            InitializeComponent();
            barHeaderItem1.Caption = user.FULLNAME;
             
            LoadListCN();     // Fix lỗi thêm chức năng khi Lập trình viên muốn mở rộng các tính năng của phần mềm 

            LoadRight();    // Fix lỗi thêm chức năng khi Lập trình viên muốn mở rộng các tính năng của phần mềm
            
        }

        public void OpenForm(Type tyForm)
        {
            foreach (var frm in MdiChildren)
            {
                if (frm.GetType() == tyForm)
                {
                    frm.Close();
                    frm.Activate();                   
                }
            }
            Form f = (Form)Activator.CreateInstance(tyForm);
            f.MdiParent = this;
            f.Show();
        }


        //public void OpenForm(Type tyForm)
        //{
        //    foreach (var frm in MdiChildren)
        //    {
        //        if (frm.GetType() == tyForm)
        //        {
        //            frm.Activate();

        //            frm.Close();
        //        }
        //    }
        //    Form f = (Form)Activator.CreateInstance(tyForm);
        //    f.MdiParent = this;
        //    f.Show();
        //}




        #region Load Thông Báo YCKT

        private void acThongBaoNgLapYC_FormClosing(object sender, DevExpress.XtraBars.Alerter.AlertFormClosingEventArgs e)
        {
            TrangThaiXuatHienTB.XuatHienTbNglapYC = 0;
        }

        private void acThongBaoPDPB_FormClosing(object sender, DevExpress.XtraBars.Alerter.AlertFormClosingEventArgs e)
        {
            TrangThaiXuatHienTB.XuatHienTbPDPB = 0;
        }

        private void acThongBaoPDIT_FormClosing(object sender, DevExpress.XtraBars.Alerter.AlertFormClosingEventArgs e)
        {
            TrangThaiXuatHienTB.XuatHienTbPDIT = 0;
        }

        private void acThongBaoTHYC_FormClosing(object sender, DevExpress.XtraBars.Alerter.AlertFormClosingEventArgs e)
        {
            TrangThaiXuatHienTB.XuatHienTbTHYC = 0;
        }

        private void acThongBaoPDLQPB_FormClosing(object sender, DevExpress.XtraBars.Alerter.AlertFormClosingEventArgs e)
        {
            TrangThaiXuatHienTB.XuatHienTbPDLQPB = 0;
        }

        private void LoadAlertNguoiLapYC()  // Thêm một biến nữa cho Hàm Load thông báo để kiểm tra Form thông báo đang Show hay ko
        {
            //if (QuanLyYCKTDAO.Instance.CheckExistYCtuchoi(CommonUser.UserStatic.MANV)&&(TrangThaiXuatHienTB.XuatHienTbNglapYC<1))
            //{
            //    Message mes = new Message();
            //    acThongBaoNgLapYC.Show(this, "Thông báo:", "Có YCKT bạn lập bị từ chối phê duyệt.", "", Properties.Resources.Hopstarter_3d_Cartoon_Vol3_Windows_Close_Program_256, mes,true);
            //    // biến trạng thái acThongBaoNglapYC đang show lên thì =1 ----> ko show nữa.
            //    TrangThaiXuatHienTB.XuatHienTbNglapYC = 1;
            //}
        }

        private void LoadAlertNguoiPDPB() // Phân tích theo tiến trình của người đi vào
        {
            // Lỗi ADMIN xảy ra ở thông báo


        }

        private void LoadAlertNguoiPDLQPB()
        {

            //if(QlyPheDuyetPBLQDAO.Instance.CheckPDLQPB(CommonUser.UserStatic.MANV) && (TrangThaiXuatHienTB.XuatHienTbPDLQPB < 1))
            //{
            //    Message mes = new Message();
            //    // acThongBaoPDPB.Show(this, "Thông báo:", "Có yêu cầu kỹ thuật cần phê duyệt.", "", Properties.Resources.Iconbell, mes);
            //    acThongBaoPDLQPB.Show(this, "THÔNG BÁO:", "Có yêu cầu kỹ thuật liên quan đến phòng ban cần bạn phê duyệt.", "", Properties.Resources.Iconbell, mes, true);
            //    TrangThaiXuatHienTB.XuatHienTbPDLQPB = 1;
            //}

        }

        private void LoadAlertNguoiPDIT() 
        {
          
        }

        private void LoadAlertPhanCongTHYC()
        {
            // admin cũng có thể vào phê duyệt

        }

      
     

      

      

        // Thông báo YCKT phòng ban liên quan.

      
        #endregion

        #region LoadListChucNang

        private void LoadListCN()   // Lấy ra List Chức năng
            // Lưu xuống một bảng dưới CSDL < bảng lưu lại toàn bộ các chức năng, khi thêm User thì sẽ Load từ bảng này lên>
        {
            ArrayList visiblePages = this.Ribbon.TotalPageCategory.GetVisiblePages();
            DataTable table_ribbon = TablePermission.Instance.CreatedTablePermission();
            foreach (RibbonPage page in visiblePages)
                {
                    var page_name = page.Name;
                    var page_caption = page.Text;

                    table_ribbon.Rows.Add(page_name, "0", page_caption); // duyệt từ page nên Page là cha, idparent=0

                    foreach (RibbonPageGroup group in page.Groups)
                    {
                        var page_group_name = group.Name;
                        var page_group_caption = group.Text;
                        if (page_group_name.Equals("ribbonPageGroup_Giaodien"))
                        {
                            break;
                        }
                        table_ribbon.Rows.Add(page_group_name, page_name, page_group_caption);
                        foreach (BarItemLink item in group.ItemLinks)
                        {
                            var item_caption = item.Caption;
                            var item_name = item.Item.Name;

                            table_ribbon.Rows.Add(item_name, page_group_name, item_caption);

                        }
                    }
                }


            // Xử lý Logic mãi cũng chán

            // Load List chức năng mới:

            List<QLYCHUCNANGDTO> LsCNmoi = new List<QLYCHUCNANGDTO>();
                
            foreach (DataRow item in table_ribbon.Rows)
            {
                QLYCHUCNANGDTO ChucNangDTO = new QLYCHUCNANGDTO(item);
                LsCNmoi.Add(ChucNangDTO);
                            
            }

            // So sánh List cũ với List mới chức năng mới để xóa bỏ những tính năng cũ

         

            List<QLYCHUCNANGDTO> LsCNcu = QLYCHUCNANGDAO.Instance.GetLsCN();

            //   Thuật toán đang bị sai, Logic đang bị sai

            // nếu không thấy chức năng cũ xuất hiện trong List mới thì mới bị xóa

            foreach (QLYCHUCNANGDTO item in LsCNcu)
            {
                int Koxuathien = 0;
                foreach (QLYCHUCNANGDTO item1 in LsCNmoi)
                {
                    if (item.ID == item1.ID)
                    {
                        Koxuathien++;
                    }
                }
                if (Koxuathien == 0)    // Nếu không xuất hiện trong List danh sách mới.
                {
                    // Xóa trong bảng quản lý chức năng 
                    QLYCHUCNANGDAO.Instance.Delete(item.ID);

                    // Xóa chức năng đó trong bảng phân quyền của các user
                    QlyPhanQuyenDAO.Instance.DeleteChucNang(item.ID);
                }
            }

            // So sánh List mới với List cũ sau xóa ===> Để add thêm những tính năng mới.

            List<QLYCHUCNANGDTO> LsCNcuSauXoa = QLYCHUCNANGDAO.Instance.GetLsCN();

            foreach (QLYCHUCNANGDTO item in LsCNmoi)
            {
                int Koxuathien = 0;
                foreach (QLYCHUCNANGDTO item1 in LsCNcuSauXoa)
                {
                    if (item.ID == item1.ID)
                    {
                        Koxuathien++;
                    }
                }

                if (Koxuathien == 0)   //Chức năng chưa từng xuất hiện trong List danh sách cũ thì thêm vào bảng chức năng, bảng phân quyền
                {
                    // **** Thêm vào trong bảng quản lý chức năng                      
                    QLYCHUCNANGDAO.Instance.Insert(item.ID, item.IDPARENT, item.MOTA);

                    // Thêm vào trong bảng phân quyền của các user + ADMIN khởi định là quyền cao nhất

                    // *** Lấy ra List User

                    List<UserDTO> LsUser = UserDAO.Instance.GetLsvUser();

                    foreach (UserDTO item2 in LsUser)
                    {
                        string BpUser = item2.BOPHAN;
                        if (BpUser == "ADMIN")
                        {
                            QuanLyQuyenDTO quyenbandau = QuanLyQuyenDAO.Instance.GetChiTietQuyen(8); // IDquyen=8  <=> Toàn quyền ADMIN
                            QlyPhanQuyenDAO.Instance.Insert(item2.MANV, item.ID, item.IDPARENT, item.MOTA, quyenbandau.IDQUYEN, quyenbandau.CHITIETQUYEN);
                        }
                        else
                        {
                            QuanLyQuyenDTO quyenbandau1 = QuanLyQuyenDAO.Instance.GetChiTietQuyen(0); // IDquyen=0  <=> Cấm
                            QlyPhanQuyenDAO.Instance.Insert(item2.MANV, item.ID, item.IDPARENT, item.MOTA, quyenbandau1.IDQUYEN, quyenbandau1.CHITIETQUYEN);
                        }
                    }
                }
            }
        }
        #endregion

        #region Load Phân Quyền chức năng của User
        private void LoadRight()
        {
            ArrayList visiblePages = this.Ribbon.TotalPageCategory.GetVisiblePages();
            List<QlyPhanQuyenDTO> lsTTPQDTO = QlyPhanQuyenDAO.Instance.GetLsTTPQuser(CommonUser.UserStatic.MANV);
            foreach (RibbonPage page in visiblePages)
            {
                foreach (var item in lsTTPQDTO)  // ẩn Page 
                {
                    if(item.ID==page.Name&&item.IDQUYEN==0)
                    {
                        page.Visible = false;   
                    }
                }
               
                foreach (RibbonPageGroup group in page.Groups)
                {
                    foreach (var item in lsTTPQDTO)
                    {
                        if (item.ID == group.Name && item.IDQUYEN == 0)   // Khóa Group
                        {
                            group.Enabled = false;   
                            
                        }
                    }
                    foreach (BarItemLink barItemLink in group.ItemLinks)  // Khóa ButtonItemLink
                    {                      
                            foreach (var item in lsTTPQDTO)
                            {
                                if (item.ID == barItemLink.Item.Name && item.IDQUYEN == 0)
                                {
                                   barItemLink.Item.Enabled = false;   // khóa nút đó lại                            
                                }
                            }                                                                         
                    }
                }              
            }

        }
       
        void LoadItem(ItemClickEventArgs e)
        {
           
            QlyPhanQuyenDTO TTPQuser = QlyPhanQuyenDAO.Instance.Getitemuser(e.Item.Name,CommonUser.UserStatic.MANV);
            CommonUser.Quyen = TTPQuser.IDQUYEN; // quyền của User đó ứng với Nút đó
        }
        #endregion
        void LoadItem1(EventArgs e)
        {
            ItemClickEventArgs c = (ItemClickEventArgs)e;
            QlyPhanQuyenDTO TTPQuser = QlyPhanQuyenDAO.Instance.Getitemuser(c.Item.Name, CommonUser.UserStatic.MANV);
            CommonUser.Quyen = TTPQuser.IDQUYEN; // quyền của User đó ứng với Nút đó
        }



        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem1(e);
           OpenForm(typeof(frmNhom));
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmPhongBan));
            
        }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
           LoadItem(e);
            OpenForm(typeof(frmDsNhanVien));
        }

        private void btnThongTinTK_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmThongTinTaiKhoan));
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);         
            OpenForm(typeof(frmDanhSachMayTinh)); 
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmLoaiMT));
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmDanhSachPhanMem));
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmChiTietCaiDat));
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmDanhSachCaiDat));
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmCauHinhChiTiet));
        }

        private void btnTonLK_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);          
            OpenForm(typeof(frmTonLinhKien));
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmMaCongCu));
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmMaLinhKien));
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmNhaCungCap));
        }

        private void btnThongKeNhap_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmThongKeNhap));
        }

        private void btnThongKeXuat_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmThongKeXuat));
        }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmQuanLyDHPB));
        }

      

       
                    
        private void barButtonItem15_ItemClick_1(object sender, ItemClickEventArgs e)
        {
           // LoadItem(e);
            OpenForm(typeof(frmSoDoToChucCongTy));
        }


        private void btnChucVu_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmChucVu));
        }

      

      
      
      
       

       

       
        #region Hàm Check gửi YCKT với sự kiện FormClosing
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
        #endregion


        private void btnPhongBan1_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmPhongBan));
        }

        private void btnNhaMay_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmNhaMay));
        }

        private void btnPhanQuyenCN_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmPQChucNang));
        }
        
        private void btnPhanCapUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmPhanCapUser));
        }

      

        

        private void btnTKtieuhaoYCKT_ItemClick(object sender, ItemClickEventArgs e)
        {
           
        }

       

        #region Hỗ trợ YCKT

        #region Tạo YCKT (Client)
       private void btnYcADID_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            LoadItem(e);
            OpenForm(typeof(frmYeuCauADID));
        }


     
       
        private void btnDoiTbiIT_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmDoiTBIT));
        }

        private void btnYcTaiPM_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmSdVaTaiPM));
        }

        private void btnTruyCapWeb_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmTruyCapWebSite));
        }

        private void btnDiChuyenMT_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        #endregion 

        #region Quản lý YCKT tại phòng ban
        private void btnQlyYCKT_ItemClick(object sender, ItemClickEventArgs e)
        {
         
        }
            

        private void btnPDYCKTPB_ItemClick(object sender, ItemClickEventArgs e)
        {
           

        }

       

        #endregion

        #region Quản lý YCKT tại phòng IT
      
        private void btnTKtieuhaoYCKT_ItemClick_1(object sender, ItemClickEventArgs e)
        {
          
        }

        private void btnThuchienYCKT_IT_ItemClick(object sender, ItemClickEventArgs e)
        {
           
        }


        #endregion

        #endregion

        private void btnYCtruycapFILESV_ItemClick(object sender, ItemClickEventArgs e)
        {
           
        }

        private void btnDSUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmDanhSachUser));
        }

        private void btnThongTinTaiKhoan_ItemClick(object sender, ItemClickEventArgs e)
        {
            //  LoadItem(e);
            OpenForm(typeof(frmThongTinTaiKhoan));
        }

        private void btnQlyYCKTlienquanPB_ItemClick(object sender, ItemClickEventArgs e)
        {
          
        }

        private void btnNhomUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmNhom));
        }

        private void btnBoPhan1_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmBoPhan));
        }

        private void btnGiaiTrinhTBIT_ItemClick(object sender, ItemClickEventArgs e)
        {
           // LoadItem(e);
            OpenForm(typeof(frmGiaiTrinhMuaTBIT));
        }

        private void btnNhomNV_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmNhom));
        }

        private void btnDsNhanVien_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadItem(e);
            OpenForm(typeof(frmDsNhanVien));
        }

        private void tmPCTH_Tick(object sender, EventArgs e)
        {
            LoadAlertPhanCongTHYC();
        }


     

        private void btnDonHangPB_ItemClick(object sender, ItemClickEventArgs e)
        {
            //  LoadItem(e);
            OpenForm(typeof(frmQuanLyDHPB));
        }

        private void btnDSWebSite_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(frmDsWebSite));
        }

        private void btnNhaMay1_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(frmNhaMay));
        }

       

        private void btnBaoDuongSuaChua_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(frmHangMucBaoDuong));
        }


        private void btnLichSuBT_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(frmLichSuBT));
        }

        private void btnTonLKTH_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(frmTonLKThuHoi));
        }

        private void btnTKTH_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(frmThongKeThuHoi));
        }

        private void btnChiTietUpdatePM_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(frmChiTietUpdatePM));
        }
    }
}
 