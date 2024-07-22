using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
   public class QLYCHUCNANGDAO
    {
        private static QLYCHUCNANGDAO instance;

        public static QLYCHUCNANGDAO Instance
        {
            get { if (instance == null) instance = new QLYCHUCNANGDAO(); return QLYCHUCNANGDAO.instance; }
            private set { QLYCHUCNANGDAO.instance = value; }
        }
        private QLYCHUCNANGDAO(){}

        // HAM LAY BANG
        public DataTable GetTable()
        {
            string query = "select* from QLYCHUCNANG";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
      
        public List<QLYCHUCNANGDTO> GetLsCN()
        {
            string query = " select * from QLYCHUCNANG ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<QLYCHUCNANGDTO> lsv = new List<QLYCHUCNANGDTO>();
            foreach (DataRow item in data.Rows)
            {
                QLYCHUCNANGDTO CnDTO = new QLYCHUCNANGDTO(item);
                lsv.Add(CnDTO);
            }
            return lsv;

        }

        public int Insert(string id,string idparent, string Mota)
        {
            string query = " insert QLYCHUCNANG(ID,IDPARENT,MOTA) values( @id , @idparent , @mota ) ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { id , idparent, Mota});
            return data;
        }
       
        public int Delete(string id)
        {
            string query = " DELETE QLYCHUCNANG WHERE ID= @macn ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { id });
            return data;
        }
    }
}
