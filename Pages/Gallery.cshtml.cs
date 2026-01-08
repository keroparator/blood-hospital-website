using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bloody_hospital.Pages
{
    public class GalleryModel : PageModel
    {
        private readonly IWebHostEnvironment _env;

        public GalleryModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public List<string> Images { get; set; } = new List<string>();

        public void OnGet()
        {
            var imagesPath = Path.Combine(_env.WebRootPath, "images");
            if (Directory.Exists(imagesPath))
            {
                Images = Directory.GetFiles(imagesPath, "*.*")
                    .Where(s => s.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || 
                                s.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) || 
                                s.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                    .Select(Path.GetFileName)
                    .Where(x => x != null)
                    .Cast<string>()
                    .ToList();
            }
        }
    }
}
