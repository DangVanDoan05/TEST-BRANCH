using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PhanQuyenUser
{
   public class PhanQuyenUser
    {
        private static PhanQuyenUser instance;

        public static PhanQuyenUser Instance
        {
            get { if (instance == null) instance = new PhanQuyenUser(); return PhanQuyenUser.instance; }
            private set { PhanQuyenUser.instance = value; }
        }
        private PhanQuyenUser() { }
        public DataTable CreatedTablePermission()
        {
            DataTable table_ribbon = new DataTable();
            table_ribbon.Columns.Add("id", typeof(string));
            table_ribbon.Columns.Add("parentid", typeof(string));
            table_ribbon.Columns.Add("caption", typeof(string));
            table_ribbon.Columns.Add("ispage", typeof(int));
            table_ribbon.Columns.Add("isgrouppage", typeof(int));
            table_ribbon.Columns.Add("image", typeof(byte[]));
            table_ribbon.Columns.Add("image_index", typeof(int));
            table_ribbon.Columns.Add("view", typeof(bool));
            table_ribbon.Columns.Add("add", typeof(bool));
            table_ribbon.Columns.Add("edit", typeof(bool));
            table_ribbon.Columns.Add("delete", typeof(bool));
            table_ribbon.Columns.Add("print", typeof(bool));
            table_ribbon.Columns.Add("extra", typeof(bool));
            table_ribbon.Columns.Add("username_edit", typeof(string));
            table_ribbon.Columns.Add("created_date", typeof(DateTime));
            table_ribbon.Columns["view"].DefaultValue = false;
            table_ribbon.Columns["add"].DefaultValue = false;
            table_ribbon.Columns["edit"].DefaultValue = false;
            table_ribbon.Columns["delete"].DefaultValue = false;
            table_ribbon.Columns["print"].DefaultValue = false;
            table_ribbon.Columns["extra"].DefaultValue = false;
            table_ribbon.Columns["image_index"].DefaultValue = -1;
            table_ribbon.Columns["username_edit"].DefaultValue = "";
            table_ribbon.Columns["created_date"].DefaultValue = DateTime.Now;
            return table_ribbon;
        }
    }
}
