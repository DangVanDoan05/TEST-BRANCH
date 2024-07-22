using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTO
{
   public class QuanLyYCKTDTO
    {
        public QuanLyYCKTDTO(DataRow row)
        {
            this.MAYCADID = row["MAYCADID"].ToString();
            this.LOAIYCADID = row["LOAIYCADID"].ToString();
            this.PBYEUCAU = row["PBYEUCAU"].ToString();
            this.NGAYYEUCAU = row["NGAYYEUCAU"].ToString();
            this.NGUOILAPYC = row["NGUOILAPYC"].ToString();
            this.TENNGUOILAPYC= row["TENNGUOILAPYC"].ToString();
            this.PHEDUYETPB = (bool)(row["PHEDUYETPB"]);
            if(this.PHEDUYETPB)
            {
                this.TENNGUOIPDPB = row["TENNGUOIPDPB"].ToString();
            }
            else
            {
                this.TENNGUOIPDPB = "";
            }
            this.NGUOIPDPB = row["NGUOIPDPB"].ToString();
           
            this.NGAYPDPB = row["NGAYPDPB"].ToString();
            this.PHEDUYETPBLQ = (bool)(row["PHEDUYETPBLQ"]);
            if (this.PHEDUYETPBLQ)
            {
                this.TENNVPDPBLQ = row["TENNVPDPBLQ"].ToString();
            }
            else
            {
                this.TENNVPDPBLQ = "";
            }          
            this.PHEDUYETIT = (bool)(row["PHEDUYETIT"]);
            if (this.PHEDUYETIT)
            {
                this.TENNGUOIPDIT = row["TENNGUOIPDIT"].ToString();
            }
            else
            {
                this.TENNGUOIPDIT = "";
            }
            this.NGUOIPDIT = row["NGUOIPDIT"].ToString();          
            this.NGAYPDIT = row["NGAYPDIT"].ToString();
            this.PHANCONGTH= row["PHANCONGTH"].ToString();
            this.TENPHANCONGTH= row["TENPHANCONGTH"].ToString();
            this.IDTINHTRANG = int.Parse(row["IDTINHTRANG"].ToString());
            this.CHITIETTT = row["CHITIETTT"].ToString();
            this.TGHOANTAT = row["TGHOANTAT"].ToString();
            this.LYDOTUCHOI = row["LYDOTUCHOI"].ToString();

        }
        private string mAYCADID;
        private string lOAIYCADID;
        private string pBYEUCAU;
        private string nGAYYEUCAU;
        private string nGUOILAPYC;
        private string tENNGUOILAPYC;
        private bool pHEDUYETPB;
        private string nGUOIPDPB;
        private string tENNGUOIPDPB;
        private string nGAYPDPB;
        private bool pHEDUYETPBLQ;
        private string tENNVPDPBLQ;
        private bool pHEDUYETIT;
        private string nGUOIPDIT;
        private string tENNGUOIPDIT;
        private string nGAYPDIT;
        private string pHANCONGTH;
        private string tENPHANCONGTH;
        private int iDTINHTRANG;
        private string cHITIETTT;
        private string tGHOANTAT;
        private string lYDOTUCHOI;
        public string MAYCADID { get => mAYCADID; set => mAYCADID = value; }
        public string LOAIYCADID { get => lOAIYCADID; set => lOAIYCADID = value; }
        public string PBYEUCAU { get => pBYEUCAU; set => pBYEUCAU = value; }
        public string NGAYYEUCAU { get => nGAYYEUCAU; set => nGAYYEUCAU = value; }
        public string NGUOILAPYC { get => nGUOILAPYC; set => nGUOILAPYC = value; }
        public bool PHEDUYETPB { get => pHEDUYETPB; set => pHEDUYETPB = value; }
        public string NGUOIPDPB { get => nGUOIPDPB; set => nGUOIPDPB = value; }
        public bool PHEDUYETIT { get => pHEDUYETIT; set => pHEDUYETIT = value; }
        public string NGUOIPDIT { get => nGUOIPDIT; set => nGUOIPDIT = value; }      
        public string NGAYPDPB { get => nGAYPDPB; set => nGAYPDPB = value; }
        public string NGAYPDIT { get => nGAYPDIT; set => nGAYPDIT = value; }
        public int IDTINHTRANG { get => iDTINHTRANG; set => iDTINHTRANG = value; }
        public string CHITIETTT { get => cHITIETTT; set => cHITIETTT = value; }
        public string PHANCONGTH { get => pHANCONGTH; set => pHANCONGTH = value; }
        public string TGHOANTAT { get => tGHOANTAT; set => tGHOANTAT = value; }
        public string TENNGUOILAPYC { get => tENNGUOILAPYC; set => tENNGUOILAPYC = value; }
        public string TENNGUOIPDPB { get => tENNGUOIPDPB; set => tENNGUOIPDPB = value; }
        public string TENNGUOIPDIT { get => tENNGUOIPDIT; set => tENNGUOIPDIT = value; }
        public string TENPHANCONGTH { get => tENPHANCONGTH; set => tENPHANCONGTH = value; }
        public string LYDOTUCHOI { get => lYDOTUCHOI; set => lYDOTUCHOI = value; }
        public string TENNVPDPBLQ { get => tENNVPDPBLQ; set => tENNVPDPBLQ = value; }
        public bool PHEDUYETPBLQ { get => pHEDUYETPBLQ; set => pHEDUYETPBLQ = value; }
    }
}
