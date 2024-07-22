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
using DevExpress.XtraGrid.Views.Grid ;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using frmMain.Quản_Lý_Đặt_Hàng;
using System.IO;
using frmMain.Quan_Ly_Dat_Hang;

namespace frmMain
{
    public partial class frmQuanLyDHPB : DevExpress.XtraEditors.XtraForm
    {
        public frmQuanLyDHPB()
        {
            InitializeComponent();
            LoadControl();
        }

        string MaDHdc = "";

        private void LoadControl()
        {
            LoadData();
          
            txtDaDat.Enabled = false;
            txtDaNhan.Enabled = false;
        }

        private void LoadData()
        {
            gridControl1.DataSource = QlyDonHangPBDAO.Instance.GetLsDsDatHangDaSX();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmDatHangPB f = new frmDatHangPB();
            f.ShowDialog();
            LoadControl();
        }

     

        private void btnQlyNhanHang_Click(object sender, EventArgs e)
        {
            if (MaDHdc == "")
            {
                MessageBox.Show($"Chưa chọn mã đơn hàng cần nhận. ", "Lỗi: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DHduocchon.MaDHdangchon = MaDHdc;
                frmNhanHangPB f = new frmNhanHangPB();
                f.ShowDialog();
                LoadControl();
            }

        }

        private void btnQlyBanGiao_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void btnDatHang1_Click(object sender, EventArgs e)
        {
            frmDatHangPB f = new frmDatHangPB();
            f.ShowDialog();
            LoadControl();
        }

            
        private void gridView1_CustomDrawRowIndicator_1(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridControl1_Click_1(object sender, EventArgs e)
        {

            try
            {
                MaDHdc = gridView1.GetFocusedRowCellValue("MADONHANG").ToString();
            }
            catch
            {
                MaDHdc = "";
            }

        }

      

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MaDHdc == "")
            {
                MessageBox.Show($"Chưa chọn mã đơn hàng cần xóa. ", "Lỗi: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                // cho phép xóa nhiều dòng trong gridview
                int dem = 0;
                //  int demloi = 0;

                List<string> LsMaDHdcChon = new List<string>();

                foreach (var item in gridView1.GetSelectedRows())
                {
                    string ma1 = gridView1.GetRowCellValue(item, "MADONHANG").ToString();
                    LsMaDHdcChon.Add(ma1);
                    dem++;
                }

                if (dem > 0)
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} đơn hàng được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {
                        int demXoa = 0;
                        foreach (string item in LsMaDHdcChon)
                        {
                            
                                QlyDonHangPBDAO.Instance.Delete(item);
                                demXoa++;
                            
                        }

                        if (demXoa < dem)
                        {
                            MessageBox.Show($"Đã xóa {demXoa} đơn hàng, {dem - demXoa} đơn hàng không thể xóa.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Đã xóa {dem} đơn hàng được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        demXoa = 0;
                        dem = 0;
                    }
                    LoadControl();
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn đơn hàng để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            LoadControl();
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
            DialogResult kq = MessageBox.Show("Bạn muốn xuất danh sách máy tính thành File Excel?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                XuatExCel();
            }
            LoadControl();
        }

        private void btnNhapExcell_Click(object sender, EventArgs e)
        {
            frmNhapExcellDsDH f = new frmNhapExcellDsDH();
            f.ShowDialog();
            LoadControl();
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
                            LoadControl();


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
            LoadControl();
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            // string ton = view.GetRowCellDisplayText(e.RowHandle, view.Columns["SLTON"]).ToString();
            string MaDonHang = view.GetRowCellValue(e.RowHandle, view.Columns["MADONHANG"]).ToString();
            List<QlyDonHangPBDTO> ListDsQuaHan = QlyDonHangPBDAO.Instance.GetLsQuaDuKienNhan();
            QlyDonHangPBDTO MaDHDTO = QlyDonHangPBDAO.Instance.GetDonHangDTO(MaDonHang);
            if (MaDHDTO.NGAYNHAN == "" )            //ĐÃ ĐẶT HÀNG
            {
                e.Appearance.BackColor = txtDaDat.BackColor;
            }
            if (MaDHDTO.NGAYNHAN != "" )     //ĐÃ NHẬN HÀNG
            {
                e.Appearance.BackColor = txtDaNhan.BackColor;
            }


            foreach (QlyDonHangPBDTO item in ListDsQuaHan)
            {
                if (MaDonHang == item.MADONHANG && MaDHDTO.NGAYNHAN == "" )
                {
                    e.Appearance.BackColor = txtDaDatLau.BackColor;
                }
            }
        }
    }
}