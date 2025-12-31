using Microsoft.AspNetCore.Mvc;
using blood_hospital_website.Data;
using blood_hospital_website.Models;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace blood_hospital_website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;


        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        } 



        [HttpPost]
        public IActionResult AboneOl(string email)
        {

            if (string.IsNullOrEmpty(email))
            {
                TempData["Hata"] = "Lütfen geçerli bir e-posta girin!";
                return RedirectToAction("Index", "Home", new { fragment = "extra" });
            }


            bool varMi = _context.Aboneler.Any(x => x.Email == email);
            if (varMi)
            {
                TempData["Hata"] = "Bu mail adresi zaten kayıtlı.";
                return RedirectToAction("Index", "Home", new { fragment = "extra" });
            }

   
            Abone yeniAbone = new Abone();
            yeniAbone.Email = email;
            yeniAbone.KayitTarihi = DateTime.Now;

            _context.Aboneler.Add(yeniAbone);
            _context.SaveChanges();


            TempData["Mesaj"] = "Abone olundu";
            
            return RedirectToAction("Index", "Home", new { fragment = "extra" });
        }


        // İLETİŞİM FORMU METODU
        [HttpPost]
        public IActionResult IletisimGonder(string ad, string email, string mesaj)
        {
            try
            {
                // --- BURALARI KENDİNE GÖRE DOLDUR ---
                string gonderenMail = "keremkahramanbruh@gmail.com";
                string gonderenSifre = "tgcz giok vgri moqr";
                string aliciMail = "keroparator@proton.me";

                // --- MAİL İÇERİĞİ HAZIRLAMA ---
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(gonderenMail, "Blood Hospital İletişim");
                mail.To.Add(aliciMail);
                
                mail.Subject = "SİTEDEN YENİ MESAJ VAR: " + ad;
                mail.Body = $"Gönderen İsim: {ad}\n" +
                            $"Gönderen Mail: {email}\n\n" +
                            $"MESAJ:\n{mesaj}";
                
                mail.IsBodyHtml = false; // Düz yazı olsun

                // --- SMTP AYARLARI (GMAIL İÇİN) ---
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(gonderenMail, gonderenSifre);
                smtp.EnableSsl = true;

                // --- GÖNDER ---
                smtp.Send(mail);

                TempData["Mesaj"] = "Mesajın başarıyla iletildi! En kısa sürede döneceğiz.";
            }
            catch (Exception ex)
            {
                // Hata olursa kullanıcıya gösterelim
                TempData["Hata"] = "Mail gönderilemedi! Hata: " + ex.Message;
            }

            // Sayfayı yenile ve iletişim bölümüne kay
            return RedirectToAction("Index", "Home", new { fragment = "iletisim" });
        }



        public IActionResult Index()
        {
            string klasorYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "screenshots");
            if (Directory.Exists(klasorYolu))
            {
                 ViewBag.Resimler = Directory.GetFiles(klasorYolu).Select(Path.GetFileName).ToList();
            }

            if (_context.Skorlar != null)
            {
                var skorListesi = _context.Skorlar.OrderBy(x => x.Sure).ToList();
                ViewBag.Skorlar = skorListesi;
            }

            return View();
        }
    }
}