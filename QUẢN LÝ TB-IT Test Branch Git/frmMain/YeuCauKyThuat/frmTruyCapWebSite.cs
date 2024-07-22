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
using DevExpress.XtraGrid.Views.Grid;

namespace frmMain.YeuCauKyThuat
{
    public partial class frmTruyCapWebSite : DevExpress.XtraEditors.XtraForm
    {
        public frmTruyCapWebSite()
        {
            InitializeComponent();
            LoadControl();
        }

        bool them;
        int idquyen = CommonUser.Quyen;
        private void LoadControl()
        {
            LockControl(true);
            LoadData();
        }

        private void LoadData()
        {
            // Load website:

            sglWebSite.Properties.DataSource = DsWebDAO.Instance.GetTable();
            sglWebSite.Properties.DisplayMember = "MAWEB";
            sglWebSite.Properties.ValueMember = "MAWEB";

            // LOAD BẢNG NHÂN VIÊN:

                   
                        
                    if(idquyen>5)
                    {
                        if(idquyen<=6)
                        {
                            gcNhanVien.DataSource = QLyYCADIDDAO.Instance.GetLsYcADIDDaHTOfBP(CommonUser.UserStatic.BOPHAN);
                        }
                        else
                        {
                            gcNhanVien.DataSource = QLyYCADIDDAO.Instance.GetLSAllYcADIDDaHT();
                        }
                    }
                    else
                    {
                        gcNhanVien.DataSource = QLyYCADIDDAO.Instance.GetLSAllYcADIDDaHTOfPB(CommonUser.UserStatic.PHONGBAN);
                    }
                   

           

            // LOAD BẢNG DANH SÁCH YÊU CẦU:
        
            string PhongBan = CommonUser.UserStatic.PHONGBAN;       
            if (PhongBan == "IT" || PhongBan == "ADMIN")
            {
                if (PhongBan == "ADMIN")     //phòng ADMIN: ADMIN thì Load toàn bộ
                {
                    gcDsYCWeb.DataSource =QlyYCTcWebDAO.Instance.GetAllYcTcWeb();
                }
                else    //phòng IT
                {
                    if (idquyen >= 4)     // Phê duyệt sơ cấp trở lên
                    {
                        gcDsYCWeb.DataSource = QlyYCTcWebDAO.Instance.GetYCwithQlyIT(CommonUser.UserStatic.MANV);
                    }
                    else
                    {
                        if (idquyen >= 2)  // Load cả những yêu cầu mà phòng IT lập
                        {
                            gcDsYCWeb.DataSource = QlyYCTcWebDAO.Instance.GetYCwithNvIT(CommonUser.UserStatic.MANV);
                        }
                    }
                }
            }

            else   // Với phòng ban khác
            {
                if (idquyen >= 7) // Tổng giám đốc
                {
                    gcDsYCWeb.DataSource = QlyYCTcWebDAO.Instance.GetAllYcTcWeb();
                }
                else
                {
                    if (idquyen >= 6) // QLCC: Load bộ phận
                    {
                        gcDsYCWeb.DataSource = QlyYCTcWebDAO.Instance.GetLsYcTcWebOfBP(CommonUser.UserStatic.BOPHAN);
                    }
                    else
                    {
                        if (idquyen == 5) // QLTC Load yêu cầu của phòng ban
                        {
                            gcDsYCWeb.DataSource = QlyYCTcWebDAO.Instance.GetLsYcTcWebOfPB(CommonUser.UserStatic.PHONGBAN);
                        }
                        else // QLSC trở xuống load những Web mà mình được truy cập
                        {
                            gcDsYCWeb.DataSource = QlyYCTcWebDAO.Instance.GetYCwithNhanVien(CommonUser.UserStatic.MANV);
                        }
                    }
                }
            }

        }

        private void LockControl(bool kt)
        {
           if(kt)
            {
                sglWebSite.Enabled = false;
                gcNhanVien.Enabled = false;
                txtMDSD.Enabled = false;

                btnThem.Enabled = true;
                btnXacNhan.Enabled = false;
                btnXoa.Enabled = true;
                btnCapNhat.Enabled = true;
            }
           else
            {
                sglWebSite.Enabled = true;
                gcNhanVien.Enabled = true;
                txtMDSD.Enabled = true;


                btnThem.Enabled = false;
                btnXacNhan.Enabled = true;
                btnXoa.Enabled = false;
                btnCapNhat.Enabled = true;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LockControl(false);
            them = true;
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if(them)
            {
                try
                {
                    //   public int Insert(string MaNV, string Phongban, string NgayYC, string MaWeb, string LinkWeb, string NglapYC, string PDPB, string PDIT, string HoanTatYC)
                    string MaWeb = sglWebSite.EditValue.ToString();
                    
                    int dem = 0;
                  
                    List<string> LsMaNVdcChon = new List<string>();
                    foreach (var item in gridView2.GetSelectedRows())
                    {
                        string ma1 = gridView2.GetRowCellValue(item, "MANV").ToString();
                        LsMaNVdcChon.Add(ma1);
                        dem++;
                    }

                    if(dem>0)
                    {
                      
                        DsWebDTO webDTO = DsWebDAO.Instance.GetWebDTO(MaWeb);
                        string LinkWeb = webDTO.LINKWEB;
                        string mucdichSD = txtMDSD.Text;
                        string NgayYC = DateTime.Now.ToString("dd/MM/yyyy");
                        string NglapYC = CommonUser.UserStatic.MANV + "-" + NgayYC + DateTime.Now.ToString(" HH:mm:ss tt");
                        string PdPB = "";
                        string PDIT = "";
                        string HoantatYC = "";
                        foreach (string item in LsMaNVdcChon)
                        {
                            //QLNhanVienDTO NVdto = QLNhanVienDAO.Instance.GetNhanVienDTO(item);
                            //string Phongban = NVdto.PHONGBAN;
                            //bool CheckWebOfNV = QlyYCTcWebDAO.Instance.CheckWebOfNV(item, MaWeb);
                            //if(!CheckWebOfNV)
                            //{
                            //    QlyYCTcWebDAO.Instance.Insert(item, Phongban, NgayYC, MaWeb, LinkWeb,mucdichSD, NglapYC, PdPB, PDIT, HoantatYC);                             
                            //}                                                    
                        }                      
                    }
                    else
                    {
                        MessageBox.Show("Chưa chọn nhân viên truy cập Web.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch 
                {
                    MessageBox.Show("Hãy chọn Website muốn truy cập.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            them = false;
            LoadControl();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string MaNV = view.GetRowCellValue(e.RowHandle, view.Columns["MANV"]).ToString();
            string MaWeb = view.GetRowCellValue(e.RowHandle, view.Columns["MAWEB"]).ToString();
            QlyYCTcWebDTO YC = QlyYCTcWebDAO.Instance.GetYcTcWebDTO(MaNV,MaWeb);

            if (YC.PDPB == "" && YC.PDIT == "" && YC.HTYC == "")
            {
                e.Appearance.BackColor =btnChoPDPB.Appearance.BackColor;
            }
            if (YC.PDPB != "" && YC.PDIT == "" && YC.HTYC == "")
            {
                e.Appearance.BackColor = btnChoPDIT.Appearance.BackColor;
            }
            if (YC.PDPB != "" && YC.PDIT != "" && YC.HTYC == "")
            {
                e.Appearance.BackColor = btnDangThYC.Appearance.BackColor;
            }
            if (YC.PDPB != "" && YC.PDIT != "" && YC.HTYC != "")
            {
                e.Appearance.BackColor = btnHoanTatYC.Appearance.BackColor;
            }
        }

        private void gcDsYCWeb_Click(object sender, EventArgs e)
        {
            try
            {
                string MaNV = gridView1.GetFocusedRowCellValue("MANV").ToString();
                string MaWeb = gridView1.GetFocusedRowCellValue("MAWEB").ToString();
                QlyYCTcWebDTO YC = QlyYCTcWebDAO.Instance.GetYcTcWebDTO(MaNV, MaWeb);
                sglWebSite.EditValue = YC.MAWEB;
                txtMDSD.Text = YC.MDSD;
            }
            catch
            {

            }
        }

        private void btnPDPB_Click(object sender, EventArgs e)
        {
            if (idquyen >= 5)
            {
                int dem = 0;

                List<QlyYCTcWebDTO> LsYCDTOdc = new List<QlyYCTcWebDTO>();
                foreach (var item in gridView1.GetSelectedRows())
                {
                    string manv = gridView1.GetRowCellValue(item, "MANV").ToString();
                    string maweb = gridView1.GetRowCellValue(item, "MAWEB").ToString();
                    QlyYCTcWebDTO YC = QlyYCTcWebDAO.Instance.GetYcTcWebDTO(manv, maweb);
                    LsYCDTOdc.Add(YC);
                    dem++;
                }

                if (dem == 0)
                {
                    MessageBox.Show("Bạn chưa chọn yêu cầu để phê duyệt.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    // Người phê duyệt phòng ban ( QLTC phòng ban trở lên)                        
                    string NgPDPB = CommonUser.UserStatic.MANV + DateTime.Now.ToString("-dd/MM/yyyy HH:mm:ss tt");

                    int DemPD = 0;
                    foreach (QlyYCTcWebDTO item in LsYCDTOdc)
                    {                      
                        string PdPB = item.PDPB;
                        if (PdPB == "") // Chưa phê duyệt PB thì mới phê duyệt
                        {
                           QlyYCTcWebDAO.Instance.UpdatePDPB(item.MANV,item.MAWEB, NgPDPB);
                            DemPD++;
                        }
                    }
                    MessageBox.Show($"Đã phê duyệt {DemPD} yêu cầu được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DemPD = 0;
                    dem = 0;
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa đủ thẩm quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadControl();
        }

        private void btnPDIT_Click(object sender, EventArgs e)
        {
            // Chức năng của IT: Nút chức năng dùng cho phòng ban IT + quyền từ phê duyệt sơ cấp trở lên

            string PhongBan = CommonUser.UserStatic.PHONGBAN;
            if ((PhongBan == "IT" || PhongBan == "ADMIN") && idquyen >= 4)
            {
                int dem = 0;

                List<QlyYCTcWebDTO> LsYCDTOdc = new List<QlyYCTcWebDTO>();
                foreach (var item in gridView1.GetSelectedRows())
                {
                    string manv = gridView1.GetRowCellValue(item, "MANV").ToString();
                    string maweb = gridView1.GetRowCellValue(item, "MAWEB").ToString();
                    QlyYCTcWebDTO YC = QlyYCTcWebDAO.Instance.GetYcTcWebDTO(manv, maweb);
                    LsYCDTOdc.Add(YC);
                    dem++;
                }
                if (dem == 0)
                {
                    MessageBox.Show("Bạn chưa chọn yêu cầu để phê duyệt.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    // Người phê duyệt phòng IT( QLSC trở lên)

                    string NgPDIT = CommonUser.UserStatic.MANV + DateTime.Now.ToString("-dd/MM/yyyy HH:mm:ss tt");

                    int DemPD = 0;
                    foreach (QlyYCTcWebDTO item in LsYCDTOdc)
                    {
                        string PdIT = item.PDIT;
                        string PdPB = item.PDPB;
                        if (PdPB != "" && PdIT == "")
                        {
                            QlyYCTcWebDAO.Instance.UpdatePDIT(item.MANV,item.MAWEB, NgPDIT);
                            DemPD++;
                        }
                    }

                    MessageBox.Show($"Đã phê duyệt {DemPD} yêu cầu được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DemPD = 0;
                    dem = 0;
                }
            }
            else
            {
                MessageBox.Show("Bạn không đủ thẩm quyền cho chức năng này!", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadControl();
        }

        private void btnHTYC_Click(object sender, EventArgs e)
        {
            string PhongBan = CommonUser.UserStatic.PHONGBAN;
            if ((PhongBan == "IT" || PhongBan == "ADMIN") && idquyen >= 2)
            {
                int dem = 0;

                List<QlyYCTcWebDTO> LsYCDTOdc = new List<QlyYCTcWebDTO>();
                foreach (var item in gridView1.GetSelectedRows())
                {
                    string manv = gridView1.GetRowCellValue(item, "MANV").ToString();
                    string maweb = gridView1.GetRowCellValue(item, "MAWEB").ToString();
                    QlyYCTcWebDTO YC = QlyYCTcWebDAO.Instance.GetYcTcWebDTO(manv, maweb);
                    LsYCDTOdc.Add(YC);
                    dem++;
                }
                if (dem == 0)
                {
                    MessageBox.Show("Bạn chưa chọn yêu cầu để cập nhật hoàn tất.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int DemHtat = 0;
                    string NgHT = CommonUser.UserStatic.MANV + DateTime.Now.ToString("-dd/MM/yyyy HH:mm:ss tt");
                    foreach (QlyYCTcWebDTO item in LsYCDTOdc)
                    {
                        
                        string PDPB = item.PDPB;
                        string PDIT = item.PDIT;
                        string HoanTatYC = item.HTYC;
                        if (PDPB != "" && PDIT != "" && HoanTatYC == "")
                        {
                            QlyYCTcWebDAO.Instance.UpdateHTYC(item.MANV,item.MAWEB, NgHT);                       
                            DemHtat++;
                        }
                    }
                    MessageBox.Show($"Đã hoàn tất {DemHtat} yêu cầu đổi thiết bị.", "THÀNH CÔNG:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dem = 0;
                    DemHtat = 0;
                }
            }
            else
            {
                MessageBox.Show("Chưa đủ thẩm quyền cho chức năng này!", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadControl();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int dem = 0;

            List<QlyYCTcWebDTO> LsYCDTOdc = new List<QlyYCTcWebDTO>();
            foreach (var item in gridView1.GetSelectedRows())
            {
                string manv = gridView1.GetRowCellValue(item, "MANV").ToString();
                string maweb = gridView1.GetRowCellValue(item, "MAWEB").ToString();
                QlyYCTcWebDTO YC = QlyYCTcWebDAO.Instance.GetYcTcWebDTO(manv, maweb);
                LsYCDTOdc.Add(YC);
                dem++;
            }

            string PhongBan = CommonUser.UserStatic.PHONGBAN;

            if (idquyen >= 3) // Quyền từ xóa trở lên
            {
                if (PhongBan == "IT") // Phòng IT: IT không thể xóa khi đã hoàn tất
                {
                    if (idquyen >= 4)
                    {
                        if (dem == 0)
                        {
                            MessageBox.Show("Bạn chưa chọn yêu cầu để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} yêu cầu  được chọn.", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (kq == DialogResult.Yes)
                            {
                                int DemXoa = 0;
                                foreach (QlyYCTcWebDTO item in LsYCDTOdc)
                                {                                
                                    if (item.HTYC == "")
                                    {
                                        QlyYCTcWebDAO.Instance.Delete(item.MANV,item.MAWEB);
                                        DemXoa++;
                                    }
                                }
                                MessageBox.Show($"Đã xóa {DemXoa} yêu cầu được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chưa đủ thẩm quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (PhongBan == "ADMIN") // Phòng ADMIN: Xóa được mọi lúc.
                    {

                        if (dem == 0)
                        {
                            MessageBox.Show("Bạn chưa chọn yêu cầu để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} yêu cầu được chọn.", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (kq == DialogResult.Yes)
                            {
                                foreach (QlyYCTcWebDTO item in LsYCDTOdc)
                                {
                                    QlyYCTcWebDAO.Instance.Delete(item.MANV, item.MAWEB);
                                }
                                MessageBox.Show($"Đã xóa {dem} yêu cầu được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else // Với các phòng ban khác.
                    {
                        if (idquyen >= 5) // Quyền từ QLTC trở lên
                        {
                            if (dem == 0)
                            {
                                MessageBox.Show("Bạn chưa chọn yêu cầu để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} yêu cầu được chọn.", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (kq == DialogResult.Yes)
                                {
                                    int demXoa = 0;
                                    foreach (QlyYCTcWebDTO item in LsYCDTOdc)
                                    {
                                      
                                        string PdIT = item.PDIT;
                                        if (PdIT == "") // Chưa phê duyệt IT
                                        {
                                            QlyYCTcWebDAO.Instance.Delete(item.MANV, item.MAWEB);
                                            demXoa++;
                                        }
                                    }
                                    MessageBox.Show($"Đã xóa {demXoa} yêu cầu được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else // Nhân viên bình thường.
                        {

                            if (dem == 0)
                            {
                                MessageBox.Show("Bạn chưa chọn yêu cầu để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} yêu cầu được chọn.", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (kq == DialogResult.Yes)
                                {
                                    int demXoa = 0;
                                    foreach (QlyYCTcWebDTO item in LsYCDTOdc)
                                    {
                                      
                                        string PdPB = item.PDPB;
                                        if (PdPB == "") // Chưa phê duyệt phòng ban.
                                        {
                                            QlyYCTcWebDAO.Instance.Delete(item.MANV, item.MAWEB);
                                            demXoa++;
                                        }

                                    }
                                    MessageBox.Show($"Đã xóa {demXoa} yêu cầu được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Chưa đủ thẩm quyền. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadControl();
        }
    }

}