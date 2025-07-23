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

    public string BorrowPub(int pubID, int userID)
    {
        // Publication object.
        Publication? pubObject = pubService.GetByID(pubID);

        // Publication checks.
        if (pubObject is null)
        {
            throw new ArgumentException($"Publication ID: '{pubID}' was not found!");
        }
        
        if (pubObject is not IBorrowable)
        {
            throw new ArgumentException($"Publication with ID: '{pubID}' cannot be borrowed!");
        }

        // User object.
        User? userObject = userService.GetByID(userID);

        // User checks.
        if (userObject == null)
        {
            throw new ArgumentException($"UserID: '{userID}' was not found!");
        }

        // Execute borrowing

        // Try borrowing book from publication service via publications internal state.
        if (!pubService.TryBorrowPub((IBorrowable)pubObject))
        {
            throw new InvalidOperationException($"Publication: '{pubObject.Title}' is not currently in stock!");
        }

        // Try adding book to user object
        if (!userService.TryBorrowPublication(userObject, pubObject))
        {
            // if it fails, "return" the book back to the pubObject
            pubService.TryReturnPub((IBorrowable)pubObject);
            throw new InvalidOperationException($"User: '{userObject.FirstName} {userObject.LastName}' already checked out this book!");

        }
        // Return confirmation if passing the validation checks.
        return $"Publication: '{pubObject.Title}' has been successfully added to User: '{userObject.FirstName} {userObject.LastName}'!";
        
    }

    public 
}