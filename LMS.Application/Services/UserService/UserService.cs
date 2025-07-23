using LMS.Infrastructure.Interfaces;
using LMS.Domain;
using LMS.Domain.Publication;

namespace LMS.Application.Services;

public class UserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public bool TryAddUser(string firstName, string lastName, string phoneNumber)
    {
        User newUser = new User(firstName, lastName, phoneNumber);
        return _repo.TryAddUser(newUser);
    }

    public bool TryRemoveUser(User userObject)
    {
        return _repo.TryRemoveUser(userObject.ID);
    }

    public bool TryBorrowPublication(User userObject, Publication pubObject)
    {
        int pubID = pubObject.ID;
        return userObject.TryAddPublicationID(pubID);
    }

    public bool TryReturnPublication(User userObject, Publication pubObject)
    {
        int pubID = pubObject.ID;
        return userObject.TryRemovePublicationID(pubID);
    }

    public User? GetByID(int ID)
    {
        _repo.TryGetByID(ID, out User? result);
        
        return result;
    }
}