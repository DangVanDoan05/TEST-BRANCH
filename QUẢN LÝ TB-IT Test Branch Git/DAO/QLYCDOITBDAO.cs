using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class QLYCDOITBDAO
    {
        private static QLYCDOITBDAO instance;

        public static QLYCDOITBDAO Instance
        {
            get { if (instance == null) instance = new QLYCDOITBDAO(); return QLYCDOITBDAO.instance; }
            private set { QLYCDOITBDAO.instance = value; }
        }

        private QLYCDOITBDAO(){}

        public List<string> GetLsNgayYcDaSX()  // Cần phải sắp xếp theo thời gian.
        {
            string query = "SELECT DISTINCT NGAYYC FROM QLYCDOITB ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<NgayYcDTO> LsNgayYC = new List<NgayYcDTO>();
            foreach (DataRow item in data.Rows)
            {
                NgayYcDTO a = new NgayYcDTO(item);
                LsNgayYC.Add(a);
            }

            int dodai = LsNgayYC.Count();
            DateTime[] MangNgayYC = new DateTime[dodai];
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
            for (int k = 0; k < dodai; k++)
            {
                string NgayYC = MangNgayYC[k].ToString("dd/MM/yyyy");
                LsNgayYCDaSX.Add(NgayYC);
            }
            return LsNgayYCDaSX;
        }

     

        public List<QLYCDOITBDTO> GetAllYcDOITB()  // Cần phải sắp xếp theo thời gian.
        {
            string query = "select * from QLYCDOITB ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            // Lấy List ngày yêu cầu đã sắp xếp
            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();
            List<QLYCDOITBDTO> lsv = new List<QLYCDOITBDTO>();
            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    QLYCDOITBDTO a = new QLYCDOITBDTO(item1);
                    if (a.NGAYYC == item)
                        lsv.Add(a);
                }
            }

            return lsv;
        }


        public QLYCDOITBDTO GetYcDoiTBDTO(string MaYCKT)
        {
            string query = "select * from QLYCDOITB where MAYCKT= @MaYCKT ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaYCKT });
            QLYCDOITBDTO a = new QLYCDOITBDTO(data.Rows[0]);
            return a;
        }


        public List<QLYCDOITBDTO> GetLsYcDoiTBOfPB(string MaPB)
        {
            string query = "select * from QLYCDOITB where PB= @pb ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPB });
            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();

            List<QLYCDOITBDTO> lsv = new List<QLYCDOITBDTO>();

            foreach (string item in LsNgayYCDaSX)
            {

                foreach (DataRow item1 in data.Rows)
                {
                    QLYCDOITBDTO a = new QLYCDOITBDTO(item1);

                    if (a.NGAYYC == item)
                    {
                        lsv.Add(a);
                    }
                }
            }
            return lsv;
        }


        public List<QLYCDOITBDTO> GetYCwithNvIT(string MaNVIT)
        {
            string query = " select * from QLYCDOITB ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            // Lấy List ngày yêu cầu đã sắp xếp
            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();
            List<QLYCDOITBDTO> lsv = new List<QLYCDOITBDTO>();

            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    QLYCDOITBDTO a = new QLYCDOITBDTO(item1);
                    string NgLapYC = a.NGLAPYC;
                    string[] d = NgLapYC.Split('-');
                    if (((a.NGAYYC == item) && (a.PDPB != "") && (a.PDIT != "")) || ((a.NGAYYC == item) && (d[0] == MaNVIT)))
                    {
                        lsv.Add(a);
                    }
                }
            }
            return lsv;

        }


        public List<QLYCDOITBDTO> GetYCwithQlyIT(string MaNVQlyIT)
        {
            string query = " select * from QLYCDOITB ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            // Lấy List ngày yêu cầu đã sắp xếp

            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();
            List<QLYCDOITBDTO> lsv = new List<QLYCDOITBDTO>();

            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    QLYCDOITBDTO a = new QLYCDOITBDTO(item1);
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
                        if ((a.NGAYYC == item) && (a.PDPB != ""))
                        {
                            lsv.Add(a);
                        }
                    }
                }
            }
            return lsv;
        }

        public List<PhongBanDTO> GetLsvPbThuocBP(string MaBP)
        {
            string query = " select * from PHONGBAN where BOPHAN= @bophan ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaBP });
            List<PhongBanDTO> lsv = new List<PhongBanDTO>();
            foreach (DataRow item in data.Rows)
            {
                PhongBanDTO phongBanDTO = new PhongBanDTO(item);
                lsv.Add(phongBanDTO);
            }
            return lsv;
        }

        public List<QLYCDOITBDTO> GetLsYcDoiTBOfBP(string MaBoPhan)
        {
            List<QLYCDOITBDTO> lsvYCOfBP = new List<QLYCDOITBDTO>();
            List<PhongBanDTO> LsPbOfBP = QLYCDOITBDAO.Instance.GetLsvPbThuocBP(MaBoPhan);
            foreach (PhongBanDTO item in LsPbOfBP)
            {
                string MaPB = item.MAPB;
                List<QLYCDOITBDTO> lsvYCOfPB = GetLsYcDoiTBOfPB(MaPB);
                foreach (QLYCDOITBDTO item1 in lsvYCOfPB)
                {
                    lsvYCOfBP.Add(item1);
                }
            }
            return lsvYCOfBP;
        }
     
        public List<QLYCDOITBDTO> GetYCwithNgLapYC(string MaNVlap)
        {
            string query = " select * from QLYCDOITB ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            // Lấy List ngày yêu cầu đã sắp xếp
            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();
            List<QLYCDOITBDTO> lsv = new List<QLYCDOITBDTO>();
            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    QLYCDOITBDTO a = new QLYCDOITBDTO(item1);
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

        // QLYCDOITB(MAYCKT, PB, NGAYYC, TBDOI, SOLUONG, MTSD, LOIHT, VITRISD, PPTT, VDKTT, NGLAPYC, PDPB, PDIT, HTYC)
        public int Insert(string MaYCKT, string Phongban, string NgayYC, string TBdoi,int Soluong, string MaytinhSD, string LoiHtai,string ViTriSDHT
                      , string PPThayThe,string VdeKoTT, string NglapYC, string PDPB, string PDIT, string HoanTatYC)
        {
            string query = "insert  QLYCDOITB(MAYCKT, PB, NGAYYC, TBDOI, SOLUONG, MTSD, LOIHT, VITRISD, PPTT, VDKTT, NGLAPYC, PDPB, PDIT, HTYC)" +
                           " values ( @maYCKT , @PB , @NgayYC , @tb , @sl , @mtsd , @loiHT , @VTSD , @pptt , @vdktt , @NglapYC , @PDPB , @PDIT , @HTYC )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaYCKT,  Phongban,  NgayYC,TBdoi, Soluong,  MaytinhSD,  LoiHtai, ViTriSDHT
                      , PPThayThe,VdeKoTT, NglapYC,  PDPB,  PDIT, HoanTatYC});
            return data;
        }

          
        public int UpdatePDPB(string MaYCKT, string PheDuyetPB)
        {
            string query = "update QLYCDOITB set PDPB= @PDPB  where  MAYCKT= @MaYCKT ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { PheDuyetPB, MaYCKT });
            return data;
        }

        public bool CheckPDDoiTBPB(string MaPB)  // Tình trạng 1: Người lập đã xác nhận
        {
            string query = " select * from QLYCDOITB where IDTINHTRANG= 1 and PHONGBAN= @pb ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPB });
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
        public bool CheckPDDoiTbIT()
        {
            string query = " select * from QLYCDOITB where IDTINHTRANG = 2 ";
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

        public bool CheckPCTHIT()
        {
            string query = " select * from QLYCDOITB where IDTINHTRANG= 4 ";
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

        public int UpdatePDIT(string MaNV, string tinhtrangYC)
        {
            string query = "update QLYCDOITB set PDIT= @PDIT  where MAYCKT= @MaYCKT ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {  tinhtrangYC, MaNV });
            return data;
        }


        // QLYCDOITB(MAYCKT, PB, NGAYYC, TBDOI, SOLUONG, MTSD, LOIHT, VITRISD, PPTT, VDKTT, NGLAPYC, PDPB, PDIT, HTYC)


        public int UpdateHTYC(string MaNV, string HoanTatYC)
        {
            string query = "update QLYCDOITB set HTYC= @HT where MAYCKT= @MaYCKT ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {HoanTatYC, MaNV });
            return data;
        }

        public int Delete(string MaYCKT)
        {
            string query = " DELETE QLYCDOITB WHERE MAYCKT= @MaYCKT ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaYCKT });
            return data;
        }

    }
}
