using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserDTO
    {

        public UserDTO()
        {
            this.MANV = "";
            this.FULLNAME = "";
            this.BOPHAN = "";
            this.PHONGBAN = "";
            this.NHOM = "";
            this.CHUCVU = "";
            this.TAIKHOAN = "";
            this.MATKHAU ="";
            this.MAQLTT = "";
        }

        public UserDTO(DataRow row)
        {
            this.MANV = row["MANV"].ToString();
            this.FULLNAME = row["FULLNAME"].ToString();
            this.BOPHAN = row["BOPHAN"].ToString();
            this.PHONGBAN = row["PHONGBAN"].ToString();
            this.NHOM = row["NHOM"].ToString();
            this.CHUCVU = row["CHUCVU"].ToString();
            this.TAIKHOAN = row["TAIKHOAN"].ToString();
            this.MATKHAU = row["MATKHAU"].ToString();                                
            this.MAQLTT= row["MAQLTT"].ToString();        
        }

        // insert QLUSER(MANV, FULLNAME, BOPHAN, PHONGBAN, NHOM, CHUCVU, TAIKHOAN, MATKHAU, MAQLTT)

        private string mANV;
        private string fULLNAME;
        private string bOPHAN;
        private string pHONGBAN;
        private string nHOM;
        private string cHUCVU;
        private string tAIKHOAN;
        private string mATKHAU;                            
        private string mAQLTT;
      

        public string MANV { get => mANV; set => mANV = value; }
        public string TAIKHOAN { get => tAIKHOAN; set => tAIKHOAN = value; }
        public string MATKHAU { get => mATKHAU; set => mATKHAU = value; }
     
        public string PHONGBAN { get => pHONGBAN; set => pHONGBAN = value; }
        public string MAQLTT { get => mAQLTT; set => mAQLTT = value; }
        public string FULLNAME { get => fULLNAME; set => fULLNAME = value; }
        public string BOPHAN { get => bOPHAN; set => bOPHAN = value; }
        public string NHOM { get => nHOM; set => nHOM = value; }
        public string CHUCVU { get => cHUCVU; set => cHUCVU = value; }
    }
}
