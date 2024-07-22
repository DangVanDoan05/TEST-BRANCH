using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MaLinhKienDTO
    {

        public MaLinhKienDTO(DataRow row)
        {
           
            this.MALK = row["MALK"].ToString();
            this.TENLK = row["TENLK"].ToString();
            this.DVTINH = row["DVTINH"].ToString();
            this.NCC = row["NCC"].ToString();
            this.SLMIN = row["SLMIN"].ToString();
            this.SLMAX = row["SLMAX"].ToString();
            this.GHICHU = row["GHICHU"].ToString();

        }

     
        private string mALK;
        private string tENLK;
        private string dVTINH;
        private string nCC;
        private string sLMIN;
        private string sLMAX;
        private string gHICHU;

     
        public string MALK { get => mALK; set => mALK = value; }
        public string TENLK { get => tENLK; set => tENLK = value; }
        public string DVTINH { get => dVTINH; set => dVTINH = value; }
        public string NCC { get => nCC; set => nCC = value; }
        public string SLMIN { get => sLMIN; set => sLMIN = value; }
        public string SLMAX { get => sLMAX; set => sLMAX = value; }
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
    }
}
