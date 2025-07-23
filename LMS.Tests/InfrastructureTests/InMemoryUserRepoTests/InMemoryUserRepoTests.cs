using LMS.Domain;
using LMS.Infrastructure.Repositories;

namespace LMS.Tests.InfrastructureTests.InMemoryUserRepoTests;

public class InMemoryUserRepoTests
{
    [Fact]
    public void TryAddUser_ShouldReturnTrueIfSuccessful()
    {
        // Arrange
        User b = new User("Rashid", "Al-Marri", "1234-5678");

        InMemoryUserRepo repo = new();
        int count = repo.GetUserCount();
        // Act
        bool res = repo.TryAddUser(b);

        // Assert
        Assert.True(res);
        Assert.Equal(count + 1, repo.GetUserCount());
        
    }

    [Fact]
    public void TryRemoveUser_ShouldReturnTrueIfSuccessful()
    {
        // Arrange 
        User b = new User("Rashid", "Al-Marri", "1234-5678");
        int bID = b.ID;
        InMemoryUserRepo repo = new();
        bool resAdd = repo.TryAddUser(b);
        int count = repo.GetUserCount();

        // Act
        bool res1 = repo.TryRemoveUser(bID);
        bool res2 = repo.TryRemoveUser(1234);

        // Assert
        Assert.Equal(count - 1, repo.GetUserCount());
        Assert.True(resAdd);
        Assert.True(res1);
        Assert.False(res2);
    }

    [Fact]
    public void TryGetByID_ShouldReturnUserOrNull()
    {
        // Arrange
        User a = new User("Rashid", "Al-Marri", "1234-5678");
        int aID = a.ID;
        User b = new User("Rashid", "Al-Marri", "1234-5678");
        int bID = b.ID;
        User c = new User("Rashid", "Al-Marri", "1234-5678");
        int cID = c.ID;
        int dID = 1234;
        User[] testArr = new User[] {a, b, c};
        InMemoryUserRepo repo = new();

        foreach (User user in testArr)
        {
            repo.TryAddUser(user);
        }

        // Act
        bool resA = repo.TryGetByID(aID, out User? outputA);
        bool resB = repo.TryGetByID(bID, out User? outputB);
        bool resC = repo.TryGetByID(cID, out User? outputC);
        bool resD = repo.TryGetByID(dID, out User? outputD);

        // Assert
        Assert.Equal(a, outputA);
        Assert.True(resA);
        
        Assert.Equal(b, outputB);
        Assert.True(resA);
        
        Assert.Equal(c, outputC);
        Assert.True(resA);

        Assert.Null(outputD);
        Assert.False(resD);
        
    }

    [Fact]
    public void GetByFirstName_ShouldReturnListOfUsersOrEmptyList()
    {
        // Act
        User a = new("Rashid", "Al-Marri", "1234-5678");
        User b = new("JDdjRashidjkdls", "Al-Marri", "1234-5678");
        User c = new("Rashidkdjsl", "Al-Marri", "1234-5678");
        User d = new("josh", "Al-Marri", "1234-5678");
        User e = new("jsdklJosh", "Al-Marri", "1234-5678");
        User f = new("JOSHdhskl", "Al-Marri", "1234-5678");
        User[] testArr = new User[] {a, b, c, d, e, f};

        List<User> groupRashid = [a, b, c];
        List<User> groupJosh = [d, e, f];
        List<User> groupS = [a, b, c, d, e, f];
        List<User> groupEmpty = [];

        InMemoryUserRepo repo = new();

        foreach (User user in testArr)
        {
            repo.TryAddUser(user);
        }

        // Act
        List<User> testGroupRashid = repo.GetByFirstName("RASHID");
        List<User> testGroupJosh = repo.GetByFirstName("JoSh");
        List<User> testGroupS = repo.GetByFirstName("s");
        List<User> testGroupEmpty = repo.GetByFirstName("Will Not Find In List");

        // Assert
        Assert.Equal(groupRashid, testGroupRashid);
        Assert.Equal(groupJosh, testGroupJosh);
        Assert.Equal(groupS, testGroupS);
        Assert.Equal(groupEmpty, testGroupEmpty);
        
    }

    [Fact]
    public void GetByLastName_ShouldReturnListOfUsersOrEmptyList()
    {
        // Act
        User a = new("Rashid", "Rashid", "1234-5678");
        User b = new("JDdjRashidjkdls", "JDdjRashidjkdls", "1234-5678");
        User c = new("Rashidkdjsl", "Rashidkdjsl", "1234-5678");
        User d = new("josh", "josh", "1234-5678");
        User e = new("jsdklJosh", "jsdklJosh", "1234-5678");
        User f = new("JOSHdhskl", "JOSHdhskl", "1234-5678");
        User[] testArr = new User[] {a, b, c, d, e, f};

        List<User> groupRashid = [a, b, c];
        List<User> groupJosh = [d, e, f];
        List<User> groupS = [a, b, c, d, e, f];
        List<User> groupEmpty = [];

        InMemoryUserRepo repo = new();

        foreach (User user in testArr)
        {
            repo.TryAddUser(user);
        }

        // Act
        List<User> testGroupRashid = repo.GetByLastName("RASHID");
        List<User> testGroupJosh = repo.GetByLastName("JoSh");
        List<User> testGroupS = repo.GetByLastName("s");
        List<User> testGroupEmpty = repo.GetByLastName("Will Not Find In List");

        // Assert
        Assert.Equal(groupRashid, testGroupRashid);
        Assert.Equal(groupJosh, testGroupJosh);
        Assert.Equal(groupS, testGroupS);
        Assert.Equal(groupEmpty, testGroupEmpty);

    }
}