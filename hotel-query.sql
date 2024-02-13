
----------------------------------------------------------------------

--Veri Taban� Olu�turma
CREATE DATABASE hotel;

-- Not: Sorgular�n hiyerar�ik s�rayla �al��t�r�lmas� gerekmektedir.

-- 1. �al��t�r�lacak komut. 
CREATE TABLE katlar (
	kat_no INT PRIMARY KEY NOT NULL,
    kat_aciklamasi VARCHAR(500) NULL
);

-- 2. �al��t�r�lacak komut. 
CREATE TABLE odalar (
	oda_no INT PRIMARY KEY NOT NULL,
	oda_durum SMALLINT NOT NULL, 
	-- oda_durum: 0 - Bo� oda // 1 - Dolu oda // 2 - Ay�rt�lm�� oda // 4 - Kullan�lamayan oda
    oda_aciklamasi VARCHAR(500) NULL,
    kat_no INT,
	FOREIGN KEY (kat_no) REFERENCES katlar(kat_no) ON DELETE CASCADE
);

-- 3. �al��t�r�lacak komut. 
CREATE TABLE musteri_bilgileri (
    m_tc VARCHAR(11) PRIMARY KEY NOT NULL,
    m_ad VARCHAR(25) NOT NULL,
	m_soyad VARCHAR(25) NOT NULL,
    m_cinsiyet BIT NULL, -- 1 Erkek, 0 Kad�n,
	m_tel_no VARCHAR(10) NOT NULL,
	m_eposta VARCHAR(100) NULL,
	m_acik_adres VARCHAR(200) NULL,
	m_kan_grubu VARCHAR(10) NULL
);

/*
	 *Personel Yetki Durumlar�:
	 2 - M�d�r
	 1 - M�d�r Yard�mc�s�
	 0 - Normal Personel
*/

-- 4. �al��t�r�lacak komut. 
-- Personel Giri� Bilgileri Tablosu
CREATE TABLE personel_giris_bilgileri (
    p_g_id VARCHAR(12) PRIMARY KEY NOT NULL,
	p_g_yetki_durumu SMALLINT NOT NULL, /* 2 - M�d�r // 1 - M�d�r Yard�mc�s� // 0 - Normal Personel */
	p_g_aktiflik_durumu BIT NOT NULL, -- 1 - Personel hesab�; aktif // 0 - Personel hesab�; aktif de�il (i�ten ��km��);
    p_g_kullanici_ad VARCHAR(25) NOT NULL,
    p_g_sifre VARCHAR(50) NOT NULL
);

-- 5. �al��t�r�lacak komut. 
-- Personel Bilgileri Tablosu
CREATE TABLE personel_bilgileri (
    p_id VARCHAR(12) PRIMARY KEY NOT NULL,
    p_ad VARCHAR(25) NOT NULL,
    p_soyad VARCHAR(25) NOT NULL,
    p_cinsiyet BIT NULL, 
	-- Cinsiyet: 1 - Erkek // 0 Kad�n
    p_ise_baslama_tarihi DATETIME NOT NULL,
    p_isten_cikis_tarihi DATETIME NULL,
    p_tel_no VARCHAR(10) NOT NULL,
    p_eposta VARCHAR(100) NULL,
    p_kan_grubu VARCHAR(10) NULL,
    p_maas INT NULL,
	FOREIGN KEY (p_id) REFERENCES personel_giris_bilgileri(p_g_id)
);

-- 6. �al��t�r�lacak komut. 
CREATE TABLE rezervasyonlar (
	rezervasyon_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    p_id VARCHAR(12) NOT NULL,
	m_tc VARCHAR(11) NOT NULL,
	oda_no INT NOT NULL,
	giris_tarihi DATETIME NOT NULL,
	rezervasyon_durumu SMALLINT NOT NULL, 
	-- rezervasyon_durumu: 3 - S�resi ge�mi� oda ay�rtma i�lemi // 2 - Aktif ay�rt�lm�� oda // 1 - Aktif kullan�lan rezervasyon // 0 - Ge�mi� (kullan�lmayan) rezervasyon 
	cikis_tarihi DATETIME NULL, 
	ucret INT NULL,
	FOREIGN KEY (p_id) REFERENCES personel_giris_bilgileri(p_g_id),
	FOREIGN KEY (m_tc) REFERENCES musteri_bilgileri(m_tc),
	FOREIGN KEY (oda_no) REFERENCES odalar(oda_no)
); 

-- 7. �al��t�r�lacak komut. 
CREATE TABLE telefon_numaralari (
	telefon_no INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	aciklama VARCHAR(250) NULL,
	kat_no INT NOT NULL,
	oda_no INT NOT NULL,
	FOREIGN KEY (kat_no) REFERENCES katlar(kat_no),
	FOREIGN KEY (oda_no) REFERENCES odalar(oda_no)
);

-- -------------------------------------------------- * Static Kay�tlar *--------------------------------------------------


-- ---*Odalar ve Katlar*----- *_*

-- Oda Durum: 0 bo�, 1 dolu
-- Kat 1
BEGIN TRANSACTION;

INSERT INTO katlar(kat_no)
VALUES(1);

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(101, 0, 1); -- 101, 102, 103, 104, 105

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(102, 0, 1); 

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(103, 0, 1); 

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(104, 1, 1); -- 1: dolu

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(105, 0, 1);

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(106, 0, 1);

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(107, 0, 1);

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(108, 0, 1);

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(109, 0, 1);

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(110, 0, 1);

COMMIT;

-- Kat 2
BEGIN TRANSACTION;

INSERT INTO katlar(kat_no)
VALUES(2);

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(201, 0, 2); -- 201, 202, 203, 204, 205

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(202, 3, 2); -- 2: Kullan�lamaz oda 

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(203, 0, 2); 

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(204, 0, 2); 

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(205, 0, 2);

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(206, 0, 2);

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(207, 0, 2);

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(208, 0, 2);

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(209, 0, 2);

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(210, 0, 2);

COMMIT;

-- Kat 3 
BEGIN TRANSACTION;

INSERT INTO katlar(kat_no)
VALUES(3);

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(301, 0, 3); -- 301, 302, 303, 304, 305

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(302, 0, 3); 

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(303, 0, 3);

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(304, 0, 3); 

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(305, 3, 3); 

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(306, 0, 3); 

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(307, 0, 3); 

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(308, 0, 3); 

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(309, 0, 3); 

INSERT INTO odalar(oda_no, oda_durum, kat_no)
VALUES(310, 0, 3); 

COMMIT;


-- ---*Personel Kay�tlar�*----- *_-

-- Root kullan�c� ekleme i�lemi
BEGIN TRANSACTION; -- ��lemleri birle�tirilmi� bir i�lem olarak ele almak i�in bir i�lem ba�lat

-- 1. personel_giris_bilgileri tablosuna veri ekleme
INSERT INTO personel_giris_bilgileri(p_g_id, p_g_yetki_durumu, p_g_aktiflik_durumu, p_g_kullanici_ad, p_g_sifre)
VALUES('x2023thRO5s', 2, 1,'root','demo'); -- Kullan�c� Ad: root, �ifre: demo

-- 2. personel_bilgileri tablosuna veri ekleme
INSERT INTO personel_bilgileri(p_id, p_ad, p_soyad, p_cinsiyet, p_ise_baslama_tarihi, p_tel_no)
VALUES('x2023thRO5s','Root','Root',1, '2023-11-27 14:30:00',5434321);

-- ��lemi tamamla (commit)
COMMIT;


-- ---*�rnek M��teri Kay�tlar�*----- -_-

-- Cinsiyet Rakamlar� (bit): m_cinsiyet; 1 - erkek, 0 - kad�n,

INSERT INTO musteri_bilgileri(m_tc, m_ad, m_soyad, m_cinsiyet, m_tel_no)
VALUES('11111111111', 'Ali', 'Ak', 1, '5546547698');--5546547698

INSERT INTO musteri_bilgileri(m_tc, m_ad, m_soyad, m_cinsiyet, m_tel_no)
VALUES('22222222222', 'Ay�e', 'Kara', 0, '5446647091');

INSERT INTO musteri_bilgileri(m_tc, m_ad, m_soyad, m_cinsiyet, m_tel_no)
VALUES('33333333333', 'Ahmet', 'Demir', 1, '5447587993');


-- --------------------------------------------------*Sorgular*--------------------------------------------------


-- T�m Tablolar
SELECT * FROM katlar;
SELECT * FROM odalar;
SELECT * FROM telefon_numaralari;

SELECT * FROM personel_bilgileri;
SELECT * FROM personel_giris_bilgileri;

SELECT * FROM musteri_bilgileri;

SELECT * FROM rezervasyonlar;


-- ?_*

SELECT k.kat_aciklamasi, o.* FROM odalar AS o
INNER JOIN katlar AS k ON k.kat_no = o.kat_no
WHERE o.kat_no = 1;

SELECT o.*, k.kat_aciklamasi FROM odalar o
INNER JOIN katlar k ON o.kat_no = o.kat_no

USE master;
ALTER DATABASE hotel SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE hotel;