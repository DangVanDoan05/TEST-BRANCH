using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
   public class LsUpdatePMDAO
    {
        private static LsUpdatePMDAO instance;

        public static LsUpdatePMDAO Instance
        {
            get { if (instance == null) instance = new LsUpdatePMDAO(); return LsUpdatePMDAO.instance; }
            private set { LsUpdatePMDAO.instance = value; }
        }

        private LsUpdatePMDAO() { }

        public List<string> GetNgayUpdateDaSX()
        {
            string query = " SELECT DISTINCT NGAYCN FROM LSUPDATEPM ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<NgayCNDTO> LsNgayCN = new List<NgayCNDTO>();
            foreach (DataRow item in data.Rows)
            {
                NgayCNDTO a = new NgayCNDTO(item);
                LsNgayCN.Add(a);
            }
            int dodai = LsNgayCN.Count();
            DateTime[] MangNgayCN = new DateTime[dodai];
            int m = 0;
            foreach (NgayCNDTO item in LsNgayCN)
            {
                MangNgayCN[m] = item.NGAYCN;
                m++;
            }

          
            // Tiến hành sắp xếp mảng thời gian đặt hàng theo thứ tự giảm dần

            for (int i = 0; i < dodai - 1; i++)
            {
                for (int j = i + 1; j < dodai; j++)
                {
                    if (MangNgayCN[j] > MangNgayCN[i])
                    {
                        DateTime trunggian = MangNgayCN[j];
                        MangNgayCN[j] = MangNgayCN[i];
                        MangNgayCN[i] = trunggian;
                    }
                }
            }
            List<string> LsNgayCNDaSX = new List<string>();
            for (int k = 0; k < dodai; k++)
            {
                string NgayCN = MangNgayCN[k].ToString("dd/MM/yyyy");
                LsNgayCNDaSX.Add(NgayCN);
            }
            return LsNgayCNDaSX;
        }


        public List<LsUpdatePMDTO> GetLsThongTinCN() // Cần sắp xếp theo thời gian
        {
            string query = " select * from LSUPDATEPM ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<string> LsNgayCNDaSX = GetNgayUpdateDaSX();
            List<LsUpdatePMDTO> Ls = new List<LsUpdatePMDTO>();
            foreach (string item in LsNgayCNDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    LsUpdatePMDTO a = new LsUpdatePMDTO(item1);
                    string[] b =a.NGAYCN.Split(' ');

                    if (b[0] == item) // Lấy ra có ngày thôi
                    {
                        Ls.Add(a);
                    }
                }
            }
            return Ls;
        }

        public LsUpdatePMDTO GetUpdateDTO(int ID)
        {
            string query = "select * from LSUPDATEPM where ID= @id ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {ID});
            LsUpdatePMDTO a = new LsUpdatePMDTO(data.Rows[0]);
            return a;
        }



        public bool CheckExist(string MaPM,string NgayCN) 
        {
            string query = "select * from LSUPDATEPM where MAPM= @ma and NGAYCN= @ngay ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPM,NgayCN });
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

       // insert LSUPDATEPM(MAPM, TENPM, NHAMAY, PB, TGYC, TGHT)

        public int Insert(string MaPM, string TenPM,string NhaMay,string PB,string TgYC,string TgHT)
        {
            string query = "insert LSUPDATEPM(MAPM, TENPM, NHAMAY, PB, TGYC, TGHT)" +
                                  " values ( @MaPM , @TenPM , @nhamay , @pb , @TgYC , @TgHT ) ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaPM,  TenPM,  NhaMay,  PB, TgYC, TgHT });
            return data;
        }



        public int DeleteLanCN(int ID)
        {
            string query = " DELETE LSUPDATEPM WHERE ID= @id ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ID });
            return data;
        }


    }
}
