namespace Persistence.DTO;

public class RegisterDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password and confirm passwords do not match")]
    public string ComparePassword { get; set; }
}
