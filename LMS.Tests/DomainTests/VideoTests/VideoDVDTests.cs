using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Publication;

namespace LMS.Tests.DomainTests.VideoTests;

public class VideoDVDTests
{
    [Fact]
    public void Constructor_ShouldInitializeAllProperties()
    {
        // Arrange
        var title = "Test Title";
        var description = "This is a test... 1 . . . 2 . . . 3";
        var director = "Tester McTesting";
        var topic = "Unit Testing";
        var isbn = "123456890";
        var runTime = 103;
        var count = 1;


        // Act
        var video = new VideoDVD(title, description, director, topic, isbn, runTime, count);


        // Assert
        Assert.Equal(title, video.Title);
        Assert.Equal(description, video.Description);
        Assert.Equal(director, video.Director);
        Assert.Equal(topic, video.Topic);
        Assert.Equal(isbn, video.Isbn);
        Assert.Equal(runTime, video.RunTime);

    }

    [Fact]
    public void Constructor_ShouldDefaultAllProperties()
    {
        // Arrange
        string title = "Unknown";
        string description = "Description not added.";
        string director = "Unknown";
        string topic = "Unknown";
        string isbn = "Unknown";
        int runTime = 0;

        // Act
        var video = new VideoDVD();

        // Assert
        Assert.Equal(title, video.Title);
        Assert.Equal(description, video.Description);
        Assert.Equal(director, video.Director);
        Assert.Equal(topic, video.Topic);
        Assert.Equal(isbn, video.Isbn);
        Assert.Equal(runTime, video.RunTime);
    }

    // Method tests.
    [Fact]
    public void TryBarrow_WithCopiesAvaliable()
    {
        //Arrange
        var title = "Test Title";
        var description = "This is a test... 1 . . . 2 . . . 3";
        var director = "Tester McTesting";
        var topic = "Unit Testing";
        var isbn = "123456890";
        var runTime = 102;
        var count = 3;
        var video = new VideoDVD(title, description, director, topic, isbn, runTime, count);

        // Act
        bool resTrue = video.TryBorrow();

        // Assert
        Assert.True(resTrue);
        Assert.Equal(2, video.CurrCount);

    }

    [Fact]
    public void TryBarrow_WithoutCopiesAvaliable()
    {
        //Arrange
        var title = "Test Title";
        var description = "This is a test... 1 . . . 2 . . . 3";
        var director = "Tester McTesting";
        var topic = "Unit Testing";
        var isbn = "123456890";
        var runTime = 102;
        var count = 1;
        var video = new VideoDVD(title, description, director, topic, isbn, runTime, count);

        //Act
        bool resTrue = video.TryBorrow();
        bool resFalse = video.TryBorrow();

        //Assert
        Assert.True(resTrue);
        Assert.False(resFalse);
        Assert.Equal(0, video.CurrCount);
    }

    [Fact]
    public void TryReturn_UnderMaxCount()
    {
        //Arrange
        var title = "Test Title";
        var description = "This is a test... 1 . . . 2 . . . 3";
        var director = "Tester McTesting";
        var topic = "Unit Testing";
        var isbn = "123456890";
        var runTime = 102;
        var count = 1;
        var video = new VideoDVD(title, description, director, topic, isbn, runTime, count);

        // Act
        bool resBarrow = video.TryBorrow();
        bool resReturn = video.TryReturn();

        // Assert
        Assert.True(resBarrow);
        Assert.True(resReturn);
        Assert.Equal(1, video.CurrCount);
    }

    [Fact]
    public void TryReturn_AtMaxCount()
    {
        //Arrange
        var title = "Test Title";
        var description = "This is a test... 1 . . . 2 . . . 3";
        var director = "Tester McTesting";
        var topic = "Unit Testing";
        var isbn = "123456890";
        var runTime = 102;
        var count = 1;
        var video = new VideoDVD(title, description, director, topic, isbn, runTime, count);

        // Act
        bool resReturn = video.TryReturn();

        // Assert
        Assert.False(resReturn);
    }


}

