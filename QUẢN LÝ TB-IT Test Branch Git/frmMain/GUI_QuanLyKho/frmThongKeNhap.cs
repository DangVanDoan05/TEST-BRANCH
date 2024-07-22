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
using QuanLyThietBiIT.Common;
using QuanLyThietBiIT.DAOIT;
using QuanLyThietBiIT.DTOIT;
using QuanLyThietBiIT.GridViewEdit;
using System.IO;

namespace QuanLyThietBiIT
{
    public partial class frmThongKeNhap : DevExpress.XtraEditors.XtraForm
    {
        public frmThongKeNhap()
        {
            InitializeComponent();
        }

        private void LoadControl()
        {
            LoadData();
        }

        private void LoadData()
        {
            gridControl1.DataSource = ThongKeNhapDAO.Instance.GetTable();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            DateTime ngaybd = dtpNgaybatdau.Value;
            DateTime ngaykt = dtpNgayketthuc.Value;
            gridControl1.DataSource = ThongKeNhapDAO.Instance.GetListNhap(ngaybd, ngaykt);
        }

        private void btnXuatExcell_Click(object sender, EventArgs e)
        {
            DateTime x = Convert.ToDateTime("05/07/2023");
            DateTime y= Convert.ToDateTime("03/07/2023");
            DateTime ngayketthuc = dtpNgayketthuc.Value;
            TimeSpan time = x - y;
            int songay = time.Days;
            int sogio = time.Hours;
            MessageBox.Show($"ngay ket thuc tru bat dau la: {sogio}, ngay ket thuc la: {ngayketthuc.ToString("dd/MM/yyyy")}");

        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            int monthNow = DateTime.Now.Month;
            int monthNowNext = monthNow + 1;
            int yearNow = DateTime.Now.Year;
            if(monthNow==12)
            {
                monthNowNext = 1;
            }

            //DateTime date = new DateTime(dtpNgaybatdau.Value.Year, dtpNgaybatdau.Value.Month, 1);
            //dtpNgaybatdau.Value = date;
            //dtpNgaybatdau.Value = Convert.ToDateTime($"01/{monthNow}/{yearNow}");

            //DateTime ngaydauthangsau = Convert.ToDateTime($"01/{monthNowNext}/{yearNow}");
            dtpNgaybatdau.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).Date;
            DateTime ngaycuoithang = dtpNgaybatdau.Value.Date.AddMonths(1).AddSeconds(-1);
            dtpNgayketthuc.Value = ngaycuoithang;
            gridControl1.DataSource = ThongKeNhapDAO.Instance.GetListNhap(dtpNgaybatdau.Value, dtpNgayketthuc.Value);
           
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnXem_Click_1(object sender, EventArgs e)
        {
            DateTime ngaybd = dtpNgaybatdau.Value;
            DateTime ngaykt = dtpNgayketthuc.Value;
            gridControl1.DataSource = ThongKeNhapDAO.Instance.GetListNhap(ngaybd, ngaykt);
        }

        private void btnCapNhatTrang_Click(object sender, EventArgs e)
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
            dtpNgaybatdau.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).Date;
            DateTime ngaycuoithang = dtpNgaybatdau.Value.Date.AddMonths(1).AddSeconds(-1);
            dtpNgayketthuc.Value = ngaycuoithang;
            gridControl1.DataSource = ThongKeNhapDAO.Instance.GetListNhap(dtpNgaybatdau.Value, dtpNgayketthuc.Value);
        }

        private void btnXuatExcell_Click_1(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
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

    }
}