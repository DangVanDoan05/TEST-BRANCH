using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;


namespace DAO
{
    public class GiaiTrinhMuaTBITDAO
    {
        private static GiaiTrinhMuaTBITDAO instance;

        public static GiaiTrinhMuaTBITDAO Instance

        {
            get { if (instance == null) instance = new GiaiTrinhMuaTBITDAO(); return GiaiTrinhMuaTBITDAO.instance; }
            private set { GiaiTrinhMuaTBITDAO.instance = value; }
        }

        private GiaiTrinhMuaTBITDAO() {}


        public List<string> GetLsNgayYcDaSX()  
        {
            string query = "SELECT DISTINCT NGAYYC FROM GIAITRINHMUATB ";
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
            string query = "select * from GIAITRINHMUATB ";
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


      


        //public List<GiaiTrinhMuaTBITDTO> GetLsYcDoiTBOfPB(string MaPB)
        //{
        //    string query = "select * from GIAITRINHMUATB where PB= @pb ";
        //    DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPB });
        //    List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();

        //    List<QLYCDOITBDTO> lsv = new List<QLYCDOITBDTO>();

        //    foreach (string item in LsNgayYCDaSX)
        //    {

        //        foreach (DataRow item1 in data.Rows)
        //        {
        //            QLYCDOITBDTO a = new QLYCDOITBDTO(item1);

        //            if (a.NGAYYC == item)
        //            {
        //                lsv.Add(a);
        //            }
        //        }
        //    }

        //    return lsv;
        //}


        public List<QLYCDOITBDTO> GetYCwithNvIT(string MaNVIT)
        {
            string query = " select * from GIAITRINHMUATB ";
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
            string query = " select * from GIAITRINHMUATB ";
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

        //public List<GiaiTrinhMuaTBITDTO> GetLsYcDoiTBOfBP(string MaBoPhan)
        //{
        //    List<GiaiTrinhMuaTBITDTO> lsvYCOfBP = new List<GiaiTrinhMuaTBITDTO>();
        //    List<PhongBanDTO> LsPbOfBP = QLYCDOITBDAO.Instance.GetLsvPbThuocBP(MaBoPhan);
        //    foreach (PhongBanDTO item in LsPbOfBP)
        //    {
        //        string MaPB = item.MAPB;
        //        List<GiaiTrinhMuaTBITDTO> lsvYCOfPB = GetLsYcDoiTBOfPB(MaPB);
        //        foreach (GiaiTrinhMuaTBITDTO item1 in lsvYCOfPB)
        //        {
        //            lsvYCOfBP.Add(item1);
        //        }
        //    }
        //    return lsvYCOfBP;
        //}


        public List<GiaiTrinhMuaTBITDTO> GetYCwithNgLapYC(string MaNVlap)
        {
            string query = " select * from GIAITRINHMUATB ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            // Lấy List ngày yêu cầu đã sắp xếp
            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();
            List<GiaiTrinhMuaTBITDTO> lsv = new List<GiaiTrinhMuaTBITDTO>();
            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    GiaiTrinhMuaTBITDTO a = new GiaiTrinhMuaTBITDTO(item1);
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



        public GiaiTrinhMuaTBITDTO GetGiaiTrinhDTO(string MaGT)
        {
            string query = "select * from GIAITRINHMUATB where MAGT= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaGT });
            GiaiTrinhMuaTBITDTO a = new GiaiTrinhMuaTBITDTO(data.Rows[0]);
            return a;
        }

     

       // insert GIAITRINHMUATB(MAGT, PB, NGAYYC, LOAITB, MDSD, SLHTAI, VTHTAI, TSSDHTAI, PPLVHTAI, SLSAU, VTSAU, TSSDSAU, PPLVSAU, NGLAPYC, PDPB, PDIT, HTYC)
        public int Insert(string MaGT, string PB,string NgayYC,string LoaiTB, string MDSD, int SLhtai, string VThtai,string TShtai, string PPLVhtai,int Slmua,string VTsaumua,string TSsaumua ,
            string PPLVSAUMUA, string NglapYc,string PDPB,string PDIT,string HTYC)
        {
            string query = " insert GIAITRINHMUATB(MAGT, PB, NGAYYC, LOAITB, MDSD, SLHTAI, VTHTAI, TSSDHTAI, PPLVHTAI, SLSAU, VTSAU, TSSDSAU, PPLVSAU, NGLAPYC, PDPB, PDIT, HTYC)" +
                           " values ( @MaGT , @pb , @NgayYC , @TBmua , @MDSD , @SLHT , @VTHT , @TSSDHT , @PPLVHT , @SLmua , @VTsau , @tssdsau , @pplvsau , @ngLap, @PDPB , @PDIT , @HTYC )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaGT,  PB,NgayYC,LoaiTB,  MDSD, SLhtai, VThtai,TShtai,  PPLVhtai,Slmua, VTsaumua, TSsaumua ,
           PPLVSAUMUA, NglapYc, PDPB,PDIT, HTYC });
            return data;

        }
        public int UpdatePDPB(string MaGT, string PheDuyetPB)
        {
            string query = "update GIAITRINHMUATB set PDPB= @PDPB  where  MAGT= @MaGT ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { PheDuyetPB, MaGT });
            return data;
        }
  
        public int UpdatePDIT(string MaGT, string PheDuyetIT)
        {
            string query = "update GIAITRINHMUATB set PDIT= @PDIT  where MAGT= @MaGT ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { PheDuyetIT, MaGT});
            return data;
        }

    
        public int UpdateHTYC(string MaGT, string HoanTatYC)
        {
            string query = "update GIAITRINHMUATB set HTYC= @HT where MAGT= @MaYCKT ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { HoanTatYC, MaGT });
            return data;
        }

        public int Delete(string MaGT)
        {
            string query = " DELETE GIAITRINHMUATB WHERE MAGT= @MaGT ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaGT });
            return data;
        }
        
    }
}
