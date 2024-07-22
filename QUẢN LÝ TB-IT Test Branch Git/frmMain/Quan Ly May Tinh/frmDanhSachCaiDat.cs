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

namespace frmMain
{
    public partial class frmDanhSachCaiDat : DevExpress.XtraEditors.XtraForm
    {
        public frmDanhSachCaiDat()
        {

            InitializeComponent();
            loadControl();
        }

        private void loadControl()
        {
            gridControl1.DataSource = DsCaiDatDAO.Instance.GetTable();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // cho phép xóa nhiều dòng trong gridview

            int dem = 0;
           
            List<int> LsTTCDdcChon = new List<int>();
            foreach (var item in gridView1.GetSelectedRows())
            {
                int id =int.Parse(gridView1.GetRowCellValue(item, "ID").ToString());
                LsTTCDdcChon.Add(id);
                dem++;
            }

            if (dem > 0)
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} thông tin cài đặt phần mềm được chọn.", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    int demXoa = 0;
                    foreach (int item in LsTTCDdcChon)
                    {

                        DsCaiDatDAO.Instance.DeleteThongTinCD(item);
                           demXoa++;
                        
                    }                   
                    MessageBox.Show($"Đã xóa {demXoa} thông tin cài đặt được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);                   
                    demXoa = 0;
                    dem = 0;
                }
                loadControl();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn thông tin cài đặt để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
               

            }
            catch
            {


            }
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

        private void btnXuatExcell_Click(object sender, EventArgs e)
        {

            DialogResult kq = MessageBox.Show("Bạn muốn xuất danh sách tồn kho thành File Excel?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                XuatExCel();
            }
            loadControl();
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

                    gridControl1.DataSource = null;

                    switch (fileExtenstion)
                    {
                        case ".xlsx":

                            //  gridColumn1.Visible = false; // Bỏ cột đầu tiên đi.
                            //   gridView1.OptionsSelection.MultiSelect = false; // Bỏ cột lựa chọn khi xuất file Excell.
                            gridControl1.ExportToXlsx(exportFilePath);

                            gridColumn1.Visible = true;
                            gridColumn1.VisibleIndex = 1;
                            gridView1.OptionsSelection.MultiSelect = true;
                            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                            loadControl();
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


        private void btnTaiForm_Click(object sender, EventArgs e)
        {
           
            DialogResult kq = MessageBox.Show("Bạn muốn tải Form Excel mẫu để nhập dữ liệu?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                TaiForm();
            }
            loadControl();
        }

       
    }
}