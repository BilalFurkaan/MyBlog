# 🎓 Mentoring Notları - Adım Adım Blog Projesi

## 📅 Tarih: 2024
## 👨‍💻 Mentor: Claude AI
## 👨‍🎓 Öğrenci: [Senin Adın]

---

## 🎯 Proje: "Adım Adım Blog"

### 📝 Proje Konsepti
- **Ana Fikir:** Öğrenme yolculuğunu paylaşan kişisel blog
- **Hedef:** Clean Architecture ile modern blog sistemi
- **İçerik:** Kod öğrenme deneyimleri, teknik yazılar, proje geliştirme süreçleri

### 🏗️ Proje Yapısı (Clean Architecture)
```
AdimAdimBlog/
├── AdimAdimBlog.Domain/          # Entities, Interfaces
├── AdimAdimBlog.Application/      # Services, DTOs, Use Cases
├── AdimAdimBlog.Persistence/      # Database, Repositories
└── Presentation/
    ├── AdimAdimBlog.WebApp/       # MVC Web App
    └── AdimAdimBlog.WebApi/       # REST API
```

---

## 📋 Task Listesi (Adım Adım)

### **Faz 1: Temel Yapı (1-2 Hafta)**

#### Task 1.1: Proje Oluşturma
**Hedef:** Clean Architecture projesi kurulumu
**Yapılacaklar:**
- Solution oluştur
- Katmanları ekle (Domain, Application, Persistence, Presentation)
- NuGet paketlerini yükle
- Referansları ayarla

**İpucu:** Mevcut Shoper projesini referans al
**Kontrol:** Proje derleniyor mu?

#### Task 1.2: Domain Entities
**Hedef:** Blog için temel varlıkları tanımlama
**Yapılacaklar:**
- `Post` entity (Blog yazısı)
- `Category` entity (Kategori)
- `Comment` entity (Yorum)
- `User` entity (Kullanıcı)

**İpucu:** Shoper'daki Product, Category gibi entity'lere bak
**Kontrol:** Entity'ler doğru property'lere sahip mi?

### **Faz 2: Veritabanı ve Repository (1 Hafta)**

#### Task 2.1: DbContext ve Migration
**Hedef:** Entity Framework kurulumu
**Yapılacaklar:**
- DbContext oluştur
- Entity konfigürasyonları
- İlk migration'ı oluştur

**İpucu:** Shoper'daki AppDbContext'e bak
**Kontrol:** Migration başarılı mı?

#### Task 2.2: Repository Pattern
**Hedef:** Veri erişim katmanı
**Yapılacaklar:**
- Generic Repository interface
- Repository implementasyonları
- Özel repository'ler (PostRepository, CategoryRepository)

**İpucu:** Shoper'daki IRepository ve implementasyonlarına bak
**Kontrol:** CRUD işlemleri çalışıyor mu?

### **Faz 3: Application Layer (1-2 Hafta)**

#### Task 3.1: DTOs
**Hedef:** Veri transfer nesneleri
**Yapılacaklar:**
- CreatePostDto, UpdatePostDto, ResultPostDto
- CreateCategoryDto, UpdateCategoryDto
- CreateCommentDto, UpdateCommentDto

**İpucu:** Shoper'daki DTO yapısına bak
**Kontrol:** DTO'lar entity'lerle uyumlu mu?

#### Task 3.2: Services
**Hedef:** İş mantığı katmanı
**Yapılacaklar:**
- IPostService, PostService
- ICategoryService, CategoryService
- ICommentService, CommentService

**İpucu:** Shoper'daki ProductService'e bak
**Kontrol:** Service'ler CRUD işlemlerini yapıyor mu?

### **Faz 4: Web Uygulaması (2 Hafta)**

#### Task 4.1: Controllers
**Hedef:** MVC Controller'ları
**Yapılacaklar:**
- PostController (CRUD)
- CategoryController
- CommentController
- HomeController (Ana sayfa)

**İpucu:** Shoper'daki CartController'a bak
**Kontrol:** Controller'lar doğru action'ları döndürüyor mu?

#### Task 4.2: Views
**Hedef:** Kullanıcı arayüzü
**Yapılacaklar:**
- Ana sayfa (Blog yazıları listesi)
- Blog yazısı detay sayfası
- Kategori sayfaları
- Admin paneli (CRUD işlemleri)

**İpucu:** Shoper'daki View yapısına bak
**Kontrol:** Sayfalar doğru verileri gösteriyor mu?

### **Faz 5: Gelişmiş Özellikler (2-3 Hafta)**

#### Task 5.1: Kullanıcı Sistemi
**Hedef:** Authentication ve Authorization
**Yapılacaklar:**
- Kullanıcı kayıt/giriş
- Rol tabanlı yetkilendirme
- Blog yazısı sahipliği

#### Task 5.2: Dosya Yükleme
**Hedef:** Resim yükleme sistemi
**Yapılacaklar:**
- Blog yazısı için resim yükleme
- Resim önizleme
- Dosya validasyonu

#### Task 5.3: Arama ve Filtreleme
**Hedef:** Gelişmiş arama
**Yapılacaklar:**
- Blog yazısı arama
- Kategori filtreleme
- Tarih filtreleme

---

## 🎓 Öğrenme Hedefleri

### **Teknik Beceriler:**
- Clean Architecture
- Entity Framework Core
- Repository Pattern
- Dependency Injection
- MVC Pattern
- LINQ Queries

### **Soft Skills:**
- Proje planlama
- Problem çözme
- Dokümantasyon
- Kod organizasyonu

---

## 📚 Referans Proje: Shoper

Bu projede öğrendiğimiz konular:
- Clean Architecture yapısı
- Repository Pattern implementasyonu
- Service Layer kullanımı
- DTO Pattern
- Entity Framework Core
- MVC Pattern

---

## 🚀 Başlama Talimatları

1. **Shoper projesi bittikten sonra başla**
2. **Bu dosyayı referans al**
3. **Her task'ı tamamladıktan sonra işaretle**
4. **Sorun yaşadığında mentor'a danış**

---

## ✅ İlerleme Takibi

- [ ] Task 1.1: Proje Oluşturma
- [ ] Task 1.2: Domain Entities
- [ ] Task 2.1: DbContext ve Migration
- [ ] Task 2.2: Repository Pattern
- [ ] Task 3.1: DTOs
- [ ] Task 3.2: Services
- [ ] Task 4.1: Controllers
- [ ] Task 4.2: Views
- [ ] Task 5.1: Kullanıcı Sistemi
- [ ] Task 5.2: Dosya Yükleme
- [ ] Task 5.3: Arama ve Filtreleme

---

**Not:** Bu dosya GitHub'a push edilecek ve referans olarak kullanılacak.
