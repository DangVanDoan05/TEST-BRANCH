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
using DevExpress.XtraGrid.Views.Grid;

namespace frmMain.YeuCauKyThuat
{
    public partial class frmSdVaTaiPM : DevExpress.XtraEditors.XtraForm
    {
        public frmSdVaTaiPM()
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
        }

        private void LockControl(bool kt)
        {
            if(kt)
            {
                sglMaPMsan.Enabled = false;
                txtPhienbanPM.Enabled = false;
                txtMucDichSD.Enabled = false;
                txtMayTinhCD.Enabled = false;
                btnCapNhatMT.Enabled = false;

                btnThem.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
            }
            else
            {
                sglMaPMsan.Enabled = true;
                txtPhienbanPM.Enabled = true;
                txtMucDichSD.Enabled = true;
                txtMayTinhCD.Enabled = false;
                btnCapNhatMT.Enabled = true;

                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;
            }
        }



       
        private void LoadData()
        {

            // Load Edit Lookup phần mềm.
            sglMaPMsan.Properties.DataSource = DanhSachPhanMemDAO.Instance.GetTable();
            sglMaPMsan.Properties.DisplayMember = "MAPM";
            sglMaPMsan.Properties.ValueMember = "MAPM";

            // Load nhân viên thực hiện của phòng IT:
          


            string PhongBan = CommonUser.UserStatic.PHONGBAN;       
                    
            // LOAD BẢNG QUẢN LÝ

            if (PhongBan == "IT" || PhongBan == "ADMIN")
            {
                if (PhongBan == "ADMIN")     //phòng ADMIN: ADMIN thì Load toàn bộ
                {
                    gridControl1.DataSource = QlyYcSDvaTaiPMDAO.Instance.GetAllYC();
                }
                else    //phòng IT
                {

                    if (idquyen >= 4) // Phê duyệt sơ cấp trở lên
                    {
                        gridControl1.DataSource = QlyYcSDvaTaiPMDAO.Instance.GetYCwithQlyIT(CommonUser.UserStatic.MANV);
                    }
                    else      // Quyền từ xác nhận trở xuống là người thực hiện yêu cầu.       
                    {
                        if (idquyen >= 2)
                        {
                            gridControl1.DataSource = QlyYcSDvaTaiPMDAO.Instance.GetYCwithNvIT(CommonUser.UserStatic.MANV);
                        }
                    }

                    // Người đc phâm công chỉ Load những yêu cầu ma mình được phân công:

                }
            }
            else   // Với phòng ban khác
            {
                if (idquyen >= 7) // Tổng giám đốc
                {
                    gridControl1.DataSource = QlyYcSDvaTaiPMDAO.Instance.GetAllYC();
                }
                else
                {
                    if (idquyen >= 6) // QLCC: Load bộ phận
                    {
                        gridControl1.DataSource = QlyYcSDvaTaiPMDAO.Instance.GetLsYcSDPMOfBP(CommonUser.UserStatic.BOPHAN);
                    }
                    else
                    {
                        if (idquyen == 5) // QLTC Load yêu cầu của phòng ban
                        {
                            gridControl1.DataSource = QlyYcSDvaTaiPMDAO.Instance.GetLsYcSDPMOfPB(CommonUser.UserStatic.PHONGBAN);
                        }
                        else // QLSC trở xuống load những yêu cầu của mình lập
                        {
                            gridControl1.DataSource = QlyYcSDvaTaiPMDAO.Instance.GetYCwithNgLapYC(CommonUser.UserStatic.MANV);
                        }
                    }
                }
            }
        }



        void Save()
        {
            if (them)
            {

                string Phongban = CommonUser.UserStatic.PHONGBAN;
                string MaYCKT = CommonUser.UserStatic.MANV + DateTime.Now.ToString("-dd/MM/yyyy HH:mm:ss tt");
                string NgayYC = DateTime.Now.ToString("dd/MM/yyyy");

                try 
                 {
                           
                            string MaPM = sglMaPMsan.EditValue.ToString();
                        
                            string TenPM = DanhSachPhanMemDAO.Instance.GetMaPM(MaPM).TENPM;
                            string ChucNang = DanhSachPhanMemDAO.Instance.GetMaPM(MaPM).CHUCNANG;
                            string PhienBanPM = txtPhienbanPM.Text;
                            string MucdichSD = txtMucDichSD.Text;
                            string MaytinhSD =txtMayTinhCD.Text;                          
                          
                         
                            if (MaytinhSD != "" && MucdichSD != "" && PhienBanPM != "" )
                            {                            
                                string NglapYC = CommonUser.UserStatic.MANV + "-" + NgayYC + DateTime.Now.ToString(" HH:mm:ss tt");
                                string PdPB = "";
                                string PDIT = "";
                                string HoantatYC = "";                              
                                QlyYcSDvaTaiPMDAO.Instance.Insert(MaYCKT, Phongban, NgayYC,MaPM,TenPM,PhienBanPM,ChucNang,MucdichSD,MaytinhSD,NglapYC,PdPB,PDIT,HoantatYC);
                            }
                            else
                            {
                                MessageBox.Show("Điền đầy đủ thông tin cần thiết.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            
                }
                catch
                {
                    MessageBox.Show("Hãy chọn phần mềm trong danh sách.", "LỖI:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            them = false;
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            if (idquyen >= 2)
            {
                LockControl(false);
                them = true;
            }
            else
            {
                MessageBox.Show($"Bạn không đủ thẩm quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void gridControl1_Click(object sender, EventArgs e)
        {

           // (MAYCKT, MANV, HOTEN, PHONGBAN, NGAYYC, TENPM, PHIENBANPM, CHUCNANG, MDSD, MTCAIDAT, NGUOILAPYC, PHEDUYETPB, PHEDUYETIT, THUCHIENYC, IDTINHTRANG,

            try
            {
               
            }
            catch
            {

            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int dem = 0;
            List<string> LsMaYCdcChon = new List<string>();
            foreach (var item in gridView1.GetSelectedRows())
            {
                string ma1 = gridView1.GetRowCellValue(item, "MAYCKT").ToString();
                LsMaYCdcChon.Add(ma1);
                dem++;
            }

            string PhongBan = CommonUser.UserStatic.PHONGBAN;

            if (idquyen >= 3) // Quyền từ xóa trở lên
            {
                if (PhongBan == "IT") // Phòng IT: IT không thể xóa khi đã hoàn tất
                {
                    if (idquyen >= 4)
                    {
                        if (dem == 0)
                        {
                            MessageBox.Show("Bạn chưa chọn yêu cầu để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} yêu cầu  được chọn.", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (kq == DialogResult.Yes)
                            {
                                int DemXoa = 0;
                                foreach (string item in LsMaYCdcChon)
                                {
                                    QLYCDOITBDTO a = QLYCDOITBDAO.Instance.GetYcDoiTBDTO(item);
                                    if (a.HTYC == "")
                                    {
                                        QLYCDOITBDAO.Instance.Delete(item);
                                        DemXoa++;
                                    }
                                }
                                MessageBox.Show($"Đã xóa {DemXoa} yêu cầu được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chưa đủ thẩm quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (PhongBan == "ADMIN") // Phòng ADMIN: Xóa được mọi lúc.
                    {

                        if (dem == 0)
                        {
                            MessageBox.Show("Bạn chưa chọn yêu cầu để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} yêu cầu được chọn.", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (kq == DialogResult.Yes)
                            {
                                foreach (string item in LsMaYCdcChon)
                                {

                                    QLYCDOITBDAO.Instance.Delete(item);

                                    // Xóa mã xuất kho của thiết bị trong bảng THỐNG KÊ XUẤT.

                                    // UPDATE LẠI SỐ LƯỢNG TỒN TRONG BẢN TỒN LINH KIỆN NẾU MÃ XUẤT KHO CHƯA ĐƯỢC KIỂM KÊ

                                }
                                MessageBox.Show($"Đã xóa {dem} yêu cầu được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else // Với các phòng ban khác.
                    {
                        if (idquyen >= 5) // Quyền từ QLTC trở lên
                        {

                            if (dem == 0)
                            {
                                MessageBox.Show("Bạn chưa chọn yêu cầu để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} yêu cầu được chọn.", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (kq == DialogResult.Yes)
                                {
                                    int demXoa = 0;
                                    foreach (string item in LsMaYCdcChon)
                                    {
                                        QLYCDOITBDTO a = QLYCDOITBDAO.Instance.GetYcDoiTBDTO(item);
                                        string PdIT = a.PDIT;
                                        if (PdIT == "") // Chưa phê duyệt IT
                                        {
                                            QLYCDOITBDAO.Instance.Delete(item);
                                            demXoa++;
                                        }
                                    }
                                    MessageBox.Show($"Đã xóa {demXoa} yêu cầu được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else // Nhân viên bình thường.
                        {

                            if (dem == 0)
                            {
                                MessageBox.Show("Bạn chưa chọn yêu cầu để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} yêu cầu được chọn.", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (kq == DialogResult.Yes)
                                {
                                    int demXoa = 0;
                                    foreach (string item in LsMaYCdcChon)
                                    {
                                        QLYCDOITBDTO a = QLYCDOITBDAO.Instance.GetYcDoiTBDTO(item);
                                        string PdPB = a.PDPB;
                                        if (PdPB == "") // Chưa phê duyệt phòng ban.
                                        {
                                            QLYCDOITBDAO.Instance.Delete(item);
                                            demXoa++;
                                        }

                                    }
                                    MessageBox.Show($"Đã xóa {demXoa} yêu cầu được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Chưa đủ thẩm quyền. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadControl();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            if(idquyen>=2)
            {
                Save();
                LoadControl();
            }
            else
            {
                MessageBox.Show($"Bạn không đủ thẩm quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"Bạn không đủ thẩm quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

      


      

    

        private void btnHoanTat_Click(object sender, EventArgs e)
        {
            string PhongBan = CommonUser.UserStatic.PHONGBAN;
            if ((PhongBan == "IT" || PhongBan == "ADMIN") && idquyen >= 2)
            {
                int dem = 0;
                List<string> LsMaYCdcChon = new List<string>();
                foreach (var item in gridView1.GetSelectedRows())
                {
                    string ma1 = gridView1.GetRowCellValue(item, "MAYCKT").ToString();
                    LsMaYCdcChon.Add(ma1);
                    dem++;
                }

                if (dem == 0)
                {
                    MessageBox.Show("Bạn chưa chọn yêu cầu để cập nhật hoàn tất.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int DemHtat = 0;
                    string NgHT = CommonUser.UserStatic.MANV + DateTime.Now.ToString("-dd/MM/yyyy HH:mm:ss tt");
                    foreach (string item in LsMaYCdcChon)
                    {
                        QlyYcSDvaTaiPMDTO Yc = QlyYcSDvaTaiPMDAO.Instance.GetYcSDPMDTO(item);
                        string PDPB = Yc.PDPB;
                        string PDIT = Yc.PDIT;
                        string HoanTatYC = Yc.HTYC;
                        if (PDPB != "" && PDIT != "" && HoanTatYC == "")
                        {
                            // CẬP NHẬT HOÀN TẤT TRONG BẢI TẢI VÀ SỬ DỤNG PHẦN MỀM:
                            QlyYcSDvaTaiPMDAO.Instance.UpdateHTYC(item, NgHT);

                           



                            

                            string MaMTCD = Yc.MTCAIDAT;
                            string[] MaMT = MaMTCD.Split(',');
                            string MaPM = Yc.MAPM;
                            string TenPM = Yc.TENPM;
                            foreach (string item1 in MaMT)
                            {
                                if(item1!="")
                                {
                                    //Cập nhật phần mềm cài đặt vào thông tin cài đặt của máy tính.
                                  //  DanhSachCaiDatDAO.Instance.Insert(item1, MaPM, TenPM, DateTime.Now.ToString("dd/MM/yyyy"));

                                    // Update lại trạng thái máy tính được chọn bằng không.
                                  //  QuanLyMayTinhDAO.Instance.UpdateDuocChon(item1, 0);
                                }
                            }
                            DemHtat++;
                        }
                    }
                    MessageBox.Show($"Đã hoàn tất {DemHtat} yêu cầu.", "THÀNH CÔNG:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dem = 0;
                    DemHtat = 0;
                }
            }
            else
            {
                MessageBox.Show("Chưa đủ thẩm quyền cho chức năng này!", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadControl();
        }
         
        private void searchLookUpEdit3View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnCapNhatMT_Click(object sender, EventArgs e)
        {
            frmDSMayTinhCD f = new frmDSMayTinhCD();
            f.ShowDialog();
            LoadData();
        }

        private void btnPDPB_Click_1(object sender, EventArgs e)
        {
            if (idquyen >= 5)
            {
                int dem = 0;
                List<string> LsMaYCdcChon = new List<string>();
                foreach (var item in gridView1.GetSelectedRows())
                {
                    string ma1 = gridView1.GetRowCellValue(item, "MAYCKT").ToString();
                    LsMaYCdcChon.Add(ma1);
                    dem++;
                }
                if (dem == 0)
                {
                    MessageBox.Show("Bạn chưa chọn yêu cầu để phê duyệt.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    // Người phê duyệt phòng ban ( QLTC phòng ban trở lên)                        
                    string NgPDPB = CommonUser.UserStatic.MANV + DateTime.Now.ToString("-dd/MM/yyyy HH:mm:ss tt");
                    int DemPD = 0;
                    foreach (string item in LsMaYCdcChon)
                    {
                        QlyYcSDvaTaiPMDTO a = QlyYcSDvaTaiPMDAO.Instance.GetYcSDPMDTO(item);

                        string PdPB = a.PDPB;

                        if (PdPB == "") // Chưa phê duyệt PB thì mới phê duyệt
                        {
                            QlyYcSDvaTaiPMDAO.Instance.UpdatePDPB(item, NgPDPB);
                            DemPD++;
                        }
                    }
                    MessageBox.Show($"Đã phê duyệt {DemPD} yêu cầu được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DemPD = 0;
                    dem = 0;

                }
            }
            else
            {
                MessageBox.Show("Bạn chưa đủ thẩm quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadControl();
        }

        private void btnPDIT_Click(object sender, EventArgs e)
        {

            // Chức năng của IT: Nút chức năng dùng cho phòng ban IT + quyền từ phê duyệt sơ cấp trở lên

            string PhongBan = CommonUser.UserStatic.PHONGBAN;
            if ((PhongBan == "IT" || PhongBan == "ADMIN") && idquyen >= 4)
            {
                // Cho phép chọn nhiều nhân viên để phê duyệt.
                int dem = 0;
                List<string> LsMaYCdcChon = new List<string>();
                foreach (var item in gridView1.GetSelectedRows())
                {
                    string ma1 = gridView1.GetRowCellValue(item, "MAYCKT").ToString();
                    LsMaYCdcChon.Add(ma1);
                    dem++;
                }
                if (dem == 0)
                {
                    MessageBox.Show("Bạn chưa chọn yêu cầu để phê duyệt.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    // Người phê duyệt phòng IT( QLSC trở lên)

                    string NgPDIT = CommonUser.UserStatic.MANV + DateTime.Now.ToString("-dd/MM/yyyy HH:mm:ss tt");

                    int DemPD = 0;

                    foreach (string item in LsMaYCdcChon)
                    {
                        QlyYcSDvaTaiPMDTO a = QlyYcSDvaTaiPMDAO.Instance.GetYcSDPMDTO(item);
                        string PdIT = a.PDIT;
                        string PdPB = a.PDPB;
                        if (PdPB != "" && PdIT == "")
                        {
                            QlyYcSDvaTaiPMDAO.Instance.UpdatePDIT(item, NgPDIT);
                            DemPD++;
                        }
                    }

                    MessageBox.Show($"Đã phê duyệt {DemPD} yêu cầu được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DemPD = 0;
                    dem = 0;
                }
            }
            else
            {
                MessageBox.Show("Bạn không đủ thẩm quyền cho chức năng này!", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadControl();
        }


        private void searchLookUpEdit3View_CustomDrawRowIndicator_1(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }


        private void btnCapNhatMT_Click_1(object sender, EventArgs e)
        {

                frmDSMayTinhCD f = new frmDSMayTinhCD() ;
                f.ShowDialog();
           
                string MaMTCD = "" ;

             

        }

        private void sglMaPMsan_EditValueChanged(object sender, EventArgs e)
        {
            string MaPM = sglMaPMsan.EditValue.ToString();
            CommonPMdc.MaPMdc = MaPM;
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string MaYC = view.GetRowCellValue(e.RowHandle, view.Columns["MAYCKT"]).ToString();
            QlyYcSDvaTaiPMDTO YC = QlyYcSDvaTaiPMDAO.Instance.GetYcSDPMDTO(MaYC);
            if (YC.PDPB == "" && YC.PDIT == "" && YC.HTYC == "")
            {
                e.Appearance.BackColor = btnChoPBPD.Appearance.BackColor;
            }
            if (YC.PDPB != "" && YC.PDIT == "" && YC.HTYC == "")
            {
                e.Appearance.BackColor = btnChoITPD.Appearance.BackColor;
            }
            if (YC.PDPB != "" && YC.PDIT != "" && YC.HTYC == "")
            {
                e.Appearance.BackColor =btnDangTH.Appearance.BackColor;
            }
            if (YC.PDPB != "" && YC.PDIT != "" && YC.HTYC != "")
            {
                e.Appearance.BackColor = btnHoanTat.Appearance.BackColor;
            }
        }
    }
}