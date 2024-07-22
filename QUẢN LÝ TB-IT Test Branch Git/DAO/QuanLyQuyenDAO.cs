using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
   public class QuanLyQuyenDAO
    {
        private static QuanLyQuyenDAO instance;

        public static QuanLyQuyenDAO Instance
        {
            get { if (instance == null) instance = new QuanLyQuyenDAO(); return QuanLyQuyenDAO.instance; }
            private set { QuanLyQuyenDAO.instance = value; }
        }
        private QuanLyQuyenDAO() { }

      
        public DataTable GetTable()
        {
            string query = "select* from QUANLYQUYEN";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        

        // Lấy ra chi tiết quyền bất kỳ
        public QuanLyQuyenDTO GetChiTietQuyen(int a)
        {
            string query = " select * from QUANLYQUYEN where IDQUYEN= @id ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { a });
            QuanLyQuyenDTO CTquyenDTO = new QuanLyQuyenDTO(data.Rows[0]);                        
            return CTquyenDTO;
        }
      

    }
}
