namespace frmMain.Quan_Ly_May_Tinh
{
    partial class frmKHBaoDuongMT
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
            this.pnlDD1 = new System.Windows.Forms.Panel();
            this.pnlDD2 = new System.Windows.Forms.Panel();
            this.dtpNam = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLapKH = new DevExpress.XtraEditors.SimpleButton();
            this.btnHoantat = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlDDK = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlDD1
            // 
            this.pnlDD1.Location = new System.Drawing.Point(1, 35);
            this.pnlDD1.Name = "pnlDD1";
            this.pnlDD1.Size = new System.Drawing.Size(1382, 194);
            this.pnlDD1.TabIndex = 0;
            // 
            // pnlDD2
            // 
            this.pnlDD2.Location = new System.Drawing.Point(1, 235);
            this.pnlDD2.Name = "pnlDD2";
            this.pnlDD2.Size = new System.Drawing.Size(1382, 186);
            this.pnlDD2.TabIndex = 1;
            // 
            // dtpNam
            // 
            this.dtpNam.CustomFormat = "yyyy";
            this.dtpNam.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNam.Location = new System.Drawing.Point(66, 10);
            this.dtpNam.Name = "dtpNam";
            this.dtpNam.Size = new System.Drawing.Size(79, 22);
            this.dtpNam.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Năm:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLapKH
            // 
            this.btnLapKH.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnLapKH.Appearance.Options.UseBackColor = true;
            this.btnLapKH.Location = new System.Drawing.Point(439, 9);
            this.btnLapKH.Name = "btnLapKH";
            this.btnLapKH.Size = new System.Drawing.Size(246, 23);
            this.btnLapKH.TabIndex = 5;
            this.btnLapKH.Text = "Đã lập kế hoạch";
            // 
            // btnHoantat
            // 
            this.btnHoantat.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnHoantat.Appearance.Options.UseBackColor = true;
            this.btnHoantat.Location = new System.Drawing.Point(691, 9);
            this.btnHoantat.Name = "btnHoantat";
            this.btnHoantat.Size = new System.Drawing.Size(299, 23);
            this.btnHoantat.TabIndex = 6;
            this.btnHoantat.Text = "Đã hoàn tất";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(322, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "Chú thích:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlDDK
            // 
            this.pnlDDK.Location = new System.Drawing.Point(1, 427);
            this.pnlDDK.Name = "pnlDDK";
            this.pnlDDK.Size = new System.Drawing.Size(1382, 202);
            this.pnlDDK.TabIndex = 9;
            // 
            // frmKHBaoDuongMT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 633);
            this.Controls.Add(this.pnlDDK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnHoantat);
            this.Controls.Add(this.btnLapKH);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpNam);
            this.Controls.Add(this.pnlDD2);
            this.Controls.Add(this.pnlDD1);
            this.Name = "frmKHBaoDuongMT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kế hoạch bảo dưỡng ";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDD1;
        private System.Windows.Forms.Panel pnlDD2;
        private System.Windows.Forms.DateTimePicker dtpNam;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnLapKH;
        private DevExpress.XtraEditors.SimpleButton btnHoantat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlDDK;
    }
}