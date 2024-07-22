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
using DevExpress.XtraBars;

namespace frmMain.YeuCauKyThuat
{
    public partial class frmDoiTBIT : DevExpress.XtraEditors.XtraForm
    {
        public frmDoiTBIT()
        {
          InitializeComponent();
          
           LoadControl();
        }

        bool them;
        int idquyen = CommonUser.Quyen;
        string MaYCKTdc = "";
     
        private void searchLookUpEdit2View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }


     
    
        private void LoadControl()
        {         
            LockControl(true);
            LoadData();
            CleanText();          
        }

        void CleanText()
        {
            txtSoLuong.Clear();
            txtMTSD.Clear();
            txtVTTSsd.Clear();
            txtLoiHT.Clear();         
            txtVdeKhongTT.Clear();
        }

        private void LockControl(bool kt)
        {
            if (kt)
            {
                sglTbDoi.Enabled = false;
                txtSoLuong.Enabled = false;
                txtMTSD.Enabled = false;
                txtLoiHT.Enabled = false;             
                txtVTTSsd.Enabled = false;
                txtLoiHT.Enabled = false;
                cbPPTT.Enabled = false;
                txtVdeKhongTT.Enabled = false;              

                btnThem.Enabled = true;               
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
            }
            else
            {
                sglTbDoi.Enabled = true;
                txtSoLuong.Enabled = true;
                txtMTSD.Enabled = true;
                txtLoiHT.Enabled = true;
                txtVTTSsd.Enabled = true;
                txtLoiHT.Enabled = true;
                cbPPTT.Enabled = true;
                txtVdeKhongTT.Enabled = true;

                btnThem.Enabled = false;             
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;
            }
        }

        private void LoadData()
        {
            string PhongBan = CommonUser.UserStatic.PHONGBAN;

            // Load thiết bị thay thế.

            sglTbDoi.Properties.DataSource = TonLinhKienDAO.Instance.GetTable();
            sglTbDoi.Properties.DisplayMember = "MALK";
            sglTbDoi.Properties.ValueMember = "MALK";

            // Load phương pháp thay thế. 
            Name ITTT = new Name("Nhờ IT thay thế.");
            Name PBTT = new Name("Phòng ban tự thay thế.");

            List<Name> LsPPTT = new List<Name>();
            LsPPTT.Add(ITTT);
            LsPPTT.Add(PBTT);
            cbPPTT.DataSource = LsPPTT;
            cbPPTT.ValueMember = "NAME";
            cbPPTT.DisplayMember = "NAME";


            // LOAD BẢNG QUẢN LÝ



            if (PhongBan == "IT" || PhongBan == "ADMIN")
            {

                if (PhongBan == "ADMIN")     //phòng ADMIN: ADMIN thì Load toàn bộ
                {
                    gridControl1.DataSource = QLYCDOITBDAO.Instance.GetAllYcDOITB();
                }
                else    //phòng IT
                {
                    if (idquyen >= 4)     // Phê duyệt sơ cấp trở lên
                    {
                        gridControl1.DataSource = QLYCDOITBDAO.Instance.GetYCwithQlyIT(CommonUser.UserStatic.MANV);
                    }
                    else          
                    {
                        if (idquyen >= 2)  // Load cả những yêu cầu mà phòng IT lập
                        {
                            gridControl1.DataSource = QLYCDOITBDAO.Instance.GetYCwithNvIT(CommonUser.UserStatic.MANV);
                        }
                    }                 
                }
            }

            else   // Với phòng ban khác
            {
                if (idquyen >= 7) // Tổng giám đốc
                {
                    gridControl1.DataSource = QLYCDOITBDAO.Instance.GetAllYcDOITB();
                }
                else
                {
                    if (idquyen >= 6) // QLCC: Load bộ phận
                    {
                        gridControl1.DataSource = QLYCDOITBDAO.Instance.GetLsYcDoiTBOfBP(CommonUser.UserStatic.BOPHAN);
                    }
                    else
                    {
                        if (idquyen == 5) // QLTC Load yêu cầu của phòng ban
                        {
                            gridControl1.DataSource = QLYCDOITBDAO.Instance.GetLsYcDoiTBOfPB(CommonUser.UserStatic.PHONGBAN);
                        }
                        else // QLSC trở xuống load những yêu cầu của mình lập
                        {
                            gridControl1.DataSource = QLYCDOITBDAO.Instance.GetYCwithNgLapYC(CommonUser.UserStatic.MANV);
                        }
                    }
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (idquyen >= 2)
            {
                them = true;
                LockControl(false);

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

       

        private void searchLookUpEdit1View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        void Save()
        {
           if(them)
            {              
                string Phongban = CommonUser.UserStatic.PHONGBAN;
                string MaYCKT = CommonUser.UserStatic.MANV + DateTime.Now.ToString("-dd/MM/yyyy HH:mm:ss tt");
                string NgayYC = DateTime.Now.ToString("dd/MM/yyyy");

                try
                {

                        string TBdoi = sglTbDoi.EditValue.ToString();                  
                        int SlTon = TonLinhKienDAO.Instance.GetMaLKTon(TBdoi).SLTON;
                    try
                    {


                        int SoluongSD = int.Parse(txtSoLuong.Text);
                        if (SoluongSD <= SlTon)
                        {
                            string MaytinhSD = txtMTSD.Text;
                            string LoiHT = txtLoiHT.Text;
                            string VitriSD = txtVTTSsd.Text;
                            string VdeKoTT = txtVdeKhongTT.Text;
                            if (MaytinhSD != "" && LoiHT != "" && VitriSD != "" && VdeKoTT != "")
                            {
                                string PPTT = cbPPTT.SelectedValue.ToString();
                                string NglapYC = CommonUser.UserStatic.MANV + "-" + NgayYC + DateTime.Now.ToString(" HH:mm:ss tt");
                                string PdPB = "";
                                string PDIT = "";
                                string HoantatYC = "";
                                QLYCDOITBDAO.Instance.Insert(MaYCKT, Phongban, NgayYC, TBdoi, SoluongSD, MaytinhSD, LoiHT, VitriSD, PPTT, VdeKoTT, NglapYC, PdPB, PDIT, HoantatYC);
                            }
                            else
                            {
                                MessageBox.Show("Điền đầy đủ thông tin cần thiết.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Số lượng sử dụng vượt quá hạn mức.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Nhập số lượng sử dụng là số nguyên.", "LỖI:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("Chọn thiết bị muốn đổi ", "LỖI:",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            them = false;          
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
                MessageBox.Show($"Bạn không đủ thẩm quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void gridControl1_Click(object sender, EventArgs e)
        {

            // QLYCDOITB(MAYCKT, MANV, HOTEN, PHONGBAN, NGAYYC, LOAITBDOI, MAYTINHSD, VITRITSSDHT, LOIHIENTAI, PPTHAYTHE, VANDEKTT, NGUOILAPYC, PHEDUYETPB, PHEDUYETIT, THUCHIENYC, IDTINHTRANG, TINHTRANGYCKT)

            try
            {
                txtVTTSsd.Text = gridView1.GetFocusedRowCellValue("VITRITSSDHT").ToString();
                txtLoiHT.Text = gridView1.GetFocusedRowCellValue("LOIHIENTAI").ToString();
                cbPPTT.Text = gridView1.GetFocusedRowCellValue("PPTHAYTHE").ToString();
                txtVdeKhongTT.Text = gridView1.GetFocusedRowCellValue("VANDEKTT").ToString();              
            }
            catch
            {

            }

        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            int IDTinhTrangYC = int.Parse(view.GetRowCellValue(e.RowHandle, view.Columns["IDTINHTRANG"]).ToString());
            string MaYCKT = view.GetRowCellValue(e.RowHandle, view.Columns["MAYCKT"]).ToString();
          
           
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
                if ( PhongBan == "IT" ) // Phòng IT: IT không thể xóa khi đã hoàn tất
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

      

        private void btnPDPB_Click(object sender, EventArgs e)
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
                        QLYCDOITBDTO a = QLYCDOITBDAO.Instance.GetYcDoiTBDTO(item);
                        string PdPB = a.PDPB;

                        if (PdPB == "") // Chưa phê duyệt PB thì mới phê duyệt
                        {
                            QLYCDOITBDAO.Instance.UpdatePDPB(item, NgPDPB);
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
                        QLYCDOITBDTO a = QLYCDOITBDAO.Instance.GetYcDoiTBDTO(item);
                        string PdIT = a.PDIT;
                        string PdPB = a.PDPB;
                        if (PdPB != "" && PdIT == "")
                        {
                            QLYCDOITBDAO.Instance.UpdatePDIT(item, NgPDIT);
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

      

        private void btnHoanTatYC_Click(object sender, EventArgs e)
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
                        QLYCDOITBDTO Yc = QLYCDOITBDAO.Instance.GetYcDoiTBDTO(item);
                        string PDPB = Yc.PDPB;
                        string PDIT = Yc.PDIT;
                        string HoanTatYC = Yc.HTYC;                                              
                        if (PDPB != "" && PDIT != "" && HoanTatYC == "")
                        {
                            QLYCDOITBDAO.Instance.UpdateHTYC(item, NgHT);

                            // UPDATE TỒN LINH KIỆN.
                            string MaLKSD = Yc.TBDOI;
                            TonLinhKienDTO TonDTO = TonLinhKienDAO.Instance.GetMaLKTon(MaLKSD);
                            int SlTon = TonDTO.SLTON;
                            int SlSD = Yc.SOLUONG;
                            TonLinhKienDAO.Instance.UpdateSLTON(MaLKSD, SlTon - SlSD);

                            //GHI, LƯU LẠI THÔNG TIN VÀO BẢNG THỐNG KÊ XUẤT

                            // Insert(string MaTKxuat, string malk, string tenlk, string ngayxuat, int slxuat, string dvtinh, string ncc, string nguoixuat, string YCKTSD, string MDSD, int idTT, string chitietTTKK)
                            String MaYCKT = Yc.MAYCKT;
                            string MaTKXUAT = "DOITB-" + MaYCKT;
                            string TenLk = TonDTO.TENLK;
                            string NgayXuat = DateTime.Now.ToString("dd/MM/yyyy");
                            string DvTinh = TonDTO.DVTINH;
                            string NCC = MaLinhKienDAO.Instance.GetRowMaLK(MaLKSD).NCC;
                            string NguoiXuat = CommonUser.UserStatic.MANV + "-" + CommonUser.UserStatic.FULLNAME;
                            string mdsd = "Yêu cầu đổi thiết bị IT.";
                            TinhTrangKiemKeDTO tinhtrangBD = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(0); // Thêm vào bảng thống kê xuất
                            ThongKeXuatDAO.Instance.Insert(MaTKXUAT, MaLKSD, TenLk, NgayXuat, SlSD, DvTinh, NCC, NguoiXuat, MaYCKT, mdsd, tinhtrangBD.IDTTKIEMKE, tinhtrangBD.CHITIETTTKK);

                            DemHtat++;
                        }                       
                    }
                    MessageBox.Show($"Đã hoàn tất {DemHtat} yêu cầu đổi thiết bị.", "THÀNH CÔNG:", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void gridView1_RowCellStyle_1(object sender, RowCellStyleEventArgs e)
        {

            GridView view = sender as GridView;
            string MaYC = view.GetRowCellValue(e.RowHandle, view.Columns["MAYCKT"]).ToString();
            QLYCDOITBDTO YC = QLYCDOITBDAO.Instance.GetYcDoiTBDTO(MaYC);
            if (YC.PDPB == "" && YC.PDIT == "" && YC.HTYC == "")
            {
                e.Appearance.BackColor = btnChoPDPB.Appearance.BackColor;
            }
            if (YC.PDPB != "" && YC.PDIT == "" && YC.HTYC == "")
            {
                e.Appearance.BackColor = btnChoPDIT.Appearance.BackColor;
            }
            if (YC.PDPB != "" && YC.PDIT != "" && YC.HTYC == "")
            {
                e.Appearance.BackColor = btnDangTHYC.Appearance.BackColor;
            }
            if (YC.PDPB != "" && YC.PDIT != "" && YC.HTYC != "")
            {
                e.Appearance.BackColor = btnHoanTat.Appearance.BackColor;
            }

        }

        private void gridControl1_Click_1(object sender, EventArgs e)
        {
            try
            {
                MaYCKTdc = gridView1.GetFocusedRowCellValue("MAYCKT").ToString();   
                QLYCDOITBDTO a = QLYCDOITBDAO.Instance.GetYcDoiTBDTO(MaYCKTdc);
                sglTbDoi.EditValue = a.TBDOI;
                txtSoLuong.Text = a.SOLUONG + "";
                txtMTSD.Text = a.MTSD;
                txtLoiHT.Text = a.MTSD;
                txtVTTSsd.Text = a.VITRISD;
                cbPPTT.SelectedValue = a.PPTT;
                txtVdeKhongTT.Text = a.VDKTT;
            }
            catch 
            {

            }           
        }
    }
}