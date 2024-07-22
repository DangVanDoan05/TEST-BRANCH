using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class QLNhanVienDAO
    {
        private static QLNhanVienDAO instance;

        public static QLNhanVienDAO Instance
        {
            get { if (instance == null) instance = new QLNhanVienDAO(); return QLNhanVienDAO.instance; }
            private set { QLNhanVienDAO.instance = value; }
        }
        private QLNhanVienDAO() { }

        // HAM LAY BANG
        public DataTable GetTable()
        {
            string query = "select * from QLYNHANVIEN";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }


        public DataTable GetNVOfPB(string MaPB)
        {
            string query = "select * from QLYNHANVIEN where PHONGBAN= @pb ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPB });
            return data;
        }

        public List<QLNhanVienDTO> GetAllNVNoADID() // thuộc cùng phòng ban và dưới chức vụ 
        {
            string query = "select * from QLYNHANVIEN  ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<QLNhanVienDTO> LsNVduoiCap = new List<QLNhanVienDTO>();
            foreach (DataRow item in data.Rows)
            {
                QLNhanVienDTO nhanvienDTO = new QLNhanVienDTO(item);
                bool CheckADID = QLyYCADIDDAO.Instance.CheckExistADID(nhanvienDTO.MANV);
                if (!CheckADID)
                {                  
                    LsNVduoiCap.Add(nhanvienDTO);                   
                }
            }
            return LsNVduoiCap;
        }

        public List<QLNhanVienDTO> GetNVOfPBDuoiCV(string MaPB,int BaCV) // thuộc cùng phòng ban và dưới chức vụ 
        {
            string query = "select * from QLYNHANVIEN where PHONGBAN= @pb ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPB });
            List<QLNhanVienDTO> LsNVduoiCap = new List<QLNhanVienDTO>();
            foreach (DataRow item in data.Rows)
            {
                QLNhanVienDTO nhanvienDTO = new QLNhanVienDTO(item);
                int bacCV = ChucVuDAO.Instance.GetBacCV(nhanvienDTO.CHUCVU);
                if(bacCV>BaCV)
                {
                   LsNVduoiCap.Add(nhanvienDTO);
                }
            }
            return LsNVduoiCap;
        }


        public List<QLNhanVienDTO> GetNVCapDuoiNoADIDWithADID(int BaCV) // thuộc cùng phòng ban và dưới chức vụ 
        {
            string query = "select * from QLYNHANVIEN ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<QLNhanVienDTO> LsNVduoiCap = new List<QLNhanVienDTO>();
            foreach (DataRow item in data.Rows)
            {
                QLNhanVienDTO nhanvienDTO = new QLNhanVienDTO(item);

                // Lỗi do Load phải nhân viên ko có tài khoản User

                int bacCV = ChucVuDAO.Instance.GetBacCV(nhanvienDTO.CHUCVU);
                bool CheckADID = QLyYCADIDDAO.Instance.CheckExistADID(nhanvienDTO.MANV);
                if (!CheckADID)
                {
                    if (bacCV > BaCV)
                    {
                        LsNVduoiCap.Add(nhanvienDTO);
                    }
                }
            }
            return LsNVduoiCap;
        }


        public List<QLNhanVienDTO> GetNVOfPBDuoiCVNoADID(string MaPB, int BaCV) // thuộc cùng phòng ban và dưới chức vụ 
        {
            string query = "select * from QLYNHANVIEN where PHONGBAN= @pb ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPB });
            List<QLNhanVienDTO> LsNVduoiCap = new List<QLNhanVienDTO>();
            foreach (DataRow item in data.Rows)
            {
                QLNhanVienDTO nhanvienDTO = new QLNhanVienDTO(item);

                // Lỗi do Load phải nhân viên ko có tài khoản User

                int bacCV = ChucVuDAO.Instance.GetBacCV(nhanvienDTO.CHUCVU);
                bool CheckADID = QLyYCADIDDAO.Instance.CheckExistADID(nhanvienDTO.MANV);
                if(!CheckADID)
                {
                    if (bacCV > BaCV)
                    {
                        LsNVduoiCap.Add(nhanvienDTO);
                    }
                }             
            }
            return LsNVduoiCap;
        }


        public List<QLNhanVienDTO> GetNVOfBPDuoiCVNoADID(string MaBoPhan)  // Cần phải sắp xếp theo thời gian.
        {
            List<QLNhanVienDTO> lsvNVOfBP = new List<QLNhanVienDTO>();
            List<PhongBanDTO> LsPbOfBP = PhongBanDAO.Instance.GetLsvPbThuocBP(MaBoPhan);
            foreach (PhongBanDTO item in LsPbOfBP)
            {
                string MaPB = item.MAPB;
                List<QLNhanVienDTO> lsvNVOfPB = GetNVOfPBDuoiCVNoADID(MaPB,6);
                foreach (QLNhanVienDTO item1 in lsvNVOfPB)
                {
                    lsvNVOfBP.Add(item1);
                }
            }
            return lsvNVOfBP;
        }



        public DataTable GetNVOfBP(string MaBP)
        {
            string query = "select * from QLYNHANVIEN where BOPHAN= @bp ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaBP});
            return data;
        }


        public QLNhanVienDTO GetNhanVienDTO(string MaNV, string Nhamay)
        {
            string query = "select* from QLYNHANVIEN where MANV= @ma and NHAMAY= @nhamay ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNV, Nhamay});

            foreach (DataRow item in data.Rows)
            {
                QLNhanVienDTO dto = new QLNhanVienDTO(item);
                return dto;
            }
            
            return null;
        }

        public bool CheckMaNVExist(string MaNV,string Nhamay)
        {
            string query = "select * from QLYNHANVIEN where MANV= @maNV and NHAMAY= @nhamay ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNV,Nhamay});
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

        public int Insert(string MaNV,string FullName,string NhaMay, string Bophan, string Phongban, string nhom, string chucvu)
        {
            string query = "insert QLYNHANVIEN(MANV, FULLNAME,NHAMAY,BOPHAN,PHONGBAN,NHOM,CHUCVU) " +
                                 "  values ( @manv , @fullname , @nhamay , @bophan , @phongban , @nhom , @chucvu  )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaNV, FullName,NhaMay, Bophan, Phongban, nhom, chucvu});
            return data;
        }

                
        // HAM XOA
        public int Delete(string maNV,string NhaMay)
        {
            string query = " DELETE QLYNHANVIEN WHERE MANV= @maNV and NHAMAY= @nhamay ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maNV,NhaMay});
            return data; 
        }

    }
}
