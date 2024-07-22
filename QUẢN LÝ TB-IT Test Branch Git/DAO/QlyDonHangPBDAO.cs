using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
     public class QlyDonHangPBDAO
    {
        private static QlyDonHangPBDAO instance;

        public static QlyDonHangPBDAO Instance
        {
            get { if (instance == null) instance = new QlyDonHangPBDAO(); return QlyDonHangPBDAO.instance; }
            private set { QlyDonHangPBDAO.instance = value; }
        }
        private QlyDonHangPBDAO() { }

        public List<QlyDonHangPBDTO> GetLsDsDatHangDaSX()
        {
            string query = "select* from QLYDONHANGPB";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<QlyDonHangPBDTO> Ls = new List<QlyDonHangPBDTO>();
            foreach (DataRow item in data.Rows)
            {
                QlyDonHangPBDTO dathang = new QlyDonHangPBDTO(item);
                Ls.Add(dathang);
            }
            int dodai = Ls.Count();
            DateTime[] ngaydathang = new DateTime[dodai];
            int m = 0;
            foreach (QlyDonHangPBDTO item in Ls)
            {
                ngaydathang[m] = Convert.ToDateTime(item.NGAYDH);
                m++;
            }
            // Tiến hành sắp xếp mảng thời gian đặt hàng theo thứ tự giảm dần
            for (int i = 0; i < dodai - 1; i++)
            {
                for (int j = i + 1; j < dodai; j++)
                {
                    if (ngaydathang[j] > ngaydathang[i])
                    {
                        DateTime trunggian = ngaydathang[j];
                        ngaydathang[j] = ngaydathang[i];
                        ngaydathang[i] = trunggian;
                    }
                }
            }
           
            // Chuyển mảng về kiểu string
            string[] ngayDHstring = new string[dodai];
            for (int h = 0; h < dodai; h++)
            {
                ngayDHstring[h] = ngaydathang[h].ToString("dd/MM/yyyy");
            }
            for (int g = 0; g < dodai; g++)
            {
                for (int v = g + 1; v < dodai; v++)
                {
                    if (ngayDHstring[v] == ngayDHstring[g])
                    {
                        ngayDHstring[v] = "";
                    }
                }
            }
            List<QlyDonHangPBDTO> LsDsDHdasx = new List<QlyDonHangPBDTO>();
            for (int n = 0; n < dodai; n++)
            {

                if (ngayDHstring[n] != "")
                {
                    foreach (QlyDonHangPBDTO item1 in Ls)
                    {
                        if (item1.NGAYDH == ngayDHstring[n])
                        {
                            LsDsDHdasx.Add(item1);

                        }
                    }
                }
            }
            return LsDsDHdasx;
        }

        public List<QlyDonHangPBDTO> GetLsQuaDuKienNhan()
        {
            string query = " select* from QLYDONHANGPB ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<QlyDonHangPBDTO> LsQuaHan = new List<QlyDonHangPBDTO>(); 
            foreach (DataRow item in data.Rows)
            {
                QlyDonHangPBDTO a = new QlyDonHangPBDTO(item);
                DateTime ngayDH = Convert.ToDateTime(a.NGAYDH);

                TimeSpan time = DateTime.Now - ngayDH;
                int songay = time.Days;

                if (songay > 15)
                {
                    LsQuaHan.Add(a);
                }
            }

            return LsQuaHan;
        }

        public DataTable GetTable()
        {
            string query = " select* from QLYDONHANGPB ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public QlyDonHangPBDTO GetDonHangDTO(string MaDonHang)
        {
            string query = "select* from QLYDONHANGPB where MADONHANG= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaDonHang});
            QlyDonHangPBDTO MaDHDTO = new QlyDonHangPBDTO(data.Rows[0]);
            return MaDHDTO;
        }


        public bool CheckDHExist(string MaDonHang)
        {
            string query = "select* from QLYDONHANGPB where MADONHANG= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaDonHang });
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
        //   insert QLYDONHANGPB(MADONHANG, PHONGBAN, NGAYDH, TENHANG, SLDAT, DONVI, MDSD, NGAYNHAN, SLNHAN, NGAYBG, SLBG, GHICHU)

        // HAM THEM

        public int Insert(string MaDonHang, string PB, string NgayDH, string tenhang, int SlDAT, string DonVi,string NhaMay, string mdsd ,string ngaynhan ,int SLnhan,string ghichu)
        {
            string query = "insert QLYDONHANGPB(MADONHANG, PHONGBAN, NGAYDH, TENHANG, SLDAT, DONVI,NHAMAY, MDSD, NGAYNHAN, SLNHAN, GHICHU)" +
                                    " values ( @MaDH , @pb , @ngadh , @tenhang , @sldat , @donvi , @nhamay , @mdsd , @ngaynhan , @slnhan , @ghichu )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {  MaDonHang,  PB,  NgayDH,  tenhang, SlDAT,  DonVi,NhaMay,mdsd,ngaynhan,  SLnhan, ghichu });
            return data;
        }

        // HAM CAP NHAT

        public int UpdateNhanHang(string MaDonHang, string ngaynhan , int SLnhan, string ghichu)
        {
            string query = "UPDATE QLYDONHANGPB SET NGAYNHAN= @ngaynhan ,SLNHAN= @slnhan ,GHICHU= @ghichu WHERE MADONHANG= @maDH ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ngaynhan, SLnhan,  ghichu, MaDonHang });
            return data;
        }


        public int UpdateBanGiao(string MaDonHang, string ngaybg, int SLBG, string ghichu)
        {
            string query = "UPDATE QLYDONHANGPB SET NGAYBG= @ngaybg ,SLBG= @slbg ,GHICHU= @ghichu WHERE MADONHANG= @maDH ";          
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ngaybg,  SLBG, ghichu, MaDonHang });
            return data;
        }


        // HAM XOA
        public int Delete(string MaDonHang)
        {
            string query = "DELETE QLYDONHANGPB WHERE MADONHANG= @maDH ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaDonHang });
            return data;              
        }


    }
}
