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
using Sessions;
using System.Net.Mail;

namespace hotel_otomasyonu
{
    public partial class add_customer_form : Form
    {
        string GlobalUserID = string.Empty;
        // 'UserInformation' sınıfını 'Session_UserInformation' yeni adı ile tanımlar
        private UserInformation SessionUserInformation;

        // Constructor; login formundan gelen 'UserInformation' nesnesini 'GetSession_UserInformation' olarak tanımlar
        public add_customer_form(UserInformation GetUserInformation)
        {
            InitializeComponent();
            // Constructor içinde alınan 'GetSession_UserInformation' nesnesini 'Session_UserInformation' üye değişkenine atar
            this.SessionUserInformation = GetUserInformation;
            GlobalUserID = SessionUserInformation.userID;
        }
        // Form içi global değişkenler

        private string ConnectionString = ConnectionStringClass.ConnectionStringVarible(); // Veri Tabanı Bağlantısı

        // --------------------------------------------------* Form Ayarları *--------------------------------------------------
        private void add_customer_form_Load(object sender, EventArgs e)
        {
            comboBox_musteri_ekle_kan_grubu.SelectedIndex = 0;
            // Gelen PersonelID 

            // MessageBox.Show("Load Eventi. PersonelID: " + GlobalUserID);

        }
        private void button_musteriyi_ekle_Click(object sender, EventArgs e)
        {
            // Zorunlu alanlar boş ise
            if (textBox_musteri_ekle_tc.Text == string.Empty || textBox_musteri_ekle_ad.Text == string.Empty || textBox_musteri_ekle_soyad.Text == string.Empty || textBox_musteri_ekle_tel_no.Text == string.Empty)
            {
                MessageBox.Show("Hata: '*' ile belirtilen alanların tamamının zorunlu olarak doldurulması gerekmektedir!", "Zorunlu Alan Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // Zorunlu alanlar boş değil ise
            else
            {
                // MessageBox.Show("Else. PersonelID: " + GlobalUserID);

                SqlConnection connect = new SqlConnection(ConnectionString);

                try
                {
                    connect.Open();

                    string CostumerqQery = "SELECT Count(*) FROM musteri_bilgileri WHERE m_tc = @m_tcs";

                    SqlCommand CostumerCommand = new SqlCommand(CostumerqQery, connect);
                    CostumerCommand.Parameters.AddWithValue("@m_tcs", Convert.ToString(textBox_musteri_ekle_tc.Text));

                    int count = Convert.ToInt16(CostumerCommand.ExecuteScalar());

                    // T.C nolu müşteri var ise
                    if (count > 0)
                    {
                        MessageBox.Show(textBox_musteri_ekle_tc.Text + " Nolu T.C'ye ait müşteri bulunmaktadır!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    // T.C nolu müşteri yok ise
                    else
                    {
                        int Cinsiyet = -1; // 1 Erkek, 0 Kadın,

                        // MessageBox.Show("Müşteri yok");

                        string InsertQuery = "INSERT INTO musteri_bilgileri(m_tc, m_ad, m_soyad, m_cinsiyet, m_tel_no, m_eposta, m_acik_adres, m_kan_grubu) VALUES(@m_tc, @m_ad, @m_soyad, @m_cinsiyet, @m_tel_no, @m_eposta, @m_acik_adres, @m_kan_grubu)";

                        // RadioButton: Erkek seçili ise
                        if (radioButton_musteri_ekle_erkek.Checked == true)
                        {
                            Cinsiyet = 1;
                        }
                        // RadioButton: Kadın seçili ise
                        else if (radioButton_musteri_ekle_kadin.Checked == true)
                        {
                            Cinsiyet = 0;
                        }

                        SqlCommand InsertCommand = new SqlCommand(InsertQuery, connect);
                        InsertCommand.Parameters.AddWithValue("@m_tc", textBox_musteri_ekle_tc.Text);
                        InsertCommand.Parameters.AddWithValue("@m_ad", textBox_musteri_ekle_ad.Text);
                        InsertCommand.Parameters.AddWithValue("@m_soyad", textBox_musteri_ekle_soyad.Text);

                        InsertCommand.Parameters.AddWithValue("@m_cinsiyet", Convert.ToInt16(Cinsiyet)); // Cinsiyet: 0 Erkek, 1 Kadın.

                        InsertCommand.Parameters.AddWithValue("@m_tel_no", textBox_musteri_ekle_tel_no.Text);
                        InsertCommand.Parameters.AddWithValue("@m_eposta", textBox_musteri_ekle_eposta.Text);
                        InsertCommand.Parameters.AddWithValue("@m_acik_adres", textBox_musteri_ekle_acik_adres.Text);
                        InsertCommand.Parameters.AddWithValue("@m_kan_grubu", comboBox_musteri_ekle_kan_grubu.Text);

                        int InsertCount = Convert.ToInt16(InsertCommand.ExecuteNonQuery());

                        // Müşteri eklendi ise
                        if (InsertCount > 0)
                        {
                            // İşlemden sonra yeni bir tane daha müşteri ekleme işlemi?
                            DialogResult result = MessageBox.Show("Müşteri başarıyla eklendi. Yeni bir müşteri daha eklemek ister misiniz?", "Müşteri Ekle", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            // Evet seçilirse
                            if (result == DialogResult.Yes)
                            {
                                textBox_musteri_ekle_tc.Text = string.Empty;
                                textBox_musteri_ekle_ad.Text = string.Empty;
                                textBox_musteri_ekle_soyad.Text = string.Empty;
                                radioButton_musteri_ekle_erkek.Checked = false;
                                radioButton_musteri_ekle_kadin.Checked = false;
                                textBox_musteri_ekle_eposta.Text = string.Empty;
                                textBox_musteri_ekle_eposta.Text = string.Empty;
                                textBox_musteri_ekle_acik_adres.Text = string.Empty;
                                comboBox_musteri_ekle_kan_grubu.SelectedItem = null;
                            }
                            // Hayır seçilirse
                            else
                            {
                                this.Close();
                            }

                        }
                        // Müşteri eklenemdi ise
                        else
                        {

                            MessageBox.Show("Müşteri Eklenemedi!");

                        }

                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show("SQL Query sırasında hata oluştu! Hata: " + ex.ToString());
                    //Application.Exit();
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

        // ------------------------------------------- * Metotlar * -------------------------------------------


        private void textBox_musteri_ekle_tc_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox_musteri_ekle_tel_no_KeyPress_1(object sender, KeyPressEventArgs e)
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
