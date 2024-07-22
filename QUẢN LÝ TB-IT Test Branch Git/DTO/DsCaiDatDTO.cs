using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class DsCaiDatDTO
    {
        public DsCaiDatDTO(DataRow row)
        {
            this.MAMT = row["MAMT"].ToString();
            this.PB = row["PB"].ToString();
            this.NHAMAY = row["NHAMAY"].ToString();
            this.MAPM = row["MAPM"].ToString();
            this.TENPM = row["TENPM"].ToString();
            this.NGAYCD = row["NGAYCD"].ToString();
            this.NGAYHT = row["NGAYHT"].ToString();
        }

        private string mAMT;
        private string pB;
        private string nHAMAY;
        private string mAPM;
        private string tENPM;
        private string nGAYCD;
        private string nGAYHT;

        public string PB { get => pB; set => pB = value; }
        public string NHAMAY { get => nHAMAY; set => nHAMAY = value; }
        public string MAMT { get => mAMT; set => mAMT = value; }
        public string MAPM { get => mAPM; set => mAPM = value; }
        public string TENPM { get => tENPM; set => tENPM = value; }
        public string NGAYCD { get => nGAYCD; set => nGAYCD = value; }
        public string NGAYHT { get => nGAYHT; set => nGAYHT = value; }
    }
}
