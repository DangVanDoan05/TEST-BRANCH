using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
   public class DSTHIETBIBGDAO
    {
        private static DSTHIETBIBGDAO instance;

        public static DSTHIETBIBGDAO Instance
        {
            get { if (instance == null) instance = new DSTHIETBIBGDAO(); return DSTHIETBIBGDAO.instance; }
            private set { DSTHIETBIBGDAO.instance = value; }
        }

        private DSTHIETBIBGDAO() { }

        public DataTable GetTable(string maBBBG)
        {
            string query = "select * from DSTHIETBIBG where MABBBG= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maBBBG });
            return data;
        }
      


        public int Insert(int stt,string maBBBG,string tenTB,string maTB,string DonVi,int soluong,string tinhtrang)
        {
            string query = "insert DSTHIETBIBG(STT,MABBBG,TENTB,MATB,DONVI,SOLUONG,TINHTRANG) values ( @stt , @mabbbg , @tentb , @matb , @donvi , @soluong , @tinhtrang ) ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {stt,maBBBG,tenTB,maTB,DonVi,soluong,tinhtrang});
            return data;
        }
        public int Update(int stt,string maBBBG, string tenTB, string maTB, string DonVi, int soluong, string tinhtrang)
        {
            string query = "update DSTHIETBIBG set MABBBG= @maBBBG , TENTB= @tenTB ,MATB= @maTB ,DONVI= @donvi ,SOLUONG= @soluong ,TINHTRANG= @tinhtrang where STT= @stt ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maBBBG,tenTB, maTB, DonVi, soluong, tinhtrang,stt});
            return data;
        }
      
        public int Delete(int stt)
        {
            string query = " DELETE DSTHIETBIBG WHERE STT= @stt ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { stt });
            return data;
        }

    }
}
