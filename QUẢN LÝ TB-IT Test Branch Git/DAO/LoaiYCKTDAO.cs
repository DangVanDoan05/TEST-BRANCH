using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAO
{
    public class LoaiYCKTDAO
    {
        private static LoaiYCKTDAO instance;

        public static LoaiYCKTDAO Instance
        {
            get { if (instance == null) instance = new LoaiYCKTDAO(); return LoaiYCKTDAO.instance; }
            private set { LoaiYCKTDAO.instance = value; }
        }
        private LoaiYCKTDAO() { }

        // HAM LAY BANG
        public DataTable GetTable()
        {
            string query = " select * from LOAIYEUCAUKT ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public List<LoaiYCKTDTO> GetLsvLoaiYC()
        {
            string query = " select * from LOAIYEUCAUKT ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<LoaiYCKTDTO> lsv = new List<LoaiYCKTDTO>();
            foreach (DataRow item in data.Rows)
            {
                LoaiYCKTDTO phongBanDTO = new LoaiYCKTDTO(item);
                lsv.Add(phongBanDTO);
            }
            return lsv;

        }


        public int Insert(string TenloaiYC, string ghichu)
        {
            string query = " insert LOAIYEUCAUKT(LOAIYCKT,GHICHU) values( @ten , @ghichu )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { TenloaiYC, ghichu });
            return data;
        }

        // HAM SUA
        public int Update(string Tenloai, string ghichu)
        {
            string query = "UPDATE	LOAIYEUCAUKT SET GHICHU= @gh WHERE LOAIYCKT= @ten ";

            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ghichu, Tenloai });
            return data;

        }

        // HAM XOA
        public int Delete(string Tenloai)
        {
            string query = "DELETE LOAIYEUCAUKT WHERE LOAIYCKT= @ten ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { Tenloai });
            return data;
        }

    }
}
