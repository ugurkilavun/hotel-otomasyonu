using Microsoft.Data.SqlClient;
using Sessions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using AllQuerys;
using System.Security.Cryptography;



namespace hotel_otomasyonu
{
    public partial class new_reservation_form : Form
    {
        // 'UserInformation' sınıfını 'Session_UserInformation' yeni adı ile tanımlar
        private UserInformation Session_UserInformation;
        public new_reservation_form(UserInformation GetSession_UserInformation)
        {
            InitializeComponent();
            // Constructor içinde alınan 'GetSession_UserInformation' nesnesini 'Session_UserInformation' üye değişkenine atar
            this.Session_UserInformation = GetSession_UserInformation;
        }

        // Form içi global değişkenler
        private string connectionString = ConnectionStringClass.ConnectionStringVarible(); // Veri tabanı bağlantısı
        // Persone ID
        string GlobalUserID = string.Empty;

        // Oda seç sekmesi; Oda durumu
        int OdaDurum = -1;

        // Rezervasyonu onayla sekmesi
        string? SecilenOdaNo = string.Empty; // Form içi global seçilen oda no
        string? MusteriTCNo = string.Empty;
        string? MusteriAd = string.Empty;
        string? MusteriSoyad = string.Empty;

        // Rezervasyonu tipi
        // 1: Normal rezervasyon,
        // 2: Geçici rezervasyondan Oda ayırtmaya
        // 3: Geçici rezervasyon; oda ayırtma
        int RezervasyonTipi = 0; 

        // -------------------------------------------------------------- * !Oda Seç Sekmesi! * --------------------------------------------------------------

        private void new_reservation_form_Load(object sender, EventArgs e)
        {
            string? UserID = Session_UserInformation.userID;
            GlobalUserID = UserID;
            // MessageBox.Show(GlobalUserID);
            // tabControl_conteyner'ın Sekmelerinin başlıklarını gizle; Bunlar "Properties" kısmından da yapılabilir
            tabControl_conteyner.Appearance = TabAppearance.FlatButtons;
            tabControl_conteyner.ItemSize = new Size(0, 1);
            tabControl_conteyner.SizeMode = TabSizeMode.Fixed;

            // Form yüklendiğinde!
            tabControl_conteyner.SelectedTab = tabPage_oda_sec;

            // Bugünden ihtibaren rezervasyon işlemi yapma!
            dateTimePicker_rezervasyon_tarihi.MinDate = DateTime.Now;
            dateTimePicker_rezervasyon_tarihi.Value = DateTime.Now;
            dateTimePicker_rezervasyon_tarihi.Format = DateTimePickerFormat.Custom;
            dateTimePicker_rezervasyon_tarihi.CustomFormat = "dd MMMM yyyy dddd HH:mm"; //4 aralık 2023 pazartesi

            // Rezarvasyon Çıkış tarihi
            dateTimePicker_rezervasyon_cikis_tarihi.MinDate = DateTime.Now;
            dateTimePicker_rezervasyon_cikis_tarihi.Value = DateTime.Now;
            dateTimePicker_rezervasyon_cikis_tarihi.Format = DateTimePickerFormat.Custom;
            dateTimePicker_rezervasyon_cikis_tarihi.CustomFormat = "dd MMMM yyyy dddd HH:mm"; //4 aralık 2023 pazartesi

        }

        //int SonSecilenOda = 0;

        private void new_reservation_form_Shown(object sender, EventArgs e)
        {

            // Oda Durumu: 0 boş, 1 dolu, 2 Ayırtılmış , 4 Kullanılamaz
            int bos_odalar = 0;
            int dolu_odalar = 0;
            int kullanilamayan_odalar = 0;
            int ayirtilmis_odalar = 0;
            string? imagePath = null;

            SqlConnection connect = new SqlConnection(connectionString);

            try
            {
                /*
                oda_no INT PRIMARY KEY NOT NULL,
                oda_durum SMALLINT NOT NULL, --0 boş, 1 dolu
                oda_aciklamasi VARCHAR(500) NULL,
                kat_no INT,
                */
                connect.Open();
                string query = "SELECT oda_no, oda_durum FROM odalar";

                SqlCommand command = new SqlCommand(query, connect);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Oda dolu mu?

                    // Dolu değil ise
                    if (Convert.ToInt16(reader[1]) == 0)
                    {
                        bos_odalar++;

                        // 'DynamicFlowLayoutPanel'
                        DynamicFlowLayoutPanel FlowLayoutPanel_container = new DynamicFlowLayoutPanel();
                        flowLayoutPanel_tum_odalar.Controls.Add(FlowLayoutPanel_container);

                        // 'DynamicLabel'
                        DynamicLabel DynamicLabel = new DynamicLabel();
                        DynamicLabel.Text = Convert.ToString(reader[0]);

                        // 'DynamicButton'
                        DynamicButton dynamicButton = new DynamicButton();
                        dynamicButton.Name = Convert.ToString(reader[0]); // Name özelliğini ayarla
                        dynamicButton.ButtonClick += DynamicButton_ButtonClick; // Olayı dinle
                        imagePath = "images/double-bed-empty.png"; // Resim yolu belirle
                        dynamicButton.BackgroundImage = Image.FromFile(imagePath); // Arka plan resmini ayarla

                        // "FlowLayoutPanel_container"a dynamicButton ve DynamicLabel ekle
                        FlowLayoutPanel_container.Controls.Add(dynamicButton);
                        FlowLayoutPanel_container.Controls.Add(DynamicLabel);

                    }
                    // Dolu ise
                    else if (Convert.ToInt16(reader[1]) == 1)
                    {
                        dolu_odalar++;

                        // 'DynamicFlowLayoutPanel'
                        DynamicFlowLayoutPanel FlowLayoutPanel_container = new DynamicFlowLayoutPanel();
                        flowLayoutPanel_tum_odalar.Controls.Add(FlowLayoutPanel_container);

                        // 'DynamicLabel'
                        DynamicLabel DynamicLabel = new DynamicLabel();
                        DynamicLabel.Text = Convert.ToString(reader[0]);

                        // 'DynamicButton'
                        DynamicButton dynamicButton = new DynamicButton();
                        dynamicButton.Name = Convert.ToString(reader[0]); // Name özelliğini ayarla
                        dynamicButton.ButtonClick += DynamicButton_ButtonClick; // Olayı dinle
                        imagePath = "images/double-bed-full.png"; // Resim yolu belirle
                        dynamicButton.BackgroundImage = Image.FromFile(imagePath); // Arka plan resmini ayarla

                        // 'FlowLayoutPanel_container'a; dynamicButton ve DynamicLabel'ı ekle
                        FlowLayoutPanel_container.Controls.Add(dynamicButton);
                        FlowLayoutPanel_container.Controls.Add(DynamicLabel);

                    }
                    // Ayırtılmış oda; Reserved
                    else if (Convert.ToInt16(reader[1]) == 2)
                    {

                        int result = QueryReservedRooms(Convert.ToString(reader[0]));

                        // Çıkış tarihi, bugünün tarihini geçmediyse (Büyük ise)
                        if (result > 0)
                        {
                            ayirtilmis_odalar++;

                            //MessageBox.Show("Çıkış Tarihi, Bugünden büyük");
                            // 'DynamicFlowLayoutPanel'
                            DynamicFlowLayoutPanel FlowLayoutPanel_container = new DynamicFlowLayoutPanel();
                            flowLayoutPanel_tum_odalar.Controls.Add(FlowLayoutPanel_container);

                            // 'DynamicLabel'
                            DynamicLabel DynamicLabel = new DynamicLabel();
                            DynamicLabel.Text = Convert.ToString(reader[0]);

                            // 'DynamicButton'
                            DynamicButton dynamicButton = new DynamicButton();
                            dynamicButton.Name = Convert.ToString(reader[0]); // Name özelliğini ayarla
                            dynamicButton.ButtonClick += DynamicButton_ButtonClick; // Olayı dinle
                            imagePath = "images/double-bed-reserved.png"; // Resim yolu belirle
                            dynamicButton.BackgroundImage = Image.FromFile(imagePath); // Arka plan resmini ayarla

                            // 'FlowLayoutPanel_container'a; dynamicButton ve DynamicLabel'ı ekle
                            FlowLayoutPanel_container.Controls.Add(dynamicButton);
                            FlowLayoutPanel_container.Controls.Add(DynamicLabel);
                        }
                        // Çıkış tarihi, bugünün tarihini geçtiyse (Küçük ise)
                        else
                        {
                            bos_odalar++;

                            RoomAndReservationInformationUpdate(Convert.ToString(reader[0]));

                            // 'DynamicFlowLayoutPanel'
                            DynamicFlowLayoutPanel FlowLayoutPanel_container = new DynamicFlowLayoutPanel();
                            flowLayoutPanel_tum_odalar.Controls.Add(FlowLayoutPanel_container);

                            // 'DynamicLabel'
                            DynamicLabel DynamicLabel = new DynamicLabel();
                            DynamicLabel.Text = Convert.ToString(reader[0]);

                            // 'DynamicButton'
                            DynamicButton dynamicButton = new DynamicButton();
                            dynamicButton.Name = Convert.ToString(reader[0]); // Name özelliğini ayarla
                            dynamicButton.ButtonClick += DynamicButton_ButtonClick; // Olayı dinle
                            imagePath = "images/double-bed-empty.png"; // Resim yolu belirle
                            dynamicButton.BackgroundImage = Image.FromFile(imagePath); // Arka plan resmini ayarla

                            // "FlowLayoutPanel_container"a dynamicButton ve DynamicLabel ekle
                            FlowLayoutPanel_container.Controls.Add(dynamicButton);
                            FlowLayoutPanel_container.Controls.Add(DynamicLabel);
                        }

                    }
                    // Kullanılamaz ise =  4
                    else if (Convert.ToInt16(reader[1]) == 4)
                    {

                        kullanilamayan_odalar++;

                        // 'DynamicFlowLayoutPanel'
                        DynamicFlowLayoutPanel FlowLayoutPanel_container = new DynamicFlowLayoutPanel();
                        flowLayoutPanel_tum_odalar.Controls.Add(FlowLayoutPanel_container);

                        // 'DynamicLabel'
                        DynamicLabel DynamicLabel = new DynamicLabel();
                        DynamicLabel.Text = Convert.ToString(reader[0]);

                        // 'DynamicButton'
                        DynamicButton dynamicButton = new DynamicButton();
                        dynamicButton.Name = Convert.ToString(reader[0]); // Name özelliğini ayarla
                        dynamicButton.ButtonClick += DynamicButton_ButtonClick; // Olayı dinle
                        imagePath = "images/double-bed-unavailable.png"; // Resim yolu belirle
                        dynamicButton.BackgroundImage = Image.FromFile(imagePath); // Arka plan resmini ayarla

                        // 'FlowLayoutPanel_container'a; dynamicButton ve DynamicLabel'ı ekle
                        FlowLayoutPanel_container.Controls.Add(dynamicButton);
                        FlowLayoutPanel_container.Controls.Add(DynamicLabel);
                    }


                    label_bos_odalar.Text = Convert.ToString(bos_odalar);
                    label_dolu_odalar.Text = Convert.ToString(dolu_odalar);
                    label_kullanilamayan_odalar.Text = Convert.ToString(kullanilamayan_odalar);
                    label_ayirtilmis_odalar.Text = Convert.ToString(ayirtilmis_odalar);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Query sırasında hata oluştu! Hata: " + ex.ToString());
            }
            finally
            {

                if (connect != null)
                {
                    connect.Close();
                }

            }

        }


        // -------------------------------------------------------------- * !Sağ Buttonlar! * --------------------------------------------------------------

        // Normal Rezervasyon
        private void button_oda_sec_Click(object sender, EventArgs e)
        {
            // Hicbir oda seçilmediyse
            if (label_son_secilen_oda.Text != string.Empty)
            {
                // Oda durumu sorgulama
                int RoomSituation = RoomSituationShow(Convert.ToInt16(label_son_secilen_oda.Text));

                // Oda durumu?

                // Boş ise
                if (RoomSituation == 0)
                {
                    MessageBox.Show("Boş Oda; Seçilebilir.", "Oda Seç", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // Oda numarası, rezervasyon için alınıyor!
                    SecilenOdaNo = label_son_secilen_oda.Text;
                    RezervasyonTipi = 1;
                    tabControl_conteyner.SelectedTab = tabPage_musteri_sec;
                }
                // Dolu ise
                else if (RoomSituation == 1)
                {
                    // MessageBox.Show("Dolu Oda; Seçilemez.", "Oda Seç");
                    MessageBox.Show("Dolu Oda; Seçilemez.", "Oda Seç", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                // Rezerve edilmiş: Ayırtılmış Odalar
                else if (RoomSituation == 2)
                {
                    // MessageBox.Show("Hicbir Oda Seçilmedi", "Oda Seç");
                    //MessageBox.Show("Ayırtılmış Oda; Seçilemez.", "Oda Seç", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MusteriSecFormunuEskiHalineGetir();
                    SecilenOdaNo = label_son_secilen_oda.Text;
                    RezervasyonTipi = 2;

                    // Ayırtılmış oda, kime ayırtılmış? Sorgula ve 'müsteri Seç' sekmesini doldur.
                    SqlConnection connect = new SqlConnection(connectionString);

                    try
                    {
                        string CustomerID = string.Empty;

                        connect.Open();

                        string Query1 = "SELECT m_tc FROM rezervasyonlar WHERE oda_no = @oda_nol AND rezervasyon_durumu = @rezervasyon_durumu";

                        SqlCommand command1 = new SqlCommand(Query1, connect);
                        command1.Parameters.AddWithValue("@oda_nol", label_son_secilen_oda.Text);
                        command1.Parameters.AddWithValue("@rezervasyon_durumu", RoomSituation);// RoomSituation = 2

                        SqlDataReader reader = command1.ExecuteReader();

                        while (reader.Read())
                        {
                            CustomerID = Convert.ToString(reader[0]);
                        }

                        reader.Close();

                        if (CustomerID != string.Empty)
                        {
                            string Query2 = "SELECT * FROM musteri_bilgileri WHERE m_tc = @m_tc";

                            SqlCommand command2 = new SqlCommand(Query2, connect);
                            command2.Parameters.AddWithValue("@m_tc", CustomerID);

                            SqlDataReader reader2 = command2.ExecuteReader();

                            while (reader2.Read())
                            {
                                textBox_musteri_sec_tc.Text = Convert.ToString(reader2[0]);
                                textBox_musteri_sec_ad.Text = Convert.ToString(reader2[1]);
                                textBox_musteri_sec_soyad.Text = Convert.ToString(reader2[2]);

                                if (Convert.ToInt16(reader2[3]) == 1)
                                {
                                    radioButton_musteri_sec_erkek.Checked = true;
                                }
                                else
                                {
                                    radioButton_musteri_sec_kadin.Checked = true;
                                }

                                textBox_musteri_sec_tel_no.Text = Convert.ToString(reader2[4]);
                                textBox_musteri_sec_eposta.Text = Convert.ToString(reader2[5]);
                                textBox_musteri_sec_acik_adres.Text = Convert.ToString(reader2[6]);
                                comboBox_musteri_sec_kan_grubu.Text = Convert.ToString(reader2[7]);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Müşterinin T.C numarası bulunamadı!", "T.C Sorgu");
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("SQL Query sırasında hata oluştu! Hata: " + ex.ToString());
                    }
                    finally
                    {

                        if (connect != null)
                        {
                            connect.Close();
                        }
                    }

                    //
                    MusteriTCNo = textBox_musteri_sec_tc.Text;
                    MusteriAd = textBox_musteri_sec_ad.Text;
                    MusteriSoyad = textBox_musteri_sec_soyad.Text;

                    textBox_musteri_sec_tc.Enabled = false;
                    button_sec_ve_ilerle.Enabled = true;
                    button_tc_ile_musteri_sorgula.Enabled = false;

                    tabControl_conteyner.SelectedTab = tabPage_musteri_sec;
                }
                // Kullanılamaz ise
                else if (RoomSituation == 4)
                {
                    // MessageBox.Show("Kullanılamaz Oda; Seçilemez.", "Oda Seç");
                    MessageBox.Show("Kullanılamaz Oda; Seçilemez.", "Oda Seç", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (RoomSituation == -1)
                {
                    // MessageBox.Show("Hicbir Oda Seçilmedi", "Oda Seç");
                    MessageBox.Show("Hicbir Oda Seçilmedi!", "Oda Seç", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            // Herhangi bir oda seçildi ise
            else
            {

                //MessageBox.Show("(i)Hicbir Oda Seçilmedi", "Oda Seç");
                MessageBox.Show("Hicbir Oda Seçilmedi", "Oda Seç", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        // Odayı Ayırtma
        private void button_odayi_ayirt_Click(object sender, EventArgs e)
        {
            // Hicbir oda seçilmediyse
            if (label_son_secilen_oda.Text != string.Empty)
            {
                // Oda durumu sorgulama
                int RoomSituation = RoomSituationShow(Convert.ToInt16(label_son_secilen_oda.Text));

                // Oda durumu?

                // Boş ise
                if (RoomSituation == 0)
                {
                    MessageBox.Show("Boş Oda; Seçilebilir.", "Oda Seç", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // Oda numarası, rezervasyon için alınıyor!
                    SecilenOdaNo = label_son_secilen_oda.Text;
                    RezervasyonTipi = 3;
                    tabControl_conteyner.SelectedTab = tabPage_musteri_sec;
                }
                // Dolu ise
                else if (RoomSituation == 1)
                {
                    // MessageBox.Show("Dolu Oda; Seçilemez.", "Oda Seç");
                    MessageBox.Show("Dolu Oda; Seçilemez.", "Oda Seç", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                // Rezerve edilmiş: Ayırtılmış Odalar
                else if (RoomSituation == 2)
                {
                    // MessageBox.Show("Hicbir Oda Seçilmedi", "Oda Seç");
                    MessageBox.Show("Ayırtılmış Odayı tekrardan ayırtamazsın; Seçilemez.", "Oda Seç", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                // Kullanılamaz ise
                else if (RoomSituation == 4)
                {
                    // MessageBox.Show("Kullanılamaz Oda; Seçilemez.", "Oda Seç");
                    MessageBox.Show("Kullanılamaz Oda; Seçilemez.", "Oda Seç", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (RoomSituation == -1)
                {
                    // MessageBox.Show("Hicbir Oda Seçilmedi", "Oda Seç");
                    MessageBox.Show("Hicbir Oda Seçilmedi!", "Oda Seç", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            // Herhangi bir oda seçildi ise
            else
            {
                //MessageBox.Show("(i)Hicbir Oda Seçilmedi", "Oda Seç");
                MessageBox.Show("Hicbir Oda Seçilmedi", "Oda Seç", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }

        private void button_temize_gec_Click(object sender, EventArgs e)
        {

            OdaDurum = -1;
            SecilenOdaNo = string.Empty;
            label_son_secilen_oda.Text = null;

        }

        private void button_islemi_iptal_et_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // -------------------------------------------------------------- * !Müşteri Seç Sekmesi! * --------------------------------------------------------------

        // müşteri sorgulama
        private void button_tc_ile_musteri_sorgula_Click(object sender, EventArgs e)
        {
            // T.C Sorgu alanı boş değilse
            if (textBox_musteri_sec_tc.Text != string.Empty)
            {

                SqlConnection connect = new SqlConnection(connectionString);

                try
                {
                    connect.Open();

                    string query = "SELECT * FROM musteri_bilgileri WHERE m_tc = @m_tc";

                    SqlCommand command = new SqlCommand(query, connect);
                    command.Parameters.AddWithValue("@m_tc", textBox_musteri_sec_tc.Text);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        textBox_musteri_sec_tc.Text = textBox_musteri_sec_tc.Text;

                        textBox_musteri_sec_ad.Text = Convert.ToString(reader["m_ad"]);
                        textBox_musteri_sec_soyad.Text = Convert.ToString(reader["m_soyad"]);

                        // Erkek
                        if (Convert.ToInt16(reader["m_cinsiyet"]) == 1)

                        {
                            radioButton_musteri_sec_erkek.Checked = true;
                        }
                        // Kadın
                        else if (Convert.ToInt16(reader["m_cinsiyet"]) == 0)
                        {
                            radioButton_musteri_sec_kadin.Checked = true;
                        }
                        // Belirsiz/Atanmamış
                        else
                        {
                            radioButton_musteri_sec_erkek.Checked = false;
                            radioButton_musteri_sec_kadin.Checked = false;
                        }

                        textBox_musteri_sec_tel_no.Text = Convert.ToString(reader["m_tel_no"]);
                        textBox_musteri_sec_eposta.Text = Convert.ToString(reader["m_eposta"]);
                        textBox_musteri_sec_acik_adres.Text = Convert.ToString(reader["m_acik_adres"]);
                        comboBox_musteri_sec_kan_grubu.Text = Convert.ToString(reader["m_kan_grubu"]);

                        MusteriTCNo = textBox_musteri_sec_tc.Text;
                        MusteriAd = textBox_musteri_sec_ad.Text;
                        MusteriSoyad = textBox_musteri_sec_soyad.Text;
                        button_sec_ve_ilerle.Enabled = true;

                        button_sec_ve_ilerle.Enabled = true;
                    }
                    else
                    {
                        MusteriSecFormunuEskiHalineGetir();
                        MessageBox.Show("Müşteri bulunamadı", "Müşteri Sorgulama");
                    }


                }
                catch (Exception ex)
                {

                    MessageBox.Show("SQL Query sırasında hata oluştu! Hata: " + ex.ToString());

                }
                finally
                {

                    if (connect != null)
                    {

                        connect.Close();

                    }

                }

            }
            // T.C Sorgu alanı boş ise
            else
            {
                MusteriSecFormunuEskiHalineGetir();
                MessageBox.Show("TC No alanı boş bırakılamaz", "Hatalı Sorgulama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


        // ---------- * Müşteri Seç Sekmesi; Geri Dön * ----------
        private void button_oda_sece_geri_don_Click(object sender, EventArgs e)
        {
            //tabControl_conteyner.TabPages.Add(tabPage_oda_sec);
            //tabControl_conteyner.TabPages.Remove(tabPage_musteri_sec);
            tabControl_conteyner.SelectedTab = tabPage_oda_sec;
            // Rezarvasyon tipini sıfırla!
            RezervasyonTipi = 0;
            MusteriSecFormunuEskiHalineGetir();
        }

        // ---------- * Müşteri Seç Sekmesi; İleri * ----------

        // Rezervasyonu onayla sekmesine gider-------
        private void button_sec_ve_ilerle_Click(object sender, EventArgs e)
        {
            SelectQuery SelectQuery = new SelectQuery();

            textBox_islem_yapan_personel.Text = SelectQuery.WithIDQueryPersonel(connectionString, GlobalUserID);

            textBox_musteri_bilgileri_tcno.Text = MusteriTCNo;
            textBox_musteri_bilgileri_ad.Text = MusteriAd;
            textBox_musteri_bilgileri_soyad.Text = MusteriSoyad;
            textBox_secilen_oda_no.Text = SecilenOdaNo;

            //textBox_rezervasyon_tipi.Text = RezervasyonTipi.ToString();
            // 1 normal rezervasyon, 2 oda ayırtmadan normale, 3 oda ayırma
            if (RezervasyonTipi == 1)
            {
                textBox_rezervasyon_tipi.Text = "Normal Rezervasyon";
                dateTimePicker_rezervasyon_cikis_tarihi.Visible = false;
                dateTimePicker_rezervasyon_cikis_tarihi.Enabled = false;

            }
            else if (RezervasyonTipi == 2)
            {
                textBox_rezervasyon_tipi.Text = "Oda Ayırtmadan Normal Rezervasyona";
                dateTimePicker_rezervasyon_cikis_tarihi.Visible = false;
                dateTimePicker_rezervasyon_cikis_tarihi.Enabled = false;

            }
            else
            {
                dateTimePicker_rezervasyon_cikis_tarihi.Visible = true;
                dateTimePicker_rezervasyon_cikis_tarihi.Enabled = true;
                textBox_rezervasyon_tipi.Text = "Oda Ayırtma";
            }

            dateTimePicker_rezervasyon_tarihi.Value = DateTime.Now;

            tabControl_conteyner.SelectedTab = tabPage_rezarvasyonu_onayla;
        }


        // ------------------------------------------- * Rezervasyonu Onayla * -------------------------------------------

        private void button_rezervasyonu_onayla_Click(object sender, EventArgs e)
        {
            // Evet-Hayır sorusu içeren bir mesaj kutusu
            DialogResult result = MessageBox.Show("Rezervasyonu onaylamak istiyor musunuz?", "Rezervasyon Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Normal Rezervasyon
                if (RezervasyonTipi == 1)
                {
                    SqlConnection connect = new SqlConnection(connectionString);
                    connect.Open();

                    SqlTransaction transaction = connect.BeginTransaction();

                    try
                    {
                        string query1 = "INSERT INTO rezervasyonlar(p_id, m_tc, oda_no, giris_tarihi, rezervasyon_durumu) VALUES(@p_id, @m_tc, @oda_no, @giris_tarihi, @rezervasyon_durumu)";

                        SqlCommand command1 = new SqlCommand(query1, connect, transaction);
                        command1.Parameters.AddWithValue("@p_id", GlobalUserID);
                        command1.Parameters.AddWithValue("@m_tc", textBox_musteri_bilgileri_tcno.Text);
                        command1.Parameters.AddWithValue("@oda_no", textBox_secilen_oda_no.Text);
                        command1.Parameters.AddWithValue("@giris_tarihi", dateTimePicker_rezervasyon_tarihi.Value);
                        command1.Parameters.AddWithValue("@rezervasyon_durumu", 1);

                        command1.ExecuteNonQuery();

                        string query2 = "UPDATE odalar SET oda_durum = @oda_durum WHERE oda_no = @uoda_no";
                        SqlCommand command2 = new SqlCommand(query2, connect, transaction);
                        command2.Parameters.AddWithValue("@oda_durum", 1);
                        command2.Parameters.AddWithValue("@uoda_no", textBox_secilen_oda_no.Text);

                        command2.ExecuteNonQuery();

                        transaction.Commit();
                    

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("SQL Query sırasında hata oluştu! Hata: " + ex.ToString());
                        transaction.Rollback();
                    }
                    finally
                    {
                        if (connect != null)
                        {
                            connect.Close();
                        }
                        MessageBox.Show("Rezervasyon başarıyla tamamlandı!", "Rezervasyon İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }

                }
                // Oda ayırtmadan normale
                else if (RezervasyonTipi == 2)
                {
                    SqlConnection connect = new SqlConnection(connectionString);
                    connect.Open();

                    SqlTransaction transaction = connect.BeginTransaction();

                    try
                    {

                        string query1 = "UPDATE rezervasyonlar SET rezervasyon_durumu = @urezervasyon_durumu, cikis_tarihi = NULL WHERE m_tc = @m_tc AND rezervasyon_durumu = @rezervasyon_durumu AND oda_no = @oda_no "; // 2

                        SqlCommand command1 = new SqlCommand(query1, connect, transaction);
                        command1.Parameters.AddWithValue("@urezervasyon_durumu", 1);
                        //command1.Parameters.AddWithValue("@cikis_tarihi", NULL);

                        command1.Parameters.AddWithValue("@m_tc", textBox_musteri_bilgileri_tcno.Text);
                        command1.Parameters.AddWithValue("@rezervasyon_durumu", 2);
                        command1.Parameters.AddWithValue("@oda_no", textBox_secilen_oda_no.Text);
                        
                        command1.ExecuteNonQuery();

                        string query2 = "UPDATE odalar SET oda_durum = @oda_durum WHERE oda_no = @uoda_no";
                        SqlCommand command2 = new SqlCommand(query2, connect, transaction);
                        command2.Parameters.AddWithValue("@oda_durum", 1);
                        command2.Parameters.AddWithValue("@uoda_no", textBox_secilen_oda_no.Text);

                        command2.ExecuteNonQuery();

                        transaction.Commit();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("SQL Query sırasında hata oluştu! Hata: " + ex.ToString());
                        transaction.Rollback();
                    }
                    finally
                    {
                        if (connect != null)
                        {
                            connect.Close();
                        }
                        MessageBox.Show("Rezervasyon başarıyla tamamlandı!", "Rezervasyon İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }

                }
                // Oda ayırtma
                else
                {
                    SqlConnection connect = new SqlConnection(connectionString);
                    connect.Open();

                    SqlTransaction transaction = connect.BeginTransaction();

                    try
                    {

                        string query1 = "INSERT INTO rezervasyonlar(p_id, m_tc, oda_no, giris_tarihi, rezervasyon_durumu, cikis_tarihi) VALUES(@p_id, @m_tc, @oda_no, @giris_tarihi, @rezervasyon_durumu, @cikis_tarihi)";

                        SqlCommand command1 = new SqlCommand(query1, connect, transaction);
                        command1.Parameters.AddWithValue("@p_id", GlobalUserID);
                        command1.Parameters.AddWithValue("@m_tc", textBox_musteri_bilgileri_tcno.Text);
                        command1.Parameters.AddWithValue("@oda_no", textBox_secilen_oda_no.Text);
                        command1.Parameters.AddWithValue("@giris_tarihi", dateTimePicker_rezervasyon_tarihi.Value);
                        command1.Parameters.AddWithValue("@rezervasyon_durumu", 2);
                        command1.Parameters.AddWithValue("@cikis_tarihi", dateTimePicker_rezervasyon_cikis_tarihi.Value);

                        command1.ExecuteNonQuery();

                        string query2 = "UPDATE odalar SET oda_durum = @oda_durum WHERE oda_no = @uoda_no";
                        SqlCommand command2 = new SqlCommand(query2, connect, transaction);
                        command2.Parameters.AddWithValue("@oda_durum", 2);
                        command2.Parameters.AddWithValue("@uoda_no", textBox_secilen_oda_no.Text);
                        command2.ExecuteNonQuery();

                        transaction.Commit();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("SQL Query sırasında hata oluştu! Hata: " + ex.ToString());
                        transaction.Rollback();
                    }
                    finally
                    {
                        if (connect != null)
                        {
                            connect.Close();
                        }
                        MessageBox.Show("Rezervasyon başarıyla tamamlandı!", "Rezervasyon İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                }

            }// Messagebox. If
        }

        // ---------- * Rezervasyonu Onayla; Geri Dön * ----------
        private void button_musteri_sece_geri_don_Click(object sender, EventArgs e)
        {


            textBox_musteri_bilgileri_tcno.Text = string.Empty;
            textBox_musteri_bilgileri_ad.Text = string.Empty;
            textBox_musteri_bilgileri_soyad.Text = string.Empty;

            textBox_secilen_oda_no.Text = string.Empty;
            textBox_islem_yapan_personel.Text = string.Empty;

            textBox_rezervasyon_tipi.Text = string.Empty;

            // Müşteri seç sekmesine
            tabControl_conteyner.SelectedTab = tabPage_musteri_sec;

        }


        // ------------------------------------------- * Metotlar * -------------------------------------------

        // DynamicIyems' dan gelen "son_secilen_oda" değerini çalıştırıyor.
        private void DynamicButton_ButtonClick(string son_secilen_oda)
        {
            // Ana formda 'son_secilen_oda' değerini kullanır
            label_son_secilen_oda.Text = son_secilen_oda;
        }

        private void MusteriSecFormunuEskiHalineGetir()
        {

            // İlk yapılacak şeyler
            // Sol taraf
            textBox_musteri_sec_tc.Enabled = true;
            textBox_musteri_sec_tc.Text = string.Empty;
            textBox_musteri_sec_ad.Text = string.Empty;
            textBox_musteri_sec_soyad.Text = string.Empty;
            radioButton_musteri_sec_erkek.Checked = false;
            radioButton_musteri_sec_kadin.Checked = false;
            textBox_musteri_sec_tel_no.Text = string.Empty;// 10 karekter
            textBox_musteri_sec_eposta.Text = string.Empty;
            textBox_musteri_sec_acik_adres.Text = string.Empty;
            //comboBox_musteri_sec_kan_grubu.Text = string.Empty;
            comboBox_musteri_sec_kan_grubu.SelectedIndex = -1;
            button_sec_ve_ilerle.Enabled = false;
            button_tc_ile_musteri_sorgula.Enabled = true;

            // Varible
            MusteriTCNo = string.Empty;
            MusteriAd = string.Empty;
            MusteriSoyad = string.Empty;
            // RezervasyonTipi = 0;

        }

        // 'textBox_sorgula_tcno_ms' harf girilirse...
        private void textBox_musteri_sec_tc_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Rakamlara ve silme (backspace)e izin ver
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        // Oda no ile oda durum sorgulama
        private int RoomSituationShow(int RoomNo)
        {
            int RoomSituation = 0;
            SqlConnection connect = new SqlConnection(connectionString);

            try
            {
                connect.Open();

                string query = "SELECT oda_durum FROM odalar WHERE oda_no = @oda_no";

                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@oda_no", label_son_secilen_oda.Text);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    RoomSituation = OdaDurum = Convert.ToInt16(reader[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Query sırasında hata oluştu! Hata: " + ex.ToString());
            }
            finally
            {

                if (connect != null)
                {
                    connect.Close();
                }
            }

            return RoomSituation;
        }

        // Odanın çıkış tarihi bugünden büyük mü, küçük mü görme?
        private int QueryReservedRooms(string RoomNo)
        {
            int Result = 0;
            SqlConnection connect = new SqlConnection(connectionString);
            // MessageBox.Show("oda no:  " + RoomNo);

            try
            {
                connect.Open();
                string ExitDateVar = string.Empty;

                string SituationQuery = "SELECT cikis_tarihi FROM rezervasyonlar WHERE oda_no = @oda_no AND rezervasyon_durumu = @rezervasyon_durumu"; //2
                SqlCommand SituationCommnd = new SqlCommand(SituationQuery, connect);
                SituationCommnd.Parameters.AddWithValue("@oda_no", RoomNo);
                SituationCommnd.Parameters.AddWithValue("@rezervasyon_durumu", 2);


                SqlDataReader SituationReader = SituationCommnd.ExecuteReader();

                while (SituationReader.Read())
                {
                    ExitDateVar = Convert.ToString(SituationReader[0]);
                }

                DateTime DateNow = DateTime.ParseExact(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff"), "dd.MM.yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);

                // MessageBox.Show("Çıkış tarihi: " + ExitDateVar);
                // MessageBox.Show("Bügün tarihi: " + DateNow);

                DateTime.TryParse(ExitDateVar, out DateTime ExitDate);

                // ExitDate, DateNow dan daha büyük
                Result = DateTime.Compare(ExitDate, DateNow);

            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Query sırasında hata oluştu! Hata: " + ex.ToString());
            }
            finally
            {

                if (connect != null)
                {
                    connect.Close();
                }
            }

            return Result;
        }

        // Çıkış tarihi, bugünün tarihini geçmesni durumunda; odayı 'boş oda' olarak ata ve rezervasyonu kapat! (3)
        private void RoomAndReservationInformationUpdate(string RoomNo)
        {
            int Result = 0;

            SqlConnection connect = new SqlConnection(connectionString);
            connect.Open();

            SqlTransaction transaction = connect.BeginTransaction();

            try
            {
                // İlk işlem: 

                // 1. Sorgu: odanın durumunu boş oda olarak ayarlamak
                string Query1 = "UPDATE odalar SET oda_durum = @oda_durum WHERE oda_no = @oda_no";

                SqlCommand command1 = new SqlCommand(Query1, connect, transaction);

                command1.Parameters.AddWithValue("@oda_durum", 0);
                command1.Parameters.AddWithValue("@oda_no", RoomNo);
                command1.ExecuteNonQuery();

                // 2. Sorgu: Odaya ait rezarvasyon durumunu '3' olarak ayarlamak. 3 = Geçmiş Oda ayırtma işlemi
                string Query2 = "UPDATE rezervasyonlar SET rezervasyon_durumu = @rezervasyon_durumu_guncelle WHERE oda_no = @oda_no AND rezervasyon_durumu = @rezervasyon_durumu";

                SqlCommand command2 = new SqlCommand(Query2, connect, transaction);

                command2.Parameters.AddWithValue("@rezervasyon_durumu_guncelle", 3); // Geçmiş ayırtma işlemi
                command2.Parameters.AddWithValue("@oda_no", RoomNo);
                command2.Parameters.AddWithValue("@rezervasyon_durumu", 2);
                command2.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Query sırasında hata oluştu! Hata: " + ex.ToString());
                transaction.Rollback();
            }
            finally
            {

                if (connect != null)
                {
                    connect.Close();
                }
            }

        }
    }
}
