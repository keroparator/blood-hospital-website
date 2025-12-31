using System;

namespace blood_hospital_website.Models
{
    public class Skor
    {
    public int Id { get; set; }
    public string? OyuncuAdi { get; set; } // Soru işareti ekle
    public string? Sure { get; set; }      // Soru işareti ekle
    public int OlumSayisi { get; set; }
    public DateTime Tarih { get; set; }
    }
}