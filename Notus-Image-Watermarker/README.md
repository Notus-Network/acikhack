# Notus Image Watermarker Kütüphanesi

Bu kütüphane, görsellerin watermark olarak imzalanması için oluşturulmuş bir kütüphanedir.
[**Nuget Linki**](https://www.nuget.org/packages/Notus-Image-Watermarker/1.0.0)

## Bağımlılıklar

Microsoft tarafından geliştirilen System.Drawing.Common kitaplığını kullanmaktadır.

### Nuget

- [**System.Drawing.Common**](https://www.nuget.org/packages/System.Drawing.Common/6.0.0)

## Kütüphanede bulunan tüm elemanlar 

### Sınıflar

- **Watermarker**

### Enumlar

- **ProtectionLevel** (Low, Medium, High)

### Fonksiyonlar

- **Watermarker.ProtectionType**
- **Watermarker.AddWatermarkToImage**

### Fonksiyonların İşlevi

#### (Private) Watermarker.ProtectionType(ProtectionLevel protectionLevel, bool isLight)

Korunma seviyesine bağlı olarak yazı büyüklüğü, yazı saydamlığı ve yazıların arasındaki boşluklarının değerlerini döndüren fonksiyon.

##### Aldığı Parametreler

- ProtectionLevel protectionLevel (Korunma Seviyesi)
- bool isLight (Açık renkli fotoğraflarda koyu renkli, Koyu renkli fotoğraflarda ise açık renkli watermark gözükmesi gerektiği için bu parametre eklenmiştir)

##### Döndürdüğü Değerler

- float[] { FontSize, XSpace, YSpace, Opacity } (Değerleri içeren float dizisi)

#### (Public) Watermarker.AddWatermarkToImage(string sourceName, string destinationPath, string walletKey, ProtectionLevel protectionLevel, bool imageIsLight)

Verilen adresteki fotoğrafa watermark ekleyen ve verilen pathe kaydeden fonksiyon.

##### Aldığı Parametreler

- string sourceName (Watermark eklenmemiş, çiğ fotoğrafın bulunduğu konum)
- string destinationPath (Watermark eklenmiş fotoğrafın hangi klasöre kaydedileceği)
- string walletKey (Kullanıcının wallet keyi)
- ProtectionLevel protectionLevel (Korunma Seviyesi)
- bool imageIsLight (Açık renkli fotoğraflarda koyu renkli, Koyu renkli fotoğraflarda ise açık renkli watermark gözükmesi gerektiği için bu parametre eklenmiştir)

##### Döndürdüğü Değerler

- bool (İşlemin düzgün bir şekilde tamamlandığını belirten bool)
