using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using System.Data.SqlClient;
using Sessions; // Namespace
//using IDCreaters; // Namespace
using Microsoft.Data.SqlClient;
using System.Windows.Forms.VisualStyles;

namespace hotel_otomasyonu
{
    public partial class main_form : Form
    {
        // 'UserInformation' sınıfını 'Session_UserInformation' yeni adı ile tanımlar
        private UserInformation Session_UserInformation;

        // Constructor; login formundan gelen 'UserInformation' nesnesini 'GetSession_UserInformation' olarak tanımlar
        public main_form(UserInformation GetSession_UserInformation)
        {
            InitializeComponent();
            // Constructor içinde alınan 'GetSession_UserInformation' nesnesini 'Session_UserInformation' üye değişkenine atar
            this.Session_UserInformation = GetSession_UserInformation;
        }

        // --------------------------------------------------* Form Ayarları *--------------------------------------------------

        // Form içi global nesneler
        UserInformation UserInformation = new UserInformation();

        // Form içi global değişkenler
        string? GlobalUserID = string.Empty;
        int UserAuthority = -1;
        private string connectionString = ConnectionStringClass.ConnectionStringVarible(); // Veri tabanı bağlantısı

        //Form Yüklendiğinde
        private void main_form_Load(object sender, EventArgs e)
        {

            // Gelen 'userID' Ve 'UserAuthorization'
            //string? userID = ;
            GlobalUserID = Session_UserInformation.userID;
            UserAuthority = Session_UserInformation.UserAuthorization;

            // Kullanıcı Adı ve Soyadı

            string? personel_ad = string.Empty;
            string? personel_soyad = string.Empty;

            SqlConnection connect = new SqlConnection(connectionString);
            try
            {

                // userID
                connect.Open();

                string query = "SELECT p_ad, p_soyad FROM personel_bilgileri WHERE p_id = @p_id";

                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@p_id", GlobalUserID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    personel_ad = Convert.ToString(reader[0]);
                    personel_soyad = Convert.ToString(reader[1]);

                }

                // Yetki Çevirme İşlemi
                string UserAuthorityStr = string.Empty;

                if (UserAuthority == 0)
                {

                    UserAuthorityStr = "Personel";
                    ButtonEnabledAndVisible(false);
                }
                else if (UserAuthority == 1)
                {

                    UserAuthorityStr = "Müdür Yardımcısı";
                    ButtonEnabledAndVisible(true);

                }
                else if (UserAuthority == 2)
                {

                    UserAuthorityStr = "Müdür";
                    ButtonEnabledAndVisible(true);

                }
                else
                {
                    UserAuthorityStr = "Yetki Hatası: Yetki bulunamadı! Program kapanıyor...";
                    Application.Exit();
                }

                // Name, Text, Y: İsmi, Alacağı değer/yazı, Y düşeyinde konumu
                labelCenter(label_ad_ve_soyad, personel_ad + " " + personel_soyad, 239);
                labelCenter(label_yetki_durumu, UserAuthorityStr, 269);

            }
            catch (Exception ex)
            {

                MessageBox.Show("SQL Query sırasında hata oluştu! Hata: " + ex.ToString());
                Application.Exit();
            }
            finally
            {

                if (connect != null)
                {
                    connect.Close();
                }

            }

        }
        //Form Gösterildiğinde
        private void main_form_Shown(object sender, EventArgs e)
        {

        }
        //Form kapatılırken
        private void main_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Close();
        }

        //Form Kapandığında
        private void main_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Tüm Uygulamlardan çık!
            Application.Exit();
        }

        // --------------------------------------------------* Butonlar *--------------------------------------------------

        private void button_yeni_rezarvasyon_Click(object sender, EventArgs e)
        {
            UserInformation.userID = GlobalUserID;
            new_reservation_form new_reservation_form = new new_reservation_form(UserInformation);



            new_reservation_form.ShowDialog();
        }

        private void button_musteri_ekle_Click(object sender, EventArgs e)
        {
            UserInformation.userID = GlobalUserID;
            add_customer_form add_Customer_Form = new add_customer_form(UserInformation);
            add_Customer_Form.ShowDialog();

        }

        private void button_musteri_sorgula_Click(object sender, EventArgs e)
        {
            query_customer_form query_customer_form = new query_customer_form();
            query_customer_form.ShowDialog();
        }

        private void button_musteri_cikisi_Click(object sender, EventArgs e)
        {
            customer_reservation_exit_form customer_reservation_exit_form = new customer_reservation_exit_form();
            customer_reservation_exit_form.ShowDialog();
        }

        private void button_tum_kayitlar_Click(object sender, EventArgs e)
        {
            // SSS!
            all_reservations_form all_Reservations_Form = new all_reservations_form();
            all_Reservations_Form.ShowDialog();
        }

        private void button_tel_numaralari_Click(object sender, EventArgs e)
        {
            phone_numbers_form phone_numbers_form = new phone_numbers_form();
            phone_numbers_form.ShowDialog();
        }

        private void button_yardim_Click(object sender, EventArgs e)
        {

            //MessageBox.Show("ID: " + UserIDCreater.CreatedUserID());

        }

        private void button_oturumu_kapat_Click(object sender, EventArgs e)
        {
            // Gloval değişkenleri unsetle.
            GlobalUserID = string.Empty;
            UserAuthority = -1;
            // Class da tutulan veriler de unsetlensin
            Session_UserInformation.userID = string.Empty;
            Session_UserInformation.UserAuthorization = -1;

            // LoginForm u aç
            LoginForm login_form = new LoginForm();
            login_form.Show();
            // Bu formu gizle
            this.Hide();

        }

        // Yetkili Ait Butonlar
        private void button_oda_kat_ekle_Click(object sender, EventArgs e)
        {
            room_and_floor_processes_form room_And_Floor_Processes_form = new room_and_floor_processes_form();
            room_And_Floor_Processes_form.ShowDialog();
        }

        // Personel İşlemleri
        private void button_personel_islemleri_Click(object sender, EventArgs e)
        {
            UserInformation.userID = GlobalUserID;
            personnel_operations_form personnel_Operations_Form = new personnel_operations_form(UserInformation);
            personnel_Operations_Form.ShowDialog();
        }

        // Personel bilgilerini düzenle
        private void button_personel_bilgilerini_duzenle_Click(object sender, EventArgs e)
        {
            UserInformation.userID = GlobalUserID;
            personnel_edit_form personnel_edit_form = new personnel_edit_form(UserInformation);
            personnel_edit_form.ShowDialog();
        }

        // Telefon No İşlemleri
        private void button_tel_no_islemleri_Click(object sender, EventArgs e)
        {
            phone_number_processes_form phone_number_processes = new phone_number_processes_form();
            phone_number_processes.ShowDialog();
        }

        // Yardım menüsü
        private void button_yardim_Click_1(object sender, EventArgs e)
        {
            helps_form helps = new helps_form();
            helps.ShowDialog();
        }

        // Giriş yapan kullanıcı bilgilerini düzenleme!
        private void button_personel_Click(object sender, EventArgs e)
        {
            UserInformation.userID = GlobalUserID;
            personnel_information_form personnel_İnformation_form = new personnel_information_form(UserInformation);
            personnel_İnformation_form.ShowDialog();
        }

        // ---------------------------------------------------------------* Metotlar *---------------------------------------------------------------
        private void labelCenter(Label name, string text, int y)
        {

            name.Text = text;
            int x = (panel_personel.ClientSize.Width - name.Width) / 2;
            name.Location = new Point(x, y);

        }

        private void ButtonEnabledAndVisible(bool Varible)
        {
            button_oda_kat_ekle.Enabled = Varible;
            button_oda_kat_ekle.Visible = Varible;
            label_oda_ve_kat_islemleri_yazi.Enabled = Varible;
            label_oda_ve_kat_islemleri_yazi.Visible = Varible;

            button_personel_islemleri.Enabled = Varible;
            button_personel_islemleri.Visible = Varible;
            label_personel_ekle_yazi.Enabled = Varible;
            label_personel_ekle_yazi.Visible = Varible;

            button_personel_bilgilerini_duzenle.Enabled = Varible;
            button_personel_bilgilerini_duzenle.Visible = Varible;
            labe_personel_bilgilerini_duzenle_yazi.Enabled = Varible;
            labe_personel_bilgilerini_duzenle_yazi.Visible = Varible;

            button_tel_no_islemleri.Enabled = Varible;
            button_tel_no_islemleri.Visible = Varible;
            label_tel_no_islemleri_yazi.Enabled = Varible;
            label_tel_no_islemleri_yazi.Visible = Varible;
        }

        
    }
}
