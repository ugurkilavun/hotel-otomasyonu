namespace hotel_otomasyonu
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            panel_giris_resim = new Panel();
            pictureBoxMainIcon = new PictureBox();
            labelManagementPanelTitle = new Label();
            panel_giris = new Panel();
            groupBox2 = new GroupBox();
            textBoxPassword1 = new TextBox();
            pictureBox6 = new PictureBox();
            groupBox1 = new GroupBox();
            pictureBox1 = new PictureBox();
            textBoxNickname = new TextBox();
            panelHorizantallyLine = new Panel();
            label_tum_hatalar = new Label();
            linkLabelForgotPassword = new LinkLabel();
            labelLoginText = new Label();
            buttonLogin = new Button();
            panel_giris_resim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMainIcon).BeginInit();
            panel_giris.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel_giris_resim
            // 
            panel_giris_resim.BackColor = SystemColors.Control;
            panel_giris_resim.BackgroundImageLayout = ImageLayout.Stretch;
            panel_giris_resim.Controls.Add(pictureBoxMainIcon);
            panel_giris_resim.Controls.Add(labelManagementPanelTitle);
            panel_giris_resim.Location = new Point(0, -2);
            panel_giris_resim.Name = "panel_giris_resim";
            panel_giris_resim.Size = new Size(380, 473);
            panel_giris_resim.TabIndex = 10;
            // 
            // pictureBoxMainIcon
            // 
            pictureBoxMainIcon.Image = (Image)resources.GetObject("pictureBoxMainIcon.Image");
            pictureBoxMainIcon.Location = new Point(37, 115);
            pictureBoxMainIcon.Name = "pictureBoxMainIcon";
            pictureBoxMainIcon.Size = new Size(306, 316);
            pictureBoxMainIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxMainIcon.TabIndex = 18;
            pictureBoxMainIcon.TabStop = false;
            // 
            // labelManagementPanelTitle
            // 
            labelManagementPanelTitle.AutoEllipsis = true;
            labelManagementPanelTitle.Font = new Font("Arial", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            labelManagementPanelTitle.Location = new Point(70, 22);
            labelManagementPanelTitle.Name = "labelManagementPanelTitle";
            labelManagementPanelTitle.Size = new Size(240, 75);
            labelManagementPanelTitle.TabIndex = 11;
            labelManagementPanelTitle.Text = "HotelX Yönetim Paneli";
            labelManagementPanelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel_giris
            // 
            panel_giris.BackColor = Color.Transparent;
            panel_giris.Controls.Add(groupBox2);
            panel_giris.Controls.Add(groupBox1);
            panel_giris.Controls.Add(panelHorizantallyLine);
            panel_giris.Controls.Add(label_tum_hatalar);
            panel_giris.Controls.Add(linkLabelForgotPassword);
            panel_giris.Controls.Add(labelLoginText);
            panel_giris.Controls.Add(buttonLogin);
            panel_giris.Location = new Point(387, -2);
            panel_giris.Name = "panel_giris";
            panel_giris.Size = new Size(384, 473);
            panel_giris.TabIndex = 31;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.White;
            groupBox2.Controls.Add(textBoxPassword1);
            groupBox2.Controls.Add(pictureBox6);
            groupBox2.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox2.Location = new Point(75, 197);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(250, 45);
            groupBox2.TabIndex = 38;
            groupBox2.TabStop = false;
            // 
            // textBoxPassword1
            // 
            textBoxPassword1.BorderStyle = BorderStyle.None;
            textBoxPassword1.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxPassword1.Location = new Point(41, 16);
            textBoxPassword1.Name = "textBoxPassword1";
            textBoxPassword1.PasswordChar = '*';
            textBoxPassword1.PlaceholderText = "******";
            textBoxPassword1.Size = new Size(198, 20);
            textBoxPassword1.TabIndex = 3;
            textBoxPassword1.Enter += textBoxPassword_Enter;
            // 
            // pictureBox6
            // 
            pictureBox6.BackColor = Color.Transparent;
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(3, 11);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(30, 30);
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.TabIndex = 28;
            pictureBox6.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Controls.Add(textBoxNickname);
            groupBox1.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox1.Location = new Point(75, 134);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(250, 45);
            groupBox1.TabIndex = 37;
            groupBox1.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(4, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(31, 27);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 28;
            pictureBox1.TabStop = false;
            // 
            // textBoxNickname
            // 
            textBoxNickname.BorderStyle = BorderStyle.None;
            textBoxNickname.CausesValidation = false;
            textBoxNickname.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxNickname.Location = new Point(42, 16);
            textBoxNickname.Name = "textBoxNickname";
            textBoxNickname.PlaceholderText = "Kullanıcı Adı";
            textBoxNickname.Size = new Size(198, 20);
            textBoxNickname.TabIndex = 3;
            // 
            // panelHorizantallyLine
            // 
            panelHorizantallyLine.BackColor = Color.Silver;
            panelHorizantallyLine.Location = new Point(142, 92);
            panelHorizantallyLine.Name = "panelHorizantallyLine";
            panelHorizantallyLine.Size = new Size(100, 2);
            panelHorizantallyLine.TabIndex = 34;
            // 
            // label_tum_hatalar
            // 
            label_tum_hatalar.AutoEllipsis = true;
            label_tum_hatalar.AutoSize = true;
            label_tum_hatalar.Font = new Font("Arial", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label_tum_hatalar.ForeColor = Color.IndianRed;
            label_tum_hatalar.Location = new Point(192, 262);
            label_tum_hatalar.Name = "label_tum_hatalar";
            label_tum_hatalar.Size = new Size(0, 19);
            label_tum_hatalar.TabIndex = 19;
            // 
            // linkLabelForgotPassword
            // 
            linkLabelForgotPassword.AutoSize = true;
            linkLabelForgotPassword.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            linkLabelForgotPassword.Location = new Point(105, 439);
            linkLabelForgotPassword.Name = "linkLabelForgotPassword";
            linkLabelForgotPassword.Size = new Size(175, 19);
            linkLabelForgotPassword.TabIndex = 0;
            linkLabelForgotPassword.TabStop = true;
            linkLabelForgotPassword.Text = "Şifrenizi mi unuttunuz?";
            linkLabelForgotPassword.LinkClicked += linkLabelForgotPassword_LinkClicked;
            // 
            // labelLoginText
            // 
            labelLoginText.AutoEllipsis = true;
            labelLoginText.AutoSize = true;
            labelLoginText.Font = new Font("Arial", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            labelLoginText.Location = new Point(153, 47);
            labelLoginText.Name = "labelLoginText";
            labelLoginText.Size = new Size(79, 33);
            labelLoginText.TabIndex = 0;
            labelLoginText.Text = "Giriş";
            // 
            // buttonLogin
            // 
            buttonLogin.BackColor = Color.FromArgb(40, 40, 40);
            buttonLogin.BackgroundImageLayout = ImageLayout.None;
            buttonLogin.Cursor = Cursors.Hand;
            buttonLogin.FlatStyle = FlatStyle.Flat;
            buttonLogin.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonLogin.ForeColor = Color.White;
            buttonLogin.Location = new Point(122, 321);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(120, 37);
            buttonLogin.TabIndex = 1;
            buttonLogin.Text = "Giriş Yap";
            buttonLogin.UseVisualStyleBackColor = false;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(769, 471);
            Controls.Add(panel_giris);
            Controls.Add(panel_giris_resim);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HotelX; Giriş Yap";
            FormClosed += LoginForm_FormClosed;
            Load += LoginForm_Load;
            panel_giris_resim.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxMainIcon).EndInit();
            panel_giris.ResumeLayout(false);
            panel_giris.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel_giris_resim;
        private Label labelManagementPanelTitle;
        private PictureBox pictureBoxMainIcon;
        private Panel panel_giris;
        private Label labelLoginText;
        private Button buttonLogin;
        private PictureBox pictureBox6;
        private TextBox textBoxPassword1;
        private LinkLabel linkLabelForgotPassword;
        private Label label_tum_hatalar;
        private Panel panelHorizantallyLine;
        private GroupBox groupBox1;
        private PictureBox pictureBox1;
        private TextBox textBoxNickname;
        private GroupBox groupBox2;
    }
}