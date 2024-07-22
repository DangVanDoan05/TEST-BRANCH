using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class KeHoachBDDTO
    {

        //  KEHOACHBD(MAKH, NHAMAY, PB, NGAY, THANG, NAM, HMBT, TRANGTHAI))

        public KeHoachBDDTO(DataRow row)
        {
            this.MAKH = row["MAKH"].ToString();
            this.NHAMAY = row["NHAMAY"].ToString();
            this.PB = row["PB"].ToString();
            this.NGAY = row["NGAY"].ToString();
            this.THANG = row["THANG"].ToString();
            this.NAM = row["NAM"].ToString();
            this.HMBT = row["HMBT"].ToString();
            this.TRANGTHAI = bool.Parse(row["TRANGTHAI"].ToString());          
        }


       

        private string mAKH;
        private string nHAMAY;
        private string pB;
        private string nGAY;
        private string tHANG;
        private string nAM;
        private string hMBT;
        private bool tRANGTHAI;

        public string NHAMAY { get => nHAMAY; set => nHAMAY = value; }
        public string PB { get => pB; set => pB = value; }
        public string NGAY { get => nGAY; set => nGAY = value; }
        public string HMBT { get => hMBT; set => hMBT = value; }
        public bool TRANGTHAI { get => tRANGTHAI; set => tRANGTHAI = value; }
        public string MAKH { get => mAKH; set => mAKH = value; }
        public string THANG { get => tHANG; set => tHANG = value; }
        public string NAM { get => nAM; set => nAM = value; }
    }
}
