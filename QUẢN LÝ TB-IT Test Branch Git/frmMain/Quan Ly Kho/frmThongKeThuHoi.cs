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

namespace frmMain.Quan_Ly_Kho
{
    public partial class frmThongKeThuHoi : DevExpress.XtraEditors.XtraForm
    {
        public frmThongKeThuHoi()
        {
            InitializeComponent();
            LoadControl();
        }

        int idquyen = CommonUser.Quyen;
        private void LoadControl()
        {
            // Dữ liệu đã được kiểm kê rồi thì không được phép xóa.( Chỉ có ADMIN mới được phép xóa)
            // Dữ liệu chưa được kiểm kê thì có thể xóa và cộng trả lại vào tồn kho
            // Kiểm kê xong mới được phép tiêu hủy

            gridControl1.DataSource = ThongKeTHDAO.Instance.GetAllListTH();
        }

      

        private void btnLoc_Click(object sender, EventArgs e)
        {
            DateTime ngaybd = dtpNgayBD.Value;
            DateTime ngaykt = dtpNgayKT.Value;
            gridControl1.DataSource = ThongKeTHDAO.Instance.GetListTH(ngaybd, ngaykt);
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

        private void btnHuyLK_Click(object sender, EventArgs e)
        {
            // Chỉ cho phép xóa được trong ngày , xóa thì trừ đi tồn, nếu tồn bằng không thì xóa trong bảng tồn.
            #region Hủy linh kiện(Tôi tính sẽ hủy như thé nay : từ a-b-c-d)

            //if (idquyen >= 3)
            //{

            // cho phép xóa nhiều dòng trong gridview

            int dem = 0;
            List<string> LsMaTKTHdcChon = new List<string>();
            foreach (var item in gridView1.GetSelectedRows())
            {
                string ma1 = gridView1.GetRowCellValue(item, "MATKTH").ToString();
                LsMaTKTHdcChon.Add(ma1);
                dem++;
            }

            if (dem <= 0)
            {
                MessageBox.Show(" Bạn chưa chọn linh kiện muốn tiêu hủy. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn cập nhật tình trạng tiêu hủy cho {dem} linh kiện được chọn.?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    int DemHuy = 0;
                    foreach (string item in LsMaTKTHdcChon)
                    {
                       

                        // Update số lượng tồn trong bảng  TỒN THU HÔI ===> Khi Hủy thì sẽ trừ đi lượng tồn thu hồi.
                        // Kiểm kê rồi vẫn có thể hủy và Update vào bảng số lượng tồn. 
                        ThongKeTHDTO THlkDTO = ThongKeTHDAO.Instance.GetTKTHDTO(item);
                        int idTTKK = THlkDTO.IDTTKIEMKE;

                        if (idTTKK != 1)  // Đang Kiểm kê thì không thể Hủy linh  kiện được
                        {
                            string MaLKTH = THlkDTO.MALK;
                            int SLTH = THlkDTO.SLTH;

                            int TonTHLK = TonLKThuHoiDAO.Instance.GetTonLKTHDTO(MaLKTH).SLTON;
                            TonLKThuHoiDAO.Instance.UpdateSLTON(MaLKTH, TonTHLK - SLTH);

                            // Update tình trạng đã hủy trong bảng thống kê thu hồi.

                            ThongKeTHDAO.Instance.UpdateTieuHuy(item, "Đã tiêu hủy.");

                            // Update tình trạng 'đã hủy ' vào bảng Quản lý vật tư thu hồi.

                            ThongKeTHDTO THdto = ThongKeTHDAO.Instance.GetTKTHDTO(item);
                            string MaTKTH = THdto.MATKTH;
                            string MaLK = THdto.MALK;
                            int dodaicatdi = MaLK.Length + 1;
                            int dodaigiulai = MaTKTH.Length - dodaicatdi;
                            // Mã bảo trì bằng mã thống kê thu hồi trừ đi mã linh kiện.
                            string MaBT = MaTKTH.Substring(0, dodaigiulai);
                            MessageBox.Show(MaBT);
                            // Cắt mã linh kiện cuối cùng để lấy ra mã bảo trì

                            QLyVTTHDAO.Instance.UpdateTT(MaBT, MaLK, "Đã tiêu hủy.");
                            DemHuy++;
                        }

                    }

                    MessageBox.Show($" Đã cập nhật hủy  {DemHuy} linh kiện thu hồi được chọn.", "THÀNH CÔNG!",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            LoadControl();
            //}
            //else
            //{
            //    MessageBox.Show($" Bạn chưa đủ thẩm quyền cho chức năng này. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            #endregion



        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;        
            string MaTKTH = view.GetRowCellValue(e.RowHandle, view.Columns["MATKTH"]).ToString();           
            ThongKeTHDTO ThongKeTHDTO = ThongKeTHDAO.Instance.GetTKTHDTO(MaTKTH);
            int IDttKK = ThongKeTHDTO.IDTTKIEMKE;
            string TinhTrangHuy = ThongKeTHDTO.LKORTH;

            if (IDttKK < 2) // Chưa hoàn tất kiểm kê.
            {
                e.Appearance.BackColor = btnChuaKK.Appearance.BackColor;
                if (IDttKK == 1) // Đang Kiểm Kê.
                {
                    e.Appearance.BackColor = btnDangKK.Appearance.BackColor;
                }
            }
            else 
            {
                e.Appearance.BackColor = btnDaKK.Appearance.BackColor;              
            }
            if (TinhTrangHuy == "Đã tiêu hủy.")
            {
                e.Appearance.BackColor = btnDaTH.Appearance.BackColor;
            }

        }
    }
}