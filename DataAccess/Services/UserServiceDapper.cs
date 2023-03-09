using DataAccess.DbAccess;
using Domain.Models;
using Domain.Models.Person;

namespace DataAccess.Services;

public class UserServiceDapper : IUserService
{
    private readonly ISqlDataAccess _db;

    public UserServiceDapper(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<PersonModel>> GetUsersAsync()
        => _db.LoadDataAsync<PersonModel, dynamic>("user_getall", new{});

    public async Task<PersonModel?> GetUserAsync(Guid id)
        => (await _db.LoadDataAsync<PersonModel, dynamic>("user_get", new { id }))
        .FirstOrDefault();

    public async Task<PersonModel> InsertUserAsync(PersonModel person)
    {
        Name name = person.UserInfo.Name;
        if (string.IsNullOrWhiteSpace(name.Firstname))
            throw new ArgumentException("Person requires a first name", "FirstName");
        
        if (string.IsNullOrWhiteSpace(name.Lastname))
            throw new ArgumentException("Person requires a last name", "LastName");
        
        await _db.SaveDataAsync("user_create", new {firstname = name.Firstname, lastname = name.Lastname});
        //HACK this doesn't update the Id of the person inserted
        return person;
    }
        
    
    public Task UpdateUserAsync(PersonModel person)
        => _db.SaveDataAsync("user_update", person);

    public Task DeleteUserAsync(Guid id)
        => _db.SaveDataAsync("user_delete", new { id });
}