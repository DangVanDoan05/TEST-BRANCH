using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NgayXuatDTO
    {
        public NgayXuatDTO(DataRow row)
        {
            this.NGAYXUAT = Convert.ToDateTime(row["NGAYXUAT"].ToString());
        }
        
        private DateTime nGAYXUAT;

        public DateTime NGAYXUAT { get => nGAYXUAT; set => nGAYXUAT = value; }
    }
}
