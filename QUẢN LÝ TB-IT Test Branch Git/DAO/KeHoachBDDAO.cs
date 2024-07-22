using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class KeHoachBDDAO
    {
        private static KeHoachBDDAO instance;

        public static KeHoachBDDAO Instance
        {
            get { if (instance == null) instance = new KeHoachBDDAO(); return KeHoachBDDAO.instance; }
            private set { KeHoachBDDAO.instance = value; }
        }


        private KeHoachBDDAO() { }

       
        public DataTable GetTable()
        {
            string query = "select* from KEHOACHBD";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

      
        public bool CheckKHExistNM(string MaKH)
        {
            string query = "select * from KEHOACHBD WHERE MAKH= @ma  ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaKH });
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


        public KeHoachBDDTO GetKHDTO(string MaKH)
        {
            string query = "select * from KEHOACHBD WHERE MAKH= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaKH });
            KeHoachBDDTO A = new KeHoachBDDTO(data.Rows[0]);
            return A;          
        }

        public List<KeHoachBDDTO> GetKHNMPhongThang(string NhaMay,string PB,string Thang,string Nam)
        {
            string query = "select * from KEHOACHBD WHERE NHAMAY= @nm and PB= @pb and THANG= @thang and NAM= @nam ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {  NhaMay,  PB,  Thang, Nam });
            List<KeHoachBDDTO> Ls = new List<KeHoachBDDTO>();
            foreach (DataRow item in data.Rows)
            {
                KeHoachBDDTO A = new KeHoachBDDTO(item);
                Ls.Add(A);
            }           
            return Ls;
        }

        public List<KeHoachBDDTO> GetKHCanTH()
        {
            string NgayHienTai = DateTime.Now.ToString("dd/MM/yyyy");
            DateTime bayngaysau = DateTime.Now + TimeSpan.Parse("7");
            string bayngay = bayngaysau.ToString("dd/MM/yyyy");

            // Lấy trong vòng 7 ngày.

            string query = "select * from KEHOACHBD WHERE TRANGTHAI=0 ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { });
            List<KeHoachBDDTO> Ls = new List<KeHoachBDDTO>();
            foreach (DataRow item in data.Rows)
            {
                KeHoachBDDTO A = new KeHoachBDDTO(item);
                string NgayKH = A.NGAY;
                if(NgayKH.Length<2)
                {
                    NgayKH = "0" + NgayKH;
                }
                string ThangKH = A.THANG;
                if(ThangKH.Length<2)
                {
                    ThangKH = "0" + ThangKH;
                }

                string NamKH = A.NAM;
                string ngaykh = NgayKH + "/" + ThangKH + "/" + NamKH;
                TimeSpan time = bayngaysau - Convert.ToDateTime(ngaykh);
                int songay = time.Days;
                if (songay >= 0)
                {
                    Ls.Add(A);
                }              
            }
            return Ls;
        }


        public List<KeHoachBDDTO> GetKH_NMvaNam(string NhaMay, string Nam)
        {
            string query = "select * from KEHOACHBD WHERE NHAMAY= @nm and NAM= @nam ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { NhaMay, Nam });
            List<KeHoachBDDTO> Ls = new List<KeHoachBDDTO>();
            foreach (DataRow item in data.Rows)
            {
                KeHoachBDDTO A = new KeHoachBDDTO(item);
                Ls.Add(A);
            }
            return Ls;
        }

        // HAM THEM

        public int Insert(string MaKH,string Nhamay, string PB, string Ngay,string Thang,string Nam, string HMBT, bool TrangThai)
        {
            string query = "insert KEHOACHBD(MAKH,NHAMAY,PB,NGAY,THANG,NAM,HMBT,TRANGTHAI) values( @ma , @NM , @PB , @ngay , @thang , @nam , @hmbt , @tt )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {  MaKH, Nhamay,  PB,  Ngay,Thang,  Nam,  HMBT, TrangThai });
            return data;
        }

        public int UpdateTrangThai(string MaKH, bool TrangThai)
        {
            string query = "update KEHOACHBD set TRANGTHAI= @tt where MAKH= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {TrangThai,MaKH });
            return data;
        }



        // HAM XOA
        public int Delete(string MaKH)
        {
            string query = "DELETE KEHOACHBD WHERE MAKH= @MaKH ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {MaKH});
            return data;
        }

    }
}
