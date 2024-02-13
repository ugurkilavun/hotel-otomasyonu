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

namespace hotel_otomasyonu
{
    public partial class phone_number_processes_form : Form
    {
        public phone_number_processes_form()
        {
            InitializeComponent();
            dataGridView_tel_no.SelectionChanged += dataGridView_tel_no_SelectionChanged;
        }

        // Form içi global değişkenler
        private string connectionString = ConnectionStringClass.ConnectionStringVarible(); // Veri tabanı bağlantısı
        DataTable datatable = new DataTable();


        // Form yüklendiğinde
        private void phone_number_processes_form_Load(object sender, EventArgs e)
        {

            dataGridViewStyle();
            DataTableHeaderColumn(datatable);

            SqlConnection connect = new SqlConnection(connectionString);

            try
            {
                // 1.Sorgu
                connect.Open();

                string katlarQuery = "SELECT kat_no FROM katlar ORDER BY kat_no ASC";

                SqlCommand katlarCommand = new SqlCommand(katlarQuery, connect);
                SqlDataReader katlatReader = katlarCommand.ExecuteReader();

                while (katlatReader.Read())
                {
                    comboBox_kat_no.Items.Add(katlatReader[0].ToString());
                }
                katlatReader.Close();
                comboBox_kat_no.SelectedIndex = 0;

                // 2.Sorgu

                string odalarQuery = "SELECT oda_no FROM odalar ORDER BY oda_no ASC";

                SqlCommand odalarCommand = new SqlCommand(odalarQuery, connect);

                SqlDataReader odalarReader = odalarCommand.ExecuteReader();
                while (odalarReader.Read())
                {
                    comboBox_oda_no.Items.Add(odalarReader[0].ToString());
                }
                odalarReader.Close();
                comboBox_oda_no.SelectedIndex = 0;
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

        }//

        private void button_ekle_Click(object sender, EventArgs e)
        {
            if (textBox_tel_no.Text == string.Empty)
            {
                MessageBox.Show("* İle gösterilen alanlar zorunlu olarak doldurulmalıdır!", "Telefon Numarası Ekle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlConnection connect = new SqlConnection(connectionString);
                try
                {
                    connect.Open();
                    int count = OdaVeTelNo();

                    if (count == 0)
                    {
                        string insertQuery = "INSERT INTO telefon_numaralari(telefon_no, aciklama, kat_no, oda_no) VALUES(@telefon_no, @aciklama, @kat_no, @oda_no)";

                        SqlCommand insertCommand = new SqlCommand(insertQuery, connect);
                        insertCommand.Parameters.AddWithValue("@telefon_no", textBox_tel_no.Text);
                        insertCommand.Parameters.AddWithValue("@aciklama", textBox_aciklama.Text);
                        insertCommand.Parameters.AddWithValue("@kat_no", Convert.ToInt16(comboBox_kat_no.Text));
                        insertCommand.Parameters.AddWithValue("@oda_no", Convert.ToInt16(comboBox_oda_no.Text));
                        int insertCount = Convert.ToInt16(insertCommand.ExecuteNonQuery());
                        if (insertCount > 0)
                        {
                            button_tel_no_goster.PerformClick();
                            MessageBox.Show("Veri eklendi");
                            textBox_tel_no.Text = string.Empty;
                            textBox_aciklama.Text = string.Empty;
                        }
                        else
                        {
                            MessageBox.Show("Veri eklenemedi");
                        }
                    }
                    else if (count == 1)
                    {
                        MessageBox.Show("Girilen telefon numarası halihazırda kullanımda!", "Telefon Numarası Ekle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (count == 2)
                    {
                        MessageBox.Show("Girilen oda numarasına kayıtlı telefon numarası bulunmaktadır!", "Telefon Numarası Ekle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (count == 3)
                    {
                        MessageBox.Show("Girilen telefon numarası ve oda numarasına halihazırda kullanımda!", "Telefon Numarası Ekle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Anlaşılmayan Hata! Yekili kişilere bu hatayı bildiriniz!", "Telefon Numarası Ekle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

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

        private void button_tel_no_goster_Click(object sender, EventArgs e)
        {
            datatable.Rows.Clear();

            SqlConnection connect = new SqlConnection(connectionString);

            try
            {
                connect.Open();

                string query = "SELECT * FROM telefon_numaralari ORDER BY oda_no ASC";
                SqlCommand command = new SqlCommand(query, connect);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //reader[0].ToString();
                    datatable.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
                }
                dataGridView_tel_no.DataSource = datatable;
            }
            finally
            {
                if (connect != null)
                {
                    connect.Close();
                }
            }
        }

        private void button_tel_no_gizle_Click(object sender, EventArgs e)
        {
            datatable.Rows.Clear();
        }

        private void button_sil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bu telefon numarasını silmek istiyor musunuz?", "Telefon No Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                SqlConnection connect = new SqlConnection(connectionString);

                try
                {
                    connect.Open();

                    string query = "DELETE FROM telefon_numaralari WHERE telefon_no = @telefon_no";
                    SqlCommand command = new SqlCommand(query, connect);
                    command.Parameters.AddWithValue("@telefon_no", textBox_sil_tel_no.Text);
                    int count = command.ExecuteNonQuery();
                    if (count > 0)
                    {
                        button_tel_no_goster.PerformClick();
                        ClearTextBox();
                        MessageBox.Show("Telefon numarası başarıyla silindi!", "Telefon No Silme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        button_tel_no_goster.PerformClick();
                        MessageBox.Show("Telefon numarası silinemedi! Lütfen bu hatayı yetkili kişilere iletiniz.", "Telefon No Silme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

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

            }
        }

        // ------------------------------------------- * Metotlar * -------------------------------------------

        private void textBox_tel_no_KeyPress(object sender, KeyPressEventArgs e)
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

        // Oda ve tel numarası birden çok kullanılmasın diye
        private int OdaVeTelNo()
        {
            int returned = 0;
            SqlConnection connect = new SqlConnection(connectionString);

            try
            {
                // Hata koduna göre mesaj yazdır!
                // 0: Girilen oda ve tel numarası eklenebilir durumda
                // 1: Sadece girilen tel eklenebilir durumda değil
                // 2: Sadece girilen oda eklenebilir durumda değil
                // 3: Girilen oda ve tel numarası eklenebilir durumda değil


                connect.Open();


                string odalarQuery = "SELECT Count(oda_no) FROM telefon_numaralari WHERE oda_no = @oda_no";
                SqlCommand odalarCommand = new SqlCommand(odalarQuery, connect);
                odalarCommand.Parameters.AddWithValue("@oda_no", Convert.ToInt16(comboBox_oda_no.Text));

                int odalarCount = Convert.ToInt16(odalarCommand.ExecuteScalar());

                // Oda var ise
                if (odalarCount > 0)
                {
                    //MessageBox.Show("oda var");
                    string telefonNumaralariQuery = "SELECT Count(telefon_no) FROM telefon_numaralari WHERE telefon_no = @telefon_no";

                    SqlCommand telefonNumaralariCommand = new SqlCommand(telefonNumaralariQuery, connect);
                    telefonNumaralariCommand.Parameters.AddWithValue("@telefon_no", Convert.ToInt32(textBox_tel_no.Text));
                    int telefonNumaralariCount = Convert.ToInt16(telefonNumaralariCommand.ExecuteScalar());

                    if (telefonNumaralariCount > 0)
                    {
                        returned = 3;
                    }
                    else
                    {
                        returned = 2;
                    }
                }
                //Oda yok ise
                else
                {
                    //MessageBox.Show("oda yok");
                    string telefonNumaralariQuery = "SELECT Count(telefon_no) FROM telefon_numaralari WHERE telefon_no = @telefon_no";

                    SqlCommand telefonNumaralariCommand = new SqlCommand(telefonNumaralariQuery, connect);
                    telefonNumaralariCommand.Parameters.AddWithValue("@telefon_no", Convert.ToInt64(textBox_tel_no.Text));
                    int telefonNumaralariCount = Convert.ToInt16(telefonNumaralariCommand.ExecuteScalar());

                    if (telefonNumaralariCount > 0)
                    {
                        returned = 1;
                    }
                    else
                    {
                        returned = 0;
                    }

                }


            }
            finally
            {
                if (connect != null)
                {
                    connect.Close();
                }
            }

            return returned;
        }

        private void dataGridViewStyle()
        {
            dataGridView_tel_no.BackgroundColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.White); ; //  dataGridView_gecmis_rezarvasyonlar: genel arka plan rengi
            dataGridView_tel_no.BorderStyle = BorderStyle.None;
            dataGridView_tel_no.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView_tel_no.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView_tel_no.DefaultCellStyle.SelectionBackColor = Color.Beige; // Seçilen satırın arka plan rengi
            dataGridView_tel_no.DefaultCellStyle.SelectionForeColor = Color.FromArgb(64, 64, 64);// Seçilen satırın metin rengi
            dataGridView_tel_no.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing; // Opsiyonel
            dataGridView_tel_no.RowHeadersWidth = 25; // dataGridView_gecmis_rezarvasyonlar.RowHeadersVisible = false;
            dataGridView_tel_no.DefaultCellStyle.Font = new Font("Franklin Gothic Book", 9);


            // Sütunlar
            dataGridView_tel_no.ColumnHeadersDefaultCellStyle.Font = new Font("Franklin Gothic Book", 9, FontStyle.Bold);
            dataGridView_tel_no.EnableHeadersVisualStyles = false;
            dataGridView_tel_no.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView_tel_no.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64); // Sütun başlıklarının arka plan rengi
            dataGridView_tel_no.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView_tel_no.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Sütun boyutunu otomatik ayarla
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
            datatable.Columns.Add("Tel No", typeof(string));
            datatable.Columns.Add("Açıklama", typeof(string));
            datatable.Columns.Add("Kat No", typeof(string));
            datatable.Columns.Add("Oda No", typeof(string));

            dataGridView_tel_no.DataSource = datatable;
        }

        private void dataGridView_tel_no_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_tel_no.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView_tel_no.SelectedRows[0];

                ClearTextBox();

                textBox_sil_tel_no.Text = selectedRow.Cells["Tel No"].Value.ToString();
                textBox_sil_aciklama.Text = selectedRow.Cells["Açıklama"].Value.ToString();
                textBox_sil_kat_no.Text = selectedRow.Cells["Kat No"].Value.ToString();
                textBox_sil_oda_no.Text = selectedRow.Cells["Oda No"].Value.ToString();
            }

        }

        private void ClearTextBox()
        {
            textBox_sil_kat_no.Text = string.Empty;
            textBox_sil_tel_no.Text = string.Empty;
            textBox_sil_oda_no.Text = string.Empty;
            textBox_sil_aciklama.Text = string.Empty;
        }

        
    }
}
