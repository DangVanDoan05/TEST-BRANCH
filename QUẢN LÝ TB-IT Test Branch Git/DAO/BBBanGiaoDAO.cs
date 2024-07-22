using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
   public class BBBanGiaoDAO
    {
        private static BBBanGiaoDAO instance;

        public static BBBanGiaoDAO Instance
        {
            get { if (instance == null) instance = new BBBanGiaoDAO(); return BBBanGiaoDAO.instance; }
            private set { BBBanGiaoDAO.instance = value; }
        }
        private BBBanGiaoDAO() { }


        public DataTable GetTable()
        {
            string query = "select * from QUANLYBBBG";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
    
        public int Insert(string MaBBBG, string ngaybg, string MaNVBG, string MaNVNBG, string LYDOBG, string TENPB, string MaTB, string Donvi, int soluong, string tinhtrang)
        {
            string query = " insert QUANLYBBBG( MABBBG,NGAYBG,MANVBG,MANVNBG,LYDOBG,TENTB,MATB,DONVI,SOLUONG,TINHTRANG) " +
                "values ( @ma , @ngayBG , @mnvbg , @manvnbg , @lydo , @tenTB , @maTB , @Donvi , @Soluong , @tinhtrang )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaBBBG, ngaybg, MaNVBG, MaNVNBG, LYDOBG, TENPB, MaTB, Donvi, soluong, tinhtrang });
            return data;

        }
        // HAM SUA
        public int Update(string MaBBBG, string ngaybg, string MaNVBG, string MaNVNBG, string LYDOBG, string TENPB, string MaTB, string Donvi, int soluong, string tinhtrang)
        {
            string query = "UPDATE	QUANLYBBBG SET NGAYBG= @ngaybg ,MANVBG= @manvbg ,MANVNBG= @manvnbg ,LYDOBG= @lydobg ,TENTB= @tenTB ,MATB= @matb ,DONVI= @dvi ,SOLUONG= @soluong ,TINHTRANG= @tinhtrang WHERE MABBBG= @ma ";

            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ngaybg, MaNVBG, MaNVNBG, LYDOBG, TENPB, MaTB, Donvi, soluong, tinhtrang,MaBBBG });
            return data;

        }
        // HAM XOA
        public int Delete(string MaBB)
        {
            string query = "DELETE QUANLYBBBG WHERE MABBBG= @maBB ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaBB });
            return data;

        }
    }
}
