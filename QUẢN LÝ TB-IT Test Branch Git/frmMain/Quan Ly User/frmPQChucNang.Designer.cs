namespace frmMain.Du_Lieu_Nguon
{
    partial class frmPQChucNang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPQChucNang));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.treeList2 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnCam = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnChiXem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnThemSua = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnXoaXacNhan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnPheDuyetSC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnToanQuyenQLTC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnToanQuyenQLCC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.mnToanQuyenTGĐ = new System.Windows.Forms.ToolStripMenuItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.treeList2);
            this.layoutControl1.Controls.Add(this.treeList1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1270, 356, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1493, 572);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // treeList2
            // 
            this.treeList2.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2});
            this.treeList2.KeyFieldName = "MANV";
            this.treeList2.Location = new System.Drawing.Point(14, 14);
            this.treeList2.MinWidth = 23;
            this.treeList2.Name = "treeList2";
            this.treeList2.OptionsSelection.MultiSelectMode = DevExpress.XtraTreeList.TreeListMultiSelectMode.CellSelect;
            this.treeList2.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check;
            this.treeList2.ParentFieldName = "MAQLTT";
            this.treeList2.Size = new System.Drawing.Size(457, 544);
            this.treeList2.TabIndex = 9;
            this.treeList2.TreeLevelWidth = 21;
            this.treeList2.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeList2_AfterCheckNode);
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.AppearanceCell.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn2.AppearanceCell.Options.UseFont = true;
            this.treeListColumn2.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn2.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn2.Caption = "Họ tên nhân viên";
            this.treeListColumn2.FieldName = "FULLNAME";
            this.treeListColumn2.MinWidth = 23;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            this.treeListColumn2.Width = 87;
            // 
            // treeList1
            // 
            this.treeList1.Caption = "Danh sách chức năng";
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn3,
            this.treeListColumn1});
            this.treeList1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeList1.HorzScrollStep = 1;
            this.treeList1.Location = new System.Drawing.Point(475, 14);
            this.treeList1.MinWidth = 23;
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check;
            this.treeList1.OptionsView.ShowCaption = true;
            this.treeList1.ParentFieldName = "IDPARENT";
            this.treeList1.RowHeight = 23;
            this.treeList1.Size = new System.Drawing.Size(1004, 544);
            this.treeList1.TabIndex = 8;
            this.treeList1.TreeLevelWidth = 21;
            this.treeList1.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeList;
            this.treeList1.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeList1_AfterCheckNode);
            this.treeList1.NodeChanged += new DevExpress.XtraTreeList.NodeChangedEventHandler(this.treeList1_NodeChanged);
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            this.treeList1.TreeListMenuItemClick += new DevExpress.XtraTreeList.TreeListMenuItemClickEventHandler(this.treeList1_TreeListMenuItemClick);
            this.treeList1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseClick);
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.AppearanceCell.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn3.AppearanceCell.Options.UseFont = true;
            this.treeListColumn3.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn3.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn3.Caption = "Chức năng ";
            this.treeListColumn3.FieldName = "MOTA";
            this.treeListColumn3.MinWidth = 23;
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 0;
            this.treeListColumn3.Width = 87;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.AppearanceCell.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn1.AppearanceCell.Options.UseFont = true;
            this.treeListColumn1.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn1.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn1.Caption = "Quyền";
            this.treeListColumn1.FieldName = "CHITIETQUYEN";
            this.treeListColumn1.MinWidth = 23;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 1;
            this.treeListColumn1.Width = 87;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnCam,
            this.toolStripSeparator1,
            this.mnChiXem,
            this.toolStripSeparator2,
            this.mnThemSua,
            this.toolStripSeparator6,
            this.mnXoaXacNhan,
            this.toolStripSeparator3,
            this.mnPheDuyetSC,
            this.toolStripSeparator4,
            this.mnToanQuyenQLTC,
            this.toolStripSeparator5,
            this.mnToanQuyenQLCC,
            this.toolStripSeparator7,
            this.mnToanQuyenTGĐ});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(170, 222);
            // 
            // mnCam
            // 
            this.mnCam.Image = global::frmMain.Properties.Resources.Icon_Cam;
            this.mnCam.Name = "mnCam";
            this.mnCam.Size = new System.Drawing.Size(169, 22);
            this.mnCam.Text = "Cấm";
            this.mnCam.Click += new System.EventHandler(this.mnCam_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
            // 
            // mnChiXem
            // 
            this.mnChiXem.Image = global::frmMain.Properties.Resources.Icon_Xem;
            this.mnChiXem.Name = "mnChiXem";
            this.mnChiXem.Size = new System.Drawing.Size(169, 22);
            this.mnChiXem.Text = "Chỉ xem";
            this.mnChiXem.Click += new System.EventHandler(this.mnChiXem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(166, 6);
            // 
            // mnThemSua
            // 
            this.mnThemSua.Image = global::frmMain.Properties.Resources.Icon_ThemSua;
            this.mnThemSua.Name = "mnThemSua";
            this.mnThemSua.Size = new System.Drawing.Size(169, 22);
            this.mnThemSua.Text = "Thêm, Sửa";
            this.mnThemSua.Click += new System.EventHandler(this.mnThemSua_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(166, 6);
            // 
            // mnXoaXacNhan
            // 
            this.mnXoaXacNhan.Image = global::frmMain.Properties.Resources.Icon_Xoa_XacNhan;
            this.mnXoaXacNhan.Name = "mnXoaXacNhan";
            this.mnXoaXacNhan.Size = new System.Drawing.Size(169, 22);
            this.mnXoaXacNhan.Text = "Xóa, Xác nhận";
            this.mnXoaXacNhan.Click += new System.EventHandler(this.mnXoaXacNhan_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(166, 6);
            // 
            // mnPheDuyetSC
            // 
            this.mnPheDuyetSC.Image = global::frmMain.Properties.Resources.Icon_PheDuyet;
            this.mnPheDuyetSC.Name = "mnPheDuyetSC";
            this.mnPheDuyetSC.Size = new System.Drawing.Size(169, 22);
            this.mnPheDuyetSC.Text = "Phê duyệt sơ cấp";
            this.mnPheDuyetSC.Click += new System.EventHandler(this.mnPheDuyet_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(166, 6);
            // 
            // mnToanQuyenQLTC
            // 
            this.mnToanQuyenQLTC.Image = global::frmMain.Properties.Resources.Icon_ToanQuyen;
            this.mnToanQuyenQLTC.Name = "mnToanQuyenQLTC";
            this.mnToanQuyenQLTC.Size = new System.Drawing.Size(169, 22);
            this.mnToanQuyenQLTC.Text = "Toàn quyền QLTC";
            this.mnToanQuyenQLTC.Click += new System.EventHandler(this.mnToanQuyenQLTC_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(166, 6);
            // 
            // mnToanQuyenQLCC
            // 
            this.mnToanQuyenQLCC.Image = global::frmMain.Properties.Resources.manager_icon_129392;
            this.mnToanQuyenQLCC.Name = "mnToanQuyenQLCC";
            this.mnToanQuyenQLCC.Size = new System.Drawing.Size(169, 22);
            this.mnToanQuyenQLCC.Text = "Toàn quyền QLCC";
            this.mnToanQuyenQLCC.Click += new System.EventHandler(this.mnToanQuyenQLCC_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(166, 6);
            // 
            // mnToanQuyenTGĐ
            // 
            this.mnToanQuyenTGĐ.Image = global::frmMain.Properties.Resources.Webalys_Kameleon_pics_Boss_3_512;
            this.mnToanQuyenTGĐ.Name = "mnToanQuyenTGĐ";
            this.mnToanQuyenTGĐ.Size = new System.Drawing.Size(169, 22);
            this.mnToanQuyenTGĐ.Text = "Toàn quyền TGĐ";
            this.mnToanQuyenTGĐ.Click += new System.EventHandler(this.mnToanQuyenTGĐ_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1493, 572);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.treeList1;
            this.layoutControlItem5.Location = new System.Drawing.Point(461, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(1008, 548);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.treeList2;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(461, 548);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Icon ThongBao loi_93497.ico");
            // 
            // frmPQChucNang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1493, 572);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmPQChucNang";
            this.Text = "Phân quyền chức năng";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnCam;
        private System.Windows.Forms.ToolStripMenuItem mnChiXem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnThemSua;
        private System.Windows.Forms.ToolStripMenuItem mnXoaXacNhan;
        private System.Windows.Forms.ToolStripMenuItem mnPheDuyetSC;
        private System.Windows.Forms.ToolStripMenuItem mnToanQuyenQLTC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private System.Windows.Forms.ToolStripMenuItem mnToanQuyenQLCC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem mnToanQuyenTGĐ;
        private DevExpress.XtraTreeList.TreeList treeList2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}