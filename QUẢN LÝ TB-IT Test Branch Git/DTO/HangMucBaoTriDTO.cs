using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HangMucBaoTriDTO
    {
        public HangMucBaoTriDTO(DataRow row)
        {

            this.MAHM = row["MAHM"].ToString();
            this.CHITIET = row["CHITIET"].ToString();           
            this.GHICHU = row["GHICHU"].ToString();

        }


        private string mAHM;
        private string cHITIET;      
        private string gHICHU;


      
       
        public string MAHM { get => mAHM; set => mAHM = value; }
        public string CHITIET { get => cHITIET; set => cHITIET = value; }
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
    }
}
