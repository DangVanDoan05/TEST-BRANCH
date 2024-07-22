using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class QLyYCADIDDTO
    {
        public QLyYCADIDDTO(DataRow row)
        {
            //  QLYCADID(MANV, HOTEN, PB, NHAMAY, NHOM, LOAIADID, ADID, MKBD, MAIL, NGAYYC, NGLAPYC, PDPB, PDIT, HTYC) 

            this.MANV = row["MANV"].ToString();
            this.HOTEN = row["HOTEN"].ToString();
            this.PB = row["PB"].ToString();
            this.NHAMAY = row["NHAMAY"].ToString();
            this.NHOM = row["NHOM"].ToString();
            this.LOAIADID = row["LOAIADID"].ToString();
            this.ADID = row["ADID"].ToString();
            this.MKBD = row["MKBD"].ToString();
            this.MAIL = row["MAIL"].ToString();
            this.NGAYYC = row["NGAYYC"].ToString();
            this.NGLAPYC = row["NGLAPYC"].ToString();
            this.PDPB = row["PDPB"].ToString();
            this.PDIT = row["PDIT"].ToString();
            this.HTYC = row["HTYC"].ToString();
            this.STT =int.Parse(row["STT"].ToString());

        }

     

        private string mANV;
        private string hOTEN;
        private string pB;
        private string nHAMAY;
        private string nHOM;
        private string lOAIADID;
        private string aDID;
        private string mKBD;
        private string mAIL;
        private string nGAYYC;
        private string nGLAPYC;
        private string pDPB;
        private string pDIT;
        private string hTYC;
        private int sTT;


        public string MANV { get => mANV; set => mANV = value; }
        public string HOTEN { get => hOTEN; set => hOTEN = value; }
        public string PB { get => pB; set => pB = value; }
        public string NHAMAY { get => nHAMAY; set => nHAMAY = value; }
        public string NHOM { get => nHOM; set => nHOM = value; }
        public string LOAIADID { get => lOAIADID; set => lOAIADID = value; }
        public string ADID { get => aDID; set => aDID = value; }
        public string MKBD { get => mKBD; set => mKBD = value; }
        public string MAIL { get => mAIL; set => mAIL = value; }
        public string NGAYYC { get => nGAYYC; set => nGAYYC = value; }
        public string NGLAPYC { get => nGLAPYC; set => nGLAPYC = value; }
        public string PDPB { get => pDPB; set => pDPB = value; }
        public string PDIT { get => pDIT; set => pDIT = value; }
        public string HTYC { get => hTYC; set => hTYC = value; }
        public int STT { get => sTT; set => sTT = value; }
    }
}
