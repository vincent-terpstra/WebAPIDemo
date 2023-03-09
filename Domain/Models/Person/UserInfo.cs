using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Person;

public class UserInfo
{
    [Required]
    public Name Name;
    
    [Required]
    public List<String> EmailAddresses { get; set; }

    [Required] 
    public List<Address> Address { get; set; } = new();
}