using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class QlyYCTcWebDTO
    {
        public QlyYCTcWebDTO(DataRow row)
        {
            // QLYCTCWEB(MANV, PB, NGAYYC, MAWEB, LINKWEB, NGLAPYC, PDPB, PDIT, HTYC)

            this.MANV = row["MANV"].ToString();          
            this.PB = row["PB"].ToString();          
            this.NGAYYC = row["NGAYYC"].ToString();
            this.MAWEB = row["MAWEB"].ToString();
            this.LINHWEB = row["LINKWEB"].ToString();
            this.MDSD = row["MDSD"].ToString();
            this.NGLAPYC = row["NGLAPYC"].ToString();
            this.PDPB = row["PDPB"].ToString();
            this.PDIT = row["PDIT"].ToString();
            this.HTYC = row["HTYC"].ToString();         
        }
    
        private string mANV;      
        private string pB;     
        private string nGAYYC;
        private string mAWEB;
        private string lINHWEB;
        private string mDSD;
        private string nGLAPYC;
        private string pDPB;
        private string pDIT;
        private string hTYC;

        public string MANV { get => mANV; set => mANV = value; }
        public string PB { get => pB; set => pB = value; }
        public string NGAYYC { get => nGAYYC; set => nGAYYC = value; }
        public string MAWEB { get => mAWEB; set => mAWEB = value; }
        public string LINHWEB { get => lINHWEB; set => lINHWEB = value; }
        public string NGLAPYC { get => nGLAPYC; set => nGLAPYC = value; }
        public string PDPB { get => pDPB; set => pDPB = value; }
        public string PDIT { get => pDIT; set => pDIT = value; }
        public string HTYC { get => hTYC; set => hTYC = value; }
        public string MDSD { get => mDSD; set => mDSD = value; }
    }
}
