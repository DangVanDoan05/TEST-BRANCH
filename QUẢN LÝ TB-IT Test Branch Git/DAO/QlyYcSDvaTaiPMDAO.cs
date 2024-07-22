using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class QlyYcSDvaTaiPMDAO
    {
        private static QlyYcSDvaTaiPMDAO instance;

        public static QlyYcSDvaTaiPMDAO Instance
        {
            get { if (instance == null) instance = new QlyYcSDvaTaiPMDAO(); return QlyYcSDvaTaiPMDAO.instance; }
            private set { QlyYcSDvaTaiPMDAO.instance = value; }
        }

        private QlyYcSDvaTaiPMDAO() { }

        public List<QlyYcSDvaTaiPMDTO> GetAllYC()  // Cần phải sắp xếp theo thời gian.
        {
            string query = "select * from QLYCSDVATAIPM ";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            // Lấy List ngày yêu cầu đã sắp xếp
            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();
            List<QlyYcSDvaTaiPMDTO> lsv = new List<QlyYcSDvaTaiPMDTO>();

            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                   QlyYcSDvaTaiPMDTO a = new QlyYcSDvaTaiPMDTO(item1);
                    if (a.NGAYYC == item)
                        lsv.Add(a);
                }
            }
            return lsv;
        }

     

        public List<string> GetLsNgayYcDaSX()  // Cần phải sắp xếp theo thời gian.
        {
            string query = "SELECT DISTINCT NGAYYC FROM QLYCSDVATAIPM ";
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

        public QlyYcSDvaTaiPMDTO GetYcSDPMDTO(string MaYCKT)
        {
            string query = "select * from QLYCSDVATAIPM where MAYCKT= @MaYCKT ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaYCKT });
            QlyYcSDvaTaiPMDTO a = new QlyYcSDvaTaiPMDTO(data.Rows[0]);
            return a;
        }










        public List<QlyYcSDvaTaiPMDTO> GetAllYcTAIPM()  // Cần phải sắp xếp theo thời gian.
        {
            string query = "select * from QLYCSDVATAIPM ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            // Lấy List ngày yêu cầu đã sắp xếp
            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();
            List<QlyYcSDvaTaiPMDTO> lsv = new List<QlyYcSDvaTaiPMDTO>();
            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    QlyYcSDvaTaiPMDTO a = new QlyYcSDvaTaiPMDTO(item1);
                    if (a.NGAYYC == item)
                    {
                        lsv.Add(a);
                    }
                }
            }

            return lsv;
        }
     
        public List<QlyYcSDvaTaiPMDTO> GetLsYcSDPMOfPB(string MaPB)
        {
            string query = " select * from QLYCSDVATAIPM where PHONGBAN= @pb ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPB });
            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();

            List<QlyYcSDvaTaiPMDTO> lsv = new List<QlyYcSDvaTaiPMDTO>();

            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                   QlyYcSDvaTaiPMDTO a = new QlyYcSDvaTaiPMDTO(item1);

                    if (a.NGAYYC == item)
                    {
                        lsv.Add(a);
                    }
                }
            }
            return lsv;
        }


        public List<QlyYcSDvaTaiPMDTO> GetYCwithNvIT(string MaNVIT)
        {
            string query = " select * from QLYCSDVATAIPM ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            // Lấy List ngày yêu cầu đã sắp xếp
            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();

            List<QlyYcSDvaTaiPMDTO> lsv = new List<QlyYcSDvaTaiPMDTO>();

            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    QlyYcSDvaTaiPMDTO a = new QlyYcSDvaTaiPMDTO(item1);
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


        public List<QlyYcSDvaTaiPMDTO> GetYCwithQlyIT(string MaNVQlyIT)
        {
            string query = " select * from QLYCSDVATAIPM ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            // Lấy List ngày yêu cầu đã sắp xếp

            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();
            List<QlyYcSDvaTaiPMDTO> lsv = new List<QlyYcSDvaTaiPMDTO>();

            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    QlyYcSDvaTaiPMDTO a = new QlyYcSDvaTaiPMDTO(item1);
                    string PhongBan = a.PHONGBAN;
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

        public List<QlyYcSDvaTaiPMDTO> GetLsYcSDPMOfBP(string MaBoPhan)
        {
            List<QlyYcSDvaTaiPMDTO> lsvYCOfBP = new List<QlyYcSDvaTaiPMDTO>();
            List<PhongBanDTO> LsPbOfBP = QLYCDOITBDAO.Instance.GetLsvPbThuocBP(MaBoPhan);
            foreach (PhongBanDTO item in LsPbOfBP)
            {
                string MaPB = item.MAPB;
                List<QlyYcSDvaTaiPMDTO> lsvYCOfPB = GetLsYcSDPMOfPB(MaPB);
                foreach (QlyYcSDvaTaiPMDTO item1 in lsvYCOfPB)
                {
                    lsvYCOfBP.Add(item1);
                }
            }
            return lsvYCOfBP;
        }

        public List<QlyYcSDvaTaiPMDTO> GetYCwithNgLapYC(string MaNVlap)
        {
            string query = " select * from QLYCSDVATAIPM ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            // Lấy List ngày yêu cầu đã sắp xếp
            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();
            List<QlyYcSDvaTaiPMDTO> lsv = new List<QlyYcSDvaTaiPMDTO>();
            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    QlyYcSDvaTaiPMDTO a = new QlyYcSDvaTaiPMDTO(item1);
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

        public int Insert(string MaYCKT, string Phongban, string NgayYC,string MaPM, string TenPM, string PhienBanPM, string ChucNang,
                        string MDSD, string MayTinhCD,string NguoilapYC,string PDPB, string PDIT, string HoanTatYC)
        {
            string query = " insert QLYCSDVATAIPM(MAYCKT,PHONGBAN,NGAYYC,MAPM,TENPM,PHIENBANPM,CHUCNANG,MDSD,MTCAIDAT,NGLAPYC,PDPB,PDIT,HTYC) " +
                           " values ( @maYCKT , @PB , @NgayYC , @MaPM , @TenPM , @PhienBanPM , @CN , @MDSD , @MTCAIDAT , @NguoiLapYC , @PDPB , @PDIT , @HTYC )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaYCKT, Phongban, NgayYC,MaPM,  TenPM, PhienBanPM,  ChucNang,
                         MDSD,  MayTinhCD, NguoilapYC, PDPB, PDIT,HoanTatYC});
            return data;

        }

        public int UpdatePDPB(string MaYCKT, string PDPB)
        {
            string query = "update QLYCSDVATAIPM set PDPB= @PDPB where MAYCKT= @MaYCKT ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {PDPB , MaYCKT});
            return data;
        }
         
        public int UpdatePDIT(string MaYCKT, string PheDuyetIT)
        {
            string query = "update QLYCSDVATAIPM set PDIT= @PDIT where MAYCKT= @Mayckt ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { PheDuyetIT, MaYCKT });
            return data;
        }


        public int UpdateHTYC(string MaYCKT, string ThucHienYC)
        {
            string query = "update QLYCSDVATAIPM set HTYC= @HTYC where MAYCKT= @MaYCKT ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ThucHienYC, MaYCKT });
            return data;
        }
     
        public int Delete(string MaYCKT)
        {
            string query = " DELETE QLYCSDVATAIPM WHERE MAYCKT= @MaYCKT " ;
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaYCKT }) ;
            return data;
        }

    }
}
