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

    #region Add and Remove User Methods.
    public string AddUser(string firstName, string lastName, string phoneNumber)
    {
        if (!userService.TryAddUser(firstName, lastName, phoneNumber))
        {
            throw new Exception("Unexpected error: User was not added. Please try again later.");
        }
        return $"User: '{firstName} {lastName}' was successfully added!";
    }

    public string RemoveUser(int userID)
    {
        User userObject = ValidateAndGetUser(userID);
        if (!userService.TryRemoveUser(userObject))
        {
            throw new Exception("Unexpected error: User was not removed. Please try again later.");
        }

        return $"User: '{userObject.FirstName} {userObject.LastName}' was successfully removed!";
    }
    #endregion

    
    #region Add and Remove Publication Methods.
    

    #endregion 
    
    #region Search and Aggregate Methods.

    #endregion
    
    #region Borrowing and Return Methods.
    // --- Borrowing Method ---
    public string BorrowPub(int pubID, int userID)
    {

        // Validate user and publication object.
        Publication pubObject = ValidateAndGetPublication(pubID);
        User userObject = ValidateAndGetUser(userID);

        // Execute borrowing
        // Try borrowing book from publication service via publications internal state.
        if (!pubService.TryBorrowPub((IBorrowable)pubObject))
        {
            throw new InvalidOperationException($"Publication: '{pubObject.Title}' is not currently in stock!");
        }

        // Try adding book to user object via users internal state.
        if (!userService.TryBorrowPublication(userObject, pubObject))
        {
            // if it fails, "return" the book back to the pubObject
            pubService.TryReturnPub((IBorrowable)pubObject);
            throw new InvalidOperationException($"User: '{userObject.FirstName} {userObject.LastName}' already checked out this book!");
        }

        return $"User: '{userObject.FirstName} {userObject.LastName}' successfully checked out Publication: '{pubObject.Title}'!";
    }

    // --- Returning Method ---
    public string ReturnPub(int pubID, int userID)
    {
        User userObject = ValidateAndGetUser(userID);
        Publication pubObject = ValidateAndGetPublication(pubID);

        if (!userService.TryReturnPublication(userObject, pubObject))
        {
            throw new InvalidOperationException($"User: '{userObject.FirstName} {userObject.LastName}' has not checked out Publication: '{pubObject}'!");
        }

        if (!pubService.TryReturnPub((IBorrowable)pubObject))
        {
            throw new InvalidOperationException($"Publication: {pubObject.Title} is already at max capacity. Removing 'ghost' copy from users inventory.");
        }

        return $"User: '{userObject.FirstName} {userObject.LastName}' successfully returned Publication: '{pubObject.Title}'!";
        
    }

    #endregion
    


    #region ---Helper Methods---
    
    private User ValidateAndGetUser(int userID)
    {
        User? userObject = userService.GetByID(userID);

        if (userObject == null)
        {
            throw new ArgumentException($"UserID: '{userID}' was not found!");
        }

        return userObject;
    }

    private Publication ValidateAndGetPublication(int pubID)
    {
        Publication? pubObject = pubService.GetByID(pubID);

        if (pubObject is null)
        {
            throw new ArgumentException($"Publication ID: '{pubID}' was not found!");
        }

        if (pubObject is not IBorrowable)
        {
            throw new ArgumentException($"Publication with ID: '{pubID}' cannot be borrowed!");
        }

        return pubObject;
    }
    
    #endregion
}