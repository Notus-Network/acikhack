# Notus File Uploader Demo

Bu proje, Notus Networkün dosya yükleme APIleri aracılığıyla görüntü yüklenmesi testi için hazırlanmıştır.

## Uygulamayı Nasıl Çalıştırabilirim?

Bir alt dizinde (acikhack\Notus-File-Uploader-Test\Notus-File-Uploader-Test) **dotnet run** komutu kullanmanız yeterlidir.

## Bağımlılıklar

Notus Core kütüphanesini kullanır.

### Nuget

- [**Notus.Core**](https://www.nuget.org/packages/Notus.Core)

## Demo ile İlgili Genel Bilgi

Dosya yüklemenin test edilmesi için yüklenmiş demo uygulamasıdır. Bu uygulama ile amaç cüzdan uygulaması esnasında kullanılacak kodların bir konsol uygulaması ile test edilmesi için web üzerinden çekilen görüntünün parçalar halinde networke gönderilmesi sağlanmaktadır. Tüm dosya parçalarının düzgün gönderilene kadar hata oluşması durumunda tekrar gönderim yapmaktadır.
