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
using DAO;
using DTO;
using DevExpress.XtraGrid.Views.Grid;

namespace frmMain
{
    public partial class frmNhapExcelDSMayTinh : DevExpress.XtraEditors.XtraForm
    {
        public frmNhapExcelDSMayTinh()
        {
            InitializeComponent();
            LockControl(true);          
        }

        List<ExcelWorksheet> Lsv;
        ExcelWorksheet worksheet;
        ExcelPackage package;
        DataTable dt =new DataTable();

        
        

        List<QuanLyMayTinhDTO> dsMT = new List<QuanLyMayTinhDTO>();



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
          

            DataTable dt = new DataTable();
            dt.Columns.Add("MAMT");           
            dt.Columns.Add("IP");
            dt.Columns.Add("MAC");
            dt.Columns.Add("DOMAIN");
            dt.Columns.Add("LOAIMT");
            dt.Columns.Add("NCC");
            dt.Columns.Add("NHAMAY");
            dt.Columns.Add("PB");
            dt.Columns.Add("NGUOISD");
            dt.Columns.Add("MATSCD");
            dt.Columns.Add("NGAYMUA");
            dt.Columns.Add("HANBH");
            dt.Columns.Add("BAOHANH");
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
                    string maMT = "";
                    try
                    {
                        maMT = worksheet.Cells[i, j].Value.ToString();
                    }
                    catch
                    {

                    }
                    j++;
                    string baohanh = "";
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
                    string domain = "";
                    try
                    {
                        domain = worksheet.Cells[i, j].Value.ToString();
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
                    string nhamay = "";
                    try
                    {
                        nhamay = worksheet.Cells[i, j].Value.ToString();
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
                        matscd = worksheet.Cells[i, j].Value.ToString();    //   QLYMAYTINH(ID,MAMT, IP, MAC, LOAIMT, NCC, NHAMAY, PB, NGUOISD, MATSCD, NGAYMUA, HANBH, BAOHANH, GHICHU)
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
                    string ghichu = "";
                    try
                    {
                        ghichu = worksheet.Cells[i, j].Value.ToString();
                    }
                    catch
                    {

                    }
                    bool bh1 = false;
                    if (baohanh == "" || baohanh == "False")
                    {
                        bh1 = false;
                    }
                    else
                    {
                        bh1 = true;
                    }

                  
                    dt.Rows.Add(maMT, ip, mac, loaiMT, ncc, nhamay ,phongban, nguoisd, matscd, ngaymua, hanbh,baohanh ,ghichu);
                    QuanLyMayTinhDTO mt = new QuanLyMayTinhDTO(maMT, bh1, ip, mac,domain, loaiMT, ncc,nhamay ,phongban, nguoisd, matscd, ngaymua, hanbh, ghichu);
                    dsMT.Add(mt);
                    
                }

                //  gridControl1.DataSource = dt;
                gridControl1.DataSource = dsMT;
                LockControl(false);
            }
            catch
            {
                MessageBox.Show($"Lỗi chưa chọn Sheet hoặc File chọn ko phải là dạng ExCell ");
            }
        }
        private void btnOpenFile_Click(object sender, EventArgs e)
        {

            OpenFile();
            LockControl(false);

        }
      
        // Chọn Sheet:
        void LockControl(bool kt)
        {
            if(kt)
            {
                btnOpenFile.Enabled = true;
                cbSheet.Enabled = false;
                
                btnXem.Enabled = false;
                btnQuayLai.Enabled =true;
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



        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        int loi = 0; int mamoi;
        List<string> MaMTton = new List<string>();
       // List<DanhSachMayTinhDTO> MaMTmoi = new List<DanhSachMayTinhDTO>();

      

     
      
        private void btnXem_Click_1(object sender, EventArgs e)
        {
            LockControl(false);
            btnXem.Enabled = true;
            ReadFileExcel();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            LockControl(true);
        }

        private void btnOpenFile_Click_1(object sender, EventArgs e)
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

        private void btnQuayLai_Click_1(object sender, EventArgs e)
        {
            LockControl(true);
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn muốn lưu dữ liệu từ File Excell vào hệ thống! ", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                foreach (QuanLyMayTinhDTO item in dsMT)
                {

                    bool CheckMaMTExist = QuanLyMayTinhDAO.Instance.CheckMaMTExist(item.MAMT);
                    if(!CheckMaMTExist)
                    {
                        QuanLyMayTinhDAO.Instance.Insert(item.MAMT, item.IP, item.MAC, item.DOMAIN, item.LOAIMT, item.NCC, item.NHAMAY, item.PB, item.NGUOISD, item.MATSCD, item.NGAYMUA, item.HANBH, item.BAOHANH, item.GHICHU);
                        mamoi++;
                    }
                    else
                    {
                        MaMTton.Add(item.MAMT);
                        loi++;
                    }
                   

                }
            }
            MessageBox.Show($"Thêm thành công {mamoi} máy tính, {loi} mã máy đã tồn tại.", "Thông Báo: ");
            gridControl1.DataSource = dsMT;

            LockControl(true);
           // this.Close();
        }

        private void gridView1_RowCellStyle_1(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string ma = view.GetRowCellValue(e.RowHandle, view.Columns["MAMT"]).ToString();

            if (MaMTton.Contains(ma)) // nếu List chứa linh kiện tồn
            {
                e.Appearance.BackColor =lblCanhBao.BackColor;
            }
        }

        private void gridView1_CustomDrawRowIndicator_1(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }
    }
}