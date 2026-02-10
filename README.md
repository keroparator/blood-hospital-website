# Blood Hospital - Oyun websites

## 🚀 Proje Hakkında

Bu repo nesne tabanlı programlama dersi ödevi olarak geliştirdiğim bir projedir. .NET kullanarak geliştirdiğim site oyuncuların skorlarını bir Supabase tablosundan takip eder ve ekrana bastırır, oyun içi görselleri dosyadan canlı olarak çeker, basit kayıt olma ve giriş yapma özelliklerine sahiptir, google smtp kullanarak mail gönderebilir.


## 🛠️ Teknolojiler

    Framework: ASP.NET Core Razor Pages

    Dil: HTML5, CSS3, JavaScript, C#

    Veritabanı: Supabase (PostgreSQL)

    Mail Servisi: Google SMTP

## ⚙️ Kurulum ve Çalıştırma

### 1 - Repoyu Klonlayın ve dosyaya gidin:
```bash 
git clone https://github.com/keroparator/blood-hospital-website.git
cd blood-hospital-website
```
### 2 - Proje dosyasında appsettings.json dosyasını açın ve kendi SupabaseUrl ve SupabaseKey'inizi girin
```bash
{
    "SupabaseUrl": "",
    "SupabaseKey": "",
    "Logging": {
    "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
    }
    }
}
```

### 3 - Terminalde proje dizinine gelerek aşağıdaki komutları çalıştırın ve terminalinizde belirtilen localhost adresine gidin:

```bash
dotnet restore
dotnet run
```
