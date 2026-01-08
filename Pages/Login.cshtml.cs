using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using bloody_hospital.Models;
using Supabase;

namespace bloody_hospital.Pages
{
    public class LoginModel : PageModel
    {
        private readonly Supabase.Client _supabase;

        public LoginModel(Supabase.Client supabase)
        {
            _supabase = supabase;
        }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync(string email, string password)
        {
            await _supabase.InitializeAsync();
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ErrorMessage = "E-Posta ve Şifre zorunludur.";
                return Page();
            }

            try
            {
                // Query users table
                var response = await _supabase.From<User>()
                    .Select("*")
                    .Match(new Dictionary<string, string> { { "mail", email }, { "password", password } })
                    .Single();

                if (response != null)
                {
                    // Create ClaimsPrincipal
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, response.Mail),
                        new Claim("UserId", response.Id.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToPage("/Index");
                }
            }
            catch (Exception)
            {
                // User not found or error
            }

            ErrorMessage = "Geçersiz E-Posta veya Şifre.";
            return Page();
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Login");
        }
    }
}
