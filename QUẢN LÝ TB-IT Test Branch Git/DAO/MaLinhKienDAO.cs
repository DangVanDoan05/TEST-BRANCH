using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class MaLinhKienDAO
    {
        private static MaLinhKienDAO instance;

        public static MaLinhKienDAO Instance
        {
            get { if (instance == null) instance = new MaLinhKienDAO(); return MaLinhKienDAO.instance; }
            private set { MaLinhKienDAO.instance = value; }
        }
        private MaLinhKienDAO() { }


        public DataTable GetTable()
        {
            string query = "select* from MALINHKIEN";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public int CheckMaLK(string MaLK)
        {
            string query = "select * from MALINHKIEN where MALK= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaLK });
            return data.Rows.Count;
        }
        public List<MaLinhKienDTO> GetListMaLK()
        {
            string query = "select* from MALINHKIEN";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<MaLinhKienDTO> lsv = new List<MaLinhKienDTO>();
            foreach (DataRow item in data.Rows)
            {
                MaLinhKienDTO ma = new MaLinhKienDTO(item);
                lsv.Add(ma);
            }
            return lsv;
        }

        // lấy thông tin mã linh  kiện:
        public MaLinhKienDTO GetRowMaLK(string malk)
        {
            string query = "select* from MALINHKIEN where MALK=N'" + malk + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            DataRow Row = data.Rows[0];
            MaLinhKienDTO MaLKDTO = new MaLinhKienDTO(Row);
            return MaLKDTO;

        }



        // HAM THEM
        public int InsertTable(string MaLK, string TenLK, string dvtinh, string ncc, string slmin, string slmax, string ghichu)
        {
            string query = "insert MALINHKIEN(MALK,TENLK,DVTINH,NCC,SLMIN,SLMAX,GHICHU) values( @ma , @ten , @dv , @ncc , @min , @max , @note )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaLK, TenLK, dvtinh, ncc, slmin, slmax, ghichu });
            return data;

        }
        // HAM SUA
        public int UpdateTable(string MaLK, string TenLK, string dvtinh, string ncc, string slmin, string slmax, string ghichu)
        {
            string query = "UPDATE	MALINHKIEN SET TENLK= @TEN ,DVTINH= @DV ,NCC= @ncc ,SLMIN= @min ,SLMAX= @max ,GHICHU= @note WHERE MALK= @ma ";

            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { TenLK, dvtinh, ncc, slmin, slmax, ghichu, MaLK });
            return data;

        }
        // HAM XOA
        public int DeleteTable(string MaLK)
        {
            string query = "DELETE MALINHKIEN WHERE MALK= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaLK });
            return data;

        }
    }
}
