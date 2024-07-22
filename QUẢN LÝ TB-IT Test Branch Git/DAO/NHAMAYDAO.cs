using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class NHAMAYDAO
    {
        private static NHAMAYDAO instance;

        public static NHAMAYDAO Instance
        {
            get { if (instance == null) instance = new NHAMAYDAO(); return NHAMAYDAO.instance; }
            private set { NHAMAYDAO.instance = value; }
        }
        private NHAMAYDAO() { }

        // HAM LAY BANG
        public DataTable GetTable()
        {
            string query = "select* from NHAMAY";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public bool CheckExist(string MaNM)
        {
            string query = "select * from NHAMAY where MANHAMAY= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNM });
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


        //  HÀM LẤY RA CỘT PHÒNG BAN      
        public List<NHAMAYDTO> GetLsvNM()
        {
            string query = " select * from NHAMAY ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<NHAMAYDTO> lsv = new List<NHAMAYDTO>();
            foreach (DataRow item in data.Rows)
            {
                NHAMAYDTO phongBanDTO = new NHAMAYDTO(item);
                lsv.Add(phongBanDTO);
            }
            return lsv;
        }

        // HAM THEM
        public int Insert(string MaNM, string TenNM, string DiaChi)
        {
            string query = "insert NHAMAY(MANHAMAY,TENNHAMAY,DIACHI) values( @maNM , @tenNM , @diachi )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaNM, TenNM, DiaChi });
            return data;
        }

        // HAM SUA
        public int Update(string MaNM, string TenNM, string DiaChi)
        {
            string query = "UPDATE	NHAMAY set TENNHAMAY= @tenNM ,DIACHI= @diachi WHERE MANHAMAY= @maNM ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { TenNM, DiaChi, MaNM });
            return data;
        }

        // HAM XOA
        public int Delete(string MaNM)
        {
            string query = "DELETE NHAMAY WHERE MANHAMAY= @maNM ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaNM });
            return data;
        }
    }
}