using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frmMain.YeuCauKyThuat
{
   public class Name
    {
        private string nAME;

        public string NAME { get => nAME; set => nAME = value; }
        public Name(string ttth)
        {
            this.NAME = ttth;
        }

    }
}
