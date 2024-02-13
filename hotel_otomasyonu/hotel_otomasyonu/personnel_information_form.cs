using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using Sessions;// namespace

namespace hotel_otomasyonu
{
    public partial class personnel_information_form : Form
    {
        private UserInformation SessionUserInformation;
        public personnel_information_form(UserInformation GetSessionUserInformation)
        {
            InitializeComponent();
            SessionUserInformation = GetSessionUserInformation;
        }

        // Form içi global değişkenler.
        private string connectionString = ConnectionStringClass.ConnectionStringVarible(); // Veri tabanı bağlantısı
        string? globalUserID = string.Empty;

        // Yüklendiğinde!
        private void personnel_information_form_Load(object sender, EventArgs e)
        {
            comboBox_kan_grubu.SelectedIndex = 0;

            comboBox_yetki_durumu.Items.Add("Müdür");
            comboBox_yetki_durumu.Items.Add("Müdür Yardımcısı");
            comboBox_yetki_durumu.Items.Add("Personel");

            // UserID
            globalUserID = SessionUserInformation.userID;

            // Kullanıcı ID'sine göre nesneleri aktif etme
            YetkiDurumunaGoreDuzenlemeIslemi(globalUserID);

            // Nesneleri doldur
            NesneleriDoldur(globalUserID);


        }

        private void button_duzenle_Click(object sender, EventArgs e)
        {
            if (textBox_ad.Text == string.Empty || textBox_soyad.Text == string.Empty || textBox_tel_no.Text == string.Empty || textBox_kullaniciadi.Text == string.Empty || textBox_sifre.Text == string.Empty)
            {
                MessageBox.Show("'*' ile gösterilen alanlar zorunlu olarak doldurulmalıdır!", "Personel Bilgiler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlConnection connect = new SqlConnection(connectionString);
                connect.Open();
                SqlTransaction transaction = connect.BeginTransaction();

                try
                {
                    // 1.Sorgu
                    string personelBilgileriQuery = "UPDATE personel_bilgileri SET p_ad = @p_ad, p_soyad = @p_soyad, p_cinsiyet = @p_cinsiyet, p_tel_no = @p_tel_no, p_eposta = @p_eposta, p_kan_grubu = @p_kan_grubu WHERE p_id = @p_id";
                    SqlCommand CpersonelBilgileriCommand = new SqlCommand(personelBilgileriQuery, connect, transaction);
                    CpersonelBilgileriCommand.Parameters.AddWithValue("@p_ad", textBox_ad.Text);
                    CpersonelBilgileriCommand.Parameters.AddWithValue("@p_soyad", textBox_soyad.Text);

                    int cinsiyet = 0;
                    if (radioButton_cinsiyet_erkek.Checked == true)
                    {
                        cinsiyet = 1;
                    }
                    else 
                    {
                        cinsiyet = 0;
                    }
                    CpersonelBilgileriCommand.Parameters.AddWithValue("@p_cinsiyet", cinsiyet);
                    CpersonelBilgileriCommand.Parameters.AddWithValue("@p_tel_no", textBox_tel_no.Text);
                    CpersonelBilgileriCommand.Parameters.AddWithValue("@p_eposta", textBox_eposta.Text);
                    CpersonelBilgileriCommand.Parameters.AddWithValue("@p_kan_grubu", comboBox_kan_grubu.Text);
                    CpersonelBilgileriCommand.Parameters.AddWithValue("@p_id", globalUserID);
                    int count1 = CpersonelBilgileriCommand.ExecuteNonQuery();
                    if (count1 > 0) 
                    {
                        MessageBox.Show("1. sorgu çalıştı");
                    }
                    else
                    {
                        MessageBox.Show("1. sorgu çalışmadı!");
                    }

                    // 2.Sorgu
                    string personelGirisBilgileriQuery = "UPDATE personel_giris_bilgileri SET p_g_kullanici_ad = @p_g_kullanici_ad, p_g_sifre = @p_g_sifre WHERE p_g_id = @p_g_id";
                    SqlCommand personelGirisBilgileriCommand = new SqlCommand(personelGirisBilgileriQuery, connect, transaction);
                    personelGirisBilgileriCommand.Parameters.AddWithValue("@p_g_kullanici_ad", textBox_kullaniciadi.Text);
                    personelGirisBilgileriCommand.Parameters.AddWithValue("@p_g_sifre", textBox_sifre.Text);
                    personelGirisBilgileriCommand.Parameters.AddWithValue("@p_g_id", globalUserID);
                    int count2 = personelGirisBilgileriCommand.ExecuteNonQuery();
                    if (count2 > 0)
                    {
                        MessageBox.Show("2. sorgu çalıştı");
                    }
                    else
                    {
                        MessageBox.Show("2. sorgu çalışmadı!");
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
                    MessageBox.Show("Düzenleme işlemi başarıyla gerçekleşti.", "Personel Düzenle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // --------------------------------------------------* Metotlar *--------------------------------------------------

        // Yetki durumuna göre nesneleri aktif etme.
        private void YetkiDurumunaGoreDuzenlemeIslemi(string userID)
        {
            SqlConnection connect = new SqlConnection(connectionString);

            try
            {
                connect.Open();

                string query = "SELECT p_g_yetki_durumu FROM personel_giris_bilgileri WHERE p_g_id = @p_g_id ";
                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@p_g_id", userID);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int authority = Convert.ToInt16(reader["p_g_yetki_durumu"]);
                    if (authority == 2)
                    {
                        // MessageBox.Show("YETKİ: " + authority);
                        //dateTimePicker_giris_tarihi.Enabled = true;
                        textBox_kullaniciadi.Enabled = true;
                    }
                    else if (authority == 1)
                    {
                        // MessageBox.Show("YETKİ: " + authority);
                        //dateTimePicker_giris_tarihi.Enabled = true;
                        textBox_kullaniciadi.Enabled = true;
                    }
                }
                //
                reader.Close();

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
            // Bitiş
        }


        //
        private void NesneleriDoldur(string userID)
        {
            SqlConnection connect = new SqlConnection(connectionString);

            try
            {
                connect.Open();

                // 1.Sorgu
                string personelBilgileriQuery = "SELECT * FROM personel_bilgileri WHERE p_id = @p_id";

                SqlCommand personelBilgileriCommand = new SqlCommand(personelBilgileriQuery, connect);
                personelBilgileriCommand.Parameters.AddWithValue("@p_id", userID);

                SqlDataReader personelBilgileriReader = personelBilgileriCommand.ExecuteReader();
                while (personelBilgileriReader.Read())
                {
                    //reader[0].ToString();
                    textBox_ad.Text = personelBilgileriReader[1].ToString();
                    textBox_soyad.Text = personelBilgileriReader[2].ToString();

                    // cinsiyet
                    if (Convert.ToInt16(personelBilgileriReader[3]) == 1)
                    {
                        radioButton_cinsiyet_erkek.Checked = true;
                    }
                    else
                    {
                        radioButton_cinsiyet_kadin.Checked = true;
                    }

                    dateTimePicker_giris_tarihi.Text = personelBilgileriReader[4].ToString();
                    //reader[5].ToString();
                    textBox_tel_no.Text = personelBilgileriReader[6].ToString();
                    textBox_eposta.Text = personelBilgileriReader[7].ToString();
                    comboBox_kan_grubu.Text = personelBilgileriReader[8].ToString();
                    textBox_maas.Text = personelBilgileriReader[9].ToString();
                }

                personelBilgileriReader.Close();

                // 2.Sorgu
                string personelGirisBilgileriQuery = "SELECT * FROM personel_giris_bilgileri WHERE p_g_id = @p_g_id;";
                SqlCommand personelGirisBilgileriCommand = new SqlCommand(personelGirisBilgileriQuery, connect);
                personelGirisBilgileriCommand.Parameters.AddWithValue("@p_g_id", userID);
                SqlDataReader personelGirisBilgileriReader = personelGirisBilgileriCommand.ExecuteReader();

                while (personelGirisBilgileriReader.Read())
                {
                    // personelGirisBilgileriReader[0].ToString();
                    if (Convert.ToInt16(personelGirisBilgileriReader[1]) == 2)
                    {
                        comboBox_yetki_durumu.SelectedIndex = 0;
                    }
                    else if (Convert.ToInt16(personelGirisBilgileriReader[1]) == 1)
                    {
                        comboBox_yetki_durumu.SelectedIndex = 1;
                    }
                    else
                    {
                        comboBox_yetki_durumu.SelectedIndex = 2;
                    }


                    if (Convert.ToInt16(personelGirisBilgileriReader[2]) == 1)
                    {
                        radioButton_aktiflik_aktif.Checked = true;
                    }
                    else
                    {
                        radioButton_aktiflik_pasif.Checked = true;
                    }

                    textBox_kullaniciadi.Text = personelGirisBilgileriReader[3].ToString();
                    textBox_sifre.Text = personelGirisBilgileriReader[4].ToString();


                }
                personelGirisBilgileriReader.Close();

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

       
    }
}
