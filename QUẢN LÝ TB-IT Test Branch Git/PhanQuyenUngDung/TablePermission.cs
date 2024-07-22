using DevExpress.Utils.Svg;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraRichEdit.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.ImageList;
using System.Windows.Forms;
using DTO;
using DevExpress.Images;


namespace PhanQuyenUngDung
{
    public class TablePermission
    {
        private static TablePermission instance;

        public static TablePermission Instance
        {
            get { if (instance == null) instance = new TablePermission(); return TablePermission.instance; }
            private set { TablePermission.instance = value; }
        }
        private TablePermission() { }
      
            public DataTable CreatedTablePermission()
            {
                DataTable table = new DataTable();
                table.Columns.Add("ID");
                table.Columns.Add("IDPARENT");
                table.Columns.Add("MOTA");
                return table;
            }
        public DataTable CreatedTablePermissionPQ()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("IDPARENT");
            table.Columns.Add("MOTA");
            table.Columns.Add("IDQUYEN");
            table.Columns.Add("CHITIETQUYEN");
            return table;
        }

        public DataTable CreatedTablePermissionPQwithUser()
        {
            DataTable table = new DataTable();
            table.Columns.Add("MANHANVIEN");
            table.Columns.Add("ID");
            table.Columns.Add("IDPARENT");
            table.Columns.Add("MOTA");
            table.Columns.Add("IDQUYEN");
            table.Columns.Add("CHITIETQUYEN");
            return table;
        }
        public Tuple<DataTable> GetAllTableMenu(RibbonControl ribbonControl)
        {
            DataTable table_ribbon = CreatedTablePermission();
            ArrayList visiblePages = ribbonControl.TotalPageCategory.GetVisiblePages();
            foreach (RibbonPage page in visiblePages)
            {
                var page_name = page.Name;
                var page_caption = page.Text;

                table_ribbon.Rows.Add(page_name, "0", page_caption); // duyệt từ page nên Page là cha, idparent=0

                foreach (RibbonPageGroup group in page.Groups)
                {
                    var page_group_name = group.Name;
                    var page_group_caption = group.Text;
                    if (page_group_name.Equals("ribbonPageGroup_Giaodien"))
                    {
                        break;
                    }

                    table_ribbon.Rows.Add(page_group_name, page_name, page_group_caption);

                    foreach (BarItemLink item in group.ItemLinks)
                    {
                        var item_caption = item.Caption;
                        var item_name = item.Item.Name;

                        table_ribbon.Rows.Add(item_name, page_group_name, item_caption);

                    }

                }
            }
            return Tuple.Create(table_ribbon);
        }
        public Tuple<DataTable> GetPermissionOfUser(RibbonControl ribbonControl,List<QlyPhanQuyenDTO> lsv)
        {
            DataTable table_ribbonPQ = CreatedTablePermissionPQ();
            ArrayList visiblePages = ribbonControl.TotalPageCategory.GetVisiblePages();
            foreach (RibbonPage page in visiblePages)
            {
                var page_name = page.Name;
                foreach (var item in lsv)
                {
                    if (item.ID == page.Name)
                    {
                        // ẩn page đó đi, gán page với quyền đó
                        table_ribbonPQ.Rows.Add(item.ID, "0", item.MOTA,item.IDQUYEN,item.CHITIETQUYEN);
                    }

                }

                foreach (RibbonPageGroup group in page.Groups)
                {
                    var page_group_name = group.Name;
                    foreach (var item in lsv)
                    {
                        if (item.ID == group.Name)
                        {
                            table_ribbonPQ.Rows.Add(item.ID, page_name, item.MOTA,item.IDQUYEN,item.CHITIETQUYEN);

                        }

                    }

                    foreach (BarItemLink barItemLink in group.ItemLinks)
                    {
                        foreach (var item in lsv)
                        {
                            if (item.ID == barItemLink.Item.Name)
                            {
                                table_ribbonPQ.Rows.Add(item.ID, page_group_name, item.MOTA, item.IDQUYEN,item.CHITIETQUYEN);

                            }

                        }

                     

                    }

                }
            }

            return Tuple.Create(table_ribbonPQ);
        }
        public Tuple<DataTable> GetPhanQuyenOfUser( List<QlyPhanQuyenDTO> lsv)
        {
            DataTable table_ribbonPQ = CreatedTablePermissionPQwithUser();
            foreach (QlyPhanQuyenDTO item in lsv)
            {
                table_ribbonPQ.Rows.Add(item.MANHANVIEN,item.ID,item.IDPARENT, item.MOTA, item.IDQUYEN, item.CHITIETQUYEN);
            }           
            return Tuple.Create(table_ribbonPQ);
        }
    }
}
