using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using DTO;

namespace frmMain.Report
{
    public partial class rptBienBanBG : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBienBanBG()
        {
            InitializeComponent();
        }
     
        private string ddTg;

        public string DdTg { get => ddTg; set => ddTg = value; }
      

     

    }
}
