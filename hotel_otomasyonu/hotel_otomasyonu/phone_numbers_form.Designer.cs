namespace hotel_otomasyonu
{
    partial class phone_numbers_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(phone_numbers_form));
            groupBox4 = new GroupBox();
            textBox_aciklama = new TextBox();
            groupBox5 = new GroupBox();
            textBox_tel_no = new TextBox();
            dataGridView_tel_no = new DataGridView();
            groupBox6 = new GroupBox();
            textBox_oda_no = new TextBox();
            groupBox7 = new GroupBox();
            textBox_kat_no = new TextBox();
            label1 = new Label();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_tel_no).BeginInit();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(textBox_aciklama);
            groupBox4.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox4.Location = new Point(887, 203);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(217, 169);
            groupBox4.TabIndex = 14;
            groupBox4.TabStop = false;
            groupBox4.Text = "Açıklama";
            // 
            // textBox_aciklama
            // 
            textBox_aciklama.BackColor = Color.White;
            textBox_aciklama.BorderStyle = BorderStyle.None;
            textBox_aciklama.CausesValidation = false;
            textBox_aciklama.Enabled = false;
            textBox_aciklama.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_aciklama.Location = new Point(10, 19);
            textBox_aciklama.Multiline = true;
            textBox_aciklama.Name = "textBox_aciklama";
            textBox_aciklama.Size = new Size(198, 144);
            textBox_aciklama.TabIndex = 4;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(textBox_tel_no);
            groupBox5.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox5.Location = new Point(596, 203);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(217, 48);
            groupBox5.TabIndex = 16;
            groupBox5.TabStop = false;
            groupBox5.Text = "Telefon Numarası";
            // 
            // textBox_tel_no
            // 
            textBox_tel_no.BackColor = Color.White;
            textBox_tel_no.BorderStyle = BorderStyle.None;
            textBox_tel_no.CausesValidation = false;
            textBox_tel_no.Enabled = false;
            textBox_tel_no.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_tel_no.Location = new Point(10, 19);
            textBox_tel_no.MaxLength = 10;
            textBox_tel_no.Name = "textBox_tel_no";
            textBox_tel_no.Size = new Size(198, 20);
            textBox_tel_no.TabIndex = 3;
            // 
            // dataGridView_tel_no
            // 
            dataGridView_tel_no.AllowUserToAddRows = false;
            dataGridView_tel_no.AllowUserToDeleteRows = false;
            dataGridView_tel_no.BackgroundColor = Color.White;
            dataGridView_tel_no.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_tel_no.Location = new Point(52, 98);
            dataGridView_tel_no.Name = "dataGridView_tel_no";
            dataGridView_tel_no.ReadOnly = true;
            dataGridView_tel_no.RowHeadersWidth = 51;
            dataGridView_tel_no.RowTemplate.Height = 29;
            dataGridView_tel_no.Size = new Size(484, 587);
            dataGridView_tel_no.TabIndex = 12;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(textBox_oda_no);
            groupBox6.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox6.Location = new Point(887, 98);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(217, 48);
            groupBox6.TabIndex = 17;
            groupBox6.TabStop = false;
            groupBox6.Text = "Oda No";
            // 
            // textBox_oda_no
            // 
            textBox_oda_no.BackColor = Color.White;
            textBox_oda_no.BorderStyle = BorderStyle.None;
            textBox_oda_no.CausesValidation = false;
            textBox_oda_no.Enabled = false;
            textBox_oda_no.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_oda_no.Location = new Point(10, 19);
            textBox_oda_no.MaxLength = 10;
            textBox_oda_no.Name = "textBox_oda_no";
            textBox_oda_no.Size = new Size(198, 20);
            textBox_oda_no.TabIndex = 11;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(textBox_kat_no);
            groupBox7.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox7.Location = new Point(596, 98);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(217, 48);
            groupBox7.TabIndex = 13;
            groupBox7.TabStop = false;
            groupBox7.Text = "Kat No";
            // 
            // textBox_kat_no
            // 
            textBox_kat_no.BackColor = Color.White;
            textBox_kat_no.BorderStyle = BorderStyle.None;
            textBox_kat_no.CausesValidation = false;
            textBox_kat_no.Enabled = false;
            textBox_kat_no.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_kat_no.Location = new Point(10, 19);
            textBox_kat_no.MaxLength = 0;
            textBox_kat_no.Name = "textBox_kat_no";
            textBox_kat_no.Size = new Size(198, 20);
            textBox_kat_no.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(476, 24);
            label1.Name = "label1";
            label1.Size = new Size(223, 27);
            label1.TabIndex = 18;
            label1.Text = "Telefon Numaraları";
            // 
            // phone_numbers_form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1175, 714);
            Controls.Add(label1);
            Controls.Add(groupBox4);
            Controls.Add(groupBox5);
            Controls.Add(dataGridView_tel_no);
            Controls.Add(groupBox6);
            Controls.Add(groupBox7);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "phone_numbers_form";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HotelX; Telefon Numaraları";
            Load += phone_numbers_form_Load;
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
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox4;
        private TextBox textBox_aciklama;
        private GroupBox groupBox5;
        private TextBox textBox_tel_no;
        private DataGridView dataGridView_tel_no;
        private GroupBox groupBox6;
        private TextBox textBox_oda_no;
        private GroupBox groupBox7;
        private TextBox textBox_kat_no;
        private Label label1;
    }
}