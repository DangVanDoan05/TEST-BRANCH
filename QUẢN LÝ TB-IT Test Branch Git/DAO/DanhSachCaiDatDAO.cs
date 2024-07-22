using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class DanhSachCaiDatDAO
    {
        private static DanhSachCaiDatDAO instance;

        public static DanhSachCaiDatDAO Instance
        {
            get { if (instance == null) instance = new DanhSachCaiDatDAO(); return DanhSachCaiDatDAO.instance; }
            private set { DanhSachCaiDatDAO.instance = value; }
        }
        private DanhSachCaiDatDAO() { }
        public DataTable GetTable()
        {
            string query = "select * from DANHSACHCAIDAT";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public List<DanhSachCaiDatDTO> GetLsDsCaiDat()
        {
            string query = "select * from DANHSACHCAIDAT";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<DanhSachCaiDatDTO> Ls = new List<DanhSachCaiDatDTO>();

            foreach (DataRow item in data.Rows)
            {
                DanhSachCaiDatDTO a = new DanhSachCaiDatDTO(item);
                Ls.Add(a);              
            }
            return Ls;
        }

        public DanhSachCaiDatDTO GetMaMT(string mamt)
        {
            string query = "select * from DANHSACHCAIDAT where MAMT= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { mamt });
            DanhSachCaiDatDTO maMTDTO = new DanhSachCaiDatDTO(data.Rows[0]);
            return maMTDTO;
        }

        public bool CheckCDPM(string MaMT)
        {
            string query = " select * from DANHSACHCAIDAT where MAMT= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaMT });
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

        public bool CheckPMtrenMT(string MaMT,string MaPM)
        {
            string query = " select * from DANHSACHCAIDAT where MAMT= @ma and MAPM= @MAPM ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaMT,MaPM});
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


        public int Insert(string MaMT, string MaPM, string TenPM, string ngaycaidat)
        {
            string query = "insert DANHSACHCAIDAT(MAMT,MAPM,TENPM,NGAYCD) values ( @maMT , @maPM , @TenPM , @ngaycai )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaMT, MaPM, TenPM, ngaycaidat });
            return data;
        }
      
        // HAM XOA
        public int Delete(string MaMT, string MaPM)
        {
            string query = "DELETE DANHSACHCAIDAT WHERE MAMT= @ma and MAPM= @mapm ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaMT, MaPM });
            return data;
        }

        public int Delete1(string MaMT)
        {
            string query = "DELETE DANHSACHCAIDAT WHERE MAMT= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaMT});
            return data;
        }

        public int DeletePM( string MaPM)
        {
            string query = "DELETE DANHSACHCAIDAT WHERE MAPM= @mapm ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaPM });
            return data;
        }
    }
}
