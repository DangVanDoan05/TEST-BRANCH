using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ThongKeNhapDAO
    {
        private static ThongKeNhapDAO instance;
        public static ThongKeNhapDAO Instance
        {
            get { if (instance == null) instance = new ThongKeNhapDAO(); return ThongKeNhapDAO.instance; }
            private set { ThongKeNhapDAO.instance = value; }
        }
        private ThongKeNhapDAO() { }

        public List<string> GetLsNgayYcDaSX()  // Cần phải sắp xếp theo thời gian.
        {
            string query = " SELECT DISTINCT NGAYNHAP FROM THONGKENHAP ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<NgayNhapDTO> LsNgayYC = new List<NgayNhapDTO>();
            foreach (DataRow item in data.Rows)
            {
                NgayNhapDTO a = new NgayNhapDTO(item);
                LsNgayYC.Add(a);
            }

            int dodai = LsNgayYC.Count();
            DateTime[] MangNgayYC = new DateTime[dodai];
            int m = 0;

            foreach (NgayNhapDTO item in LsNgayYC)
            {
                MangNgayYC[m] = item.NGAYNHAP;
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

        public DataTable GetTable()
        {
            string query = "select * from THONGKENHAP";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public bool CheckExistXuatKho(string MaLK, string NgayNhap) 
        {
            string query = "select * from THONGKENHAP where MALK= @malk ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaLK });
            // Lấy ra List từ ngày nhập đó trở đi.
            List<ThongKeNhapDTO> lsvNhap = new List<ThongKeNhapDTO>(); 
            
            foreach (DataRow item in data.Rows)
            {
                    ThongKeNhapDTO a = new ThongKeNhapDTO(item);
                    string ngaynhap = a.NGAYNHAP;
                DateTime TgNhapSau = Convert.ToDateTime(ngaynhap);
                DateTime TgNhap = Convert.ToDateTime(NgayNhap);
                TimeSpan time = TgNhapSau-TgNhap;
                int songay = time.Days;
                if (songay >= 0) // Xuất kho trong ngày hoặc xuất kho sau ngày đó.
                {
                    lsvNhap.Add(a);
                }
            }
            int dem = lsvNhap.Count;
            if(dem<=1)
            {

                foreach (ThongKeNhapDTO item1 in lsvNhap)
                {
                    bool CheckXuatKho = ThongKeXuatDAO.Instance.CheckXuatMaLKTuNgay(item1.MALK, item1.NGAYNHAP);
                    if(CheckXuatKho)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
               
            }
            else
            {// Khởi tạo một mảng 2 phần tử.
                // Lấy ra được ngày nhập liền kề của nó.
                string[] MangNgayNhap = new string[2];
                int n = 0;
                foreach (ThongKeNhapDTO item1 in lsvNhap)
                {
                    MangNgayNhap[n] = item1.NGAYNHAP;
                    n++;
                }
                string NgayKT = MangNgayNhap[1];
                DateTime ngaybd = Convert.ToDateTime(NgayNhap);
                DateTime ngaykt = Convert.ToDateTime(NgayKT);
                bool CheckXuatKho1 = ThongKeXuatDAO.Instance.CheckXuatMaLKTrongKhoang(MaLK, ngaybd, ngaykt);
                if (CheckXuatKho1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }


        public List<ThongKeNhapDTO> GetAllListNhap() // Sắp xếp theo ngày tháng
        {
            string query = "select * from THONGKENHAP ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<ThongKeNhapDTO> lsv = new List<ThongKeNhapDTO>();
            List<string> LsNgayYCDaSX = GetLsNgayYcDaSX();

            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    ThongKeNhapDTO a = new ThongKeNhapDTO(item1);
                    if (a.NGAYNHAP == item)
                    {
                        lsv.Add(a);
                    }
                }
            }
            return lsv;
        }





        //lẤY RA LIST MÃ NHẬP THỎA ĐIỀU KIỆN TRONG KHOẢNG THỜI GIAN
        public List<ThongKeNhapDTO> GetListNhap(DateTime ngaybd, DateTime ngaykt)
        {
            string query = "select * from THONGKENHAP";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<ThongKeNhapDTO> lsv = new List<ThongKeNhapDTO>();
            foreach (DataRow item in data.Rows)
            {
                ThongKeNhapDTO tknhap = new ThongKeNhapDTO(item);
                lsv.Add(tknhap);
            }
            // lấy ra list thỏa điều kiện
            List<ThongKeNhapDTO> lsv1 = new List<ThongKeNhapDTO>();
            foreach (ThongKeNhapDTO item in lsv)
            {
                string time = item.NGAYNHAP;
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

        public int Insert(string MaTKnhap,string malk, string tenlk, string ngaynhap, int slnhap, string dvtinh, string ncc, string nguoinhap, string ghichu, int idTT,string chitietTTKK)
        {
            string query = "insert THONGKENHAP(MATKNHAP,MALK,TENLK,NGAYNHAP,SLNHAP,DVTINH,NCC,NGUOINHAP,GHICHU,IDTTKIEMKE,CHITIETTTKK)" +
                                " values( @MaTknhap , @ma , @ten , @ngn , @slg , @dvtinh , @ncc , @nguoinhap , @ghichu , @id , @chitiet ) ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {MaTKnhap, malk, tenlk, ngaynhap, slnhap, dvtinh, ncc, nguoinhap, ghichu,idTT,chitietTTKK });
            return data;
        }

        public ThongKeNhapDTO GetTKnhapDTO(string MaTknhap)
        {
            string query = "select * from THONGKENHAP WHERE MATKNHAP= @malk ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaTknhap });
            DataRow row = data.Rows[0];
            ThongKeNhapDTO a = new ThongKeNhapDTO(row);
            return a;
        }

        public int UpdateTTKiemKe(string maTKnhap, int idKiemKe, string ChiTietKK)
        {
            string query = "update THONGKENHAP set IDTTKIEMKE= @IDKK ,CHITIETTTKK= @chitietKK where MATKNHAP= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { idKiemKe, ChiTietKK, maTKnhap });
            return data;
        }


        public int UpdateHoanTatKK(string maTKnhap, int idKiemKe, string ChiTietKK)
        {
            string query = "update THONGKENHAP set IDTTKIEMKE= @IDKK ,CHITIETTTKK= @chitietKK where MATKNHAP= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { idKiemKe, ChiTietKK, maTKnhap });
            return data;
        }


        public int Delete(string MaTKnhap)
        {
            string query = " DELETE THONGKENHAP WHERE MATKNHAP= @malk ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaTKnhap });
            return data;
        }

    }
}
