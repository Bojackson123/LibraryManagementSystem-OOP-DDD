using LMS.Domain.Publication;

namespace LMS.Tests.DomainTests.BookTests
{
    public class BookTests
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
            var count = 3;

            // Act
            var book = new Book(title, description, author, topic, isbn, count);

            // Assert
            Assert.Equal(title, book.Title);
            Assert.Equal(description, book.Description);
            Assert.Equal(author, book.Author);
            Assert.Equal(topic, book.Topic);
            Assert.Equal(isbn, book.Isbn);
            Assert.Equal(count, book.MaxCount);
            Assert.Equal(count, book.CurrCount);
        }

        [Fact]
        public void Constructor_ShouldDefaultAllProperties()
        {
            // Arrange
            var title = "Unknown";
            var description = "No description added.";
            var author = "Unknown";
            var topic = "Unknown";
            var isbn = "Unknown";
            var count = 1;

            // Act
            var book = new Book();

            // Assert
            Assert.Equal(title, book.Title);
            Assert.Equal(description, book.Description);
            Assert.Equal(author, book.Author);
            Assert.Equal(topic, book.Topic);
            Assert.Equal(isbn, book.Isbn);
            Assert.Equal(count, book.MaxCount);
            Assert.Equal(count, book.CurrCount);
        }

        // Method tests.
        [Fact]
        public void TryBarrow_WithCopiesAvaliable()
        {
            //Arrange
            var title = "C# in Depth";
            var description = "Deep dive into C#.";
            var author = "Jon Skeet";
            var topic = "Programming";
            var isbn = "1234567890";
            var count = 3;
            Book book = new(title, description, author, topic, isbn, count);

            // Act
            bool resTrue = book.TryBorrow();

            // Assert
            Assert.True(resTrue);
            Assert.Equal(2, book.CurrCount);

        }

        [Fact]
        public void TryBarrow_WithoutCopiesAvaliable()
        {
            //Arrange
            var title = "C# in Depth";
            var description = "Deep dive into C#.";
            var author = "Jon Skeet";
            var topic = "Programming";
            var isbn = "1234567890";
            var count = 1;
            Book book = new(title, description, author, topic, isbn, count);

            //Act
            bool resTrue = book.TryBorrow();
            bool resFalse = book.TryBorrow();

            //Assert
            Assert.True(resTrue);
            Assert.False(resFalse);
            Assert.Equal(0, book.CurrCount);
        }

        [Fact]
        public void TryReturn_UnderMaxCount()
        {
            //Arrange
            var title = "C# in Depth";
            var description = "Deep dive into C#.";
            var author = "Jon Skeet";
            var topic = "Programming";
            var isbn = "1234567890";
            var count = 1;
            Book book = new(title, description, author, topic, isbn, count);

            // Act
            bool resBarrow = book.TryBorrow();
            bool resReturn = book.TryReturn();

            // Assert
            Assert.True(resBarrow);
            Assert.True(resReturn);
            Assert.Equal(1, book.CurrCount);
        }

        [Fact]
        public void TryReturn_AtMaxCount()
        {
            //Arrange
            var title = "C# in Depth";
            var description = "Deep dive into C#.";
            var author = "Jon Skeet";
            var topic = "Programming";
            var isbn = "1234567890";
            var count = 1;
            Book book = new(title, description, author, topic, isbn, count);

            // Act
            bool resReturn = book.TryReturn();

            // Assert
            Assert.False(resReturn);
        }


    }
}
