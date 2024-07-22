using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DanhSachPhanMemDTO
    {
        public DanhSachPhanMemDTO(DataRow row)
        {

            this.MAPM = row["MAPM"].ToString();
            this.TENPM = row["TENPM"].ToString();
            this.LICENSE = row["LICENSE"].ToString();
            this.NGAYMUA = row["NGAYMUA"].ToString();
            this.HANSD = row["HANSD"].ToString();
            this.NCC = row["NCC"].ToString();
            this.CHUCNANG = row["CHUCNANG"].ToString();
            this.GHICHU = row["GHICHU"].ToString();
        }
        public DanhSachPhanMemDTO(string maPM,string tenPM,string License, string ngaymua,string hansd,string ncc,string ghichu)
        {
            this.MAPM = maPM;
            this.TENPM = tenPM;
            this.LICENSE = License;
            this.NGAYMUA =ngaymua;
            this.HANSD = hansd;
            this.NCC =ncc;
            this.GHICHU = ghichu;
        }
        private string mAPM;
        private string tENPM;
        private string lICENSE;
        private string nGAYMUA;
        private string hANSD;
        private string nCC;
        private string cHUCNANG;
        private string gHICHU;

        public string MAPM { get => mAPM; set => mAPM = value; }
        public string TENPM { get => tENPM; set => tENPM = value; }
        public string LICENSE { get => lICENSE; set => lICENSE = value; }
        public string NGAYMUA { get => nGAYMUA; set => nGAYMUA = value; }
        public string HANSD { get => hANSD; set => hANSD = value; }
        public string NCC { get => nCC; set => nCC = value; }
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
        public string CHUCNANG { get => cHUCNANG; set => cHUCNANG = value; }
    }
}
