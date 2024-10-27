# Proje Takip Sistemi

Bu proje, birden fazla görevi projelere ekleyebilen ve görevlerin durumunu izleyebilen bir **Proje Takip Sistemi**'dir. Proje, kullanıcıların yeni projeler oluşturmasına, görevler eklemesine ve görevleri yönetmesine olanak tanır. 

## Amaç
- Bir projeye birden fazla görev eklenebilmesi
- Görevlerin durumlarının izlenmesi
- Kullanıcı yetkilendirmesiyle proje ve görevlerin güvenli şekilde yönetilmesi

## Teknolojiler
- **ASP.NET Core**: API geliştirme
- **Entity Framework Core**: Veritabanı işlemleri
- **MSSQL**: Veritabanı yönetimi
- **Katmanlı Mimari**: N-Tier yapı
- **JWT**: Kimlik doğrulama
- **Dependency Injection**: Bağımlılıkların yönetimi
- **SOLID Prensipleri**: Temiz kod ve sürdürülebilir yapı
- **AutoMapper**: Nesne dönüşümleri

## Özellikler
1. **Kimlik Doğrulama:** Kullanıcılar JWT ile sisteme giriş yapabilir.
2. **Proje Yönetimi:** Yeni projeler oluşturulabilir ve başlangıç-bitiş tarihleri eklenir.
3. **Görev Yönetimi:** Görevler projelere eklenebilir ve durumu güncellenebilir.
4. **Görev Silme/Düzenleme:** Görevlerin durumu değiştirilip silinebilir.
5. **Listeleme:** Proje ve görevlerin detaylı listesi görüntülenebilir.

## Veritabanı Tasarımı
### Projeler Tablosu (Projects)
| Alan Adı      | Veri Tipi   | Açıklama                    |
|---------------|-------------|-----------------------------|
| id            | INT         | Projenin benzersiz kimliği  |
| name          | VARCHAR     | Proje adı                   |
| start_date    | DATE        | Başlangıç tarihi            |
| end_date      | DATE        | Bitiş tarihi                |

### Görevler Tablosu (Tasks)
| Alan Adı        | Veri Tipi        | Açıklama                      |
|-----------------|------------------|-------------------------------|
| id              | INT              | Görevin benzersiz kimliği     |
| project_id      | INT (Foreign Key)| Proje ile ilişkilendirme      |
| title           | VARCHAR          | Görev başlığı                 |
| description     | TEXT             | Görev açıklaması              |
| creation_date   | DATETIME         | Oluşturulma tarihi            |
| status          | ENUM             | Durum: new, in_progress, completed |

## API Endpoints
- **/api/auth/login:** Kullanıcı girişi
- **/api/auth/register:** Yeni kullanıcı kaydı
- **/api/projects:** Proje oluşturma ve listeleme
- **/api/tasks:** Görev ekleme, düzenleme, silme

## Kullanım
1. **Kimlik Doğrulama:** Öncelikle kullanıcı giriş yapar.
2. **Proje Ekleme:** Kullanıcı, proje adı ve tarih bilgilerini girer.
3. **Görev Ekleme:** Seçilen projeye görev eklenir ve durum atanır.
4. **Listeleme ve Düzenleme:** Projeler ve görevler görüntülenir, düzenlenir veya silinir.

## Kurulum
1. **Proje Klonlama:**  
   ```bash
   git clone https://github.com/OnurBaha/ProjectTrackingSystem.git
