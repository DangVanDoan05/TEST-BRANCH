using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
   public class QlyPhanQuyenDAO
    {
        private static QlyPhanQuyenDAO instance;

        public static QlyPhanQuyenDAO Instance
        {
            get { if (instance == null) instance = new QlyPhanQuyenDAO(); return QlyPhanQuyenDAO.instance; }
            private set { QlyPhanQuyenDAO.instance = value; }
        }
        public DataTable GetTable()
        {
            string query = "select* from QUANLYPHANQUYEN";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public List<QlyPhanQuyenDTO> GetLsTTPQuser(string maNV)
        {
            string query = "select* from QUANLYPHANQUYEN where MANHANVIEN= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maNV });

            List<QlyPhanQuyenDTO> lsv = new List<QlyPhanQuyenDTO>();
            foreach (DataRow item in data.Rows)
            {
                QlyPhanQuyenDTO phongBanDTO = new QlyPhanQuyenDTO(item);
                lsv.Add(phongBanDTO);
            }
            return lsv;

        }
        public int GetIDquyenPDPB(string MaNV)
        {
            string query = "select* from QUANLYPHANQUYEN where ID= 'btnQlyYCKT' AND MANHANVIEN= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNV });          
            QlyPhanQuyenDTO phanquyenDTO = new QlyPhanQuyenDTO(data.Rows[0]);
            int IDquyenPDPB = phanquyenDTO.IDQUYEN;
            return IDquyenPDPB;
        }

        public int GetIDquyenPDYcADID(string MaNV)
        {
            string query = " select * from QUANLYPHANQUYEN where ID= 'btnYcADID' AND MANHANVIEN= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNV });
            QlyPhanQuyenDTO phanquyenDTO = new QlyPhanQuyenDTO(data.Rows[0]);
            int IDquyenPDPB = phanquyenDTO.IDQUYEN;
            return IDquyenPDPB;
        }

        public int GetIDquyenPDLQPB(string MaNV)
        {
            string query = "select* from QUANLYPHANQUYEN where ID= 'btnQlyYCKTlienquanPB' AND MANHANVIEN= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNV });
            QlyPhanQuyenDTO phanquyenDTO = new QlyPhanQuyenDTO(data.Rows[0]);
            int IDquyenPDPB = phanquyenDTO.IDQUYEN;
            return IDquyenPDPB;
        }

        // Lấy ra những mã nhân viên toàn quyền thực hiện YCKT
        public List<UserDTO> GetMaNVTHYCKT(string MaQlyLogon)
        {
            string query = "select* from QUANLYPHANQUYEN where ID= 'btnThuchienYCKT_IT' AND IDQUYEN = 5";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<QlyPhanQuyenDTO> lsv = new List<QlyPhanQuyenDTO>();
            foreach (DataRow item in data.Rows)
            {
                QlyPhanQuyenDTO phongBanDTO = new QlyPhanQuyenDTO(item);
                lsv.Add(phongBanDTO);
            }

            List<string> lsUser = new List<string>();
            foreach (QlyPhanQuyenDTO item in lsv)
            {
                string MaNV = item.MANHANVIEN;
                lsUser.Add(MaNV);
            }
            // Duyệt List User ----> để lấy List User DTO
            List<UserDTO> ls1 = new List<UserDTO>();
            foreach (string item in lsUser)
            {
                UserDTO user = UserDAO.Instance.GetUserDTO1(item);
                ls1.Add(user);
            }
            return ls1;
        }

        // *** Lấy ra quản lý cao cấp 
        public List<UserDTO> GetQLCCPB(string MaPB)
        {
            string query = "select* from QUANLYPHANQUYEN where ID= N'btnQlyYCKT' AND IDQUYEN = 6 ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            List<string> lsMaNVQly = new List<string>();

            foreach (DataRow item in data.Rows)
            {
                QlyPhanQuyenDTO phanQuyenDTO = new QlyPhanQuyenDTO(item);
                lsMaNVQly.Add(phanQuyenDTO.MANHANVIEN);
            }

            List<UserDTO> lsQlyTCPBDTO = new List<UserDTO>();
            foreach (string item in lsMaNVQly)
            {
                UserDTO user = UserDAO.Instance.GetUserDTO1(item);
                if (user.PHONGBAN == MaPB)
                {
                    lsQlyTCPBDTO.Add(user);
                }
            }
            return lsQlyTCPBDTO;
        }

        // *** Lấy ra quản lý trung cấp của một phòng ban.

        public List<UserDTO> GetQLTCPB(string MaPB)
        {
            string query = "select* from QUANLYPHANQUYEN where ID= N'btnQlyYCKT' AND IDQUYEN = 5 ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<string> lsMaNVQly = new List<string>();
            foreach (DataRow item in data.Rows)
            {
                QlyPhanQuyenDTO phanQuyenDTO = new QlyPhanQuyenDTO(item);
                lsMaNVQly.Add(phanQuyenDTO.MANHANVIEN);
            }
            List<UserDTO> lsQlyTCPBDTO = new List<UserDTO>();
            foreach (string item in lsMaNVQly)
            {
                UserDTO user = UserDAO.Instance.GetUserDTO1(item);
                if (user.PHONGBAN == MaPB)
                {
                    lsQlyTCPBDTO.Add(user);
                }
            }
            return lsQlyTCPBDTO;
        }

        // *** Lấy ra quản lý sơ cấp của một phòng ban.
        public List<UserDTO> GetQLSCPB(string MaPB)
        {
            string query = "select* from QUANLYPHANQUYEN where ID= N'btnQlyYCKT' AND IDQUYEN = 4 ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<string> lsMaNVQly = new List<string>();
            foreach (DataRow item in data.Rows)
            {
                QlyPhanQuyenDTO phanQuyenDTO = new QlyPhanQuyenDTO(item);
                lsMaNVQly.Add(phanQuyenDTO.MANHANVIEN);
            }
            List<UserDTO> lsQlyTCPBDTO = new List<UserDTO>();
            foreach (string item in lsMaNVQly)
            {
                UserDTO user = UserDAO.Instance.GetUserDTO1(item);
                if (user.PHONGBAN == MaPB)
                {
                    lsQlyTCPBDTO.Add(user);
                }
            }
            return lsQlyTCPBDTO;
        }

        public bool CheckQLSC(string MaNV)
        {
            string query = "select* from QUANLYPHANQUYEN where ID= N'btnQlyYCKT' AND IDQUYEN = 4 and MANHANVIEN= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNV });
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
        public bool CheckQLTC(string MaNV)
        {
            string query = "select* from QUANLYPHANQUYEN where ID= N'btnQlyYCKT' AND IDQUYEN = 5 and MANHANVIEN= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNV });
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
        public bool CheckQLCC(string MaNV)
        {
            string query = "select* from QUANLYPHANQUYEN where ID= N'btnQlyYCKT' AND IDQUYEN = 6 and MANHANVIEN= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { MaNV });
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
        // *** Lấy ra List mã nhân viên có quyền phê duyệt trong phòng ban
        public List<UserDTO>GetLsMaNVPDPB(string MaPB)
        {
            string query = "select* from QUANLYPHANQUYEN where ID= 'btnPDYCKTPB' AND IDQUYEN >=4";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<QlyPhanQuyenDTO> lsv = new List<QlyPhanQuyenDTO>();
            foreach (DataRow item in data.Rows)
            {
                QlyPhanQuyenDTO phongBanDTO = new QlyPhanQuyenDTO(item);
                lsv.Add(phongBanDTO);
            }

            List<string> lsUser = new List<string>();
            foreach (QlyPhanQuyenDTO item in lsv)
            {
                string MaNV = item.MANHANVIEN;
                lsUser.Add(MaNV);
            }
            // Duyệt List User ----> để lấy List User DTO

            // *** Lấy List những người có quyền phê duyệt
            List<UserDTO> ls1 = new List<UserDTO>();
            foreach (string item in lsUser)
            {
                UserDTO user = UserDAO.Instance.GetUserDTO1(item);
                if(user.PHONGBAN==MaPB)
                {
                    ls1.Add(user);
                }              
            }
           
            return ls1;
        }

        public QlyPhanQuyenDTO Getitemuser(string tenut,string MaNV) // Thiếu biến User nhập vào
        {
            string query = "select* from QUANLYPHANQUYEN where ID= @1 and MANHANVIEN= @ma ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tenut,MaNV });

            
            foreach (DataRow item in data.Rows)
            {
                QlyPhanQuyenDTO quyenDTO = new QlyPhanQuyenDTO(item);
                return quyenDTO;
            }
            return null;

        }

        public int Insert(string MaNV, string id,string idparent,string mota, int idquyen, string chitietquyen)
        {
            string query = "insert  QUANLYPHANQUYEN(MANHANVIEN,ID,IDPARENT,MOTA,IDQUYEN,CHITIETQUYEN) values( @maNV , @ID , @IDcha , @mota , @idquyen , @chitietquyen )";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] {MaNV, id,idparent,mota,idquyen,chitietquyen});
            return data;
        }
     
        public int UpdateUserRight(string MaNV, string id, int idquyen,string chitietquyen)
        {
            string query = "UPDATE	QUANLYPHANQUYEN SET IDQUYEN= @quyen ,CHITIETQUYEN= @chitietquyen WHERE MANHANVIEN= @maNV and ID= @ID ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { idquyen,chitietquyen, MaNV,id });
            return data;

        }
       
        public int Delete(string MaNV)
        {
            string query = "DELETE QUANLYPHANQUYEN WHERE MANHANVIEN= @manv ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { MaNV });
            return data;
        }


        public int DeleteChucNang(string IDchucnang)
        {
            string query = "DELETE QUANLYPHANQUYEN WHERE ID= @id ";
            int data = DataProvider.Instance.ExecuteNonQuery(query, new object[] { IDchucnang });
            return data;
        }

    }
}
