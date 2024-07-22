using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhongBanDTO
    {
        // PHONGBAN(MAPB, TENPB, GHICHU, NHAMAY, BOPHAN)
        public PhongBanDTO(DataRow row)
        {        
            this.MAPB = row["MAPB"].ToString();
            this.TENPB = row["TENPB"].ToString();
            this.BOPHAN = row["BOPHAN"].ToString();
            this.GHICHU = row["GHICHU"].ToString();
            this.NHAMAY = row["NHAMAY"].ToString();
        }

      

        private string mAPB;
        private string tENPB;
        private string nHAMAY;
        private string gHICHU;
        private string bOPHAN;

        public string MAPB { get => mAPB; set => mAPB = value; }
        public string TENPB { get => tENPB; set => tENPB = value; }
        public string BOPHAN { get => bOPHAN; set => bOPHAN = value; }
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
        public string NHAMAY { get => nHAMAY; set => nHAMAY = value; }
    }
}
