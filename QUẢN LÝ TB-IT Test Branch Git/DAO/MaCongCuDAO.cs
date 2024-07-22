using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class MaCongCuDAO
    {
        private static MaCongCuDAO instance;

        public static MaCongCuDAO Instance
        {
            get { if (instance == null) instance = new MaCongCuDAO(); return MaCongCuDAO.instance; }
            private set { MaCongCuDAO.instance = value; }
        }
        private MaCongCuDAO() { }


        public DataTable GetTable()
        {
            string query = " select* from MACONGCU ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public int CheckMaCC(string MaCC)
        {
            string query = "select * from MACONGCU where MACC= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaCC });
            return data.Rows.Count;
        }


        public int Insert(string MaCC, string TenCC, int solg, string dvtinh, string ncc, string ghichu)
        {
            string query = "insert MACONGCU(MACC,TENCC,SOLUONG,DVTINH,NCC,GHICHU) values( @ma , @ten , @SOLG , @dvi , @ncc , @note )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaCC, TenCC, solg, dvtinh, ncc, ghichu });
            return data;

        }
        public int InsertDeu(string MaCC)
        {
            string query = "insert MACONGCU(MACC) values( N'"+MaCC+"' )";
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            return data;

        }

        public int Update(string MaCC, string TenCC, int solg, string dvtinh, string ncc, string ghichu)
        {
            string query = "UPDATE	MACONGCU SET TENCC= @TEN ,SOLUONG= @SL ,DVTINH= @DV ,NCC= @ncc ,GHICHU= @note WHERE MACC= @ma ";

            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { TenCC, solg, dvtinh, ncc, ghichu, MaCC });
            return data;

        }

        // HAM XOA
        public int Delete(string MaCC)
        {
            string query = "DELETE MACONGCU WHERE MACC= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaCC });
            return data;
        }

    }
}
