using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class ThongKeTHDAO
    {
        private static ThongKeTHDAO instance;
        public static ThongKeTHDAO Instance
        {
            get { if (instance == null) instance = new ThongKeTHDAO(); return ThongKeTHDAO.instance; }
            private set { ThongKeTHDAO.instance = value; }
        }
        private ThongKeTHDAO() { }

        public List<string> GetLsNgayTHDaSX()  // Cần phải sắp xếp theo thời gian.
        {
            string query = " SELECT DISTINCT NGAYTH FROM THONGKETHUHOI ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<NgayTHDTO> LsNgayTH = new List<NgayTHDTO>();
            foreach (DataRow item in data.Rows)
            {
                NgayTHDTO a = new NgayTHDTO(item);
                LsNgayTH.Add(a);
            }

            int dodai = LsNgayTH.Count();
            DateTime[] MangNgayTH = new DateTime[dodai];
            int m = 0;

            foreach (NgayTHDTO item in LsNgayTH)
            {
                MangNgayTH[m] = item.NGAYTH;
                m++;
            }

            // Sắp xếp ngày thu hồi.

            // Tiến hành sắp xếp mảng thời gian đặt hàng theo thứ tự giảm dần

            for (int i = 0; i < dodai - 1; i++)
            {
                for (int j = i + 1; j < dodai; j++)
                {
                    if (MangNgayTH[j] > MangNgayTH[i])
                    {
                        DateTime trunggian = MangNgayTH[j];
                        MangNgayTH[j] = MangNgayTH[i];
                        MangNgayTH[i] = trunggian;
                    }
                }
            }

            List<string> LsNgayTHDaSX = new List<string>();
            for (int k = 0; k < dodai; k++)
            {
                string NgayTH = MangNgayTH[k].ToString("dd/MM/yyyy");
                LsNgayTHDaSX.Add(NgayTH);
            }
            return LsNgayTHDaSX;
        }

        public DataTable GetTable()
        {
            string query = "select * from THONGKETHUHOI";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public List<ThongKeTHDTO> GetAllListTH() // Sắp xếp theo ngày tháng
        {
            string query = "select * from THONGKETHUHOI ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<ThongKeTHDTO> lsv = new List<ThongKeTHDTO>();
            List<string> LsNgayTHDaSX = GetLsNgayTHDaSX();

            foreach (string item in LsNgayTHDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    ThongKeTHDTO a = new ThongKeTHDTO(item1);
                    if (a.NGAYTH == item)
                    {
                        lsv.Add(a);
                    }
                }
            }
            return lsv;
        }





        //lẤY RA LIST MÃ NHẬP THỎA ĐIỀU KIỆN TRONG KHOẢNG THỜI GIAN
        public List<ThongKeTHDTO> GetListTH(DateTime ngaybd, DateTime ngaykt)
        {
            string query = "select * from THONGKETHUHOI";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<ThongKeTHDTO> lsv = new List<ThongKeTHDTO>();
            foreach (DataRow item in data.Rows)
            {
                ThongKeTHDTO tknhap = new ThongKeTHDTO(item);
                lsv.Add(tknhap);
            }
            // lấy ra list thỏa điều kiện
            List<ThongKeTHDTO> lsv1 = new List<ThongKeTHDTO>();
            foreach (ThongKeTHDTO item in lsv)
            {
                string time = item.NGAYTH;
                DateTime ngaynhap = Convert.ToDateTime(time);
                TimeSpan time1 = ngaynhap - ngaybd;
                TimeSpan time2 = ngaykt - ngaynhap;
                int songaybatdau = time1.Days;
                int songayketthuc = time2.Days;
                if (songaybatdau >= 0 && songayketthuc >= 0)
                {
                    lsv1.Add(item);
                }

            }
            return lsv1;
        }

        public int Insert(string MaTKTH, string malk, string tenlk, string ngayTH,string NhaMay,string PB, int slTH, string dvtinh, string nguoiTH,string LuuKhoOrTieuHuy, int idTT, string chitietTTKK)
        {
            string query = "insert THONGKETHUHOI(MATKTH,MALK,TENLK,NGAYTH,NHAMAY,PB,SLTH,DVTINH,NGUOITH,LKORTH,IDTTKIEMKE,CHITIETTTKK)" +
                                " values( @MaTk , @ma , @ten , @ngTh , @NM , @pb , @slg , @dvtinh , @nguoiTH , @lkOrTh , @id , @chitiet ) ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaTKTH, malk, tenlk,ngayTH,NhaMay,PB,slTH, dvtinh,  nguoiTH, LuuKhoOrTieuHuy,idTT,  chitietTTKK });
            return data;

        }

        public ThongKeTHDTO GetTKTHDTO(string MaTknhap)
        {
            string query = "select * from THONGKETHUHOI WHERE MATKTH= @malk ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaTknhap });
            DataRow row = data.Rows[0];
            ThongKeTHDTO a = new ThongKeTHDTO(row);
            return a;
        }

        public int UpdateTTKiemKe(string maTKnhap, int idKiemKe, string ChiTietKK)
        {
            string query = "update THONGKETHUHOI set IDTTKIEMKE= @IDKK ,CHITIETTTKK= @chitietKK where MATKTH= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { idKiemKe, ChiTietKK, maTKnhap });
            return data;
        }

        public int UpdateTieuHuy(string maTKthuhoi, string LuukhoOrTieuHuy)
        {
            string query = "update THONGKETHUHOI set LKORTH= @th where MATKTH= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {LuukhoOrTieuHuy,maTKthuhoi });
            return data;
        }

        public int UpdateHoanTatKK(string maTKnhap, int idKiemKe, string ChiTietKK)
        {
            string query = "update THONGKETHUHOI set IDTTKIEMKE= @IDKK ,CHITIETTTKK= @chitietKK where MATKTH= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { idKiemKe, ChiTietKK, maTKnhap });
            return data;
        }


        public int Delete(string MaTKTH)
        {
            string query = " DELETE THONGKETHUHOI WHERE MATKTH= @maTH ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaTKTH });
            return data;
        }
    }
}
