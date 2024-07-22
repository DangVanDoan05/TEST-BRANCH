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
using DevExpress.XtraGrid.Views.Grid;
using System.IO;

namespace frmMain.Quan_Ly_Kho
{
    public partial class frmTonLKThuHoi : DevExpress.XtraEditors.XtraForm
    {
        public frmTonLKThuHoi()
        {
            InitializeComponent();
            LoadControl();
        }

        private void LoadControl()
        {
            LockControl();
            LoadData();
        }

        private void LoadData()
        {
            gridControl1.DataSource = TonLKThuHoiDAO.Instance.GetListMaLKTH();
        }

        private void LockControl()
        {
           
        }

        private void btnKhoaKK_Click(object sender, EventArgs e)
        {         
            //if (idquyen < 5)
            //{
            //    MessageBox.Show($"Bạn chưa đủ thẩm quyền cho chức năng này.", "Lỗi: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //else   // Từ QLTC trở lên
            //{

                // cho phép xóa nhiều dòng trong gridview
                int dem = 0;
                //  int demloi = 0;

                List<string> LsMaLKTHdcChon = new List<string>();

                foreach (var item in gridView1.GetSelectedRows())
                {
                    string ma1 = gridView1.GetRowCellValue(item, "MALK").ToString();
                    LsMaLKTHdcChon.Add(ma1);
                    dem++;
                }

                if (dem > 0)
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn kiểm kê {dem} linh kiện được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {
                        foreach (string item in LsMaLKTHdcChon) 
                        {
                            TonLKThuHoiDTO TonLKTHdto = TonLKThuHoiDAO.Instance.GetTonLKTHDTO(item);
                            int IDttKK1 = TonLKTHdto.IDTTKK;

                            if (IDttKK1 == 0) // đang không kiểm kê mới chuyển qua được kiểm kê
                            {
                                TinhTrangKiemKeDTO DangKK = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(1);  // Tình trạng 1: đang kiểm kê.
                                TonLKThuHoiDAO.Instance.UpdateTTKK(item, DangKK.IDTTKIEMKE, DangKK.CHITIETTTKK);

                                //UPDATE TÌNH TRẠNG KIỂM KÊ TẠI BẢNG THỐNG KÊ THU HỒI

                                List<ThongKeTHDTO> LsTKTH = ThongKeTHDAO.Instance.GetAllListTH();
                                foreach (ThongKeTHDTO item1 in LsTKTH)
                                {
                                    if (item1.MALK == item && item1.IDTTKIEMKE < 1 &&item1.LKORTH== "Lưu kho.")   // Đã tiêu hủy LK rồi thì không kiểm kê
                                    {
                                        ThongKeTHDAO.Instance.UpdateTTKiemKe(item1.MATKTH, DangKK.IDTTKIEMKE, DangKK.CHITIETTTKK);
                                    }
                                }
                              
                                // CẬP NHẬT TÌNH TRẠNG TRONG BẢNG LỊCH SỬ BẢO TRÌ 

                            }
                        }
                        MessageBox.Show($"Có {dem} mã linh kiện thu hồi đang trong tình trạng kiểm kê.", "Thành công! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    LoadControl();

                }

                else
                {
                    MessageBox.Show("Bạn chưa chọn mã linh kiện muốn kiểm kê.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

         //   }
            LoadControl();
        }

        private void btnHoanTatKK_Click(object sender, EventArgs e)
        {
           
            // Phải ở trạng thái đang kiểm kê mới hoàn tất đc.
          
            //if (idquyen < 5)
            //{
            //    MessageBox.Show($"Bạn chưa đủ thẩm quyền cho chức năng này.", "Lỗi: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //else   // Từ QLTC trở lên
            //{
            // cho phép xóa nhiều dòng trong gridview
            int dem = 0;
            //  int demloi = 0;

            List<string> LsMaLKTHdcChon = new List<string>();

            foreach (var item in gridView1.GetSelectedRows())
            {
                string ma1 = gridView1.GetRowCellValue(item, "MALK").ToString();
                LsMaLKTHdcChon.Add(ma1);
                dem++;
            }

            if (dem > 0)
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn bỏ kiểm kê {dem} linh kiện được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    int demCapNhat = 0;
                    foreach (string item in LsMaLKTHdcChon)
                    {
                        // Kiểm tra mã linh kiện nó phải đang trong trạng thái kiểm kê
                        TonLKThuHoiDTO TonLKTH = TonLKThuHoiDAO.Instance.GetTonLKTHDTO(item);

                        int IDttKK1 = TonLKTH.IDTTKK;
                        if (IDttKK1 == 1) //Với những mã linh kiện đang để tình trạng bằng 1
                        {

                            // Update lại thông tin bảng tồn LINH KIỆN THU HỒI.

                            TinhTrangKiemKeDTO DangKhongKK = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(0);  // Tình trạng 0: đang ko kiểm kê.
                            TonLKThuHoiDAO.Instance.UpdateTTKK(item, DangKhongKK.IDTTKIEMKE, DangKhongKK.CHITIETTTKK);
                            demCapNhat++;
                            // update thống kê nhập thu hồi. ( Khóa thống kê thu hồi.)

                            List<ThongKeTHDTO> LsTKTH = ThongKeTHDAO.Instance.GetAllListTH();
                            foreach (ThongKeTHDTO item1 in LsTKTH)
                            {
                                int IDttKK = item1.IDTTKIEMKE;
                                if (item1.MALK == item && item1.IDTTKIEMKE == 1 && item1.LKORTH == "Lưu kho.")
                                {
                                    TinhTrangKiemKeDTO d = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(2); // Tình trạng 2: hoàn tất Kiểm Kê
                                    string ChitietKK = $"Hoàn tất kiểm kê lúc {DateTime.Now.ToString("HH:mm:ss")} ngày {DateTime.Now.ToString("dd/MM/yyyy")}.";
                                    ThongKeTHDAO.Instance.UpdateHoanTatKK(item1.MATKTH, d.IDTTKIEMKE, ChitietKK);
                                }
                            }

                            // Có nên Cập nhật vào Bảng lịch sử bảo trì hay không.

                        }
                    }
                    MessageBox.Show($"Có {demCapNhat} mã linh kiện đã bỏ tình trạng kiểm kê.", "Thành công! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn mã linh kiện muốn hoàn tất kiểm kê.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // }
            LoadControl();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string MaLK = view.GetRowCellValue(e.RowHandle, view.Columns["MALK"]).ToString();
            TonLKThuHoiDTO TonLKTHdto = TonLKThuHoiDAO.Instance.GetTonLKTHDTO(MaLK);
            int IDttKK = TonLKTHdto.IDTTKK;          
            if (IDttKK == 0) 
            {
                e.Appearance.BackColor = btnKhongKK.Appearance.BackColor;
            }
            if (IDttKK == 1)
            {
                e.Appearance.BackColor =btnDangKK.Appearance.BackColor;
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
            LoadControl();
        }
    }
}