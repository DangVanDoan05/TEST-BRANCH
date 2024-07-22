using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
   public class UserDAO
    {
        private static UserDAO instance;

        public static UserDAO Instance
        {
            get { if (instance == null) instance = new UserDAO(); return UserDAO.instance; }
            private set { UserDAO.instance = value; }
        }
        private UserDAO() { }


        // HAM LAY BANG
        public DataTable GetTable()
        {
            string query = "select * from QLUSER";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

      

      

        // Hàm đệ quy lấy quản lý sơ cấp của nhân viên
    
        //public string GetQLCCNgLapYC(string MaNgLapYC)
        //{
        //    // Check quản lý trung cấp phải check ở trong bảng chức vụ
        //    if (ChucVuDAO.Instance.CheckQLCC(MaNgLapYC))
        //    {
        //        return MaNgLapYC;
        //    }
        //    else
        //    {
        //        // Nếu không thì kiểm tra đến mã quản lý trực tiếp
        //        UserDTO userDTO = GetUserDTO1(MaNgLapYC);
        //        string maQLTT = userDTO.MAQLTT;
        //        return GetQLTCNgLapYC(maQLTT);
        //    }
        //}

        public UserDTO GetQLTCPB(string MaPB)
        {
            string query = "select * from QLUSER where PHONGBAN= @pb and CHUCVU= N'QLTC' ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPB });          
            UserDTO userDTO = new UserDTO(data.Rows[0]);                      
            return userDTO;
        }
        public UserDTO GetQLSCPB(string MaPB)
        {
            string query = "select * from QLUSER where PHONGBAN= @pb and CHUCVU= N'QLSC' ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPB });
            UserDTO userDTO = new UserDTO(data.Rows[0]);
            return userDTO;
        }
        public DataTable GetUserOfPB(string MaPB)
        {
            string query = "select * from QLUSER where PHONGBAN= @pb ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPB });         
            return data;
        }
        public DataTable GetUserOfBP(string MaBoPhan)
        {
            string query = "select * from QLUSER where BOPHAN= @BoPhan ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaBoPhan });
            return data;
        }

        // **** Lấy ra toàn bộ danh sách cấp dưới của nhân viên

        public List<UserDTO> GetLsvUser()
        {
            string query = "select * from QLUSER ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<UserDTO> lsv = new List<UserDTO>();
            foreach (DataRow item in data.Rows)
            {
                UserDTO userDTO = new UserDTO(item);
                lsv.Add(userDTO);
            }
            return lsv ;
        }

        public List<UserDTO> GetLsvUserTreeList()
        {
            string query = "select * from QLUSER where PHONGBAN != N'ADMIN' ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<UserDTO> lsv = new List<UserDTO>();
            foreach (DataRow item in data.Rows)
            {
                UserDTO userDTO = new UserDTO(item);
                lsv.Add(userDTO);
            }
            return lsv;
        }

        public List<UserDTO> GetLsvUserNoQLTT()
        {          
            string query = "select * from QLUSER where MAQLTT = N'' and PHONGBAN != N'ADMIN' ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);         
            List<UserDTO> lsv = new List<UserDTO>();
            foreach (DataRow item in data.Rows)
            {
                UserDTO userDTO = new UserDTO(item);
                lsv.Add(userDTO);
            }
            return lsv;
        }



        public List<UserDTO> GetLsvUserNoQLTTOfBP(string MaBP)
        {
            string query = "select * from QLUSER where MAQLTT = N'' and BOPHAN= @BoPhan ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaBP });  
            List<UserDTO> lsv = new List<UserDTO>();    
            foreach (DataRow item in data.Rows) 
            {
                UserDTO userDTO = new UserDTO(item);    
                lsv.Add(userDTO); 
            }
            return lsv; 
        }

        public List<UserDTO> GetLsvUserNoQLTTOfPB(string MaPB)
        {
            string query = "select * from QLUSER where MAQLTT = N'' and PHONGBAN= @pb ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaPB });
            List<UserDTO> lsv = new List<UserDTO>();
            foreach (DataRow item in data.Rows)
            {
                UserDTO userDTO = new UserDTO(item);
                lsv.Add(userDTO);
            }
            return lsv;
        }

        public List<UserDTO> GetListNVIT()
        {
            string query = " select * from QLUSER where PHONGBAN = 'IT'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<UserDTO> lsv = new List<UserDTO>();
            foreach (DataRow item in data.Rows)
            {
                UserDTO userDTO = new UserDTO(item);
                lsv.Add(userDTO);
            }
            return lsv;
        }

        public List<UserDTO> GetUser(string tk)
        {
            string query = "select* from QLUSER where TAIKHOAN= @tk ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {tk});
            List<UserDTO> lsv = new List<UserDTO>();
            foreach (DataRow item in data.Rows)
            {
                UserDTO userDTO = new UserDTO(item);
                lsv.Add(userDTO);
            }
            return lsv;
        }

        public List<UserDTO> GetLsOneUser(string MaNV)
        {
            string query = "select* from QLUSER where MANV= @ma  ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNV });
            List<UserDTO> lsv = new List<UserDTO>();
            foreach (DataRow item in data.Rows)
            {
                UserDTO userDTO = new UserDTO(item);
                lsv.Add(userDTO);
            }
            return lsv;
        }

        public UserDTO GetUserDTO(string tk)
        {
            string query = "select* from QLUSER where TAIKHOAN= @tk ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {tk});
            UserDTO lsv = new UserDTO(data.Rows[0]);
            return lsv;
        }

        public UserDTO GetUserDTO1(string maNV)
        {
            string query = "select* from QLUSER where MANV= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maNV });
            UserDTO lsv = new UserDTO(data.Rows[0]);
            return lsv;
        }

        public DataTable GetRowAccount(string taikhoan)
        {
            string query = "select * from QLUSER where TAIKHOAN= @tk ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { taikhoan });
            return data;
        }

        public bool CheckUserExist(string MaNV)
        {
            string query = " select * from QLUSER where MANV= @manv ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNV });
            int socot= data.Rows.Count;
            if(socot>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckMaNVExist(string MaNV)
        {
            string query = "select * from QLUSER where MANV= @maNV ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {MaNV});
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


        // HAM KIEM TRA TAI KHOAN, MAT KHAU:

        public int CheckTaiKhoanExist(string taikhoan)
        {
            string query = "select * from QLUSER where TAIKHOAN= @tk ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { taikhoan });
            return data.Rows.Count;
        }

        // HAM THEM

        public int Insert(string MaNV,string HoTen,string BoPhan,string PhongBan,string Nhom,string ChucVu,string TaiKhoan, string MatKhau,string maQLTT)
        {
            string query =" insert QLUSER(MANV,FULLNAME,BOPHAN,PHONGBAN,NHOM,CHUCVU,TAIKHOAN,MATKHAU,MAQLTT) " +
                           "  values ( @manv , @fullname , @bophan , @phongban , @nhom , @chucvu , @taikhoan , @matkhau , @maqltt )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaNV, HoTen,  BoPhan, PhongBan,  Nhom,  ChucVu, TaiKhoan,  MatKhau, maQLTT });
            return data;
        }


        // HÀM SỬA MẬT KHẨU
      
        public int UpdateMKUSER(string maNV, string MatKhau)
        {
            string query = "update QLUSER set MATKHAU= @mk  where MANV= @maNV ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MatKhau, maNV });
            return data;
        }

        public int UpdateQLTT(string maNV, string maQLTT)
        {
            string query = "update QLUSER set MAQLTT= @maqltt  where MANV= @maNV ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maQLTT, maNV });
            return data;
        }

        // HAM XOA
        public int Delete(string maNV)
        {
            string query = " DELETE QLUSER WHERE MANV= @maNV ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maNV });
            return data;
        }

    }
}
