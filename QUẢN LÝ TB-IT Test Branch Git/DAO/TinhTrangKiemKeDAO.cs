using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;


namespace DAO
{
    public class TinhTrangKiemKeDAO
    {
        private static TinhTrangKiemKeDAO instance;

        public static TinhTrangKiemKeDAO Instance
        {
            get { if (instance == null) instance = new TinhTrangKiemKeDAO(); return TinhTrangKiemKeDAO.instance; }
            private set { TinhTrangKiemKeDAO.instance = value; }
        }
        private TinhTrangKiemKeDAO() { }


        public DataTable GetTable()
        {
            string query = "select* from TINHTRANGKIEMKE";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }


        // Lấy ra chi tiết quyền bất kỳ

        public TinhTrangKiemKeDTO GetKiemKeDTO(int a)
        {
            string query = " select * from TINHTRANGKIEMKE where IDTTKIEMKE= @id ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { a });
            TinhTrangKiemKeDTO tinhtrangDTO = new TinhTrangKiemKeDTO(data.Rows[0]);
            return tinhtrangDTO;
        }
    }
}
