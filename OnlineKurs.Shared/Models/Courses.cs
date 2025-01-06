using System.ComponentModel.DataAnnotations;

namespace OnlineKurs.Shared.Models;

public class Courses
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)] // Kurs başlığı maksimum uzunluk
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)] // Açıklamanın maksimum uzunluğu
    public string Description { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public int UserId { get; set; }

    // Navigasyon özelliği nullable olabilir
    public Users? User { get; set; }
    
    // enrollments ve reviews için navigasyon özelliği
    public ICollection<Enrollments> Enrollments { get; set; } = new List<Enrollments>();
    public ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();
}
