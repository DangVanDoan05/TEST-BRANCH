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
    public partial class frmThongKeNhap : DevExpress.XtraEditors.XtraForm
    {
        public frmThongKeNhap()
        {
            InitializeComponent();
            LoadControl();
        }

        string MaTkNhap = "";

        int idquyen = CommonUser.Quyen;
        private void LoadControl()
        {
            if(idquyen>=1) 
            {
                LoadData();
            }        
        }

        private void LoadData()
        {
            gridControl1.DataSource = ThongKeNhapDAO.Instance.GetAllListNhap();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            DateTime ngaybd = dtpNgaybatdau.Value;
            DateTime ngaykt = dtpNgayketthuc.Value;
            gridControl1.DataSource = ThongKeNhapDAO.Instance.GetListNhap(ngaybd, ngaykt);
        }

        private void btnXuatExcell_Click(object sender, EventArgs e)
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

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                MaTkNhap = gridView1.GetFocusedRowCellValue("MATKNHAP").ToString();
               
            }
            catch
            {

            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Chỉ cho phép xóa được trong ngày , xóa thì trừ đi tồn, nếu tồn bằng không thì xóa trong bảng tồn.
            // Đã có xuất kho sau đó thì không được phép xóa.
            // Xóa đi mà tồn về âm: ( Xử lý sau) Dựa vào mã TK nhập, có dữ liệu về thời gian.



            if(idquyen>=3)
            {
                // cho phép xóa nhiều dòng trong gridview
                int dem = 0;
                List<string> LsMaTKdcChon = new List<string>();
                foreach (var item in gridView1.GetSelectedRows())
                {
                    string ma1 = gridView1.GetRowCellValue(item, "MATKNHAP").ToString();
                    LsMaTKdcChon.Add(ma1);
                    dem++;
                }

                if (dem<=0)
                {
                    MessageBox.Show(" Bạn chưa chọn lần nhập kho muốn xóa! ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {                
                    DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} lần nhập kho được chọn. ?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {
                        int DemXoa = 0;

                        foreach (string item in LsMaTKdcChon)
                        {
                            ThongKeNhapDTO nhapDTO = ThongKeNhapDAO.Instance.GetTKnhapDTO(item);
                            string MaLK1 = nhapDTO.MALK;
                            string NgayNhap = nhapDTO.NGAYNHAP;
                            int IDttKK = nhapDTO.IDTTKIEMKE;

                            // Kiểm tra có dữ liệu xuất kho từ ngày đó chưa===> Chưa có lần xuất kho cho đến khi có lần nhập tiếp theo thì được phép xóa
                            bool CheckXuatKho = ThongKeNhapDAO.Instance.CheckExistXuatKho(MaLK1,NgayNhap);

                            if (IDttKK == 0&&!CheckXuatKho) // Dữ liệu chưa kiểm kê thì mới có thể xóa.
                            {
                                // UPDATE LẠI SỐ LƯỢNG TỒN, TRỪ GIẢ TỒN THIẾT BỊ, TRỪ trả lại số lượng nhập kho 

                                string MaLK = nhapDTO.MALK;
                                TonLinhKienDTO TonLKDTO = TonLinhKienDAO.Instance.GetMaLKTon(MaLK);
                                int slnhap = nhapDTO.SLNHAP;
                                int slton = TonLKDTO.SLTON;
                                int sltonmoi = slton - slnhap;
                                TonLinhKienDAO.Instance.UpdateSLTON(MaLK, sltonmoi);

                                // XÓA TRONG BẢNG THỐNG KÊ NHẬP.
                                ThongKeNhapDAO.Instance.Delete(item);

                                DemXoa++;
                            }
                            
                        }

                        MessageBox.Show($" Đã xóa thành công {DemXoa} lần nhập kho.", "THÀNH CÔNG!");
                    }
                }
                LoadControl();
            }
            else
            {
                MessageBox.Show($" Bạn chưa đủ thẩm quyền cho chức năng này. ", "Lỗi:",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
           
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string MaTKnhap = view.GetRowCellValue(e.RowHandle, view.Columns["MATKNHAP"]).ToString();
            int IDttKK = (ThongKeNhapDAO.Instance.GetTKnhapDTO(MaTKnhap)).IDTTKIEMKE;
            if (IDttKK ==0) // Chưa kiểm kê.
            {
                e.Appearance.BackColor = btnChuaKK.Appearance.BackColor;
            }
            if(IDttKK == 1) // Đang kiểm kê.
            {
                e.Appearance.BackColor = btnDangKK.Appearance.BackColor;
            }
            if (IDttKK == 2) // Đã kiểm kê.
            {
                e.Appearance.BackColor = btnDaKK.Appearance.BackColor;
            }
        }
    }
}