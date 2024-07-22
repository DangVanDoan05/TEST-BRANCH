using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class QlyDonHangPBDTO
    {

        //   insert QLYDONHANGPB(MADONHANG, PHONGBAN, NGAYDH, TENHANG, SLDAT, DONVI, MDSD, NGAYNHAN, SLNHAN, GHICHU)

        public QlyDonHangPBDTO(DataRow row)
        {
            this.MADONHANG = row["MADONHANG"].ToString();
            this.PHONGBAN = row["PHONGBAN"].ToString();
            this.NGAYDH =  row["NGAYDH"].ToString();
            this.TENHANG = row["TENHANG"].ToString();
            this.SLDAT = int.Parse(row["SLDAT"].ToString());
            this.DONVI = row["DONVI"].ToString();
            this.NHAMAY = row["NHAMAY"].ToString();
            this.MDSD = row["MDSD"].ToString();
            this.NGAYNHAN = row["NGAYNHAN"].ToString();
            this.SLNHAN =int.Parse( row["SLNHAN"].ToString());                                
            this.GHICHU = row["GHICHU"].ToString();
        }
  
        private string mADONHANG;
        private string pHONGBAN;
        private string nGAYDH;
        private string tENHANG;
        private int sLDAT;
        private string dONVI;
        private string nHAMAY;
        private string mDSD;
        private string nGAYNHAN;      
        private int sLNHAN;             
        private string gHICHU;


        public string MADONHANG { get => mADONHANG; set => mADONHANG = value; }
        public string PHONGBAN { get => pHONGBAN; set => pHONGBAN = value; }
        public string NGAYDH { get => nGAYDH; set => nGAYDH = value; }
        public string TENHANG { get => tENHANG; set => tENHANG = value; }
        public int SLDAT { get => sLDAT; set => sLDAT = value; }
        public string DONVI { get => dONVI; set => dONVI = value; }     
        public string NGAYNHAN { get => nGAYNHAN; set => nGAYNHAN = value; }
        public int SLNHAN { get => sLNHAN; set => sLNHAN = value; }
    
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
        public string MDSD { get => mDSD; set => mDSD = value; }
        public string NHAMAY { get => nHAMAY; set => nHAMAY = value; }

    }
}
