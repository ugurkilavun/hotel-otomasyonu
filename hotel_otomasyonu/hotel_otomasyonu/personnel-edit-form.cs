using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sessions;
using System.Security.Cryptography;
using Microsoft.VisualBasic.ApplicationServices;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Diagnostics.Eventing.Reader;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace hotel_otomasyonu
{
    public partial class personnel_edit_form : Form
    {
        private UserInformation SessionUserInformation;
        public personnel_edit_form(UserInformation GetSessionUserInformation)
        {
            InitializeComponent();

            dataGridView_personeller.SelectionChanged += dataGridView_personeller_SelectionChanged;
            SessionUserInformation = GetSessionUserInformation;
        }

        // Form içi global değişkenler
        private string connectionString = ConnectionStringClass.ConnectionStringVarible(); // Veri tabanı bağlantısı
        DataTable datatable = new DataTable();
        string? globalUserID = string.Empty;
        int globalAuthority = 0;
        string? globalPersonalID = null;

        private void personnel_edit_form_Load(object sender, EventArgs e)
        {
            // dateTimePicker_musteri_ekle_cikis_tarihi.MinDate = DateTime.Now;
            //dateTimePicker_musteri_ekle_giris_tarihi.MinDate = DateTime.Now;

            // Yetki Durumu
            globalUserID = SessionUserInformation.userID;
            KullaniciYetkiDurumunaGoreCombobox(globalUserID);

            // Gridview 
            dataGridViewStyle();
            DataTableHeaderColumn(datatable);

            //
            comboBox_personel_duzenle_kan_grubu.SelectedIndex = 0;
        }

        private void button_sorgula_Click(object sender, EventArgs e)
        {
            AllPasifAndClear();

            if (textBox_sorgula_ad.Text == string.Empty)
            {
                MessageBox.Show("'Ad' alanı boş bırakılamaz.", "Personel Sorgula", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                datatable.Rows.Clear();
                //dataGridView_personeller.DataSource = datatable;
                SqlConnection connect = new SqlConnection(connectionString);
                try
                {
                    connect.Open();

                    string query = "SELECT * FROM personel_bilgileri WHERE p_ad LIKE '%' + @p_ad + '%' AND p_soyad LIKE '%' + @p_soyad + '%'  "; //  OR p_soyad LIKE '%' + @p_soyad + '%'   

                    SqlCommand command = new SqlCommand(query, connect);
                    command.Parameters.AddWithValue("@p_ad", textBox_sorgula_ad.Text);
                    command.Parameters.AddWithValue("@p_soyad", textBox_sorgula_soyad.Text);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        reader.Close();
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string? cinsiyet = string.Empty;
                            if (Convert.ToInt16(reader[3]) == 1)
                            {
                                cinsiyet = "Erkek";
                            }
                            else
                            {
                                cinsiyet = "Kadın";
                            }

                            datatable.Rows.Add(reader[0], reader[1], reader[2], cinsiyet, reader[4], reader[5], reader[6], reader[7], reader[8], reader[9]);
                        }

                        dataGridView_personeller.DataSource = datatable;
                        reader.Close();
                    }
                    else
                    {
                        MessageBox.Show("Personel bulunamadı!", "Personel Sorgula", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    //this.Close();
                }
            }
        }

        private void button_personel_bilgilerini_düzenle_Click(object sender, EventArgs e)
        {
            if (textBox_personel_duzenle_ad.Text != null || textBox_personel_duzenle_soyad.Text != null || textBox_personel_duzenle_tel_no.Text != null || textBox_personel_duzenle_kullaniciadi.Text != null || textBox_personel_duzenle_sifre.Text != null)
            {
                MessageBox.Show("Personel ID: " + globalPersonalID);
                if (checkBox_cikis_tarihi.Checked == true)
                {
                    MessageBox.Show("checkBox_cikis_tarihi == true");
                    personelInformationUpdateExitDateFull(dateTimePicker_musteri_ekle_cikis_tarihi);
                }
                else
                {
                    MessageBox.Show("checkBox_cikis_tarihi == false");
                    personelInformationUpdateExitDateNull();
                }

                
            }
            else
            {
                MessageBox.Show("'*' ile gösterilen alanlar zorunlu olarak doldurulması gerekmektedir", "Personel Düzenle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // --------------------------------------------------* Metotlar *--------------------------------------------------

        private void dataGridViewStyle()
        {
            dataGridView_personeller.BackgroundColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Control); ; //  dataGridView_gecmis_rezarvasyonlar: genel arka plan rengi
            dataGridView_personeller.BorderStyle = BorderStyle.None;
            dataGridView_personeller.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView_personeller.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView_personeller.DefaultCellStyle.SelectionBackColor = Color.Beige; // Seçilen satırın arka plan rengi
            dataGridView_personeller.DefaultCellStyle.SelectionForeColor = Color.FromArgb(64, 64, 64);// Seçilen satırın metin rengi
            dataGridView_personeller.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing; // Opsiyonel
            dataGridView_personeller.RowHeadersWidth = 25; // dataGridView_gecmis_rezarvasyonlar.RowHeadersVisible = false;
            dataGridView_personeller.DefaultCellStyle.Font = new Font("Franklin Gothic Book", 9);


            // Sütunlar
            dataGridView_personeller.ColumnHeadersDefaultCellStyle.Font = new Font("Franklin Gothic Book", 9, FontStyle.Bold);
            dataGridView_personeller.EnableHeadersVisualStyles = false;
            dataGridView_personeller.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView_personeller.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64); // Sütun başlıklarının arka plan rengi
            dataGridView_personeller.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView_personeller.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Sütun boyutunu otomatik ayarla
        }

        private void DataTableHeaderColumn(DataTable datatable)
        {
            //DataTable'a sütunlar ekleyin (örneğin, "ID" ve "Ad" sütunları)
            //Rezervasyon ID, Personel ID, Müşteri T.C, Oda No, Giriş Tarihi, Rezarvasyon Durumu, Çıkış Tarihi, Ucret
            //datatable.Columns.Add("Rezarvasyon ID", typeof(string));
            //datatable.Columns.Add("Personel ID", typeof(string));
            //datatable.Columns.Add("T.C No", typeof(string));
            //datatable.Columns.Add("Oda No", typeof(string));
            //datatable.Columns.Add("Giriş Tarihi", typeof(string));
            //datatable.Columns.Add("Rezarvasyon Durumu", typeof(string));
            //datatable.Columns.Add("Çıkış Tarihi", typeof(string));
            // datatable.Columns.Add("Ücret", typeof(string));

            // Yeni
            datatable.Columns.Add("Personel ID", typeof(string));
            datatable.Columns.Add("Ad", typeof(string));
            datatable.Columns.Add("Soyad", typeof(string));
            datatable.Columns.Add("Cinsiyet", typeof(string));
            datatable.Columns.Add("İşe Başlama Tarihi", typeof(string));
            datatable.Columns.Add("İşten Çıkış Tarihi", typeof(string));
            datatable.Columns.Add("Tel No", typeof(string));
            datatable.Columns.Add("E-Posta", typeof(string));
            datatable.Columns.Add("Kan Grubu", typeof(string));
            datatable.Columns.Add("Maaş", typeof(string));

            dataGridView_personeller.DataSource = datatable;
        }

        private void AllActiveAndClear()
        {
            // Temizle
            textBox_personel_duzenle_ad.Text = string.Empty;
            textBox_personel_duzenle_soyad.Text = string.Empty;
            radioButton_personel_duzenle_cinsiyet_erkek.Checked = true;
            radioButton_personel_duzenle_cinsiyet_kadin.Checked = false;
            dateTimePicker_musteri_ekle_giris_tarihi.Value = DateTime.Now;
            dateTimePicker_musteri_ekle_cikis_tarihi.Value = DateTime.Now;
            textBox_personel_duzenle_tel_no.Text = string.Empty;
            textBox_personel_duzenle_eposta.Text = string.Empty;

            textBox_personel_duzenle_maas.Text = string.Empty;
            comboBox_personel_duzenle_yetki_durumu.Enabled = true;
            radioButton_personel_duzenle_aktiflik_aktif.Checked = true;
            radioButton_personel_duzenle_aktiflik_pasif.Checked = false;
            textBox_personel_duzenle_kullaniciadi.Text = string.Empty;
            textBox_personel_duzenle_sifre.Text = string.Empty;

            // Aktif ET
            textBox_personel_duzenle_ad.Enabled = true;
            textBox_personel_duzenle_soyad.Enabled = true;
            radioButton_personel_duzenle_cinsiyet_erkek.Enabled = true;
            radioButton_personel_duzenle_cinsiyet_kadin.Enabled = true;
            dateTimePicker_musteri_ekle_giris_tarihi.Enabled = true;
            //dateTimePicker_musteri_ekle_cikis_tarihi.Enabled = true;
            textBox_personel_duzenle_tel_no.Enabled = true;
            textBox_personel_duzenle_eposta.Enabled = true;
            comboBox_personel_duzenle_kan_grubu.Enabled = true;
            textBox_personel_duzenle_maas.Enabled = true;

            comboBox_personel_duzenle_yetki_durumu.Enabled = true;
            radioButton_personel_duzenle_aktiflik_aktif.Enabled = true;
            radioButton_personel_duzenle_aktiflik_pasif.Enabled = true;
            textBox_personel_duzenle_kullaniciadi.Enabled = true;
            textBox_personel_duzenle_sifre.Enabled = true;

            button_personel_bilgilerini_düzenle.Enabled = true;
        }

        private void AllPasifAndClear()
        {
            // Temizle
            textBox_personel_duzenle_ad.Text = string.Empty;
            textBox_personel_duzenle_soyad.Text = string.Empty;
            radioButton_personel_duzenle_cinsiyet_erkek.Checked = true;
            radioButton_personel_duzenle_cinsiyet_kadin.Checked = false;
            dateTimePicker_musteri_ekle_giris_tarihi.Value = DateTime.Now;
            dateTimePicker_musteri_ekle_cikis_tarihi.Value = DateTime.Now;
            textBox_personel_duzenle_tel_no.Text = string.Empty;
            textBox_personel_duzenle_eposta.Text = string.Empty;
            comboBox_personel_duzenle_kan_grubu.SelectedIndex = -1;
            textBox_personel_duzenle_maas.Text = string.Empty;

            comboBox_personel_duzenle_yetki_durumu.SelectedIndex = -1;
            radioButton_personel_duzenle_aktiflik_aktif.Checked = true;
            radioButton_personel_duzenle_aktiflik_pasif.Checked = false;
            textBox_personel_duzenle_kullaniciadi.Text = string.Empty;
            textBox_personel_duzenle_sifre.Text = string.Empty;

            globalPersonalID = null;

            // Pasif ET
            textBox_personel_duzenle_ad.Enabled = false;
            textBox_personel_duzenle_soyad.Enabled = false;
            radioButton_personel_duzenle_cinsiyet_erkek.Enabled = false;
            radioButton_personel_duzenle_cinsiyet_kadin.Enabled = false;
            dateTimePicker_musteri_ekle_giris_tarihi.Enabled = false;
            //dateTimePicker_musteri_ekle_cikis_tarihi.Enabled = false;
            textBox_personel_duzenle_tel_no.Enabled = false;
            textBox_personel_duzenle_eposta.Enabled = false;
            comboBox_personel_duzenle_kan_grubu.Enabled = false;
            textBox_personel_duzenle_maas.Enabled = false;

            comboBox_personel_duzenle_yetki_durumu.Enabled = false;
            radioButton_personel_duzenle_aktiflik_aktif.Enabled = false;
            radioButton_personel_duzenle_aktiflik_pasif.Enabled = false;
            textBox_personel_duzenle_kullaniciadi.Enabled = false;
            textBox_personel_duzenle_sifre.Enabled = false;

            button_personel_bilgilerini_düzenle.Enabled = false;
        }

        private void dataGridView_personeller_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_personeller.SelectedRows.Count > 0)
            {
                // MessageBox.Show("Göster");
                DataGridViewRow selectedRow = dataGridView_personeller.SelectedRows[0];

                
                // MessageBox.Show(selectedRow.Cells["Personel ID"].Value.ToString());
                FillTextboxs(selectedRow.Cells["Personel ID"].Value.ToString());
            }

            //textBox_kat_duzenle_kat_no.Text = selectedRow.Cells["Kat No"].Value.ToString();
            //textBox_kat_duzenle_kat_aciklama.Text = selectedRow.Cells["Kat Açıklama"].Value.ToString();
        }

        private void KullaniciYetkiDurumunaGoreCombobox(string pID)
        {
            comboBox_personel_duzenle_yetki_durumu.Items.Clear();
            SqlConnection connect = new SqlConnection(connectionString);

            try
            {
                connect.Open();

                string query = "SELECT p_g_yetki_durumu FROM personel_giris_bilgileri WHERE p_g_id = @p_id";
                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@p_id", pID);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int yetki = Convert.ToInt16(reader["p_g_yetki_durumu"]);
                    // Müdür
                    if (yetki == 2)
                    {
                        comboBox_personel_duzenle_yetki_durumu.Items.Add("Müdür");
                        comboBox_personel_duzenle_yetki_durumu.Items.Add("Müdür Yardımcısı");
                        comboBox_personel_duzenle_yetki_durumu.Items.Add("Personel");

                        comboBox_personel_duzenle_yetki_durumu.SelectedIndex = 2;
                        globalAuthority = Convert.ToInt16(reader["p_g_yetki_durumu"]);
                    }
                    // Müdür Yardımcısı
                    else if (yetki == 1)
                    {
                        comboBox_personel_duzenle_yetki_durumu.Items.Add("Müdür Yardımcısı");
                        comboBox_personel_duzenle_yetki_durumu.Items.Add("Personel");

                        comboBox_personel_duzenle_yetki_durumu.SelectedIndex = 1;
                        globalAuthority = Convert.ToInt16(reader["p_g_yetki_durumu"]);

                    }
                    // Personel
                    else
                    {
                        comboBox_personel_duzenle_yetki_durumu.Items.Add("Personel");

                        comboBox_personel_duzenle_yetki_durumu.SelectedIndex = 0;
                        globalAuthority = Convert.ToInt16(reader["p_g_yetki_durumu"]);

                    }
                }
                else
                {
                    MessageBox.Show("İşlem yapılan ID ile bir personel bulunamadı. Lütfen hatayı yetkili kişilere iletiniz!", "Personel Ekle", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.Close();
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
        private void FillTextboxs(string UserID)
        {
            SqlConnection connect = new SqlConnection(connectionString);

            try
            {
                connect.Open();

                string yetkiDurumuQuery = "SELECT p_g_yetki_durumu FROM personel_giris_bilgileri WHERE p_g_id = @p_g_id";

                SqlCommand yetkiDurumuCommand = new SqlCommand(yetkiDurumuQuery, connect);
                yetkiDurumuCommand.Parameters.AddWithValue("@p_g_id", UserID);
                SqlDataReader yetkiDurumureader = yetkiDurumuCommand.ExecuteReader();

                int yetkiDurumu = 0;
                while (yetkiDurumureader.Read())
                {
                    yetkiDurumu = Convert.ToInt16(yetkiDurumureader[0]);
                }
                yetkiDurumureader.Close();

                // Düzenleme yapcacağı kişinin yetki durumu ile şu anda sistemde olan kişinin yetki durumu karşılaştırması!
                // globalAuthority == yetkiDurumu
                if (yetkiDurumu > globalAuthority)
                {
                    AllPasifAndClear();
                    MessageBox.Show("Yetki durumu sizinkinden büyük olan personele/personellere düzenleme yapılamaz.", "Personel Düzenle", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    // Her şeyi aktif et
                    AllActiveAndClear();

                    string personelBilgileriQuery = "SELECT * FROM personel_bilgileri WHERE p_id = @p_id";

                    SqlCommand personelBilgileriCommand = new SqlCommand(personelBilgileriQuery, connect);
                    personelBilgileriCommand.Parameters.AddWithValue("@p_id", UserID);
                    SqlDataReader personelBilgilerireader = personelBilgileriCommand.ExecuteReader();

                    while (personelBilgilerireader.Read())
                    {

                        textBox_personel_duzenle_ad.Text = personelBilgilerireader[1].ToString();
                        textBox_personel_duzenle_soyad.Text = personelBilgilerireader[2].ToString();

                        if (Convert.ToInt16(personelBilgilerireader[3]) == 1)
                        {
                            radioButton_personel_duzenle_cinsiyet_erkek.Checked = true;
                            radioButton_personel_duzenle_cinsiyet_kadin.Checked = false;
                        }
                        else
                        {
                            radioButton_personel_duzenle_cinsiyet_erkek.Checked = false;
                            radioButton_personel_duzenle_cinsiyet_kadin.Checked = true;
                        }
                        dateTimePicker_musteri_ekle_giris_tarihi.Text = personelBilgilerireader[4].ToString();

                        if (personelBilgilerireader[5].ToString() != string.Empty)
                        {
                            checkBox_cikis_tarihi.Checked = true;
                            dateTimePicker_musteri_ekle_cikis_tarihi.Text = personelBilgilerireader[5].ToString();
                        }
                        else
                        {
                            checkBox_cikis_tarihi.Checked = false;
                        }
                        // dateTimePicker_musteri_ekle_cikis_tarihi.Text = reader[5].ToString();
                        textBox_personel_duzenle_tel_no.Text = personelBilgilerireader[6].ToString();
                        textBox_personel_duzenle_eposta.Text = personelBilgilerireader[7].ToString();
                        comboBox_personel_duzenle_kan_grubu.Text = personelBilgilerireader[8].ToString();
                        textBox_personel_duzenle_maas.Text = personelBilgilerireader[9].ToString();

                        //comboBox_personel_duzenle_yetki_durumu.Enabled = true;
                        //radioButton_personel_duzenle_aktiflik_aktif.Enabled = true;
                        //radioButton_personel_duzenle_aktiflik_pasif.Enabled = true;
                        //textBox_personel_duzenle_kullaniciadi.Enabled = true;
                        //textBox_personel_duzenle_sifre.Enabled = true;
                    }
                    personelBilgilerireader.Close();

                    string personelGirisBilgileriQuery = "SELECT * FROM personel_giris_bilgileri WHERE p_g_id = @p_g_id";

                    SqlCommand personelGirisBilgileriCommand = new SqlCommand(personelGirisBilgileriQuery, connect);
                    personelGirisBilgileriCommand.Parameters.AddWithValue("@p_g_id", UserID);
                    SqlDataReader personelGirisBilgilerireader = personelGirisBilgileriCommand.ExecuteReader();
                    while (personelGirisBilgilerireader.Read())
                    {
                        //comboBox_personel_duzenle_yetki_durumu.Text = personelGirisBilgilerireader[1].ToString(); 
                        if (Convert.ToInt16(personelGirisBilgilerireader[1]) == 2)
                        {
                            comboBox_personel_duzenle_yetki_durumu.Text = "Müdür";
                        }
                        else if(Convert.ToInt16(personelGirisBilgilerireader[1]) == 1)
                        {
                            comboBox_personel_duzenle_yetki_durumu.Text = "Müdür Yardımcısı";
                        }
                        else
                        {
                            comboBox_personel_duzenle_yetki_durumu.Text = "Personel";
                        }


                        if (Convert.ToInt16(personelGirisBilgilerireader[2]) == 1)
                        {
                            radioButton_personel_duzenle_aktiflik_aktif.Checked = true;
                        }
                        else
                        {
                            radioButton_personel_duzenle_aktiflik_pasif.Checked = true;
                        }

                        textBox_personel_duzenle_kullaniciadi.Text = personelGirisBilgilerireader[3].ToString();
                        textBox_personel_duzenle_sifre.Text = personelGirisBilgilerireader[4].ToString();

                    }
                    personelGirisBilgilerireader.Close();
                    // Personelin ID'sini almak için!
                    globalPersonalID = UserID;

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

        private void checkBox_cikis_tarihi_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_cikis_tarihi.Checked)
            {
                dateTimePicker_musteri_ekle_cikis_tarihi.Enabled = true;
            }
            else
            {
                dateTimePicker_musteri_ekle_cikis_tarihi.Enabled = false;
            }

        }

        private void personelInformationUpdateExitDateFull(DateTimePicker DateTimePicker)
        {
            SqlConnection connect = new SqlConnection(connectionString);
            connect.Open();
            SqlTransaction transaction = connect.BeginTransaction();

            try
            {
                // 1. Sorgu için olanlar
                int cinsiyet = 0;
                if (radioButton_personel_duzenle_cinsiyet_erkek.Checked == true)
                {
                    cinsiyet = 1;
                }
                else
                {
                    cinsiyet = 0;
                }

                // 2. Sorgu için olanlar
                int yetki = 0;
                if (comboBox_personel_duzenle_yetki_durumu.Text == "Müdür")
                {
                    yetki = 2;
                }
                else if (comboBox_personel_duzenle_yetki_durumu.Text == "Müdür Yardımcısı")
                {
                    yetki = 1;
                }
                else
                {
                    yetki = 0;
                }

                int aktiflik = 0;
                if (radioButton_personel_duzenle_aktiflik_aktif.Checked == true)
                {
                    aktiflik = 1;
                }
                else
                {
                    aktiflik = 0;
                }

                // 1. Sorgu
                string personelBilgileriQuery = "UPDATE personel_bilgileri SET p_ad = @p_ad, p_soyad = @p_soyad, p_cinsiyet = @p_cinsiyet, p_ise_baslama_tarihi = @p_ise_baslama_tarihi, p_isten_cikis_tarihi = @p_isten_cikis_tarihi, p_tel_no = @p_tel_no, p_eposta = @p_eposta, p_kan_grubu = @p_kan_grubu, p_maas = @p_maas WHERE p_id = @p_id";
                SqlCommand personelBilgilericommand = new SqlCommand(personelBilgileriQuery, connect, transaction);
                // @p_ad, @p_soyad, @p_cinsiyet, @p_ise_baslama_tarihi, @p_isten_cikis_tarihi, @p_tel_no, @p_eposta, @p_kan_grubu, @p_maas
                personelBilgilericommand.Parameters.AddWithValue("@p_ad", textBox_personel_duzenle_ad.Text);
                personelBilgilericommand.Parameters.AddWithValue("@p_soyad", textBox_personel_duzenle_soyad.Text);

                personelBilgilericommand.Parameters.AddWithValue("@p_cinsiyet", cinsiyet);
                personelBilgilericommand.Parameters.AddWithValue("@p_ise_baslama_tarihi", dateTimePicker_musteri_ekle_giris_tarihi.Value);
                personelBilgilericommand.Parameters.AddWithValue("@p_isten_cikis_tarihi", DateTimePicker.Value);
                personelBilgilericommand.Parameters.AddWithValue("@p_tel_no", textBox_personel_duzenle_tel_no.Text);
                personelBilgilericommand.Parameters.AddWithValue("@p_eposta", textBox_personel_duzenle_eposta.Text);
                personelBilgilericommand.Parameters.AddWithValue("@p_kan_grubu", comboBox_personel_duzenle_kan_grubu.SelectedItem);
                personelBilgilericommand.Parameters.AddWithValue("@p_maas", textBox_personel_duzenle_maas.Text);
                // WHERE; Personel ID
                personelBilgilericommand.Parameters.AddWithValue("@p_id", globalPersonalID);

                int personelBilgileriCount = Convert.ToInt16(personelBilgilericommand.ExecuteNonQuery());
                if (personelBilgileriCount > 0)
                {
                    MessageBox.Show("1. Düzenleme başarılı!");
                }
                else
                {
                    MessageBox.Show("1. Düzenleme başarısız oldu!");
                }

                // 2. Sorgu
                string personelGirisBilgileriQuery = "UPDATE personel_giris_bilgileri SET p_g_yetki_durumu = @p_g_yetki_durumu, p_g_aktiflik_durumu = @p_g_aktiflik_durumu, p_g_kullanici_ad = @p_g_kullanici_ad, p_g_sifre = @p_g_sifre WHERE p_g_id = @p_g_id";
                SqlCommand personelGirisBilgilericommand = new SqlCommand(personelGirisBilgileriQuery, connect, transaction);
                // @p_g_yetki_durumu, @p_g_aktiflik_durumu, @p_g_kullanici_ad, @p_g_sifre
                personelGirisBilgilericommand.Parameters.AddWithValue("@p_g_yetki_durumu", yetki);
                personelGirisBilgilericommand.Parameters.AddWithValue("@p_g_aktiflik_durumu", aktiflik);
                personelGirisBilgilericommand.Parameters.AddWithValue("@p_g_kullanici_ad", textBox_personel_duzenle_kullaniciadi.Text);
                personelGirisBilgilericommand.Parameters.AddWithValue("@p_g_sifre", textBox_personel_duzenle_sifre.Text);
                // WHERE; Personel ID
                personelGirisBilgilericommand.Parameters.AddWithValue("@p_g_id", globalPersonalID);

                int personelGirisBilgileriCount = Convert.ToInt16(personelGirisBilgilericommand.ExecuteNonQuery());
                if (personelGirisBilgileriCount > 0)
                {
                    MessageBox.Show("2. Düzenleme başarılı!"); 
                }
                else
                {
                    MessageBox.Show("2. Düzenleme başarısız oldu!");
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show("SQL Query sırasında hata oluştu! Hata: " + ex.ToString());
            }
            finally
            {
                if (connect != null)
                {
                    connect.Close();
                }
                button_sorgula.PerformClick();
                MessageBox.Show("Personelin bilgileri başarılı bir şekilde düzenlendi!", "Personel Düzenle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void personelInformationUpdateExitDateNull()
        {
            SqlConnection connect = new SqlConnection(connectionString);
            connect.Open();
            SqlTransaction transaction = connect.BeginTransaction();

            try
            {
                // 1. Sorgu için olanlar
                int cinsiyet = 0;
                if (radioButton_personel_duzenle_cinsiyet_erkek.Checked == true)
                {
                    cinsiyet = 1;
                }
                else
                {
                    cinsiyet = 0;
                }

                // 2. Sorgu için olanlar
                int yetki = 0;
                if (comboBox_personel_duzenle_yetki_durumu.Text == "Müdür")
                {
                    yetki = 2;
                }
                else if (comboBox_personel_duzenle_yetki_durumu.Text == "Müdür Yardımcısı")
                {
                    yetki = 1;
                }
                else
                {
                    yetki = 0;
                }

                //
                int aktiflik = 0;
                if (radioButton_personel_duzenle_aktiflik_aktif.Checked == true)
                {
                    aktiflik = 1;
                }
                else if (radioButton_personel_duzenle_aktiflik_pasif.Checked == false)
                {
                    aktiflik = 0;
                }

                // 1. Sorgu
                string personelBilgileriQuery = "UPDATE personel_bilgileri SET p_ad = @p_ad, p_soyad = @p_soyad, p_cinsiyet = @p_cinsiyet, p_ise_baslama_tarihi = @p_ise_baslama_tarihi, p_isten_cikis_tarihi = NULL, p_tel_no = @p_tel_no, p_eposta = @p_eposta, p_kan_grubu = @p_kan_grubu, p_maas = @p_maas WHERE p_id = @p_id";
                SqlCommand personelBilgilericommand = new SqlCommand(personelBilgileriQuery, connect, transaction);
                // @p_ad, @p_soyad, @p_cinsiyet, @p_ise_baslama_tarihi, @p_isten_cikis_tarihi, @p_tel_no, @p_eposta, @p_kan_grubu, @p_maas
                personelBilgilericommand.Parameters.AddWithValue("@p_ad", textBox_personel_duzenle_ad.Text);
                personelBilgilericommand.Parameters.AddWithValue("@p_soyad", textBox_personel_duzenle_soyad.Text);

                personelBilgilericommand.Parameters.AddWithValue("@p_cinsiyet", cinsiyet);
                personelBilgilericommand.Parameters.AddWithValue("@p_ise_baslama_tarihi", dateTimePicker_musteri_ekle_giris_tarihi.Value);
                // personelBilgilericommand.Parameters.AddWithValue("@p_isten_cikis_tarihi", string.Empty); // string.Empty, null, ""
                personelBilgilericommand.Parameters.AddWithValue("@p_tel_no", textBox_personel_duzenle_tel_no.Text);
                personelBilgilericommand.Parameters.AddWithValue("@p_eposta", textBox_personel_duzenle_eposta.Text);
                personelBilgilericommand.Parameters.AddWithValue("@p_kan_grubu", comboBox_personel_duzenle_kan_grubu.SelectedItem);
                personelBilgilericommand.Parameters.AddWithValue("@p_maas", textBox_personel_duzenle_maas.Text);
                // WHERE; Personel ID
                personelBilgilericommand.Parameters.AddWithValue("@p_id", globalPersonalID);

                int personelBilgileriCount = Convert.ToInt16(personelBilgilericommand.ExecuteNonQuery());
                if (personelBilgileriCount > 0)
                {
                    MessageBox.Show("1. Düzenleme başarılı!");
                }
                else
                {
                    MessageBox.Show("1. Düzenleme başarısız oldu!");
                }

                // 2. Sorgu
                string personelGirisBilgileriQuery = "UPDATE personel_giris_bilgileri SET p_g_yetki_durumu = @p_g_yetki_durumu, p_g_aktiflik_durumu = @p_g_aktiflik_durumu, p_g_kullanici_ad = @p_g_kullanici_ad, p_g_sifre = @p_g_sifre WHERE p_g_id = @p_g_id";
                SqlCommand personelGirisBilgilericommand = new SqlCommand(personelGirisBilgileriQuery, connect, transaction);
                // @p_g_yetki_durumu, @p_g_aktiflik_durumu, @p_g_kullanici_ad, @p_g_sifre
                personelGirisBilgilericommand.Parameters.AddWithValue("@p_g_yetki_durumu", yetki);
                personelGirisBilgilericommand.Parameters.AddWithValue("@p_g_aktiflik_durumu", aktiflik);
                personelGirisBilgilericommand.Parameters.AddWithValue("@p_g_kullanici_ad", textBox_personel_duzenle_kullaniciadi.Text);
                personelGirisBilgilericommand.Parameters.AddWithValue("@p_g_sifre", textBox_personel_duzenle_sifre.Text);
                // WHERE; Personel ID
                personelGirisBilgilericommand.Parameters.AddWithValue("@p_g_id", globalPersonalID);

                int personelGirisBilgileriCount = Convert.ToInt16(personelGirisBilgilericommand.ExecuteNonQuery());
                if (personelGirisBilgileriCount > 0)
                {
                    MessageBox.Show("2. Düzenleme başarılı!");
                }
                else
                {
                    MessageBox.Show("2. Düzenleme başarısız oldu!");
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show("SQL Query sırasında hata oluştu! Hata: " + ex.ToString());
            }
            finally
            {
                if (connect != null)
                {
                    connect.Close();
                }
                button_sorgula.PerformClick();
                MessageBox.Show("Personelin bilgileri başarılı bir şekilde düzenlendi!", "Personel Düzenle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


    }
}
