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
using System.IO;
using DevExpress.XtraGrid.Columns;

namespace frmMain.Du_Lieu_Nguon
{
    public partial class frmDsWebSite : DevExpress.XtraEditors.XtraForm
    {
        public frmDsWebSite()
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
            CleanText();
        }
        void CleanText()
        {
            txtMaWeb.Clear();
            txtLinkWeb.Clear();
            txtGhiChu.Clear();
        }
        private void LockControl(bool kt)
        {
            if (kt)
            {
                txtMaWeb.Enabled = false;
                txtLinkWeb.Enabled = false;
                txtGhiChu.Enabled = false;

                btnThem.Enabled = true;             
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
            }
            else
            {
                txtMaWeb.Enabled = true;
                txtLinkWeb.Enabled = true;
                txtGhiChu.Enabled = true;

                btnThem.Enabled = false;              
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;
            }
        }

        private void LoadData()
        {
          
               
                gridControl1.DataSource = DsWebDAO.Instance.GetTable();
            
        }
        void Save()
        {
            if (them)
            {

                string MaWeb = txtMaWeb.Text.Trim();
                string TenWeb = txtLinkWeb.Text.Trim();
                string ghichu = txtGhiChu.Text.Trim();

                if (MaWeb != "")
                {
                    bool CheckWebExits = DsWebDAO.Instance.CheckExist(MaWeb);
                    if (CheckWebExits)
                    {
                        MessageBox.Show($" Mã trang Web {MaWeb} đã tồn tại", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                       
                            DsWebDAO.Instance.Insert(MaWeb, TenWeb, ghichu);
                            MessageBox.Show($" Đã thêm mã trang Web {MaWeb} ", "THÀNH CÔNG!");
                        
                    }
                    them = false;
                }
                else
                {
                    MessageBox.Show($"Mã trang Web không được phép để trống.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                string MaWeb = txtMaWeb.Text.Trim();
                string TenWeb = txtLinkWeb.Text.Trim();
                string ghichu = txtGhiChu.Text.Trim();


                DsWebDAO.Instance.Update(MaWeb, TenWeb, ghichu);
                MessageBox.Show($"Đã sửa thông tin mã trang Web {MaWeb}", "THÀNH CÔNG!");
                
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LockControl(false);
            them = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
           
                LockControl(false);
                txtMaWeb.Enabled = false;
                                          
                    // cho phép xóa nhiều dòng trong gridview
                    int dem = 0;
                    List<string> LsBPdcChon = new List<string>();
                    foreach (var item in gridView1.GetSelectedRows())
                    {
                        string ma1 = gridView1.GetRowCellValue(item, "MAWEB").ToString();
                        LsBPdcChon.Add(ma1);
                        dem++;
                    }
                    if(dem>0)
                    {
                        DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} trang Web được chọn. ?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (kq == DialogResult.Yes)
                        {
                            foreach (string item in LsBPdcChon)
                            {
                                DsWebDAO.Instance.Delete(item);
                            }
                            MessageBox.Show($" Đã xóa thành công {dem} mã trang Web.", "THÀNH CÔNG!");
                        }
                    }
                    else
                    {
                        MessageBox.Show(" Bạn chưa chọn trang web để xóa! ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            GridColumn A = new GridColumn();
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

        private void btnNhapExcell_Click(object sender, EventArgs e)
        {
            //LockControl(false);
            //frmNhapExcelDSPhanMem f = new frmNhapExcelDSPhanMem();
            //f.ShowDialog();
            //LoadControl();
        }

        private void btnXuatExcell_Click(object sender, EventArgs e)
        {
            LockControl(false);
            DialogResult kq = MessageBox.Show("Bạn muốn xuất danh sách phần mềm thành File Excel?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                XuatExCel();
            }
            LoadControl();
        }
    }
}