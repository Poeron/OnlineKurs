using System.ComponentModel.DataAnnotations;

namespace OnlineKurs.Models;

public class Users
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)] // Kullanıcı adının maksimum uzunluğu
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress] // Geçerli bir e-posta adresi formatı kontrolü
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)] // Minimum şifre uzunluğu kontrolü
    public string Password { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Role { get; set; } = "user";
}
