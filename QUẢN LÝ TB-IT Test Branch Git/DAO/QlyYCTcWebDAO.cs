using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
   public class QlyYCTcWebDAO
    {
      
            private static QlyYCTcWebDAO instance;

            public static QlyYCTcWebDAO Instance
            {
                get { if (instance == null) instance = new QlyYCTcWebDAO(); return QlyYCTcWebDAO.instance; }
                private set { QlyYCTcWebDAO.instance = value; }
            }

            private QlyYCTcWebDAO() { }

            public List<string> GetLsNgayYcDaSX()  
            {
                string query = "SELECT DISTINCT NGAYYC FROM QLYCTCWEB ";
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



            public List<QlyYCTcWebDTO> GetAllYcTcWeb()  
            {
                string query = "select * from QLYCTCWEB ";
                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                // Lấy List ngày yêu cầu đã sắp xếp
                List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();
                List<QlyYCTcWebDTO> lsv = new List<QlyYCTcWebDTO>();

                foreach (string item in LsNgayYCDaSX)
                {
                    foreach (DataRow item1 in data.Rows)
                    {
                        QlyYCTcWebDTO a = new QlyYCTcWebDTO(item1);
                        if (a.NGAYYC == item)
                            lsv.Add(a);
                    }
                }
                return lsv;
            }


            public QlyYCTcWebDTO GetYcTcWebDTO(string MaNV,string MaWeb)
            {
                string query = "select * from QLYCTCWEB where MANV= @MaYCKT and MAWEB= @MaWeb ";
                DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNV,MaWeb});
                QlyYCTcWebDTO a = new QlyYCTcWebDTO(data.Rows[0]);
                return a; 
            }


            public List<QlyYCTcWebDTO> GetLsYcTcWebOfPB(string MaPB)
            {
                string query = "select * from QLYCTCWEB where PB= @pb ";
                DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPB });
                List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();

                List<QlyYCTcWebDTO> lsv = new List<QlyYCTcWebDTO>();

                foreach (string item in LsNgayYCDaSX)
                {
                    foreach (DataRow item1 in data.Rows)
                    {
                        QlyYCTcWebDTO a = new QlyYCTcWebDTO(item1);
                        if (a.NGAYYC == item)
                        {
                            lsv.Add(a);
                        }
                    }
                }
                return lsv;
            }


            public List<QlyYCTcWebDTO> GetYCwithNvIT(string MaNVIT)
            {
                string query = " select * from QLYCTCWEB ";
                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                // Lấy List ngày yêu cầu đã sắp xếp
                List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();
                List<QlyYCTcWebDTO> lsv = new List<QlyYCTcWebDTO>();

                foreach (string item in LsNgayYCDaSX)
                {
                    foreach (DataRow item1 in data.Rows)
                    {
                        QlyYCTcWebDTO a = new QlyYCTcWebDTO(item1);
                        string MaNV = a.MANV;                      
                        if (((a.NGAYYC == item) && (a.PDPB != "") && (a.PDIT != "")) || ((a.NGAYYC == item) && (MaNV == MaNVIT)))
                        {
                            lsv.Add(a);
                        }
                    }
                }
                return lsv;
            }


            public List<QlyYCTcWebDTO> GetYCwithQlyIT(string MaNVQlyIT)
            {
                string query = " select * from QLYCTCWEB ";
                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                // Lấy List ngày yêu cầu đã sắp xếp

                List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();

                List<QlyYCTcWebDTO> lsv = new List<QlyYCTcWebDTO>();

                foreach (string item in LsNgayYCDaSX)
                {
                    foreach (DataRow item1 in data.Rows)
                    {
                        QlyYCTcWebDTO a = new QlyYCTcWebDTO(item1);
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

            public List<QlyYCTcWebDTO> GetLsYcTcWebOfBP(string MaBoPhan)
            {
                List<QlyYCTcWebDTO> lsvYCOfBP = new List<QlyYCTcWebDTO>();
                List<PhongBanDTO> LsPbOfBP = QlyYCTcWebDAO.Instance.GetLsvPbThuocBP(MaBoPhan);
                foreach (PhongBanDTO item in LsPbOfBP)
                {
                    string MaPB = item.MAPB;
                    List<QlyYCTcWebDTO> lsvYCOfPB = GetLsYcTcWebOfPB(MaPB);
                    foreach (QlyYCTcWebDTO item1 in lsvYCOfPB)
                    {
                        lsvYCOfBP.Add(item1);
                    }
                }
                return lsvYCOfBP;
            }

            public List<QlyYCTcWebDTO> GetYCwithNhanVien(string MaNVlap)
            {
                string query = " select * from QLYCTCWEB ";
                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                // Lấy List ngày yêu cầu đã sắp xếp
                List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();
                List<QlyYCTcWebDTO> lsv = new List<QlyYCTcWebDTO>();
                foreach (string item in LsNgayYCDaSX)
                {
                    foreach (DataRow item1 in data.Rows)
                    {
                        QlyYCTcWebDTO a = new QlyYCTcWebDTO(item1);
                        string MaNV = a.MANV;
                       
                        if (((a.NGAYYC == item) && (MaNV == MaNVlap)))
                        {
                            lsv.Add(a);
                        }
                    }
                }
                return lsv;
            }

        public bool CheckWebOfNV(string MaNV,string Web)
        {
            string query = "select * from QLYCTCWEB  where  MANV= @Ma and MAWEB= @web ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNV, Web});
            if(data.Rows.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }


        // insert QLYCTCWEB(MANV,PB,NGAYYC,MAWEB,LINKWEB,NGLAPYC,PDPB,PDIT,HTYC) 
        public int Insert(string MaNV, string Phongban, string NgayYC, string MaWeb, string LinkWeb,string MDSD, string NglapYC, string PDPB, string PDIT, string HoanTatYC)
            {
                string query = "insert QLYCTCWEB(MANV,PB,NGAYYC,MAWEB,LINKWEB,MDSD,NGLAPYC,PDPB,PDIT,HTYC) "+
                                   " values ( @maNV , @PB , @NgayYC , @maweb , @linkweb , @MDSD , @nglapYC , @PDPB , @PDIT , @htyc )";
                int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaNV, Phongban, NgayYC, MaWeb,LinkWeb,MDSD,NglapYC,PDPB,PDIT,HoanTatYC });
                     
                return data;
            }


            public int UpdatePDPB(string MaNV,string MaWeb, string PheDuyetPB)
            {
                string query = "update QLYCTCWEB set PDPB= @PDPB  where  MANV= @Ma and MAWEB= @web ";
                int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { PheDuyetPB, MaNV,MaWeb });
                return data;
            }

         
         
            public int UpdatePDIT(string MaNV,string MaWeb, string PDIT)
            {
                string query = " update QLYCTCWEB set PDIT= @PDIT  where MANV= @Ma and MAWEB= @web ";
                int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { PDIT, MaNV,MaWeb});
                return data;
            }


            // QLYCDOITB(MAYCKT, PB, NGAYYC, TBDOI, SOLUONG, MTSD, LOIHT, VITRISD, PPTT, VDKTT, NGLAPYC, PDPB, PDIT, HTYC)


            public int UpdateHTYC(string MaNV, string MaWeb, string HoanTatYC)
            {
                string query = "update QLYCTCWEB set HTYC= @HT where MANV= @Ma and MAWEB= @web ";
                int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { HoanTatYC, MaNV,MaWeb });
                return data;
            }

            
            public int Delete(string MaNV, string MaWeb)
            {
                string query = " DELETE QLYCTCWEB WHERE MANV= @Ma and MAWEB= @web ";
                int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaNV, MaWeb });
                return data;
            }

        
    }
}
