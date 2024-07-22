using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
   public class QLyYCADIDDAO
    {
        private static QLyYCADIDDAO instance;

        public static QLyYCADIDDAO Instance
        {
            get { if (instance == null) instance = new QLyYCADIDDAO(); return QLyYCADIDDAO.instance; }
            private set { QLyYCADIDDAO.instance = value; }
        }

        private QLyYCADIDDAO() { }

        public List<QLyYCADIDDTO> GetLSAllYcADID()  // Cần phải sắp xếp theo thời gian.
        {
            string query = "select * from QLYCADID ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            // Lấy List ngày yêu cầu đã sắp xếp
            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();
            List<QLyYCADIDDTO> lsv = new List<QLyYCADIDDTO>();
            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    QLyYCADIDDTO a = new QLyYCADIDDTO(item1);
                    if (a.NGAYYC == item)
                        lsv.Add(a);
                }
            }
            return lsv;
        }

        public List<QLyYCADIDDTO> GetLSAllYcADIDDaHT()  // Cần phải sắp xếp theo thời gian.
        {
            string query = "select * from QLYCADID ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            // Lấy List ngày yêu cầu đã sắp xếp
            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();
            List<QLyYCADIDDTO> lsv = new List<QLyYCADIDDTO>();
            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    QLyYCADIDDTO a = new QLyYCADIDDTO(item1);
                    if (a.NGAYYC == item&&a.HTYC!="")
                        lsv.Add(a);
                }
            }
            return lsv;
        }


        public List<QLyYCADIDDTO> GetLSAllYcADIDDaHTOfPB(string MaPB)  // Cần phải sắp xếp theo thời gian.
        {
            string query = "select * from QLYCADID where PB= @PB ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPB });
            // Lấy List ngày yêu cầu đã sắp xếp
            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();
            List<QLyYCADIDDTO> lsv = new List<QLyYCADIDDTO>();
            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    QLyYCADIDDTO a = new QLyYCADIDDTO(item1);
                    if (a.NGAYYC == item && a.HTYC != "")
                        lsv.Add(a);
                }
            }
            return lsv;
        }


        public List<QLyYCADIDDTO> GetLsYcADIDDaHTOfBP(string MaBoPhan)
        {
            List<QLyYCADIDDTO> lsvYCOfBP = new List<QLyYCADIDDTO>();
            List<PhongBanDTO> LsPbOfBP = PhongBanDAO.Instance.GetLsvPbThuocBP(MaBoPhan);
            foreach (PhongBanDTO item in LsPbOfBP)
            {
                string MaPB = item.MAPB;
                List<QLyYCADIDDTO> lsvYCOfPB = GetLSAllYcADIDDaHTOfPB(MaPB);
                foreach (QLyYCADIDDTO item1 in lsvYCOfPB)
                {
                    lsvYCOfBP.Add(item1);
                }
            }

            return lsvYCOfBP;

        }



        public List<string> GetLsNgayYcDaSX()  // Cần phải sắp xếp theo thời gian.
        {
            string query = "SELECT DISTINCT NGAYYC FROM QLYCADID ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<NgayYcDTO> LsNgayYC = new List<NgayYcDTO>();
            foreach (DataRow item in data.Rows)
            {
                NgayYcDTO a = new NgayYcDTO(item);
                LsNgayYC.Add(a);
            }

            int dodai = LsNgayYC.Count();
            DateTime [] MangNgayYC = new DateTime[dodai];
            int m = 0;
            foreach (NgayYcDTO item in LsNgayYC)
            {
                MangNgayYC[m] = item.NGAYYC;
                m++;
            }
            // Sắp xếp ngày yêu cầu:
            // Tiến hành sắp xếp mảng thời gian đặt hàng theo thứ tự giảm dần
            for (int i = 0; i < dodai - 1; i++)
            {
                for (int j = i + 1; j < dodai; j++)
                {
                    if (MangNgayYC[j] > MangNgayYC[i])
                    {
                        DateTime trunggian = MangNgayYC[j];
                        MangNgayYC[j] = MangNgayYC[i];
                        MangNgayYC[i] = trunggian;
                    }
                }
            }
            List<string> LsNgayYCDaSX = new List<string>();

            for (int k = 0; k < dodai ; k++)
            {
                string NgayYC = MangNgayYC[k].ToString("dd/MM/yyyy");
                LsNgayYCDaSX.Add(NgayYC);
            }
            return LsNgayYCDaSX;
        }

        public QLyYCADIDDTO GetYcADIDDTO(string MaNV) 
        {
            string query = "select * from QLYCADID where MANV= @maNV ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNV });                    
            QLyYCADIDDTO a = new QLyYCADIDDTO(data.Rows[0]);                           
            return a;
        }


        public List<QLyYCADIDDTO> GetLsYcADIDOfPB(string MaPB)  
        {         
            string query = "select * from QLYCADID where PB= @pb ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPB });         
            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();

            List<QLyYCADIDDTO> lsv = new List<QLyYCADIDDTO>();

            foreach (string item in LsNgayYCDaSX)  
            {

                foreach (DataRow item1 in data.Rows)
                {
                    QLyYCADIDDTO a = new QLyYCADIDDTO(item1);
                  
                    if (a.NGAYYC == item)
                    {
                        lsv.Add(a);
                    }
                }
            }                  
            return lsv;

        }


        public List<QLyYCADIDDTO> GetYCwithNvIT(string MaNVIT)  
        {
         
            string query = " select * from QLYCADID "; 
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            // Lấy List ngày yêu cầu đã sắp xếp
            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();
            List<QLyYCADIDDTO> lsv = new List<QLyYCADIDDTO>();
            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    QLyYCADIDDTO a = new QLyYCADIDDTO(item1);
                    string NgLapYC = a.NGLAPYC;
                    string[] d = NgLapYC.Split('-');
                    if( ((a.NGAYYC == item) && (a.PDPB != "")&&(a.PDIT!=""))||((a.NGAYYC == item) &&(d[0]==MaNVIT)))
                    {
                        lsv.Add(a);
                    }
                }
            }
            return lsv;

        }


        public List<QLyYCADIDDTO> GetYCwithQlyIT(string MaNVQlyIT) 
        {         
            string query = " select * from QLYCADID "; 
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            // Lấy List ngày yêu cầu đã sắp xếp
            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();
            List<QLyYCADIDDTO> lsv = new List<QLyYCADIDDTO>();

            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    QLyYCADIDDTO a = new QLyYCADIDDTO(item1);
                    string PhongBan = a.PB;
                    if (PhongBan == "IT") // Với phòng ban IT lấy toàn bộ yêu cầu phòng IT
                    {
                        if ((a.NGAYYC == item))
                        {
                            lsv.Add(a);
                        }                                             
                    }
                    else  // Phòng ban khác: Load toàn bộ những yêu cầu đã được phê duyệt PB
                    {                      
                        if ((a.NGAYYC == item) && (a.PDPB!=""))
                        {
                            lsv.Add(a);
                        }
                    }
                }
            }
            return lsv;
        }

        public List<QLyYCADIDDTO> GetLsYcADIDOfBP(string MaBoPhan) 
        {
            List<QLyYCADIDDTO> lsvYCOfBP = new List<QLyYCADIDDTO>();
            List<PhongBanDTO> LsPbOfBP = PhongBanDAO.Instance.GetLsvPbThuocBP(MaBoPhan);
            foreach (PhongBanDTO item in LsPbOfBP)
            {
                string MaPB = item.MAPB;
                List<QLyYCADIDDTO> lsvYCOfPB = GetLsYcADIDOfPB(MaPB);
                foreach (QLyYCADIDDTO item1 in lsvYCOfPB)
                {
                    lsvYCOfBP.Add(item1);
                }
            }
            return lsvYCOfBP;
        }
        
        public List<QLyYCADIDDTO> GetYCwithNgLapYC(string MaNVlap)  
        {
            string query = " select * from QLYCADID ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            // Lấy List ngày yêu cầu đã sắp xếp
            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();
            List<QLyYCADIDDTO> lsv = new List<QLyYCADIDDTO>();
            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    QLyYCADIDDTO a = new QLyYCADIDDTO(item1);
                    string NgLapYC = a.NGLAPYC;
                    string[] d = NgLapYC.Split('-');
                    if (((a.NGAYYC == item) && (d[0] == MaNVlap)))
                    {
                        lsv.Add(a);
                    }
                }
            }
            return lsv;
        }


        public bool CheckExistADID( string MaNV)
        {
            string query =" select * from QLYCADID where MANV= @maNV ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNV });
            int dem = data.Rows.Count;
            if (dem > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckMailExist(string Mail)
        {
            string query = " select * from QLYCADID where MAIL= @MAIL ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { Mail });
            int dem = data.Rows.Count;
            if (dem > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object GetSTTMailChung(string Mail)
        {
            string query = "SELECT MAX(STT) FROM QLYCADID WHERE Mail = @Mail ";
            object data = DataProvider.Instance.ExecuteScalar(query, new object[] { Mail });
            return data;
        }

        public object GetSTTMaxADIDchung(string MaPB)
        {
            string query = "SELECT MAX(STT) FROM QLYCADID WHERE LOAIADID = 'ADID dùng chung.' and PB= @pb ";
            object data = DataProvider.Instance.ExecuteScalar(query, new object[] { MaPB });          
            return data;
        }

        public int Insert(string MaNV , string Hoten,string Phongban, string Nhamay, string nhom, string LoaiADID,string ADID,string MKBD, string Mail, string ngayYC,
                        string NgLapYC, string PdPB, string PDIT, string HoantatYC,int STT )
        {
            string query = "insert QLYCADID(MANV, HOTEN, PB, NHAMAY, NHOM, LOAIADID, ADID, MKBD, MAIL, NGAYYC, NGLAPYC, PDPB, PDIT, HTYC,STT)" +
                        " values ( @maNV , @Hoten , @PB , @NM , @nhom , @LoaiADID , @ADID , @MK , @mail , @ngayYC , @Nglap , @PDPB , @PDIT , @HTYC , @stt )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaNV ,  Hoten, Phongban, Nhamay,  nhom,  LoaiADID,ADID, MKBD,  Mail, ngayYC,
                         NgLapYC,  PdPB,  PDIT,  HoantatYC ,STT });
            return data;
        }

        // HÀM SỬA 

     

      
        public int UpdatePDPB(string MaNV, string PheDuyetPB)
        {
            string query = "update QLYCADID set PDPB= @PDPB  where MANV= @MaNV ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { PheDuyetPB, MaNV });
            return data;
        }

        public bool CheckPDADIDPB(string MaPB)  
        {
            string query = " select * from QLYCADID where IDTINHTRANG= 1 and PHONGBAN= @pb ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {MaPB});
            int dem = data.Rows.Count;
            if (dem > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckPDADIDIT()
        {
            string query = " select * from QLYCADID where IDTINHTRANG = 2 ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            int dem = data.Rows.Count;
            if(dem>0)
            {
                return true;
            }
            else
            {
                return false;
            }           
        }

        public bool CheckPCTHIT()
        {
            string query = " select * from QLYCADID where IDTINHTRANG= 4 ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            int dem = data.Rows.Count;
            if (dem > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int UpdatePDIT(string MaNV, string PheDuyetIT)
        {
            string query = "update QLYCADID set PDIT= @PDIT where MANV= @MaNV ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { PheDuyetIT , MaNV });
            return data;
        }

      
        public int UpdateHTADrieng(string MaNV,string ADID,string MKMD,string Mail, string NgHoanTatYC)
        {
            string query = " update QLYCADID set ADID= @adid , MKBD= @MKBD , MAIL= @mail , HTYC= @HTYC  where MANV= @MaNV ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ADID,  MKMD , Mail , NgHoanTatYC , MaNV });
            return data;
        }

        public int UpdateHTADchung(string MaNV, string NgHoanTatYC)
        {
            string query = "update QLYCADID set HTYC= @HTYC  where MANV= @MaNV ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { NgHoanTatYC, MaNV });
            return data;
        }


        public int Delete(string maNV)
        {
            string query = " DELETE QLYCADID WHERE MANV= @maNV ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maNV });
            return data;
        }

    }
}
