using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class NgayTHDTO
    {
        public NgayTHDTO(DataRow row)
        {
            this.NGAYTH = Convert.ToDateTime(row["NGAYTH"].ToString());
        }

        private DateTime nGAYTH;

        public DateTime NGAYTH { get => nGAYTH; set => nGAYTH = value; }
    }
}
