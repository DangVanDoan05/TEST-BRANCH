using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class ThongKeVtuTieuHaoDTO
    {
        public ThongKeVtuTieuHaoDTO(DataRow row)
        {         
            this.MAVATTUSD = row["MAVATTUSD"].ToString();
            this.TENVATTUSD = row["TENVATTUSD"].ToString();
            this.SOLUONG = int.Parse(row["SOLUONG"].ToString());
            this.DONVI = row["DONVI"].ToString();
        }
        public ThongKeVtuTieuHaoDTO(string mavattu,string tenvattu,int soluong,string donvi)
        {
            this.MAVATTUSD = mavattu;
            this.TENVATTUSD = tenvattu;
            this.SOLUONG = soluong;
            this.DONVI = donvi;
        }

        private string mAVATTUSD;
        private string tENVATTUSD;
        private int sOLUONG;
        private string dONVI;


     
        public string MAVATTUSD { get => mAVATTUSD; set => mAVATTUSD = value; }
        public string TENVATTUSD { get => tENVATTUSD; set => tENVATTUSD = value; }
        public int SOLUONG { get => sOLUONG; set => sOLUONG = value; }
        public string DONVI { get => dONVI; set => dONVI = value; }
    }
}
