using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
   public class MaMatHangDAO
    {
        private static MaMatHangDAO instance;

        public static MaMatHangDAO Instance
        {
            get { if (instance == null) instance = new MaMatHangDAO(); return MaMatHangDAO.instance; }
            private set { MaMatHangDAO.instance = value; }
        }
        private MaMatHangDAO() { }
         public DataTable GetTable()
        {
            string query = "select* from MAMATHANG";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public List<MaMatHangDTO> GetListMaMH()
        {
            string query = "select * from MAMATHANG";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<MaMatHangDTO> lsv = new List<MaMatHangDTO>();
            foreach (DataRow item in data.Rows)
            {
                MaMatHangDTO mathangDTO = new MaMatHangDTO(item);
                lsv.Add(mathangDTO);
            }
            return lsv;
        }
        public MaMatHangDTO GetMaMHDTO(string mamh)
        {
            string query = "select* from MAMATHANG where MAMH= @mh ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { mamh });
            MaMatHangDTO ma = new MaMatHangDTO(data.Rows[0]);
            return ma;
        }
        public int GetSoCot(string mamh)
        {
            string query = "select* from MAMATHANG where MAMH= @mh ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { mamh});
            return data.Rows.Count;
        }

        public int Insert(string MaMH, string TenMH)
        {
            string query = "insert MAMATHANG(MAMH,TENMH) values( @ma , @ten )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaMH, TenMH });
            return data;

        }
       
        public int Update(string TenMH, string MaMH)
        {
            string query = "UPDATE	MAMATHANG SET TENMH= @ten WHERE MAMH= @maMH ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { TenMH, MaMH});
            return data;

        }
       public int Delete(string MaMH)
        {
            string query = "DELETE MAMATHANG WHERE MAMH= @maMH ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaMH });
            return data;

        }
    }
}
