using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class QLyVTSDDAO
    {
        private static QLyVTSDDAO instance;

        public static QLyVTSDDAO Instance
        {
            get { if (instance == null) instance = new QLyVTSDDAO(); return QLyVTSDDAO.instance; }
            private set { QLyVTSDDAO.instance = value; }
        }

        private QLyVTSDDAO() {}

        public DataTable GetTable()
        {
            string query = "select * from QLYVATTUSD ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public List<QLyVTSDDTO> GetLsVtOfMaBT(string MaBT)
        {
            string query = " select * from QLYVATTUSD where MABT= @MaBT ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaBT });
            List<QLyVTSDDTO> ls = new List<QLyVTSDDTO>();
            foreach (DataRow item in data.Rows)
            {
                QLyVTSDDTO a = new QLyVTSDDTO(item);
                ls.Add(a);
            }
            return ls;

        }


        public List<QLyVTSDDTO> GetLsMaBTsdVtu(string MaVatTu)
        {
            string query = " select * from QLYVATTUSD where MAVTSD= @MaVT ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaVatTu });
            List<QLyVTSDDTO> ls = new List<QLyVTSDDTO>();
            foreach (DataRow item in data.Rows)
            {
                QLyVTSDDTO a = new QLyVTSDDTO(item);
                ls.Add(a);
            }
            return ls;
        }


        public List<QLyVTSDDTO> GetLsMaBTcanKK(string MaVatTu)
        {
            string query = " select * from QLYVATTUSD where MAVTSD= @MaVT and IDTTKIEMKE<1";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaVatTu });
            List<QLyVTSDDTO> ls = new List<QLyVTSDDTO>();
            foreach (DataRow item in data.Rows)
            {
                QLyVTSDDTO a = new QLyVTSDDTO(item);
                ls.Add(a);
            }
            return ls;
        }

        public List<QLyVTSDDTO> GetLsMaBTcanHT(string MaVatTu)
        {
            string query = " select * from QLYVATTUSD where MAVTSD= @MaVT and IDTTKIEMKE = 1";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaVatTu });
            List<QLyVTSDDTO> ls = new List<QLyVTSDDTO>();
            foreach (DataRow item in data.Rows)
            {
                QLyVTSDDTO a = new QLyVTSDDTO(item);
                ls.Add(a);
            }
            return ls;
        }

        public QLyVTSDDTO GetVTSDDTO(string MaBT, string MaVtu)
        {
            string query = " select * from QLYVATTUSD where MABT= @MaBT and MAVTSD= @MaVtu ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaBT, MaVtu });          
            QLyVTSDDTO a = new QLyVTSDDTO(data.Rows[0]);                         
            return a;
        }

        public bool CheckExist(string MaBT, string MaVtu)
        {
            string query = "select * from QLYVATTUSD where MABT= @MaBT and MAVTSD= @MaVtu ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaBT, MaVtu });
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


        public bool CheckSDVT(string MaBT)
        {
            string query = "select * from QLYVATTUSD where MABT= @MaBT ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaBT });
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

        // insert QLYVATTUSD(MABT,MAVTSD,TENVTSD,SLVTSD)
        public int Insert(string MaBT, string MaVtu, string TenVtu, int SoLuongSD, int IdTTKiemKe, string ChiTietTTKK)
        {
            string query = "insert QLYVATTUSD(MABT,MAVTSD,TENVTSD,SLVTSD,IDTTKIEMKE,CHITIETTTKK) values ( @MaBT , @mavattu , @ten , @soluong , @idKK , @ChitietTT ) ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaBT,MaVtu, TenVtu,  SoLuongSD,IdTTKiemKe,ChiTietTTKK });
            return data;
        }


        public int UpdateTTKK(string MaBT, int IdTTKiemKe, string ChiTietTTKK)
        {
            string query = " update QLYVATTUSD set IDTTKIEMKE= @id , CHITIETTTKK= @Chitiet WHERE MABT= @maBT ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {IdTTKiemKe,ChiTietTTKK, MaBT });
            return data;
        }

        public int DeleteVtu(string MaBT, string MaVtu)
        {
            string query = " DELETE QLYVATTUSD WHERE MABT= @maBT and MAVTSD= @MaVtu ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaBT, MaVtu });
            return data;
        }

        public int Delete(string MaBT)
        {
            string query = " DELETE QLYVATTUSD WHERE MABT= @maBT ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaBT });
            return data;
        }

    }
}
