using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace hotel_otomasyonu
{
    public partial class room_and_floor_processes_form : Form
    {
        // Form içi global değişkenler
        DataTable dataTable_odalar = new DataTable();
        DataTable dataTable_katlar = new DataTable();
        private string connectionString = ConnectionStringClass.ConnectionStringVarible(); // Veri tabanı bağlantısı
        string? girisTarihi = string.Empty;
        string? globalMusteriTC = string.Empty;


        public room_and_floor_processes_form()
        {
            InitializeComponent();
            dataGridView_odalar.SelectionChanged += dataGridView_odalar_SelectionChanged;
            dataGridView_katlar.SelectionChanged += dataGridView_katlar_SelectionChanged;
        }

        private void room_and_floor_processes_form_Load(object sender, EventArgs e)
        {

            button_tum_odalari_goster.Focus();

            // dataGridView stilleri
            dataGridViewStyle(dataGridView_odalar);
            dataGridViewStyle(dataGridView_katlar);

            // DataTable başlıkları
            DataTableHeaderColumnRooms();
            DataTableHeaderColumnFloors();

            // ComboBox içerikleri
            ComboBoxLoadFloor(comboBox_oda_kat_no);

            // ComboBox'lar indexleri
            comboBox_oda_durumu.SelectedIndex = 0;
            comboBox_oda_kat_no.SelectedIndex = 0;
        }

        private void button_oda_ekle_Click(object sender, EventArgs e)
        {
            // 'textBox_oda_no' değeri dolu ise
            if (textBox_oda_no.Text != string.Empty)
            {
                //MessageBox.Show("1. if geçildi!");

                SqlConnection connect = new SqlConnection(connectionString);
                try
                {
                    connect.Open();

                    string query = "SELECT Count(*) FROM odalar WHERE oda_no = @oda_no_select";

                    SqlCommand command = new SqlCommand(query, connect);
                    command.Parameters.AddWithValue("@oda_no_select", textBox_oda_no.Text);
                    int count = Convert.ToInt16(command.ExecuteScalar());

                    // Eklenmeye çalışılan oda numarası hali hazırda mevcut ise
                    if (count > 0)
                    {
                        MessageBox.Show("Bu 'Oda Numarası ile işlem yapılamaz! 'Eklenmeye çalışan 'Oda Numarası' hali hazırda mevcut!", "Oda Ekle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    // Eklenmeye çalışılan oda numarası hali hazırda mevcut değil ise
                    else
                    {
                        //0 Aktif, 4 Kullanılamaz
                        int roomSituation = 4;
                        if (comboBox_oda_durumu.SelectedItem == "Aktif")
                        { // 0 - Aktif, 4 - Kullanılamaz
                            roomSituation = 0;
                        }
                        else
                        {
                            roomSituation = 4;
                        }

                        //MessageBox.Show("2. if'e gecildi");
                        //MessageBox.Show("kat:" + comboBox_oda_kat_no.SelectedValue);

                        string insertQuery = "INSERT INTO odalar(oda_no, oda_durum, oda_aciklamasi, kat_no) VALUES(@oda_no, @oda_durum, @oda_aciklamasi, @kat_no)";

                        SqlCommand insertCommand = new SqlCommand(insertQuery, connect);
                        insertCommand.Parameters.AddWithValue("@oda_no", textBox_oda_no.Text);
                        insertCommand.Parameters.AddWithValue("@oda_durum", roomSituation);
                        insertCommand.Parameters.AddWithValue("@oda_aciklamasi", textBox_oda_aciklamasi.Text);
                        insertCommand.Parameters.AddWithValue("@kat_no", comboBox_oda_kat_no.SelectedItem);// comboBox_oda_kat_no.SelectedValue

                        int insertCount = Convert.ToInt16(insertCommand.ExecuteNonQuery());
                        // Eklenmeye başarılı ise
                        if (insertCount > 0)
                        {
                            MessageBox.Show("Oda başarılı bir şekilde eklendi", "Oda Ekle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox_oda_no.Text = string.Empty;
                            comboBox_oda_durumu.SelectedIndex = 0;
                            textBox_oda_aciklamasi.Text = string.Empty;
                            comboBox_oda_kat_no.SelectedIndex = 0;
                            button_tum_odalari_gizle.PerformClick();
                        }
                        // Eklenmeye başarısız ise
                        else
                        {
                            MessageBox.Show("Oda eklenemedi!", "Oda Ekle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
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
            // 'textBox_oda_no' değeri dolu değil ise
            else
            {
                MessageBox.Show("'*' ile gösterilen alanlar zorunlu olarak doldurulmalıdır!", "Oda Ekle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Kat ekle
        private void button_kat_ekle_Click(object sender, EventArgs e)
        {
            // 'textBox_kat_no' değeri dolu ise
            if (textBox_kat_no.Text != string.Empty)
            {
                SqlConnection connect = new SqlConnection(connectionString);

                try
                {
                    connect.Open();
                    // textBox_kat_no.Text
                    // Eklenmeye çalışılan 'Kat Numarası' hali hazırda mevcut ise
                    int selectCount = CountFloor(textBox_kat_no.Text);
                    if (selectCount > 0)
                    {
                        MessageBox.Show("Bu 'Kat Numarası' ile işlem yapılamaz! 'Eklenmeye çalışan 'Kat Numarası' hali hazırda mevcut!", "Kat Ekle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    // Eklenmeye çalışılan 'Kat Numarası' hali hazırda mevcut değil ise
                    else
                    {
                        string insertQuery = "INSERT INTO katlar(kat_no, kat_aciklamasi) VALUES(@kat_no, @kat_aciklamasi)";

                        SqlCommand insertCommand = new SqlCommand(insertQuery, connect);
                        insertCommand.Parameters.AddWithValue("@kat_no", textBox_kat_no.Text);
                        insertCommand.Parameters.AddWithValue("@kat_aciklamasi", textBox_kat_aciklamasi.Text);

                        int insertCount = Convert.ToInt16(insertCommand.ExecuteNonQuery());

                        // Eklenmeye başarılı ise
                        if (insertCount > 0)
                        {
                            MessageBox.Show("Kat başarılı bir şekilde eklendi", "Kat Ekle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox_kat_no.Text = string.Empty;
                            textBox_kat_aciklamasi.Text = string.Empty;
                            button_tum_katlari_gizle.PerformClick();

                            // Tekrardan oda eklenmek istenirse 'Kat Numaraları' güncellensin diyedir!
                            ComboBoxLoadFloor(comboBox_oda_kat_no);
                        }
                        // Eklenmeye başarısız ise
                        else
                        {
                            MessageBox.Show("Kat eklenemedi!", "Kat Ekle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
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
            // 'textBox_kat_no' değeri dolu değil ise
            else
            {
                MessageBox.Show("'*' ile gösterilen alanlar zorunlu olarak doldurulmalıdır!", "Kat Ekle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ==========* !Verileri Kısmı

        // Odalar
        private void button_tum_odalari_goster_Click(object sender, EventArgs e)
        {
            //77777
            dataTable_odalar.Rows.Clear();

            SqlConnection connect = new SqlConnection(connectionString);
            try
            {
                connect.Open();

                // Odalar
                string roomQuery = "SELECT * FROM odalar";
                SqlCommand roomCommand = new SqlCommand(roomQuery, connect);

                SqlDataReader roomReader = roomCommand.ExecuteReader();

                while (roomReader.Read())
                {
                    string roomName = roomReader[0].ToString();

                    // Oda Durumunu Çevirme
                    string roomSituation = string.Empty;
                    if (Convert.ToInt16(roomReader[1]) == 0)
                    {
                        roomSituation = "Aktif";
                    }
                    else if (Convert.ToInt16(roomReader[1]) == 1)
                    {
                        roomSituation = "Dolu";
                    }
                    else if (Convert.ToInt16(roomReader[1]) == 2)
                    {
                        roomSituation = "Ayırtılmış";
                    }
                    else
                    {
                        roomSituation = "Kullanılamaz";
                    }

                    string roomDescription = roomReader[2].ToString();
                    string roomFloor = roomReader[3].ToString();

                    dataTable_odalar.Rows.Add(roomName, roomSituation, roomDescription, roomFloor);
                    dataGridView_odalar.DataSource = dataTable_odalar;
                }
                roomReader.Close();


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
        private void button_tum_odalari_gizle_Click(object sender, EventArgs e)
        {
            dataTable_odalar.Rows.Clear();
        }

        // Katlar
        private void button_tum_katlari_goster_Click(object sender, EventArgs e)
        {
            dataTable_katlar.Rows.Clear();
            SqlConnection connect = new SqlConnection(connectionString);
            try
            {
                connect.Open();

                // Katlar
                string floorQuery = "SELECT * FROM katlar";

                SqlCommand floorCommand = new SqlCommand(floorQuery, connect);

                SqlDataReader floorReader = floorCommand.ExecuteReader();

                while (floorReader.Read())
                {
                    string floorName = floorReader[0].ToString();
                    string floorDesciption = floorReader[1].ToString();

                    dataTable_katlar.Rows.Add(floorName, floorDesciption);
                    dataGridView_katlar.DataSource = dataTable_katlar;
                }
                floorReader.Close();

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

        private void button_tum_katlari_gizle_Click(object sender, EventArgs e)
        {
            dataTable_katlar.Rows.Clear();
        }

        // =========- Duzenle Kısmı
        // Oda Düzenle
        private void button_oda_duzenle_Click(object sender, EventArgs e)
        {
            // 'textBox_oda_duzenle_oda_no' boş değil ise
            if (textBox_oda_duzenle_oda_no.Text != string.Empty)
            {
                SqlConnection connect = new SqlConnection(connectionString);

                try
                {
                    connect.Open();

                    string query = "UPDATE odalar SET oda_durum = @oda_durum, oda_aciklamasi = @oda_aciklamasi, kat_no = @kat_no WHERE oda_no = @oda_no_where ";

                    SqlCommand command = new SqlCommand(query, connect);
                    
                    int roomSituation = 0;
                    // 'comboBox_oda_duzenle_oda_durumu' Aktif ise
                    if (comboBox_oda_duzenle_oda_durumu.SelectedItem == "Aktif")
                    {
                        roomSituation = 0;
                    }
                    // 'comboBox_oda_duzenle_oda_durumu' Kullanılamaz ise
                    else
                    {
                        roomSituation = 4;
                    }
                    command.Parameters.AddWithValue("@oda_durum", roomSituation);

                    command.Parameters.AddWithValue("@oda_aciklamasi", textBox_oda_duzenle_oda_aciklamasi.Text);
                    command.Parameters.AddWithValue("@kat_no", comboBox_oda_duzenle_kat_oda_no.SelectedItem);
                    command.Parameters.AddWithValue("@oda_no_where", textBox_oda_duzenle_oda_no.Text);

                    int count = Convert.ToInt16(command.ExecuteNonQuery());

                    if (count > 0)
                    {
                        button_tum_odalari_goster.PerformClick();
                        MessageBox.Show("Oda düzenleme işlemi başarıyla gerçekleşti", "Oda Düzenle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Hata: Oda düzenleme işlemi gerçekleşemedi!", "Oda Düzenle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            // 'textBox_oda_duzenle_oda_no' boş ise
            else
            {
                MessageBox.Show("Herhangi bir oda seçilmeden düzenleme İşlemi yapılamaz!", "Oda Düzenle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_odayi_sil_Click(object sender, EventArgs e)
        {
            if (textBox_oda_duzenle_oda_no.Text != string.Empty)
            {
                DialogResult result = MessageBox.Show("Bu odayı silmek istiyor musunuz?", "Oda Düzenle", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Evet seçilirse
                if (result == DialogResult.Yes)
                {
                    SqlConnection connect = new SqlConnection(connectionString);
                    // MessageBox.Show("sill!", "Oda Düzenle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    try
                    {
                        connect.Open();

                        string query = "DELETE FROM odalar WHERE oda_no = @oda_no";

                        SqlCommand command = new SqlCommand(query, connect);
                        command.Parameters.AddWithValue("@oda_no", textBox_oda_duzenle_oda_no.Text);
                        int count = Convert.ToInt16(command.ExecuteNonQuery());

                        if (count > 0)
                        {
                            button_tum_odalari_goster.PerformClick();
                            UpdateRoomSituatinFalse();
                            MessageBox.Show("Oda başarılı bir şekilde silindi!", "Oda Sil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Oda silme işlemi başarısız oldu!", "Oda Sil", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            else
            {
                MessageBox.Show("Herhangi bir oda seçilmeden düzenleme İşlemi yapılamaz!", "Oda Düzenle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Kat Düzenle
        private void button_kat_duzenle_Click(object sender, EventArgs e)
        {
            if (textBox_kat_duzenle_kat_no.Text != string.Empty)
            {
                SqlConnection connect = new SqlConnection(connectionString);

                try
                {
                    connect.Open();
                    string query = "UPDATE katlar SET kat_aciklamasi = @kat_aciklamasi WHERE kat_no = @kat_no";
                    
                    SqlCommand command = new SqlCommand(query, connect);
                    command.Parameters.AddWithValue("@kat_aciklamasi", textBox_kat_duzenle_kat_aciklama.Text);
                    command.Parameters.AddWithValue("@kat_no", textBox_kat_duzenle_kat_no.Text);
                    int count = Convert.ToInt16(command.ExecuteNonQuery());

                    if (count > 0)
                    {
                        button_tum_katlari_goster.PerformClick();
                        MessageBox.Show("Kat düzenleme işlemi başarıyla gerçekleşti", "Kat Düzenle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Kat düzenleme işlemi gerçekleşemedi", "Kat Düzenle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            else
            {
                MessageBox.Show("Herhangi bir kat seçilmeden düzenleme İşlemi yapılamaz!", "Kat Düzenle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        // Kat Sil
        private void button_kati_sil_Click(object sender, EventArgs e)
        {
            if (textBox_kat_duzenle_kat_no.Text != string.Empty)
            {
                SqlConnection connect = new SqlConnection(connectionString);
                DialogResult result = MessageBox.Show("Bu katı silmek istiyor musunuz?", "Kat Düzenle", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Evet seçilirse
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        connect.Open();
                        string countQuery = "SELECT Count(*) FROM odalar WHERE kat_no = @kat_no_count";
                        SqlCommand countCommand = new SqlCommand(countQuery, connect);
                        countCommand.Parameters.AddWithValue("@kat_no_count", textBox_kat_duzenle_kat_no.Text);
                        int floorCount = Convert.ToInt16(countCommand.ExecuteScalar());
                        // Silinmesi istenilen kat herhangi bir oda numarasına ile bağlı mı?

                        // Bir kat numarasına bağlı ise
                        if (floorCount > 0)
                        {
                            MessageBox.Show("Bu silme işlemi yapılamaz. Hali hazırda bu kat numarasına bağlı odalar mevcuttur; öncelikle silinmesi istenilen kat numarasına bağlı 'TÜM' odalar silinmelidir!", "Kat Sil", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        // Bir kat numarasına bağlı değil ise
                        else
                        {
                            string query = "DELETE FROM katlar WHERE kat_no = @kat_no";

                            SqlCommand command = new SqlCommand(query, connect);
                            command.Parameters.AddWithValue("@kat_no", textBox_kat_duzenle_kat_no.Text);
                            int count = Convert.ToInt16(command.ExecuteNonQuery());
                            if (count > 0)
                            {
                                button_tum_katlari_goster.PerformClick();
                                UpdateFloorSituatinFalse();
                                MessageBox.Show("Kat başarılı bir şekilde silindi!", "Kat Sil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Kat silme işlemi başarısız oldu!", "Kat Sil", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
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
            else
            {
                MessageBox.Show("Herhangi bir kat seçilmeden silme İşlemi yapılamaz!", "Kat Silme", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // -------------------------------------------------------------- * Metotlar * --------------------------------------------------------------

        // =-----------= Genel Metotlar
        private void dataGridViewStyle(DataGridView dataGridView)
        {
            dataGridView.BackgroundColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Control); ; //  dataGridView_gecmis_rezarvasyonlar: genel arka plan rengi
            //dataGridView.BackgroundColor = Color.White; //  dataGridView_gecmis_rezarvasyonlar: genel arka plan rengi (beyaz)
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.Beige; // Seçilen satırın arka plan rengi
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.FromArgb(64, 64, 64);// Seçilen satırın metin rengi
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing; // Opsiyonel
            dataGridView.RowHeadersWidth = 25; // dataGridView_gecmis_rezarvasyonlar.RowHeadersVisible = false;
            dataGridView.DefaultCellStyle.Font = new Font("Franklin Gothic Book", 9);

            // Sütunlar
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Franklin Gothic Book", 9, FontStyle.Bold);
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64); // Sütun başlıklarının arka plan rengi
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Sütun boyutunu otomatik ayarla
        }

        private void DataTableHeaderColumnRooms()
        {
            dataTable_odalar.Columns.Add("Oda No", typeof(string));
            dataTable_odalar.Columns.Add("Oda Durumu", typeof(string));
            dataTable_odalar.Columns.Add("Oda Açıklaması", typeof(string));
            dataTable_odalar.Columns.Add("Bağlı Olduğu Kat", typeof(string));
            dataGridView_odalar.DataSource = dataTable_odalar;
        }

        private void DataTableHeaderColumnFloors()
        {
            dataTable_katlar.Columns.Add("Kat No", typeof(string));
            dataTable_katlar.Columns.Add("Kat Açıklama", typeof(string));

            dataGridView_katlar.DataSource = dataTable_katlar;
        }

        // =-----------= Oda Metotları
        private void textBox_oda_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private int CountRoom(string oda_no)
        {
            SqlConnection connect = new SqlConnection(connectionString);
            int selectCount = 0;
            try
            {
                connect.Open();

                string selectQuery = "SELECT Count(*) FROM odalar WHERE oda_no = @oda_no_select";

                SqlCommand selectCommand = new SqlCommand(selectQuery, connect);
                selectCommand.Parameters.AddWithValue("@oda_no_select", oda_no);// textBox_kat_no.Text
                selectCount = Convert.ToInt16(selectCommand.ExecuteScalar());
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
            return selectCount;
        }

        // Odalar
        private void dataGridView_odalar_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_odalar.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView_odalar.SelectedRows[0];

                //textBox_rezervasyon_cikisi_personeladsoyad.Text = selectedRow.Cells["İşlemi Yapan Personel"].Value.ToString();
                if (selectedRow.Cells["Oda Durumu"].Value.ToString() == "Dolu" || selectedRow.Cells["Oda Durumu"].Value.ToString() == "Ayırtılmış")
                {
                    UpdateRoomSituatinFalse();
                    MessageBox.Show("Oda durumu: 'Dolu' ya da 'Ayırtılmış' durumda ise oda düzenlemesi yapılamaz!", "Oda Düzenle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Her şeyi aktif et
                    UpdateRoomSituatinTrue();
                    // 'comboBox_oda_duzenle_kat_oda_no' nesnesini doldur
                    ComboBoxLoadFloor(comboBox_oda_duzenle_kat_oda_no);

                    // MessageBox.Show("Değiştiriliebilir");
                    // textBox_oda_duzenle_oda_no.Enabled = true;
                    // comboBox_oda_duzenle_oda_durumu.Enabled = true;
                    // textBox_oda_duzenle_oda_aciklamasi.Enabled = true;
                    // comboBox_oda_duzenle_kat_oda_no.Enabled = true;

                    textBox_oda_duzenle_oda_no.Text = selectedRow.Cells["Oda No"].Value.ToString();
                    comboBox_oda_duzenle_oda_durumu.Text = selectedRow.Cells["Oda Durumu"].Value.ToString();
                    textBox_oda_duzenle_oda_aciklamasi.Text = selectedRow.Cells["Oda Açıklaması"].Value.ToString();
                    comboBox_oda_duzenle_kat_oda_no.Text = selectedRow.Cells["Bağlı Olduğu Kat"].Value.ToString();
                }
            }
        }

        // Oda güncelleme işleminde her şeyi deaktif et
        private void UpdateRoomSituatinFalse()
        {
            textBox_oda_duzenle_oda_no.Enabled = false;
            comboBox_oda_duzenle_oda_durumu.Enabled = false;
            textBox_oda_duzenle_oda_aciklamasi.Enabled = false;
            comboBox_oda_duzenle_kat_oda_no.Enabled = false;

            textBox_oda_duzenle_oda_no.Text = string.Empty;
            comboBox_oda_duzenle_oda_durumu.SelectedIndex = 0;
            textBox_oda_duzenle_oda_aciklamasi.Text = string.Empty;
            comboBox_oda_duzenle_kat_oda_no.Items.Clear();

            button_oda_duzenle.Enabled = false;
            button_odayi_sil.Enabled = false;
        }

        // Oda güncelleme işleminde her şeyi aktif et
        private void UpdateRoomSituatinTrue()
        {
            textBox_oda_duzenle_oda_no.Enabled = false;
            comboBox_oda_duzenle_oda_durumu.Enabled = true;
            textBox_oda_duzenle_oda_aciklamasi.Enabled = true;
            comboBox_oda_duzenle_kat_oda_no.Enabled = true;

            textBox_oda_duzenle_oda_no.Text = string.Empty;
            comboBox_oda_duzenle_oda_durumu.SelectedIndex = 0;
            textBox_oda_duzenle_oda_aciklamasi.Text = string.Empty;
            //comboBox_oda_duzenle_kat_oda_no.SelectedIndex = 0;

            button_oda_duzenle.Enabled = true;
            button_odayi_sil.Enabled = true;
        }



        // =-----------= Kat Metotları
        private void textBox_kat_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void ComboBoxLoadFloor(System.Windows.Forms.ComboBox comboBox)
        {
            // BU ifade; bu metot birden çok yerde kullanıdığı için 'ComboBox'larda veri birikintisi yapmasın diye vardır!
            comboBox.Items.Clear();

            // 
            SqlConnection connect = new SqlConnection(connectionString);

            try
            {
                connect.Open();

                string query = "SELECT kat_no FROM katlar";

                SqlCommand command = new SqlCommand(query, connect);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox.Items.Add(reader[0].ToString());
                }
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
        }

        private int CountFloor(string kat_no)
        {
            SqlConnection connect = new SqlConnection(connectionString);
            int selectCount = 0;
            try
            {
                connect.Open();

                string selectQuery = "SELECT Count(*) FROM katlar WHERE kat_no = @kat_no_select";

                SqlCommand selectCommand = new SqlCommand(selectQuery, connect);
                selectCommand.Parameters.AddWithValue("@kat_no_select", kat_no);// textBox_kat_no.Text
                selectCount = Convert.ToInt16(selectCommand.ExecuteScalar());
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
            return selectCount;
        }

        private void dataGridView_katlar_SelectionChanged(object sender, EventArgs e)
        {
            // dataTable_katlar.Columns.Add("Kat No", typeof(string));
            // dataTable_katlar.Columns.Add("Kat Açıklama", typeof(string));
            if (dataGridView_katlar.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView_katlar.SelectedRows[0];

                // Her şeyi aktif et
                UpdateFloorSituatinTrue();

                textBox_kat_duzenle_kat_no.Text = selectedRow.Cells["Kat No"].Value.ToString();
                textBox_kat_duzenle_kat_aciklama.Text = selectedRow.Cells["Kat Açıklama"].Value.ToString();
            }
        }

        private void UpdateFloorSituatinFalse()
        {
            textBox_kat_duzenle_kat_no.Text = string.Empty;
            textBox_kat_duzenle_kat_aciklama.Text = string.Empty;

            textBox_kat_duzenle_kat_no.Enabled = false;
            textBox_kat_duzenle_kat_aciklama.Enabled = false;

            button_kat_duzenle.Enabled = false;
            button_kati_sil.Enabled = false;
        }

        // Oda güncelleme işleminde her şeyi aktif et
        private void UpdateFloorSituatinTrue()
        {
            textBox_kat_duzenle_kat_no.Text = string.Empty;
            textBox_kat_duzenle_kat_aciklama.Text = string.Empty;

            textBox_kat_duzenle_kat_no.Enabled = false;
            textBox_kat_duzenle_kat_aciklama.Enabled = true;

            button_kat_duzenle.Enabled = true;
            button_kati_sil.Enabled = true;
        }

        
    }
}
