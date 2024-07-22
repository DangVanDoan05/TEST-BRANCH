using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThietBiIT.GridViewEdit
{
   public class ColumSTT
    {
        private static ColumSTT instance;

        internal static ColumSTT Instance
        {
            get { if (instance == null) instance = new ColumSTT(); return ColumSTT.instance; }
            private set { ColumSTT.instance = value; }
        }
        private ColumSTT() { }
        public void CustomDrawRowIndicator(RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0) return;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
}
