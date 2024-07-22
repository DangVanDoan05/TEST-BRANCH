using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
   public class NhaCungCapDAO
    {
        private static NhaCungCapDAO instance;

        public static NhaCungCapDAO Instance
        {
            get { if (instance == null) instance = new NhaCungCapDAO(); return NhaCungCapDAO.instance; }
            private set { NhaCungCapDAO.instance = value; }
        }
        private NhaCungCapDAO() { }

        // HAM LAY BANG
        public DataTable GetTable()
        {
            string query = "select* from NHACUNGCAP";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public List<NhaCungCapDTO> GetListNCC()
        {
            string query = "select* from NHACUNGCAP";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<NhaCungCapDTO> lsv = new List<NhaCungCapDTO>();
            foreach (DataRow item in data.Rows)
            {
                NhaCungCapDTO ncc = new NhaCungCapDTO(item);
                lsv.Add(ncc);
            }
            return lsv;
        }
        // HAM LAY HANG THUC HIEN
        public int CheckMaNCC(string MaNCC)
        {
            string query = "select * from NHACUNGCAP where MANCC= @mancc ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNCC });
            return data.Rows.Count;
        }
        // HAM THEM
        public int InsertTable(string MaNCC, string TenNCC, string diachi, string dt, string web)
        {
            string query = "insert NHACUNGCAP(MANCC,TENNCC,DIACHI,DIENTHOAI,WEBSITE) values( @maNCC , @tenNCC , @dc , @dt , @web )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaNCC, TenNCC, diachi, dt, web });
            return data;

        }
        // HAM SUA
        public int UpdateTable(string MaNCC, string TenNCC, string diachi, string dt, string web)
        {
            string query = "UPDATE	NHACUNGCAP SET TENNCC= @ten ,DIACHI= @dc ,DIENTHOAI= @dt ,WEBSITE= @VEB WHERE MANCC= @ma ";
            // string query = string.Format("UPDATE PHONGBAN SET TENPB=N'{0}' WHERE MAPB=N'{1}'",TenPB,MaPB);
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { TenNCC, diachi, dt, web, MaNCC });
            return data;

        }
        // HAM XOA
        public int DeleteTable(string MaNCC)
        {
            string query = "DELETE NHACUNGCAP WHERE MANCC= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaNCC });
            return data;

        }

    }
}
