
using LMS.Domain.Publication;

namespace LMS.Tests.DomainTests.AudioBookCDTests
{
    public class AudioBookCDTests
    {
        [Fact]
        public void Constructor_ShouldInitializeAllProperties()
        {
            // Arrange
            var title = "C# in Depth";
            var description = "Deep dive into C#.";
            var author = "Jon Skeet";
            var topic = "Programming";
            var isbn = "1234567890";
            var narrator = "John Smith";
            var runTime = 100;
            var count = 3;

            // Act
            var audioBook = new AudioBookCD(title, description, author, topic, isbn, narrator, runTime, count);

            // Assert
            Assert.Equal(title, audioBook.Title);
            Assert.Equal(description, audioBook.Description);
            Assert.Equal(author, audioBook.Author);
            Assert.Equal(topic, audioBook.Topic);
            Assert.Equal(isbn, audioBook.Isbn);
            Assert.Equal(narrator, audioBook.Narrator);
            Assert.Equal(runTime, audioBook.RunTime);
            Assert.Equal(count, audioBook.MaxCount);
            Assert.Equal(count, audioBook.CurrCount);
        }

        [Fact]
        public void Constructor_ShouldDefaultAllProperties()
        {
            // Arrange
            string title = "Unknown";
            string description = "No description added.";
            string author = "Unknown";
            string topic = "Unknown";
            string isbn = "Unknown";
            string narrator = "Unknown";
            int runTime = 0;
            int count = 1;

            // Act
            var audioBook = new AudioBookCD();

            // Assert
            Assert.Equal(title, audioBook.Title);
            Assert.Equal(description, audioBook.Description);
            Assert.Equal(author, audioBook.Author);
            Assert.Equal(topic, audioBook.Topic);
            Assert.Equal(isbn, audioBook.Isbn);
            Assert.Equal(narrator, audioBook.Narrator);
            Assert.Equal(runTime, audioBook.RunTime);
            Assert.Equal(count, audioBook.MaxCount);
            Assert.Equal(count, audioBook.CurrCount);
        }

        // Method tests.
        [Fact]
        public void TryBarrow_WithCopiesAvaliable()
        {
            // Arrange
            var title = "C# in Depth";
            var description = "Deep dive into C#.";
            var author = "Jon Skeet";
            var topic = "Programming";
            var isbn = "1234567890";
            var narrator = "John Smith";
            var runTime = 100;
            var count = 3;
            var audioBook = new AudioBookCD(title, description, author, topic, isbn, narrator, runTime, count);

            // Act
            bool resTrue = audioBook.TryBorrow();

            // Assert
            Assert.True(resTrue);
            Assert.Equal(2, audioBook.CurrCount);

        }

        [Fact]
        public void TryBarrow_WithoutCopiesAvaliable()
        {
            // Arrange
            var title = "C# in Depth";
            var description = "Deep dive into C#.";
            var author = "Jon Skeet";
            var topic = "Programming";
            var isbn = "1234567890";
            var narrator = "John Smith";
            var runTime = 100;
            var count = 1;
            var audioBook = new AudioBookCD(title, description, author, topic, isbn, narrator, runTime, count);

            //Act
            bool resTrue = audioBook.TryBorrow();
            bool resFalse = audioBook.TryBorrow();

            //Assert
            Assert.True(resTrue);
            Assert.False(resFalse);
            Assert.Equal(0, audioBook.CurrCount);
        }

        [Fact]
        public void TryReturn_UnderMaxCount()
        {
            // Arrange
            var title = "C# in Depth";
            var description = "Deep dive into C#.";
            var author = "Jon Skeet";
            var topic = "Programming";
            var isbn = "1234567890";
            var narrator = "John Smith";
            var runTime = 100;
            var count = 1;
            var audioBook = new AudioBookCD(title, description, author, topic, isbn, narrator, runTime, count);

            // Act
            bool resBarrow = audioBook.TryBorrow();
            bool resReturn = audioBook.TryReturn();

            // Assert
            Assert.True(resBarrow);
            Assert.True(resReturn);
            Assert.Equal(1, audioBook.CurrCount);
        }

        [Fact]
        public void TryReturn_AtMaxCount()
        {
            // Arrange
            var title = "C# in Depth";
            var description = "Deep dive into C#.";
            var author = "Jon Skeet";
            var topic = "Programming";
            var isbn = "1234567890";
            var narrator = "John Smith";
            var runTime = 100;
            var count = 1;
            var audioBook = new AudioBookCD(title, description, author, topic, isbn, narrator, runTime, count);

            // Act
            bool resReturn = audioBook.TryReturn();

            // Assert
            Assert.False(resReturn);
        }
    }
}
