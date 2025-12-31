using System;
using System.ComponentModel.DataAnnotations;

namespace blood_hospital_website.Models
{
    public class Abone
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        public DateTime KayitTarihi { get; set; }
    }
}