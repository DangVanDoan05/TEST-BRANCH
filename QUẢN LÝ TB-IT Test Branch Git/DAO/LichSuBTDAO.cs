using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
   public class LichSuBTDAO
    {
        private static LichSuBTDAO instance;

        public static LichSuBTDAO Instance
        {
            get { if (instance == null) instance = new LichSuBTDAO(); return LichSuBTDAO.instance; }
            private set { LichSuBTDAO.instance = value; }
        }

        private LichSuBTDAO() {}

        public List<string> GetNgayBTDaSX()
        {
            string query = " SELECT DISTINCT NGAY FROM LICHSUBAOTRI ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<NgayDTO> LsNgayBT = new List<NgayDTO>();
            foreach (DataRow item in data.Rows)
            {
                NgayDTO a = new NgayDTO(item);
                LsNgayBT.Add(a);
            }
            int dodai = LsNgayBT.Count();
            DateTime[] MangNgayBT = new DateTime[dodai];
            int m = 0;
            foreach (NgayDTO item in LsNgayBT)
            {
                MangNgayBT[m] = item.NGAY;
                m++;
            }

            // Sắp xếp ngày bảo trì.
            // Tiến hành sắp xếp mảng thời gian đặt hàng theo thứ tự giảm dần

            for (int i = 0; i < dodai - 1; i++)
            {
                for (int j = i + 1; j < dodai; j++)
                {
                    if (MangNgayBT[j] > MangNgayBT[i])
                    {
                        DateTime trunggian = MangNgayBT[j];
                        MangNgayBT[j] = MangNgayBT[i];
                        MangNgayBT[i] = trunggian;
                    }
                }
            }
            List<string> LsNgayBTDaSX = new List<string>();
            for (int k = 0; k < dodai; k++)
            {
                string NgayBT = MangNgayBT[k].ToString("dd/MM/yyyy");
                LsNgayBTDaSX.Add(NgayBT);
            }
            return LsNgayBTDaSX;
        }


        public List<LichSuBTDTO> GetLsBT() // Cần sắp xếp theo thời gian
        {
            string query = " select * from LICHSUBAOTRI ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<string> LsNgayYCDaSX = GetNgayBTDaSX();
            List<LichSuBTDTO> Ls = new List<LichSuBTDTO>();
            foreach (string item in LsNgayYCDaSX)
            {
                foreach (DataRow item1 in data.Rows)
                {
                    LichSuBTDTO a = new LichSuBTDTO(item1);
                    if (a.NGAY == item)
                    {
                        Ls.Add(a);
                    }
                }
            }                   
            return Ls;
        }


        public LichSuBTDTO GetLsBTDTO(string MaBT)
        {
            string query = " select * from LICHSUBAOTRI where MABT= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaBT });          
            LichSuBTDTO a = new LichSuBTDTO(data.Rows[0]);                         
            return a;
        }

        public bool CheckExist(string MaBT)
        {
            string query = "select * from LICHSUBAOTRI where MABT= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaBT });
            int dem= data.Rows.Count;
            if(dem>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

        public int Insert(string MaBT, string MaMT, string NhaMay, string PB, string Ngay, string HangMucBT, string VTSD,string VTTH,int idTTKK,string ChiTietTTKK,string ghichu, string NguoiCN)
        {
            string query = "insert LICHSUBAOTRI(MABT,MAMT,NHAMAY,PB,NGAY,HMBT,VTSD,VTTH,IDTTKIEMKE,CHITIETTTKK,GHICHU,NGUOICN)" +
                             " values ( @MaBT , @MaMT , @NM , @PB , @NGay , @HMBT , @VTSD , @VTTH , @ID , @Chitiet , @ghichu , @NguoiCN ) ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {MaBT,MaMT,NhaMay,PB,Ngay,HangMucBT, VTSD,VTTH,idTTKK,ChiTietTTKK,ghichu,NguoiCN});
            return data;
        }


        public int UpdateTTKK(string MaBT, int idTTKK, string ChiTietTTKK)
        {
            string query = " Update LICHSUBAOTRI set IDTTKIEMKE= @id ,CHITIETTTKK= @ChiTiet WHERE MABT= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {idTTKK, ChiTietTTKK, MaBT });
            return data;
        }


        public int Delete(string MaBT)
        {
            string query = " DELETE LICHSUBAOTRI WHERE MABT= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaBT });
            return data;
        }

    }
}
