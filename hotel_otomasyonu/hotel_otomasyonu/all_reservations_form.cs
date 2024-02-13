using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AllQuerys; // Namespace

namespace hotel_otomasyonu
{
    public partial class all_reservations_form : Form
    {
        public all_reservations_form()
        {
            InitializeComponent();
        }

        // Form içi global değişkenler
        private string connectionString = ConnectionStringClass.ConnectionStringVarible(); // Veri tabanaı bağlantısı
        // string GlobalTCNumber = string.Empty;
        int whileCount = 0;
        int TotalPrice = 0;

        // --------------------------------------------------* Form Ayarları *--------------------------------------------------

        private void all_reservations_form_Load(object sender, EventArgs e)
        {
            // Form Load Event
        }
        DataTable DataTableReservations = new DataTable();
        private void all_reservations_form_Shown(object sender, EventArgs e)
        {
            dataGridViewStyle();
            DataTableHeaderColumn(DataTableReservations);
            // veriEkle(100);


        }

        // --------------------------------------------------* Sadece Aktif Rezervasyonları Gösterme *--------------------------------------------------

        // Aktif tüm rezervasyonlar
        private void button_aktif_tum_rezervasyonları_goster_Click(object sender, EventArgs e)
        {
            ClearEveryThing();
            //DataTableHeaderColumn(DataTableReservations);
            SqlConnection connect = new SqlConnection(connectionString);

            try
            {
                /*
                     rezervasyon_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
                     p_id VARCHAR(12) NOT NULL,
	                 m_tc VARCHAR(11) NOT NULL,
	                 oda_no INT NOT NULL,
	                 giris_tarihi DATETIME NOT NULL,
	                 rezervasyon_durumu BIT NOT NULL, -- 1 Aktif Rezervasyon, 0 Pasif(geçmiş) Rezervasyon
	                 cikis_tarihi DATETIME NULL, 
	                 ucret INT NULL,
                */

                connect.Open();

                string query = "SELECT * FROM rezervasyonlar WHERE rezervasyon_durumu = @rezervasyon_durumu OR rezervasyon_durumu = @rezervasyon_durumu2 ORDER BY giris_tarihi ASC";

                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@rezervasyon_durumu", 1);
                command.Parameters.AddWithValue("@rezervasyon_durumu2", 2);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string ReservationID, PersonelID, CostumerID, DoorNumber, FirstDate, StringReservationStation, LastDate, Price;
                    int ReservationStation;

                    ReservationID = Convert.ToString(reader[0]);

                    // Personel ID ile personel ad ve soyad sorgusu
                    SelectQuery SelectQuery = new SelectQuery();
                    PersonelID = SelectQuery.WithIDQueryPersonel(connectionString, Convert.ToString(reader[1]));

                    // Müşteri T.C No ile Müşteri ad ve soyad 
                    CostumerID = SelectQuery.WithIDQueryCostumer(connectionString, Convert.ToString(reader[2]));

                    DoorNumber = Convert.ToString(reader[3]);
                    FirstDate = Convert.ToString(reader[4]);

                    ReservationStation = Convert.ToInt16(reader[5]);
                    if (ReservationStation == 1)
                    {
                        StringReservationStation = "Aktif Rezervasyon";
                    }
                    else if (ReservationStation == 2)
                    {
                        StringReservationStation = "Aktif Oda Ayırtma";
                    }
                    else
                    {
                        StringReservationStation = "Belli Değil";
                    }


                    LastDate = Convert.ToString(reader[6]);
                    Price = Convert.ToString(reader[7]) + " ₺";

                    DataTableReservations.Rows.Add(ReservationID, PersonelID, CostumerID, DoorNumber, FirstDate, StringReservationStation, LastDate, Price);

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

        // Geçmiş tüm rezervasyonlar
        private void button_gecmis_tum_rezervasyonları_goster_Click(object sender, EventArgs e)
        {

            ClearEveryThing();
            //DataTableHeaderColumn(DataTableReservations);
            SqlConnection connect = new SqlConnection(connectionString);

            try
            {
                /*
                     rezervasyon_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
                     p_id VARCHAR(12) NOT NULL,
	                 m_tc VARCHAR(11) NOT NULL,
	                 oda_no INT NOT NULL,
	                 giris_tarihi DATETIME NOT NULL,
	                 rezervasyon_durumu BIT NOT NULL, -- 1 Aktif Rezervasyon, 0 Pasif(geçmiş) Rezervasyon
	                 cikis_tarihi DATETIME NULL, 
	                 ucret INT NULL,
                */

                connect.Open();

                string query = "SELECT * FROM rezervasyonlar WHERE rezervasyon_durumu = @rezervasyon_durumu OR rezervasyon_durumu = @rezervasyon_durumu3 ORDER BY giris_tarihi ASC";

                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@rezervasyon_durumu", 0);
                command.Parameters.AddWithValue("@rezervasyon_durumu3", 3);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string ReservationID, PersonelID, CostumerID, DoorNumber, FirstDate, StringReservationStation, LastDate, Price;
                    int ReservationStation;

                    ReservationID = Convert.ToString(reader[0]);

                    // Personel ID ile personel ad ve soyad sorgusu
                    SelectQuery SelectQuery = new SelectQuery();
                    PersonelID = SelectQuery.WithIDQueryPersonel(connectionString, Convert.ToString(reader[1]));

                    // Müşteri T.C No ile Müşteri ad ve soyad 
                    CostumerID = SelectQuery.WithIDQueryCostumer(connectionString, Convert.ToString(reader[2]));

                    DoorNumber = Convert.ToString(reader[3]);
                    FirstDate = Convert.ToString(reader[4]);


                    ReservationStation = Convert.ToInt16(reader[5]);
                    if (ReservationStation == 0)
                    {
                        StringReservationStation = "Geçmiş Rezervasyon";
                        TotalPrice += Convert.ToInt32(reader[7]);
                    }
                    else if (ReservationStation == 1)
                    {
                        StringReservationStation = "Aktif Rezervasyon";
                    }
                    else if (ReservationStation == 2)
                    {
                        StringReservationStation = "Aktif Oda Ayırtma";
                    }
                    else
                    {
                        StringReservationStation = "Geçmiş Oda Ayırtma";
                    }


                    LastDate = Convert.ToString(reader[6]);
                    Price = Convert.ToString(reader[7]) + " ₺";

                    DataTableReservations.Rows.Add(ReservationID, PersonelID, CostumerID, DoorNumber, FirstDate, StringReservationStation, LastDate, Price);
                    whileCount++;
                }

                groupBox_istatistikler.Text = "Geçmiş Reservasyon İstatistikleri";
                label_toplam_ucret.Text = Convert.ToString(TotalPrice);
                label_toplam_rezervasyon.Text = Convert.ToString(whileCount);


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

        private void button_tum_rezervasyonları_goster_Click(object sender, EventArgs e)
        {

            ClearEveryThing();
            //DataTableHeaderColumn(DataTableReservations);
            SqlConnection connect = new SqlConnection(connectionString);

            try
            {
                /*
                     rezervasyon_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
                     p_id VARCHAR(12) NOT NULL,
	                 m_tc VARCHAR(11) NOT NULL,
	                 oda_no INT NOT NULL,
	                 giris_tarihi DATETIME NOT NULL,
	                 rezervasyon_durumu BIT NOT NULL, -- 1 Aktif Rezervasyon, 0 Pasif(geçmiş) Rezervasyon
	                 cikis_tarihi DATETIME NULL, 
	                 ucret INT NULL,
                */

                connect.Open();

                string query = "SELECT * FROM rezervasyonlar WHERE rezervasyon_durumu = @rezervasyon_durumu OR rezervasyon_durumu = @rezervasyon_durumu1 OR rezervasyon_durumu = @rezervasyon_durumu2 OR rezervasyon_durumu = @rezervasyon_durumu3 ORDER BY giris_tarihi ASC";

                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@rezervasyon_durumu", 0);
                command.Parameters.AddWithValue("@rezervasyon_durumu1", 1);
                command.Parameters.AddWithValue("@rezervasyon_durumu2", 2);
                command.Parameters.AddWithValue("@rezervasyon_durumu3", 3);


                SqlDataReader reader = command.ExecuteReader();

                
                
                while (reader.Read())
                {
                    string ReservationID, PersonelID, CostumerID, DoorNumber, FirstDate, StringReservationStation, LastDate, Price;
                    int ReservationStation;
                    

                    ReservationID = Convert.ToString(reader[0]);

                    // Personel ID ile personel ad ve soyad sorgusu
                    SelectQuery SelectQuery = new SelectQuery();
                    PersonelID = SelectQuery.WithIDQueryPersonel(connectionString, Convert.ToString(reader[1]));

                    // Müşteri T.C No ile Müşteri ad ve soyad 
                    CostumerID = SelectQuery.WithIDQueryCostumer(connectionString, Convert.ToString(reader[2]));

                    DoorNumber = Convert.ToString(reader[3]);
                    FirstDate = Convert.ToString(reader[4]);


                    ReservationStation = Convert.ToInt16(reader[5]);
                    if (ReservationStation == 0)
                    {
                        StringReservationStation = "Geçmiş Rezervasyon";
                        // Rezervasyonun Durumu = Pasif/Geçmiş ise ücret değişkenin değeri vardır.
                        // Rezervasyonun Durumu = Aktif ise ücret değişkenin değeri yoktur ve Integer değişkene string(NULL, vb gibi) değer atandığı için hata alınır.
                        TotalPrice += Convert.ToInt32(reader[7]);
                    }
                    else if (ReservationStation == 1)
                    {
                        StringReservationStation = "Aktif Rezervasyon";  
                    }
                    else if (ReservationStation == 2)
                    {
                        StringReservationStation = "Aktif Oda Ayırtma";
                    }
                    else if (ReservationStation == 3)
                    {
                        StringReservationStation = "Geçmiş Oda Ayırtma";
                    }
                    else
                    {
                        StringReservationStation = "Belli Değil!";
                    }

                    LastDate = Convert.ToString(reader[6]);
                    Price = Convert.ToString(reader[7]) + " ₺";

                    whileCount++;

                    DataTableReservations.Rows.Add(ReservationID, PersonelID, CostumerID, DoorNumber, FirstDate, StringReservationStation, LastDate, Price);
                }
                groupBox_istatistikler.Text = "Tüm Reservasyon İstatistikleri";
                label_toplam_ucret.Text = Convert.ToString(TotalPrice);
                label_toplam_rezervasyon.Text = Convert.ToString(whileCount);

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





        // --------------------------------------------------* Metotlar *--------------------------------------------------


        private void dataGridViewStyle()
        {
            dataGridView_tum_rezarvasyonlar.BackgroundColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Control); ; //  dataGridView_gecmis_rezarvasyonlar: genel arka plan rengi
            dataGridView_tum_rezarvasyonlar.BorderStyle = BorderStyle.None;
            dataGridView_tum_rezarvasyonlar.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView_tum_rezarvasyonlar.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView_tum_rezarvasyonlar.DefaultCellStyle.SelectionBackColor = Color.Beige; // Seçilen satırın arka plan rengi
            dataGridView_tum_rezarvasyonlar.DefaultCellStyle.SelectionForeColor = Color.FromArgb(64, 64, 64);// Seçilen satırın metin rengi
            dataGridView_tum_rezarvasyonlar.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing; // Opsiyonel
            dataGridView_tum_rezarvasyonlar.RowHeadersWidth = 25; // dataGridView_gecmis_rezarvasyonlar.RowHeadersVisible = false;
            dataGridView_tum_rezarvasyonlar.DefaultCellStyle.Font = new Font("Franklin Gothic Book", 9);


            // Sütunlar
            dataGridView_tum_rezarvasyonlar.ColumnHeadersDefaultCellStyle.Font = new Font("Franklin Gothic Book", 9, FontStyle.Bold);
            dataGridView_tum_rezarvasyonlar.EnableHeadersVisualStyles = false;
            dataGridView_tum_rezarvasyonlar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView_tum_rezarvasyonlar.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64); // Sütun başlıklarının arka plan rengi
            dataGridView_tum_rezarvasyonlar.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView_tum_rezarvasyonlar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Sütun boyutunu otomatik ayarla
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
            datatable.Columns.Add("Rezarvasyon No", typeof(string));
            datatable.Columns.Add("İşlemi Yapan Personel", typeof(string));
            datatable.Columns.Add("Müşteri Ad Soyad", typeof(string));
            datatable.Columns.Add("Oda No", typeof(string));
            datatable.Columns.Add("Giriş Tarihi", typeof(string));
            datatable.Columns.Add("Rezarvasyon Durumu", typeof(string));
            datatable.Columns.Add("Çıkış Tarihi", typeof(string));
            datatable.Columns.Add("Ücret", typeof(string));

            dataGridView_tum_rezarvasyonlar.DataSource = datatable;
        }
        private void ClearEveryThing()
        {
            DataTableReservations.Rows.Clear();
            label_toplam_ucret.Text = "0";
            label_toplam_rezervasyon.Text = "0";
            whileCount = 0;
            TotalPrice = 0;
        }

       
    }
}