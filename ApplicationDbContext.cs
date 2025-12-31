using Microsoft.EntityFrameworkCore;
using blood_hospital_website.Models; // Örn: blood_hospital_website.Models

namespace blood_hospital_website.Data // Örn: blood_hospital_website.Data
{
    // Hata burada yapılmıştı, parantezler ve class yapısı bozuktu. Doğrusu bu:
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Skor> Skorlar { get; set; }
        public DbSet<Abone> Aboneler { get; set; }
    }
}