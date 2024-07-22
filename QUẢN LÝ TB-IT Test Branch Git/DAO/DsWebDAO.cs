using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
   public class DsWebDAO
    {
        private static DsWebDAO instance;

        public static DsWebDAO Instance
        {
            get { if (instance == null) instance = new DsWebDAO(); return DsWebDAO.instance; }
            private set { DsWebDAO.instance = value; }
        }
        private DsWebDAO() { }

        // HAM LAY BANG
        public DataTable GetTable()
        {
            string query = "select* from DSWEBSITE";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public DsWebDTO GetWebDTO(string MaWeb)
        {
            string query = "select* from DSWEBSITE WHERE MAWEB= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaWeb });
            DsWebDTO a = new DsWebDTO(data.Rows[0]);
            return a;
        }

        public bool CheckExist(string MaWEB)
        {
            string query = "DELETE DSWEBSITE WHERE MAWEB= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaWEB });
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

        // HAM THEM

        public int Insert(string MaWeb, string LinkWeb, string Ghichu)
        {
            string query = "insert DSWEBSITE(MAWEB,LINKWEB,GHICHU) values( @ma , @ten , @ghichu )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaWeb, LinkWeb , Ghichu });
            return data;
        }

        // HAM SUA
        public int Update(string MaWeb, string LinkWeb, string Ghichu)
        {
            string query = "update DSWEBSITE set LINKWEB= @link ,GHICHU= @ghichu where MAWEB= @web ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { LinkWeb, Ghichu, MaWeb });
            return data;
        }

        // HAM XOA
        public int Delete(string MaWeb)
        {
            string query = "DELETE DSWEBSITE WHERE MAWEB= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaWeb });
            return data;
        }

    }
}
