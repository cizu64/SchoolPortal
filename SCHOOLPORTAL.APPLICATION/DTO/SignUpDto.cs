namespace Application;
using System.ComponentModel.DataAnnotations;
public record SignUpDto
{
    [Required]
    public string Firstname { get; set; }
    [Required]
    public string Lastname { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MaxLength(8)]
    public string Password { get; set; }
    [Required]
    public string Gender { get; set; }
    [Required]
    public int Age { get; set; }

    [Required]

    public string City { get; set;}
    [Required]

    public string Street { get; set;}

    [Required]

    public string Country { get; set; }
    [Required]

    public string State { get; set;}
}
