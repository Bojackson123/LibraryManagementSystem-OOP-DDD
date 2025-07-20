using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Publication;

namespace LMS.Tests.DomainTests.EncyclopediaTests
{
    public class EncyclopediaTests
    {
        [Fact]
        public void Constructor_ShouldInitializeAllProperties()
        {
            // Arrange
            var authors = new string[] { "Test Testing", "Rest Resting" };
            var title = "Test book";
            var description = "Something or another.";
            var topic = "Unit Testing";
            var isbn = "1234567890";
            var edition = 1;


            // Act
            var encyclo = new Encyclopedia(authors, title, description, topic, isbn, edition);

            // Assert
            Assert.Equal(authors, encyclo.Authors);
            Assert.Equal(title, encyclo.Title);
            Assert.Equal(description, encyclo.Description);
            Assert.Equal(topic, encyclo.Topic);
            Assert.Equal(isbn, encyclo.Isbn);
            Assert.Equal(edition, encyclo.Edition);
        }

        [Fact]
        public void Constructor_ShouldDefaultAllProperties()
        {
            // Arrange
            string[]? authors = new string[] { "Unknown" };
            string title = "Unknown";
            string description = "No description added";
            string topic = "Unknown";
            string isbn = "Unknown";
            int edition = 1;

            // Act
            var encyclo = new Encyclopedia(null);

            // Assert
            Assert.Equal(authors, encyclo.Authors);
            Assert.Equal(title, encyclo.Title);
            Assert.Equal(description, encyclo.Description);
            Assert.Equal(topic, encyclo.Topic);
            Assert.Equal(isbn, encyclo.Isbn);
            Assert.Equal(edition, encyclo.Edition);
        }

        [Theory]
        [MemberData(nameof(EncyclopediaTestData.ParseAuthorsCases), MemberType = typeof(EncyclopediaTestData))]
        public void ParseAuthors_ReturnsCombinedString(string[] authors, string expected)
        {
            // Arrange
            var encyclo = new Encyclopedia(null);

            // Act
            string result = encyclo.ParseAuthors(authors);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(EncyclopediaTestData.CardinalToOrdCases), MemberType = typeof(EncyclopediaTestData))]
        public void CardinalToOrdCases_ReturnsRightSuffix(int number, string expected)
        {
            // Arrange
            var encyclo = new Encyclopedia(null);

            // Act
            string result = encyclo.CardinalToOrdinal(number);

            // Assert
            Assert.Equal(result, expected);
        }

    }
}
