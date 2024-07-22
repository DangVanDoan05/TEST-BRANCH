using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NgayDHDTO
    {
      
            public NgayDHDTO(DataRow row)
            {
                this.NGAYDH = Convert.ToDateTime(row["NGAYDH"].ToString());
            }

            private DateTime nGAYDH;

            public DateTime NGAYDH { get => nGAYDH; set => nGAYDH = value; }

    }
}
