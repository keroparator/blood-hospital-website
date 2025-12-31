using Microsoft.AspNetCore.Mvc;
using blood_hospital_website.Data; // Context'in olduğu yer
using blood_hospital_website.Models; // Modellerin olduğu yer
using System.Linq;

namespace blood_hospital_website.Controllers
{
    public class HomeController : Controller
    {
        // 1. Veritabanı bağlantısını tanımlıyoruz
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // 2. Resim işleri (Daha önce yapmıştık, aynen kalsın)
            string klasorYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "screenshots");
            if (Directory.Exists(klasorYolu))
            {
                 ViewBag.Resimler = Directory.GetFiles(klasorYolu).Select(Path.GetFileName).ToList();
            }

            // 3. Veritabanından Skorları Çek (Süresi en az olan en başa)
            // ToList() demezsen HTML tarafında hata alırsın!
            var skorListesi = _context.Skorlar.OrderBy(x => x.Sure).ToList();
            
            // 4. Listeyi HTML'e postala
            ViewBag.Skorlar = skorListesi;

            return View();
        }
    }
}