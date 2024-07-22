using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class TinhTrangKiemKeDTO
    {
        public TinhTrangKiemKeDTO(DataRow row)
        {
            this.IDTTKIEMKE = int.Parse(row["IDTTKIEMKE"].ToString());
            this.CHITIETTTKK = row["CHITIETTTKK"].ToString();
        }

       //INHTRANGKIEMKE(IDTTKIEMKE, CHITIETTTKK)

        private int iDTTKIEMKE;
        private string cHITIETTTKK;

        public int IDTTKIEMKE { get => iDTTKIEMKE; set => iDTTKIEMKE = value; }
        public string CHITIETTTKK { get => cHITIETTTKK; set => cHITIETTTKK = value; }
    }
}
