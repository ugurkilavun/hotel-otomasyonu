namespace hotel_otomasyonu
{
    partial class customer_reservation_exit_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(customer_reservation_exit_form));
            button_rezarvasyon_sorgula = new Button();
            dataGridView_rezervasyon_cikisi = new DataGridView();
            groupBox2 = new GroupBox();
            groupBox9 = new GroupBox();
            textBox_musteri_cikisi_sorgula_tc_no = new TextBox();
            panel3 = new Panel();
            button_ucreti_hesapla = new Button();
            groupBoxr = new GroupBox();
            textBox_rezervasyon_cikisi_rezervasyon_no = new TextBox();
            groupBox1 = new GroupBox();
            textBox_rezervasyon_cikisi_personeladsoyad = new TextBox();
            groupBox3 = new GroupBox();
            textBox_rezervasyon_cikisi_adsoyad = new TextBox();
            groupBox4 = new GroupBox();
            textBox_rezervasyon_cikisi_oda_no = new TextBox();
            groupBox5 = new GroupBox();
            textBox_rezervasyon_cikisi_giris_tarihi = new TextBox();
            groupBox6 = new GroupBox();
            textBox_rezervasyon_cikisi_cikisi_rezervsayon_durumu = new TextBox();
            groupBox7 = new GroupBox();
            textBox_rezervasyon_cikisi_cikis_tarihi = new TextBox();
            groupBox8 = new GroupBox();
            textBox_rezervasyon_cikisi_ucret = new TextBox();
            label1 = new Label();
            label2 = new Label();
            button_rezervsayon_cikisi = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView_rezervasyon_cikisi).BeginInit();
            groupBox2.SuspendLayout();
            groupBox9.SuspendLayout();
            groupBoxr.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox8.SuspendLayout();
            SuspendLayout();
            // 
            // button_rezarvasyon_sorgula
            // 
            button_rezarvasyon_sorgula.BackColor = Color.FromArgb(64, 64, 64);
            button_rezarvasyon_sorgula.Cursor = Cursors.Hand;
            button_rezarvasyon_sorgula.FlatAppearance.BorderSize = 0;
            button_rezarvasyon_sorgula.FlatStyle = FlatStyle.Flat;
            button_rezarvasyon_sorgula.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button_rezarvasyon_sorgula.ForeColor = Color.White;
            button_rezarvasyon_sorgula.Location = new Point(376, 38);
            button_rezarvasyon_sorgula.Name = "button_rezarvasyon_sorgula";
            button_rezarvasyon_sorgula.Size = new Size(86, 38);
            button_rezarvasyon_sorgula.TabIndex = 1;
            button_rezarvasyon_sorgula.Text = "Sorgula";
            button_rezarvasyon_sorgula.UseVisualStyleBackColor = false;
            button_rezarvasyon_sorgula.Click += button_rezarvasyon_sorgula_Click;
            // 
            // dataGridView_rezervasyon_cikisi
            // 
            dataGridView_rezervasyon_cikisi.AllowUserToAddRows = false;
            dataGridView_rezervasyon_cikisi.AllowUserToDeleteRows = false;
            dataGridView_rezervasyon_cikisi.BackgroundColor = SystemColors.Control;
            dataGridView_rezervasyon_cikisi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_rezervasyon_cikisi.Location = new Point(24, 195);
            dataGridView_rezervasyon_cikisi.Name = "dataGridView_rezervasyon_cikisi";
            dataGridView_rezervasyon_cikisi.ReadOnly = true;
            dataGridView_rezervasyon_cikisi.RowHeadersWidth = 51;
            dataGridView_rezervasyon_cikisi.RowTemplate.Height = 29;
            dataGridView_rezervasyon_cikisi.Size = new Size(1100, 516);
            dataGridView_rezervasyon_cikisi.TabIndex = 2;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox9);
            groupBox2.Controls.Add(button_rezarvasyon_sorgula);
            groupBox2.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox2.Location = new Point(586, 23);
            groupBox2.Margin = new Padding(3, 4, 3, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 4, 3, 4);
            groupBox2.Size = new Size(602, 96);
            groupBox2.TabIndex = 17;
            groupBox2.TabStop = false;
            groupBox2.Text = "T.C ile Sorgula";
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(textBox_musteri_cikisi_sorgula_tc_no);
            groupBox9.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox9.Location = new Point(141, 28);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(217, 48);
            groupBox9.TabIndex = 23;
            groupBox9.TabStop = false;
            groupBox9.Text = "T.C No";
            // 
            // textBox_musteri_cikisi_sorgula_tc_no
            // 
            textBox_musteri_cikisi_sorgula_tc_no.BorderStyle = BorderStyle.None;
            textBox_musteri_cikisi_sorgula_tc_no.CausesValidation = false;
            textBox_musteri_cikisi_sorgula_tc_no.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_musteri_cikisi_sorgula_tc_no.Location = new Point(10, 19);
            textBox_musteri_cikisi_sorgula_tc_no.MaxLength = 11;
            textBox_musteri_cikisi_sorgula_tc_no.Name = "textBox_musteri_cikisi_sorgula_tc_no";
            textBox_musteri_cikisi_sorgula_tc_no.Size = new Size(198, 20);
            textBox_musteri_cikisi_sorgula_tc_no.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Silver;
            panel3.Location = new Point(1169, 195);
            panel3.Name = "panel3";
            panel3.Size = new Size(2, 516);
            panel3.TabIndex = 0;
            // 
            // button_ucreti_hesapla
            // 
            button_ucreti_hesapla.BackColor = Color.FromArgb(64, 64, 64);
            button_ucreti_hesapla.Cursor = Cursors.Hand;
            button_ucreti_hesapla.FlatAppearance.BorderSize = 0;
            button_ucreti_hesapla.FlatStyle = FlatStyle.Flat;
            button_ucreti_hesapla.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button_ucreti_hesapla.ForeColor = Color.White;
            button_ucreti_hesapla.Location = new Point(214, 11);
            button_ucreti_hesapla.Name = "button_ucreti_hesapla";
            button_ucreti_hesapla.Size = new Size(122, 32);
            button_ucreti_hesapla.TabIndex = 0;
            button_ucreti_hesapla.Text = "Ücreti Hesapla";
            button_ucreti_hesapla.UseVisualStyleBackColor = false;
            button_ucreti_hesapla.Click += button_ucreti_hesapla_Click;
            // 
            // groupBoxr
            // 
            groupBoxr.Controls.Add(textBox_rezervasyon_cikisi_rezervasyon_no);
            groupBoxr.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxr.Location = new Point(1342, 242);
            groupBoxr.Name = "groupBoxr";
            groupBoxr.Size = new Size(217, 48);
            groupBoxr.TabIndex = 0;
            groupBoxr.TabStop = false;
            groupBoxr.Text = "Rezervasyon No";
            // 
            // textBox_rezervasyon_cikisi_rezervasyon_no
            // 
            textBox_rezervasyon_cikisi_rezervasyon_no.BorderStyle = BorderStyle.None;
            textBox_rezervasyon_cikisi_rezervasyon_no.CausesValidation = false;
            textBox_rezervasyon_cikisi_rezervasyon_no.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_rezervasyon_cikisi_rezervasyon_no.Location = new Point(10, 19);
            textBox_rezervasyon_cikisi_rezervasyon_no.Name = "textBox_rezervasyon_cikisi_rezervasyon_no";
            textBox_rezervasyon_cikisi_rezervasyon_no.Size = new Size(198, 20);
            textBox_rezervasyon_cikisi_rezervasyon_no.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox_rezervasyon_cikisi_personeladsoyad);
            groupBox1.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox1.Location = new Point(1232, 329);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(217, 48);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "İşlem Yapan Personel";
            // 
            // textBox_rezervasyon_cikisi_personeladsoyad
            // 
            textBox_rezervasyon_cikisi_personeladsoyad.BorderStyle = BorderStyle.None;
            textBox_rezervasyon_cikisi_personeladsoyad.CausesValidation = false;
            textBox_rezervasyon_cikisi_personeladsoyad.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_rezervasyon_cikisi_personeladsoyad.Location = new Point(10, 19);
            textBox_rezervasyon_cikisi_personeladsoyad.Name = "textBox_rezervasyon_cikisi_personeladsoyad";
            textBox_rezervasyon_cikisi_personeladsoyad.Size = new Size(198, 20);
            textBox_rezervasyon_cikisi_personeladsoyad.TabIndex = 0;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(textBox_rezervasyon_cikisi_adsoyad);
            groupBox3.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox3.Location = new Point(1496, 320);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(217, 48);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Müşteri Ad Soyad";
            // 
            // textBox_rezervasyon_cikisi_adsoyad
            // 
            textBox_rezervasyon_cikisi_adsoyad.BorderStyle = BorderStyle.None;
            textBox_rezervasyon_cikisi_adsoyad.CausesValidation = false;
            textBox_rezervasyon_cikisi_adsoyad.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_rezervasyon_cikisi_adsoyad.Location = new Point(10, 19);
            textBox_rezervasyon_cikisi_adsoyad.Name = "textBox_rezervasyon_cikisi_adsoyad";
            textBox_rezervasyon_cikisi_adsoyad.Size = new Size(198, 20);
            textBox_rezervasyon_cikisi_adsoyad.TabIndex = 0;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(textBox_rezervasyon_cikisi_oda_no);
            groupBox4.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox4.Location = new Point(1232, 416);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(217, 48);
            groupBox4.TabIndex = 0;
            groupBox4.TabStop = false;
            groupBox4.Text = "Oda No";
            // 
            // textBox_rezervasyon_cikisi_oda_no
            // 
            textBox_rezervasyon_cikisi_oda_no.BorderStyle = BorderStyle.None;
            textBox_rezervasyon_cikisi_oda_no.CausesValidation = false;
            textBox_rezervasyon_cikisi_oda_no.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_rezervasyon_cikisi_oda_no.Location = new Point(10, 19);
            textBox_rezervasyon_cikisi_oda_no.Name = "textBox_rezervasyon_cikisi_oda_no";
            textBox_rezervasyon_cikisi_oda_no.Size = new Size(198, 20);
            textBox_rezervasyon_cikisi_oda_no.TabIndex = 0;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(textBox_rezervasyon_cikisi_giris_tarihi);
            groupBox5.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox5.Location = new Point(1496, 416);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(217, 48);
            groupBox5.TabIndex = 0;
            groupBox5.TabStop = false;
            groupBox5.Text = "Giriş Tarihi";
            // 
            // textBox_rezervasyon_cikisi_giris_tarihi
            // 
            textBox_rezervasyon_cikisi_giris_tarihi.BorderStyle = BorderStyle.None;
            textBox_rezervasyon_cikisi_giris_tarihi.CausesValidation = false;
            textBox_rezervasyon_cikisi_giris_tarihi.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_rezervasyon_cikisi_giris_tarihi.Location = new Point(10, 19);
            textBox_rezervasyon_cikisi_giris_tarihi.Name = "textBox_rezervasyon_cikisi_giris_tarihi";
            textBox_rezervasyon_cikisi_giris_tarihi.Size = new Size(198, 20);
            textBox_rezervasyon_cikisi_giris_tarihi.TabIndex = 0;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(textBox_rezervasyon_cikisi_cikisi_rezervsayon_durumu);
            groupBox6.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox6.Location = new Point(1232, 503);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(217, 48);
            groupBox6.TabIndex = 0;
            groupBox6.TabStop = false;
            groupBox6.Text = "Rezervasyon Durumu";
            // 
            // textBox_rezervasyon_cikisi_cikisi_rezervsayon_durumu
            // 
            textBox_rezervasyon_cikisi_cikisi_rezervsayon_durumu.BorderStyle = BorderStyle.None;
            textBox_rezervasyon_cikisi_cikisi_rezervsayon_durumu.CausesValidation = false;
            textBox_rezervasyon_cikisi_cikisi_rezervsayon_durumu.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_rezervasyon_cikisi_cikisi_rezervsayon_durumu.Location = new Point(10, 19);
            textBox_rezervasyon_cikisi_cikisi_rezervsayon_durumu.Name = "textBox_rezervasyon_cikisi_cikisi_rezervsayon_durumu";
            textBox_rezervasyon_cikisi_cikisi_rezervsayon_durumu.Size = new Size(198, 20);
            textBox_rezervasyon_cikisi_cikisi_rezervsayon_durumu.TabIndex = 0;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(textBox_rezervasyon_cikisi_cikis_tarihi);
            groupBox7.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox7.Location = new Point(1496, 503);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(217, 48);
            groupBox7.TabIndex = 33;
            groupBox7.TabStop = false;
            groupBox7.Text = "Çıkış Tarihi (Bugün)";
            // 
            // textBox_rezervasyon_cikisi_cikis_tarihi
            // 
            textBox_rezervasyon_cikisi_cikis_tarihi.BorderStyle = BorderStyle.None;
            textBox_rezervasyon_cikisi_cikis_tarihi.CausesValidation = false;
            textBox_rezervasyon_cikisi_cikis_tarihi.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_rezervasyon_cikisi_cikis_tarihi.Location = new Point(10, 19);
            textBox_rezervasyon_cikisi_cikis_tarihi.Name = "textBox_rezervasyon_cikisi_cikis_tarihi";
            textBox_rezervasyon_cikisi_cikis_tarihi.Size = new Size(198, 20);
            textBox_rezervasyon_cikisi_cikis_tarihi.TabIndex = 0;
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(textBox_rezervasyon_cikisi_ucret);
            groupBox8.Controls.Add(button_ucreti_hesapla);
            groupBox8.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox8.Location = new Point(1314, 590);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(341, 48);
            groupBox8.TabIndex = 0;
            groupBox8.TabStop = false;
            groupBox8.Text = "Ücret";
            // 
            // textBox_rezervasyon_cikisi_ucret
            // 
            textBox_rezervasyon_cikisi_ucret.BorderStyle = BorderStyle.None;
            textBox_rezervasyon_cikisi_ucret.CausesValidation = false;
            textBox_rezervasyon_cikisi_ucret.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_rezervasyon_cikisi_ucret.Location = new Point(10, 19);
            textBox_rezervasyon_cikisi_ucret.Name = "textBox_rezervasyon_cikisi_ucret";
            textBox_rezervasyon_cikisi_ucret.Size = new Size(198, 20);
            textBox_rezervasyon_cikisi_ucret.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(474, 165);
            label1.Name = "label1";
            label1.Size = new Size(184, 27);
            label1.TabIndex = 34;
            label1.Text = "Rezervasyonlar";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(1357, 157);
            label2.Name = "label2";
            label2.Size = new Size(189, 27);
            label2.TabIndex = 35;
            label2.Text = "Müşteri Bilgileri";
            // 
            // button_rezervsayon_cikisi
            // 
            button_rezervsayon_cikisi.BackColor = Color.FromArgb(64, 64, 64);
            button_rezervsayon_cikisi.Cursor = Cursors.Hand;
            button_rezervsayon_cikisi.FlatAppearance.BorderSize = 0;
            button_rezervsayon_cikisi.FlatStyle = FlatStyle.Flat;
            button_rezervsayon_cikisi.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button_rezervsayon_cikisi.ForeColor = Color.White;
            button_rezervsayon_cikisi.Location = new Point(1393, 713);
            button_rezervsayon_cikisi.Name = "button_rezervsayon_cikisi";
            button_rezervsayon_cikisi.Size = new Size(178, 65);
            button_rezervsayon_cikisi.TabIndex = 36;
            button_rezervsayon_cikisi.Text = "Rezervasyon Çıkışı Yap";
            button_rezervsayon_cikisi.UseVisualStyleBackColor = false;
            button_rezervsayon_cikisi.Click += button_rezervsayon_cikisi_Click;
            // 
            // customer_reservation_exit_form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1774, 823);
            Controls.Add(button_rezervsayon_cikisi);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(groupBox8);
            Controls.Add(groupBox7);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            Controls.Add(groupBoxr);
            Controls.Add(panel3);
            Controls.Add(groupBox2);
            Controls.Add(dataGridView_rezervasyon_cikisi);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "customer_reservation_exit_form";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HotelX; Müşteri Çıkışı";
            Load += customer_reservation_exit_form_Load;
            Shown += customer_reservation_exit_form_Shown;
            ((System.ComponentModel.ISupportInitialize)dataGridView_rezervasyon_cikisi).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            groupBoxr.ResumeLayout(false);
            groupBoxr.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button_rezarvasyon_sorgula;
        private DataGridView dataGridView_rezervasyon_cikisi;
        private GroupBox groupBox2;
        private Panel panel3;
        private Button button_ucreti_hesapla;
        private GroupBox groupBoxr;
        private TextBox textBox_rezervasyon_cikisi_rezervasyon_no;
        private GroupBox groupBox1;
        private TextBox textBox_rezervasyon_cikisi_personeladsoyad;
        private GroupBox groupBox3;
        private TextBox textBox_rezervasyon_cikisi_adsoyad;
        private GroupBox groupBox4;
        private TextBox textBox_rezervasyon_cikisi_oda_no;
        private GroupBox groupBox5;
        private TextBox textBox_rezervasyon_cikisi_giris_tarihi;
        private GroupBox groupBox6;
        private TextBox textBox_rezervasyon_cikisi_cikisi_rezervsayon_durumu;
        private GroupBox groupBox7;
        private TextBox textBox_rezervasyon_cikisi_cikis_tarihi;
        private GroupBox groupBox8;
        private TextBox textBox_rezervasyon_cikisi_ucret;
        private GroupBox groupBox9;
        private TextBox textBox_musteri_cikisi_sorgula_tc_no;
        private Label label1;
        private Label label2;
        private Button button_rezervsayon_cikisi;
    }
}