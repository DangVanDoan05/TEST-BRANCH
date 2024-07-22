using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LichSuBTDTO
    {

        // LICHSUBAOTRI(MABT,MAMT,NHAMAY,PB,NGAY,HMBT,VTSD,VTTH,IDTTKK,CHITIETTTKK,GHICHU,NGUOICN)

        public LichSuBTDTO(DataRow row)
        {
            this.MABT = row["MABT"].ToString();
            this.MAMT = row["MAMT"].ToString();
            this.NHAMAY = row["NHAMAY"].ToString();
            this.PB = row["PB"].ToString();
            this.NGAY = row["NGAY"].ToString();
            this.HMBT = row["HMBT"].ToString();
            this.VTSD = row["VTSD"].ToString();
            this.VTTH = row["VTTH"].ToString();
            this.IDTTKK =int.Parse( row["IDTTKIEMKE"].ToString());
            this.CHITIETTTKK = row["CHITIETTTKK"].ToString();
            this.GHICHU = row["GHICHU"].ToString();
            this.NGUOICN = row["NGUOICN"].ToString();
        }

        // LICHSUBAOTRI(MABT,MAMT,NHAMAY,PB,NGAY,HMBT,VTSD,VTTH,IDTTKK,CHITIETTTKK,GHICHU,NGUOICN)

        private string mABT;
        private string mAMT;
        private string nHAMAY;
        private string pB;
        private string nGAY;
        private string hMBT;
        private string vTSD;
        private string vTTH;
        private int iDTTKK;
        private string cHITIETTTKK;
        private string gHICHU;
        private string nGUOICN;

        public string MABT { get => mABT; set => mABT = value; }
        public string MAMT { get => mAMT; set => mAMT = value; }
        public string NHAMAY { get => nHAMAY; set => nHAMAY = value; }
        public string PB { get => pB; set => pB = value; }
        public string NGAY { get => nGAY; set => nGAY = value; }
        public string HMBT { get => hMBT; set => hMBT = value; }
        public string VTSD { get => vTSD; set => vTSD = value; }         
        public string NGUOICN { get => nGUOICN; set => nGUOICN = value; }     
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
        public string VTTH { get => vTTH; set => vTTH = value; }
        public int IDTTKK { get => iDTTKK; set => iDTTKK = value; }
        public string CHITIETTTKK { get => cHITIETTTKK; set => cHITIETTTKK = value; }
    }
}
