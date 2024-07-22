using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class TonLKThuHoiDAO
    {
        private static TonLKThuHoiDAO instance;

        public static TonLKThuHoiDAO Instance
        {
            get { if (instance == null) instance = new TonLKThuHoiDAO(); return TonLKThuHoiDAO.instance; }
            private set { TonLKThuHoiDAO.instance = value; }
        }
        private TonLKThuHoiDAO() { }
      

        public List<TonLKThuHoiDTO> GetListMaLKTon()
        {
            string query = "select * from TONLKTHUHOI";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<TonLKThuHoiDTO> lsv = new List<TonLKThuHoiDTO>();
            foreach (DataRow item in data.Rows)
            {
                TonLKThuHoiDTO ton = new TonLKThuHoiDTO(item);
                lsv.Add(ton);
            }
            return lsv;
        }

        public List<TonLKThuHoiDTO> GetListMaLKTH()
        {
            string query = "select * from TONLKTHUHOI";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<TonLKThuHoiDTO> lsv = new List<TonLKThuHoiDTO>();
            foreach (DataRow item in data.Rows)
            {
                TonLKThuHoiDTO ton = new TonLKThuHoiDTO(item);
                if(ton.SLTON>0)
                {
                    lsv.Add(ton);
                }
                else
                {
                    Delete(ton.MALK);
                }              
            }
            return lsv;
        }



        public TonLKThuHoiDTO GetTonLKTHDTO(string MaLKTH)
        {
            string query = " select * from TONLKTHUHOI where MALK= @malk ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {MaLKTH});          
            TonLKThuHoiDTO ton = new TonLKThuHoiDTO(data.Rows[0]);
            return ton;
        }


        public DataTable GetTable()
        {
            string query = "select * from TONLKTHUHOI";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public bool CheckMaLKTHTon(string malkTH)
        {
            string query = "select * from TONLKTHUHOI where MALK= @malk ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { malkTH });
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

        public int Insert(string malkTH, string tenlkTH, int slton, string dvtinh, int idKiemKe, string ChiTietKK)
        {
            string query = "insert TONLKTHUHOI(MALK,TENLK,SLTON,DVTINH,IDTTKK,CHITIETTTKK)" +
                                " values ( @ma , @ten , @slton , @dvtinh , @idKK , @chitietKK ) ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {malkTH,  tenlkTH, slton,  dvtinh, idKiemKe, ChiTietKK });
            return data;
        }

        public int UpdateTTKK(string malkTH, int idKiemKe, string ChiTietKK)
        {
            string query = "update TONLKTHUHOI set IDTTKK= @id ,CHITIETTTKK= @chitiet where MALK= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {idKiemKe, ChiTietKK,malkTH});
            return data;
        }

        public int UpdateSLTON(string malk, int slton)
        {
            string query = "update TONLKTHUHOI set SLTON= @ton where MALK= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { slton, malk });
            return data;
        }

        public int Delete(string malkTH)
        {
            string query = " DELETE TONLKTHUHOI WHERE MALK= @malk ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { malkTH });
            return data;
        }

    }
}
