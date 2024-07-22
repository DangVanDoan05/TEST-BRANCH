using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class BoPhanDAO
    {
        private static BoPhanDAO instance;

        public static BoPhanDAO Instance
        {
            get { if (instance == null) instance = new BoPhanDAO(); return BoPhanDAO.instance; }
            private set { BoPhanDAO.instance = value; }
        }
        private BoPhanDAO() { }

        // HAM LAY BANG
        public DataTable GetTable()
        {
            string query = "select* from BOPHAN";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public BoPhanDTO GetBoPhanDTO(string MaBP, string ThuocNM)
        {
            string query = "select* from BOPHAN where MABOPHAN= @ma and NHAMAY= @nhamay ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaBP, ThuocNM });
            BoPhanDTO a = new BoPhanDTO(data.Rows[0]);
            return a;
        }

        public List<BoPhanDTO> GetLsBPdtoOfNM( string NhaMay)
        {
            string query = " select * from BOPHAN where NHAMAY= @nhamay ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { NhaMay });
            List<BoPhanDTO> Ls = new List<BoPhanDTO>();
            foreach (DataRow item in data.Rows)
            {
                BoPhanDTO a = new BoPhanDTO(item);
                Ls.Add(a);
            }         
            return Ls;
        }

        public bool CheckBPExistNM(string MaBP,string ThuocNM)
        {
            string query = "select * from BOPHAN where MABP= @ma and NHAMAY= @nhamay ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaBP , ThuocNM });
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
     
        public int Insert(string MaBP, string TenBP, string Ghichu,string ThuocNM )
        {
            string query = "insert BOPHAN(MABP, TENBP, GHICHU, NHAMAY) values( @mabp , @tenbp , @ghichu , @nhamay )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaBP, TenBP,  Ghichu, ThuocNM });
            return data;
        }

        // HAM SUA
        public int Update(string MaBP, string TenBP, string Ghichu,string ThuocNM )
        {
            string query = "UPDATE	BOPHAN set TENBP= @ten ,GHICHU= @ghichu WHERE MABP= @mabp and NHAMAY= @nhamay ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {TenBP, Ghichu, MaBP,ThuocNM });
            return data;
        }

        // HAM XOA
        public int Delete(string MaBP,string ThuocNM )
        {
            string query = "DELETE BOPHAN WHERE MABP= @mabp and NHAMAY= @nhamay ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaBP,ThuocNM});
            return data;
        }
    }
}
