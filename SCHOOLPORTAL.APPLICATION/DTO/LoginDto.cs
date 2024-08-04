namespace Application;
using System.ComponentModel.DataAnnotations;

public record LoginDto
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password{get;set;}
}