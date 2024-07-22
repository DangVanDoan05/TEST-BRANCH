using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class QLyVTSDDTO
    {
        public QLyVTSDDTO(DataRow row)
        {
            this.MABT = row["MABT"].ToString();
            this.MAVTSD = row["MAVTSD"].ToString();
            this.TENVTSD = row["TENVTSD"].ToString();
            this.SLVTSD = int.Parse(row["SLVTSD"].ToString());          
            this.IDTTKIEMKE = int.Parse(row["IDTTKIEMKE"].ToString());
            this.CHITIETTTKK = row["CHITIETTTKK"].ToString();
        }

        //  QLYVATTUSD(MABT,MAVTSD,TENVTSD,SLVTSD,IDTTKIEMKE,CHITIETTTKK)

        private string mABT;
        private string mAVTSD;
        private string tENVTSD;
        private int sLVTSD;
        private int iDTTKIEMKE;
        private string cHITIETTTKK;

        public string MABT { get => mABT; set => mABT = value; }
        public string MAVTSD { get => mAVTSD; set => mAVTSD = value; }
        public string TENVTSD { get => tENVTSD; set => tENVTSD = value; }
        public int SLVTSD { get => sLVTSD; set => sLVTSD = value; }
        public int IDTTKIEMKE { get => iDTTKIEMKE; set => iDTTKIEMKE = value; }
        public string CHITIETTTKK { get => cHITIETTTKK; set => cHITIETTTKK = value; }


    }
}
