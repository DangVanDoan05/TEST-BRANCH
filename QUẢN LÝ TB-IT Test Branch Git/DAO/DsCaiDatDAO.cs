using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
   public class DsCaiDatDAO
    {
        private static DsCaiDatDAO instance;

        public static DsCaiDatDAO Instance
        {
            get { if (instance == null) instance = new DsCaiDatDAO(); return DsCaiDatDAO.instance; }
            private set { DsCaiDatDAO.instance = value; }
        }
        private DsCaiDatDAO() { }
        public DataTable GetTable()
        {
            string query = "select * from DSCAIDAT";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public bool CheckCDPM(string MaMT)
        {
            string query = " select * from DSCAIDAT where MAMT= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaMT });
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

        public bool CheckPMtrenMT(string MaMT, string MaPM)
        {
            string query = " select * from DSCAIDAT where MAMT= @ma and MAPM= @MAPM ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaMT, MaPM });
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

        public List<QuanLyMayTinhDTO> GetLsMTcaiPM(string MaPM)
        {
            string query = " select * from DSCAIDAT where MAPM= @MAPM ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {  MaPM });
            List<string> LsMaMT = new List<string>();
            foreach (DataRow item in data.Rows)
            {
                DsCaiDatDTO a = new DsCaiDatDTO(item);
                if(!LsMaMT.Contains(a.MAMT))
                {
                    LsMaMT.Add(a.MAMT);
                }
            }

            List<QuanLyMayTinhDTO> ls = new List<QuanLyMayTinhDTO>();
            foreach (string item1 in LsMaMT)
            {
                QuanLyMayTinhDTO b = QuanLyMayTinhDAO.Instance.GetMaMT(item1);
                ls.Add(b);
            }

            return ls;
        }


        // DSCAIDAT(MAMT,PB,NHAMAY,MAPM,TENPM,NGAYCD,NGAYHT)

        public int Insert(string MaMT,string PhongBan, string NhaMay, string MaPM, string TenPM, string ngaycaidat,string ngayhoantat )
        {
            string query = "insert DSCAIDAT(MAMT,PB,NHAMAY,MAPM,TENPM,NGAYCD,NGAYHT) values ( @maMT , @pb , @nhamay , @maPM , @TenPM , @ngaycai , @ngayht )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaMT,PhongBan,NhaMay, MaPM, TenPM, ngaycaidat,ngayhoantat });
            return data;
        }

        // HAM XOA
        public int Delete(string MaMT, string MaPM,string ngaycaidat)
        {
            string query = "DELETE DSCAIDAT WHERE MAMT= @ma and MAPM= @mapm and NGAYCD= @ngaycd ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaMT, MaPM,ngaycaidat });
            return data;
        }

        public int DeleteThongTinCD(int ID)
        {
            string query = "DELETE DSCAIDAT WHERE ID= @id ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {ID});
            return data;
        }

        public int DeletePMCD(string MaPM, string ngaycaidat)
        {
            string query = "DELETE DSCAIDAT WHERE MAPM= @mapm and NGAYCD= @ngaycd ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaPM, ngaycaidat });
            return data;
        }

        public int Delete1(string MaMT)
        {
            string query = "DELETE DSCAIDAT WHERE MAMT= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaMT });
            return data;
        }

        public int DeletePM(string MaPM)
        {
            string query = "DELETE DSCAIDAT WHERE MAPM= @mapm ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaPM });
            return data;
        }
    }
}
