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