<h1 align="center"> 🏢 Hotel Otomasyonu </h1>
<p> ⏺ Proje, C# dili (.NET 7.0 Framework), Visual Studio 2022 ve Microsoft SQL Server yazılımları kullanılarak geliştirilmiştir.</p><br>

<h2 align="center"> ✒️ Projenin Amacı </h2>
<p> ⏺ Bu proje, Bilgisayar Programcılığı Programı/Bölümü'nde verilen Sistem Analizi ve Tasarımı dersi kapsamında bir bitirme ödevi/projesi olarak hazırlanmıştır.</p><br>

<h2 align="center"> # Hiyerarşik Kullanılan Sekmelere Ait Görseller # </h2>

<!-- Görseller/Images -->

<br>
<h3>➥ Giriş Formu </h3>

<p>Tek bir giriş ekranı üzerinden, kullanıcının yetki durumuna göre butonlar görünür ve aktif hale gelir veya gizlenip pasif olur.<br>Yetkiye bağlı olarak erişilebilecek işlemler şunlardır:</p>
<ul>
  <li>Oda ve Kat İşlemleri</li>
  <li>Personel Ekle</li>
  <li>Personel Bilgilerini Düzenle</li>
  <li>Telefon Numarası İşlemleri</li>
</ul>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/1-giris-form.png"><br>

<!-- -_- -->
<hr><br>

<h3>➥ Ana Form </h3>

<p>Ana Form üzerinde, kullanıcının adı, soyadı ve yetki durumu görüntülenmektedir.
Profil/Avatar alanına tıklandığında ise, oturum açmış kullanıcıya ait tüm bilgiler görüntülenir ve bu bilgiler düzenlenebilir.</p>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/2.1-ana-form.png"><br><br>
<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/2.2-bilgilerini-duzenle-form.png"><br>

<!-- *_- -->
<hr><br>

<h3>➥ Yeni Rezarvasyon Formu </h3>

<p><b>Not:</b> <strong>Tüm Odalar</strong> başlığı altında listelenen odalar, veri tabanından dinamik olarak çekilmektedir.
Veri tabanına yeni bir oda eklendiğinde (bu işlem <i>Oda ve Kat</i> İşlemleri ekranından kolayca yapılabilir), <strong>Yeni Rezervasyon</strong> Formu kapatılıp yeniden açıldığında, yeni eklenen oda otomatik olarak listelenir ve bu oda üzerinden işlem yapılabilir.</p>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/3.1.0-yeni-rezarvasyon-form.png"><br><br>

<p><strong>Normal Rezervasyon</strong></p>
<p>Müşterinin fiziksel olarak resepsiyonda bulunduğu durumlarda kullanılan rezervasyon türüdür.
Bu işlem sırasında çıkış tarihi girilmez. Müşteri çıkış yapmak istediğinde resepsiyona uğrayarak Müşteri Çıkışı işlemiyle çıkışını gerçekleştirmelidir.

Rezervasyon tamamlandığında, ilgili oda durumu kırmızıya döner ve müşteri çıkış yapana kadar oda üzerinde başka (çıkış işlemi hariç) herhangi bir işlem yapılamaz.</p>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/3.1.1-yeni-rezarvasyon_normal-rezarvasyon-form.png"><br>
<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/3.1.2-yeni-rezarvasyon_normal-rezarvasyon_onay-form.png"><br><br>

<p><strong>Geçici Süreliğine Ayırt</strong><p>
<p>Müşteri henüz gelmeden, kendisine özel bir oda ayırtmak istiyorsa bu işlem kullanılır.
Rezervasyon sırasında çıkış tarihi kesin olarak belirlenir. İşlem tamamlandığında oda rengi sarıya döner.

Müşteri, resepsiyona fiziksel olarak geldiğinde bu rezervasyon, <strong>Normal Rezervasyon</strong>'a dönüştürülebilir ve o andan itibaren ücretlendirme başlar.

Eğer müşteri, belirttiği tarihte otele gelmezse; sistem otomatik olarak çıkış işlemini gerçekleştirir ve oda durumu <strong>"boş oda"</strong> olarak güncellenir.</p>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/3.2.1-yeni-rezarvasyon_gecici-sureligine-ayirt-form.png"><br>
<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/3.2.2-yeni-rezarvasyon_gecici-sureligine-ayirt_onay-form.png"><br><br>

<p><strong>Oda Renkleri ve Anlamları</strong><p>

| Renk       | Anlam            |
| ---------- | ---------------- |
| 🟩 Yeşil   | Boş oda          |
| 🟥 Kırmızı | Dolu oda         |
| 🟨 Sarı    | Ayırtılmış oda   |
| ⬜ Gri     | Kullanılamaz oda |

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/3.3.0-yeni-rezarvasyon_oda-renkleri-form.png"><br><br>

<!-- *_* -->
<hr><br>

<h3>➥ Müşteri Ekle </h3>

<p>Yeni müşteri eklemek için kullanılır.</p>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/4-musteri-ekle-form.png"><br><br>

<!-- -_* -->
<hr><br>

<h3>➥ Müsteri Sorgula </h3>

<p>Sisteme kayıtlı müşterilerin bilgileri doğrulanmak, güncellenmek ve aktif ya da geçmiş rezervasyonları görüntülenmek amacıyla kullanılır.</p>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/5-musteri-sorgula-form.png"><br><br>

<!-- -_- -->
<hr><br>

<h3>➥ Müsteri Çıkışı </h3>

<p>Sisteme kayıtlı müşterilerin aktif rezervasyonlarını kapalı duruma getirmek, yani müşteri çıkışı işlemini gerçekleştirmek için kullanılır.</p>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/6-musteri-cikisi-form.png"><br><br>

<!-- ?_- -->
<hr><br>

<h3>➥ Tüm Kayıtlar </h3>

<p>Tüm aktif ve geçmiş rezervasyonlar ile oda ayırtma işlemleri bu bölümde listelenir.</p>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/7-tum-rezarvasyonlar-form.png"><br><br>

<!-- ?_? -->
<hr><br>

<h3>➥ Telefon Numaraları </h3>

<p>Oda ve katlar bağlı tüm telefon numaraları burada listelenir.</p>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/8-telefon-numaralari-form.png"><br><br>

<!-- ?,? -->
<hr><br>

<h3>➥ Yardım </h3>

<p>Sistemin kullanımını detaylı ve adım adım anlatan bir kılavuz.</p>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/9-yardim-form.png"><br><br>

<!-- ? -->
<hr>
<i>Aşağıda açıklanan formlara sadece Müdür ve Müdür Yardımcısı yetkisine sahip kullanıcılar erişebilir ve görüntüleyebilir.</i>
<br>

<h3>➥ Oda ve Kat İşlemleri </h3>

<p>Oda ve katlar bu form alanında eklenir.</p>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/10-oda-ve-kat-islemleri-form.png"><br><br>

<!-- ** -->
<hr><br>

<h3>➥ Personel Ekle </h3>

<p>Sisteme yeni bir personel/çalışan eklemek için kullanılır.</p>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/11-personel-ekle-form.png"><br><br>

<!-- ** -->
<hr><br>

<h3>➥ Personel Bilgilerini Düzenle </h3>

<p>Sisteme kayıtlı personel ve çalışanların bilgilerini düzenlemek ya da işten ayrılan personelin hesabını pasif hale getirmek için kullanılır.</p>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/12-personel-duzenle-form.png"><br><br>

<!-- ** -->
<hr><br>

<h3>➥ Telefon No İşlemleri </h3>

<p>Odalara ve katlara bağlı telefon numarası eklemek veya düzenlemek için kullanılır.</p>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/13-telefon-no-islemleri-form.png"><br><br>

<hr><br>

<h2 align="center"> ⭐ Sistem Kurulumu </h2>
<h3> ⭕ Veri Tabanı İşlemi</h3>
<p>Sistemin doğru çalışabilmesi için, <a href="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/hotel-query.sql">"hotel-query.sql"</a> ile belirtilen query dosyasıyla veri tabanı oluşturulmalıdır.</p>

<h3> ⭕ Sistem İçi Değişiklikler</h3>
<p>Sistem, veri tabanı bağlantısını tek bir değişken üzerinden yönetmektedir. Bu ayarı özelleştirmek için, <a href="https://github.com/ugurkilavun/hotel-otomasyonu/tree/main">hotel-otomasyonu</a> reposundaki <a href="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/hotel_otomasyonu/hotel_otomasyonu/Classes.cs">hotel_otomasyonu/hotel_otomasyonu/Classes.cs</a> dosyasında bulunan <i>public static string ConnectionStringVarible()</i> metodundaki <i>ConnectionString</i> değişkeninin değerinin güncellenmesi yeterlidir.
</p>
