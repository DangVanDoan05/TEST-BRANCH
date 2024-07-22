using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NhomDTO
    {
       
        public NhomDTO(DataRow row)
        {
            this.MANHOM = row["MANHOM"].ToString();
            this.TENNHOM = row["TENNHOM"].ToString();
            this.GHICHU = row["GHICHU"].ToString();
            this.NHAMAY = row["NHAMAY"].ToString();
            this.BOPHAN = row["BOPHAN"].ToString();
            this.PHONGBAN= row["PHONGBAN"].ToString();
        }
        // QLNHOM(MANHOM,TENNHOM,GHICHU,NHAMAY,BOPHAN,PHONGBAN)
        private string mANHOM;
        private string tENNHOM;
        private string gHICHU;
        private string nHAMAY;
        private string bOPHAN;
        private string pHONGBAN;

        public string MANHOM { get => mANHOM; set => mANHOM = value; }
        public string TENNHOM { get => tENNHOM; set => tENNHOM = value; }
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
        public string PHONGBAN { get => pHONGBAN; set => pHONGBAN = value; }
        public string NHAMAY { get => nHAMAY; set => nHAMAY = value; }
        public string BOPHAN { get => bOPHAN; set => bOPHAN = value; }
    }
}
