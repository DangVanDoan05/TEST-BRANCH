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
using OfficeOpenXml;
using System.IO;
using DTO;
using DAO;
using DevExpress.XtraGrid.Views.Grid;

namespace frmMain.Quan_Ly_May_Tinh
{
    public partial class frmNhapExcelDSPhanMem : DevExpress.XtraEditors.XtraForm
    {
        public frmNhapExcelDSPhanMem()
        {
            InitializeComponent();
            LockControl(true);

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
        List<ExcelWorksheet> Lsv;
        ExcelWorksheet worksheet;
        ExcelPackage package;
        DataTable dt = new DataTable();
        List<DanhSachPhanMemDTO> LsdsPM = new List<DanhSachPhanMemDTO>();
        List<string> MaPMloi=new List<string>();
        private void ReadFileExcel()
        {
                DataTable dt = new DataTable();
                dt.Columns.Add("MAPM");
                dt.Columns.Add("TENPM");
                dt.Columns.Add("LICENSE");
                dt.Columns.Add("NGAYMUA");
                dt.Columns.Add("HANSD");
                dt.Columns.Add("NCC");
                dt.Columns.Add("CHUCNANG");
                dt.Columns.Add("GHICHU");
            try
            {

                string ten = cbSheet.Text;
                worksheet = package.Workbook.Worksheets[ten];
                
                //   duyet tuan tu tu dong thu 2 den dong cuoi cung cua file, luu y file excel bat dau tu so 1
                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    //  biến j biểu thị cho một cột dữ liệu trong file Excell

                    int j = 2;
                    string maPM = "";
                    try
                    {
                        maPM = worksheet.Cells[i, j].Value.ToString();
                    }
                    catch
                    {

                    }
                    j++;
                    string TenPM = "";
                    try
                    {
                        TenPM = worksheet.Cells[i, j].Value.ToString();
                    }
                    catch
                    {

                    }
                    j++;
                    string License = "";
                    try
                    {
                        License = worksheet.Cells[i, j].Value.ToString();
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
                    string hansd = "";
                    try
                    {
                        hansd = worksheet.Cells[i, j].Value.ToString();
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
                    string ghichu = "";
                    try
                    {
                        ghichu = worksheet.Cells[i, j].Value.ToString();
                    }
                    catch
                    {

                    }


                    dt.Rows.Add(maPM, TenPM, License, ngaymua, hansd, ncc, ghichu);
                    DanhSachPhanMemDTO pmDTO = new DanhSachPhanMemDTO(maPM, TenPM, License, ngaymua, hansd, ncc, ghichu);
                    LsdsPM.Add(pmDTO);
                    gridControl1.DataSource = dt;
                    LockControl(false);

                }
            }
            catch
            {
                MessageBox.Show($"Lỗi chưa chọn Sheet hoặc File chọn ko phải là dạng ExCell ");
            }
        }

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

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

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
        int loi = 0, mamoi = 0;

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string maPM = view.GetRowCellValue(e.RowHandle, view.Columns["MAPM"]).ToString();

            if (MaPMloi.Contains(maPM)) // nếu List chứa linh kiện tồn
            {
                e.Appearance.BackColor = txtPMdatt.BackColor;
            }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn muốn lưu dữ liệu từ File Excell vào hệ thống! ", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                foreach (DanhSachPhanMemDTO item in LsdsPM)
                {
                    try
                    {
                        DanhSachPhanMemDAO.Instance.Insert(item.MAPM, item.TENPM, item.LICENSE, item.NGAYMUA, item.HANSD, item.NCC,item.CHUCNANG, item.GHICHU);
                        mamoi++;
                    }
                    catch
                    {
                        MaPMloi.Add(item.MAPM);
                        loi++;
                    }

                }
            }
            MessageBox.Show($"Thêm thành công {mamoi} máy tính, {loi} mã máy bị lỗi.", "Thông Báo: ");
            gridControl1.DataSource = LsdsPM;

            LockControl(true);
           

        }
    }
}