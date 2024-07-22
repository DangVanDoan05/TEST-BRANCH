using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class TinhTrangYCKTDAO
    {
        private static TinhTrangYCKTDAO instance;

        public static TinhTrangYCKTDAO Instance
        {
            get { if (instance == null) instance = new TinhTrangYCKTDAO(); return TinhTrangYCKTDAO.instance; }
            private set { TinhTrangYCKTDAO.instance = value; }
        }
        private TinhTrangYCKTDAO() { }


        public DataTable GetTable()
        {
            string query = "select* from QLTINHTRANGYCKT";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }


        // Lấy ra chi tiết quyền bất kỳ

        public TinhTrangYCKTDTO GetChiTietQuyen(int a)
        {
            string query = " select * from QLTINHTRANGYCKT where IDTINHTRANG= @id ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { a });
            TinhTrangYCKTDTO tinhtrangDTO = new TinhTrangYCKTDTO(data.Rows[0]);
            return tinhtrangDTO;
        }

    }
}
