using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DAO;
using DTO;
using frmMain.Report;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Grid;

namespace frmMain.YeuCauKyThuat
{
    public partial class frmQlyYCKTLQPB : DevExpress.XtraEditors.XtraForm
    {

        public frmQlyYCKTLQPB()
        {
            InitializeComponent();
            LoadControl();

        }

        string maYCADID = "";
        private void LoadControl()
        {
            LoadData();

        }



        private void LoadData()
        {
            try
            {
                DateTime ngaybd = dtpNgayBĐ.Value;
                DateTime ngaykt = dtpNgayKT.Value;

                // Sai Logic

              //  gridControl1.DataSource = QuanLyYCKTDAO.Instance.GetListYcPDPB_IT(ngaybd, ngaykt);
              //  sglNhanLuc.Properties.DataSource = QlyPhanQuyenDAO.Instance.GetMaNVTHYCKT();
                sglNhanLuc.Properties.DisplayMember = "MANV";
                sglNhanLuc.Properties.ValueMember = "MANV";
            }
            catch
            {
                MessageBox.Show("Hãy để định dạng thời gian máy tính của bạn theo dd/MM/yyyy.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void btnLocYC_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime ngaybd = dtpNgayBĐ.Value;
                DateTime ngaykt = dtpNgayKT.Value;
              //  gridControl1.DataSource = QuanLyYCKTDAO.Instance.GetListYcPDPB_IT(ngaybd, ngaykt);
            
            }
            catch 
            {
                MessageBox.Show("Hãy để định dạng thời gian máy tính của bạn theo dd/MM/yyyy.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
           
        }
        private void btnXem_Click(object sender, EventArgs e)
        {

            if (maYCADID == "")
            {
                MessageBox.Show("Bạn chưa chọn yêu cầu kỹ thuật cần xem!", "Cảnh báo:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                QuanLyYCKTDTO YcDTO = QuanLyYCKTDAO.Instance.GetYcktDTO(maYCADID);
                DialogResult kq = MessageBox.Show($"Bạn muốn xem mã yêu cầu {maYCADID} ?", "Thông báo: ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    // Update trạng thái quản lý IT đã xem
                    //****Nếu chưa xem thì mới Update lên trạng thái đã xem
                    if (YcDTO.IDTINHTRANG<6)
                    {
                        TinhTrangYCKTDTO tinhtrang = TinhTrangYCKTDAO.Instance.GetChiTietQuyen(6); // Tình trạng 6: quản lý IT đã xem
                        QuanLyYCKTDAO.Instance.UpdateTinhTrang(maYCADID, tinhtrang.IDTINHTRANG, tinhtrang.CHITIETTT);
                    }
                    //*** Trường hợp là yêu cầu ADID
                    if (YcDTO.LOAIYCADID == "Đăng ký User mới")
                    {
                        try
                        {
                            rptYeuCauADID rpt = new rptYeuCauADID(ChiTietYeuCauADIDDAO.Instance.GetLsUserYCADID(maYCADID), QuanLyYCKTDAO.Instance.GetYcktDTO(maYCADID));
                            rpt.ShowPreview();
                        }
                        catch
                        {
                        }
                    }
                    //*** Trường hợp là yêu cầu đổi thiết bị
                    if (YcDTO.LOAIYCADID == "YÊU CẦU ĐỔI THIẾT BỊ IT")
                    {
                        ChiTietDoiThietBiDTO ChiTietYCdoiDTO = ChiTietYeuCauDOITBDAO.Instance.GetChiTietYCdoiDTO(maYCADID);
                        try
                        {
                            rptYeuCauDoiTBIT rpt = new rptYeuCauDoiTBIT(ChiTietYCdoiDTO, QuanLyYCKTDAO.Instance.GetYcktDTO(maYCADID));
                            rpt.ShowPreview();
                        }
                        catch
                        {
                        }

                    }

                    #region Xem yêu cầu sử dụng phần mềm.
                    //*** Trường hợp là yêu cầu sử dụng phần mềm.
                    if (YcDTO.LOAIYCADID == "YÊU CẦU ĐỔI SỬ DỤNG VÀ TẢI PHẦN MỀM")
                    {
                        ChiTietYcSDPMDTO ChiTietYcDTO = ChiTietYcSDPMDAO.Instance.GetChiTietYCDTO(maYCADID);
                        QuanLyYCKTDTO ycktDTO = QuanLyYCKTDAO.Instance.GetYcktDTO(maYCADID);
                        try
                        {
                            rptYeuCauSD_TaiPM rpt = new rptYeuCauSD_TaiPM(ChiTietYcDTO, ycktDTO);
                            rpt.ShowPreview();
                        }
                        catch
                        {
                        }
                    }
                    #endregion

                }
            }

        }

        private void btnPheDuyet_Click(object sender, EventArgs e)
        {

            // Update vào bảng quản lý yêu cầu đó được phê duyệt
            if (maYCADID == "")
            {
                MessageBox.Show("Bạn chưa chọn yêu cầu kỹ thuật cần phê duyệt.", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                QuanLyYCKTDTO ycDcChon = QuanLyYCKTDAO.Instance.GetYcktDTO(maYCADID);

                if (ycDcChon.IDTINHTRANG < 6) //tình trạng 6: IT đã xem YC Nếu quản lý IT chưa xem
                {
                   MessageBox.Show("Hãy xem yêu cầu trước khi phê duyệt.", "Thông báo: ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn phê duyệt yêu cầu {maYCADID} ?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {                     
                        //**** Load bảng quản lý
                        string ngayPDPB = DateTime.Now.ToString("dd/MM/yyyy");
                        TinhTrangYCKTDTO tinhtrang = TinhTrangYCKTDAO.Instance.GetChiTietQuyen(8); // Tình trạng số 8: IT đã phê duyệt 
                        QuanLyYCKTDAO.Instance.UpdatePDIT(maYCADID, true, CommonUser.UserStatic.MANV,CommonUser.UserStatic.FULLNAME,ngayPDPB, tinhtrang.IDTINHTRANG, tinhtrang.CHITIETTT);
                        //**** Load luôn cả vào report
                        //rptYeuCauADID rpt = new rptYeuCauADID(ChiTietYeuCauADIDDAO.Instance.GetLsUserYCADID(maYCADID),QuanLyYCKTDAO.Instance.GetYcktDTO(maYCADID));
                        //rpt.ShowPreview();
                        MessageBox.Show($"Phê duyệt thành công yêu cầu được chọn.", "Thông báo: ");
                    }
                }
               
            }

            LoadControl();

        }

        private void btnPhancongTH_Click(object sender, EventArgs e)
        {
           
                if (maYCADID == "")
                {
                    MessageBox.Show("Chưa chọn yêu cầu kỹ thuật để phân công. ", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    QuanLyYCKTDTO ycDcChon = QuanLyYCKTDAO.Instance.GetYcktDTO(maYCADID);

                    if (ycDcChon.IDTINHTRANG < 8) // Nếu quản lý IT chưa phê duyệt.
                    {
                        MessageBox.Show("Không thể phân công do chưa phê duyệt.", "Thông báo: ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        try
                        {
                            string maNV = sglNhanLuc.Text;
                            string tenNV = (UserDAO.Instance.GetUserDTO1(maNV)).FULLNAME;
                            DialogResult kq = MessageBox.Show($"Bạn muốn phân công {tenNV} xử lý yêu cầu ? ", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (kq == DialogResult.Yes)
                            {
                               TinhTrangYCKTDTO tinhtrang1 = TinhTrangYCKTDAO.Instance.GetChiTietQuyen(9); // Tình trạng số 9: Đã phân công thực hiện
                               QuanLyYCKTDAO.Instance.UpdatePCTH(maYCADID, maNV,tenNV,tinhtrang1.IDTINHTRANG, tinhtrang1.CHITIETTT);
                               MessageBox.Show($"Thành công, yêu cầu đã được gửi đến {tenNV}.", " Thông báo: ");
                            
                            }
                        }
                        catch 
                        {
                            MessageBox.Show("Chưa chọn nhân viên để phân công. ", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                           

                    }

                }
                LoadControl();
           

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                maYCADID = gridView1.GetFocusedRowCellValue("MAYCADID").ToString();
            }
            catch
            {

            }
        }

        private void btnCapNhatTrang_Click(object sender, EventArgs e)
        {
            try
            {
                int monthNow = DateTime.Now.Month;
                int monthNowNext = monthNow + 1;
                int yearNow = DateTime.Now.Year;
                if (monthNow == 12)
                {
                    monthNowNext = 1;
                }

                //DateTime date = new DateTime(dtpNgaybatdau.Value.Year, dtpNgaybatdau.Value.Month, 1);
                //dtpNgaybatdau.Value = date;
                //dtpNgaybatdau.Value = Convert.ToDateTime($"01/{monthNow}/{yearNow}");

                //DateTime ngaydauthangsau = Convert.ToDateTime($"01/{monthNowNext}/{yearNow}");
                dtpNgayBĐ.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).Date;
                DateTime ngaycuoithang = dtpNgayBĐ.Value.Date.AddMonths(1).AddSeconds(-1);
                dtpNgayKT.Value = ngaycuoithang;
             //   gridControl1.DataSource = QuanLyYCKTDAO.Instance.GetListYcPDPB_IT(dtpNgayBĐ.Value, dtpNgayKT.Value);
            }
            catch
            {
                MessageBox.Show("Hãy để định dạng thời gian máy tính của bạn theo dd/MM/yyyy.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
           
        }

       

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string maYCADID = view.GetRowCellValue(e.RowHandle, view.Columns["MAYCADID"]).ToString();
            QuanLyYCKTDTO YCDTO = QuanLyYCKTDAO.Instance.GetYcktDTO(maYCADID);
            if ( YCDTO.IDTINHTRANG < 8) // IT chưa phê duyệt
            {
                e.Appearance.BackColor = txtChuaPD.BackColor;
            }
            if (YCDTO.IDTINHTRANG == 8) // phê duyệt IT
            {
                e.Appearance.BackColor = txtDaPD.BackColor;
            }
            if (YCDTO.IDTINHTRANG == 9) // đã phân công TH
            {
                e.Appearance.BackColor = txtDaPCTH.BackColor;
            }
            if (YCDTO.IDTINHTRANG >=9 && YCDTO.IDTINHTRANG <13) // chạy từ 6 đến 7
            {
                e.Appearance.BackColor = txtDangTH.BackColor;
            }           
            if (YCDTO.IDTINHTRANG == 13)
            {
                e.Appearance.BackColor = txtHoanTatYCKT.BackColor;
            }
        }

        private void searchLookUpEdit1View_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnCapNhatlaiYCKT_Click(object sender, EventArgs e)
        {
            // Nút chức năng cho phép đẩy lại YCKT cho nhân viên thực hiện trong trường hợp cập nhật sai vật tư.
            if (maYCADID == "")
            {
                MessageBox.Show("Chưa chọn yêu cầu kỹ thuật để phân công. ", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Tình trạng hiện tại là đã được hoàn tất rồi.
                QuanLyYCKTDTO ycDcChon = QuanLyYCKTDAO.Instance.GetYcktDTO(maYCADID);

                if (ycDcChon.IDTINHTRANG < 13) // Nếu yêu cầu chưa được hoàn tất
                {
                    MessageBox.Show("Chức năng chỉ được áp dụng cho yêu cầu đã được hoàn tất.", "Thông báo: ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else // nếu yêu cầu đã được hoàn tất.
                {
                                       
                        DialogResult kq = MessageBox.Show($"Bạn muốn cập nhật lại yêu cầu ? ", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (kq == DialogResult.Yes)
                        {
                            TinhTrangYCKTDTO tinhtrang = TinhTrangYCKTDAO.Instance.GetChiTietQuyen(9); // Tình trạng số 9: Đã phân công thực hiện
                            QuanLyYCKTDAO.Instance.UpdateTinhTrang(maYCADID,tinhtrang.IDTINHTRANG,tinhtrang.CHITIETTT);

                            // *** Cộng lại lượng vật tư sử dụng vào số lượng tồn.

                            List<ChiTietTHYCKTDTO> LstVtuSD = ChiTietTHYCKTDAO.Instance.GetListVtu(maYCADID);
                            List<TonLinhKienDTO> LsLinhKienTon = TonLinhKienDAO.Instance.GetListMaLKTon();// lấy ra được danh sách mã linh kiện tồn
                            foreach (ChiTietTHYCKTDTO item in LstVtuSD)
                            {
                                foreach (TonLinhKienDTO item1 in LsLinhKienTon)
                                {
                                    if (item.MAVATTUSD == item1.MALK)
                                    {
                                        item1.SLTON = item1.SLTON + item.SOLUONG; // Cộng lại số lượng sử dụng vào số lượng tồn
                                        TonLinhKienDAO.Instance.UpdateSLTON(item1.MALK, item1.SLTON);
                                    }
                                }
                            }
                            MessageBox.Show($"Cập nhật lại tình trạng cho YCKT {maYCADID} thành công! ", " Thông báo: ");
                        }
                                     

                }

            }
            LoadControl();
        }
        private void btnTuChoiYCKT_Click(object sender, EventArgs e)
        {
            // IT vẫn có thể từ chối yêu cầu cả khi
            // Update từ chối yêu cầu phê duyệt
            if (maYCADID == "")
            {
                MessageBox.Show("Bạn chưa chọn yêu cầu kỹ thuật muốn từ chối.", "Cảnh báo:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                QuanLyYCKTDTO ycDcChon = QuanLyYCKTDAO.Instance.GetYcktDTO(maYCADID);
                if (ycDcChon.IDTINHTRANG < 6) // yêu cầu phải xem trước
                {
                    MessageBox.Show("Hãy xem yêu cầu trước khi từ chối. ", "Cảnh báo:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (ycDcChon.IDTINHTRANG >= 8&&ycDcChon.IDTINHTRANG<12) // nếu quản lý IT đã phê duyệt, IT vẫn có quyền từ chối
                    {
                        // MessageBox.Show("Không thể từ chối do yêu cầu đã được phê duyệt.", "Cảnh báo:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        DialogResult kq = MessageBox.Show($"Yêu cầu đã được phê duyệt trước đó, bạn vẫn muốn từ chối.", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (kq == DialogResult.Yes)
                        {
                            // Tình trạng số 7: Quản lý IT từ chối YCKT
                            // ====> Update về trạng thái số 7 + update chưa phê duyệt
                            TinhTrangYCKTDTO tinhtrang = TinhTrangYCKTDAO.Instance.GetChiTietQuyen(7);                          
                            YeuCauKyThuatTinh.MaYCKTtuchoi = maYCADID;
                            frmCapNhatTuChoiYCKT f = new frmCapNhatTuChoiYCKT();
                            f.ShowDialog();
                        }
                    }
                    else
                    {
                        YeuCauKyThuatTinh.MaYCKTtuchoi = maYCADID;
                        frmCapNhatTuChoiYCKT f = new frmCapNhatTuChoiYCKT();
                        f.ShowDialog();
                    }
                }
            }
            LoadControl();
        }
    }
}