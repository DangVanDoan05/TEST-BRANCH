using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BoPhanDTO
    {
        public BoPhanDTO(DataRow row)
        {
            this.MABP = row["MABP"].ToString();
            this.TENBP = row["TENBP"].ToString();
            this.GHICHU = row["GHICHU"].ToString();
            this.NHAMAY = row["NHAMAY"].ToString();
        }

        //  BOPHAN(MABP, TENBP, GHICHU, NHAMAY)

        private string mABP;
        private string tENBP;
        private string gHICHU;
        private string nHAMAY;


       
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
        public string MABP { get => mABP; set => mABP = value; }
        public string TENBP { get => tENBP; set => tENBP = value; }
        public string NHAMAY { get => nHAMAY; set => nHAMAY = value; }
    }
}
