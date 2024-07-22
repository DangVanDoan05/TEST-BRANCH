using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class NgayCNDTO
    {
        public NgayCNDTO(DataRow row)
        {
            string NgayCNchuoi = row["NGAYCN"].ToString();
            string[] a = NgayCNchuoi.Split(' ');
            string Ngay = a[0];
            this.NGAYCN = Convert.ToDateTime(Ngay);
        }


        private DateTime nGAYCN;       
        public DateTime NGAYCN { get => nGAYCN; set => nGAYCN = value; }
    }
}
