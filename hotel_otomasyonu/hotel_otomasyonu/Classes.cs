using Microsoft.Data.SqlClient;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices;
using hotel_otomasyonu;
using AllQuerys; // Namespace



// ------------------------------------------------------------------------- *Sürükle Bırak Nesneleri* -------------------------------------------------------------------------


namespace hotel_otomasyonu
{
    // ConnectionString
    public class ConnectionStringClass
    {
        public static string ConnectionStringVarible()
        {
            // Veri tabananı bağlantısı.
            string ConnectionString = @"Data Source=HONOR\SQLEXPRESS;Initial Catalog=hotel;Integrated Security=True;Trust Server Certificate=True";

            return ConnectionString;
        }
    }
   
    // ====== Dynamic Items
    public class DynamicPictureBox : PictureBox
    {
        public DynamicPictureBox()
        {
            this.ForeColor = Color.White;
            this.Size = new System.Drawing.Size(150, 100);//Genişlik, Yükseklik
            //this.FlatStyle = FlatStyle.Flat;
            //this.FlatAppearance.BorderSize = 0;
            this.Cursor = Cursors.Hand;
            this.Padding = new Padding(0, 100, 0, 0);

            // Button'un arka plan rengini ayarlama
            this.BackColor = System.Drawing.Color.Transparent;

            // Button'un üzerine bir arka plan resmi eklemek için Image özelliğini ayarla
            //string imagePath = "images/double-bed.png"; // Resmin dosya yolunu belirleme

            // Button'un arka plan resmini ayarla
            //this.BackgroundImage = Image.FromFile(imagePath);

            // Button'un arka plan resmini ortala
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // Button'un tıklama olayını belirleme
            this.Click += new EventHandler(DynamicPictureBox_Click);
        }

        // Click olayı
        private void DynamicPictureBox_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.Name + ". Dinamik PictureBox'a tıklandı!");
        }

    }

    public class DynamicLabel : Label
    {
        public DynamicLabel()
        {
            this.ForeColor = Color.Black;
            this.Size = new System.Drawing.Size(140, 20);//Genişlik, Yükseklik
            this.BackColor = System.Drawing.Color.Transparent;
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BorderStyle = BorderStyle.None;
        }
    }

    public class DynamicButton : Button
    {
        // rooms_and_reservations_form'a değer göndermek için Click olayı.
        public event Action<string> ButtonClick;

        private string son_secilen_oda = string.Empty;
        public DynamicButton()
        {
            // Button'un özelliklerini özelleştirme
            this.ForeColor = Color.Black;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Size = new System.Drawing.Size(150, 100);
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Cursor = Cursors.Hand;
            this.Padding = new Padding(0, 60, 0, 0);

            // Button'un arka plan rengini ayarlama
            this.BackColor = System.Drawing.Color.Transparent;

            // Button'un üzerine bir arka plan resmi eklemek için Image özelliğini ayarla
            string imagePath = "images/double-bed.png"; // Resmin dosya yolunu belirleme

            // Button'un arka plan resmini ayarla
            // this.BackgroundImage = Image.FromFile(imagePath);

            // Button'un arka plan resmini ortala
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // Button'un tıklama olayını belirleme
            this.Click += new EventHandler(DynamicButton_Click);
        }

        // Click olayı
        private void DynamicButton_Click(object sender, EventArgs e)
        {

            SelectQuery SelectQuery = new SelectQuery();

            son_secilen_oda = this.Name;

            string RoomDescription = Convert.ToString(SelectQuery.RoomNoWithRoomDescription(son_secilen_oda));
            string RoomSituation = Convert.ToString(SelectQuery.RoomNoWithRoomSituation(son_secilen_oda));
            //MessageBox.Show("Funciyon sonrası");
            //string RoomSituation = string.Empty;

            MessageBox.Show("Oda Numarası: " + son_secilen_oda + "\n\r" + "Oda Durumu: " + RoomSituation + "\n\r" + "Oda Açıklaması: " + RoomDescription, "Oda Bilgileri", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // MessageBox.Show(f_secili + ". Dinamik Button'a tıklandı

            // Buton tıklanınca olayı tetikle

            // Öncellikle 'new_reservation_form' da 'DynamicButton'a tıklandığında Buttonun ismi 'son_secilen_oda' değişkenine atanır, 
            // Ardından da 'DynamicButton_ButtonClick' metoduna 'ButtonClick?.Invoke(son_secilen_oda)' ile 'son_secilen_oda'nın değeri gönderilir/atanır.
            ButtonClick?.Invoke(son_secilen_oda);
        }

    }

    public class DynamicFlowLayoutPanel : FlowLayoutPanel
    {
        public DynamicFlowLayoutPanel()
        {
            this.Size = new System.Drawing.Size(155, 135);//Genişlik, Yükseklik
            this.BorderStyle = BorderStyle.FixedSingle;
        }
    }

    // ====== Custom Items
    public class CustomButton : System.Windows.Forms.Button
    {
        private const int BorderWidth = 15;
        private bool isEnter = false;

        public CustomButton()
        {

            this.Text = "CustomButton";
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.Size = new System.Drawing.Size(100, 30);// Genişlik, Yükseklik
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.BorderColor = Color.Black; // Kenarlık rengini istediğiniz renge ayarlayabilirsiniz
            this.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.Padding = new Padding(20, 0, 0, 0); // İç Boşluk: Left, Top, Right, Bottom

            //System.Windows.Forms.Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.NoneEnabled;

            // Eventler
            this.Enter += CustomButton_Enter;
            this.Leave += CustomButton_Leave;
            this.MouseEnter += CustomButton_MouseEnter;
            this.MouseLeave += CustomButton_MouseLeave;
            //this.MouseHover += CustomButton_MouseHover;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Tıklanma durumunda kenarlık çizimi
            if (isEnter)
            {
                using (Pen pen = new Pen(this.FlatAppearance.BorderColor, BorderWidth))
                {
                    e.Graphics.DrawLine(pen, new Point(0, 0), new Point(0, this.Height));
                }
            }
        }

        // Eventler; Olaylar
        private void CustomButton_Enter(object? sender, EventArgs e)
        {
            isEnter = true;
            this.Invalidate(); // Yeniden çizim için OnPaint metodunu tetikler
        }

        private void CustomButton_Leave(object? sender, EventArgs e)
        {
            isEnter = false;
            this.Invalidate(); // Yeniden çizim için OnPaint metodunu tetikler
        }

        private void CustomButton_MouseEnter(object? sender, EventArgs e)
        {
            // Fare butonun üzerine gelince buradaki kod çalışacak
            this.BackColor = Color.White;
            this.ForeColor = Color.Red;
        }

        private void CustomButton_MouseLeave(object? sender, EventArgs e)
        {
            // Fare butonun üzerinden ayrılınca buradaki kod çalışacak
            this.BackColor = Color.White;
            this.ForeColor = Color.FromArgb(64, 64, 64);
        }


    } // CustomButton End
}

// ------------------------------------------------------------------------- *Oturum* -------------------------------------------------------------------------

namespace Sessions
{

    // Kullanıcı bilgilerini temsil eden sınıf
    public class UserInformation
    {
        public string? userID { get; set; }
        public int UserAuthorization { get; set; }

    }
    
}

// ------------------------------------------------------------------------- *Veri Tabanı İle Alækalı* -------------------------------------------------------------------------
namespace QueryCustomerFromTC
{
    public class ClassQueryCustomerFromTC
    {
     
    }

}

// ------------------------------------------------------------------------- *Personel, Müşteri ID Oluşturucu* -------------------------------------------------------------------------
namespace IDCreaters
{
    public class UserIDCreater
    {
      
        static int ThisYear() { 

            // Yıl nesnesini tanımla
            DateTime year = DateTime.Now;

            // Yıl alma fonksiyonunu çağırın
            int OnlyYear = year.Year;

            return OnlyYear;
        }

        // Büyük harf: uppercase
        static char RandomUppercaseLatter()
        {
            // Random nesnesini tanımla
            Random RandomNumber = new Random();

            // 1 ile 27 arasında sayı üret
            int RandomNumberInt = RandomNumber.Next(1, 27);

            // A harfinin ASCII değeri
            int asciiA = (int)'A';

            // Verilen sayıyı harfe çevirme
            char letter = (char)(asciiA + RandomNumberInt - 1);

            // Sayıyı harfe çevirme
            char randomLetter = letter;

            return letter;
        }

        // Küçük harf: lowercase
        static char RandomLowercaseLatter()
        {
            // Random nesnesini tanımla
            Random RandomNumber = new Random();

            // 1 ile 27 arasında sayı üret
            int RandomNumberInt = RandomNumber.Next(1, 27);

            // A harfinin ASCII değeri
            int asciiA = (int)'a';

            // Verilen sayıyı harfe çevirme
            char letter = (char)(asciiA + RandomNumberInt - 1);

            // Sayıyı harfe çevirme
            char randomLetter = letter;

            return letter;
        }

        static int OnlyRandomNumber()
        {
            // Random nesnesini tanımla
            Random RandomNumber = new Random();

            // 1 ile 27 arasında sayı üret
            int RandomNumberInt = RandomNumber.Next(0, 9);

            return RandomNumberInt;
        }

        // Kullanıcı ID oluşturma: Örnek 'x2023WhKL5s' Formatında
        internal static string CreatedUserID() // internal, public
        {
            string userID = "x" + ThisYear() + RandomUppercaseLatter() + RandomLowercaseLatter() + RandomUppercaseLatter() + RandomUppercaseLatter() + OnlyRandomNumber() + RandomLowercaseLatter();

            return userID;
        }

    }
}

// ------------------------------------------------------------------------- *Events* -------------------------------------------------------------------------

namespace AllEvents
{

    // Sadece rakamlara izin ver
    public class KeyPress
    {
        public void HandleKeyPress(object sender, KeyPressEventArgs e) 
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
            
    }




}

// ------------------------------------------------------------------------- *ALL Querys* -------------------------------------------------------------------------

namespace AllQuerys
{

    public class SelectQuery
    {
        // Personel ID ile Personel Ad ve Soyad Sorgulama.
        internal static string WithIDQueryPersonel(string connectionString, string IDparameters) {

            string PersonelNameAndLastName = string.Empty;

            SqlConnection connect = new SqlConnection(connectionString);

            try
            {
                connect.Open();

                string query = "SELECT p_ad, p_soyad FROM personel_bilgileri WHERE p_id = @IDparameters";

                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@IDparameters", IDparameters);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PersonelNameAndLastName = Convert.ToString(reader[0]) + " " + Convert.ToString(reader[1]);
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

            return PersonelNameAndLastName;
        }


        // T.C No ile Müşterinin Ad ve Soyad Sorgulama.
        internal string WithIDQueryCostumer(string connectionString, string IDparameters)
        {

            string PersonelNameAndLastName = string.Empty;

            SqlConnection connect = new SqlConnection(connectionString);

            try
            {
                connect.Open();

                string query = "SELECT m_ad, m_soyad FROM musteri_bilgileri WHERE m_tc = @IDparameters";

                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@IDparameters", IDparameters);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PersonelNameAndLastName = Convert.ToString(reader[0]) + " " + Convert.ToString(reader[1]);
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

            return PersonelNameAndLastName;
        }

        // T.C no ile müşterinin olup olmadğına bakma
        internal int OneParametersWithTCSQueryCount(string connectionString, string query, string Qparameters) {

            SqlConnection connect = new SqlConnection(connectionString);
            int count = -1;
            try { 

                connect.Open();

                SqlCommand commanad = new SqlCommand(query, connect);
                commanad.Parameters.AddWithValue("@parameters", Qparameters);

                count = Convert.ToInt16(commanad.ExecuteScalar());

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

            return count;

        }

        // Akif VE Deaktif Rezervasyon sorgusu
        internal int QueryReservation(string connectionString, string query, string parameters1, string parameters2)
        {

            SqlConnection connect = new SqlConnection(connectionString);
            int count = -1;

            try
            {

                connect.Open();

                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@parameters1", parameters1);
                command.Parameters.AddWithValue("@parameters2", parameters2);

                count = Convert.ToInt16(command.ExecuteScalar());


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

            return count;

        }

        // Oda numarası ile oda durumu sorgulama
        internal string RoomNoWithRoomSituation(string RoomNo)
        {
            string connectionString= ConnectionStringClass.ConnectionStringVarible();

            string RoomSituation = string.Empty;
            SqlConnection connect = new SqlConnection(connectionString);
            try
            {
                connect.Open();

                string query = "SELECT oda_durum FROM odalar WHERE oda_no = @oda_no";

                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@oda_no", RoomNo);
                SqlDataReader reader = command.ExecuteReader();
                 
                 while (reader.Read()) {
                    RoomSituation = Convert.ToString(reader[0]);
                }
                
                // RoomSituation = Convert.ToString(reader["oda_durum"]);

                //reader.Close();
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

            if (Convert.ToInt16(RoomSituation) == 0)
            {
                RoomSituation = "Boş";
            }
            else if (Convert.ToInt16(RoomSituation) == 1)
            {
                RoomSituation = "Dolu";
            }
            else if (Convert.ToInt16(RoomSituation) == 2)
            {
                RoomSituation = "Ayırtılmış";
            }
            else if (Convert.ToInt16(RoomSituation) == 4)
            {
                RoomSituation = "Kullanılamaz Oda";
            }

            return RoomSituation;
        }

        // oda numarası ile oda açıklamasını sorgulama
        internal string RoomNoWithRoomDescription(string RoomNo)
        {
            string connectionStirng = ConnectionStringClass.ConnectionStringVarible();

            string RoomDescription = string.Empty;
            SqlConnection connect = new SqlConnection(connectionStirng);
            try
            {
                connect.Open();

                string query = "SELECT oda_aciklamasi FROM odalar WHERE oda_no = @oda_no";

                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@oda_no", RoomNo);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    RoomDescription = Convert.ToString(reader[0]);
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

            return RoomDescription;
        }

    } // Class's End


}




