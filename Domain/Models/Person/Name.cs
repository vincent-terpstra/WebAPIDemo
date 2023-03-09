using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Person;

public class Name
{
    [Required]
    [MaxLength(15, ErrorMessage = "First Name is too long")]
    [MinLength(5, ErrorMessage = "First Name is too short")]
    public string Firstname { get; set; }

    [Required]
    [MaxLength(15, ErrorMessage = "Last Name is too long")]
    [MinLength(5, ErrorMessage = "Last Name is too short")]
    public string Lastname { get; set; }
}