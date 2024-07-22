using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class LoaiMayTinhDAO
    {
        private static LoaiMayTinhDAO instance;

        public static LoaiMayTinhDAO Instance
        {
            get { if (instance == null) instance = new LoaiMayTinhDAO(); return LoaiMayTinhDAO.instance; }
            private set { LoaiMayTinhDAO.instance = value; }
        }
        private LoaiMayTinhDAO() { }

        // HAM LAY BANG
        public DataTable GetTable()
        {
            string query = "select* from LOAIMAYTINH";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public List<LoaiMayTinhDTO> GetListLoaiMT()
        {
            string query = "select* from LOAIMAYTINH";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<LoaiMayTinhDTO> lsv = new List<LoaiMayTinhDTO>();
            foreach (DataRow item in data.Rows)
            {
                LoaiMayTinhDTO loaiMTDTO = new LoaiMayTinhDTO(item);
                lsv.Add(loaiMTDTO);
            }
            return lsv;
        }


        public int CheckLoai(string tenloaiMT)
        {
            string query = "select* from LOAIMAYTINH where TENLOAIMT= @ten ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tenloaiMT });
            return data.Rows.Count;
        }

        public int Insert(string Tenloai, string ghichu)
        {
            string query = "insert LOAIMAYTINH(TENLOAIMT,GHICHU) values( @ten , @ghichu )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { Tenloai, ghichu });

            return data;

        }

        // HAM SUA
        public int Update(string Tenloai, string ghichu)
        {
            string query = "UPDATE	LOAIMAYTINH SET GHICHU= @GH WHERE TENLOAIMT= @ten ";

            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ghichu, Tenloai });
            return data;

        }

        // HAM XOA
        public int Delete(string Tenloai)
        {
            string query = "DELETE LOAIMAYTINH WHERE TENLOAIMT= @ten ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { Tenloai });
            return data;
        }

    }
}
