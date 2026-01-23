Blood Hospital - Game Website

Bu proje nesne tabanlı programlama dersi için geliştirdiğim projedir. .NET Razor Pages temeli kullanarak geliştirdiğim site oyuncuların skorlarını takip edebileceği, oyun içi görsellere ulaşabileceği ve iletişim kurabileceği dinamik bir yapı sunar.

🚀 Proje Hakkında & Özellikler

Bu web sitesi sadece statik bir tanıtım sayfası değil, oyun verileriyle etkileşime giren dinamik bir uygulamadır.

    Supabase Entegrasyonu: Veritabanı olarak Supabase altyapısı kullanılmıştır. Kullanıcı verileri ve skorlar PostgreSQL tabanlı bu veritabanında tutulur.

    Gerçek Zamanlı Veri : Oyun içerisindeki skorlar supabase client kütüphanesi aracılığıyla canlı olarak çekilir.

    Dinamik Galeri Sistemi: Galeri sayfası System.IO kütüphanesi kullanılarak sunucuda(wwwroot/images) bulunan görseller taranır. Bu sayede klasöre yeni resim atıldığında kod değiştirmeden galeri güncellenir.

    Google SMTP: Mail gönderimleri için Google SMTP altyapısı entegre edilmiştir.

    Authentication: Çerez tabanlı kimlik doğrulama sistemi ile giriş ve çıkış işlemleri sağlanır.

🛠️ Teknolojiler

    Framework: .NET 8.0 (ASP.NET Core Razor Pages)

    Dil: HTML5, CSS3, Bootstrap, JavaScript, C#

    Veritabanı: Supabase (PostgreSQL)

    Mail Servisi: Google SMTP

⚙️ Kurulum ve Çalıştırma

Projeyi çalıştırmak için aşağıdaki adımları izleyin:

    Repoyu Klonlayın:

    git clone https://github.com/keroparator/blood-hospital-website.git
    cd blood-hospital-website

    Yapılandırma Ayarlarını Girin: Projenin kök dizinindeki appsettings.json (veya appsettings.Development.json) dosyasını açın ve kendi Supabase ve Mail ayarlarınızı girin. Program.cs dosyasında belirtilen anahtarlar şunlardır:
    JSON

    {
      "SupabaseUrl": "",
      "SupabaseKey": "",
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      }
      // SMTP ayarları kod içerisinde IConfiguration üzerinden çekiliyorsa buraya eklenmelidir.
    }

    Bağımlılıkları Yükleyin ve Çalıştırın: Terminalde proje dizinine gelerek aşağıdaki komutları çalıştırın:
    Bash

    dotnet restore
    dotnet run

    Terminalinizde belirtilen Localhost adresinde gidin.
