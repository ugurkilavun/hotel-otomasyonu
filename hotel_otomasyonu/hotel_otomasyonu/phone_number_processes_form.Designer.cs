namespace hotel_otomasyonu
{
    partial class phone_number_processes_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(phone_number_processes_form));
            groupBox_addd = new GroupBox();
            comboBox_kat_no = new ComboBox();
            groupBox_conteyner = new GroupBox();
            groupBox2 = new GroupBox();
            textBox_aciklama = new TextBox();
            button_ekle = new Button();
            groupBox_telnoo = new GroupBox();
            textBox_tel_no = new TextBox();
            groupBox1 = new GroupBox();
            comboBox_oda_no = new ComboBox();
            groupBox3 = new GroupBox();
            groupBox4 = new GroupBox();
            textBox_sil_aciklama = new TextBox();
            button_tel_no_gizle = new Button();
            button_sil = new Button();
            button_tel_no_goster = new Button();
            groupBox5 = new GroupBox();
            textBox_sil_tel_no = new TextBox();
            dataGridView_tel_no = new DataGridView();
            groupBox6 = new GroupBox();
            textBox_sil_oda_no = new TextBox();
            groupBox7 = new GroupBox();
            textBox_sil_kat_no = new TextBox();
            groupBox_addd.SuspendLayout();
            groupBox_conteyner.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox_telnoo.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_tel_no).BeginInit();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox_addd
            // 
            groupBox_addd.Controls.Add(comboBox_kat_no);
            groupBox_addd.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox_addd.Location = new Point(35, 56);
            groupBox_addd.Name = "groupBox_addd";
            groupBox_addd.Size = new Size(217, 48);
            groupBox_addd.TabIndex = 0;
            groupBox_addd.TabStop = false;
            groupBox_addd.Text = "Kat No";
            // 
            // comboBox_kat_no
            // 
            comboBox_kat_no.BackColor = Color.White;
            comboBox_kat_no.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_kat_no.FlatStyle = FlatStyle.Flat;
            comboBox_kat_no.FormattingEnabled = true;
            comboBox_kat_no.Location = new Point(6, 17);
            comboBox_kat_no.Name = "comboBox_kat_no";
            comboBox_kat_no.Size = new Size(205, 24);
            comboBox_kat_no.TabIndex = 1;
            // 
            // groupBox_conteyner
            // 
            groupBox_conteyner.Controls.Add(groupBox2);
            groupBox_conteyner.Controls.Add(button_ekle);
            groupBox_conteyner.Controls.Add(groupBox_telnoo);
            groupBox_conteyner.Controls.Add(groupBox1);
            groupBox_conteyner.Controls.Add(groupBox_addd);
            groupBox_conteyner.Location = new Point(22, 113);
            groupBox_conteyner.Name = "groupBox_conteyner";
            groupBox_conteyner.Size = new Size(586, 432);
            groupBox_conteyner.TabIndex = 0;
            groupBox_conteyner.TabStop = false;
            groupBox_conteyner.Text = "Odalara Tel No Ekle";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBox_aciklama);
            groupBox2.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox2.Location = new Point(326, 161);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(217, 169);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Açıklama";
            // 
            // textBox_aciklama
            // 
            textBox_aciklama.BackColor = Color.White;
            textBox_aciklama.BorderStyle = BorderStyle.None;
            textBox_aciklama.CausesValidation = false;
            textBox_aciklama.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_aciklama.Location = new Point(10, 19);
            textBox_aciklama.Multiline = true;
            textBox_aciklama.Name = "textBox_aciklama";
            textBox_aciklama.Size = new Size(198, 144);
            textBox_aciklama.TabIndex = 4;
            // 
            // button_ekle
            // 
            button_ekle.BackColor = Color.FromArgb(64, 64, 64);
            button_ekle.Cursor = Cursors.Hand;
            button_ekle.FlatAppearance.BorderSize = 0;
            button_ekle.FlatStyle = FlatStyle.Flat;
            button_ekle.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button_ekle.ForeColor = Color.White;
            button_ekle.Location = new Point(238, 366);
            button_ekle.Name = "button_ekle";
            button_ekle.Size = new Size(111, 38);
            button_ekle.TabIndex = 5;
            button_ekle.Text = "Ekle";
            button_ekle.UseVisualStyleBackColor = false;
            button_ekle.Click += button_ekle_Click;
            // 
            // groupBox_telnoo
            // 
            groupBox_telnoo.Controls.Add(textBox_tel_no);
            groupBox_telnoo.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox_telnoo.Location = new Point(35, 161);
            groupBox_telnoo.Name = "groupBox_telnoo";
            groupBox_telnoo.Size = new Size(217, 48);
            groupBox_telnoo.TabIndex = 0;
            groupBox_telnoo.TabStop = false;
            groupBox_telnoo.Text = "Telefon Numarası *";
            // 
            // textBox_tel_no
            // 
            textBox_tel_no.BackColor = Color.White;
            textBox_tel_no.BorderStyle = BorderStyle.None;
            textBox_tel_no.CausesValidation = false;
            textBox_tel_no.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_tel_no.Location = new Point(10, 19);
            textBox_tel_no.MaxLength = 10;
            textBox_tel_no.Name = "textBox_tel_no";
            textBox_tel_no.Size = new Size(198, 20);
            textBox_tel_no.TabIndex = 3;
            textBox_tel_no.KeyPress += textBox_tel_no_KeyPress;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBox_oda_no);
            groupBox1.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox1.Location = new Point(326, 56);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(217, 48);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Oda No";
            // 
            // comboBox_oda_no
            // 
            comboBox_oda_no.BackColor = Color.White;
            comboBox_oda_no.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_oda_no.FlatStyle = FlatStyle.Flat;
            comboBox_oda_no.FormattingEnabled = true;
            comboBox_oda_no.Location = new Point(6, 17);
            comboBox_oda_no.Name = "comboBox_oda_no";
            comboBox_oda_no.Size = new Size(205, 24);
            comboBox_oda_no.TabIndex = 2;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(groupBox4);
            groupBox3.Controls.Add(button_tel_no_gizle);
            groupBox3.Controls.Add(button_sil);
            groupBox3.Controls.Add(button_tel_no_goster);
            groupBox3.Controls.Add(groupBox5);
            groupBox3.Controls.Add(dataGridView_tel_no);
            groupBox3.Controls.Add(groupBox6);
            groupBox3.Controls.Add(groupBox7);
            groupBox3.Location = new Point(646, 35);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(1033, 664);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            groupBox3.Text = "Telefon Numaralı Ve Silme İşlemi";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(textBox_sil_aciklama);
            groupBox4.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox4.Location = new Point(804, 353);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(217, 169);
            groupBox4.TabIndex = 6;
            groupBox4.TabStop = false;
            groupBox4.Text = "Açıklama";
            // 
            // textBox_sil_aciklama
            // 
            textBox_sil_aciklama.BackColor = Color.White;
            textBox_sil_aciklama.BorderStyle = BorderStyle.None;
            textBox_sil_aciklama.CausesValidation = false;
            textBox_sil_aciklama.Enabled = false;
            textBox_sil_aciklama.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_sil_aciklama.Location = new Point(10, 19);
            textBox_sil_aciklama.Multiline = true;
            textBox_sil_aciklama.Name = "textBox_sil_aciklama";
            textBox_sil_aciklama.Size = new Size(198, 144);
            textBox_sil_aciklama.TabIndex = 4;
            // 
            // button_tel_no_gizle
            // 
            button_tel_no_gizle.BackColor = Color.FromArgb(64, 64, 64);
            button_tel_no_gizle.Cursor = Cursors.Hand;
            button_tel_no_gizle.FlatAppearance.BorderSize = 0;
            button_tel_no_gizle.FlatStyle = FlatStyle.Flat;
            button_tel_no_gizle.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button_tel_no_gizle.ForeColor = Color.White;
            button_tel_no_gizle.Location = new Point(511, 133);
            button_tel_no_gizle.Name = "button_tel_no_gizle";
            button_tel_no_gizle.Size = new Size(160, 56);
            button_tel_no_gizle.TabIndex = 7;
            button_tel_no_gizle.Text = "Telefon Numaralarını Gizle";
            button_tel_no_gizle.UseVisualStyleBackColor = false;
            button_tel_no_gizle.Click += button_tel_no_gizle_Click;
            // 
            // button_sil
            // 
            button_sil.BackColor = Color.Red;
            button_sil.Cursor = Cursors.Hand;
            button_sil.FlatAppearance.BorderSize = 0;
            button_sil.FlatStyle = FlatStyle.Flat;
            button_sil.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button_sil.ForeColor = Color.White;
            button_sil.Location = new Point(739, 578);
            button_sil.Name = "button_sil";
            button_sil.Size = new Size(111, 38);
            button_sil.TabIndex = 10;
            button_sil.Text = "Sil";
            button_sil.UseVisualStyleBackColor = false;
            button_sil.Click += button_sil_Click;
            // 
            // button_tel_no_goster
            // 
            button_tel_no_goster.BackColor = Color.FromArgb(64, 64, 64);
            button_tel_no_goster.Cursor = Cursors.Hand;
            button_tel_no_goster.FlatAppearance.BorderSize = 0;
            button_tel_no_goster.FlatStyle = FlatStyle.Flat;
            button_tel_no_goster.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button_tel_no_goster.ForeColor = Color.White;
            button_tel_no_goster.Location = new Point(511, 45);
            button_tel_no_goster.Name = "button_tel_no_goster";
            button_tel_no_goster.Size = new Size(160, 56);
            button_tel_no_goster.TabIndex = 0;
            button_tel_no_goster.Text = "Telefon Numaralarını Göster";
            button_tel_no_goster.UseVisualStyleBackColor = false;
            button_tel_no_goster.Click += button_tel_no_goster_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(textBox_sil_tel_no);
            groupBox5.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox5.Location = new Point(513, 353);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(217, 48);
            groupBox5.TabIndex = 7;
            groupBox5.TabStop = false;
            groupBox5.Text = "Telefon Numarası *";
            // 
            // textBox_sil_tel_no
            // 
            textBox_sil_tel_no.BackColor = Color.White;
            textBox_sil_tel_no.BorderStyle = BorderStyle.None;
            textBox_sil_tel_no.CausesValidation = false;
            textBox_sil_tel_no.Enabled = false;
            textBox_sil_tel_no.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_sil_tel_no.Location = new Point(10, 19);
            textBox_sil_tel_no.MaxLength = 10;
            textBox_sil_tel_no.Name = "textBox_sil_tel_no";
            textBox_sil_tel_no.Size = new Size(198, 20);
            textBox_sil_tel_no.TabIndex = 3;
            // 
            // dataGridView_tel_no
            // 
            dataGridView_tel_no.AllowUserToAddRows = false;
            dataGridView_tel_no.AllowUserToDeleteRows = false;
            dataGridView_tel_no.BackgroundColor = Color.White;
            dataGridView_tel_no.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_tel_no.Location = new Point(21, 45);
            dataGridView_tel_no.Name = "dataGridView_tel_no";
            dataGridView_tel_no.ReadOnly = true;
            dataGridView_tel_no.RowHeadersWidth = 51;
            dataGridView_tel_no.RowTemplate.Height = 29;
            dataGridView_tel_no.Size = new Size(484, 587);
            dataGridView_tel_no.TabIndex = 0;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(textBox_sil_oda_no);
            groupBox6.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox6.Location = new Point(804, 248);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(217, 48);
            groupBox6.TabIndex = 8;
            groupBox6.TabStop = false;
            groupBox6.Text = "Oda No";
            // 
            // textBox_sil_oda_no
            // 
            textBox_sil_oda_no.BackColor = Color.White;
            textBox_sil_oda_no.BorderStyle = BorderStyle.None;
            textBox_sil_oda_no.CausesValidation = false;
            textBox_sil_oda_no.Enabled = false;
            textBox_sil_oda_no.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_sil_oda_no.Location = new Point(10, 19);
            textBox_sil_oda_no.MaxLength = 10;
            textBox_sil_oda_no.Name = "textBox_sil_oda_no";
            textBox_sil_oda_no.Size = new Size(198, 20);
            textBox_sil_oda_no.TabIndex = 11;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(textBox_sil_kat_no);
            groupBox7.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox7.Location = new Point(513, 248);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(217, 48);
            groupBox7.TabIndex = 0;
            groupBox7.TabStop = false;
            groupBox7.Text = "Kat No";
            // 
            // textBox_sil_kat_no
            // 
            textBox_sil_kat_no.BackColor = Color.White;
            textBox_sil_kat_no.BorderStyle = BorderStyle.None;
            textBox_sil_kat_no.CausesValidation = false;
            textBox_sil_kat_no.Enabled = false;
            textBox_sil_kat_no.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_sil_kat_no.Location = new Point(10, 19);
            textBox_sil_kat_no.MaxLength = 0;
            textBox_sil_kat_no.Name = "textBox_sil_kat_no";
            textBox_sil_kat_no.Size = new Size(198, 20);
            textBox_sil_kat_no.TabIndex = 4;
            // 
            // phone_number_processes_form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1705, 732);
            Controls.Add(groupBox3);
            Controls.Add(groupBox_conteyner);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "phone_number_processes_form";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HotelX; Telefon No İşlemleri";
            Load += phone_number_processes_form_Load;
            groupBox_addd.ResumeLayout(false);
            groupBox_conteyner.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox_telnoo.ResumeLayout(false);
            groupBox_telnoo.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_tel_no).EndInit();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox_addd;
        private ComboBox comboBox_kat_no;
        private GroupBox groupBox_conteyner;
        private GroupBox groupBox1;
        private ComboBox comboBox_oda_no;
        private GroupBox groupBox_telnoo;
        private TextBox textBox_tel_no;
        private Button button_ekle;
        private GroupBox groupBox2;
        private TextBox textBox_aciklama;
        private GroupBox groupBox3;
        private DataGridView dataGridView_tel_no;
        private Button button_tel_no_gizle;
        private Button button_tel_no_goster;
        private GroupBox groupBox4;
        private TextBox textBox_sil_aciklama;
        private Button button_sil;
        private GroupBox groupBox5;
        private TextBox textBox_sil_tel_no;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private TextBox textBox_sil_oda_no;
        private TextBox textBox_sil_kat_no;
    }
}