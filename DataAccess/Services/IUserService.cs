using Domain.Models.Person;

namespace DataAccess.Services;

public interface IUserService
{
    Task<IEnumerable<PersonModel>> GetUsersAsync();
    Task<PersonModel?> GetUserAsync(Guid id);
    Task<PersonModel> InsertUserAsync(PersonModel person);
    Task UpdateUserAsync(PersonModel person);
    Task DeleteUserAsync(Guid id);
}