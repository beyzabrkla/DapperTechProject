# 📊 Ad-Analytics: 1 Milyondan fazla Veri ile Küresel Reklam Analitiği

## 🌍 Proje Senaryosu
Dünya çapında yayın yapan devasa bir reklam ağını yönettiğinizi hayal edin. Web sitelerinde (Publishers) yayınlanan reklamların (Campaigns) her bir görüntülenme ve tıklanma anı, sistemimize bir "log" olarak kaydediliyor. 

Elimizdeki **1.048.575 satırlık veri**, aslında dünyanın dört bir yanındaki insanların reklamlarımıza verdiği gerçek tepkileri temsil ediyor. <br>
Bu proje, bu devasa veri yığınını (Big Data) işleyerek reklam verenlerin (Clients) şu kritik sorularına cevap vermek için geliştirilmiştir:

* **Kampanya Performansı:** "Yaz Sezonu kampanyam toplamda ne kadar izlendi ve etkileşim aldı?" <br>
* **Coğrafi Strateji:** "Reklamlarım hangi ülkede veya bölgede daha çok dikkat çekiyor?" <br>
* **Bütçe Optimizasyonu:** "Param hangi yayıncı sitesinde (Publisher) daha verimli harcanıyor? En düşük maliyetle (CPC) en yüksek tıklamayı nerede alıyorum?"

## 🎨 Tasarım Yaklaşımı (Stitch AI)
Bu projenin arayüz tasarımı **Stitch AI** platformu kullanılarak kurgulanmıştır. 
- **Modern UI/UX:** AI destekli tasarım süreçleri sayesinde verilerin en okunabilir ve estetik şekilde sunulması sağlandı.
- **Kullanıcı Odaklı:** Karmaşık veri setleri, Stitch AI'ın sunduğu modern panel yapısıyla sadeleştirilerek son kullanıcı (reklam veren) için karar destek mekanizmasına dönüştürüldü.

## 🚀 Teknik Hedefler
Bu projenin ana amacı, **Big Data** ölçeğindeki veriyi anlamlı iş zekası (BI) raporlarına dönüştürmektir. 1 milyonu aşkın satır arasında kaybolmadan, saniyeler içinde şu sonuçları üretiyorum:

- **Hız ve Performans:** Dapper ORM kullanarak doğrudan SQL optimizasyonu ile milisaniyelik yanıt süreleri.
- **Veri Görselleştirme:** Karmaşık tablolar yerine Chart.js ile "bir bakışta analiz" imkanı.
- **Verimlilik Analizi:** Tıklama başına maliyet (Avg. CPC) üzerinden yayıncı performans ölçümü.

## 🛠️ Teknolojik Stack
* **Backend:** .NET 8.0 (ASP.NET Core MVC)
* **Veri Erişimi:** Dapper ORM (Yüksek performanslı veri çekme)
* **Veritabanı:** MS SQL Server (1M+ Kayıt)
* **Frontend:** Tailwind CSS & Chart.js (Modern ve responsive arayüz)
* **Mimari:** N-Tier Architecture & Repository Pattern

## 📁 Katmanlı Mimari Yapısı
- **EntityLayer:** Veri tabanı şemalarının (Campaign, AdImpression, Region, vb.) modellenmesi.
- **BusinessLayer:** İş mantığının yönetildiği ve Repository pattern'in uygulandığı katman.
- **DTOLayer:** Performans odaklı, sadece gerekli verinin taşındığı DTO sınıfları.
- **UI Layer:** Son kullanıcının (Reklam Veren) veriyi analiz ettiği dashboard paneli.


## 📊 Dashboard Paneli Özellikleri
- **Üst Özet Kartları:** Toplam Gösterim, Tıklama Sayısı, CTR (Tıklama Oranı) ve Ortalama Tıklama Maliyeti.
- **Harcama Analizi:** Bütçeyi en çok kullanan kampanyaların analizi.
- **Bölgesel Dağılım:** Reklamların coğrafi performans haritası.
- **Sektörel Dağılım:** Reklamların kategorilerine göre (E-Ticaret, Eğitim, Sağlık vb.) yoğunluk analizi.
- **Yayıncı Verimliliği:** En iyi ROI (Yatırım Getirisi) sağlayan yayıncıların listelenmesi.
<img width="1919" height="908" alt="Ekran görüntüsü 2026-04-26 202805" src="https://github.com/user-attachments/assets/5251f242-201e-46b9-a0e7-7c90484f88c1" />
<img width="1916" height="906" alt="Ekran görüntüsü 2026-04-26 202819" src="https://github.com/user-attachments/assets/fa1550a8-c68e-4922-93fd-4959e164c483" />

## 📝 AdImpression (Analitik Log Yönetimi & Akıllı Filtreleme)
Bu modül, küresel reklam ağındaki her bir etkileşimi (görüntülenme ve tıklama) takip eden bir "Uçuş Kayıt Cihazı" görevi görür.
Milyonlarca satır veri arasından sadece ihtiyacınız olanı çekip çıkarmanızı sağlayan yüksek performanslı bir arama motoru gibi çalışır.

#### 🎨 Kullanıcı Deneyimi (UI/UX)
**Stitch AI** tarafından tasarlanan bu sayfada veriler sadece listelenmez, anlamlandırılır:
- **Durum İndikatörleri:** Tıklama (Yeşil) ve Gösterim (Gri) logları görsel simgelerle anında ayırt edilebilir.
- **Zengin Detay Kartları:** Her log satırı; zaman damgası, benzersiz Log ID, yayıncı URL'si ve maliyet bilgisiyle şeffaf bir şekilde sunulur.
- **Akıllı Filtreleme Paneli:** Üst kısımda yer alan dropdown menüler sayesinde, 1 milyon satır içerisinden örneğin "Almanya (DE) bölgesindeki Evcil Hayvan Dostu Ürünler kampanyasının tıklamaları" saniyeler içinde süzülebilir.

### 📸 Modül Görselleri
<img width="1386" height="1245" alt="localhost_7019_AdImpression_AdImpressionList" src="https://github.com/user-attachments/assets/a87062ee-dafb-4a89-bc36-7f4b0748ce1d" />
<img width="1386" height="1245" alt="localhost_7019_AdImpression_AdImpressionList_campaignId=23 publisherId= regionId=2 isClicked=" src="https://github.com/user-attachments/assets/2c9218dd-f733-4660-92dd-d09e6ce8a71d" />

## 📢 Kampanya Yönetimi (Campaign Management)
Bu modül, reklam verenlerin stratejilerini hayata geçirdiği, bütçelerini yönettiği ve kampanya detaylarını optimize ettiği kontrol merkezidir.

### 🏗️ Teknik Uygulama ve Performans
Sistemin bu bölümünde Repository Pattern ve Dapper ORM kullanılarak veri tabanı işlemleri en optimize hale getirilmiştir:
Server-Side Sayfalama ve Filtreleme: Milyonlarca kayıt arasından sadece gerekli olan 12 kaydı çekmek için SQL OFFSET-FETCH yapısı kullanılmıştır. Bu, uygulamanın bellek (RAM) kullanımını minimize eder.
Gelişmiş Dinamik Sorgulama: Kullanıcının seçtiği "Bölge", "Reklam Türü" veya "Arama Terimi"ne göre SQL sorguları WHERE 1=1 yapısı üzerinde dinamik olarak inşa edilir.
Asenkron Operasyonlar: Task<List<ResultCampaignDTO>> yapısı ve async/await kullanımı sayesinde, ağır veritabanı yükleri altında bile kullanıcı arayüzü (UI) kilitlenmez.

### 🛠️ Operasyonel Özellikler
- **Akıllı Arama:** CampaignName veya ClientName kolonları üzerinden LIKE sorguları ile anlık arama desteği.
- **Tam Kontrol (CRUD):** Yeni kampanya ekleme, mevcut bütçeleri revize etme ve performanssız kampanyaları silme yetenekleri.
- **Dropdown Entegrasyonu:** Bölgeler ve kampanya türleri veri tabanından dinamik olarak beslenerek kullanıcıya hatasız veri girişi imkanı sunulur.

### 📸 Modül Görselleri
<img width="1386" height="1316" alt="localhost_7019_Campaign_CampaignList" src="https://github.com/user-attachments/assets/93120dae-0312-475d-9964-ba286405102d" />
<img width="1401" height="911" alt="localhost_7019_Campaign_CampaignList_search= region=UK type=Arama+A%C4%9F%C4%B1" src="https://github.com/user-attachments/assets/73846e54-750e-4446-887a-591b205a9242" />
<img width="1401" height="911" alt="localhost_7019_Campaign_CreateCampaign" src="https://github.com/user-attachments/assets/fe33acdd-b512-4c66-a0d1-226a05e8532e" />
<img width="1401" height="911" alt="localhost_7019_Campaign_UpdateCampaign_50" src="https://github.com/user-attachments/assets/d70d6478-baa8-4e10-a1d3-75a67675f463" />


## 📂 Kategori Yönetimi (Category Management)
Kategori modülü, reklam ağındaki yayıncıların (Publishers) hangi içerik türlerinde hizmet verdiğini sınıflandıran ve sistem genelindeki içeriği organize eden temel yapı taşıdır.

### 🏗️ Teknik Uygulama ve Performans
Kategori işlemlerinde Dapper ORM'in hızı, esnek bir filtreleme mimarisiyle birleştirilmiştir:
Dinamik Sorgu Yönetimi: GetCategoriesPagedAsync metodu, kullanıcıdan gelen "Statü" (Aktif/Pasif) ve "Arama Terimi" parametrelerine göre SQL sorgusunu anlık olarak inşa eder.
Verimli Sayfalama (Server-Side Paging): Tüm kategorileri belleğe yüklemek yerine, OFFSET ve FETCH NEXT komutları kullanılarak sadece ilgili sayfadaki 12 kayıt veritabanından çekilir.
İstatistiksel Veri Yönetimi: CategoryList aksiyonu, tek bir sayfada hem filtreli sonuçları hem de veritabanındaki toplam/aktif kategori sayılarını asenkron olarak hesaplayarak kullanıcıya sunar.

### 🛠️ Operasyonel Özellikler
- **Durum Bazlı Filtreleme:** Kategorileri "Aktif" veya "Pasif" durumlarına göre süzerek sistemdeki içerik ağacını kolayca yönetme imkanı.
- **Hızlı Arama:** Kategori adları üzerinden LIKE operatörü ile gerçek zamanlı arama desteği.
- **Esnek CRUD Yapısı:** Yeni kategori ekleme (Create), mevcut kategori adını veya durumunu güncelleme (Update) ve ihtiyaç duyulmayan kategorileri kaldırma (Delete) yetenekleri.

### 📸 Modül Görselleri

<img width="1401" height="1385" alt="localhost_7019_Category_CategoryList (1)" src="https://github.com/user-attachments/assets/04accde2-42d7-41f7-8928-b71419febb12" />
<img width="1401" height="911" alt="localhost_7019_Category_CategoryList_status=false" src="https://github.com/user-attachments/assets/4ec4e6fc-be46-4cd3-ba52-14ac076ef59c" />
<img width="1401" height="911" alt="localhost_7019_Category_CreateCategory" src="https://github.com/user-attachments/assets/c517d716-8cfa-4511-af78-5ec7035f69e7" />
<img width="1401" height="911" alt="localhost_7019_Category_UpdateCategory_43" src="https://github.com/user-attachments/assets/22eb1814-46f0-4330-a32d-1c5f5527fa03" />


## 📂 Yayıncı Yönetimi (Publisher Management)
Yayıncı modülü, reklamların sergilendiği web platformlarının ve dijital mecraların sisteme kaydedildiği, kategorize edildiği ve aktiflik durumlarının kontrol edildiği yönetim katmanıdır.

### 🏗️ Teknik Uygulama ve Performans
- **Yayıncı verilerinin yönetimi**, ilişkisel veritabanı yapısı ve Dapper'ın sağladığı sorgu optimizasyonları ile kurgulanmıştır:
- **İlişkisel Veri Eşleştirme (Join):** Yayıncı listelenirken INNER JOIN kullanılarak her yayıncının ait olduğu kategori adı (CategoryName) veritabanı seviyesinde tek bir sorgu ile getirilir.
- **Dinamik Filtreleme:** Kategori bazlı süzme ve yayıncı durumuna (Aktif/Pasif) göre listeleme işlemleri, @catId IS NULL kontrolü içeren optimize edilmiş SQL sorguları ile yönetilir.
- **Sunucu Taraflı Sayfalama:** Binlerce yayıncı sitesi arasında hızlı navigasyon sağlamak amacıyla OFFSET-FETCH mimarisi kullanılarak sadece talep edilen sayfadaki veriler yüklenir.

### 🛠️ Operasyonel Özellikler
- **Kategori Bazlı Sınıflandırma:** Yayıncıları içerik türlerine göre filtreleyerek, reklam verenlerin doğru mecralara ulaşmasını sağlama.
- **Durum Kontrolü: Yayıncı ağını** anlık olarak aktif veya pasif hale getirerek reklam yayınını global ölçekte durdurma veya başlatma yetkisi.
- **Esnek Yönetim:** Yeni yayıncı URL'si tanımlama, kategori değişikliği yapma ve yayıncı silme süreçlerinin uçtan uca yönetimi.

### 📸 Modül Görselleri
<img width="1386" height="1247" alt="localhost_7019_Publisher_PublisherList" src="https://github.com/user-attachments/assets/4e8d6585-48b1-4704-a35a-6e4a747362f8" />
<img width="1401" height="911" alt="localhost_7019_Publisher_PublisherList_categoryId=35 status=" src="https://github.com/user-attachments/assets/ac158e0f-3434-44ca-a9d5-8e9408d3f714" />
<img width="1401" height="911" alt="localhost_7019_Publisher_CreatePublisher" src="https://github.com/user-attachments/assets/5cc55f9e-60a0-4006-b3a6-3add6a4b9537" />
<img width="1401" height="911" alt="localhost_7019_Publisher_UpdatePublisher_70" src="https://github.com/user-attachments/assets/1c8b069f-a7c6-41fa-944b-e3ef245e9365" />


