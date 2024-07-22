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

namespace frmMain.Quan_Ly_May_Tinh
{
    public partial class frmLichSuBT : DevExpress.XtraEditors.XtraForm
    {
        public frmLichSuBT()
        {
            InitializeComponent();
            LoadControl();
        }


        string MaMT = "";
        string MaHMBT = "";
        bool them;

        private void LoadControl()
        {
            DeleteMaBTChuaLuu();
            LockControl(true);
            LoadData();
            CleanText();
        }

        private void DeleteMaBTChuaLuu()
        {

            // Nếu không tồn tại trong bảng lịch sử thì xóa vật tư sử dụng, vật tư thu hồi đi.
            if(CommonMaBaoTri.MaBaoTri!="")
            {
                bool CheckExistLichSu = LichSuBTDAO.Instance.CheckExist(CommonMaBaoTri.MaBaoTri);
                if(!CheckExistLichSu)
                {
                    QLyVTSDDAO.Instance.Delete(CommonMaBaoTri.MaBaoTri);
                    QLyVTTHDAO.Instance.Delete(CommonMaBaoTri.MaBaoTri);
                }
            }

        }

        private void CleanText()
        {
            sglMaMT.EditValue = "";
            sglHangMucBT.EditValue = "";
            MaMT = "";
            MaHMBT = "";
        }

        private void LoadData()
        {
            gridControl1.DataSource = LichSuBTDAO.Instance.GetLsBT();

            // Load dữ liệu Edit Lookup

            sglMaMT.Properties.DataSource = QuanLyMayTinhDAO.Instance.GetListMaMT();
            sglMaMT.Properties.DisplayMember = "MAMT";
            sglMaMT.Properties.ValueMember = "MAMT";

            sglHangMucBT.Properties.DataSource = HangMucBaoTriDAO.Instance.GetTable();
            sglHangMucBT.Properties.DisplayMember = "MAHM";
            sglHangMucBT.Properties.ValueMember = "MAHM";

        }

        // Có sử dụng vật tư và thu hồi vật tư
        private void LockControl(bool kt)
        {
            if(kt) // TRẠNG THÁI BAN ĐẦU.
            {
                
                sglMaMT.Enabled = false;
                dtpNgay.Enabled = false;
                sglHangMucBT.Enabled = false;
                btnCNVTSD.Enabled = false;
                txtGhiChu.Enabled= false;
                                          
                btnThem.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
              
            }
            else
            {                
                sglMaMT.Enabled = true;
                dtpNgay.Enabled = true;
                sglHangMucBT.Enabled =true;
                btnCNVTSD.Enabled = true;
                txtGhiChu.Enabled = true;
               

                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;             
            }
           
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnCNVTSD_Click(object sender, EventArgs e)
        {
                      
                if(MaHMBT==""||MaMT=="")
                {
                    MessageBox.Show("Chưa chọn mã máy tính và hạng mục bảo trì.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Load bảng tồn linh kiện xem có vật tư đang kiểm kê không thì sẽ không cho nhập
                    bool CheckLKDangKK = TonLinhKienDAO.Instance.CheckExistMaLKDangKK();
                    if(CheckLKDangKK)
                    {
                        MessageBox.Show("Không thể cập nhật do có linh kiện đang kiểm kê.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        CommonMaBaoTri.MaBaoTri = MaMT + "-" + dtpNgay.Value.ToString("dd/MM/yyyy") + "-" + MaHMBT;
                        bool CheckMaBTExitst = LichSuBTDAO.Instance.CheckExist(CommonMaBaoTri.MaBaoTri);

                            if(CheckMaBTExitst)
                            {
                                MessageBox.Show($"Đã tồn tại hạng mục '{MaHMBT}' cho máy tính {MaMT} vào ngày {dtpNgay.Value.ToString("dd/MM/yyyy")}", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                frmVatTuSD f = new frmVatTuSD(false);
                                f.ShowDialog();
                            }
                                             
                    }                  
                }
                
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LockControl(false);
            them = true;
        }

        private void searchLookUpEdit1View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }
         
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // khi lưu thì mới tác động đến tồn, thống kê nhập xuất , liên quan đến thống kê thu hồi
            // Thêm vật tư, nếu không nhấn lưu thì xóa vật tư đi.
            if(them)
            {              
                if (CommonMaBaoTri.MaBaoTri == "")
                {
                    if (MaHMBT == "" || MaMT == "")
                    {
                        MessageBox.Show("Chưa chọn mã máy tính và hạng mục bảo trì.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        CommonMaBaoTri.MaBaoTri = MaMT + "-" + dtpNgay.Value.ToString("dd/MM/yyyy") + "-" + MaHMBT;
                        bool CheckMaBTExitst = LichSuBTDAO.Instance.CheckExist(CommonMaBaoTri.MaBaoTri);

                        if (CheckMaBTExitst)
                        {
                            MessageBox.Show($"Đã tồn tại hạng mục '{MaHMBT}' cho máy tính {MaMT} vào ngày {dtpNgay.Value.ToString("dd/MM/yyyy")}", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {

                            QuanLyMayTinhDTO a = QuanLyMayTinhDAO.Instance.GetMaMT(MaMT);
                            string NhaMay = a.NHAMAY;
                            string Phongban = a.PB;
                            CommonMaBaoTri.MaMayTinh = MaMT;
                            string NgayBT = dtpNgay.Value.ToString("dd/MM/yyyy");
                            string HangmucBT = sglHangMucBT.EditValue.ToString();
                            string VTSD = "Không";
                            bool CheckSDVT = QLyVTSDDAO.Instance.CheckSDVT(CommonMaBaoTri.MaBaoTri);
                            if (CheckSDVT)
                            {
                                VTSD = "Có";
                            }
                            string VTTH = "Không";
                            bool CheckTHVT = QLyVTTHDAO.Instance.CheckTHVT(CommonMaBaoTri.MaBaoTri);
                            if (CheckTHVT)
                            {
                                VTTH = "Có";
                            }
                            TinhTrangKiemKeDTO TTKK = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(0); // tình trạng 0 ban đầu đang chưa kiểm kê 
                            string Ghichu = txtGhiChu.Text;
                            string NguoiCN = CommonUser.UserStatic.MANV + "-" + CommonUser.UserStatic.FULLNAME;

                            //  Insert(string MaBT, string MaMT, string NhaMay, string PB, string Ngay, string HangMucBT, string VTSD, string VTTH, bool TrangThai, string ghichu, string NguoiCN)
                            DialogResult kq = MessageBox.Show($"Bạn muốn thêm thông tin sửa chữa cho mã máy tính {MaMT}.", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            if (kq == DialogResult.Yes)
                            {
                                // Kiểm điều kiện kiểm kê.
                                if (!CheckSDVT && !CheckTHVT)
                                {
                                    TinhTrangKiemKeDTO TTKK3 = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(3); // Hạng mục không cần kiểm kê.

                                    // Thêm vào bảng lịch sử bảo trì.

                                    LichSuBTDAO.Instance.Insert(CommonMaBaoTri.MaBaoTri, MaMT, NhaMay, Phongban, NgayBT, HangmucBT, VTSD, VTTH, TTKK3.IDTTKIEMKE, TTKK3.CHITIETTTKK, Ghichu, NguoiCN);
                                }
                                else
                                {
                                    // Thêm vào bảng lịch sử bảo trì.

                                    LichSuBTDAO.Instance.Insert(CommonMaBaoTri.MaBaoTri, MaMT, NhaMay, Phongban, NgayBT, HangmucBT, VTSD, VTTH, TTKK.IDTTKIEMKE, TTKK.CHITIETTTKK, Ghichu, NguoiCN);

                                }

                                // Vật tư sử dụng -----> Tồn kho, Thống kê xuất.

                                // Tồn kho:

                                if (CheckSDVT)
                                {
                                    List<QLyVTSDDTO> lsVTSDdto = QLyVTSDDAO.Instance.GetLsVtOfMaBT(CommonMaBaoTri.MaBaoTri);
                                    foreach (QLyVTSDDTO item in lsVTSDdto)
                                    {
                                        // Với mỗi linh kiện thì Update Tồn và Thống kê xuất.
                                        // Update số lượng tồn

                                        string MaLK = item.MAVTSD;
                                        TonLinhKienDTO TonMaLK = TonLinhKienDAO.Instance.GetMaLKTon(MaLK);
                                        int SLTON = TonMaLK.SLTON;
                                        int SLSD = item.SLVTSD;
                                        int TonMoi = SLTON - SLSD;// Không chạy vào đây rồi, sai rồi
                                        MessageBox.Show(MaLK + "   " + TonMoi);
                                        TonLinhKienDAO.Instance.UpdateSLTON(MaLK, TonMoi);

                                        // THÊM TRONG BẢNG THỐNG KÊ XUẤT.

                                        string MaTKxuat = CommonMaBaoTri.MaBaoTri + "-" + MaLK;  // Không thể trùng mã này được.
                                        MaLinhKienDTO MaLKDTO = MaLinhKienDAO.Instance.GetRowMaLK(MaLK);
                                        string TenLK = MaLKDTO.TENLK;
                                        string ncc = MaLKDTO.NCC;
                                        string ngayxuat = DateTime.Now.ToString("dd/MM/yyyy");
                                        string dvtinh = MaLKDTO.DVTINH;
                                        int slxuat = SLSD;
                                        string NguoiXuat = CommonUser.UserStatic.MANV + "-" + CommonUser.UserStatic.FULLNAME;
                                        string ycktSD = CommonMaBaoTri.MaBaoTri;
                                        string MDSD = $" Bảo dưỡng máy tính  {CommonMaBaoTri.MaMayTinh} ";
                                        TinhTrangKiemKeDTO tinhtrangBD = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(0);
                                        ThongKeXuatDAO.Instance.Insert(MaTKxuat, MaLK, TenLK, ngayxuat, SLSD, dvtinh, ncc, NguoiXuat, ycktSD, MDSD, tinhtrangBD.IDTTKIEMKE, tinhtrangBD.CHITIETTTKK);
                                    }
                                }

                                // Vật tư thu hồi -----> Tồn thu hồi, thống kê thu hồi.

                                // Tồn kho thu hồi:

                                if (CheckTHVT)
                                {
                                    List<QLyVTTHDTO> lsVTTHdto = QLyVTTHDAO.Instance.GetLsVtOfMaBT(CommonMaBaoTri.MaBaoTri);
                                    foreach (QLyVTTHDTO item in lsVTTHdto)
                                    {

                                        // Với mỗi linh kiện thì Update Tồn thu hồi và Thống kê thu hồi LK:
                                        // UPDATE SỐ LƯỢNG TỒN TRONG BẢNG TỒN LINH KIỆN THU HỒI
                                        string MaVtu = item.MAVTTH;
                                        MaLinhKienDTO MaLKdto = MaLinhKienDAO.Instance.GetRowMaLK(MaVtu);
                                        bool CheckVtuTonTH = TonLKThuHoiDAO.Instance.CheckMaLKTHTon(MaVtu);
                                        if (!CheckVtuTonTH) // Nếu chưa tồn tại thì thêm vào bảng.
                                        {
                                            TinhTrangKiemKeDTO KiemKeBD = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(0); // tình trạng 0: đang chưa kiểm kê
                                            TonLKThuHoiDAO.Instance.Insert(MaVtu, MaLKdto.TENLK, item.SLVTTH, MaLKdto.DVTINH, KiemKeBD.IDTTKIEMKE, KiemKeBD.CHITIETTTKK);
                                        }
                                        else
                                        {
                                            // Nếu đã tồn tại thì lấy số lượng tồn

                                            TonLKThuHoiDTO a1 = TonLKThuHoiDAO.Instance.GetTonLKTHDTO(MaVtu);
                                            int TonCu = a1.SLTON;
                                            TonLKThuHoiDAO.Instance.UpdateSLTON(MaVtu, TonCu + item.SLVTTH);
                                        }


                                        // THÊM VÀO BẢNG THỐNG KÊ THU HỒI

                                        string MaTKTH = CommonMaBaoTri.MaBaoTri + "-" + MaVtu; 
                                        MaLinhKienDTO MaLKDTO = MaLinhKienDAO.Instance.GetRowMaLK(MaVtu);
                                        string MaMT = CommonMaBaoTri.MaMayTinh;
                                        QuanLyMayTinhDTO a2 = QuanLyMayTinhDAO.Instance.GetMaMT(MaMT);
                                        string PhongBan = a2.PB;
                                        string NM = a2.NHAMAY;
                                        string TenLK = MaLKDTO.TENLK;
                                        string ncc = MaLKDTO.NCC;
                                        string ngayTH = DateTime.Now.ToString("dd/MM/yyyy");
                                        string dvtinh = MaLKDTO.DVTINH;
                                        int SLTH = item.SLVTTH;
                                        string NguoiTH = CommonUser.UserStatic.MANV + "-" + CommonUser.UserStatic.FULLNAME;

                                        TinhTrangKiemKeDTO tinhtrangBD = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(0);  // Tình trạng 0: đang chưa kiểm kê.

                                        // Thêm vào bảng thống kê thu hồi.

                                        ThongKeTHDAO.Instance.Insert(MaTKTH, MaVtu, TenLK, ngayTH,NM,PhongBan,SLTH,dvtinh,NguoiTH, "Lưu kho.", tinhtrangBD.IDTTKIEMKE, tinhtrangBD.CHITIETTTKK);

                                    }
                                }

                                MessageBox.Show("Thêm lịch sử bảo trì máy tính thành công.", "Thông báo:");
                            }


                        }
                    }

                }
                else // Mã bảo trì khác rỗng.
                {
                    bool CheckMaBTExitst = LichSuBTDAO.Instance.CheckExist(CommonMaBaoTri.MaBaoTri);

                    if (CheckMaBTExitst)
                    {
                        MessageBox.Show($"Đã tồn tại hạng mục '{MaHMBT}' cho máy tính {MaMT} vào ngày {dtpNgay.Value.ToString("dd/MM/yyyy")}", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {



                        QuanLyMayTinhDTO a = QuanLyMayTinhDAO.Instance.GetMaMT(MaMT);
                        string NhaMay = a.NHAMAY;
                        string Phongban = a.PB;
                        CommonMaBaoTri.MaMayTinh = MaMT;
                        string NgayBT = dtpNgay.Value.ToString("dd/MM/yyyy");
                        string HangmucBT = sglHangMucBT.EditValue.ToString();
                        string VTSD = "Không";
                        bool CheckSDVT = QLyVTSDDAO.Instance.CheckSDVT(CommonMaBaoTri.MaBaoTri);
                        if (CheckSDVT)
                        {
                            VTSD = "Có";
                        }
                        string VTTH = "Không";
                        bool CheckTHVT = QLyVTTHDAO.Instance.CheckTHVT(CommonMaBaoTri.MaBaoTri);
                        if (CheckTHVT)
                        {
                            VTTH = "Có";
                        }
                        TinhTrangKiemKeDTO TTKK = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(0); // tình trạng 0 ban đầu đang chưa kiểm kê 
                        string Ghichu = txtGhiChu.Text;
                        string NguoiCN = CommonUser.UserStatic.MANV + "-" + CommonUser.UserStatic.FULLNAME;

                        //  Insert(string MaBT, string MaMT, string NhaMay, string PB, string Ngay, string HangMucBT, string VTSD, string VTTH, bool TrangThai, string ghichu, string NguoiCN)
                        DialogResult kq = MessageBox.Show($"Bạn muốn thêm thông tin sửa chữa cho mã máy tính {MaMT}.", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        if (kq == DialogResult.Yes)
                        {
                            // Kiểm điều kiện kiểm kê.
                            if(!CheckSDVT&&!CheckTHVT)
                            {
                                TinhTrangKiemKeDTO TTKK3 = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(3); // Hạng mục không cần kiểm kê.
                                
                                // Thêm vào bảng lịch sử bảo trì.

                                LichSuBTDAO.Instance.Insert(CommonMaBaoTri.MaBaoTri, MaMT, NhaMay, Phongban, NgayBT, HangmucBT, VTSD, VTTH, TTKK3.IDTTKIEMKE, TTKK3.CHITIETTTKK, Ghichu, NguoiCN);
                            }
                            else
                            {
                                // Thêm vào bảng lịch sử bảo trì.

                                LichSuBTDAO.Instance.Insert(CommonMaBaoTri.MaBaoTri, MaMT, NhaMay, Phongban, NgayBT, HangmucBT, VTSD, VTTH, TTKK.IDTTKIEMKE, TTKK.CHITIETTTKK, Ghichu, NguoiCN);

                            }

                            

                            // Vật tư sử dụng -----> Tồn kho, Thống kê xuất.

                            // Tồn kho:
                            if (CheckSDVT)
                            {
                                List<QLyVTSDDTO> lsVTSDdto = QLyVTSDDAO.Instance.GetLsVtOfMaBT(CommonMaBaoTri.MaBaoTri);
                                foreach (QLyVTSDDTO item in lsVTSDdto)
                                {
                                    // Với mỗi linh kiện thì Update Tồn và Thống kê xuất.
                                    string MaLK = item.MAVTSD;
                                    TonLinhKienDTO TonMaLK = TonLinhKienDAO.Instance.GetMaLKTon(MaLK);
                                    int SLTON = TonMaLK.SLTON;
                                    int SLSD = item.SLVTSD;
                                    TonLinhKienDAO.Instance.UpdateSLTON(MaLK, SLTON - SLSD); // Sai ở đây rồi

                                    // THÊM TRONG BẢNG THỐNG KÊ XUẤT.
                                    string MaTKxuat = CommonMaBaoTri.MaBaoTri + "-" + MaLK;  // Không thể trùng mã này được.
                                    MaLinhKienDTO MaLKDTO = MaLinhKienDAO.Instance.GetRowMaLK(MaLK);
                                    string TenLK = MaLKDTO.TENLK;
                                    string ncc = MaLKDTO.NCC;
                                    string ngayxuat = DateTime.Now.ToString("dd/MM/yyyy");
                                    string dvtinh = MaLKDTO.DVTINH;
                                    int slxuat = SLSD;
                                    string NguoiXuat = CommonUser.UserStatic.MANV + "-" + CommonUser.UserStatic.FULLNAME;
                                    string ycktSD = CommonMaBaoTri.MaBaoTri;
                                    string MDSD = $" Bảo dưỡng máy tính  {CommonMaBaoTri.MaMayTinh} ";
                                    TinhTrangKiemKeDTO tinhtrangBD = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(0);
                                    ThongKeXuatDAO.Instance.Insert(MaTKxuat, MaLK, TenLK, ngayxuat, SLSD, dvtinh, ncc, NguoiXuat, ycktSD, MDSD, tinhtrangBD.IDTTKIEMKE, tinhtrangBD.CHITIETTTKK);
                                }
                            }
                            // Vật tư thu hồi -----> Tồn thu hồi, thống kê thu hồi.

                            // Tồn kho thu hồi:

                            if (CheckTHVT)
                            {
                                List<QLyVTTHDTO> lsVTTHdto = QLyVTTHDAO.Instance.GetLsVtOfMaBT(CommonMaBaoTri.MaBaoTri);
                                foreach (QLyVTTHDTO item in lsVTTHdto)
                                {

                                    // Với mỗi linh kiện thì Update Tồn thu hồi và Thống kê thu hồi LK:
                                    // UPDATE SỐ LƯỢNG TỒN TRONG BẢNG TỒN LINH KIỆN THU HỒI

                                    string MaVtu = item.MAVTTH;
                                    MaLinhKienDTO MaLKdto = MaLinhKienDAO.Instance.GetRowMaLK(MaVtu);
                                    bool CheckVtuTonTH = TonLKThuHoiDAO.Instance.CheckMaLKTHTon(MaVtu);
                                    if (!CheckVtuTonTH) // Nếu chưa tồn tại thì thêm vào bảng.
                                    {
                                        TinhTrangKiemKeDTO KiemKeBD = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(0); // tình trạng 0: đang chưa kiểm kê
                                        TonLKThuHoiDAO.Instance.Insert(MaVtu, MaLKdto.TENLK, item.SLVTTH, MaLKdto.DVTINH, KiemKeBD.IDTTKIEMKE, KiemKeBD.CHITIETTTKK);
                                    }
                                    else
                                    {
                                        // Nếu đã tồn tại thì lấy số lượng tồn

                                        TonLKThuHoiDTO a1 = TonLKThuHoiDAO.Instance.GetTonLKTHDTO(MaVtu);
                                        int TonCu = a1.SLTON;
                                        TonLKThuHoiDAO.Instance.UpdateSLTON(MaVtu, TonCu + item.SLVTTH);

                                    }


                                    // THÊM VÀO BẢNG THỐNG KÊ THU HỒI

                                    string MaTKTH = CommonMaBaoTri.MaBaoTri + "-" + MaVtu;
                                    MaLinhKienDTO MaLKDTO = MaLinhKienDAO.Instance.GetRowMaLK(MaVtu);
                                    string MaMT = CommonMaBaoTri.MaMayTinh;
                                    QuanLyMayTinhDTO a2 = QuanLyMayTinhDAO.Instance.GetMaMT(MaMT);
                                    string PhongBan = a2.PB;
                                    string NM = a2.NHAMAY;
                                    string TenLK = MaLKDTO.TENLK;
                                    string ncc = MaLKDTO.NCC;
                                    string ngayTH = DateTime.Now.ToString("dd/MM/yyyy");
                                    string dvtinh = MaLKDTO.DVTINH;
                                    int SLTH = item.SLVTTH;

                                    string NguoiTH = CommonUser.UserStatic.MANV + "-" + CommonUser.UserStatic.FULLNAME;

                                    TinhTrangKiemKeDTO tinhtrangBD = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(0);  // Tình trạng 0: đang chưa kiểm kê.

                                    ThongKeTHDAO.Instance.Insert(MaTKTH, MaVtu, TenLK, ngayTH,NM,PhongBan, SLTH, dvtinh, NguoiTH, "Lưu kho.", tinhtrangBD.IDTTKIEMKE, tinhtrangBD.CHITIETTTKK);
                                }
                            }
                            MessageBox.Show("Thêm lịch sử bảo trì máy tính thành công.", "Thông báo:");
                        }
                    }

                }
                them = false;
                LoadControl();
            }

        }

        private void btnXemVTSD_Click(object sender, EventArgs e)
        {
            int dem = 0;
            List<LichSuBTDTO> LsMaBtriDcChon = new List<LichSuBTDTO>();

            foreach (var item in gridView1.GetSelectedRows())
            {
                string MaBT = gridView1.GetRowCellValue(item, "MABT").ToString();
                LichSuBTDTO a = LichSuBTDAO.Instance.GetLsBTDTO(MaBT);
                LsMaBtriDcChon.Add(a);
                dem++;
            }
            if(dem==1)
            {

                foreach (LichSuBTDTO item1 in LsMaBtriDcChon)
                {
                    CommonMaBaoTri.MaBaoTri = item1.MABT;
                    frmVatTuSD f = new frmVatTuSD(true);
                    f.ShowDialog();
                }
                
            }
            else
            {
                MessageBox.Show("Chưa chọn mã bảo trì cần xem hoặc không thể xem đc nhiều mã bảo trì cùng lúc", "Lỗi:",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Đang kiểm kê và hoàn tất kiểm kê  thì không được phép xóa.
            int dem = 0;

            List<LichSuBTDTO> LsMaBtriDcChon = new List<LichSuBTDTO>();

            foreach (var item in gridView1.GetSelectedRows())
            {
                string MaBT = gridView1.GetRowCellValue(item,"MABT").ToString();
                LichSuBTDTO a = LichSuBTDAO.Instance.GetLsBTDTO(MaBT);
                LsMaBtriDcChon.Add(a);
                dem++;
            }

            if (dem > 0)
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} mã bảo trì được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    int demXoa = 0;
                    foreach (LichSuBTDTO item in LsMaBtriDcChon)
                    {
                        int IdTTKiemke = item.IDTTKK;
                        if (IdTTKiemke < 1||IdTTKiemke>=3)
                        {
                            // Tình trạng không kiểm kê mới được phép xóa

                            // Với mỗi mã bảo trì được chọn:
                            string VTSD = item.VTSD;
                            string VTTH = item.VTTH;
                            if (VTSD == "Có")
                            {

                                // Xoá trong bảng vật tư sử dụng: 

                                // Update lại số lượng tồn trong bảng tồn linh kiện:

                                List<QLyVTSDDTO> LsVtOfMaBT = QLyVTSDDAO.Instance.GetLsVtOfMaBT(item.MABT);
                                foreach (QLyVTSDDTO item1 in LsVtOfMaBT)
                                {
                                    string MaLK = item1.MAVTSD;
                                    int SLXuatVtu = item1.SLVTSD;
                                    int TonHtai = TonLinhKienDAO.Instance.GetMaLKTon(MaLK).SLTON;
                                    int SltonSau = TonHtai + SLXuatVtu;
                                    TonLinhKienDAO.Instance.UpdateSLTON(MaLK, SltonSau);
                                }

                                // Xóa trong BẢNG VẬT TƯ SỬ DỤNG

                                QLyVTSDDAO.Instance.Delete(item.MABT);

                                // Xóa trong bảng thống kê xuất.

                                foreach (QLyVTSDDTO item2 in LsVtOfMaBT)
                                {
                                    ThongKeXuatDAO.Instance.DeleteMaTKxuat(item.MABT + "-" + item2.MAVTSD);
                                }

                            }

                            if (VTTH == "Có")
                            {

                                // Xoá trong bảng vật tư thu hồi:

                                // Update lại số lượng tồn trong bảng tồn linh kiện thu hồi:

                                // Đã tiêu hủy rồi thì không cộng trả tồn.

                                List<QLyVTTHDTO> LsVtTHOfMaBT = QLyVTTHDAO.Instance.GetLsVtOfMaBT(item.MABT);

                                foreach (QLyVTTHDTO item1 in LsVtTHOfMaBT)
                                {
                                    if(item1.TINHTRANG == "Lưu kho.")
                                    {
                                        // Cập nhật tồn trong bảng thu hồi.

                                        string MaLK = item1.MAVTTH;
                                        int SLXuatVtu = item1.SLVTTH;
                                        int TonHtai = TonLKThuHoiDAO.Instance.GetTonLKTHDTO(MaLK).SLTON;
                                        int SltonSau = TonHtai - SLXuatVtu;
                                        TonLKThuHoiDAO.Instance.UpdateSLTON(MaLK, SltonSau);
                                    }                                  
                                    // Xóa trong thống kê thu hồi linh kiện.  
                                    ThongKeTHDAO.Instance.Delete(item.MABT + "-" + item1.MAVTTH);
                                }                             
                            }

                            // Xóa trong bảng Lịch sử bảo trì:
                            LichSuBTDAO.Instance.Delete(item.MABT);
                            demXoa++;
                        }
                    }
                    MessageBox.Show($"Đã xóa {demXoa} mã bảo trì được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn mã bảo trì để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadControl();


        }

        private void sglMaMT_EditValueChanged(object sender, EventArgs e)
        {
            MaMT = sglMaMT.EditValue.ToString();
        }

        private void sglHangMucBT_EditValueChanged(object sender, EventArgs e)
        {
            MaHMBT = sglHangMucBT.EditValue.ToString();
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;         
            string MaBT = view.GetRowCellValue(e.RowHandle, view.Columns["MABT"]).ToString();
            LichSuBTDTO a = LichSuBTDAO.Instance.GetLsBTDTO(MaBT);

            int IdTTKK = a.IDTTKK;

            if (IdTTKK == 0) // Nếu đang không kiểm kê.
            {
                e.Appearance.BackColor =btnChuaKK.Appearance.BackColor;
            }
            if (IdTTKK == 1) // Đang  kiểm kê.
            {
                e.Appearance.BackColor = btnDangKK.Appearance.BackColor;
            }
            if (IdTTKK == 2) //Hoàn tất kiểm kê.
            {
                e.Appearance.BackColor = btnDaKK.Appearance.BackColor;
            }

        }
    }

}