using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class ThongKeTHDTO
    {

        // THONGKETHUHOI(MATKTH,MALK,TENLK,NGAYTH,SLTH,DVTINH,NGUOITH,GHICHU,IDTTKIEMKE,CHITIETTTKK)
        public ThongKeTHDTO(DataRow row)
        {
            this.MATKTH = row["MATKTH"].ToString();
            this.MALK = row["MALK"].ToString();
            this.TENLK = row["TENLK"].ToString();
            this.NGAYTH = row["NGAYTH"].ToString();
            this.NHAMAY = row["NHAMAY"].ToString();
            this.PB = row["PB"].ToString();
            this.SLTH = int.Parse(row["SLTH"].ToString());
            this.DVTINH = row["DVTINH"].ToString();         
            this.NGUOITH = row["NGUOITH"].ToString();
            this.LKORTH = row["LKORTH"].ToString();
            this.IDTTKIEMKE = int.Parse(row["IDTTKIEMKE"].ToString());
            this.CHITIETTTKK = row["CHITIETTTKK"].ToString();
        }

       

        private string mATKTH;
        private string mALK;
        private string tENLK;
        private string nGAYTH;
        private string nHAMAY;
        private string pB;
        private int sLTH;
        private string dVTINH;     
        private string nGUOITH;
        private string lKORTH;
        private int iDTTKIEMKE;
        private string cHITIETTTKK;

        public string MATKTH { get => mATKTH; set => mATKTH = value; }
        public string MALK { get => mALK; set => mALK = value; }
        public string TENLK { get => tENLK; set => tENLK = value; }
        public string NGAYTH { get => nGAYTH; set => nGAYTH = value; }
        public int SLTH { get => sLTH; set => sLTH = value; }
        public string DVTINH { get => dVTINH; set => dVTINH = value; }
        public string NGUOITH { get => nGUOITH; set => nGUOITH = value; }
     
        public int IDTTKIEMKE { get => iDTTKIEMKE; set => iDTTKIEMKE = value; }
        public string CHITIETTTKK { get => cHITIETTTKK; set => cHITIETTTKK = value; }
        public string PB { get => pB; set => pB = value; }
        public string LKORTH { get => lKORTH; set => lKORTH = value; }
        public string NHAMAY { get => nHAMAY; set => nHAMAY = value; }
    }
}
