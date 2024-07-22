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
using DevExpress.XtraGrid.Views.Grid;
using DAO;
using DTO;
using System.IO;

namespace frmMain
{
    public partial class frmTonLinhKien : DevExpress.XtraEditors.XtraForm
    {
        public frmTonLinhKien()
        {
            InitializeComponent();
            
            LoadControl();

        }

        string MaLKdc = "";
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
            gridControl1.DataSource = TonLinhKienDAO.Instance.GetTable();

        }
        private int CheckTon(string ma)
        {
            int ktTon = 0;
            MaLinhKienDTO malkDTO = MaLinhKienDAO.Instance.GetRowMaLK(ma);
            TonLinhKienDTO malkTonDTO = TonLinhKienDAO.Instance.GetMaLKTon(ma);
            if (malkTonDTO.SLTON <= int.Parse(malkDTO.SLMIN))
            {
                ktTon = 1;
            }
            if ((malkTonDTO.SLTON > int.Parse(malkDTO.SLMIN))) // Trường hợp 2 là trường hợp bình thường.
            {
                ktTon = 2;
            }
          
          
            return ktTon;
        }

        private void btnNhapKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(idquyen>=2) // Quyền từ thêm sửa trở lên mới đc nhập kho.
            {
                frmNhapKho f = new frmNhapKho();
                f.ShowDialog();
                LoadData();
            }
            else
            {
                MessageBox.Show("Bạn chưa đủ thẩm quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }          
        }

         // Bôi màu

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            // string ton = view.GetRowCellDisplayText(e.RowHandle, view.Columns["SLTON"]).ToString();
            string ton = view.GetRowCellValue(e.RowHandle, view.Columns["SLTON"]).ToString();         
            string ma = view.GetRowCellValue(e.RowHandle, view.Columns["MALK"]).ToString();
            TonLinhKienDTO TonLkDTO = TonLinhKienDAO.Instance.GetMaLKTon(ma);
            int IDttKK = TonLkDTO.IDTTKIEMKE;
            int ktMaTon = CheckTon(ma);           
          
            if (ktMaTon == 2) // Tồn ok: lớn hơn min 
            {
                e.Appearance.BackColor = btnTonOK.Appearance.BackColor;
            }

            if (ktMaTon == 1) // nho hon min
                {
                    e.Appearance.BackColor = btnTonRatIt.Appearance.BackColor;
                }

            if (IDttKK == 1) // Nếu đang kiểm kê.
            {
                e.Appearance.BackColor = btnDangKK.Appearance.BackColor;
            }


            //private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
            //{
            //    GridView view = sender as GridView;
            //    // string ton = view.GetRowCellDisplayText(e.RowHandle, view.Columns["SLTON"]).ToString();
            //    string ton = view.GetRowCellValue(0, view.Columns["SLTON"]).ToString();

            //    if (int.Parse(ton) == 500)
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //    }

            //if (ton > 0)
            //{
            //    e.Appearance.BackColor = Color.Red;
            //}

        }

        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

      


      

        private void btnCapNhat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                MaLKdc = gridView1.GetFocusedRowCellValue("MALK").ToString();              
            }
            catch
            {
            }
        }

        private void btnXuatKho1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (idquyen >= 2) // Quyền từ thêm sửa trở lên mới đc xuất kho.
            {
                if (MaLKdc == "")
                {
                    MessageBox.Show("Bạn chưa chọn mã linh kiện muốn xuất kho.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    TonLinhKienDTO TonLKDTO = TonLinhKienDAO.Instance.GetMaLKTon(MaLKdc);
                    int IDttKK = TonLKDTO.IDTTKIEMKE;
                    if(IDttKK==0)
                    {
                        DHduocchon.MaLKdangchon = MaLKdc;
                        frmXuatKho f = new frmXuatKho();
                        f.ShowDialog();
                        LoadData();
                        MaLKdc = "";
                    }
                    else
                    {
                        MessageBox.Show($"Không thể xuất kho do mã LK: {MaLKdc} đang kiểm kê.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa đủ thẩm quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void btnXuatExcell_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            DialogResult kq = MessageBox.Show("Bạn muốn xuất danh sách tồn kho thành File Excel?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                XuatExCel();
            }
            LoadControl();

        }

        private void btnKhoaKK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            // Nếu đang khóa rồi thì thôi 
            if (idquyen<5) 
            {
                MessageBox.Show($"Bạn chưa đủ thẩm quyền cho chức năng này.", "Lỗi: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else   // Từ QLTC trở lên
            {
                // cho phép xóa nhiều dòng trong gridview
                int dem = 0;
                //  int demloi = 0;

                List<string> LsMaLKdcChon = new List<string>();

                foreach (var item in gridView1.GetSelectedRows())
                {
                    string ma1 = gridView1.GetRowCellValue(item, "MALK").ToString();
                    LsMaLKdcChon.Add(ma1);
                    dem++;
                }

                if (dem > 0)
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn kiểm kê {dem} linh kiện được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {                     
                        foreach (string item in LsMaLKdcChon) // Mã linh kiện được chọn
                        {
                            TonLinhKienDTO TonLK = TonLinhKienDAO.Instance.GetMaLKTon(item);
                            int IDttKK1 = TonLK.IDTTKIEMKE;
                            if (IDttKK1 == 0) // đang không kiểm kê mới chuyển qua được kiểm kê
                            {
                                TinhTrangKiemKeDTO DangKK = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(1);  // Tình trạng 1: đang kiểm kê.
                                TonLinhKienDAO.Instance.UpdateTTKiemKe(item, DangKK.IDTTKIEMKE, DangKK.CHITIETTTKK);

                                //UPDATE TÌNH TRẠNG KIỂM KÊ TẠI BẢNG THỐNG KÊ NHẬP.
                                List<ThongKeNhapDTO> LsTKnhap = ThongKeNhapDAO.Instance.GetAllListNhap();
                                foreach (ThongKeNhapDTO item1 in LsTKnhap)
                                {
                                    if (item1.MALK == item && item1.IDTTKIEMKE < 1)
                                    {
                                        ThongKeNhapDAO.Instance.UpdateTTKiemKe(item1.MATKNHAP, DangKK.IDTTKIEMKE, DangKK.CHITIETTTKK);
                                    }
                                }

                                // Xử lý, Cập nhật tình trạng kiểm kê trong bảng lịch sử bảo trì.


                                // Lấy List mã bảo trì chưa được kiểm kê.

                                    // Cập nhật tình trạng khóa kiểm kê cho mã bảo trì trong bảng vật tư sử dụng
                                   
                                List<QLyVTSDDTO> Ls = QLyVTSDDAO.Instance.GetLsMaBTcanKK(item); // đúng vật tư cần kiểm kê và tình trạng chưa hoàn tất kiểm kê

                                foreach (QLyVTSDDTO item1 in Ls)
                                {
                                    // ĐANG KIỂM KÊ BẢNG VẬT TƯ SỬ DỤNG
                                    QLyVTSDDAO.Instance.UpdateTTKK(item1.MABT, DangKK.IDTTKIEMKE, DangKK.CHITIETTTKK);

                                    // ĐANG KIỂM KÊ BẢNG LỊCH SỬ BẢO TRÌ.
                                    LichSuBTDAO.Instance.UpdateTTKK(item1.MABT, DangKK.IDTTKIEMKE, DangKK.CHITIETTTKK);
                                }

                                //UPDATE TÌNH TRẠNG KIỂM KÊ TẠI BẢNG THỐNG KÊ XUẤT.

                                List<ThongKeXuatDTO> LsTKxuat = ThongKeXuatDAO.Instance.GetAllListXuat();
                                foreach (ThongKeXuatDTO item2 in LsTKxuat)
                                {
                                    if (item2.MALK == item && item2.IDTTKIEMKE < 1)
                                    {
                                        ThongKeXuatDAO.Instance.UpdateTinhTrangKK(item2.MATKXUAT, DangKK.IDTTKIEMKE, DangKK.CHITIETTTKK);
                                    }
                                }

                              

                                // XỬ LÝ TRONG BẢNG LỊCH SỬ BẢO TRÌ:

                            }
                        }                                            
                        MessageBox.Show($"Có {dem} mã linh kiện đang trong tình trạng kiểm kê.", "Thành công! ", MessageBoxButtons.OK, MessageBoxIcon.Information);                      
                    }

                    LoadControl();
                }

                else
                {
                    MessageBox.Show("Bạn chưa chọn mã linh kiện muốn kiểm kê.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            LoadControl();
        }

        private void btnHoanTatKK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //phải đang kiểm kê .
            // Phải ở trạng thái đang kiểm kê mới hoàn tất đc.
            // Chọn mã linh kiện mà chứ có phải chọn toàn bộ đâu. 
            //if (idquyen < 5)
            //{
            //    MessageBox.Show($"Bạn chưa đủ thẩm quyền cho chức năng này.", "Lỗi: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //else   // Từ QLTC trở lên
            //{
                // cho phép xóa nhiều dòng trong gridview
                int dem = 0;
                //  int demloi = 0;

                List<string> LsMaLKdcChon = new List<string>();

                foreach (var item in gridView1.GetSelectedRows())
                {
                    string ma1 = gridView1.GetRowCellValue(item, "MALK").ToString();
                    LsMaLKdcChon.Add(ma1);
                    dem++;
                }

                if (dem > 0)        
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn bỏ kiểm kê {dem} linh kiện được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {
                        foreach (string item in LsMaLKdcChon)
                        {
                            // Kiểm tra mã linh kiện nó phải đang trong trạng thái kiểm kê
                            TonLinhKienDTO TonLK = TonLinhKienDAO.Instance.GetMaLKTon(item);
                            int IDttKK1 = TonLK.IDTTKIEMKE;

                            if(IDttKK1==1) //Với những mã linh kiện đang để tình trạng bằng 1
                            {
                                // Update lại thông tin bảng tồn.

                                TinhTrangKiemKeDTO DangKK = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(0);  // Tình trạng 0: đang ko kiểm kê.
                                TonLinhKienDAO.Instance.UpdateTTKiemKe(item, DangKK.IDTTKIEMKE, DangKK.CHITIETTTKK);
                             
                                // update thống kê nhập( Khóa thống kê nhập)

                                List<ThongKeNhapDTO> LsTKnhap = ThongKeNhapDAO.Instance.GetAllListNhap();
                                foreach (ThongKeNhapDTO item1 in LsTKnhap)
                                {
                                    int IDttKK = item1.IDTTKIEMKE;
                                    if (IDttKK < 2 && item1.MALK == item)
                                    {
                                        TinhTrangKiemKeDTO d = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(2);
                                        string ChitietKK = $"Hoàn tất kiểm kê lúc {DateTime.Now.ToString("HH:mm:ss")} ngày {DateTime.Now.ToString("dd/MM/yyyy")}.";
                                        ThongKeNhapDAO.Instance.UpdateHoanTatKK(item1.MATKNHAP, d.IDTTKIEMKE, ChitietKK);

                                    }
                                }

                                // Xử lý, Cập nhật tình trạng kiểm kê trong bảng lịch sử bảo trì.


                                // Lấy List mã bảo trì chưa được kiểm kê.


                                // Cập nhật tình trạng hoàn tất cho mã bảo trì trong bảng vật tư sử dụng

                                List<QLyVTSDDTO> Ls = QLyVTSDDAO.Instance.GetLsMaBTcanHT(item);
                                
                                foreach (QLyVTSDDTO item1 in Ls)
                                {
                                    TinhTrangKiemKeDTO d1 = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(2); // Hoàn tất KK
                                    string ChitietKK = $"Hoàn tất kiểm kê lúc {DateTime.Now.ToString("HH:mm:ss")} ngày {DateTime.Now.ToString("dd/MM/yyyy")}.";

                                    // ĐANG KIỂM KÊ BẢNG VẬT TƯ SỬ DỤNG
                                    QLyVTSDDAO.Instance.UpdateTTKK(item1.MABT, d1.IDTTKIEMKE, ChitietKK);

                                    // ĐANG KIỂM KÊ BẢNG LỊCH SỬ BẢO TRÌ.
                                    LichSuBTDAO.Instance.UpdateTTKK(item1.MABT, d1.IDTTKIEMKE,ChitietKK);
                                }

                                // update thống kê xuất.

                                List<ThongKeXuatDTO> LsTKxuat = ThongKeXuatDAO.Instance.GetAllListXuat();

                                foreach (ThongKeXuatDTO item2 in LsTKxuat)
                                {
                                    int IDttKK = item2.IDTTKIEMKE;
                                    if (IDttKK < 2 && item2.MALK == item)
                                    {
                                        TinhTrangKiemKeDTO d = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(2); // Hoàn tất KK
                                        string ChitietKK = $"Hoàn tất kiểm kê lúc {DateTime.Now.ToString("HH:mm:ss")} ngày {DateTime.Now.ToString("dd/MM/yyyy")}.";
                                        ThongKeXuatDAO.Instance.UpdateTinhTrangKK(item2.MATKXUAT, d.IDTTKIEMKE, ChitietKK);
                                    }
                                }

                              
                            }
                        }
                        MessageBox.Show($"Có {dem} mã linh kiện đang trong tình trạng không kiểm kê.", "Thành công! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}