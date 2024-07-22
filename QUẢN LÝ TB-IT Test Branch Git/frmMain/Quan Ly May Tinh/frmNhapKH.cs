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

namespace frmMain.Quan_Ly_May_Tinh
{
    public partial class frmNhapKH : DevExpress.XtraEditors.XtraForm
    {
        public frmNhapKH()
        {
            InitializeComponent();
            LoadControl();
        }

        bool them;
        private void LoadControl()
        {

            LockControl(true);
            LoadData();
            CleanText();
        }

        private void CleanText()
        {
            txtNgay.Clear();
        }

        private void LoadData()
        {
            txtNam.Text = CommonKHBD.NamChon;
            txtNhaMay.Text= CommonKHBD.NhaMay;
            txtPB.Text = CommonKHBD.MaPBchon;         
            txtThang.Text = CommonKHBD.ThangChon;
           
                      
            sglHangMuc.Properties.DataSource = HangMucBaoTriDAO.Instance.GetTable();
            sglHangMuc.Properties.DisplayMember = "MAHM";
            sglHangMuc.Properties.ValueMember = "MAHM";


            gridControl1.DataSource = KeHoachBDDAO.Instance.GetKHNMPhongThang(txtNhaMay.Text, txtPB.Text, txtThang.Text, txtNam.Text);

        }

        private void LockControl(bool kt)
        {
            if(kt)
            {
                txtNam.Enabled = false; 
                txtNhaMay.Enabled = false; 
                txtPB.Enabled = false; 
                txtNgay.Enabled = false; 
                txtThang.Enabled = false; 
                sglHangMuc.Enabled = false; 

                btnThem.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
                btnHoanTat.Enabled = true;                           
            }
            else
            {
                txtNam.Enabled = false;
                txtNhaMay.Enabled = false;
                txtPB.Enabled = false;
                txtNgay.Enabled = true;
                txtThang.Enabled = false;
                sglHangMuc.Enabled = true;

                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;
                btnHoanTat.Enabled = true;
            }

        }

        private void searchLookUpEdit1View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            LockControl(false);
            them = true;
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

     

        private void btnXoa_Click(object sender, EventArgs e)
        {
            #region  Xóa trong kế hoạch bảo trì: Xóa trong bảng " Kế hoạch bảo trì ", Xóa trong " Lịch sử bảo trì MT " với những kế hoạch đã hoàn tất  
                                               
            // cho phép xóa nhiều dòng trong gridview

            int dem = 0;
            List<string> LsMaKHDcChon = new List<string>();

            foreach (var item in gridView1.GetSelectedRows())
            {
                string MaKH = gridView1.GetRowCellValue(item, "MAKH").ToString();
                LsMaKHDcChon.Add(MaKH);
                dem++;
            }

            if (dem > 0)
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} kế hoạch được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    foreach (string item in LsMaKHDcChon)
                    {
                        KeHoachBDDTO KehoachDTO = KeHoachBDDAO.Instance.GetKHDTO(item);
                        string NhaMay = KehoachDTO.NHAMAY;
                        string PhongBan = KehoachDTO.PB;
                        string ngay = KehoachDTO.NGAY;
                        if (ngay.Length < 2)
                        {
                            ngay = "0" + ngay;
                        }
                        string thang = KehoachDTO.THANG;
                        if (thang.Length < 2)
                        {
                            thang = "0" + thang;
                        }
                        string nam = KehoachDTO.NAM;
                        string NgayBT = ngay + "/" + thang + "/" + nam;
                        string HangMucBT = KehoachDTO.HMBT;
                        if (KehoachDTO.TRANGTHAI) // nếu trạng thái đã hoàn tất thì xóa trong bảng lịch sử bảo trì máy tính // Xóa theo nhà máy phòng ban và hạng mục
                        {
                            List<LichSuBTDTO> LsBTMT = LichSuBTDAO.Instance.GetLsBT();

                            foreach (LichSuBTDTO item1 in LsBTMT)
                            {
                                if(item1.NHAMAY==NhaMay&&item1.PB==PhongBan&&item1.NGAY==NgayBT&&item1.HMBT==HangMucBT)
                                {
                                    LichSuBTDAO.Instance.Delete(item1.MABT);
                                }
                            }
                        }
                        KeHoachBDDAO.Instance.Delete(item);

                    }
                    MessageBox.Show($"Đã xóa {dem} kế hoạch được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn kế hoạch để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadControl();
            #endregion

        }


        private void Save()
        {
            #region Chỉ lưu vào bảng " Kế hoạch bảo dưỡng "
            if (them)
            {
                // insert KEHOACHBD(MAKH, NHAMAY, PB, NGAY, THANG, NAM, HMBT, TRANGTHAI)
                string NhaMay = txtNhaMay.Text;
                string PhongBan = txtPB.Text;
                string Ngay = "";
                try
                {
                    int ngay = int.Parse(txtNgay.Text);
                    Ngay = ngay + "";
                }
                catch
                {
                    MessageBox.Show("Hãy nhập ngày dưới dạng số.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (Ngay != "")
                {
                    int NgayKH = int.Parse(Ngay);
                    if (NgayKH >= 1 && NgayKH <= 31)
                    {

                        string Thang = txtThang.Text;
                        string Nam = txtNam.Text;
                        try
                        {
                            string HangMucBT = sglHangMuc.EditValue.ToString();
                            string MaKH = NhaMay + "-" + PhongBan + "-" + Ngay + Thang + Nam + "-" + HangMucBT;
                            bool trangThai = false;
                            bool CheckExist = KeHoachBDDAO.Instance.CheckKHExistNM(MaKH);
                            if (CheckExist)
                            {
                                MessageBox.Show($"Hạng mục bảo trì '{HangMucBT}' đã có trong ngày {Ngay} ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                KeHoachBDDAO.Instance.Insert(MaKH, NhaMay, PhongBan, Ngay, Thang, Nam, HangMucBT, trangThai);

                                
                                MessageBox.Show($"Đã thêm thành công kế hoạch. ", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Hãy chọn hạng mục bảo trì trong danh sách.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Hãy nhập ngày từ 1 đến 31.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Chưa nhập ngày cần bảo dưỡng.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            them = false;
            #endregion
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            Save();
            LoadControl();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void btnHoanTat_Click(object sender, EventArgs e)
        {
            #region Thêm vào bảng " Lịch sử bảo trì máy tính" và Update trạng thái trong bảng " Kế hoạch ". 
            int dem = 0;
            List<string> LsMaKHDcChon = new List<string>();
            foreach (var item in gridView1.GetSelectedRows())
            {
                string MaKH = gridView1.GetRowCellValue(item, "MAKH").ToString();
                LsMaKHDcChon.Add(MaKH);
                dem++;
            }

            // Hoàn tất thì mới cập nhật lịch sử bảo trì.

            if (dem > 0)
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn hoàn tất {dem} kế hoạch được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    foreach (string item in LsMaKHDcChon) // Mã kế hoạch được chọn
                    {
                        // LICHSUBAOTRI(MABT, MAMT, NHAMAY, PB, NGAY, HMBT, VTSD, SOLUONG, TRANGTHAI, NGUOICN)
                        KeHoachBDDTO KehoachDTO = KeHoachBDDAO.Instance.GetKHDTO(item);
                        string NhaMay = KehoachDTO.NHAMAY;
                        string PhongBan = KehoachDTO.PB;
                        string ngay = KehoachDTO.NGAY;
                        if(ngay.Length<2)
                        {
                            ngay = "0" + ngay;
                        }
                        string thang = KehoachDTO.THANG;
                        if(thang.Length<2)
                        {
                            thang = "0" + thang;
                        }
                        string nam = KehoachDTO.NAM;
                        string Ngay = ngay + "/" + thang + "/" + nam;
                        string HangMucBT = KehoachDTO.HMBT;
                        string VTSD = "";
                        string VTTH = "";
                        string GhiChu = "";

                        TinhTrangKiemKeDTO TTKK = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(3); // tình trạng 3 Hạng mục không cần kiểm kê

                        string NguoiCN = CommonUser.UserStatic.MANV + "_" + CommonUser.UserStatic.FULLNAME;

                        // lấy ra list máy tính phòng ban, nhà máy, trừ các máy tính còn bảo hành.

                        List<QuanLyMayTinhDTO> LsMT = QuanLyMayTinhDAO.Instance.GetLsMTBaoTri(NhaMay, PhongBan);

                        foreach (QuanLyMayTinhDTO item1 in LsMT)
                        {
                            string MaMT = item1.MAMT;
                            string MaBT = MaMT+"-"+ Ngay+"-"+ HangMucBT;
                            LichSuBTDAO.Instance.Insert(MaBT, MaMT, NhaMay, PhongBan, Ngay, HangMucBT, VTSD,VTTH,TTKK.IDTTKIEMKE,TTKK.CHITIETTTKK,GhiChu, NguoiCN);
                        }                  
                        KeHoachBDDAO.Instance.UpdateTrangThai(item,true);
                    }
                    MessageBox.Show($"Đã hoàn tất {dem} kế hoạch được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn kế hoạch để hoàn tất.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadControl();
            #endregion 

        }
    }
}