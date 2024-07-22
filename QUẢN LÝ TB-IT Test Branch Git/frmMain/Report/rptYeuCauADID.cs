using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DAO;
using DTO;
using System.Collections.Generic;

namespace frmMain.Report
{
    public partial class rptYeuCauADID : DevExpress.XtraReports.UI.XtraReport
    {
        public rptYeuCauADID()
        {
            InitializeComponent();
        }
        //private List<ChiTietYCADIDDTO> _lstUserYcADID;
        //private QuanLyYCKTDTO _UserPD;
        //private string _NgayPDPB;
        //private string _NgayPDIT;
        //public rptYeuCauADID(List<ChiTietYCADIDDTO> list, QuanLyYCKTDTO UserPD)
        //{
        //    InitializeComponent();
        //    this._lstUserYcADID= list;
        //    this._UserPD = UserPD;
        //    this.DataSource = _lstUserYcADID;
        //    LoadChitietYC();
        //    this.lblDxPD.Text = _UserPD.TENNGUOILAPYC;
        //    if (_UserPD.PHEDUYETPB)
        //    {
        //        this.lblPbPD.Text = _UserPD.TENNGUOIPDPB;
        //    }
        //    else
        //    {
        //        this.lblPbPD.Text = "";
        //    }

        //    if(_UserPD.PHEDUYETIT)
        //    {
        //        this.lblItPD.Text=_UserPD.TENNGUOIPDIT;
        //    }
        //    else
        //    {
        //        this.lblItPD.Text = "";
        //    }                           
        //    this.lblNgayThamTra.Text = _UserPD.NGAYPDPB;
        //    this.lblNgayPheDuyet.Text = _UserPD.NGAYPDIT;
        //}
        //public rptYeuCauADID(List<ChiTietYCADIDDTO> list)
        //{
        //    InitializeComponent();
        //    this._lstUserYcADID = list;          
        //    this.DataSource = _lstUserYcADID;
        //    LoadChitietYC();
        //   // this.lblDxPD.Text = CommonUser.UserStatic.FULLNAME;           
        //}

        //private void LoadChitietYC()
        //{
        //    lblSTT.DataBindings.Add("Text", _lstUserYcADID, "STT");
        //    lblMaNV.DataBindings.Add("Text", _lstUserYcADID, "MANV");
        //    lblHo.DataBindings.Add("Text", _lstUserYcADID, "HO");
        //    lblTen.DataBindings.Add("Text", _lstUserYcADID, "TEN");
        //    lblPhanlaiYC.DataBindings.Add("Text", _lstUserYcADID, "PHANLOAIYC");
        //    lblPhongBan.DataBindings.Add("Text", _lstUserYcADID, "PHONGBAN");
        //    lblNhamay.DataBindings.Add("Text", _lstUserYcADID, "NHAMAY");
        //    lblADID.DataBindings.Add("Text", _lstUserYcADID, "ADID");
        //    lblNhom.DataBindings.Add("Text", _lstUserYcADID,"NHOM");
        //    lblMail.DataBindings.Add("Text", _lstUserYcADID,"SDMAIL");
        //    lblNgayDeXuat.DataBindings.Add("Text", _lstUserYcADID, "NGAYTAOYC");
        //}
      

    }
}
