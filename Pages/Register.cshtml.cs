using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using bloody_hospital.Models;
using System.Net;
using System.Net.Mail;

namespace bloody_hospital.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly Supabase.Client _supabase;
        private readonly IConfiguration _configuration;

        public RegisterModel(Supabase.Client supabase, IConfiguration configuration)
        {
            _supabase = supabase;
            _configuration = configuration;
        }

        [BindProperty]
        public string Email { get; set; } = string.Empty;
        [BindProperty]
        public string Password { get; set; } = string.Empty;
        [BindProperty]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string? Message { get; set; }
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync(string email, string password, string confirmPassword)
        {
            await _supabase.InitializeAsync();
             if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ErrorMessage = "Tüm alanlar zorunludur.";
                return Page();
            }

            if (password != confirmPassword)
            {
                ErrorMessage = "Şifreler eşleşmiyor.";
                return Page();
            }

            try
            {
                
                var existing = await _supabase.From<User>().Select("id").Filter("mail", Supabase.Postgrest.Constants.Operator.Equals, email).Get();
                if (existing.Models.Any())
                {
                     ErrorMessage = "Bu e-posta adresi zaten kayıtlı.";
                     return Page();
                }

               
                var newUser = new User { Mail = email, Password = password };
                await _supabase.From<User>().Insert(newUser);

                
                await SendWelcomeEmail(email);

                Message = "Kayıt başarıyla oluşturuldu! Giriş yapabilirsiniz.";
                
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Kayıt sırasında bir hata oluştu: {ex.Message}";
            }

            return Page();
        }

        private async Task SendWelcomeEmail(string toEmail)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            var host = smtpSettings["Host"];
            var port = int.Parse(smtpSettings["Port"] ?? "587");
            var user = smtpSettings["User"];
            var pass = smtpSettings["Pass"];

            if(string.IsNullOrEmpty(host) || string.IsNullOrEmpty(user) || user == "your_email@gmail.com") return; // Skip if not configured

            using (var client = new SmtpClient(host, port))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(user, pass);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(user, "Bloody Hospital"),
                    Subject = "Bloody Hospital'a Hoşgeldiniz!",
                    Body = "<h1>Kayıt Başarılı!</h1>",
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
