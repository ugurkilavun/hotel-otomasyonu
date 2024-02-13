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
using AllQuerys; // namespace
using System.Globalization;

namespace hotel_otomasyonu
{
    public partial class customer_reservation_exit_form : Form
    {

        DataTable dataTable = new DataTable();
        public customer_reservation_exit_form()
        {
            InitializeComponent();
            // DataGridView'in SelectionChanged olayına abone olalım
            dataGridView_rezervasyon_cikisi.SelectionChanged += DataGridView1_SelectionChanged;
        }

        // Form içi global değişkenler
        private string connectionString = ConnectionStringClass.ConnectionStringVarible(); // Veri tabanı bağlantısı
        DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
        DateTime datetime = DateTime.Now;
        string? girisTarihi = string.Empty;
        string? globalMusteriTC = string.Empty;

        private void customer_reservation_exit_form_Load(object sender, EventArgs e)
        {
            dataGridViewStyle(dataGridView_rezervasyon_cikisi);
            DataTableHeaderColumn(dataTable, dataGridView_rezervasyon_cikisi);

            TextboxStyles(textBox_rezervasyon_cikisi_rezervasyon_no);
            TextboxStyles(textBox_rezervasyon_cikisi_personeladsoyad);
            TextboxStyles(textBox_rezervasyon_cikisi_adsoyad);
            TextboxStyles(textBox_rezervasyon_cikisi_oda_no);
            TextboxStyles(textBox_rezervasyon_cikisi_giris_tarihi);
            TextboxStyles(textBox_rezervasyon_cikisi_cikisi_rezervsayon_durumu);
            TextboxStyles(textBox_rezervasyon_cikisi_cikis_tarihi);
            TextboxStyles(textBox_rezervasyon_cikisi_ucret);
        }

        private void customer_reservation_exit_form_Shown(object sender, EventArgs e)
        {

        }

        // ------------------------------------------- * Rezervayon Sorgula * -------------------------------------------
        private void button_rezarvasyon_sorgula_Click(object sender, EventArgs e)
        {
            if (textBox_musteri_cikisi_sorgula_tc_no.Text == string.Empty)
            {
                ClearEveryThing(dataTable);
                ClearTextBox();
                globalMusteriTC = string.Empty;
                // NesneleriDeAktifEt();
                MessageBox.Show("HATA: TC No alanı boş bırakılamaz", "Hatalı Sorgulama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // MessageBox.Show(globalMusteriTC);
            }
            else
            {
                ClearEveryThing(dataTable);
                ClearTextBox();
                globalMusteriTC = string.Empty;

                SqlConnection connect = new SqlConnection(connectionString);

                try
                {

                    connect.Open();

                    string query = "SELECT * FROM rezervasyonlar WHERE m_tc = @m_tc AND rezervasyon_durumu = @rezervasyon_durumu"; // (rezervasyon_durumu = @rezervasyon_durumu OR rezervasyon_durumu = @oda_ayirtma)
                    SqlCommand command = new SqlCommand(query, connect);
                    command.Parameters.AddWithValue("@m_tc", textBox_musteri_cikisi_sorgula_tc_no.Text);
                    command.Parameters.AddWithValue("@rezervasyon_durumu", 1);
                    // command.Parameters.AddWithValue("@oda_ayirtma", 2);
                    // Oda Ayırtma işlemi çıkışı yapılmıyor: Çünkü oda ayırma işleminin kapalı durumu (3)'tür!

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        SelectQuery SelectQuery = new SelectQuery();
                        reader.Close();
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string ReservationID, PersonelID, CostumerID, DoorNumber, FirstDate, ReservationSituation, LastDate, Price;

                            // Reservasyon ID
                            ReservationID = Convert.ToString(reader[0]);
                            // Personel Ad Soyad
                            PersonelID = SelectQuery.WithIDQueryPersonel(connectionString, Convert.ToString(reader[1]));
                            // Müşteri ID/Ad Soyad
                            globalMusteriTC = Convert.ToString(reader[2]);
                            CostumerID = SelectQuery.WithIDQueryCostumer(connectionString, Convert.ToString(reader[2]));
                            // Oda Numarası
                            DoorNumber = Convert.ToString(reader[3]);
                            // Giriş Tarihi
                            FirstDate = Convert.ToString(reader[4]);
                            // Reservasyon Durumu
                            if (Convert.ToInt16(reader[5]) == 1)
                            {
                                ReservationSituation = "Aktif Rezervasyon";
                            }
                            else if (Convert.ToInt16(reader[5]) == 2)
                            {
                                ReservationSituation = "Aktif Oda Ayırtma";
                            }
                            else
                            {
                                ReservationSituation = "Geçmiş Rezervasyon";
                            }
                            // Çıkış Tarihi
                            LastDate = Convert.ToString(reader[6]);
                            // Ücret
                            Price = Convert.ToString(reader[7]);


                            dataTable.Rows.Add(ReservationID, PersonelID, CostumerID, DoorNumber, FirstDate, ReservationSituation, LastDate, Price);

                        }
                        reader.Close();
                        dataGridView_rezervasyon_cikisi.DataSource = dataTable;

                        // MessageBox.Show(globalMusteriTC);

                    }
                    else
                    {
                        MessageBox.Show("Müşteriye ait reservayon kaydı bulunamadı.", "Reservasyon Kaydı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // MessageBox.Show(globalMusteriTC);
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


        // Rezervasyon Çıkışı
        private void button_rezervsayon_cikisi_Click(object sender, EventArgs e)
        {
            if (textBox_rezervasyon_cikisi_ucret.Text != string.Empty)
            {
                SqlConnection connect = new SqlConnection(connectionString);
                connect.Open();
                SqlTransaction transaction = connect.BeginTransaction();

                try
                {
                    // 1. Düzenleme
                    string query1 = "UPDATE rezervasyonlar SET rezervasyon_durumu = @rezervasyon_durumu_guncelle, cikis_tarihi = @cikis_tarihi, ucret = @ucret WHERE m_tc = @m_tc AND oda_no = @oda_no AND rezervasyon_durumu = @rezervasyon_durumu";
                    
                    SqlCommand command1 = new SqlCommand(query1, connect, transaction);
                    command1.Parameters.AddWithValue("@rezervasyon_durumu_guncelle", 0);
                    command1.Parameters.AddWithValue("@cikis_tarihi", DateTime.Now); // dateTimePicker_rezervasyon_cikisi_cikis_tarihi.Value
                    // command1.Parameters.AddWithValue("@cikis_tarihi", DateTime.TryParse(textBox_rezervasyon_cikisi_cikis_tarihi.Text, out DateTime ExitDate));//dateTimePicker_rezervasyon_cikisi_cikis_tarihi.Value
                    command1.Parameters.AddWithValue("@ucret", textBox_rezervasyon_cikisi_ucret.Text);
                    // WHERE
                    command1.Parameters.AddWithValue("@m_tc", globalMusteriTC);
                    command1.Parameters.AddWithValue("@oda_no", textBox_rezervasyon_cikisi_oda_no.Text);
                    command1.Parameters.AddWithValue("@rezervasyon_durumu", 1);

                    int count1 = command1.ExecuteNonQuery();

                    if (count1 > 0)
                    {
                        MessageBox.Show("1. Sorgu çalıştı");
                    }
                    else
                    {
                        MessageBox.Show("1. Sorgu çalıştmadı");

                    }


                    // 2. Düzenleme
                    string query2 = "UPDATE odalar SET oda_durum = @oda_durum WHERE oda_no = @oda_no";

                    SqlCommand command2 = new SqlCommand(query2, connect, transaction);
                    command2.Parameters.AddWithValue("@oda_durum", 0);
                    command2.Parameters.AddWithValue("@oda_no", Convert.ToInt16(textBox_rezervasyon_cikisi_oda_no.Text));
                    int count2 = command2.ExecuteNonQuery();

                    if (count2 > 0)
                    {
                        MessageBox.Show("2. Sorgu çalıştı");
                    }
                    else
                    {
                        MessageBox.Show("2. Sorgu çalıştmadı");

                    }

                    transaction.Commit();
                    MessageBox.Show("Rezervasyon çıkış işlemi başarıyla tamamlandı!", "Rezervasyon Çıkış İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    // T.C ile sorgula alanını tekrar çalıştırarak veriler güncel kalacak.
                    button_rezarvasyon_sorgula.PerformClick();
                }

            }
            else
            {
                MessageBox.Show("Ücreti hesaplamadan çıkış işlemi yapılamaz!", "Rezervasyon Çıkış İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        // ------------------------------------------- * Ücret Hesapla * -------------------------------------------

        private void button_ucreti_hesapla_Click(object sender, EventArgs e)
        {
            if (textBox_rezervasyon_cikisi_adsoyad.Text != string.Empty || textBox_rezervasyon_cikisi_oda_no.Text != string.Empty)
            {
                UcretHesapla(textBox_rezervasyon_cikisi_ucret);
            }
            else
            {
                MessageBox.Show("Bu işlemi yapabilmek için; Öncelikle bir rezervasyon kaydı seçmeniz gerekmektedir!", "Rezervasyon Çıkış İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        // ------------------------------------------- * Metotlar * -------------------------------------------

        private void dataGridViewStyle(DataGridView DataGridView)
        {
            DataGridView.BackgroundColor = Color.White; //  dataGridView_gecmis_rezarvasyonlar: genel arka plan rengi; System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Control);
            DataGridView.BorderStyle = BorderStyle.None;
            DataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            DataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView.DefaultCellStyle.SelectionBackColor = Color.Beige; // Seçilmiş olan satırın arka plan rengi
            DataGridView.DefaultCellStyle.SelectionForeColor = Color.FromArgb(64, 64, 64);// Seçilmiş olan satırın metin rengi
            DataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing; // Opsiyonel
            DataGridView.RowHeadersWidth = 25; // dataGridView_gecmis_rezarvasyonlar.RowHeadersVisible = false;
            DataGridView.DefaultCellStyle.Font = new Font("Franklin Gothic Book", 9);
            DataGridView.ReadOnly = true;

            // Sütunlar
            DataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Franklin Gothic Book", 9, FontStyle.Bold);
            DataGridView.EnableHeadersVisualStyles = false;
            DataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            DataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64); // Sütun başlıklarının arka plan rengi: Eski;Color.FromArgb(64, 64, 64)
            DataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Sütun boyutunu otomatik ayarla
        }

        private void DataTableHeaderColumn(DataTable datatable, DataGridView DataGridView)
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
            datatable.Columns.Add("Rezervasyon Tipi", typeof(string));
            datatable.Columns.Add("Çıkış Tarihi", typeof(string));
            datatable.Columns.Add("Ücret", typeof(string));
            //datatable.Columns.Add.bu buttonColumn = new DataGridViewButtonColumn();
            //datatable.Columns.Add("boş", typeof(string));

            DataGridView.DataSource = datatable;
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // DataGridView'den seçilen satırın verilerini TextBox'lara yazalım
            if (dataGridView_rezervasyon_cikisi.SelectedRows.Count > 0)
            {
                // 'dataGridView_rezervasyon_cikisi' nın satırını bir değişkene atayalım
                DataGridViewRow selectedRow = dataGridView_rezervasyon_cikisi.SelectedRows[0];

                // Öncelikle Textbox'ları unsetleyelim
                ClearTextBox();

                // Ardından ilgili sütunlardaki değerleri TextBox'lara ata
                textBox_rezervasyon_cikisi_rezervasyon_no.Text = selectedRow.Cells["Rezarvasyon No"].Value.ToString();
                textBox_rezervasyon_cikisi_personeladsoyad.Text = selectedRow.Cells["İşlemi Yapan Personel"].Value.ToString();
                textBox_rezervasyon_cikisi_adsoyad.Text = selectedRow.Cells["Müşteri Ad Soyad"].Value.ToString();
                textBox_rezervasyon_cikisi_oda_no.Text = selectedRow.Cells["Oda No"].Value.ToString();
                textBox_rezervasyon_cikisi_giris_tarihi.Text = selectedRow.Cells["Giriş Tarihi"].Value.ToString();
                // Ücret hesaplanabilsin diye 'Giriş tarihi' alınıyor
                girisTarihi = selectedRow.Cells["Giriş Tarihi"].Value.ToString();
                textBox_rezervasyon_cikisi_cikisi_rezervsayon_durumu.Text = selectedRow.Cells["Rezervasyon Tipi"].Value.ToString();
                textBox_rezervasyon_cikisi_cikis_tarihi.Text = DateTime.Now.ToString(); // Çıkış Tarihi
            }
        }

        private void ClearEveryThing(DataTable datatable)
        {
            datatable.Rows.Clear();
        }

        private void ClearTextBox()
        {
            textBox_rezervasyon_cikisi_rezervasyon_no.Text = string.Empty;
            textBox_rezervasyon_cikisi_personeladsoyad.Text = string.Empty;
            textBox_rezervasyon_cikisi_adsoyad.Text = string.Empty;
            textBox_rezervasyon_cikisi_oda_no.Text = string.Empty;
            textBox_rezervasyon_cikisi_giris_tarihi.Text = string.Empty;
            textBox_rezervasyon_cikisi_cikisi_rezervsayon_durumu.Text = string.Empty;
            textBox_rezervasyon_cikisi_cikis_tarihi.Text = string.Empty;
            textBox_rezervasyon_cikisi_ucret.Text = string.Empty;
        }

        private void TextboxStyles(TextBox textbox)
        {
            textbox.Enabled = false;
            textbox.BackColor = Color.White;
        }

        // Ücret hesaplama fonksiyonu
        private void UcretHesapla(TextBox textBox)
        {
            // Tarihler Eşit mi? Bunu unutma!

            // Algoritma:
            // 1. Başla!

            // 2. Yıllar Eşit mi?   
            // 3. Yıllar Değil Arada Kaç Yıl Olduğunu Hesapla Ve Adım 5'e Git
            // 4. Yıllar Eşit İse; Adım 5'e Git

            // 5. Aylar Eşit mi?
            // 6. Aylar Eşit Değilse; Arada Kaç Ay Olduğunu Hesapla Ve Adım 8'e git
            // 7. Aylar Eşit İse; Adım 8'e git

            // 8. Günler Eşit Mi?
            // 9. Günler Eşit İse Adım 11'e git
            // 10. Günler Eşit Değil İse;  Arada Kaç gÜN Olduğunu Hesapla Ve Adım 11'e git
            // 11. Günlük Oda Ücreti İle Çarp Ve Tutarı Hesapla

            // 12. Bitir!


            // Tüm Değişkenleri Burada Tanımla

            // Neseleri tanıma
            DateTime.TryParse(girisTarihi, out DateTime date);

            // DateTime Bugun = DateTime.Now;
            string cikisTarihiMetni = textBox_rezervasyon_cikisi_cikis_tarihi.Text;
            DateTime cikisTarihi;
            DateTime.TryParse(cikisTarihiMetni, out cikisTarihi);

            // Giriş Tarihinin gününü, ayını ve yılını harici bir şekilde al
            string GirisGun = date.Day.ToString();
            string GirisAy = date.Month.ToString();
            string GirisYil = date.Year.ToString();

            // Bugünün gününü, ayını ve yılını harici bir şekilde al
            string CikisGun = cikisTarihi.Day.ToString();
            string CikisAy = cikisTarihi.Month.ToString();
            string CikisYil = cikisTarihi.Year.ToString();

            // Günlük kalma/oda ücreti
            int GunlukUcret = 340;

            // Hesaplanan ücret
            int HesaplananUcret = 0;

            // / Giriş günün; o ayın sonuna kadar olan gün sayısı
            int GirisGunSayisi = 0; // Aylar Eşit Değil İse de; Ayın giriş gününden, ayın son gününe kadar olan günleri al!


            // Giriş yılı ve çıkış yılı eşit mi?

            // Yıllar Eşit İse
            if (Convert.ToInt16(GirisYil) == Convert.ToInt16(CikisYil))
            {
                // MessageBox.Show("Yıllar Eşit!");
                // Aylar Eşit İse
                if (Convert.ToInt16(GirisAy) == Convert.ToInt16(CikisAy))
                {
                    // MessageBox.Show("Aylar Eşit!");
                    // Günler Eşit İse
                    if (Convert.ToInt16(GirisGun) == Convert.ToInt16(CikisGun))
                    {
                        // MessageBox.Show("Günler Eşit!");
                        MessageBox.Show("Hesaplanan Ücret: " + GunlukUcret);
                        textBox.Text = Convert.ToString(GunlukUcret);
                    }
                    // Günler Eşit Değil İse
                    else
                    {
                        // MessageBox.Show("Günler Eşit Değil!");
                        HesaplananUcret = (Convert.ToInt16(CikisGun) - Convert.ToInt16(GirisGun)) * GunlukUcret;
                        // MessageBox.Show("Toplam Gün Sayısı: " + (Convert.ToInt16(CikisGun) - Convert.ToInt16(GirisGun)));
                        MessageBox.Show("Hesaplanan Ücret: " + HesaplananUcret);
                        textBox.Text = Convert.ToString(HesaplananUcret);
                    }
                }
                // Aylar Eşit Değil İse 
                else
                {
                    // Giriş ayını 'Integer' olarak başka değişkene ata 
                    int GirisAyArtır = Convert.ToInt16(GirisAy);
                    // int AradaKacAyVar = 0;
                    int ToplamGunSayisi = 0;

                    // MessageBox.Show("Aylar Eşit Değil!");

                    // Girilen ayı, çıkış yapılacak aya kadar artır!
                    while (GirisAyArtır < Convert.ToInt16(CikisAy))
                    {
                        // (IF) - İlk ay artmadığından dolayı, ilk ayın: (Aynı ayın gün sayısı - Aynı ayın günü),
                        // Yani: Giriş yapılan ayın günüden, giriş yapılan ayın son gününe kadar olan tarih!
                        if (GirisAyArtır == Convert.ToInt16(GirisAy))
                        {
                            GirisGunSayisi = DateTime.DaysInMonth(Convert.ToInt16(GirisYil), Convert.ToInt16(GirisAy)); // Yıl, Ay;
                            GirisGunSayisi = (GirisGunSayisi - Convert.ToInt16(GirisGun)) + 1; // '+1' giriş yapılan günü sayması için
                            ToplamGunSayisi = GirisGunSayisi;
                        }
                        else
                        {
                            // Ayların gün sayılarını al
                            ToplamGunSayisi = ToplamGunSayisi + DateTime.DaysInMonth(Convert.ToInt16(GirisYil), GirisAyArtır); // Yıl, Ay;
                            // ToplamGunSayisi;
                        }
                        // Giriş ayının; ayını artır
                        GirisAyArtır++;
                        // AradaKacAyVar++;
                    }

                    // 'While' bitince toplam gün sayısı ve çıkış tarihinin gününe kadar olan günleri topla ve günlük ücret ile çarp!
                    HesaplananUcret = (ToplamGunSayisi + Convert.ToInt16(CikisGun)) * GunlukUcret;
                    // MessageBox.Show("Toplam Gün Sayısı: " + (ToplamGunSayisi + Convert.ToInt16(CikisGun)));
                    MessageBox.Show("Hesaplanan Ücret: " + HesaplananUcret);
                    textBox.Text = Convert.ToString(HesaplananUcret);
                }
            }
            // Yıllar Eşit Değil İse
            else
            {
                MessageBox.Show("Yıllar Eşit Değil!");
            }


        }

    }
}
