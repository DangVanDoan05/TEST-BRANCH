using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DTO;
using DAO;

namespace frmMain.Report
{
    public partial class rptDiChuyenMayTinh : DevExpress.XtraReports.UI.XtraReport
    {
        //public rptDiChuyenMayTinh()
        //{
        //    InitializeComponent();
        //}
        //private ChiTietYCDiChuyenMTDTO _ChiTietDCMT;
        //private QuanLyYCKTDTO _UserPD;
        //private QlyPheDuyetPBLQDTO _UserPDPBLQ;
        //public rptDiChuyenMayTinh(ChiTietYCDiChuyenMTDTO ChitietYcDcMT, QuanLyYCKTDTO QuanlyYCDTO,QlyPheDuyetPBLQDTO PDPBLQdto)
        //{
        //    InitializeComponent();
        //    this._ChiTietDCMT = ChitietYcDcMT;
        //    this._UserPD = QuanlyYCDTO;
        //    this._UserPDPBLQ = PDPBLQdto;
        //    //*** Load dữ liệu 
        //    this.lblPbYC.Text = PhongBanDAO.Instance.GetPBDTO( _ChiTietDCMT.PHONGBANYC).TENPB;
        //    this.lblNgayYC.Text = _ChiTietDCMT.NGAYYC;
        //    this.lblTenMTtrTD.Text = _ChiTietDCMT.TENMTTRUOCTD;
        //    this.lblMucDichSD.Text = _ChiTietDCMT.MDSDHT;
        //    this.lblNgSDtrTD.Text = _ChiTietDCMT.NGUOISDHT;
        //    this.lblViTritrTD.Text = _ChiTietDCMT.VITRIHT;
        //    this.lblNoiDungYC.Text = _ChiTietDCMT.NOIDUNGYC;
        //    this.lblLyDo.Text = _ChiTietDCMT.LYDOYC;
        //    this.lblPBsdSauTD.Text = PhongBanDAO.Instance.GetPBDTO( _ChiTietDCMT.PBSDSAUTD).TENPB;
        //    this.lblVitriSauTD.Text = _ChiTietDCMT.VITRISAUTD;
        //    this.lblNgSdSauTD.Text = _ChiTietDCMT.NGUOISDSAUTD;
        //    this.lblTenMTSauTD.Text = _ChiTietDCMT.TENMTSAUTD;
        //    this.lblGhiChu.Text = _ChiTietDCMT.GHICHU;
        //    // Nếu chưa phê duyệt thì để trống  
        //    this.lblNgayDeXuat.Text = _UserPD.NGAYYEUCAU;

        //  //  this.lblDxPD.Text = UserDAO.Instance.GetUserDTO1(_UserPD.NGUOILAPYC).FULLNAME;

        //    // 2 trường hợp di chuyển cùng phòng ban và khác phòng ban

        //    if (_UserPD.PHEDUYETPB)
        //    {
        //        this.lblQlSC_PbPD.Text = _UserPD.TENNGUOIPDPB;
        //        this.lblNgayPBPD.Text = _UserPD.NGAYPDPB;
        //    }
        //    else
        //    {
        //        this.lblQlSC_PbPD.Text = "";
        //        this.lblNgayPBPD.Text = "";
        //    }
        //    if (_UserPDPBLQ.PHEDUYETPBLQ)
        //    {
        //        this.lblQlySC_PbLQ.Text = _UserPDPBLQ.TENNVPDPBLQ;
        //        this.lblNgayPBLQPD.Text = _UserPDPBLQ.NGAYPDPBLQ;
        //    }
        //    else
        //    {
        //        this.lblNgayPBLQPD.Text = "";
        //        this.lblNgayPBLQPD.Text = "";
        //    }
        //    if (_UserPD.PHEDUYETIT)  // Phê duyệt IT
        //    {
        //        this.lblQlSC_IT.Text = _UserPD.TENNGUOIPDIT;

        //        this.lblNgayPDIT.Text = _UserPD.NGAYPDIT;
        //    }
        //    else
        //    {
        //        this.lblQlSC_IT.Text = "";
        //        this.lblQlSC_IT.Text = "";
        //        this.lblQlSC_IT.Text = "";
        //        this.lblQlSC_IT.Text = "";
        //    }        
       // }
    }
}
