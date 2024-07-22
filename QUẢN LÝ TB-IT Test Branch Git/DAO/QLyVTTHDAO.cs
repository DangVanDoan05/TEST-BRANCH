using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
   public class QLyVTTHDAO
    {
        private static QLyVTTHDAO instance;

        public static QLyVTTHDAO Instance
        {
            get { if (instance == null) instance = new QLyVTTHDAO(); return QLyVTTHDAO.instance; }
            private set { QLyVTTHDAO.instance = value; }
        }

        private QLyVTTHDAO() { }

        public DataTable GetTable()
        {
            string query = "select * from QLYVATTUTH ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public List<QLyVTTHDTO> GetLsVtOfMaBT(string Mabt)
        {
            string query = " select * from QLYVATTUTH where MABT= @MaBT ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { Mabt });
            List<QLyVTTHDTO> ls = new List<QLyVTTHDTO>();
            foreach (DataRow item in data.Rows)
            {
                QLyVTTHDTO a = new QLyVTTHDTO(item);
                ls.Add(a);
            }
            return ls;
        }

        public QLyVTTHDTO GetVtTHOfMaBTdto(string Mabt,string MaVtu)
        {
            string query = " select * from QLYVATTUTH where MABT= @MaBT and MAVTTH= @MaVtu ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { Mabt , MaVtu });          
            QLyVTTHDTO a = new QLyVTTHDTO(data.Rows[0]);                       
            return a;
        }

        public bool CheckTHVT(string MaBT)
        {
            string query = "select * from QLYVATTUTH where MABT= @MaBT ";
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

        public bool CheckExist(string MaBT, string MaVtu)
        {
            string query = "select * from QLYVATTUTH where MABT= @MaBT and MAVTTH= @MaVtu ";
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

        // insert QLYVATTUTH(MABT,MAVTTH,TENVTTH,SLVTTH)
        public int Insert(string MaBT, string MaVtuTH, string TenVtuTH, int SoLuongTH,string TinhTrang)
        {
            string query = "insert QLYVATTUTH(MABT,MAVTTH,TENVTTH,SLVTTH,TINHTRANG) values ( @MaBT , @mavattuTh , @ten , @soluong , @tinhtrang ) ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaBT,  MaVtuTH,TenVtuTH,  SoLuongTH,TinhTrang });
            return data;
        }


        public int UpdateTT(string MaBT,string MaVtuTH, string TinhTrang)
        {
            string query = " Update QLYVATTUTH set TINHTRANG= @tt WHERE MABT= @maBT and MAVTTH= @MaVtu ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {TinhTrang,MaBT,MaVtuTH });
            return data;
        }



        public int DeleteVtu(string MaBT, string MaVtuTH)
        {
            string query = " DELETE QLYVATTUTH WHERE MABT= @maBT and MAVTTH= @MaVtu ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaBT, MaVtuTH });
            return data;
        }

        public int Delete(string MaBT)
        {
            string query = " DELETE QLYVATTUTH WHERE MABT= @maBT ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaBT });
            return data;
        }

    }
}
