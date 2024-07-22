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
using DevExpress.XtraGrid.Views.Grid;

namespace frmMain
{
    public partial class frmThongKeXuat : DevExpress.XtraEditors.XtraForm
    {
        public frmThongKeXuat()
        {
            InitializeComponent();
            LoadControl();
        }

       
        int idquyen = CommonUser.Quyen;
        private void LoadControl()
        {
            LoadData();
        }


        private void LoadData()
        {
            gridControl1.DataSource = ThongKeXuatDAO.Instance.GetAllListXuat();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            DateTime ngbd = dtpNgayBatDau.Value;
            DateTime ngkt = dtpNgayKetThuc.Value;
            gridControl1.DataSource = ThongKeXuatDAO.Instance.GetListXuat(ngbd, ngkt);
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


        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(idquyen>=3)
            {                                            
                    // cho phép xóa nhiều dòng trong gridview
                    int dem = 0;
                    List<string> LsMaTKdcChon = new List<string>();
                    foreach (var item in gridView1.GetSelectedRows())
                    {
                        string ma1 = gridView1.GetRowCellValue(item, "MATKXUAT").ToString();
                        LsMaTKdcChon.Add(ma1);
                        dem++;
                    }

                if (dem <= 0)
                {
                    MessageBox.Show(" Bạn chưa chọn lần xuất kho muốn xóa! ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} lần xuất kho được chọn. ?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {
                        int DemXoa = 0;
                        foreach (string item in LsMaTKdcChon)
                        {
                            ThongKeXuatDTO xuatDTO = ThongKeXuatDAO.Instance.GetTKxuatDTO(item);
                            int IDttKK = xuatDTO.IDTTKIEMKE;
                            string YCKT = xuatDTO.YCKTSD;
                            if (IDttKK <= 0 && YCKT == "") // Dữ liệu chưa kiểm kê + là dữ liệu xuất kho thì mới được xóa.
                            {
                                // UPDATE LẠI SỐ LƯỢNG TỒN, Cộng GIẢ TỒN THIẾT BỊ 
                                string MaLK = xuatDTO.MALK;
                                TonLinhKienDTO TonLKDTO = TonLinhKienDAO.Instance.GetMaLKTon(MaLK);
                                int slxuat = xuatDTO.SLXUAT;
                                int slton = TonLKDTO.SLTON;
                                int sltonmoi = slton + slxuat;
                                TonLinhKienDAO.Instance.UpdateSLTON(MaLK, sltonmoi);

                                // XÓA TRONG BẢNG THỐNG KÊ xuất.
                                ThongKeXuatDAO.Instance.DeleteMaTKxuat(item);
                                DemXoa++;
                            }
                        }
                        MessageBox.Show($" Đã xóa thành công {DemXoa} lần xuất kho.", "THÀNH CÔNG!");
                    }
                }
                LoadControl();
            }
            else
            {
                MessageBox.Show(" Bạn chưa đủ thẩm quyền cho chức năng này. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;          
            string MaTKxuat = view.GetRowCellValue(e.RowHandle, view.Columns["MATKXUAT"]).ToString();
            int IDttKK = (ThongKeXuatDAO.Instance.GetTKxuatDTO(MaTKxuat)).IDTTKIEMKE;          
            if (IDttKK==0) // Chưa kiểm kê.
            {
                e.Appearance.BackColor = btnChuaKK.Appearance.BackColor;
            }
            if (IDttKK == 1) // đang kiểm kê.
            {
                e.Appearance.BackColor = btnDangKK.Appearance.BackColor;
            }
            if (IDttKK == 2) // đã kiểm kê.
            {
                e.Appearance.BackColor = btnDaKK.Appearance.BackColor;
            }

        }
    }
}
    