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
    
    public string AddBook(string title, string description, string author, string topic, string isbn, int count)
    {
        Book book = new Book(title, description, author, topic, isbn, count);
        if (!pubService.TryAddPublication(book))
        {
            throw new Exception("Unexpected error: Book was not added. Please try again later.");
        }
        return $"Book: '{title}' by {author} was successfully added!";
    }

    public string AddAudioBookCD(string title, string description, string author, string topic, string isbn, string narrator, int runTime, int count)
    {
        AudioBookCD audioBook = new AudioBookCD(title, description, author, topic, isbn, narrator, runTime, count);
        if (!pubService.TryAddPublication(audioBook))
        {
            throw new Exception("Unexpected error: AudioBook CD was not added. Please try again later.");
        }
        return $"AudioBook CD: '{title}' by {author} was successfully added!";
    }

    public string AddEncyclopedia(string[] authors, string title, string description, string topic, string isbn, int edition)
    {
        Encyclopedia encyclopedia = new Encyclopedia(authors, title, description, topic, isbn, edition);
        if (!pubService.TryAddPublication(encyclopedia))
        {
            throw new Exception("Unexpected error: Encyclopedia was not added. Please try again later.");
        }
        return $"Encyclopedia: '{title}' was successfully added!";
    }

    public string AddPeriodical(string title, string description, string isbn, string topic, string publisher, int issueNumber, int count)
    {
        Periodical periodical = new Periodical(title, description, isbn, topic, publisher, issueNumber, count);
        if (!pubService.TryAddPublication(periodical))
        {
            throw new Exception("Unexpected error: Periodical was not added. Please try again later.");
        }
        return $"Periodical: '{title}' was successfully added!";
    }

    public string AddVideoStreaming(string title, string description, string director, string topic, string isbn, int runTime)
    {
        VideoStreaming video = new VideoStreaming(title, description, director, topic, isbn, runTime);
        if (!pubService.TryAddPublication(video))
        {
            throw new Exception("Unexpected error: Video Streaming was not added. Please try again later.");
        }
        return $"Video Streaming: '{title}' directed by {director} was successfully added!";
    }

    public string AddVideoDVD(string title, string description, string director, string topic, string isbn, int runTime, int count)
    {
        VideoDVD videoDVD = new VideoDVD(title, description, director, topic, isbn, runTime, count);
        if (!pubService.TryAddPublication(videoDVD))
        {
            throw new Exception("Unexpected error: Video DVD was not added. Please try again later.");
        }
        return $"Video DVD: '{title}' directed by {director} was successfully added!";
    }

    public string RemovePublication(int pubID)
    {
        Publication pubObject = ValidateAndGetPublication(pubID);
        if (!pubService.TryRemovePublication(pubID))
        {
            throw new Exception("Unexpected error: Publication was not removed. Please try again later.");
        }
        return $"Publication: '{pubObject.Title}' was successfully removed!";
    }

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