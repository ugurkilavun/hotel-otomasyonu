using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Sessions; // Namesapce
using IDCreaters;


namespace hotel_otomasyonu
{
    public partial class personnel_operations_form : Form
    {

        private UserInformation SessionUserInformation;
        public personnel_operations_form(UserInformation GetSessionUserInformation)
        {
            InitializeComponent();
            // Constructor içinde alınan 'GetSession_UserInformation' nesnesini 'Session_UserInformation' üye değişkenine atar
            SessionUserInformation = GetSessionUserInformation; // this.
        }

        // Form içi global değişkenler
        private string connectionString = ConnectionStringClass.ConnectionStringVarible(); // Veri tabanı bağlantısı
        string? globalUserID = String.Empty;

        private void personnel_operations_form_Load(object sender, EventArgs e)
        {
            globalUserID = SessionUserInformation.userID;
            //MessageBox.Show("User id " + globalUserID);
            KullaniciYetkiDurumunaGoreCombobox(globalUserID);

            dateTimePicker_musteri_ekle_giris_tarihi.MinDate = DateTime.Now;
            comboBox_personel_ekle_kan_grubu.SelectedIndex = 0;
        }

        // Personel Ekle
        private void button_personeli_ekle_Click(object sender, EventArgs e)
        {
            if (textBox_personel_ekle_ad.Text == string.Empty || textBox_personel_ekle_soyad.Text == string.Empty || textBox_personel_ekle_tel_no.Text == string.Empty || textBox_personel_ekle_kullaniciadi.Text == string.Empty || textBox_personel_ekle_sifre.Text == string.Empty)
            {
                MessageBox.Show("'*' ile belirtilen yerler mecburi olarak dolurulması gerekmektedir!", "Personel Ekle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Personele özel ID oluşturma metodu
                UserIDCreater UserIDCreater = new UserIDCreater();
                string Userid = UserIDCreater.CreatedUserID();
                MessageBox.Show("Oluşturulan ID: " + Userid);

                SqlConnection connect = new SqlConnection(connectionString);
                connect.Open();

                SqlTransaction transaction = connect.BeginTransaction();

                // 1. Sorgu için gerekli olanlar 
                int cinsiyet = 1;
                if (radioButton_personel_ekle_cinsiyet_erkek.Checked == true)
                {
                    cinsiyet = 1;
                }
                else
                {
                    cinsiyet = 0;
                }

                // 2. Sorgu için gerekli olanlar 
                int yetki = 0;
                if (comboBox_personel_ekle_yetki_durumu.SelectedItem == "Müdür") 
                {
                    yetki = 2;
                }
                else if (comboBox_personel_ekle_yetki_durumu.SelectedItem == "Müdür Yardımcısı")
                {
                    yetki = 1;
                }
                else if (comboBox_personel_ekle_yetki_durumu.SelectedItem == "Personel")
                {
                    yetki = 0;
                }
                else
                {
                    yetki = 0;
                }

                int aktiflik = 0;
                if (radioButton_personel_ekle_aktiflik_aktif.Checked == true)
                {
                    aktiflik = 1;
                }
                else 
                { 
                    aktiflik = 0; 
                }


                try
                {
                    // 1. Sorgu
                    string personelBilgileriQuery = "INSERT INTO personel_bilgileri(p_id, p_ad, p_soyad, p_cinsiyet, p_ise_baslama_tarihi, p_tel_no, p_eposta, p_kan_grubu, p_maas) VALUES(@p_id, @p_ad, @p_soyad, @p_cinsiyet, @p_ise_baslama_tarihi, @p_tel_no, @p_eposta, @p_kan_grubu, @p_maas)";
                    SqlCommand personelBilgileriCommand = new SqlCommand(personelBilgileriQuery, connect, transaction);
                    personelBilgileriCommand.Parameters.AddWithValue("@p_id", Userid);
                    personelBilgileriCommand.Parameters.AddWithValue("@p_ad", textBox_personel_ekle_ad.Text);
                    personelBilgileriCommand.Parameters.AddWithValue("@p_soyad", textBox_personel_ekle_soyad.Text);
                    personelBilgileriCommand.Parameters.AddWithValue("@p_cinsiyet", cinsiyet);
                    personelBilgileriCommand.Parameters.AddWithValue("@p_ise_baslama_tarihi", dateTimePicker_musteri_ekle_giris_tarihi.Value);
                    personelBilgileriCommand.Parameters.AddWithValue("@p_tel_no", textBox_personel_ekle_tel_no.Text);
                    personelBilgileriCommand.Parameters.AddWithValue("@p_eposta", textBox_personel_ekle_eposta.Text);
                    personelBilgileriCommand.Parameters.AddWithValue("@p_kan_grubu", comboBox_personel_ekle_kan_grubu.SelectedItem);
                    personelBilgileriCommand.Parameters.AddWithValue("@p_maas", textBox_personel_ekle_maas.Text);
                    int countInsert1 = Convert.ToInt16(personelBilgileriCommand.ExecuteNonQuery());
                    if (countInsert1 > 0) {
                        MessageBox.Show("1. Sorgu başarıyla çalıştı.");
                    }
                    else
                    {
                        MessageBox.Show("1. Sorgu çalışmadı!");
                    }

                    // 2. Sorgu
                    string personelGirisBilgileriQuery = "INSERT INTO personel_giris_bilgileri(p_g_id, p_g_yetki_durumu, p_g_aktiflik_durumu, p_g_kullanici_ad, p_g_sifre) VALUES(@p_g_id, @p_g_yetki_durumu, @p_g_aktiflik_durumu, @p_g_kullanici_ad, @p_g_sifre)";
                    SqlCommand personelGirisBilgileriCommand = new SqlCommand(personelGirisBilgileriQuery, connect, transaction);
                    personelGirisBilgileriCommand.Parameters.AddWithValue("@p_g_id", Userid);
                    personelGirisBilgileriCommand.Parameters.AddWithValue("@p_g_yetki_durumu", yetki);
                    personelGirisBilgileriCommand.Parameters.AddWithValue("@p_g_aktiflik_durumu", aktiflik);
                    personelGirisBilgileriCommand.Parameters.AddWithValue("@p_g_kullanici_ad", textBox_personel_ekle_kullaniciadi.Text);
                    personelGirisBilgileriCommand.Parameters.AddWithValue("@p_g_sifre", textBox_personel_ekle_sifre.Text);
                    int countInsert2 = Convert.ToInt16(personelGirisBilgileriCommand.ExecuteNonQuery());
                    if (countInsert2 > 0)
                    {
                        MessageBox.Show("2. Sorgu başarıyla çalıştı.");
                    }
                    else
                    {
                        MessageBox.Show("2. Sorgu çalışmadı!");
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
                    MessageBox.Show("Personel başarılı bir şekilde eklendi.", "Personel Ekle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
        // -------------------------------------------------------------- * Metotlar * --------------------------------------------------------------
        private void KullaniciYetkiDurumunaGoreCombobox(string pID)
        {
            comboBox_personel_ekle_yetki_durumu.Items.Clear();
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
                        comboBox_personel_ekle_yetki_durumu.Items.Add("Müdür");
                        comboBox_personel_ekle_yetki_durumu.Items.Add("Müdür Yardımcısı");
                        comboBox_personel_ekle_yetki_durumu.Items.Add("Personel");

                        comboBox_personel_ekle_yetki_durumu.SelectedIndex = 2;
                    }
                    // Müdür Yardımcısı
                    else if (yetki == 1)
                    {
                        comboBox_personel_ekle_yetki_durumu.Items.Add("Müdür Yardımcısı");
                        comboBox_personel_ekle_yetki_durumu.Items.Add("Personel");

                        comboBox_personel_ekle_yetki_durumu.SelectedIndex = 1;
                    }
                    // Personel
                    else
                    {
                        comboBox_personel_ekle_yetki_durumu.Items.Add("Personel");

                        comboBox_personel_ekle_yetki_durumu.SelectedIndex = 0;
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox_personel_ekle_tel_no_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
