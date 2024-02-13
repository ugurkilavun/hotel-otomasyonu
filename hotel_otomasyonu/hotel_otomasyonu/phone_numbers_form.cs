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
    public partial class phone_numbers_form : Form
    {
        public phone_numbers_form()
        {
            InitializeComponent();
            dataGridView_tel_no.SelectionChanged += dataGridView_tel_no_SelectionChanged;
        }

        // Form içi global değişkenler
        private string connectionString = ConnectionStringClass.ConnectionStringVarible(); // Veri tabanı bağlantısı
        DataTable datatable = new DataTable();

        private void phone_numbers_form_Load(object sender, EventArgs e)
        {
            dataGridViewStyle();
            DataTableHeaderColumn(datatable);
            FormLoaded();

        }

        // ------------------------------------------- * Metotlar * -------------------------------------------
        private void FormLoaded()
        {
            SqlConnection connect = new SqlConnection(connectionString);

            try
            {
                connect.Open();
                string query = "SELECT * FROM telefon_numaralari ORDER BY telefon_no ASC";
                SqlCommand command = new SqlCommand(query, connect);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    datatable.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
                }

                dataGridView_tel_no.DataSource = datatable;
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

                textBox_tel_no.Text = selectedRow.Cells["Tel No"].Value.ToString();
                textBox_aciklama.Text = selectedRow.Cells["Açıklama"].Value.ToString();
                textBox_kat_no.Text = selectedRow.Cells["Kat No"].Value.ToString();
                textBox_oda_no.Text = selectedRow.Cells["Oda No"].Value.ToString();
            }

        }

        private void ClearTextBox()
        {
            textBox_kat_no.Text = string.Empty;
            textBox_tel_no.Text = string.Empty;
            textBox_oda_no.Text = string.Empty;
            textBox_aciklama.Text = string.Empty;
        }
    }
}
