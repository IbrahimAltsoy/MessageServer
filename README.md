# abc

# 🧾 Smart Visit App – Backend & Admin Panel

---

## 🇹🇷 Proje Tanımı / 🇬🇧 Project Overview

**🇹🇷 Türkçe:**  
Smart Visit, çeşitli işletmelerin (oto yıkama, terzi, kuaför vb.) müşteri girişlerini dijitalleştirmesini ve takip etmesini sağlayan bir B2B uygulamasıdır. Müşteriler işletme girişinde QR kod okutarak kendilerini kaydeder, işletme müşterinin bilgilerini anlık olarak görür. Ürün veya hizmet hazır olduğunda SMS ile otomatik bilgilendirme yapılır.

**🇬🇧 English:**  
Smart Visit is a B2B application that enables businesses (e.g. car washes, tailors, hairdressers) to digitally register and manage their customers. Customers scan a QR code upon entry, submit their details, and the business is instantly notified. When the service is completed, an automated SMS is sent to the customer.

---

## 🧩 Proje Yapısı / Solution Structure

```bash
SmartVisit.sln
│
├── src/
│   ├── Core/
│   │   ├── Core.Application         # CQRS, DI, Rule Layer, Service Contracts
│   │   ├── Core.CrossCuttingConcerns # Caching, Logging, Validation, etc.
│   │   ├── Core.Helper              # Ortak yardımcı sınıflar
│   │   ├── Core.Mailing             # Mail şablonları, SMTP servis
│   │   ├── Core.Persistence         # Ortak repository interface'leri
│   │   ├── Core.Security            # Token üretimi, şifreleme, kimlik yönetimi
│   │   ├── Core.WebAPI              # Ortak response, hata, API base katmanı
│   │   └── Core.Test                # Test altyapısı
│
│   ├── Application/                # Servisler, Features, Helpers
│   ├── Domain/                     # Entity, DTO, Enum, Events
│   ├── Infrastructure/            # 3. parti entegrasyonlar (ör: SMS, Tatil API)
│   ├── Persistence/               # DbContext, Repository, Migrations, Interceptor, SmsSettings
│   ├── WebAPI/                    # Ana API projesi (mobil ve panel için)
│   └── SmartVisitServer.Web/      # Admin panel (Razor Pages veya MVC)
```

---

## ⚙️ Kullanılan Teknolojiler / Tech Stack

| Katman / Layer       | Teknolojiler / Tools                          |
| -------------------- | --------------------------------------------- |
| Backend Framework    | ASP.NET Core 9                                |
| Mimari               | Onion Architecture + Modular Monolith         |
| Veri Erişimi         | Entity Framework Core + Code First            |
| Authentication       | JWT + Custom Claims                           |
| Authorization        | Role Based Access + Policy Based              |
| Veritabanı           | SQL Server                                    |
| Validasyon           | FluentValidation                              |
| Dependency Injection | Built-in DI + Custom Service Registration     |
| Mapping              | AutoMapper                                    |
| Arka Plan İşlemler   | BackgroundService                             |
| Mail Servisi         | Custom Mailing Module (SMTP / Template Ready) |
| SMS Servisi          | 3. parti adapter (MassSMS gibi)               |
| Logging              | Serilog                                       |
| Exception Handling   | Global Exception Middleware                   |
| Deployment           | GitHub Actions + FTP (Plesk Server)           |

---

## 🧠 Mimarî Yapılar / Architectural Patterns

| Yapı / Pattern                |
| ----------------------------- |
| Onion Architecture            |
| CQRS                          |
| MediatR                       |
| Repository Pattern            |
| Unit of Work                  |
| Custom Interceptors           |
| Domain Events                 |
| Business Rules Layer          |
| Validation Behaviors          |
| Performance Logging           |
| Exception Middleware          |
| TokenHandler (JWT)            |
| AutoMapper                    |
| Custom Authorize Attributes   |
| Custom ServiceRegistration.cs |

---

## 🚀 Kurulum / Installation

### Gerekli Bağımlılıklar:

- .NET 9 SDK
- SQL Server
- SMTP hesabı (opsiyonel)
- SMS API servisi (opsiyonel)

### Başlangıç:

```bash
1. Veritabanını oluşturun ve bağlantıyı appsettings.json dosyasına girin
2. Migrations klasörü üzerinden database güncellemesi yapın:
   dotnet ef database update --project Persistence

3. API'yi başlatın:
   dotnet run --project WebAPI

4. Admin paneli için:
   dotnet run --project SmartVisitServer.Web
```

---

## 📌 Özellikler / Key Features

- 📲 QR kod ile müşteri kayıt sistemi
- ✉️ Otomatik SMS ve e-posta bildirimleri
- 📊 Raporlama (Günlük, Aylık)
- 🛠️ SMS şablonları ve zamanlama ayarları
- 👤 Yetkili personel girişi
- 🧾 Kalan SMS görüntüleme
- 🧠 Custom Rule engine (ör: randevuya özel hatırlatma)
- 🔒 Rol bazlı yetkilendirme (Admin / Personel ayrımı)

---

## 🤝 Katkı ve Geri Bildirim / Contributing

**🇹🇷** Geri bildirim, katkı ve önerileriniz için pull request veya issue gönderebilirsiniz.  
**🇬🇧** Feel free to submit issues or pull requests for feedback and improvements.

---

## 📄 Lisans / License

**🇹🇷** Bu proje açık kaynak değildir. Tüm hakları saklıdır.  
**🇬🇧** This project is not open-source. All rights reserved.
