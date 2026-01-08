using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using bloody_hospital.Models;
using Supabase;

namespace bloody_hospital.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Supabase.Client _supabase;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(Supabase.Client supabase, IWebHostEnvironment env, ILogger<IndexModel> logger)
        {
            _supabase = supabase;
            _env = env;
            _logger = logger;
        }

        public List<Speedrun> Speedrunners { get; set; } = new List<Speedrun>();
        public List<string> GalleryImages { get; set; } = new List<string>();
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            await _supabase.InitializeAsync();
            // Fetch Speedrunners
            try
            {
                var response = await _supabase.From<Speedrun>()
                    .Select("*")
                    .Order("score", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Limit(5)
                    .Get();
                
                Speedrunners = response.Models;
            }
            catch (Exception ex)
            {
                // Handle error (log it)
                Console.WriteLine($"Error fetching speedrunners: {ex.Message}");
                ErrorMessage = $"Veri çekme hatası: {ex.Message}";
                // Fallback or empty list
            }

            // Fetch Gallery Images
            var imagesPath = Path.Combine(_env.WebRootPath, "images");
            if (Directory.Exists(imagesPath))
            {
                var files = Directory.GetFiles(imagesPath, "*.*")
                    .Where(s => s.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || 
                                s.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) || 
                                s.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                    .Select(Path.GetFileName)
                    .Where(x => x != null)
                    .Cast<string>()
                    .ToList();
                
                GalleryImages = files;
            }
        }
    }
}
