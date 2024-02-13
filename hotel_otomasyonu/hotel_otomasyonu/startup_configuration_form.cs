using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
// using ItemObjects;

namespace hotel_otomasyonu
{


    public partial class startup_configuration_form : Form
    {
        public startup_configuration_form()
        {
            InitializeComponent();
            //this.Text = "Custom Button with Border";
            //this.Size = new System.Drawing.Size(300, 150);

            //customButton = new CustomButton();
            //customButton.Text = "Click Me";
            // customButton.Size = new System.Drawing.Size(100, 40);
            // customButton.Location = new System.Drawing.Point(100, 50);
            // customButton.Click += CustomButton_Click;
            // startup_configuration_form startup_configuration_form = new startup_configuration_form();
            // startup_configuration_form.Controls.Add(customButton);
        }



        private string connectionString = ConnectionStringClass.ConnectionStringVarible(); // Veri tabanı bağlantısı
        private void startup_configuration_form_Load(object sender, EventArgs e)
        {
            //timer_progressBar.Start();

            progressBar_startup.ForeColor = Color.Red; // İlerleme çubuğunun rengi
            progressBar_startup.BackColor = Color.Red;
        }

        private void timer_progressBar_Tick(object sender, EventArgs e)
        {


            progressBar_startup.Value += 1;

            if (progressBar_startup.Value == 100)
            {
                timer_progressBar.Stop();
                //login_form login_form = new login_form();
                //login_form.Show();
                //this.Hide();
            }

            // Highlight
            // katlar, odalar, musteri_bilgileri, personel_giris_bilgileri, personel_bilgileri, rezervasyonlar
            VeriTabaniSorgu(20, connectionString, "katlar", "Veri Tabanı Kontrolü;", "Katlar tablosu mevcut.", "tablosuna ulaşılamadı!");
            VeriTabaniSorgu(30, connectionString, "odalar", "Veri Tabanı Kontrolü;", "Odalar tablosu mevcut.", "tablosuna ulaşılamadı!");
            VeriTabaniSorgu(40, connectionString, "musteri_bilgileri", "Veri Tabanı Kontrolü;", "Müsteri Bilgileri tablosu mevcut.", "tablosuna ulaşılamadı!");
            VeriTabaniSorgu(50, connectionString, "personel_giris_bilgileri", "Veri Tabanı Kontrolü;", "Personel Giris Bilgileri tablosu mevcut.", "tablosuna ulaşılamadı!");
            VeriTabaniSorgu(60, connectionString, "personel_bilgileri", "Veri Tabanı Kontrolü;", "Personel Bilgileri tablosu mevcut.", "tablosuna ulaşılamadı!");
            VeriTabaniSorgu(70, connectionString, "rezervasyonlar", "Veri Tabanı Kontrolü;", "Rezervasyonlar tablosu mevcut.", "tablosuna ulaşılamadı!");
            Sorgu(90, "Veri Tabanı Kontrolü Tamamlandı; Her şey güncel!", string.Empty);


        }

        private void VeriTabaniSorgu(int ifValue, string connectionString, string tableName, string LeftText, string RightText, string qException)
        {
            if (progressBar_startup.Value == ifValue)
            {
                SqlConnection connect = new SqlConnection(connectionString);
                try
                {
                    connect.Open();
                    // 
                    string query = $"SELECT COUNT(*) FROM {tableName}";
                    SqlCommand command = new SqlCommand(query, connect);
                    command.Parameters.AddWithValue("@tabloismi", tableName);

                    int count = Convert.ToInt16(command.ExecuteScalar());
                    if (count > 0)
                    {
                        //MessageBox.Show("Var");
                        label_yazi.Text = LeftText;
                        label_surec_yazi.Text = RightText;
                    }
                    else
                    {
                        label_surec_yazi.Text = tableName + " " + qException;
                        label_surec_yazi.Text = string.Empty;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("SQL Query sırasında hata oluştu! Hata: " + ex.ToString());
                    timer_progressBar.Stop();
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

        // Sorgu
        private void Sorgu(int ifValue, string Text, string ProcessText)
        {
            if (progressBar_startup.Value == ifValue)
            {
                label_yazi.Text = Text;
                label_surec_yazi.Text = ProcessText;
                // label_surec_yazi.Text = RightText;
            }
        }

    }
}
