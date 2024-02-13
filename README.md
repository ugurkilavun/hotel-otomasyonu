<h1 align="center"> 🏢 Hotel Otomasyonu </h1
<p> ⏺ Proje; C dili, Visual Studio 2022 <i>(.NET 7.0 Framework)</i> ve Microsoft SQL Server yazılımları ile tasarlanmış/geliştirilmiştir.</p><br>

<h2 align="center"> ✒️ Projenin Amacı </h2>
<p> ⏺ Bilgisayar Programcılığı Programı/Bölümü, *Sistem Analizi ve Tasarım* dersi için bir bitirme ödevi/projesidir.</p><br>

<h2 align="center"> # Hiyerarşik Kullanılan Sekmelere Ait Görseller # </h2>

<!-- Görseller/Images -->

<br>
<h3>➥ Giriş Formu </h3>

<p>Tek bir giriş ekranından, kullanıcının yetki durumuna göre; <i>buttonlar görünür ve aktif</i> ya da <i>gizle ve pasif olcaktır</i> (Oda ve Kat İşlemleri, Personel Ekle, Personel Bilgilerini Düzenle ve Telefon No İşlemleri).</p>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/1-giris-form.png"><br>

<!-- -_- -->
<hr><br>

<h3>➥ Ana Form </h3>

<p>
 Ana Form'da kullanıcının adı, soyadı ve yetki durumu görüntülenmektedir. Avatar/Profil resmi bulunan alana tıklandığında ise, giriş yapan/aktif oturum açan kullanıcıya ait tüm bilgiler gösterilerek düzenlenebilmektedir.</p>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/2.1-ana-form.png"><br><br>
<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/2.2-bilgilerini-duzenle-form.png"><br>

<!-- *_- -->
<hr><br>

<h3>➥ Yeni Rezarvasyon Formu </h3>

<p><b>NOT:</b> <i>Tüm Odalar</i> başlığının altında bulunan odalar veri tabanından çekilmekte olup, veri tabanına yeni bi' oda eklenmesi durumunda (Oda ve Kat İşlemleri ile kolayca eklenebilir.) <i>Yeni Rezarvasyon Formu</i>'nu kapatıp açılması ile otomatik (dinamik) olarak listelenip, yeni eklenen oda üzerinde işlem yapılabilmektedir.</p>


<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/3.1.0-yeni-rezarvasyon-form.png"><br>

<p><b>Normal Rezarvasyon:</b> Müşteri fiziksel olarak resepsiyonda bulunması halinde kullanılacak rezervasyon işlemidir. Bu işlem sırasında çıkış tarihi girilmez, müşteri çıkış yapacağı zaman resepsiyona uğrayıp, resepsiyondan çıkış yapması gerekmektedir (Müşteri Çıkışı ile yapılır). İşlem yapıldığında oda durumu kırmızıya döner ve müşteri çıkış yapana kadar hiçbir işlem yapılamaz.</p>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/3.1.1-yeni-rezarvasyon_normal-rezarvasyon-form.png"><br>
<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/3.1.2-yeni-rezarvasyon_normal-rezarvasyon_onay-form.png"><br><br>

<p><b>Oda Ayırtma:</b> Müşteri gelmeden; kendisine ait oda ayırtmak istiyor ise bu işlem kullanılır. İşlem sırasında çıkış tarihi kesin olarak belirlenir, işlem yapıldıktan sonra ise oda rengi sarıya döner. Müşteri fiziksel olarak resepsiyona geldiğinde, oda <i>Normal Rezarvasyon</i>'a çevrilebilir ve fiziksel olarak resepsiyondan rezarvasyon yaptırdığı tarihten itibaren ücret uygulanır. Müşteri belirtiği tarihte gelmemesi durumunda ise, sistem otomatik olarak çıkış işlemini yapıp oda durumunu <code>boş odaya</code> çevirir.</p>

<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/3.2.1-yeni-rezarvasyon_gecici-sureligine-ayirt-form.png"><br>
<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/3.2.2-yeni-rezarvasyon_gecici-sureligine-ayirt_onay-form.png"><br><br>

<p><b>Oda Renkleri ve Anlamları:</b>
 <ul>
  <li>Yeşil: Boş oda</li>
  <li>Kırmızı: Dolu oda</li>
  <li>Sarı: Ayırtılmış oda</li>
  <li>Gri: Kullanılamaz oda</li>
 </ul>
</p>
<img src="https://github.com/ugurkilavun/hotel-otomasyonu/blob/main/ilgili-resimler/3.3.0-yeni-rezarvasyon_oda-renkleri-form.png"><br><br>

<hr><br><br>

