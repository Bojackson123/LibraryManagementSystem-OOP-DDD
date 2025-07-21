using LMS.Domain;

namespace LMS.Infrastructure.Interfaces;

public interface IUserRepository
{
    bool TryAddUser(User userObject);

    bool TryRemoveUser(int ID);

    bool TryGetByID(int ID, out User? result);

    List<User> GetByFirstName(string firstName);

    List<User> GetByLastName(string lastName);
}
