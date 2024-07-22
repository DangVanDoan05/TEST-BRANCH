using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class ChucVuDTO
    {
        public ChucVuDTO(DataRow row)
        {
            this.MACHUCVU = row["MACHUCVU"].ToString();          
            this.TENCHUCVU = row["TENCHUCVU"].ToString();
            this.BACCV =int.Parse(row["BACCV"].ToString());
        }

        public ChucVuDTO(string MaCV, string TenCV,int BacCV)
        {
            this.MACHUCVU = MaCV;
            this.TENCHUCVU = TenCV;
            this.BACCV = BacCV;
        }

        private string mACHUCVU;

      

        private string tENCHUCVU;
        private int bACCV;

        public string MACHUCVU { get => mACHUCVU; set => mACHUCVU = value; }
      
        public string TENCHUCVU { get => tENCHUCVU; set => tENCHUCVU = value; }
        public int BACCV { get => bACCV; set => bACCV = value; }
    }
}
