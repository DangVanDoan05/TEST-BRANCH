using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ThongKeXuatDAO
    {
        private static ThongKeXuatDAO instance;
        public static ThongKeXuatDAO Instance
        {
            get { if (instance == null) instance = new ThongKeXuatDAO(); return ThongKeXuatDAO.instance; }
            private set { ThongKeXuatDAO.instance = value; }
        }

        private ThongKeXuatDAO() { }

        public List<string> GetLsNgayXuatDaSX()  // Cần phải sắp xếp theo thời gian.
        {
            string query = "SELECT DISTINCT NGAYXUAT FROM THONGKEXUAT ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<NgayXuatDTO> LsNgayYC = new List<NgayXuatDTO>();
            foreach (DataRow item in data.Rows)
            {
                NgayXuatDTO a = new NgayXuatDTO(item);
                LsNgayYC.Add(a);
            }

            int dodai = LsNgayYC.Count();
            DateTime[] MangNgayYC = new DateTime[dodai];
            int m = 0;
            foreach (NgayXuatDTO item in LsNgayYC)
            {
                MangNgayYC[m] = item.NGAYXUAT;
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


        public List<ThongKeXuatDTO> GetAllListXuat() // Sắp xếp theo ngày tháng
        {
            string query = "select * from THONGKEXUAT";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<ThongKeXuatDTO> lsv = new List<ThongKeXuatDTO>();
            List<string> LsNgayYCDaSX = GetLsNgayXuatDaSX();            
            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    ThongKeXuatDTO a = new ThongKeXuatDTO(item1);
                    if (a.NGAYXUAT == item)
                    {
                        lsv.Add(a);
                    }
                }
            }
            return lsv;
        }


        //    string query = "select * from THONGKENHAP";
        //    DataTable data = DataProvider.Instance.ExecuteQuery(query);
        //    List<ThongKeNhapDTO> lsv = new List<ThongKeNhapDTO>();
        //        foreach (DataRow item in data.Rows)
        //        {
        //            ThongKeNhapDTO tknhap = new ThongKeNhapDTO(item);
        //    lsv.Add(tknhap);
        //        }
        //// lấy ra list thỏa điều kiện
        //List<ThongKeNhapDTO> lsv1 = new List<ThongKeNhapDTO>();
        //        foreach (ThongKeNhapDTO item in lsv)
        //        {
        //            string time = item.NGAYNHAP;
        //DateTime ngaynhap = Convert.ToDateTime(time);
        //TimeSpan time1 = ngaynhap - ngaybd;
        //TimeSpan time2 = ngaykt - ngaynhap;
        //int songaybatdau = time1.Days;
        //int songayketthuc = time2.Days;
        //            if (songaybatdau >= 0 && songayketthuc >= 0)
        //            {
        //                lsv1.Add(item);
        //            }

        //        }
        //        return lsv1;


        // Check thông tin xuất từ ngày này đến ngày này.
        public bool CheckXuatMaLKTrongKhoang(string MaLK,DateTime ngaybd, DateTime ngaykt)
        {
            string query = "select * from THONGKEXUAT where MALK= @malk ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaLK });
            List<ThongKeXuatDTO> lsv = new List<ThongKeXuatDTO>();
            foreach (DataRow item in data.Rows)
            {
                ThongKeXuatDTO tknhap = new ThongKeXuatDTO(item);
                lsv.Add(tknhap);
            }
            // lấy ra list thỏa điều kiện
            List<ThongKeXuatDTO> lsv1 = new List<ThongKeXuatDTO>();
            foreach (ThongKeXuatDTO item in lsv)
            {
                string time = item.NGAYXUAT;
                DateTime ngayxuat = Convert.ToDateTime(time);
                TimeSpan time1 = ngayxuat - ngaybd;
                TimeSpan time2 = ngaykt - ngayxuat;
                int songaybatdau = time1.Days;
                int songayketthuc = time2.Days;
                if (songaybatdau >= 0 && songayketthuc >= 0)
                {
                    lsv1.Add(item);
                }           
            }

            int dem = lsv1.Count;
            if(dem>0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool CheckXuatMaLKTuNgay(string MaLK, string NgayNhapLK)
        {
            string query = "select * from THONGKEXUAT where MALK= @malk ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaLK });
            List<ThongKeXuatDTO> lsv = new List<ThongKeXuatDTO>();
            foreach (DataRow item in data.Rows)
            {
                ThongKeXuatDTO tkXuat = new ThongKeXuatDTO(item);
                string TgXuat = tkXuat.NGAYXUAT;
                DateTime NgayXuat = Convert.ToDateTime(TgXuat);
                DateTime NgayNhap = Convert.ToDateTime(NgayNhapLK);
                TimeSpan time = NgayXuat - NgayNhap;
                int songay = time.Days;
                if(songay>=0) // Xuất kho trong ngày hoặc xuất kho sau ngày đó.
                { 
                    lsv.Add(tkXuat);
                }             
            }                                          
            int dem = lsv.Count;
            if (dem > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ThongKeXuatDTO> GetListXuat(DateTime ngaybd, DateTime ngaykt)
        {
            string query = "select * from THONGKEXUAT";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<ThongKeXuatDTO> lsv = new List<ThongKeXuatDTO>();
            foreach (DataRow item in data.Rows)
            {
                ThongKeXuatDTO tknhap = new ThongKeXuatDTO(item);
                lsv.Add(tknhap);
            }
            // lấy ra list thỏa điều kiện
            List<ThongKeXuatDTO> lsv1 = new List<ThongKeXuatDTO>();
            foreach (ThongKeXuatDTO item in lsv)
            {
                string time = item.NGAYXUAT;
                DateTime ngayxuat = Convert.ToDateTime(time);
                TimeSpan time1 = ngayxuat - ngaybd;
                TimeSpan time2 = ngaykt - ngayxuat;
                int songaybatdau = time1.Days;
                int songayketthuc = time2.Days;
                if (songaybatdau >= 0 && songayketthuc >= 0)
                {
                    lsv1.Add(item);
                }

            }
            return lsv1;
        }

        public DataTable GetTable()
        {
            string query = "select * from THONGKEXUAT";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public ThongKeXuatDTO GetTKxuatDTO(string MaTkxuat)
        {
           string query = "select * from THONGKEXUAT WHERE MATKXUAT= @malk ";
           DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaTkxuat });         
           ThongKeXuatDTO a = new ThongKeXuatDTO(data.Rows[0]);
           return a;           
        }

        public int Insert(string MaTKxuat,string malk, string tenlk, string ngayxuat, int slxuat, string dvtinh, string ncc, string nguoixuat, string YCKTSD , string MDSD, int idTT, string chitietTTKK)
        {
            string query = "insert THONGKEXUAT(MATKXUAT,MALK,TENLK,NGAYXUAT,SLXUAT,DVTINH,NCC,NGUOIXUAT,YCKTSD,MDSD,IDTTKIEMKE,CHITIETTTKK)" +
                            " values ( @maTKxuat , @MaLK , @ten , @ngX , @slg , @dvtinh , @ncc , @nguoixuat , @YCKTSD , @mdsd , @idTT , @Chitiet ) ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {MaTKxuat,malk, tenlk, ngayxuat, slxuat, dvtinh, ncc, nguoixuat, YCKTSD,MDSD,idTT,chitietTTKK });
            return data;
        }


        public int UpdateSLSDVtu(string malk, int slxuat, string YCKTSD)
        {
            string query = "Update THONGKEXUAT set SLXUAT= @soluong where MALK= @maLK and YCKTSD= @yc ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {slxuat, malk , YCKTSD });
            return data;
        }

        public int DeleteSDVtu(string malk, string YCKTSD)
        {
            string query = "delete THONGKEXUAT where MALK= @maLK and YCKTSD= @yc ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { malk, YCKTSD });
            return data;
        }


        public int UpdateTinhTrangKK(string maTKxuat, int idKiemKe, string ChiTietKK)
        {
            string query = "update THONGKEXUAT set IDTTKIEMKE= @IDKK ,CHITIETTTKK= @chitietKK where MATKXUAT= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { idKiemKe, ChiTietKK, maTKxuat });
            return data;
        }


        public int Delete( string YCKTSD)
        {
            string query = "delete THONGKEXUAT where YCKTSD= @yc ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { YCKTSD });
            return data;
        }

        public int DeleteMaTKxuat(string MaTKxuat)
        {
            string query = "delete THONGKEXUAT where MATKXUAT= @MA ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaTKxuat });
            return data;
        }

    }
}
