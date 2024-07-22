using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class NhomDAO
    {
        private static NhomDAO instance;

        public static NhomDAO Instance
        {
            get { if (instance == null) instance = new NhomDAO(); return NhomDAO.instance; }
            private set { NhomDAO.instance = value; }
        }


        private NhomDAO(){}
        
        public DataTable GetTable()
        {
            string query = "select * from QLNHOM";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public NhomDTO GetNhomDTO(string MaNhom, string NhaMay, string BoPhan, string PhongBan )
        {
            string query = "select * from QLNHOM where MANHOM= @ma and NHAMAY= @NM and BOPHAN= @BP and PHONGBAN= @pb ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNhom, NhaMay,  BoPhan, PhongBan });
            NhomDTO a = new NhomDTO(data.Rows[0]);
            return a;
        }

        public List<NhomDTO> GetNhomOfPB(string MaPB)
        {
            string query = "select * from QLNHOM where PHONGBAN= @pb ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPB });
            List<NhomDTO> ls = new List<NhomDTO>();
            foreach (DataRow item in data.Rows)
            {
                NhomDTO a = new NhomDTO(item);
                ls.Add(a);
            }
            return ls;
        }
        public bool CheckNhomTP(string MaNhom,string MaPB)
        {
            string query = "select * from QLNHOM where MANHOM= @ma and PHONGBAN= @pb ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {MaNhom,MaPB});
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

        public bool CheckPbNoGroup( string MaPB )
        {
            string query = "select * from QLNHOM where PHONGBAN= @pb ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPB });
            int dem = data.Rows.Count;
            if (dem <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckNhomExist(string MaNhom, string NhaMay, string BoPhan, string PhongBan)
        {
            string query = "select * from QLNHOM where MANHOM= @ma and NHAMAY= @NM and BOPHAN= @BP and PHONGBAN= @pb ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNhom, NhaMay, BoPhan, PhongBan });
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

        public List<NhomDTO> GetLsvNhomThuocBP(string MaBP)
        {            
            List<PhongBanDTO> lsv = PhongBanDAO.Instance.GetLsvPbThuocBP(MaBP);
            List<string> LsPB = new List<string>();
            foreach (PhongBanDTO item in lsv)
            {
                LsPB.Add(item.MAPB);
            }
            List<NhomDTO> LsNhomOfBP = new List<NhomDTO>();
            foreach (string item in LsPB)
            {
                List<NhomDTO> LsNhomOfPB = NhomDAO.instance.GetNhomOfPB(item);
                foreach (NhomDTO item1 in LsNhomOfPB)
                {
                    LsNhomOfBP.Add(item1);
                }
            }
            return LsNhomOfBP;
        }

        public int Insert(string MaNhom, string TenNhom,string GhiChu,string NhaMay, string BoPhan,string PhongBan)
        {
            string query = "insert QLNHOM(MANHOM,TENNHOM,GHICHU,NHAMAY,BOPHAN,PHONGBAN) values( @ma , @ten , @ghichu , @NhaMay , @BoPhan , @phongban )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaNhom,TenNhom,GhiChu,NhaMay ,BoPhan,PhongBan});
            return data;
        }
       
      


        public int Delete(string MaNhom, string NhaMay, string BoPhan, string PhongBan)
        {
            string query = " DELETE QLNHOM WHERE MANHOM= @ma and NHAMAY= @NM and BOPHAN= @BP and PHONGBAN= @pb ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {MaNhom , NhaMay, BoPhan, PhongBan});
            return data;
        }
    }
}
