using System.ComponentModel.DataAnnotations;

namespace OnlineKurs.Models;

public class Reviews
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CourseId { get; set; }

    [Required]
    public int UserId { get; set; }

    // Rating 1-5 arası değer alacak
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; } = 1;

    [MaxLength(1000)] // Maksimum yorum uzunluğu
    public string Comment { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Courses? Course { get; set; }
    public Users? User { get; set; }
}
