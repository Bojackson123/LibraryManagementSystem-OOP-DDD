using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Publication;

namespace LMS.Tests.DomainTests.PeriodicalTests
{
    public class PeriodicalTests
    {
        [Fact]
        public void Constructor_ShouldInitializeAllProperties()
        {
            // Arrange
            var title = "C# in Depth";
            var description = "Deep dive into C#.";
            var topic = "Programming";
            var isbn = "1234567890";
            var publisher = "Jon Skeet";
            var issueNumber = 8;
            var count = 3;

            // Act
            var period = new Periodical(title, description, isbn, topic, publisher, issueNumber, count);

            // Assert
            Assert.Equal(title, period.Title);
            Assert.Equal(description, period.Description);
            Assert.Equal(publisher, period.Publisher);
            Assert.Equal(topic, period.Topic);
            Assert.Equal(isbn, period.Isbn);
            Assert.Equal(count, period.MaxCount);
            Assert.Equal(count, period.CurrCount);
            Assert.Equal(issueNumber, period.IssueNumber);
        }

        [Fact]
        public void Constructor_ShouldDefaultAllProperties()
        {
            // Arrange
            string title = "Unknown";
            string description = "No description added.";
            string topic = "Unknown";
            string isbn = "Unknown";
            string publisher = "Unknown";
            int issueNumber = 1;
            int count = 1;

            // Act
            var period = new Periodical();

            Assert.Equal(title, period.Title);
            Assert.Equal(description, period.Description);
            Assert.Equal(publisher, period.Publisher);
            Assert.Equal(topic, period.Topic);
            Assert.Equal(isbn, period.Isbn);
            Assert.Equal(count, period.MaxCount);
            Assert.Equal(count, period.CurrCount);
            Assert.Equal(issueNumber, period.IssueNumber);
        }

        [Theory]
        [InlineData(13)]
        [InlineData(0)]
        public void Constructor_InitIssueNumberOutOfBounds(int number)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Periodical(issueNumber: number));

        }

        [Theory]
        [InlineData(1)]
        [InlineData(12)]
        public void Constructor_InitIssueNumberInBounds(int number)
        {
            // Act
            var period = new Periodical(issueNumber: number);

            //Assert
            Assert.Equal(number, period.IssueNumber);
        }

        [Fact]
        public void TryBarrow_WithCopiesAvaliable()
        {
            //Arrange
            var title = "C# in Depth";
            var description = "Deep dive into C#.";
            var topic = "Programming";
            var isbn = "1234567890";
            var publisher = "Jon Skeet";
            var issueNumber = 8;
            var count = 3;
            var period = new Periodical(title, description, isbn, topic, publisher, issueNumber, count);

            // Act
            bool resTrue = period.TryBorrow();

            // Assert
            Assert.True(resTrue);
            Assert.Equal(2, period.CurrCount);

        }

        [Fact]
        public void TryBarrow_WithoutCopiesAvaliable()
        {
            //Arrange
            var title = "C# in Depth";
            var description = "Deep dive into C#.";
            var topic = "Programming";
            var isbn = "1234567890";
            var publisher = "Jon Skeet";
            var issueNumber = 8;
            var count = 1;
            var period = new Periodical(title, description, isbn, topic, publisher, issueNumber, count);

            //Act
            bool resTrue = period.TryBorrow();
            bool resFalse = period.TryBorrow();

            //Assert
            Assert.True(resTrue);
            Assert.False(resFalse);
            Assert.Equal(0, period.CurrCount);
        }

        [Fact]
        public void TryReturn_UnderMaxCount()
        {
            //Arrange
            var title = "C# in Depth";
            var description = "Deep dive into C#.";
            var topic = "Programming";
            var isbn = "1234567890";
            var publisher = "Jon Skeet";
            var issueNumber = 8;
            var count = 1;
            var period = new Periodical(title, description, isbn, topic, publisher, issueNumber, count);

            // Act
            bool resBarrow = period.TryBorrow();
            bool resReturn = period.TryReturn();

            // Assert
            Assert.True(resBarrow);
            Assert.True(resReturn);
            Assert.Equal(1, period.CurrCount);
        }

        [Fact]
        public void TryReturn_AtMaxCount()
        {
            //Arrange
            var title = "C# in Depth";
            var description = "Deep dive into C#.";
            var topic = "Programming";
            var isbn = "1234567890";
            var publisher = "Jon Skeet";
            var issueNumber = 8;
            var count = 1;
            var period = new Periodical(title, description, isbn, topic, publisher, issueNumber, count);

            // Act
            bool resReturn = period.TryReturn();

            // Assert
            Assert.False(resReturn);
        }

    }
}
