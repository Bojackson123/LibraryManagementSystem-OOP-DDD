using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Publication;

namespace LMS.Tests.DomainTests.VideoTests.Streaming;

public class VideoStreamingTests
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
        var runTime = 102;


        // Act
        var video = new VideoStreaming(title, description, director, topic, isbn, runTime);


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
        var video = new VideoStreaming();

        // Assert
        Assert.Equal(title, video.Title);
        Assert.Equal(description, video.Description);
        Assert.Equal(director, video.Director);
        Assert.Equal(topic, video.Topic);
        Assert.Equal(isbn, video.Isbn);
        Assert.Equal(runTime, video.RunTime);
    }
}
