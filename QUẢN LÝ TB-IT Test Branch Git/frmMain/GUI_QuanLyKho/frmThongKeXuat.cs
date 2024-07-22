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
using QuanLyThietBiIT.GridViewEdit;
using System.IO;

namespace QuanLyThietBiIT
{
    public partial class frmThongKeXuat : DevExpress.XtraEditors.XtraForm
    {
        public frmThongKeXuat()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            gridControl1.DataSource = ThongKeXuatDAO.Instance.GetTable();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            DateTime ngbd = dtpNgayBatDau.Value;
            DateTime ngkt = dtpNgayKetThuc.Value;
            gridControl1.DataSource = ThongKeXuatDAO.Instance.GetListXuat(ngbd,ngkt);
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
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
            dtpNgayBatDau.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).Date;
            DateTime ngaycuoithang = dtpNgayBatDau.Value.Date.AddMonths(1).AddSeconds(-1);
            dtpNgayKetThuc.Value = ngaycuoithang;
            gridControl1.DataSource = ThongKeXuatDAO.Instance.GetListXuat(dtpNgayBatDau.Value, dtpNgayKetThuc.Value);

        }

        private void btnXem_Click_1(object sender, EventArgs e)
        {
            DateTime ngbd = dtpNgayBatDau.Value;
            DateTime ngkt = dtpNgayKetThuc.Value;
            gridControl1.DataSource = ThongKeXuatDAO.Instance.GetListXuat(ngbd, ngkt);
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
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
            dtpNgayBatDau.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).Date;
            DateTime ngaycuoithang = dtpNgayBatDau.Value.Date.AddMonths(1).AddSeconds(-1);
            dtpNgayKetThuc.Value = ngaycuoithang;
            gridControl1.DataSource = ThongKeXuatDAO.Instance.GetListXuat(dtpNgayBatDau.Value, dtpNgayKetThuc.Value);
        }

        private void btnXuatExcell_Click(object sender, EventArgs e)
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