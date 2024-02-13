using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using Sessions;// Namespace

namespace hotel_otomasyonu
{
    public partial class LoginForm : Form
    {
        // Veri Taban� Ba�lant�s�

        // ConnectionStringClass ConnectionString = new ConnectionStringClass();
        // Yukar�daki gibi Nesneyi tan�mlamaya gerek yok.
        // ��nk�, nesne zaten ayn� namespace'in (hotel_otomasyonu) i�inde bulunuyor.
        private string ConnectionString = ConnectionStringClass.ConnectionStringVarible();

        public LoginForm()
        {
            InitializeComponent();
        }

        // Form Y�klendi�inde!
        private void LoginForm_Load(object sender, EventArgs e)
        {
            buttonLogin.Focus();
            //panelNickname.BorderStyle = BorderStyle.FixedSingle();
        }

        // From Kapand���nda
        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {//T�m Uygulamlardan ��k!
            Application.Exit();

        }
       
        // ---------------------------------------------------------------* Giri� Yap B�l�m� *---------------------------------------------------------------

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // Giri� i�in kullan�lan 'TextBox'lar�n hepsi dolu mu?

            // TextBox'lar�n herhangi biri dolu de�ilse 
            if (textBoxNickname.Text == string.Empty || textBoxPassword1.Text == string.Empty)
            {
                //MessageBox.Show("veri giri�i do�ru de�il");
                labelAllException("Kullan�c� ad� veya �ifre eksik!");
            }
            else
            {
                
                SqlConnection connect = new SqlConnection(ConnectionString);

                try
                {
                    connect.Open();
                    string query = "SELECT p_g_id, p_g_yetki_durumu, p_g_aktiflik_durumu FROM personel_giris_bilgileri WHERE p_g_kullanici_ad = @pgkullanici_ad AND p_g_sifre = @pgsifre";

                    SqlCommand command = new SqlCommand(query, connect);
                    command.Parameters.AddWithValue("@pgkullanici_ad", textBoxNickname.Text);
                    command.Parameters.AddWithValue("@pgsifre", textBoxPassword1.Text);

                    SqlDataReader reader = command.ExecuteReader();

                    // Personel Var �se
                    if (reader.Read())
                    {
                        string PersonnelID = reader["p_g_id"].ToString();
                        int AuthorityStatus = Convert.ToInt16(reader["p_g_yetki_durumu"]);
                        int ActivityStatus = Convert.ToInt16(reader["p_g_aktiflik_durumu"]);

                        //MessageBox.Show("id " + PersonnelID + " "+ AuthorityStatus + " " + ActivityStatus);
                        // Personelin hesab� aktif ise
                        if (ActivityStatus == 1)
                        {

                            // Kullan�c� Oturumunu Tutacak S�n�f ve A��lacak '(main_form) Form
                            UserInformation UserInformation = new UserInformation();
                            main_form main_form = new main_form(UserInformation);

                            // Yetki Durumu Sorgulama

                            // Yetki: Personel
                            if (AuthorityStatus == 0)
                            {
                                //MessageBox.Show("Yetki Durumu: Personel - " + var_user_authority + ". ID'si - " + var_userID);
                                UserInformation.userID = PersonnelID;
                                UserInformation.UserAuthorization = AuthorityStatus;
                                main_form.Show();
                                this.Hide();
                            }
                            // Yetki: M�d�r Yard�mc�s�
                            else if (AuthorityStatus == 1)
                            {
                                //MessageBox.Show("Yetki Durumu: M�d�r Yard�mc�s� - " + var_user_authority + ". ID'si - " + var_userID);
                                UserInformation.userID = PersonnelID;
                                UserInformation.UserAuthorization = AuthorityStatus;
                                main_form.Show();
                                this.Hide();
                            }
                            // Yetki: M�d�r
                            else if (AuthorityStatus == 2)
                            {
                                //MessageBox.Show("Yetki Durumu: M�d�r - " + var_user_authority + ". ID'si - " + var_userID);
                                UserInformation.userID = PersonnelID;
                                UserInformation.UserAuthorization = AuthorityStatus;
                                main_form.Show();
                                this.Hide();
                            }
                            // Bilinemeyen/Bulunamayan Yetki Durumu
                            else
                            {
                                //MessageBox.Show("Yanl�� �ifre veya kullan�c� ad�!", "Giri� - Yetki");
                                labelAllException("Yetkisiz Giri�!");
                            }
                        }
                        // Personelin hesab� aktif de�il ise
                        else
                        {
                            labelAllException("Bu hesap; �u an aktif de�ildir!");
                        }
                    }
                    // Personel Yok �se
                    else
                    {
                        //MessageBox.Show("Yanl�� �ifre veya kullan�c� ad�!", "Giri�");
                        labelAllException("Yanl�� kullan�c� ad� veya �ifre!");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SQL Query s�ras�nda hata olu�tu! Hata: " + ex.ToString());
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

        private void linkLabelForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // T�kland���nda: bir form a��ls�n ve personel ad, soyad, giri� tarihi, eposta adresi gibi de�ikenler al�narak yeni �ifre belirlenip personele iletilsin!
            MessageBox.Show("Bu i�lem kullan�m i�in haz�r de�il.", "�ifremi Unuttum");
        }

        private void textBoxNickname_Enter(object sender, EventArgs e)
        {
            // TextBoxClear(textBoxNickname);
        }

        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            // TextBoxClear(textBoxPassword);
        }

        // ---------------------------------------------------------------* Metotlar *---------------------------------------------------------------

        //TextBox'�n i�indeki yaz�y� sil
        private void TextBoxClear(TextBox textBox)
        {
            textBox.Text = string.Empty;
        }

        private void labelAllException(string hata)
        {
            label_tum_hatalar.Text = hata;
            int x = (panel_giris.ClientSize.Width - label_tum_hatalar.Width) / 2;
            int y = 262;
            label_tum_hatalar.Location = new Point(x, y);
        }

       
    }
}