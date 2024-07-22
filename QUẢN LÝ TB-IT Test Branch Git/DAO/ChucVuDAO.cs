using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
   public class ChucVuDAO
    {
        private static ChucVuDAO instance;

        public static ChucVuDAO Instance
        {
            get { if (instance == null) instance = new ChucVuDAO(); return ChucVuDAO.instance; }
            private set { ChucVuDAO.instance = value; }
        }

        private ChucVuDAO() { }

        // HAM LAY BANG
        public DataTable GetTable()
        {
            string query = "select* from QUANLYCHUCVU";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public List<ChucVuDTO> GetLsvCVDTO()
        {
            string query = "select* from QUANLYCHUCVU";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<ChucVuDTO> lsv = new List<ChucVuDTO>();
            foreach (DataRow item in data.Rows)
            {
                ChucVuDTO cvDTO = new ChucVuDTO(item);
                lsv.Add(cvDTO);
            }
            // Sắp xếp danh sách theo bậc chức vụ
            int dodai = lsv.Count;
            // DateTime[] ngaydathang = new DateTime[dodai];
            ChucVuDTO[] MangCV = new ChucVuDTO[dodai];
            int m = 0;

            foreach (ChucVuDTO item in lsv)
            {
                MangCV[m] = item;
                m++;
            }

            for (int i = 0; i < dodai - 1; i++)
            {
                for (int j = i + 1; j < dodai; j++)
                {
                    if (MangCV[j].BACCV < MangCV[i].BACCV)
                    {
                        // Đổi chỗ
                        ChucVuDTO trunggian = new ChucVuDTO(MangCV[j].MACHUCVU, MangCV[j].TENCHUCVU, MangCV[j].BACCV);
                        MangCV[j] = MangCV[i];
                        MangCV[i] = trunggian;
                    }
                }
            }
            List<ChucVuDTO> LsDaSX = new List<ChucVuDTO>();
            for (int k = 0; k < dodai; k++)
            {
                LsDaSX.Add(MangCV[k]);
            }
            return LsDaSX;
        }
        public ChucVuDTO GetChucVuDTO(string MaCV)
        {
            string query = "select* from QUANLYCHUCVU where MACHUCVU= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {MaCV});
            ChucVuDTO a = new ChucVuDTO(data.Rows[0]);
            return a;
        }

        public int GetBacCV(string MaChucVu)
        {         
            string query = "select * from QUANLYCHUCVU where MACHUCVU= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaChucVu });
            ChucVuDTO a = new ChucVuDTO(data.Rows[0]);
            return a.BACCV;
        }

        //public bool CheckQLCC(string MaNV)
        //{
        //    int BacCV = GetBacCV(MaNV);
        //    if (BacCV == 2)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public bool CheckQLTC(string MaNV)
        //{
        //    int BacCV = GetBacCV(MaNV);
        //    if(BacCV==3)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    } 
        //}

        //public bool CheckQLSC(string MaNV)
        //{
        //    int BacCV = GetBacCV(MaNV);
        //    if (BacCV == 4)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

        public List<ChucVuDTO> GetTableBacCVlonhon(int BacCV)
        {
            string query = "select* from QUANLYCHUCVU where BACCV> @bac ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { BacCV });
            List<ChucVuDTO> lsv = new List<ChucVuDTO>();
            foreach (DataRow item in data.Rows)
            {
                ChucVuDTO cvDTO = new ChucVuDTO(item);
                lsv.Add(cvDTO);
            }
            // Sắp xếp danh sách theo bậc chức vụ
            int dodai = lsv.Count;
            // DateTime[] ngaydathang = new DateTime[dodai];
            ChucVuDTO[] MangCV = new ChucVuDTO[dodai];
            int m = 0;

            foreach (ChucVuDTO item in lsv)
            {
                MangCV[m] = item;
                m++;
            }

            for (int i = 0; i < dodai - 1; i++)
            {
                for (int j = i + 1; j < dodai; j++)
                {
                    if (MangCV[j].BACCV < MangCV[i].BACCV)
                    {
                        // Đổi chỗ
                        ChucVuDTO trunggian = new ChucVuDTO(MangCV[j].MACHUCVU, MangCV[j].TENCHUCVU, MangCV[j].BACCV);
                        MangCV[j] = MangCV[i];
                        MangCV[i] = trunggian;
                    }
                }
            }
            List<ChucVuDTO> LsDaSX = new List<ChucVuDTO>();
            for (int k = 0; k < dodai; k++)
            {
                LsDaSX.Add(MangCV[k]);
            }
            return LsDaSX;
        }

        public bool CheckMaCVExist(string MaCV, int BacCV)
        {
            string query = "select * from QUANLYCHUCVU where MACHUCVU= @maCV or BACCV= @bac ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaCV, BacCV });
            int dem = data.Rows.Count;
            if(dem>0)
            {
                return true;
            }
            else
            {
                return false;
            }         
        }
        public List<ChucVuDTO> GetLsvCVDaSX()
        {
            string query = " select * from QUANLYCHUCVU ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<ChucVuDTO> lsv = new List<ChucVuDTO>();
            foreach (DataRow item in data.Rows)
            {
                ChucVuDTO cvDTO = new ChucVuDTO(item);
                lsv.Add(cvDTO);
            }

            // Sắp xếp danh sách theo bậc chức vụ
            int dodai = lsv.Count;
            // DateTime[] ngaydathang = new DateTime[dodai];
            ChucVuDTO[] MangCV = new ChucVuDTO[dodai];
            int m = 0;

            foreach (ChucVuDTO item in lsv)
            {
                MangCV[m] = item;
                m++;
            }

            for(int i=0;i<dodai-1;i++)
            {
                for(int j=i+1;j<dodai;j++)
                {
                    if(MangCV[j].BACCV<MangCV[i].BACCV)
                    {
                        // Đổi chỗ
                        ChucVuDTO trunggian = new ChucVuDTO(MangCV[j].MACHUCVU, MangCV[j].TENCHUCVU, MangCV[j].BACCV);
                        MangCV[j] = MangCV[i];
                        MangCV[i] = trunggian;
                    }
                }
            }
            List<ChucVuDTO> LsDaSX = new List<ChucVuDTO>();
            for (int k = 0; k < dodai ; k++)
            {
                LsDaSX.Add(MangCV[k]);
            }
            return LsDaSX;
        }
        public int Insert(string MaCV, string TenCV,int BacCV)
        {
            string query = "insert QUANLYCHUCVU(MACHUCVU,TENCHUCVU,BACCV) values( @ma , @ten , @baccv )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaCV,TenCV,BacCV});
            return data;
        }

        // HAM SUA
        public int Update(string MaCV, string TenCV, int BacCV)
        {
            string query = "UPDATE	QUANLYCHUCVU SET TENCHUCVU= @ten ,BACCV= @bacCV WHERE MACHUCVU= @ma ";          
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {TenCV,BacCV,MaCV});
            return data;
        }

        // HAM XOA
        public int Delete(string MaCV)
        {
            string query = "DELETE QUANLYCHUCVU WHERE MACHUCVU= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaCV });
            return data;
        }
    }
}
