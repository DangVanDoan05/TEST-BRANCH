using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DanhSachMayInDAO
    {
        private static DanhSachMayInDAO instance;

        public static DanhSachMayInDAO Instance
        {
            get { if (instance == null) instance = new DanhSachMayInDAO(); return DanhSachMayInDAO.instance; }
            private set { DanhSachMayInDAO.instance = value; }
        }
        private DanhSachMayInDAO() { }


        public DataTable GetTable()
        {
            string query = "select * from DANHSACHMAYIN";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public int CheckMaMI(string maMI)
        {
            string query = "select* from DANHSACHMAYIN where MAMAYIN= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maMI });
            return data.Rows.Count;
        }

        public int Insert(string maMI, string tenMI, string Phongban, string IP, string MAC, string ngaymua, string hanbh, string ghichu)
        {
            string query = " insert DANHSACHMAYIN (MAMAYIN,TENMAYIN,PHONGBAN,IP,MAC,NGAYMUA,HANBH,GHICHU) values( @ma , @ten , @phongban , @ip , @mac , @ngaymua , @hanbh , @ghichu )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maMI, tenMI, Phongban, IP, MAC, ngaymua, hanbh, ghichu });

            return data;

        }


        public int Update(string maMI, string tenMI, string Phongban, string IP, string MAC, string ngaymua, string hanbh, string ghichu)
        {
            string query = "UPDATE	DANHSACHMAYIN SET TENMAYIN= @ten ,PHONGBAN= @pb ,IP= @ip ,MAC= @mac ,NGAYMUA= @ngaymua ,HANBH= @han ,GHICHU= @ghichu where MAMAYIN= @ma ";

            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tenMI, Phongban, IP, MAC, ngaymua, hanbh, ghichu, maMI });
            return data;

        }

        // HAM XOA
        public int Delete(string maMI)
        {
            string query = "DELETE DANHSACHMAYIN WHERE MAMAYIN= @ten ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maMI });
            return data;
        }

    }
}
