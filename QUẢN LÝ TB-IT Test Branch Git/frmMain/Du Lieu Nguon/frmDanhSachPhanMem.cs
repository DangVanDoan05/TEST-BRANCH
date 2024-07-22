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
using System.IO;
using OfficeOpenXml;
using frmMain.Quan_Ly_May_Tinh;
using static DevExpress.Utils.Svg.CommonSvgImages;
using DevExpress.XtraGrid.Columns;

namespace frmMain
{
    public partial class frmDanhSachPhanMem : DevExpress.XtraEditors.XtraForm
    {
        public frmDanhSachPhanMem()
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
            LoadCBX();
        }

        private void LoadCBX()
        {
            cbNCC.DataSource = NhaCungCapDAO.Instance.GetListNCC();
            cbNCC.DisplayMember = "MANCC";
            cbNCC.ValueMember = "MANCC";
        }

        private void CleanText()
        {
            txtMaPhanMem.Clear();
            txtTenPhanMem.Clear();
            txtLicense.Clear();
            txtGhiChu.Clear();
        }

        private void LoadData()
        {

            gridControl1.DataSource = DanhSachPhanMemDAO.Instance.GetTable();
        }

        private void LockControl(bool kt)
        {
            if (kt)
            {
                txtMaPhanMem.Enabled = false;
                txtTenPhanMem.Enabled = false;
                txtLicense.Enabled = false;
                txtGhiChu.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
                btnTaiForm.Enabled = true;
                btnNhapExcel.Enabled = true;
                btnXuatExcel.Enabled = true;
               
            }

            else
            {
                txtMaPhanMem.Enabled = true;
                txtTenPhanMem.Enabled = true;
                txtLicense.Enabled = true;
                txtGhiChu.Enabled = true;
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;
                btnTaiForm.Enabled = false;
                btnNhapExcel.Enabled = false;
                btnXuatExcel.Enabled = false;

            }
        }



        void Save()
        {
            try
            {
                if (them)
                {
                    string maPM = txtMaPhanMem.Text;
                    string tenPM = txtTenPhanMem.Text;
                    string license = txtLicense.Text;
                    string ngaymua = dtpNgayMua.Value.ToString("dd/MM/yyyy");
                    string hansd = dtpHanSuDung.Value.ToString("dd/MM/yyyy");
                    string ghichu = txtGhiChu.Text;
                    string chucnang = txtChucnang.Text;
                    string ncc = cbNCC.Text;
                    int socot = DanhSachPhanMemDAO.Instance.CheckMaPM(maPM);
                    if (socot > 0)
                    {
                        MessageBox.Show(" Mã phần mềm đã tồn tại!", "Thông Báo");
                    }
                    else
                    {
                        DialogResult kq = MessageBox.Show($"Bạn muốn thêm mã phần mềm {maPM}", "Thông Báo:", MessageBoxButtons.YesNo);
                        if (kq == DialogResult.Yes)
                        {
                            DanhSachPhanMemDAO.Instance.Insert(maPM, tenPM, license, ngaymua, hansd, ncc,chucnang, ghichu);
                            MessageBox.Show($" Thêm mã phần mềm {maPM} thành công! ");
                        }
                        them = false;
                        LoadControl();

                    }
                }
                else
                { 
                    // UPDATE THONG TIN
                    string maPM = txtMaPhanMem.Text;
                    string tenPM = txtTenPhanMem.Text;
                    string license = txtLicense.Text;
                    string ngaymua = dtpNgayMua.Value.ToString("dd/MM/yyyy");
                    string hansd = dtpHanSuDung.Value.ToString("dd/MM/yyyy");
                    string ghichu = txtGhiChu.Text;
                    string ChucNang = txtChucnang.Text;
                    string ncc = cbNCC.Text;

                    if (maPM == "")
                    {
                        MessageBox.Show($" Bạn chưa chọn mã phần mềm để thay đổi thông tin!", "Thông Báo:");
                    }
                    else
                    {
                        DialogResult kq = MessageBox.Show($"Bạn muốn sửa thông tin của mã phần mềm {maPM}", "Thông Báo:", MessageBoxButtons.YesNo);
                        if (kq == DialogResult.Yes)
                        {
                            DanhSachPhanMemDAO.Instance.Update(maPM, tenPM, license, ngaymua, hansd, ncc,ChucNang, ghichu);
                            MessageBox.Show($" Sửa thông tin mã phần mềm {maPM} thành công! ");
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Hãy chọn nhà cung cấp có trong danh sách ", "Thông Báo:");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LockControl(false);
            them = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaPhanMem.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaPhanMem.Enabled = false;
            string ma = txtMaPhanMem.Text.Trim();


            if (ma == "")
            {
                MessageBox.Show(" Bạn chưa chọn mã phần mềm để xóa! ", " Thông Báo");
            }
            else
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa các mã phần mềm đã chọn?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    int dem = 0;
                    // cho phép xóa nhiều dòng trong gridview
                    foreach (var item in gridView1.GetSelectedRows())
                    {
                        string ma1 = gridView1.GetRowCellValue(item, "MAPM").ToString();

                        DanhSachPhanMemDAO.Instance.Delete(ma1);

                        // Xóa trong cả bảng quản lý cài đặt phần mềm.                       
                        DsCaiDatDAO.Instance.DeletePM(ma1);

                        dem++;
                    }
                    MessageBox.Show($"Xóa thành công {dem} mã phần mềm được chọn.", "Thông Báo: ");
                   
                }
            }
            LoadControl();
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                txtMaPhanMem.Text = gridView1.GetFocusedRowCellValue("MAPM").ToString();
                txtTenPhanMem.Text = gridView1.GetFocusedRowCellValue("TENPM").ToString();
                txtLicense.Text = gridView1.GetFocusedRowCellValue("LICENSE").ToString();
                txtGhiChu.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();
                dtpNgayMua.Value = DateTime.Parse(gridView1.GetFocusedRowCellValue("NGAYMUA").ToString());
                dtpHanSuDung.Value = DateTime.Parse(gridView1.GetFocusedRowCellValue("HANSD").ToString());
                cbNCC.SelectedValue = gridView1.GetFocusedRowCellValue("NCC").ToString();
            }
            catch
            {
                
            }
            
        }

        // Xử lý Excell cho Form danh sách phần mềm:
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
                            gridView1.OptionsSelection.MultiSelect = false;
                          GridColumn A=  new GridColumn();
                            A.FieldName = "MAPM";
                            A.Caption = "Test";
                            gridView1.Columns.Add(A);
                            A.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;


                            gridControl1.ExportToXlsx(exportFilePath);

                            gridView1.OptionsSelection.MultiSelect = true;
                            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
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

                    string MaPM = "";
                    // câu lệnh select trả ra một bảng trắng 
                    gridControl1.DataSource = DanhSachPhanMemDAO.Instance.GetRowMaPM(MaPM);

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

       
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            LockControl(false);
            DialogResult kq = MessageBox.Show("Bạn muốn xuất danh sách phần mềm thành File Excel?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                XuatExCel();
            }
            LoadControl();
        }

        private void btnTaiForm_Click(object sender, EventArgs e)
        {
            LockControl(false);
            DialogResult kq = MessageBox.Show("Bạn muốn tải Form Excel mẫu để nhập dữ liệu?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                TaiForm();
            }
            LoadControl();
        }

        private void btnNhapExcel_Click(object sender, EventArgs e)
        {
            LockControl(false);
            frmNhapExcelDSPhanMem f = new frmNhapExcelDSPhanMem();
            f.ShowDialog();
            LoadControl();
        }

    }

}