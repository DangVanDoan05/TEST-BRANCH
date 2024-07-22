using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
   public class PhongBanDAO
    {
        private static PhongBanDAO instance;

        public static PhongBanDAO Instance
        {
            get { if (instance == null) instance = new PhongBanDAO(); return PhongBanDAO.instance; }
            private set { PhongBanDAO.instance = value; }
        }

        private PhongBanDAO() { }

        // HAM LAY BANG

        public DataTable GetTable()
        {
            string query = " select* from PHONGBAN ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }


        // HÀM LẤY RA CỘT PHÒNG BAN
        // lay ra list nhung hang

        public List<PhongBanDTO> GetLsvPbNoBP()
        {
            string query = " select * from PHONGBAN where BOPHAN='' ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<PhongBanDTO> lsv = new List<PhongBanDTO>();
            foreach (DataRow item in data.Rows)
            {
                PhongBanDTO phongBanDTO = new PhongBanDTO(item);
                lsv.Add(phongBanDTO);
            }
            return lsv;

        }

        // Tìm những phòng ban không tồn tại trong bảng nhóm:
        public List<PhongBanDTO> GetLsvPbNoGroup()
        {
            string query = " select * from PHONGBAN ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<PhongBanDTO> lsvPB = new List<PhongBanDTO>();
            foreach (DataRow item in data.Rows)
            {
                PhongBanDTO phongBanDTO = new PhongBanDTO(item);
                lsvPB.Add(phongBanDTO);
            }
            List<PhongBanDTO> lsvPbNoGroup = new List<PhongBanDTO>();
            foreach (PhongBanDTO item in lsvPB)
            {
                string MaPB = item.MAPB;
                bool CheckPbNoGroup = NhomDAO.Instance.CheckPbNoGroup(MaPB);
                if(CheckPbNoGroup)
                {
                    lsvPbNoGroup.Add(item);
                }
            }
            return lsvPbNoGroup;
        }

        public List<PhongBanDTO> GetLsvPbThuocBP(string MaBP)
        {
            string query = " select * from PHONGBAN where BOPHAN= @bophan ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {MaBP});
            List<PhongBanDTO> lsv = new List<PhongBanDTO>();
            foreach (DataRow item in data.Rows)
            {
                PhongBanDTO phongBanDTO = new PhongBanDTO(item);
                lsv.Add(phongBanDTO);
            }              
            return lsv;
        }
        public List<PhongBanDTO> GetLsvPB()
        {
            string query = " select * from PHONGBAN ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<PhongBanDTO> lsv = new List<PhongBanDTO>();
            foreach (DataRow item in data.Rows)
            {
                PhongBanDTO phongBanDTO = new PhongBanDTO(item);
                lsv.Add(phongBanDTO);
            }
            return lsv;

        }
      

        public PhongBanDTO GetPBDTO(string MaPB, string NhaMay, string BoPhan)
        {
            string query = " select * from PHONGBAN where MAPB= @ma and NHAMAY= @nm and BOPHAN= @BoPhan ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPB, NhaMay, BoPhan });                       
            PhongBanDTO phongBanDTO = new PhongBanDTO(data.Rows[0]);                        
            return phongBanDTO;
        }
     
        public bool CheckPBExist(string MaPB, string NhaMay, string BoPhan)
        {
            string query = "select * from PHONGBAN where MAPB= @maPB and NHAMAY= @nm and BOPHAN= @BoPhan ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPB,NhaMay,BoPhan});
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

        // HAM THEM

        public int Insert(string MaPB, string TenPB, string GhiChu,string NhaMay, string BoPhan)
        {
            string query = "insert PHONGBAN(MAPB,TENPB,GHICHU,NHAMAY,BOPHAN) values( @maPB , @tenPB , @ghichu , @nm , @bp )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {  MaPB, TenPB,  GhiChu,NhaMay, BoPhan });
            return data;
        }

        // HAM SUA

        public int Update(string MaPB, string TenPB, string GhiChu, string NhaMay, string BoPhan)
        {
            string query = "UPDATE	PHONGBAN SET TENPB= @tenPB ,GHICHU= @ghichu WHERE MAPB= @maPB and NHAMAY= @nm and BOPHAN= @BoPhan ";           
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {TenPB,GhiChu,MaPB,NhaMay,BoPhan});
            return data;
        }

        // HAM XOA

        public int Delete(string MaPB, string NhaMay, string BoPhan)
        {
            string query = "DELETE PHONGBAN WHERE MAPB= @maPB and NHAMAY= @nm and BOPHAN= @BoPhan ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaPB, NhaMay, BoPhan});
            return data;
        }

    }

}
