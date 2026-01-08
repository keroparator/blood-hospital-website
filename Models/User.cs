using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace bloody_hospital.Models
{
    [Table("players")]
    public class User : BaseModel
    {
        [PrimaryKey("id")]
        public long Id { get; set; }

        [Column("mail")]
        public string Mail { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;
    }
}
