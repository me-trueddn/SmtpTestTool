using System;
using System.Net;
using System.Net.Mail;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== SMTP Test Aracı ===");

        // Kullanıcıdan girişleri al
        Console.Write("SMTP Sunucu: ");
        string smtpServer = Console.ReadLine();

        Console.Write("Port (Örn: 587): ");
        int port = int.Parse(Console.ReadLine());

        Console.Write("SSL Kullanılsın mı? (true/false): ");
        bool enableSSL = bool.Parse(Console.ReadLine());

        Console.Write("E-posta Kullanıcı Adı: ");
        string username = Console.ReadLine();

        Console.Write("Şifre: ");
        string password = ReadPassword(); // Şifreyi gizli almak için özel fonksiyon

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
                IsBodyHtml = false // Düz metin olarak gönderilecek
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

    // Şifre girişini gizlemek için
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
                Console.Write("*"); // Şifreyi * ile gizle
            }
        } while (key.Key != ConsoleKey.Enter);
        Console.WriteLine();
        return password;
    }
}
