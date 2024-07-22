using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NHAMAYDTO
    {
      //  NHAMAY(MANHAMAY, TENNHAMAY, DIACHI)
        
        public NHAMAYDTO(DataRow row)
        {
            this.MANHAMAY = row["MANHAMAY"].ToString();
            this.TENNHAMAY= row["TENNHAMAY"].ToString();
            this.DIACHI = row["DIACHI"].ToString();
        }

        private string mANHAMAY;
        private string tENNHAMAY;
        private string dIACHI;

        public string MANHAMAY { get => mANHAMAY; set => mANHAMAY = value; }
        public string TENNHAMAY { get => tENNHAMAY; set => tENNHAMAY = value; }
        public string DIACHI { get => dIACHI; set => dIACHI = value; }
    }
}
