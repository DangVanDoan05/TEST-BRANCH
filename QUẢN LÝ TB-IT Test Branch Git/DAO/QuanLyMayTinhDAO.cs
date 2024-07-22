using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class QuanLyMayTinhDAO
    {
        private static QuanLyMayTinhDAO instance;

        public static QuanLyMayTinhDAO Instance
        {
            get { if (instance == null) instance = new QuanLyMayTinhDAO(); return QuanLyMayTinhDAO.instance; }
            private set { QuanLyMayTinhDAO.instance = value; }
        }
        private QuanLyMayTinhDAO() { }


        public DataTable GetTable()
        {
            string query = "select * from QLYMAYTINH";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public int TongMT()
        {
            string query = "select * from QLYMAYTINH";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data.Rows.Count;
        }

        public List<QuanLyMayTinhDTO> GetListMaMT()
        {
            string query = "select * from QLYMAYTINH";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<QuanLyMayTinhDTO> lsv = new List<QuanLyMayTinhDTO>();
            foreach (DataRow item in data.Rows)
            {
                QuanLyMayTinhDTO maMT = new QuanLyMayTinhDTO(item);
                lsv.Add(maMT);
            }

            return lsv;
        }

        public List<QuanLyMayTinhDTO> GetListMaMTPB(string MaPB)
        {
            string query = "select * from QLYMAYTINH where PHONGBAN= @pb ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {MaPB});
            List<QuanLyMayTinhDTO> lsv = new List<QuanLyMayTinhDTO>();
            foreach (DataRow item in data.Rows)
            {
                QuanLyMayTinhDTO maMT = new QuanLyMayTinhDTO(item);
                lsv.Add(maMT);
            }
            return lsv;
        }

        public List<QuanLyMayTinhDTO> GetLsMTBaoTri(string NhaMay,string MaPB)
        {
            string query = "select * from QLYMAYTINH where NHAMAY= @NM and PB= @pb and BAOHANH=0 and LOAIMT!='LAPTOP' ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {NhaMay, MaPB });
            List<QuanLyMayTinhDTO> lsv = new List<QuanLyMayTinhDTO>();
            foreach (DataRow item in data.Rows)
            {
                QuanLyMayTinhDTO maMT = new QuanLyMayTinhDTO(item);
                lsv.Add(maMT);
            }
            return lsv;
        }


      


        public List<string> GetListMaPBDD1()
        {
            string query = " select * from QLYMAYTINH where NHAMAY = 'DD1' ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);          
            List<string> LsMaPB = new List<string>();
            foreach (DataRow item in data.Rows)
            {
                QuanLyMayTinhDTO maMT = new QuanLyMayTinhDTO(item);
                string MaPB = maMT.PB;
                if(!LsMaPB.Contains(MaPB))
                {
                    LsMaPB.Add(MaPB);
                }
            }
            return LsMaPB;
        }

        public List<string> GetListMaPBDD2()
        {
            string query = " select * from QLYMAYTINH where NHAMAY = 'DD2' ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<string> LsMaPB = new List<string>();
            foreach (DataRow item in data.Rows)
            {
                QuanLyMayTinhDTO maMT = new QuanLyMayTinhDTO(item);
                string MaPB = maMT.PB;
                if (!LsMaPB.Contains(MaPB))
                {
                    LsMaPB.Add(MaPB);
                }
            }
            return LsMaPB;
        }

        public List<string> GetListMaPBDDK()
        {
            string query = " select * from QLYMAYTINH where NHAMAY = 'DDK' ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<string> LsMaPB = new List<string>();
            foreach (DataRow item in data.Rows)
            {
                QuanLyMayTinhDTO maMT = new QuanLyMayTinhDTO(item);
                string MaPB = maMT.PB;
                if (!LsMaPB.Contains(MaPB))
                {
                    LsMaPB.Add(MaPB);
                }
            }
            return LsMaPB;
        }


      

        public List<QuanLyMayTinhDTO> GetListMaMTBP(string MaBoPhan)
        {
            List<QuanLyMayTinhDTO> lsvMTOfBP = new List<QuanLyMayTinhDTO>();
            List<PhongBanDTO> LsPbOfBP = PhongBanDAO.Instance.GetLsvPbThuocBP(MaBoPhan);
            foreach (PhongBanDTO item in LsPbOfBP)
            {
                string MaPB = item.MAPB;
                List<QuanLyMayTinhDTO> lsvYCOfPB = GetListMaMTPB(MaPB);
                foreach (QuanLyMayTinhDTO item1 in lsvYCOfPB)
                {
                    lsvMTOfBP.Add(item1);
                }
            }
            return lsvMTOfBP;
        }

        public QuanLyMayTinhDTO GetMaMT(string mamt)
        {
            string query = "select * from QLYMAYTINH where MAMT= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { mamt });
            QuanLyMayTinhDTO maMTDTO = new QuanLyMayTinhDTO(data.Rows[0]);
            return maMTDTO;
        }

        public QuanLyMayTinhDTO GetMaMT1(string mamt)
        {
            string query = $"select * from QLYMAYTINH where MAMT='{mamt}' ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            QuanLyMayTinhDTO maMTDTO = new QuanLyMayTinhDTO(data.Rows[0]);
            return maMTDTO;
        }

        public bool CheckMaMTExist(string MaMT)
        {
            string query = "select * from QLYMAYTINH where MAMT= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaMT });
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

        public int Insert(string MaMT, string IP, string MAC,string Domain , string LOAIMT, string NCC,string NhaMay, string phongban, string nguoisd, string matscd, string ngaymua, string hanbh,bool baohanh,string ghichu)
        {
            string query = "insert QLYMAYTINH(MAMT,IP,MAC,DOMAIN,LOAIMT,NCC,NHAMAY,PB,NGUOISD,MATSCD,NGAYMUA,HANBH,BAOHANH,GHICHU)" +
                        " values ( @maMT , @IP , @mac , @Domain , @loaimt , @ncc , @nhamay , @pb , @ngsd , @matscd , @ngaymua , @hbh , @baohanh , @ghichu )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {  MaMT,  IP,  MAC,Domain, LOAIMT,NCC, NhaMay,  phongban, nguoisd, matscd, ngaymua,  hanbh,  baohanh, ghichu });
            return data;
        }

        // HAM SUA

        public int Update(string MaMT, string IP, string MAC,string Domain, string LOAIMT, string NCC, string NhaMay, string phongban, string nguoisd, string matscd, string ngaymua, string hanbh, bool baohanh, string ghichu)
        {
            string query = "UPDATE QLYMAYTINH set IP= @ip ,MAC= @mac ,DOMAIN= @domain ,LOAIMT= @LoaiMT ,NCC= @NCC ,NHAMAY= @nhamay ,PB= @pb ,NGUOISD= @NgSD ,MATSCD= @MaTSCD " +
                ",NGAYMUA= @NgayMua ,HANBH= @hanBH ,BAOHANH= @bh ,GHICHU= @GhiChu where MAMT= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {IP, MAC,Domain,LOAIMT, NCC, NhaMay, phongban, nguoisd, matscd, ngaymua, hanbh, baohanh, ghichu,MaMT});
            return data;
        }

       

        // HAM XOA
        public int Delete(string MaMT)
        {
            string query = "DELETE QLYMAYTINH WHERE MAMT= @ma ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaMT });
            return data;

        }

    }
}
