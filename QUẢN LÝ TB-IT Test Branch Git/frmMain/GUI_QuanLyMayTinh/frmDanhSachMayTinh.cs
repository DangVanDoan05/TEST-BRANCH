
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
using QuanLyThietBiIT.DAOIT;
using QuanLyThietBiIT.DTOIT;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using OfficeOpenXml;
using Microsoft.Win32;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System.ComponentModel.Design;
using QuanLyThietBiIT.GridViewEdit;
using System.Windows.Data;

namespace QuanLyThietBiIT.GUI_QuanLyMayTinh
{
    public partial class frmDanhSachMayTinh : DevExpress.XtraEditors.XtraForm
    {
        public frmDanhSachMayTinh()
        {
            InitializeComponent();
            LoadControl();
        }
        int luu = 0;
        private void LoadControl()
        {
            LoadData();
            LoadCBX();
            LockControl(true);
            CleanText();
        }


        private void LockControl(bool kt)
        {
            if (kt)
            {
                txtMaMT.Enabled = false;
                txtDiaChiIP.Enabled = false;
                txtDcMAC.Enabled = false;
                txtNguoiSD.Enabled = false;
                txtMaTSCD.Enabled = false;
                txtGhiChu.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
                btnHuy.Enabled = false;
                btnChiTietCauHinh.Enabled = true;
                btnTaiForm.Enabled = true;
                btnNhapExcell.Enabled = true;
                btnXuatExcell.Enabled = true;

            }
            else
            {
                txtMaMT.Enabled = true;
                txtDiaChiIP.Enabled = true;
                txtDcMAC.Enabled = true;
                txtNguoiSD.Enabled = true;
                txtMaTSCD.Enabled = true;
                txtGhiChu.Enabled = true;
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = false;
                btnHuy.Enabled = true;
                btnChiTietCauHinh.Enabled = false;
                btnTaiForm.Enabled = false;
                btnNhapExcell.Enabled =false;
                btnXuatExcell.Enabled = false;

            }

        }

        private void LoadData()
        {

            gridControl1.DataSource = DanhSachMayTinhDAO.Instance.GetTable();
        }

        private void LoadCBX()
        {
            cbNCC.DataSource = NhaCungCapDAO.Instance.GetListNCC();
            cbNCC.DisplayMember = "MANCC";
            cbNCC.ValueMember = "MANCC";
            cbLoaiMT.DataSource = LoaiMayTinhDAO.Instance.GetListLoaiMT();
            cbLoaiMT.DisplayMember = "TENLOAIMT";
            cbLoaiMT.ValueMember = "TENLOAIMT";
            cbPhongBan.DataSource = PhongBanDAO.Instance.GetLsvPB();
            cbPhongBan.DisplayMember = "MAPB";
            cbPhongBan.ValueMember = "MAPB";

        }
        private void CleanText()
        {
            txtMaMT.Clear();
            txtDiaChiIP.Clear();
            txtDcMAC.Clear();
            txtNguoiSD.Clear();
            txtMaTSCD.Clear();
            txtGhiChu.Clear();
        }
        void Save()
        {

            switch(luu)
            {
                case 1: // luu khi them du lieu

                    {
                        string maMT = txtMaMT.Text.Trim();
                        string dcIP = txtDiaChiIP.Text.Trim();
                        string mac = txtDcMAC.Text.Trim();
                        string loaiMT = cbLoaiMT.SelectedValue.ToString();
                        string ncc = cbNCC.SelectedValue.ToString();
                        string phongban = cbPhongBan.SelectedValue.ToString();
                        string nguoisd = txtNguoiSD.Text;
                        string MaTSCD = txtMaTSCD.Text;
                        bool baohanh = false;
                        string ngaymua = dtpNgayMua.Value.ToString("dd/MM/yyyy");
                        string hanbh = dtpHanBaoHanh.Value.ToString("dd/MM/yyyy");
                        string ghichu = txtGhiChu.Text;
                        int socot = DanhSachMayTinhDAO.Instance.CheckMaMT(maMT);
                        if (socot == 1)
                        {
                            MessageBox.Show(" Mã máy tính đã tồn tại!", "Thông Báo");
                        }
                        else
                        {
                            TimeSpan timebh = dtpHanBaoHanh.Value - DateTime.Now;
                            int songay = timebh.Days;
                            if (songay > 0)
                            {
                                baohanh = true;
                            }
                            DialogResult kq = MessageBox.Show($"Bạn muốn thêm mã máy tính {maMT}", "Thông Báo:", MessageBoxButtons.YesNo);
                            if (kq == DialogResult.Yes)
                            {
                                DanhSachMayTinhDAO.Instance.Insert(maMT, baohanh, dcIP, mac, loaiMT, ncc, phongban, nguoisd, MaTSCD, ngaymua, hanbh, ghichu);
                                MessageBox.Show($" Thêm mã maý tính {maMT} thành công! ");
                            }


                        }
                        luu = 0;
                    }
                    break ;
                
               case 2:

                    { // UPDATE THONG TIN
                        string maMT = txtMaMT.Text.Trim();
                        string dcIP = txtDiaChiIP.Text.Trim();
                        string mac = txtDcMAC.Text.Trim();
                        string loaiMT = cbLoaiMT.SelectedValue.ToString();
                        string ncc = cbNCC.SelectedValue.ToString();
                        string phongban = cbPhongBan.SelectedValue.ToString();
                        string nguoisd = txtNguoiSD.Text;
                        string MaTSCD = txtMaTSCD.Text;
                        bool baohanh = false;
                        string ngaymua = dtpNgayMua.Value.ToString("dd/MM/yyyy");
                        string hanbh = dtpHanBaoHanh.Value.ToString("dd/MM/yyyy");
                        string ghichu = txtGhiChu.Text;
                        if (maMT == "")
                        {
                            MessageBox.Show("Bạn chưa chọn Mã Máy Tính để sửa! ", "Thông Báo:");
                        }
                        else
                        {

                            TimeSpan timebh = dtpHanBaoHanh.Value - DateTime.Now;
                            int songay = timebh.Days;
                            if (songay > 0)
                            {
                                baohanh = true;
                            }
                            DialogResult kq = MessageBox.Show($"Bạn muốn sửa thông tin của Mã máy tính {maMT}", "Thông Báo:", MessageBoxButtons.YesNo);
                            if (kq == DialogResult.Yes)
                            {
                                DanhSachMayTinhDAO.Instance.Update(maMT, baohanh, dcIP, mac, loaiMT, ncc, phongban, nguoisd, MaTSCD, ngaymua, hanbh, ghichu);

                                MessageBox.Show($" Sửa thông tin mã máy tính {maMT} thành công! ");
                            }
                        }
                        luu = 0;
                    }
                        break;
                case 3:
                    {
                       DialogResult kq= MessageBox.Show($"Bạn muốn lưu lại danh sách máy tính bên dưới vào CSDL?","Thông Báo:",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                        if(kq==DialogResult.Yes)
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("MAMT");
                            dt.Columns.Add("BAOHANH");
                            dt.Columns.Add("IP");
                            dt.Columns.Add("MAC");
                            dt.Columns.Add("LOAIMT");
                            dt.Columns.Add("NCC");
                            dt.Columns.Add("PHONGBAN");
                            dt.Columns.Add("NGUOISD");
                            dt.Columns.Add("MATSCD");
                            dt.Columns.Add("NGAYMUA");
                            dt.Columns.Add("HANBH");
                            dt.Columns.Add("GHICHU");
                            dt = data;
                            foreach (DataRow item in dt.Rows)
                            {
                                DanhSachMayTinhDTO dsmtDTO = new DanhSachMayTinhDTO(item,0);

                                string maMT = dsmtDTO.MAMT;
                                string dcIP = dsmtDTO.IP;
                                string mac = dsmtDTO.MAC;
                                string loaiMT = dsmtDTO.LOAIMT;
                                string ncc = dsmtDTO.NCC;
                                string phongban = dsmtDTO.PHONGBAN;
                                string nguoisd = dsmtDTO.NGUOISD;
                                string MaTSCD = dsmtDTO.MATSCD;
                                bool baohanh = dsmtDTO.BAOHANH;
                                string ngaymua = dsmtDTO.NGAYMUA;
                                string hanbh = dsmtDTO.HANBH;
                                string ghichu = dsmtDTO.GHICHU;
                                MessageBox.Show($"{maMT},{dcIP},{mac},{loaiMT},{ncc},{phongban},{nguoisd},{MaTSCD},{baohanh}");

                                DanhSachMayTinhDAO.Instance.Insert(maMT, baohanh, dcIP, mac, loaiMT, ncc, phongban, nguoisd, MaTSCD, ngaymua, hanbh, ghichu);
                                
                               
                               
                            }
                        }
                    }
                    break;

            }
        
        }
       private DataTable NhapExCel()
        {
            // Khai bao bien de luu duong dan file can import
            string filePath = "";
            // tao openfile dialog dde mo file excell
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            // chi loc ra cac file co dinh dang excel
            // dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";
            // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
            if (dialog.ShowDialog() != DialogResult.Cancel)
            {
                filePath = dialog.FileName;
            }
            // Nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Duong dan bao cao khong hop le.");
               
            }
            // tao ra bảng mẫu danh sach User Infor rong de hung du lieu.

            DataTable dt = new DataTable();
            dt.Columns.Add("MAMT");
            dt.Columns.Add("BAOHANH");
            dt.Columns.Add("IP");
            dt.Columns.Add("MAC");
            dt.Columns.Add("LOAIMT");
            dt.Columns.Add("NCC");
            dt.Columns.Add("PHONGBAN");
            dt.Columns.Add("NGUOISD");
            dt.Columns.Add("MATSCD");
            dt.Columns.Add("NGAYMUA");
            dt.Columns.Add("HANBH");
            dt.Columns.Add("GHICHU");
            // mo file excell
            var package = new ExcelPackage(new FileInfo(filePath));
            // ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Su dung muc dich khong thuong mai
            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
            //   duyet tuan tu tu dong thu 2 den dong cuoi cung cua file, luu y file excel bat dau tu so 1
            for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
            {
                //  biến j biểu thị cho một cột dữ liệu trong file Excell
               
                int j = 2;
                string maMT = "";
                try
                {
                     maMT = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string baohanh="";
                try
                {
                     baohanh = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string ip = "";
                try
                {
                     ip = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string mac = "";
                try
                {
                     mac = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string loaiMT = "";
                try
                {
                     loaiMT = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string ncc = "";
                try
                {
                     ncc = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string phongban = "";
                try
                {
                     phongban = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string nguoisd = "";
                try
                {
                     nguoisd = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string matscd = "";

                try
                {
                     matscd = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string ngaymua = "";
                try
                {
                     ngaymua = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string hanbh = "";
                try
                {
                    hanbh = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                j++;
                string ghichu ="";
                try
                {
                    ghichu = worksheet.Cells[i, j].Value.ToString();
                }
                catch
                {

                }
                dt.Rows.Add(maMT,baohanh,ip,mac,loaiMT,ncc,phongban,nguoisd,matscd,ngaymua,hanbh,ghichu);
                
            }
            return dt;
        }
        void XuatExCel()
        {
            using (System.Windows.Forms.SaveFileDialog saveDialog = new System.Windows.Forms.SaveFileDialog())
            {
                saveDialog.Filter = "Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                if (saveDialog.ShowDialog() != DialogResult.Cancel)
                {
                    string exportFilePath = saveDialog.FileName;
                    string fileExtenstion = new FileInfo(exportFilePath).Extension;

                    switch (fileExtenstion)
                    {
                        case ".xlsx":
                            gridControl1.ExportToXlsx(exportFilePath);
                            break;

                        default:
                            break;
                    }

                    if (File.Exists(exportFilePath))
                    {
                        try
                        {
                            //Try to open the file and let windows decide how to open it.
                            System.Diagnostics.Process.Start(exportFilePath);
                        }
                        catch
                        {
                            String msg = "The file could not be opened." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                            MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        String msg = "The file could not be saved." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                        MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        void TaiForm()
        {
            using (System.Windows.Forms.SaveFileDialog saveDialog = new System.Windows.Forms.SaveFileDialog())
            {
                saveDialog.Filter = "Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                if (saveDialog.ShowDialog() != DialogResult.Cancel)
                {
                    string exportFilePath = saveDialog.FileName;
                    string fileExtenstion = new FileInfo(exportFilePath).Extension;
                    string taikhoan = "";
                    gridControl1.DataSource = UserDAO.Instance.GetRowAccount(taikhoan);

                    switch (fileExtenstion)
                    {
                        case ".xlsx":
                            gridControl1.ExportToXlsx(exportFilePath);
                            break;

                        default:
                            break;
                    }

                    if (File.Exists(exportFilePath))
                    {
                        try
                        {
                            //Try to open the file and let windows decide how to open it.
                            System.Diagnostics.Process.Start(exportFilePath);
                        }
                        catch
                        {
                            String msg = "The file could not be opened." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                            MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        String msg = "The file could not be saved." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                        MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            LoadControl();
        }


        private void gridControl1_Click(object sender, EventArgs e)
        {
            txtMaMT.Text = gridView1.GetFocusedRowCellValue("MAMT").ToString();
            txtDiaChiIP.Text = gridView1.GetFocusedRowCellValue("IP").ToString();
            txtDcMAC.Text = gridView1.GetFocusedRowCellValue("MAC").ToString();
            txtNguoiSD.Text = gridView1.GetFocusedRowCellValue("NGUOISD").ToString();
            txtGhiChu.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();
            txtMaTSCD.Text = gridView1.GetFocusedRowCellValue("MATSCD").ToString();
            dtpNgayMua.Value = Convert.ToDateTime(gridView1.GetFocusedRowCellValue("NGAYMUA").ToString());
            dtpHanBaoHanh.Value = Convert.ToDateTime(gridView1.GetFocusedRowCellValue("HANBH").ToString());


        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            LockControl(false);
            luu=1;
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaMT.Enabled = false;
            luu = 2;
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {

            LockControl(false);
            txtMaMT.Enabled = false;
            string ma = txtMaMT.Text.Trim();
            if (ma == "")
            {
                MessageBox.Show(" Bạn chưa chọn mã máy tính để xóa! ", " Thông Báo");
            }
            else
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa mã máy tính {ma} ?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    // cho phép xóa nhiều dòng trong gridview
                    foreach (var item in gridView1.GetSelectedRows())
                    {
                        string ma1 = gridView1.GetRowCellValue(item, "MALK").ToString();
                        DanhSachMayTinhDAO.Instance.Delete(ma);
                        MessageBox.Show($"Xóa thành công mã máy tính : {ma}", "Thông Báo ");
                    }
                    //MaLinhKienDAO.Instance.DeleteTable(ma);
                    //TonLinhKienDAO.Instance.Delete(ma);
                    //MessageBox.Show($"Xóa thành công mã linh kiện: {ma}", "Thông Báo ");
                }
            }
            LoadControl();
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            Save();
            LoadControl();
        }

        private void btnCapNhat_Click_1(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        DataTable data = new DataTable();
        private void btnNhapExcell_Click(object sender, EventArgs e)
        {
            LockControl(false);
            DialogResult kq = MessageBox.Show("Bạn muốn nhập danh sách máy tính từ File Excel?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(kq==DialogResult.Yes)
            {
                data = NhapExCel();
                gridControl1.DataSource= data;
               
            }
            luu = 3;
            
        }

        private void btnXuatExcell_Click(object sender, EventArgs e)
        {
            LockControl(false);
            DialogResult kq = MessageBox.Show("Bạn muốn xuất danh sách máy tính thành File Excel?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                XuatExCel();
            }
        }

        private void btnTaiForm_Click(object sender, EventArgs e)
        {
            LockControl(false);
            DialogResult kq = MessageBox.Show("Bạn muốn tải Form Excel mẫu để nhập dữ liệu?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                TaiForm();
            }
        }







    }
 }
