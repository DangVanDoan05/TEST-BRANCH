using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class QuanLyMayTinhDTO
    {
         // QLYMAYTINH(MAMT, IP, MAC,DOMAIN, LOAIMT, NCC, NHAMAY, PB, NGUOISD, MATSCD, NGAYMUA, HANBH, BAOHANH, GHICHU)

        public QuanLyMayTinhDTO(DataRow row)  //14 cột
        {          
            this.MAMT = row["MAMT"].ToString();
            this.BAOHANH = (bool)row["BAOHANH"];
            this.IP = row["IP"].ToString();
            this.MAC = row["MAC"].ToString();
            this.DOMAIN = row["DOMAIN"].ToString();
            this.LOAIMT = row["LOAIMT"].ToString();
            this.NCC = row["NCC"].ToString();
            this.NHAMAY = row["NHAMAY"].ToString();
            this.PB = row["PB"].ToString();
            this.NGUOISD = row["NGUOISD"].ToString();
            this.MATSCD = row["MATSCD"].ToString();
            this.NGAYMUA = row["NGAYMUA"].ToString();
            this.HANBH = row["HANBH"].ToString();
            this.GHICHU = row["GHICHU"].ToString();          
        }

        public QuanLyMayTinhDTO(string maMT, bool baohanh, string ip, string mac,string Domain,string LoaiMT, string NCC, string Nhamay, string phongban, string nguoisd, string mtscd, string ngaymua, string hanbh, string ghichu)
        {
            this.MAMT = maMT;
            this.BAOHANH = baohanh;
            this.IP = ip;
            this.MAC = mac;
            this.DOMAIN = Domain;           
            this.LOAIMT = LoaiMT;
            this.NCC = NCC;
            this.NHAMAY = Nhamay;
            this.PB= phongban;
            this.NGUOISD = nguoisd;
            this.MATSCD = mtscd;
            this.NGAYMUA = ngaymua;
            this.HANBH = hanbh;
            this.GHICHU = ghichu;
        }



        public QuanLyMayTinhDTO(DataRow row, int a = 0)
        {

            this.MAMT = row["MAMT"].ToString();
            this.BAOHANH = true;
            this.IP = row["IP"].ToString();
            this.MAC = row["MAC"].ToString();
            this.LOAIMT = row["LOAIMT"].ToString();
            this.NCC = row["NCC"].ToString();
            this.PB = row["PHONGBAN"].ToString();
            this.NGUOISD = row["NGUOISD"].ToString();
            this.NGAYMUA = row["NGAYMUA"].ToString();
            this.HANBH = row["HANBH"].ToString();
            this.GHICHU = row["GHICHU"].ToString();

        }


        private string mAMT;      
        private string iP;
        private string mAC;
        private string dOMAIN;
        private string lOAIMT;
        private string nCC;
        private string nHAMAY;
        private string pB;
        private string nGUOISD;
        private string mATSCD;
        private string nGAYMUA;
        private string hANBH;
        private bool bAOHANH;
        private string gHICHU;
       

      
        public string MAMT { get => mAMT; set => mAMT = value; }
        public bool BAOHANH { get => bAOHANH; set => bAOHANH = value; }
        public string IP { get => iP; set => iP = value; }
        public string MAC { get => mAC; set => mAC = value; }
        public string LOAIMT { get => lOAIMT; set => lOAIMT = value; }
        public string NCC { get => nCC; set => nCC = value; }
       
        public string NGUOISD { get => nGUOISD; set => nGUOISD = value; }
        public string MATSCD { get => mATSCD; set => mATSCD = value; }
        public string NGAYMUA { get => nGAYMUA; set => nGAYMUA = value; }
        public string HANBH { get => hANBH; set => hANBH = value; }
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
        public string NHAMAY { get => nHAMAY; set => nHAMAY = value; }
        public string PB { get => pB; set => pB = value; }
        public string DOMAIN { get => dOMAIN; set => dOMAIN = value; }
    }
}
