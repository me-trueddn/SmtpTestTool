# Basit Bir SMTP Test Aracı Geliştirme

Günümüzde e-posta gönderimi, birçok yazılım sisteminde kritik bir rol oynamaktadır. SMTP (Simple Mail Transfer Protocol), e-posta gönderimi için kullanılan temel protokoldür. Bu yazıda, **.NET 8** kullanarak **C# Console** uygulaması şeklinde basit bir SMTP test aracı geliştireceğiz.

## 1. Proje Oluşturma

Öncelikle Visual Studio veya komut satırı kullanarak yeni bir **C# Console App** projesi oluşturuyoruz.

```sh
mkdir SmtpTestTool
cd SmtpTestTool
dotnet new console -n SmtpTestTool
cd SmtpTestTool
```

Bu adımlarla yeni bir .NET 8 tabanlı console projesi oluşturulmuş olacak.

## 2. SMTP E-posta Gönderim Kodları

`` dosyamızın içeriğini aşağıdaki gibi düzenleyelim:

```csharp
using System;
using System.Net;
using System.Net.Mail;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== SMTP Test Aracı ===");

        Console.Write("SMTP Sunucu: ");
        string smtpServer = Console.ReadLine();

        Console.Write("Port (Örn: 587): ");
        int port = int.Parse(Console.ReadLine());

        Console.Write("SSL Kullanılsın mı? (true/false): ");
        bool enableSSL = bool.Parse(Console.ReadLine());

        Console.Write("E-posta Kullanıcı Adı: ");
        string username = Console.ReadLine();

        Console.Write("Şifre: ");
        string password = ReadPassword();

        Console.Write("Alıcı E-posta: ");
        string toEmail = Console.ReadLine();

        Console.Write("E-posta Konusu: ");
        string subject = Console.ReadLine();

        Console.Write("E-posta İçeriği: ");
        string body = Console.ReadLine();

        try
        {
            SmtpClient client = new SmtpClient(smtpServer, port)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = enableSSL
            };

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(username),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            mail.To.Add(toEmail);

            Console.WriteLine("\n📤 E-posta gönderiliyor...");
            client.Send(mail);
            Console.WriteLine("✅ E-posta başarıyla gönderildi.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Hata: {ex.Message}");
        }
    }

    static string ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);
            if (key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
        } while (key.Key != ConsoleKey.Enter);
        Console.WriteLine();
        return password;
    }
}
```

Bu kod ile SMTP sunucu bilgilerini girerek e-posta gönderimi yapabilirsiniz.

## 3. Yayınlama (Publish) İşlemi

Eğer **Native AOT (Ahead-of-Time) Compilation** kullanıyorsanız, proje dosyanızın (`.csproj`) içeriğini aşağıdaki gibi güncelleyin:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <PublishAot>true</PublishAot>
    <InvariantGlobalization>true</InvariantGlobalization>
    <RuntimeIdentifier>win-x64</RuntimeIdentifier>
  </PropertyGroup>
</Project>
```

Ardından, **uygulamanızı derleyip yayınlamak** için aşağıdaki komutu çalıştırabilirsiniz:

```sh
dotnet publish -c Release -r win-x64 --self-contained true
```

Eğer **Linux veya macOS** için çalıştıracaksanız, `win-x64` yerine **linux-x64** veya **osx-x64** kullanabilirsiniz.

## 4. Sonuç

Bu yazıda, **.NET 8 ile bir SMTP test aracı** geliştirdik. Konsol üzerinden SMTP sunucu bilgileri girilerek e-posta gönderimi yapılabiliyor. Eğer özel bir e-posta sunucunuz varsa, uygun bilgileri girerek kolayca test edebilirsiniz. 🚀

