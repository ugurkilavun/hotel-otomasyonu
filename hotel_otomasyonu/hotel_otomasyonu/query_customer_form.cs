using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AllQuerys; // Namespace

namespace hotel_otomasyonu
{
    public partial class query_customer_form : Form
    {
        public query_customer_form()
        {
            InitializeComponent();
        }

        // Form içi global değişkenler
        private string connectionString = ConnectionStringClass.ConnectionStringVarible(); // Veri tabanı bağlantısı
        string GlobalTCNumber = string.Empty;

        private void query_customer_form_Load(object sender, EventArgs e)
        {
            dataGridViewStyle();
            textBox_musteri_ve_rezervasyon_sorgula_ad.Height = 0;
        }

        // ------------------------------------------- * Müşteri Ve Rezervasyon Sorgula * -------------------------------------------
        private void button_musteri_ve_rezervasyon_sorgula_Click(object sender, EventArgs e)
        {
            // T.C No alanı boş ise
            if (textBox_musteri_ve_rezervasyon_sorgula_tc_no.Text == string.Empty)
            {
                NesneleriTemizle();
                NesneleriDeAktifEt();
                MessageBox.Show("HATA: TC No alanı boş bırakılamaz", "Hatalı Sorgulama", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            // T.C No alanı boş değil ise
            else
            {
                SelectQuery SelectQuery = new SelectQuery();

                string Fquery = "SELECT COUNT(*) FROM musteri_bilgileri WHERE m_tc = @parameters";

                int Fcount = SelectQuery.OneParametersWithTCSQueryCount(connectionString, Fquery, textBox_musteri_ve_rezervasyon_sorgula_tc_no.Text);

                // Müşteri var ise
                if (Fcount > 0)
                {
                    // TC'yi bir değişkene atama amacı; Müşteri güncelleme işleminde sorunlarla karşılaşmamaktır!
                    GlobalTCNumber = textBox_musteri_ve_rezervasyon_sorgula_tc_no.Text;

                    NesneleriAktifEt();
                    NesneleriTemizle();
                    //MessageBox.Show("Müşteri Var");
                    SqlConnection connect = new SqlConnection(connectionString);

                    try
                    {

                        connect.Open();

                        string query = "SELECT * FROM musteri_bilgileri WHERE m_tc = @m_tc";

                        SqlCommand command = new SqlCommand(query, connect);
                        command.Parameters.AddWithValue("@m_tc", textBox_musteri_ve_rezervasyon_sorgula_tc_no.Text);

                        SqlDataReader reader = command.ExecuteReader();

                        int cinsiyet = -1;

                        while (reader.Read())
                        {

                            //reader[0];
                            textBox_musteri_ve_rezervasyon_sorgula_ad.Text = Convert.ToString(reader[1]);
                            textBox_musteri_ve_rezervasyon_sorgula_soyad.Text = Convert.ToString(reader[2]);
                            cinsiyet = Convert.ToInt16(reader[3]);


                            textBox_musteri_ve_rezervasyon_sorgula_tel_no.Text = Convert.ToString(reader[4]);
                            textBox_musteri_ve_rezervasyon_sorgula_eposta.Text = Convert.ToString(reader[5]);
                            textBox_musteri_ve_rezervasyon_sorgula_acik_adres.Text = Convert.ToString(reader[6]);
                            comboBox_musteri_ve_rezervasyon_sorgula_kan_grubu.Text = Convert.ToString(reader[7]);

                        }

                        // Eğer SqlDataReader birden çok yerde ve tek connect ile kullanılacaksa kapatılmalıdır.
                        reader.Close();

                        if (cinsiyet == 0)
                        {
                            radioButton_musteri_ve_rezervasyon_sorgula_kadin.Checked = true;
                        }
                        else if (cinsiyet == 1)
                        {
                            radioButton_musteri_ve_rezervasyon_sorgula_erkek.Checked = true;
                        }
                        else
                        {
                            radioButton_musteri_ve_rezervasyon_sorgula_kadin.Checked = false;
                            radioButton_musteri_ve_rezervasyon_sorgula_erkek.Checked = false;
                        }

                        // Rezervasyon Sorgulama
                        // 1 Aktif Rezervasyon, 0 Pasif(geçmiş) Rezervasyon

                        // Aktif Reservasyon Sorgusu
                        string FActiveReservationQuery = "SELECT count(*) FROM rezervasyonlar WHERE m_tc = @parameters1 AND rezervasyon_durumu = @parameters2";
                        int FActiveReservationCount = SelectQuery.QueryReservation(connectionString, FActiveReservationQuery, textBox_musteri_ve_rezervasyon_sorgula_tc_no.Text, "1");

                        // Geçmiş Reservasyon Sorgusu
                        string FPasifReservationQuery = "SELECT count(*) FROM rezervasyonlar WHERE m_tc = @parameters1 AND rezervasyon_durumu = @parameters2";
                        int FPasifReservationCount = SelectQuery.QueryReservation(connectionString, FPasifReservationQuery, textBox_musteri_ve_rezervasyon_sorgula_tc_no.Text, "0");

                        // ------*Aktif Reservasyonlar 

                        // Aktif rezervasyon var ise
                        if (FActiveReservationCount > 0)
                        {
                            //MessageBox.Show("Aktif rezarvasyon var");

                            // Rezervasyon ID, Personel ID, Oda No, Giriş Tarihi, Çıkış Tarihi, Ucret
                            string ActiveReservationQuery = "SELECT rezervasyon_id, p_id, oda_no, giris_tarihi, cikis_tarihi, ucret FROM rezervasyonlar WHERE m_tc = @m_tc AND rezervasyon_durumu = @rezervasyon_durumu";

                            SqlCommand ActiveReservationCommand = new SqlCommand(ActiveReservationQuery, connect);
                            ActiveReservationCommand.Parameters.AddWithValue("@m_tc", textBox_musteri_ve_rezervasyon_sorgula_tc_no.Text);
                            ActiveReservationCommand.Parameters.AddWithValue("@rezervasyon_durumu", 1);

                            SqlDataReader Rreader = ActiveReservationCommand.ExecuteReader();

                            DataTable ActiveReservatioDataTable = new DataTable();
                            DataTableHeaderColumn(ActiveReservatioDataTable); // ActiveReservatioDataTable'a başlık verilerini ve stillerini ekle.

                            // Veri atam işlemi.
                            while (Rreader.Read())
                            {
                                // Rezervasyon ID, Personel ID, Müşteri T.C, Oda No, Giriş Tarihi, Rezarvasyon Durumu, Çıkış Tarihi, Ucret

                                // Rezervasyon ID, Personel ID, Oda No, Giriş Tarihi, Çıkış Tarihi, Ucret
                                string ReservationID, PersonelID, DoorNumber, FirstDate, LastDate, Money;

                                ReservationID = Convert.ToString(Rreader[0]);

                                PersonelID = Convert.ToString(Rreader[1]);
                                string personelnamesurnmae = SelectQuery.WithIDQueryPersonel(connectionString, PersonelID);

                                DoorNumber = Convert.ToString(Rreader[2]);
                                FirstDate = Convert.ToString(Rreader[3]);
                                LastDate = Convert.ToString(Rreader[4]);
                                Money = Convert.ToString(Rreader[5]);

                                ActiveReservatioDataTable.Rows.Add(ReservationID, personelnamesurnmae, DoorNumber, FirstDate, LastDate, Money);
                                //ActiveReservatioDataTable.Rows.Add(ReservationID, PersonelID, CostumerID, FloorNumber, FirstDate, StrReservationStation, LastDate, Money);

                                //ActiveReservatioDataTable.Rows.Add(Convert.ToString(Rreader[0]), Convert.ToString(Rreader[1]), Convert.ToString(Rreader[2]), Convert.ToString(Rreader[3]), Convert.ToString(Rreader[4]), Convert.ToInt16(Rreader[5]), Convert.ToString(Rreader[6]), Convert.ToString(Rreader[7]));
                            }

                            Rreader.Close();

                            //ActiveReservationAdapter.Fill(ActiveReservatioDataTable);

                            dataGridView_aktif_rezarvasyonlar.DataSource = ActiveReservatioDataTable;

                        }
                        // Aktif rezervasyon yok ise
                        else
                        {
                            MessageBox.Show("Müşteriye ait aktif rezervasyon bulunamadı!", "Rezervasyon", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // DataTable ActiveReservatioDataTable = new DataTable();
                            // ActiveReservatioDataTable.Columns.Add("Rezervasyon Bulunamadı", typeof(string));
                            // ActiveReservatioDataTable.Rows.Add("Aktif rezervasyon bulunmamakta");
                            // dataGridView_aktif_rezarvasyonlar.DataSource = ActiveReservatioDataTable;
                        }

                        // ------*Geçmiş Rezarvasyonlar

                        //  Geçmiş rezarvasyon var ise
                        if (FPasifReservationCount > 0)
                        {
                            //MessageBox.Show("Geçmiş rezarvasyon var");

                            // Rezervasyon ID, Personel ID, Oda No, Giriş Tarihi, Çıkış Tarihi, Ucret
                            string PasifReservationQuery = "SELECT rezervasyon_id, p_id, oda_no, giris_tarihi, cikis_tarihi, ucret FROM rezervasyonlar WHERE m_tc = @m_tc AND rezervasyon_durumu = @rezervasyon_durumu";

                            SqlCommand PasifeReservationCommand = new SqlCommand(PasifReservationQuery, connect);
                            PasifeReservationCommand.Parameters.AddWithValue("@m_tc", textBox_musteri_ve_rezervasyon_sorgula_tc_no.Text);
                            PasifeReservationCommand.Parameters.AddWithValue("@rezervasyon_durumu", 0);

                            SqlDataReader Rreader = PasifeReservationCommand.ExecuteReader();

                            DataTable PasifReservatioDataTable = new DataTable();
                            DataTableHeaderColumn(PasifReservatioDataTable); // ActiveReservatioDataTable'a başlık verilerini ve stillerini ekle.

                            // Veri atam işlemi.
                            while (Rreader.Read())
                            {
                                // Rezervasyon ID, Personel ID, Müşteri T.C, Oda No, Giriş Tarihi, Rezarvasyon Durumu, Çıkış Tarihi, Ucret

                                // Rezervasyon ID, Personel ID, Oda No, Giriş Tarihi, Çıkış Tarihi, Ucret
                                string ReservationID, PersonelID, DoorNumber, FirstDate, LastDate, Money;

                                ReservationID = Convert.ToString(Rreader[0]);

                                PersonelID = Convert.ToString(Rreader[1]);
                                string personelnamesurnmae = SelectQuery.WithIDQueryPersonel(connectionString, PersonelID);

                                DoorNumber = Convert.ToString(Rreader[2]);
                                FirstDate = Convert.ToString(Rreader[3]);
                                LastDate = Convert.ToString(Rreader[4]);
                                Money = Convert.ToString(Rreader[5]);

                                PasifReservatioDataTable.Rows.Add(ReservationID, personelnamesurnmae, DoorNumber, FirstDate, LastDate, Money);
                                //ActiveReservatioDataTable.Rows.Add(ReservationID, PersonelID, CostumerID, FloorNumber, FirstDate, StrReservationStation, LastDate, Money);

                                //ActiveReservatioDataTable.Rows.Add(Convert.ToString(Rreader[0]), Convert.ToString(Rreader[1]), Convert.ToString(Rreader[2]), Convert.ToString(Rreader[3]), Convert.ToString(Rreader[4]), Convert.ToInt16(Rreader[5]), Convert.ToString(Rreader[6]), Convert.ToString(Rreader[7]));
                            }

                            Rreader.Close();

                            //ActiveReservationAdapter.Fill(ActiveReservatioDataTable);

                            dataGridView_gecmis_rezarvasyonlar.DataSource = PasifReservatioDataTable;

                        }
                        // Geçmiş rezervasyon yok ise
                        else
                        {
                            MessageBox.Show("Müşteriye ait geçmiş rezervasyon bulunamadı!", "Rezervasyon", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //DataTable ActiveReservatioDataTable = new DataTable();
                            //ActiveReservatioDataTable.Columns.Add("Rezervasyon Bulunamadı", typeof(string));
                            //ActiveReservatioDataTable.Rows.Add("Geçmiş rezervasyon bulunmamakta");
                            //dataGridView_gecmis_rezarvasyonlar.DataSource = ActiveReservatioDataTable;
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
                // Müşteri yok ise
                else
                {
                    NesneleriTemizle();
                    NesneleriDeAktifEt();
                    MessageBox.Show(textBox_musteri_ve_rezervasyon_sorgula_tc_no.Text + " T.C Numaraya bağlı müşteri bulunamadı!", "Müşteri Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

        }

        // ------------------------------------------- * Müşteri Bilgilerini Güncelle * -------------------------------------------
        private void button_musteri_ve_rezervasyon_sorgula_guncelle_Click(object sender, EventArgs e)
        {
            // Ad, Soyad ve Tel no gibi alanlar dolu mu?

            // Dolu ise
            if (textBox_musteri_ve_rezervasyon_sorgula_ad.Text == string.Empty || textBox_musteri_ve_rezervasyon_sorgula_soyad.Text == string.Empty || textBox_musteri_ve_rezervasyon_sorgula_tel_no.Text == string.Empty)
            {
                MessageBox.Show("Hata: '*' ile belirtilen alanların tamamının zorunlu olarak doldurulması gerekmektedir!", "Zorunlu Alan Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // Dolu değil ise
            else
            {
                //MessageBox.Show("İşlem yapılabilir");
                SqlConnection connect = new SqlConnection(connectionString);
                try
                {
                    bool cinsiyet = true;

                    if (radioButton_musteri_ve_rezervasyon_sorgula_erkek.Checked == true)
                    {
                        //MessageBox.Show("erkek");
                        cinsiyet = true;
                    }
                    else if (radioButton_musteri_ve_rezervasyon_sorgula_kadin.Checked == true)
                    {
                        //MessageBox.Show("Kadin");
                        cinsiyet = false;
                    }

                    connect.Open();

                    string query = "UPDATE musteri_bilgileri SET m_ad = @m_ad, m_soyad = @m_soyad, m_cinsiyet = @m_cinsiyet, m_tel_no = @m_tel_no, m_eposta = @m_eposta, m_acik_adres = @m_acik_adres, m_kan_grubu = @m_kan_grubu WHERE m_tc = @m_tc";

                    SqlCommand command = new SqlCommand(query, connect);

                    command.Parameters.AddWithValue("@m_tc", GlobalTCNumber);


                    command.Parameters.AddWithValue("@m_ad", textBox_musteri_ve_rezervasyon_sorgula_ad.Text);
                    command.Parameters.AddWithValue("@m_soyad", textBox_musteri_ve_rezervasyon_sorgula_soyad.Text);

                    command.Parameters.AddWithValue("@m_cinsiyet", cinsiyet);

                    command.Parameters.AddWithValue("@m_tel_no", textBox_musteri_ve_rezervasyon_sorgula_tel_no.Text);
                    command.Parameters.AddWithValue("@m_eposta", textBox_musteri_ve_rezervasyon_sorgula_eposta.Text);
                    command.Parameters.AddWithValue("@m_acik_adres", textBox_musteri_ve_rezervasyon_sorgula_acik_adres.Text);
                    command.Parameters.AddWithValue("@m_kan_grubu", comboBox_musteri_ve_rezervasyon_sorgula_kan_grubu.Text);

                    int count = command.ExecuteNonQuery();
                    if (count > 0)
                    {
                        MessageBox.Show("Müşterinin bilgileri başarılı bir şekilde güncellendi!", "Müşteri Sorgula", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Müşterinin bilgileri bir hatadan dolayı güncellenemedi! Lütfen bu hatayı yetkili kişilere iletiniz!", "Müşteri Sorgula", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        }

        // ------------------------------------------- * Metotlar * -------------------------------------------

        private void dataGridViewStyle()
        {
            dataGridView_gecmis_rezarvasyonlar.BackgroundColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Control); ; //  dataGridView_gecmis_rezarvasyonlar: genel arka plan rengi
            dataGridView_gecmis_rezarvasyonlar.BorderStyle = BorderStyle.None;
            dataGridView_gecmis_rezarvasyonlar.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView_gecmis_rezarvasyonlar.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView_gecmis_rezarvasyonlar.DefaultCellStyle.SelectionBackColor = Color.Beige; // Seçilen satırın arka plan rengi
            dataGridView_gecmis_rezarvasyonlar.DefaultCellStyle.SelectionForeColor = Color.FromArgb(64, 64, 64);// Seçilen satırın metin rengi
            dataGridView_gecmis_rezarvasyonlar.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing; // Opsiyonel
            dataGridView_gecmis_rezarvasyonlar.RowHeadersWidth = 25; // dataGridView_gecmis_rezarvasyonlar.RowHeadersVisible = false;
            dataGridView_gecmis_rezarvasyonlar.DefaultCellStyle.Font = new Font("Franklin Gothic Book", 9);

            // Sütunlar
            dataGridView_gecmis_rezarvasyonlar.ColumnHeadersDefaultCellStyle.Font = new Font("Franklin Gothic Book", 9, FontStyle.Bold);
            dataGridView_gecmis_rezarvasyonlar.EnableHeadersVisualStyles = false;
            dataGridView_gecmis_rezarvasyonlar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView_gecmis_rezarvasyonlar.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64); // Sütun başlıklarının arka plan rengi
            dataGridView_gecmis_rezarvasyonlar.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView_gecmis_rezarvasyonlar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Sütun boyutunu otomatik ayarla

            //---------------------------------------------------------------------------------------------------------------------------

            dataGridView_aktif_rezarvasyonlar.BackgroundColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Control); //  dataGridView_gecmis_rezarvasyonlar: genel arka plan rengi
            dataGridView_aktif_rezarvasyonlar.BorderStyle = BorderStyle.None;
            dataGridView_aktif_rezarvasyonlar.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView_aktif_rezarvasyonlar.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView_aktif_rezarvasyonlar.DefaultCellStyle.SelectionBackColor = Color.Beige; // Seçilen satırın arka plan rengi
            dataGridView_aktif_rezarvasyonlar.DefaultCellStyle.SelectionForeColor = Color.FromArgb(64, 64, 64);// Seçilen satırın metin rengi
            dataGridView_aktif_rezarvasyonlar.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing; // Opsiyonel
            dataGridView_aktif_rezarvasyonlar.RowHeadersWidth = 25; // dataGridView_gecmis_rezarvasyonlar.RowHeadersVisible = false;
            dataGridView_aktif_rezarvasyonlar.DefaultCellStyle.Font = new Font("Franklin Gothic Book", 9);

            // Sütunlar
            dataGridView_aktif_rezarvasyonlar.ColumnHeadersDefaultCellStyle.Font = new Font("Franklin Gothic Book", 9, FontStyle.Bold);
            dataGridView_aktif_rezarvasyonlar.EnableHeadersVisualStyles = false;
            dataGridView_aktif_rezarvasyonlar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView_aktif_rezarvasyonlar.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64); // Sütun başlıklarının arka plan rengi
            dataGridView_aktif_rezarvasyonlar.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView_aktif_rezarvasyonlar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Sütun boyutunu otomatik ayarla
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
            datatable.Columns.Add("Oda No", typeof(string));
            datatable.Columns.Add("Giriş Tarihi", typeof(string));
            datatable.Columns.Add("Çıkış Tarihi", typeof(string));
            datatable.Columns.Add("Ücret", typeof(string));
        }

        private void NesneleriAktifEt()
        {
            textBox_musteri_ve_rezervasyon_sorgula_ad.Enabled = true;
            textBox_musteri_ve_rezervasyon_sorgula_soyad.Enabled = true;
            textBox_musteri_ve_rezervasyon_sorgula_tel_no.Enabled = true;
            textBox_musteri_ve_rezervasyon_sorgula_eposta.Enabled = true;
            comboBox_musteri_ve_rezervasyon_sorgula_kan_grubu.Enabled = true;
            radioButton_musteri_ve_rezervasyon_sorgula_erkek.Enabled = true;
            radioButton_musteri_ve_rezervasyon_sorgula_kadin.Enabled = true;
            textBox_musteri_ve_rezervasyon_sorgula_acik_adres.Enabled = true;
            button_musteri_ve_rezervasyon_sorgula_guncelle.Enabled = true;
            dataGridView_aktif_rezarvasyonlar.Enabled = true;
            dataGridView_gecmis_rezarvasyonlar.Enabled = true;
        }

        private void NesneleriDeAktifEt()
        {
            textBox_musteri_ve_rezervasyon_sorgula_ad.Enabled = false;
            textBox_musteri_ve_rezervasyon_sorgula_soyad.Enabled = false;
            textBox_musteri_ve_rezervasyon_sorgula_tel_no.Enabled = false;
            textBox_musteri_ve_rezervasyon_sorgula_eposta.Enabled = false;
            comboBox_musteri_ve_rezervasyon_sorgula_kan_grubu.Enabled = false;
            radioButton_musteri_ve_rezervasyon_sorgula_erkek.Enabled = false;
            radioButton_musteri_ve_rezervasyon_sorgula_kadin.Enabled = false;
            textBox_musteri_ve_rezervasyon_sorgula_acik_adres.Enabled = false;
            button_musteri_ve_rezervasyon_sorgula_guncelle.Enabled = false;
            dataGridView_aktif_rezarvasyonlar.Enabled = false;
            dataGridView_gecmis_rezarvasyonlar.Enabled = false;
        }

        private void NesneleriTemizle()
        {
            textBox_musteri_ve_rezervasyon_sorgula_ad.Text = string.Empty;
            textBox_musteri_ve_rezervasyon_sorgula_soyad.Text = string.Empty;
            textBox_musteri_ve_rezervasyon_sorgula_tel_no.Text = string.Empty;
            textBox_musteri_ve_rezervasyon_sorgula_eposta.Text = string.Empty;
            comboBox_musteri_ve_rezervasyon_sorgula_kan_grubu.SelectedItem = null;
            radioButton_musteri_ve_rezervasyon_sorgula_erkek.Checked = false;
            radioButton_musteri_ve_rezervasyon_sorgula_kadin.Checked = false;
            textBox_musteri_ve_rezervasyon_sorgula_acik_adres.Text = string.Empty;
            dataGridView_aktif_rezarvasyonlar.DataSource = null;
            dataGridView_gecmis_rezarvasyonlar.DataSource = null;
        }

        private void panel_musteri_sec_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
