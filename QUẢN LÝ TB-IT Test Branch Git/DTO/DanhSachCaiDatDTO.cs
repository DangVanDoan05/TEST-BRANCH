using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DanhSachCaiDatDTO
    {
        public DanhSachCaiDatDTO(DataRow row)
        {
            this.MAMT = row["MAMT"].ToString();
            this.MAPM = row["MAPM"].ToString();
            this.TENPM = row["TENPM"].ToString();
            this.NGAYCD = row["NGAYCD"].ToString();

        }




        private string mAMT;
        private string mAPM;
        private string tENPM;
        private string nGAYCD;


        public string MAPM { get => mAPM; set => mAPM = value; }
        public string TENPM { get => tENPM; set => tENPM = value; }
    
        public string MAMT { get => mAMT; set => mAMT = value; }
        public string NGAYCD { get => nGAYCD; set => nGAYCD = value; }
    }
}
