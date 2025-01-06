using System.ComponentModel.DataAnnotations;

namespace OnlineKurs.Shared.Models;

public class Enrollments
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int CourseId { get; set; }

    [Required]
    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

    public Users? User { get; set; }
    public Courses? Course { get; set; }
}
