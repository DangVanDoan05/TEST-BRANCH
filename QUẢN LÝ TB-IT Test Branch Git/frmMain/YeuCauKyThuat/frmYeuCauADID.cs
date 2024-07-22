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
    public partial class frmYeuCauADID : DevExpress.XtraEditors.XtraForm
    {
        public frmYeuCauADID()
        {
            InitializeComponent();
            
            LoadControl();
        }

        bool them;
        int idquyen = CommonUser.Quyen;


      

        private void LoadControl()
        {                         
            LoadData();
            LockControl(true);
            CleanText();
            LoadGl();
        }

        private void LoadGl() 
        {
            //load sgl Nhân viên


            if (idquyen >= 6)  // QLCC trở lên.
            {
                if (idquyen >= 7)      //  tổng giám đốc
                {
                    sglMaNV.Properties.DataSource = QLNhanVienDAO.Instance.GetAllNVNoADID();
                    sglMaNV.Properties.DisplayMember = "MANV"; 
                    sglMaNV.Properties.ValueMember = "MANV"; 
                }
                else      // Trưởng bộ phận.
                {
                    int BacCV = ChucVuDAO.Instance.GetBacCV(CommonUser.UserStatic.CHUCVU);
                    sglMaNV.Properties.DataSource = QLNhanVienDAO.Instance.GetNVOfBPDuoiCVNoADID(CommonUser.UserStatic.BOPHAN);
                    sglMaNV.Properties.DisplayMember = "MANV";
                    sglMaNV.Properties.ValueMember = "MANV";
                }
            }
            else   // QLTC trở xuống: lấy người trong cùng phòng ban và chức vụ thấp hơn mình.
            {
                int BacCV = ChucVuDAO.Instance.GetBacCV(CommonUser.UserStatic.CHUCVU);
                sglMaNV.Properties.DataSource = QLNhanVienDAO.Instance.GetNVOfPBDuoiCVNoADID(CommonUser.UserStatic.PHONGBAN, BacCV);
                sglMaNV.Properties.DisplayMember = "MANV";
                sglMaNV.Properties.ValueMember = "MANV";
            }


            //load combox phan loai
            #region data cb phân loai
            Name ADIDuser = new Name("ADID nhân viên.");
            Name ADIDchung = new Name("ADID dùng chung.");
            List<Name> LsLoaiADID = new List<Name>();
            LsLoaiADID.Add(ADIDuser);
            LsLoaiADID.Add(ADIDchung);

            cbLoaiYCADID.DataSource = LsLoaiADID;
            cbLoaiYCADID.ValueMember = "NAME";
            cbLoaiYCADID.DisplayMember = "NAME";
            #endregion

        }

        private void CleanText()
        {
            cbLoaiYCADID.Text = "";
            sglMaNV.Text = "";
            radCoMail.Checked = false;
        }

        private void LockControl(bool kt)
        {
            if(kt)
            {
                cbLoaiYCADID.Enabled = false;
                cbLoaiYCADID.SelectedValue = "ADID dùng chung.";
                sglMaNV.Enabled = false;             
                radCoMail.Enabled = false;
                
                btnThem.Enabled = true;             
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
            }
            else
            {

                cbLoaiYCADID.Enabled = true;
                sglMaNV.Enabled = false;
                radCoMail.Enabled = false;

                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;

            }
        }

        private void LoadData()
        {

            string PhongBan = CommonUser.UserStatic.PHONGBAN;

            if (PhongBan == "IT" || PhongBan == "ADMIN")
            {
                if (PhongBan == "ADMIN")     //phòng ADMIN: ADMIN thì Load toàn bộ
                {
                    gridControl1.DataSource = QLyYCADIDDAO.Instance.GetLSAllYcADID();
                }
                else    //phòng IT
                {

                    if (idquyen >= 4) // Phê duyệt sơ cấp trở lên
                    {
                        // Yêu cầu từ phòng ban khác thì chỉ Load những yêu cầu đã đc phê duyệt phòng ban
                        // Yêu cầu từ phòng ban IT thì phải Load toàn bộ.

                        gridControl1.DataSource = QLyYCADIDDAO.Instance.GetYCwithQlyIT(CommonUser.UserStatic.MANV);
                    }
                    else      // Quyền từ xác nhận trở xuống là người thực hiện yêu cầu.       
                    {
                        if (idquyen >= 2)
                        {
                            gridControl1.DataSource = QLyYCADIDDAO.Instance.GetYCwithNvIT(CommonUser.UserStatic.MANV);
                        }
                    }

                    // Người đc phâm công chỉ Load những yêu cầu ma mình được phân công:

                }
            }

            else   // Với phòng ban khác
            {
                if (idquyen >= 7) // Tổng giám đốc
                {
                    gridControl1.DataSource = QLyYCADIDDAO.Instance.GetLSAllYcADID();
                }
                else
                {
                    if (idquyen >= 6) // QLCC: Load bộ phận
                    {
                        gridControl1.DataSource = QLyYCADIDDAO.Instance.GetLsYcADIDOfBP(CommonUser.UserStatic.BOPHAN);
                    }
                    else
                    {
                        if (idquyen == 5) // QLTC Load yêu cầu của phòng ban
                        {
                            gridControl1.DataSource = QLyYCADIDDAO.Instance.GetLsYcADIDOfPB(CommonUser.UserStatic.PHONGBAN);
                        }
                        else // QLSC trở xuống load những yêu cầu của mình lập
                        {
                            gridControl1.DataSource = QLyYCADIDDAO.Instance.GetYCwithNgLapYC(CommonUser.UserStatic.MANV);  // Load những yêu cầu IDTINHTRANG<= 1
                        }
                    }
                }
            }



        }

        void Save()
        {
            //Insert(string MaNV, string Hoten, string Phongban, string Nhamay, string nhom, string LoaiADIS, string ADID, string MKBD, string Mail, string ngayYC,
            //  string NgLapYC, string PdPB, string PDIT, string HoantatYC)
            string LoaiYCADID = cbLoaiYCADID.SelectedValue.ToString();
            if(LoaiYCADID!="")
            {
                if (LoaiYCADID == "ADID nhân viên.")
                {
                    string MaNV = "";
                    MaNV = sglMaNV.EditValue.ToString();
                    if(MaNV=="")
                    {
                        MessageBox.Show("Chưa chọn mã nhân viên.", "Lỗi:");
                    }
                    else
                    {
                        bool Check = QLyYCADIDDAO.Instance.CheckExistADID(MaNV);
                        if (Check)
                        {
                            MessageBox.Show("Mã nhân viên đã đăng ký ADID.", "Lỗi:",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                        else
                        {

                        //    QLNhanVienDTO NV = QLNhanVienDAO.Instance.GetNhanVienDTO(MaNV);
                        //    string Hoten = NV.FULLNAME;
                        //    string Phongban = NV.PHONGBAN;
                        //    string Nhamay = CommonUser.NhaMayDN;
                        //    string Nhom = NV.NHOM;
                        //    string ADID = "";
                        //    string MKBD = "";
                        //    string Mail = "";
                        //    if(radCoMail.Checked)
                        //    {
                        //        Mail = "Có";
                        //    }
                        //    else
                        //    {
                        //        Mail = "Không";
                        //    }
                        //    string NgayYC = DateTime.Now.ToString("dd/MM/yyyy");
                        //    string NglapYC = CommonUser.UserStatic.MANV + "-" + NgayYC + DateTime.Now.ToString(" HH:mm:ss tt");
                        //    string PdPB = "";
                        //    string PDIT = "";
                        //    string HoantatYC = "";
                        //    int stt = 0 ;
                        //    QLyYCADIDDAO.Instance.Insert(MaNV, Hoten, Phongban, Nhamay, Nhom, LoaiYCADID, ADID, MKBD, Mail, NgayYC, NglapYC, PdPB, PDIT, HoantatYC,stt);

                        }
                    }
                }
               else  // ADID dùng chung
                {
                    string Hoten = "";
                    string Phongban = CommonUser.UserStatic.PHONGBAN;
                    string Nhamay = CommonUser.NhaMayDN;
                    string Nhom = CommonUser.UserStatic.NHOM;                   
                                                                                    
                   
                    string MKBD = "";
                    string Mail = "Không";
                    string NgayYC = DateTime.Now.ToString("dd/MM/yyyy");
                    string NglapYC = CommonUser.UserStatic.MANV + "-" + NgayYC + DateTime.Now.ToString(" HH:mm:ss tt");
                    string PdPB = "";
                    string PDIT = "";
                    string HoantatYC = "";
                    int stt = 0;
                    try
                    {
                        stt = int.Parse(QLyYCADIDDAO.Instance.GetSTTMaxADIDchung(Phongban).ToString());
                    }
                    catch 
                    {                      
                    }                   
                    stt++;
                    string ADID = "";
                    string MaNV = "";
                    if (stt<10)
                    {
                         ADID = "DD-" + Phongban + $"0{stt}";
                         MaNV = "DD-" + Phongban + $"0{stt}";
                    }
                    else
                    {
                         ADID = "DD-" + Phongban + $"{stt}";
                         MaNV = "DD-" + Phongban + $"{stt}";
                    }
                    QLyYCADIDDAO.Instance.Insert(MaNV, Hoten, Phongban, Nhamay, Nhom, LoaiYCADID, ADID, MKBD, Mail, NgayYC, NglapYC, PdPB, PDIT, HoantatYC,stt);
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn loại ADID muốn tạo.", "Lỗi:");
            }
            them = false;
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

        private void gridControl1_Click(object sender, EventArgs e)
        {

            try
            {
                sglMaNV.EditValue = gridView1.GetFocusedRowCellValue("MANV").ToString();
               
            }
            catch
            {

            }


        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void searchLookUpEdit1View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

      

        private void sglMaNV_EditValueChanged(object sender, EventArgs e)
        {
            string MaNV = sglMaNV.EditValue.ToString();
          //  QLNhanVienDTO NhanVienDTO = QLNhanVienDAO.Instance.GetNhanVienDTO(MaNV);
         
        }

        private void searchLookUpEdit1View_CustomDrawRowIndicator_1(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            if(idquyen>=2)
            {
                LockControl(false);
                them = true;
            }
            else
            {
                MessageBox.Show("Chưa đủ thẩm quyền. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            if (idquyen >= 2)
            {
                Save();
                LoadControl();
            }
            else
            {
                MessageBox.Show("Chưa đủ thẩm quyền. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }          
        }

      

        private void btnXacNhan_Click(object sender, EventArgs e)
        {          
            string MaPB = CommonUser.UserStatic.PHONGBAN;
            //  if (idquyen >= 3 && MaPB!="IT" )

            if (idquyen >= 3 )
            {

                // Cho phép chọn nhiều nhân viên để phê duyệt.

                int dem = 0;
                List<string> LsMaNVdcChon = new List<string>();
                foreach (var item in gridView1.GetSelectedRows())
                {
                    string ma1 = gridView1.GetRowCellValue(item, "MANV").ToString();
                    LsMaNVdcChon.Add(ma1);
                    dem++;
                }
                if(dem == 0)
                {
                    MessageBox.Show("Bạn chưa chọn nhân viên để xác nhận.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn xác nhận cho {dem} nhân viên được chọn.", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {
                       // // Người xác nhận

                       // string MaNVlapYC = CommonUser.UserStatic.MANV;
                       // string TenNVlapYC = CommonUser.UserStatic.FULLNAME;
                       // string ThoiGianXN = DateTime.Now.ToString("ddMMyyyy HHmmss");
                       // string NgLapYC = MaNVlapYC + "_" + TenNVlapYC + "_" + ThoiGianXN;

                       // // Người phê duyệt phòng ban(QL trung cấp phòng ban.)

                       // UserDTO QLTCPB = UserDAO.Instance.GetQLTCPB(CommonUser.UserStatic.PHONGBAN);
                       //// string TenPB = PhongBanDAO.Instance.GetPBDTO(CommonUser.UserStatic.PHONGBAN).TENPB;
                       // string TenNVQLTC = QLTCPB.FULLNAME;
                       // string ChitietTT = $"Đang chờ QLTC phòng {TenPB}: {TenNVQLTC} phê duyệt. ";

                       // TinhTrangYCKTDTO tinhtrang = TinhTrangYCKTDAO.Instance.GetChiTietQuyen(1); //Tình trạng 1: người lập YC đã xác nhận.

                       // int DemXN = 0;

                       // foreach (string item in LsMaNVdcChon)
                       // {

                       //     //QLyYCADIDDTO a = QLyYCADIDDAO.Instance.GetYcADIDDTO(item);
                       //     //int idtt = a.IDTINHTRANG;
                       //     //if (idtt >= 0 && idtt < 1)  // Tình trạng chưa xác nhận
                       //     //{
                       //     //    QLyYCADIDDAO.Instance.UpdateXNngLap(item, NgLapYC, tinhtrang.IDTINHTRANG, ChitietTT);
                       //     //    DemXN++;
                       //     //}                                                  
                       // }

                       // MessageBox.Show($"Đã xác nhận {DemXN} nhân viên đăng ký ADID được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       // dem = 0;
                       // DemXN = 0;
                    }
                }
            }

            else
            {
                MessageBox.Show("Bạn chưa đủ thẩm quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadControl();
        }

        private void gridControl1_Click_1(object sender, EventArgs e)
        {
            try
            {
                sglMaNV.EditValue = gridView1.GetFocusedRowCellValue("MANV").ToString();
              
            }
            catch
            {

            }
        }

        private void btnPDIT_Click(object sender, EventArgs e)
        {
            // Chức năng của IT: Nút chức năng dùng cho phòng ban IT + quyền từ phê duyệt sơ cấp trở lên

            string PhongBan = CommonUser.UserStatic.PHONGBAN;
            if ((PhongBan == "IT"||PhongBan=="ADMIN") && idquyen>=4)
            {
                // Cho phép chọn nhiều nhân viên để phê duyệt.
                int dem = 0;
                List<string> LsMaNVdcChon = new List<string>();
                foreach (var item in gridView1.GetSelectedRows())
                {
                    string ma1 = gridView1.GetRowCellValue(item, "MANV").ToString();
                    LsMaNVdcChon.Add(ma1);
                    dem++;
                }
                if (dem == 0)
                {
                    MessageBox.Show("Bạn chưa chọn nhân viên để phê duyệt.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                   
                        // Người phê duyệt phòng IT( QLSC trở lên)
                                          
                        string NgPDIT = CommonUser.UserStatic.MANV + DateTime.Now.ToString("-dd/MM/yyyy HH:mm:ss tt");

                        int DemPD = 0;

                        foreach (string item in LsMaNVdcChon)
                        {
                            QLyYCADIDDTO a = QLyYCADIDDAO.Instance.GetYcADIDDTO(item);
                            string  PdIT = a.PDIT;
                            string PdPB = a.PDPB;
                            if (PdPB!=""&&PdIT=="") 
                            {
                                QLyYCADIDDAO.Instance.UpdatePDIT(item, NgPDIT);
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

        private string GetMail(string HoTen) 
        {

            string tenthuong = RemoveSign4VietnameseString(HoTen).ToLower();
            string[] arrayHoTen = tenthuong.Split(' ');
            int dodai = arrayHoTen.Length;
            string Ho = arrayHoTen[0];
            string Ten = arrayHoTen[dodai - 1];
            string ChuoiGiua = Ten + "." + Ho;
            string Chuoitruoc = "";
            string Chuoisau = "";
            string Hauto = "";
            string Mail = "";
            if(CommonUser.NhaMayDN=="DD1")
            {
                Hauto = "@dongduongpla.com.vn";
            }
            else
            {
                if(CommonUser.NhaMayDN == "DD2")
                {
                    Hauto = "@dongduong2.com";
                }
            }

            if(dodai<=2)
            {
                Mail = Chuoitruoc + ChuoiGiua + Chuoisau + Hauto;
                bool CheckMail2 = QLyYCADIDDAO.Instance.CheckMailExist(Mail);
                if(CheckMail2)
                {
                    Chuoisau =int.Parse(QLyYCADIDDAO.Instance.GetSTTMailChung(Mail).ToString()+1).ToString();
                    Mail = Chuoitruoc + ChuoiGiua + Chuoisau + Hauto;
                }
            }
            else
            {                                
                    if(dodai==3)  // độ dài bằng 3: 0(Họ) , 1(Đệm) , 2(Tên) 
                    {
                        for (int i = 1; i <= dodai - 2; i++)
                        {
                            string KTlot = arrayHoTen[i].Substring(0, 1).ToLower();
                            Chuoisau = Chuoisau + KTlot; // Lấy đầu tên Lót để sau
                        }
                        Mail = Chuoitruoc + ChuoiGiua + Chuoisau + Hauto;
                        bool CheckMail3 = QLyYCADIDDAO.Instance.CheckMailExist(Mail);
                        if(CheckMail3)
                        {
                            Chuoitruoc = arrayHoTen[dodai - 2];
                            Chuoisau = "";
                            Mail = Chuoitruoc + ChuoiGiua + Chuoisau + Hauto;// Đảo tên lót lên đầu
                            bool CheckMail4 = QLyYCADIDDAO.Instance.CheckMailExist(Mail);
                            if (CheckMail4)
                            {
                                Chuoisau = int.Parse(QLyYCADIDDAO.Instance.GetSTTMailChung(Mail).ToString() + 1).ToString();
                                Mail = Chuoitruoc + ChuoiGiua + Chuoisau + Hauto; // Lấy số thứ tự
                            }
                        }                      
                    }                                                            
                    if(dodai==4)  // độ dài bằng 3: 0(Họ) , 1(Đệm) , 2(Đệm) , 3(Tên) 
                    {
                        for (int i = 1; i <= dodai - 2; i++)
                        {
                            string KTlot = arrayHoTen[i].Substring(0, 1).ToLower();
                            Chuoisau = Chuoisau + KTlot;
                        }
                        Mail = Chuoitruoc + ChuoiGiua + Chuoisau + Hauto; 
                        bool CheckMail1 = QLyYCADIDDAO.Instance.CheckMailExist(Mail);
                        if (CheckMail1)
                        {
                            Chuoitruoc = arrayHoTen[2];
                            Chuoisau = arrayHoTen[1].Substring(0, 1).ToLower();
                            Mail = Chuoitruoc + ChuoiGiua + Chuoisau + Hauto;
                            bool CheckMail2 = QLyYCADIDDAO.Instance.CheckMailExist(Mail);
                            if (CheckMail2)
                            {
                                Chuoisau = int.Parse(QLyYCADIDDAO.Instance.GetSTTMailChung(Mail).ToString() + 1).ToString();
                                Mail = Chuoitruoc + ChuoiGiua + Chuoisau + Hauto;
                            }
                        }
                       
                    }                                                                          
            }
            
            return Mail;
        }

        private void btnHoanTatYC_Click(object sender, EventArgs e)
        {                    
            string PhongBan = CommonUser.UserStatic.PHONGBAN;
            if ((PhongBan == "IT"||PhongBan=="ADMIN") && idquyen >= 2)
            {             
                int dem = 0;
                List<string> LsMaNVdcChon = new List<string>();
                foreach (var item in gridView1.GetSelectedRows())
                {
                    string ma1 = gridView1.GetRowCellValue(item, "MANV").ToString();
                    LsMaNVdcChon.Add(ma1);
                    dem++;
                }
                
                if(dem == 0)    
                {
                    MessageBox.Show("Bạn chưa chọn yêu cầu để cập nhật hoàn tất.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {                                   
                  
                    
                        int DemHtat = 0;
                        string NgHT = CommonUser.UserStatic.MANV + DateTime.Now.ToString("-dd/MM/yyyy HH:mm:ss tt");
                        foreach (string item in LsMaNVdcChon)
                        {
                              
                            string MaNV = item;
                            QLyYCADIDDTO YcADIDDTO = QLyYCADIDDAO.Instance.GetYcADIDDTO(MaNV);
                            string PDPB = YcADIDDTO.PDPB;
                            string PDIT = YcADIDDTO.PDIT;
                            string HoanTatYC = YcADIDDTO.HTYC;
                            string LoaiADID = YcADIDDTO.LOAIADID;
                            if (LoaiADID == "ADID dùng chung.")
                            {
                                if (HoanTatYC == "" && PDIT != "")
                                {
                                    QLyYCADIDDAO.Instance.UpdateHTADchung(item, NgHT);
                                    DemHtat++;
                                }
                            }
                            else
                            {
                                                             
                                    string HoTenNV = YcADIDDTO.HOTEN;
                                    // Cắt chuỗi lấy ký tự đầu và ký tự cuối
                                    string[] arrayHoTen = HoTenNV.Split(' ');
                                    int dodai = arrayHoTen.Length;
                                    string Ho = arrayHoTen[0];
                                    string Ten = arrayHoTen[dodai - 1];
                                    string Kytudau = Ho.Substring(0, 1).ToLower();
                                    string Kytucuoi = Ten.Substring(0, 1).ToLower();
                                    string HautoViet = Kytudau + Kytucuoi;
                                    string HautoAnh = RemoveSign4VietnameseString(HautoViet);

                                    string ADID = MaNV + HautoAnh;

                                    string KtDau = PhongBan.Substring(0, 1);
                                    int leng = PhongBan.Length;
                                    string KtCuoi = PhongBan.Substring(1, leng-1).ToLower();
                                    string MKBD = KtDau + KtCuoi + "@123"; // Viết thường hết những ký tự sau.


                                    string Mail = "Không";

                                    if (YcADIDDTO.MAIL == "Có")     
                                    {
                                        Mail = GetMail(HoTenNV);                                        
                                    }

                                    if (PDPB!="" && PDIT != ""&& HoanTatYC == "")
                                    {
                                        QLyYCADIDDAO.Instance.UpdateHTADrieng(item, ADID, MKBD, Mail, NgHT);
                                        DemHtat++;
                                    }
                                                       
                            }
                        }
                        MessageBox.Show($"Đã hoàn tất {DemHtat} yêu cầu đăng ký ADID.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dem = 0;
                        DemHtat = 0;
                    

                }
            }
            else
            {
                MessageBox.Show("Bạn không đủ thẩm quyền cho chức năng này!", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadControl();
        }

        private void btnPDPB_Click(object sender, EventArgs e)
        {          
            if (idquyen>= 5)
            {              
                int dem = 0;
                List<string> LsMaNVdcChon = new List<string>();
                foreach (var item in gridView1.GetSelectedRows())
                {
                    string ma1 = gridView1.GetRowCellValue(item, "MANV").ToString();
                    LsMaNVdcChon.Add(ma1);
                    dem++;
                }
                if (dem == 0)
                {
                    MessageBox.Show("Bạn chưa chọn nhân viên để phê duyệt.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                  
                        // Người phê duyệt phòng ban ( QLTC phòng ban trở lên)                        
                        string NgPDPB = CommonUser.UserStatic.MANV  + DateTime.Now.ToString("-dd/MM/yyyy HH:mm:ss tt");
                                                                                     
                        int DemPD = 0 ;
                        foreach (string item in LsMaNVdcChon)
                        {
                            QLyYCADIDDTO a = QLyYCADIDDAO.Instance.GetYcADIDDTO(item);
                            string  PdPB = a.PDPB;

                            if (PdPB=="") // Chưa phê duyệt PB thì mới phê duyệt
                            {
                                QLyYCADIDDAO.Instance.UpdatePDPB(item, NgPDPB);
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


        private void gridView1_CustomDrawRowIndicator_1(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {           
            int dem = 0;
            List<string> LsMaNVdcChon = new List<string>();
            foreach (var item in gridView1.GetSelectedRows())
            {
                string ma1 = gridView1.GetRowCellValue(item, "MANV").ToString();
                LsMaNVdcChon.Add(ma1);
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
                            DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} yêu cầu đăng ký ADID được chọn.", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (kq == DialogResult.Yes)
                            {
                                int DemXoa = 0;
                                foreach (string item in LsMaNVdcChon)
                                {
                                    QLyYCADIDDTO a = QLyYCADIDDAO.Instance.GetYcADIDDTO(item);
                                    if(a.HTYC=="")
                                    {
                                        QLyYCADIDDAO.Instance.Delete(item);
                                        DemXoa++;
                                    }                                  
                                }
                                MessageBox.Show($"Đã xóa {DemXoa} yêu cầu được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chưa đủ thẩm quyền cho chức năng này. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} yêu cầu đăng ký ADID được chọn.", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (kq == DialogResult.Yes)
                            {
                                foreach (string item in LsMaNVdcChon)
                                {
                                    QLyYCADIDDAO.Instance.Delete(item);
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
                                DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} yêu cầu đăng ký ADID được chọn.", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (kq == DialogResult.Yes)
                                {
                                    int demXoa = 0;
                                    foreach (string item in LsMaNVdcChon)
                                    {
                                        QLyYCADIDDTO a = QLyYCADIDDAO.Instance.GetYcADIDDTO(item);
                                        string PdIT = a.PDIT;
                                        if(PdIT=="") // Chưa phê duyệt IT
                                        {
                                            QLyYCADIDDAO.Instance.Delete(item);
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
                                DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} yêu cầu đăng ký ADID được chọn.", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if(kq == DialogResult.Yes)
                                {
                                    int demXoa = 0;
                                    foreach (string item in LsMaNVdcChon)
                                    {
                                        QLyYCADIDDTO a = QLyYCADIDDAO.Instance.GetYcADIDDTO(item);

                                        string PdPB = a.PDPB;

                                        if (PdPB=="") // Chưa phê duyệt phòng ban.
                                        {
                                            QLyYCADIDDAO.Instance.Delete(item);
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

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnCapNhat_Click_1(object sender, EventArgs e)
        { 
            if(idquyen>=2)
            {
                LoadControl();
            }
            else
            {
                MessageBox.Show("Chưa đủ thẩm quyền. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                           
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string MaNV = view.GetRowCellValue(e.RowHandle, view.Columns["MANV"]).ToString();
            QLyYCADIDDTO YC = QLyYCADIDDAO.Instance.GetYcADIDDTO(MaNV);

            if (YC.PDPB==""&&YC.PDIT==""&&YC.HTYC=="") 
            {
                 e.Appearance.BackColor = txtChoPDPB.BackColor;
            }
            if (YC.PDPB != "" && YC.PDIT == "" && YC.HTYC == "") 
            {
                e.Appearance.BackColor = txtChoPDIT.BackColor;
            }
            if (YC.PDPB != "" && YC.PDIT != "" && YC.HTYC == "")
            {
                e.Appearance.BackColor = txtDangTH.BackColor;
            }
            if (YC.PDPB != "" && YC.PDIT != "" && YC.HTYC != "")  
            {
                e.Appearance.BackColor = txtHoanTat.BackColor;
            }         
        }

        private void cbLoaiYCADID_SelectedValueChanged(object sender, EventArgs e)
        {
            string LoaiADID = cbLoaiYCADID.SelectedValue.ToString();
            if (LoaiADID == "ADID nhân viên."&&them)
            {
                sglMaNV.Enabled = true;
                radCoMail.Enabled = true;
            }
            else
            {
                sglMaNV.Enabled = false;
                radCoMail.Enabled = false;
            }
        }

      
    }
 }
