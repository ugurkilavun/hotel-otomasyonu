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
        // Veri Tabaný Baðlantýsý

        // ConnectionStringClass ConnectionString = new ConnectionStringClass();
        // Yukarýdaki gibi Nesneyi tanýmlamaya gerek yok.
        // Çünkü, nesne zaten ayný namespace'in (hotel_otomasyonu) içinde bulunuyor.
        private string ConnectionString = ConnectionStringClass.ConnectionStringVarible();

        public LoginForm()
        {
            InitializeComponent();
        }

        // Form Yüklendiðinde!
        private void LoginForm_Load(object sender, EventArgs e)
        {
            buttonLogin.Focus();
            //panelNickname.BorderStyle = BorderStyle.FixedSingle();
        }

        // From Kapandýðýnda
        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {//Tüm Uygulamlardan çýk!
            Application.Exit();

        }
       
        // ---------------------------------------------------------------* Giriþ Yap Bölümü *---------------------------------------------------------------

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // Giriþ için kullanýlan 'TextBox'larýn hepsi dolu mu?

            // TextBox'larýn herhangi biri dolu deðilse 
            if (textBoxNickname.Text == string.Empty || textBoxPassword1.Text == string.Empty)
            {
                //MessageBox.Show("veri giriþi doðru deðil");
                labelAllException("Kullanýcý adý veya þifre eksik!");
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

                    // Personel Var Ýse
                    if (reader.Read())
                    {
                        string PersonnelID = reader["p_g_id"].ToString();
                        int AuthorityStatus = Convert.ToInt16(reader["p_g_yetki_durumu"]);
                        int ActivityStatus = Convert.ToInt16(reader["p_g_aktiflik_durumu"]);

                        //MessageBox.Show("id " + PersonnelID + " "+ AuthorityStatus + " " + ActivityStatus);
                        // Personelin hesabý aktif ise
                        if (ActivityStatus == 1)
                        {

                            // Kullanýcý Oturumunu Tutacak Sýnýf ve Açýlacak '(main_form) Form
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
                            // Yetki: Müdür Yardýmcýsý
                            else if (AuthorityStatus == 1)
                            {
                                //MessageBox.Show("Yetki Durumu: Müdür Yardýmcýsý - " + var_user_authority + ". ID'si - " + var_userID);
                                UserInformation.userID = PersonnelID;
                                UserInformation.UserAuthorization = AuthorityStatus;
                                main_form.Show();
                                this.Hide();
                            }
                            // Yetki: Müdür
                            else if (AuthorityStatus == 2)
                            {
                                //MessageBox.Show("Yetki Durumu: Müdür - " + var_user_authority + ". ID'si - " + var_userID);
                                UserInformation.userID = PersonnelID;
                                UserInformation.UserAuthorization = AuthorityStatus;
                                main_form.Show();
                                this.Hide();
                            }
                            // Bilinemeyen/Bulunamayan Yetki Durumu
                            else
                            {
                                //MessageBox.Show("Yanlýþ þifre veya kullanýcý adý!", "Giriþ - Yetki");
                                labelAllException("Yetkisiz Giriþ!");
                            }
                        }
                        // Personelin hesabý aktif deðil ise
                        else
                        {
                            labelAllException("Bu hesap; þu an aktif deðildir!");
                        }
                    }
                    // Personel Yok Ýse
                    else
                    {
                        //MessageBox.Show("Yanlýþ þifre veya kullanýcý adý!", "Giriþ");
                        labelAllException("Yanlýþ kullanýcý adý veya þifre!");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SQL Query sýrasýnda hata oluþtu! Hata: " + ex.ToString());
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
            // Týklandýðýnda: bir form açýlsýn ve personel ad, soyad, giriþ tarihi, eposta adresi gibi deðikenler alýnarak yeni þifre belirlenip personele iletilsin!
            MessageBox.Show("Bu iþlem kullaným için hazýr deðil.", "Þifremi Unuttum");
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

        //TextBox'ýn içindeki yazýyý sil
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