using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class DanhSachPhanMemDAO
    {
        private static DanhSachPhanMemDAO instance;

        public static DanhSachPhanMemDAO Instance
        {
            get { if (instance == null) instance = new DanhSachPhanMemDAO(); return DanhSachPhanMemDAO.instance; }
            private set { DanhSachPhanMemDAO.instance = value; }
        }

        private DanhSachPhanMemDAO() { }
        public List<DanhSachPhanMemDTO> GetListMaMT()
        {
            string query = "select * from DANHSACHPHANMEM";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<DanhSachPhanMemDTO> lsv = new List<DanhSachPhanMemDTO>();
            foreach (DataRow item in data.Rows)
            {
                DanhSachPhanMemDTO maPM = new DanhSachPhanMemDTO(item);
                lsv.Add(maPM);
            }

            return lsv;
        }
        public DanhSachPhanMemDTO GetMaPM(String MaPM)
        {
            string query = "select * from DANHSACHPHANMEM where MAPM= @MA ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPM });

            DataRow row = data.Rows[0];
            DanhSachPhanMemDTO phanMemDTO = new DanhSachPhanMemDTO(row);

            return phanMemDTO;
        }


        public DataTable GetTable()
        {
            string query = "select* from DANHSACHPHANMEM";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public DataTable GetRowMaPM(string MaPM)
        {
            string query = "select * from DANHSACHPHANMEM where MAPM= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPM });
            return data;
        }
        public int CheckMaPM(string MaPM)
        {
            string query = "select * from DANHSACHPHANMEM where MAPM= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPM });
            return data.Rows.Count;
        }

        // HAM THEM

        public int Insert(string MaPM, string TenPM, string license, string ngmua, string hansd, string ncc,string Chucnang, string ghichu)
        {
            string query = "insert DANHSACHPHANMEM(MAPM,TENPM,LICENSE,NGAYMUA,HANSD,NCC,CHUCNANG,GHICHU) values( @ma , @ten , @LICENSE , @ngmua , @hsd , @ncc , @CN , @note )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaPM, TenPM, license, ngmua, hansd, ncc,Chucnang, ghichu });
            return data;

        }
        // HAM SUA
        public int Update(string MaPM, string TenPM, string license, string ngmua, string hansd, string ncc,string Chucnang, string ghichu)
        {
            string query = "UPDATE	DANHSACHPHANMEM SET TENPM= @tenpm ,LICENSE= @lisen ,NGAYMUA= @ngmua ,HANSD= @han ,NCC= @ncc ,CHUCNANG= @CN ,GHICHU= @ghichu  WHERE MAPM= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { TenPM, license, ngmua, hansd, ncc,Chucnang ,ghichu, MaPM });
            return data;

        }
        // HAM XOA
        public int Delete(string MaPM)
        {
            string query = "DELETE DANHSACHPHANMEM WHERE MAPM= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaPM });
            return data;

        }
    }
}
