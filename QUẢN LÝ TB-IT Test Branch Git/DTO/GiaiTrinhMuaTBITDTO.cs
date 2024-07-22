using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GiaiTrinhMuaTBITDTO
    {

        // insert GIAITRINHMUATB(MAGT, PB, NGAYYC, LOAITB, MDSD, SLHTAI, VTHTAI, TSSDHTAI, PPLVHTAI, SLSAU, VTSAU, TSSDSAU, PPLVSAU, NGLAPYC, PDPB, PDIT, HTYC)

        public GiaiTrinhMuaTBITDTO(DataRow row)
        {
            this.MAGT = row["MAGT"].ToString();
            this.PB = row["PB"].ToString();
            this.NGAYYC = row["NGAYYC"].ToString();
            this.LOAITB = row["LOAITB"].ToString();
            this.MDSD = row["MDSD"].ToString();
            this.SLHTAI =int.Parse( row["SLHTAI"].ToString());
            this.VTHTAI = row["VTHTAI"].ToString();
            this.TSSDHTAI = row["TSSDHTAI"].ToString();
            this.PPLVHTAI = row["PPLVHTAI"].ToString();
            this.SLSAU =int.Parse(row["SLSAU"].ToString());
            this.VTSAU = row["VTSAU"].ToString();
            this.TSSDSAU = row["TSSDSAU"].ToString();
            this.PPLVSAU = row["PPLVSAU"].ToString();
            this.NGLAPYC = row["NGLAPYC"].ToString();
            this.PDPB = row["PDPB"].ToString();
            this.PDIT = row["PDIT"].ToString();
            this.HTYC = row["HTYC"].ToString();
        }

        

        private string mAGT;
        private string pB;
        private string nGAYYC;
        private string lOAITB;
        private string mDSD;
        private int sLHTAI;
        private string vTHTAI;
        private string tSSDHTAI;
        private string pPLVHTAI;
        private int sLSAU;
        private string vTSAU;
        private string tSSDSAU;
        private string pPLVSAU;
        private string nGLAPYC;
        private string pDPB;
        private string pDIT;
        private string hTYC;

        public string MAGT { get => mAGT; set => mAGT = value; }
        public string PB { get => pB; set => pB = value; }
        public string NGAYYC { get => nGAYYC; set => nGAYYC = value; }
        public string LOAITB { get => lOAITB; set => lOAITB = value; }
        public string MDSD { get => mDSD; set => mDSD = value; }
     
        public string VTHTAI { get => vTHTAI; set => vTHTAI = value; }
        public string TSSDHTAI { get => tSSDHTAI; set => tSSDHTAI = value; }
        public string PPLVHTAI { get => pPLVHTAI; set => pPLVHTAI = value; }
        public int SLSAU { get => sLSAU; set => sLSAU = value; }
        public string VTSAU { get => vTSAU; set => vTSAU = value; }
        public string TSSDSAU { get => tSSDSAU; set => tSSDSAU = value; }
        public string PPLVSAU { get => pPLVSAU; set => pPLVSAU = value; }
        public string NGLAPYC { get => nGLAPYC; set => nGLAPYC = value; }
        public string PDPB { get => pDPB; set => pDPB = value; }
        public string PDIT { get => pDIT; set => pDIT = value; }
        public string HTYC { get => hTYC; set => hTYC = value; }
        public int SLHTAI { get => sLHTAI; set => sLHTAI = value; }
    }
}
