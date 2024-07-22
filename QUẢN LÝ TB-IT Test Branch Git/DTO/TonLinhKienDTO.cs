using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TonLinhKienDTO
    {
      
        public TonLinhKienDTO(DataRow row)
        {          
            this.MALK = row["MALK"].ToString();
            this.TENLK = row["TENLK"].ToString();
            this.SLTON = int.Parse(row["SLTON"].ToString());
            this.DVTINH = row["DVTINH"].ToString();                         
            this.IDTTKIEMKE = int.Parse(row["IDTTKIEMKE"].ToString());
            this.CHITIETTTKK = row["CHITIETTTKK"].ToString();
        }

    
        private string mALK;
        private string tENLK;
        private int sLTON;
        private string dVTINH;         
        private int iDTTKIEMKE;
        private string cHITIETTTKK;


        public string MALK { get => mALK; set => mALK = value; }
        public string TENLK { get => tENLK; set => tENLK = value; }
        public int SLTON { get => sLTON; set => sLTON = value; }
        public string DVTINH { get => dVTINH; set => dVTINH = value; }     
        public int IDTTKIEMKE { get => iDTTKIEMKE; set => iDTTKIEMKE = value; }
        public string CHITIETTTKK { get => cHITIETTTKK; set => cHITIETTTKK = value; }
    }
}
