using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class QLNhanVienDTO
    {

        // QLNHANVIEN(MANV, FULLNAME, BOPHAN, PHONGBAN, NHOM, CHUCVU)
        public QLNhanVienDTO(DataRow row)
        {
            this.MANV = row["MANV"].ToString();
            this.FULLNAME = row["FULLNAME"].ToString();
            this.NHAMAY = row["NHAMAY"].ToString();
            this.BOPHAN = row["BOPHAN"].ToString();
            this.PHONGBAN = row["PHONGBAN"].ToString();
            this.NHOM = row["NHOM"].ToString();
            this.CHUCVU = row["CHUCVU"].ToString();
        }

      //  QLYNHANVIEN(MANV, FULLNAME, NHAMAY, BOPHAN, PHONGBAN, NHOM, CHUCVU)

        private string mANV;
        private string fULLNAME;
        private string nHAMAY;
        private string bOPHAN;
        private string pHONGBAN;
        private string nHOM;
        private string cHUCVU;

        public string MANV { get => mANV; set => mANV = value; }           
        public string FULLNAME { get => fULLNAME; set => fULLNAME = value; }
        public string BOPHAN { get => bOPHAN; set => bOPHAN = value; }      
        public string NHOM { get => nHOM; set => nHOM = value; }
        public string CHUCVU { get => cHUCVU; set => cHUCVU = value; }
        public string PHONGBAN { get => pHONGBAN; set => pHONGBAN = value; }
        public string NHAMAY { get => nHAMAY; set => nHAMAY = value; }
    }
}
