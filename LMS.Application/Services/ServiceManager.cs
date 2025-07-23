using LMS.Infrastructure.Repositories;
using LMS.Domain.Publication;
using LMS.Domain;

namespace LMS.Application.Services;

public class ServiceManager
{
    private static InMemoryUserRepo _userRepo = new();
    private UserService userService = new(_userRepo);

    private static InMemoryPubRepo _pubRepo = new();
    private PublicationService pubService = new(_pubRepo);

    public string TryBorrowPub(int pubID, int userID)
    {

        // Publication object.
        Publication? pubObject = pubService.GetByID(pubID);

        // Publication checks.
        if (pubObject is null)
        {
            throw new ArgumentException($"ID: '{pubID}' is was not found!");
        }

        if (pubObject is not IBorrowable)
        {
            throw new ArgumentException($"Publication with ID: '{pubID}' cannot be borrowed!");
        }

        // User object
        

        if (pubService.TryBorrowPub())

        
        
    }
}