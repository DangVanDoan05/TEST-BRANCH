namespace QuanLyThietBiIT
{
    partial class frmNhanHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNhanHang));
            this.cbMaLK = new System.Windows.Forms.ComboBox();
            this.txtTenLK = new System.Windows.Forms.TextBox();
            this.txtSLnhan = new System.Windows.Forms.TextBox();
            this.dtpNgaynhanhang = new System.Windows.Forms.DateTimePicker();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbMaLK
            // 
            this.cbMaLK.FormattingEnabled = true;
            this.cbMaLK.Location = new System.Drawing.Point(188, 20);
            this.cbMaLK.Name = "cbMaLK";
            this.cbMaLK.Size = new System.Drawing.Size(336, 21);
            this.cbMaLK.TabIndex = 0;
            this.cbMaLK.SelectedValueChanged += new System.EventHandler(this.cbMaLK_SelectedValueChanged);
            // 
            // txtTenLK
            // 
            this.txtTenLK.Enabled = false;
            this.txtTenLK.Location = new System.Drawing.Point(188, 82);
            this.txtTenLK.Name = "txtTenLK";
            this.txtTenLK.Size = new System.Drawing.Size(336, 21);
            this.txtTenLK.TabIndex = 1;
            // 
            // txtSLnhan
            // 
            this.txtSLnhan.Location = new System.Drawing.Point(188, 219);
            this.txtSLnhan.Name = "txtSLnhan";
            this.txtSLnhan.Size = new System.Drawing.Size(336, 21);
            this.txtSLnhan.TabIndex = 2;
            // 
            // dtpNgaynhanhang
            // 
            this.dtpNgaynhanhang.CalendarFont = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgaynhanhang.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaynhanhang.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgaynhanhang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgaynhanhang.Location = new System.Drawing.Point(188, 152);
            this.dtpNgaynhanhang.Name = "dtpNgaynhanhang";
            this.dtpNgaynhanhang.Size = new System.Drawing.Size(336, 22);
            this.dtpNgaynhanhang.TabIndex = 3;
            // 
            // btnLuu
            // 
            this.btnLuu.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.btnLuu.Location = new System.Drawing.Point(250, 280);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(76, 44);
            this.btnLuu.TabIndex = 4;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "Mã Linh Kiện:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tên Linh Kiện:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(36, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "Ngày nhận hàng:";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(36, 221);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "Số lượng nhận:";
            // 
            // frmNhanHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 360);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.dtpNgaynhanhang);
            this.Controls.Add(this.txtSLnhan);
            this.Controls.Add(this.txtTenLK);
            this.Controls.Add(this.cbMaLK);
            this.Name = "frmNhanHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhận Hàng";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbMaLK;
        private System.Windows.Forms.TextBox txtTenLK;
        private System.Windows.Forms.TextBox txtSLnhan;
        private System.Windows.Forms.DateTimePicker dtpNgaynhanhang;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}