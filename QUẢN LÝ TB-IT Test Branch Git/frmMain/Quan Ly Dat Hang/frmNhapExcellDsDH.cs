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
using OfficeOpenXml;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;

namespace frmMain.Quan_Ly_Dat_Hang
{
    public partial class frmNhapExcellDsDH : DevExpress.XtraEditors.XtraForm
    {
        public frmNhapExcellDsDH()
        {
            InitializeComponent();
            LockControl(true);
        }


        List<ExcelWorksheet> Lsv;
        ExcelWorksheet worksheet;
        ExcelPackage package;

        DataTable dt = new DataTable();
        List<QlyDonHangPBDTO> dsDH = new List<QlyDonHangPBDTO>();



        private void OpenFile()
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
            else
            {
                // tao ra bảng mẫu danh sach User Infor rong de hung du lieu.


                // mo file excell
                package = new ExcelPackage(new FileInfo(filePath));
                foreach (ExcelWorksheet item in package.Workbook.Worksheets)
                {

                    string a = item.Name;
                    //Thêm tên Sheet vào combobox
                    cbSheet.Items.Add(a);
                }
            }



        }
        // Chọn Sheet.
        private void ReadFileExcel()
        {
            //  QLYDONHANGPB(MADONHANG, PHONGBAN, NGAYDH, TENHANG, SLDAT, DONVI, NHAMAY, MDSD, NGAYNHAN, SLNHAN, GHICHU)

            DataTable dt = new DataTable();
            dt.Columns.Add("MADONHANG");
            dt.Columns.Add("PHONGBAN");
            dt.Columns.Add("NGAYDH");
            dt.Columns.Add("TENHANG");
            dt.Columns.Add("SLDAT");
            dt.Columns.Add("DONVI");
            dt.Columns.Add("NHAMAY");
            dt.Columns.Add("MDSD");
            dt.Columns.Add("NGAYNHAN");
            dt.Columns.Add("SLNHAN");           
            dt.Columns.Add("GHICHU");
            try
            {
                string ten = cbSheet.Text;
                worksheet = package.Workbook.Worksheets[ten];


                //   duyet tuan tu tu dong thu 2 den dong cuoi cung cua file, luu y file excel bat dau tu so 1

                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    //  biến j biểu thị cho một cột dữ liệu trong file Excell

                    //   QLYMAYTINH(ID,MAMT, IP, MAC, LOAIMT, NCC, NHAMAY, PB, NGUOISD, MATSCD, NGAYMUA, HANBH, BAOHANH, GHICHU)
                    int j = 2;
                    string MaDH = "";
                    try
                    {
                      MaDH   = worksheet.Cells[i, j].Value.ToString();
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
                    string ngaydh = "";   //  QLYDONHANGPB(MADONHANG, PHONGBAN, NGAYDH, TENHANG, SLDAT, DONVI, NHAMAY, MDSD, NGAYNHAN, SLNHAN, GHICHU)
                    try
                    {
                        ngaydh = worksheet.Cells[i, j].Value.ToString();
                    }
                    catch
                    {

                    }
                    j++;
                    string tenhang = "";
                    try
                    {
                        tenhang = worksheet.Cells[i, j].Value.ToString();
                    }
                    catch
                    {

                    }
                    j++;
                    string sldat = "";
                    try
                    {
                        sldat = worksheet.Cells[i, j].Value.ToString();
                    }
                    catch
                    {

                    }
                    j++;
                    string donvi = "";
                    try
                    {
                        donvi = worksheet.Cells[i, j].Value.ToString();
                    }
                    catch
                    {

                    }
                    j++;
                    string nhamay = ""; //  QLYDONHANGPB(MADONHANG, PHONGBAN, NGAYDH, TENHANG, SLDAT, DONVI, NHAMAY, MDSD, NGAYNHAN, SLNHAN, GHICHU)
                    try
                    {
                        nhamay = worksheet.Cells[i, j].Value.ToString();
                    }
                    catch
                    {

                    }
                    j++;
                    string mdsd  = "";
                    try
                    {
                        mdsd = worksheet.Cells[i, j].Value.ToString();
                    }
                    catch
                    {

                    }
                    j++;
                    string ngaynhan = "";
                    try
                    {
                        ngaynhan = worksheet.Cells[i, j].Value.ToString();
                    }
                    catch
                    {

                    }
                    j++;
                    string slnhan    = "";
                    try
                    {
                        slnhan = worksheet.Cells[i, j].Value.ToString();
                    }
                    catch
                    {

                    }
                    j++;
                    string ghichu = "";

                    try
                    {
                        ghichu = worksheet.Cells[i, j].Value.ToString();    
                    }
                    catch
                    {

                    }

                   // QLYDONHANGPB(MADONHANG, PHONGBAN, NGAYDH, TENHANG, SLDAT, DONVI, NHAMAY, MDSD, NGAYNHAN, SLNHAN, GHICHU)
                    dt.Rows.Add(MaDH,phongban,ngaydh,tenhang,sldat,donvi,nhamay,mdsd,ngaynhan,slnhan, ghichu);                 
                }

                foreach (DataRow item in dt.Rows)
                {
                    QlyDonHangPBDTO DHdto = new QlyDonHangPBDTO(item);
                    dsDH.Add(DHdto);
                }
                 gridControl1.DataSource = dsDH;             
                LockControl(false);
            }
            catch
            {
                MessageBox.Show($"Lỗi chưa chọn Sheet hoặc File chọn ko phải là dạng ExCell ");
            }
        }

        void LockControl(bool kt)
        {
            if (kt)
            {
                btnOpenFile.Enabled = true;
                cbSheet.Enabled = false;

                btnXem.Enabled = false;
                btnQuayLai.Enabled = true;
                btnLuu.Enabled = false;
            }
            else
            {
                btnOpenFile.Enabled = false;
                cbSheet.Enabled = true;
                btnLuu.Enabled = true;
                btnXem.Enabled = true;
                btnQuayLai.Enabled = true;
            }
        }

        int loi = 0; int mamoi;
        List<string> MaMTton = new List<string>();

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFile();
            LockControl(false);
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            LockControl(false);
            btnXem.Enabled = true;
            ReadFileExcel();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            LockControl(true);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            DialogResult kq = MessageBox.Show("Bạn muốn lưu dữ liệu từ File Excell vào hệ thống! ", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                foreach (QlyDonHangPBDTO DHdto in dsDH)
                {                   
                    bool CheckMaDHExist = QlyDonHangPBDAO.Instance.CheckDHExist(DHdto.MADONHANG);                                    
                    if (!CheckMaDHExist) 
                    {
                        // QLYDONHANGPB(MADONHANG, PHONGBAN, NGAYDH, TENHANG, SLDAT, DONVI, NHAMAY, MDSD, NGAYNHAN, SLNHAN, GHICHU)
                        QlyDonHangPBDAO.Instance.Insert(DHdto.MADONHANG,DHdto.PHONGBAN,DHdto.NGAYDH,DHdto.TENHANG,DHdto.SLDAT,DHdto.DONVI,DHdto.NHAMAY,DHdto.MDSD,DHdto.NGAYNHAN,DHdto.SLNHAN,DHdto.GHICHU);
                        mamoi++;
                    }
                    else
                    {
                        MaMTton.Add(DHdto.MADONHANG);
                        loi++;
                    }
                }
            }
            MessageBox.Show($"Thêm thành công {mamoi} đơn hàng, {loi} mã đơn hàng đã tồn tại.", "Thông báo: ");
            gridControl1.DataSource = dsDH;
            LockControl(true);
            // this.Close();
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string ma = view.GetRowCellValue(e.RowHandle, view.Columns["MADONHANG"]).ToString();

            if (MaMTton.Contains(ma)) // nếu List chứa linh kiện tồn
            {
                e.Appearance.BackColor = btnDaTonTai.Appearance.BackColor;
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }
        // List<DanhSachMayTinhDTO> MaMTmoi = new List<DanhSachMayTinhDTO>();





    }

}