using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Person;

public class PersonModel
{
    [Key]
    public Guid Id { get; set; }

    [Required] public UserInfo UserInfo { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime LastModified { get; set; }

}