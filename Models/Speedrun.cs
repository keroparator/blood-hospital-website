using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace bloody_hospital.Models
{
    [Table("speedrun")]
    public class Speedrun : BaseModel
    {
        [PrimaryKey("id")]
        public long Id { get; set; }

        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Column("score")]
        public float Score { get; set; }

        [Column("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }
    }
}
