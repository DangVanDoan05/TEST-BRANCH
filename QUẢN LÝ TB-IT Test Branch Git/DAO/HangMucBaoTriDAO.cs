using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
   public class HangMucBaoTriDAO
    {
        private static HangMucBaoTriDAO instance;

        public static HangMucBaoTriDAO Instance
        {
            get { if (instance == null) instance = new HangMucBaoTriDAO(); return HangMucBaoTriDAO.instance; }
            private set { HangMucBaoTriDAO.instance = value; }
        }
        private HangMucBaoTriDAO() { }


        public DataTable GetTable()
        {
            string query = "select * from HANGMUCBAOTRI ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
                 
        public bool CheckMaHMExist(string MaHM)
        {
            string query = "select * from HANGMUCBAOTRI WHERE MAHM= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaHM });
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


        public int Insert(string MaHM, string Chitiet, string Ghichu)
        {
            string query = " insert HANGMUCBAOTRI(MAHM,CHITIET,GHICHU)" +
                                " values ( @maHM , @Chitiet , @ghichu )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaHM,Chitiet,Ghichu});
            return data;
        }

        // HAM SUA

        public int Update(string MaHM, string Chitiet, string Ghichu)
        {
            string query = " UPDATE HANGMUCBAOTRI set CHITIET= @chitiet ,GHICHU= @ghichu where MAHM= @hm  ";
           
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { Chitiet, Ghichu, MaHM });
            return data;
        }



        // HAM XOA

        public int Delete(string MaHM)
        {
            string query = "DELETE HANGMUCBAOTRI WHERE MAHM= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaHM });
            return data;
        }



    }
}
