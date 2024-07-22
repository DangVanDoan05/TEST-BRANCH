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
using QuanLyThietBiIT.Common;
using DevExpress.XtraGrid.Views.Grid;
using QuanLyThietBiIT.GridViewEdit;

namespace QuanLyThietBiIT
{
    public partial class frmTonLinhKien : DevExpress.XtraEditors.XtraForm
    {
        public frmTonLinhKien()
        {
            InitializeComponent();
            LoadControl();
        }

        private void LoadControl()
        {
            LoadData();
            LoadView();
        }

        private void LoadView()
        {

        }

        private void LoadData()
        {
            gridControl1.DataSource = TonLinhKienDAO.Instance.GetTable();


        }

        private void btnNhapKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmNhapKhoLK f = new frmNhapKhoLK();
            f.ShowDialog();
            LoadData();

        }

        private void btnXuatKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmXuatKhoLK f = new frmXuatKhoLK();
            f.ShowDialog();
            LoadData();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmNhapKhoLK f = new frmNhapKhoLK();
            f.ShowDialog();
            LoadData();
        }

        private void btnDatHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDatHang f = new frmDatHang();
            f.ShowDialog();
            LoadData();
        }

        private void btnCapNhat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnNhanHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmNhanHang f = new frmNhanHang();
            f.ShowDialog();
            LoadData();
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
            if ((malkTonDTO.SLTON > int.Parse(malkDTO.SLMIN)) && (malkTonDTO.SLTON < int.Parse(malkDTO.SLMAX)))
            {
                ktTon = 2;
            }
            if (malkTonDTO.SLTON >= int.Parse(malkDTO.SLMAX))
            {
                ktTon = 3;
            }
            if (malkTonDTO.DATHANG==true)
            {
                ktTon = 4;
            }
            return ktTon;
        }


        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            // string ton = view.GetRowCellDisplayText(e.RowHandle, view.Columns["SLTON"]).ToString();
            string ton = view.GetRowCellValue(e.RowHandle, view.Columns["SLTON"]).ToString();
            string dathang = view.GetRowCellValue(e.RowHandle, view.Columns["DATHANG"]).ToString();
            string ma = view.GetRowCellValue(e.RowHandle, view.Columns["MALK"]).ToString();
            int ktMaTon = CheckTon(ma);
            if (ktMaTon == 3) //lon hon max
            {
                e.Appearance.BackColor = txtTonLyTuong.BackColor;
            }
            if (ktMaTon == 1) // nho hon min
            {
                e.Appearance.BackColor = txtTonRatIT.BackColor; 
            }
            if (ktMaTon == 2)
            {
                e.Appearance.BackColor = Color.White; 
            }
            if(ktMaTon==4)
            {
                e.Appearance.BackColor = txtDangDatThem.BackColor;
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
    }



}



