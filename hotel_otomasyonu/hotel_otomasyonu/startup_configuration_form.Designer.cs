namespace hotel_otomasyonu
{
    partial class startup_configuration_form
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(startup_configuration_form));
            pictureBox2 = new PictureBox();
            label_yonetim_paneli_baslik = new Label();
            progressBar_startup = new ProgressBar();
            timer_progressBar = new System.Windows.Forms.Timer(components);
            label_yazi = new Label();
            label_surec_yazi = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            ımageList_test = new ImageList(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(191, 64);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(306, 316);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 20;
            pictureBox2.TabStop = false;
            // 
            // label_yonetim_paneli_baslik
            // 
            label_yonetim_paneli_baslik.AutoEllipsis = true;
            label_yonetim_paneli_baslik.AutoSize = true;
            label_yonetim_paneli_baslik.Font = new Font("Arial", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label_yonetim_paneli_baslik.Location = new Point(139, 16);
            label_yonetim_paneli_baslik.Name = "label_yonetim_paneli_baslik";
            label_yonetim_paneli_baslik.Size = new Size(393, 33);
            label_yonetim_paneli_baslik.TabIndex = 19;
            label_yonetim_paneli_baslik.Text = "Configürasyonlar Yapılıyor...";
            label_yonetim_paneli_baslik.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // progressBar_startup
            // 
            progressBar_startup.BackColor = Color.FromArgb(200, 199, 179);
            progressBar_startup.ForeColor = Color.FromArgb(200, 199, 179);
            progressBar_startup.Location = new Point(50, 467);
            progressBar_startup.Name = "progressBar_startup";
            progressBar_startup.Size = new Size(570, 23);
            progressBar_startup.TabIndex = 21;
            // 
            // timer_progressBar
            // 
            timer_progressBar.Tick += timer_progressBar_Tick;
            // 
            // label_yazi
            // 
            label_yazi.AutoSize = true;
            label_yazi.BackColor = Color.Transparent;
            label_yazi.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label_yazi.Location = new Point(3, 0);
            label_yazi.Name = "label_yazi";
            label_yazi.Size = new Size(293, 18);
            label_yazi.TabIndex = 22;
            label_yazi.Text = "Veri Tabanı Ve Tablolar Kontrol Ediliyor...";
            // 
            // label_surec_yazi
            // 
            label_surec_yazi.AutoSize = true;
            label_surec_yazi.BackColor = Color.Transparent;
            label_surec_yazi.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label_surec_yazi.Location = new Point(302, 0);
            label_surec_yazi.Name = "label_surec_yazi";
            label_surec_yazi.Size = new Size(0, 17);
            label_surec_yazi.TabIndex = 23;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.Transparent;
            flowLayoutPanel1.Controls.Add(label_yazi);
            flowLayoutPanel1.Controls.Add(label_surec_yazi);
            flowLayoutPanel1.Location = new Point(50, 441);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(570, 20);
            flowLayoutPanel1.TabIndex = 24;
            // 
            // ımageList_test
            // 
            ımageList_test.ColorDepth = ColorDepth.Depth32Bit;
            ımageList_test.ImageStream = (ImageListStreamer)resources.GetObject("ımageList_test.ImageStream");
            ımageList_test.TransparentColor = Color.Transparent;
            ımageList_test.Images.SetKeyName(0, "door.png");
            ımageList_test.Images.SetKeyName(1, "add_customer.png");
            ımageList_test.Images.SetKeyName(2, "archives.png");
            ımageList_test.Images.SetKeyName(3, "door.png");
            ımageList_test.Images.SetKeyName(4, "query_customer.png");
            ımageList_test.Images.SetKeyName(5, "question-mark.png");
            ımageList_test.Images.SetKeyName(6, "reservation.jpg");
            ımageList_test.Images.SetKeyName(7, "tel.png");
            // 
            // startup_configuration_form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(672, 555);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(progressBar_startup);
            Controls.Add(pictureBox2);
            Controls.Add(label_yonetim_paneli_baslik);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "startup_configuration_form";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HotelX; Başlangıç Konfigürasyonları";
            Load += startup_configuration_form_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox2;
        private Label label_yonetim_paneli_baslik;
        private ProgressBar progressBar_startup;
        private System.Windows.Forms.Timer timer_progressBar;
        private Label label_yazi;
        private Label label_surec_yazi;
        private FlowLayoutPanel flowLayoutPanel1;
        private ImageList ımageList_test;
    }
}