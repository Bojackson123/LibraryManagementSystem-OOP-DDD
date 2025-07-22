using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain;
using LMS.Domain.Publication;
using LMS.Infrastructure;
using LMS.Infrastructure.Repositories;

namespace LMS.Tests.InfrastructureTests.InMemoryPubRepoTests
{
    public class InMemoryPubRepoTests
    {
        [Fact]
        public void TryAddPublication_ShouldReturnTrueIfSuccessful()
        {
            // Arrange
            Book b = new();
            InMemoryPubRepo repo = new();

            // Act
            bool res = repo.TryAddPublication(b);

            // Assert
            Assert.True(res);
        }

        [Fact]
        public void TryAddPublication_ShouldAcceptAllPublicationTypes()
        {
            // Arrange
            Publication[] testArr =
            [
                new Book(),
                new AudioBookCD(),
                new Encyclopedia(null),
                new Periodical(),
                new VideoStreaming(),
                new VideoDVD()
            ];
            InMemoryPubRepo repo = new();
            int count = repo.GetPublicationsCount();
            Assert.Equal(0, count);

            // Act
            foreach (Publication pub in testArr)
            {
                repo.TryAddPublication(pub);
            }

            // Assert
            Assert.Equal(6, repo.GetPublicationsCount());
        }

        [Fact]
        public void TryRemovePublication_ShouldReturnTrueIfSuccessful()
        {
            // Arrange
            Book b1 = new();
            int b1ID = b1.ID;
            InMemoryPubRepo repo = new();
            repo.TryAddPublication(b1);
            int countBefore = repo.GetPublicationsCount();

            // Act
            bool res1 = repo.TryRemovePublication(b1ID);
            bool res2 = repo.TryRemovePublication(1234);
            // Assert
            Assert.Equal(countBefore - 1, repo.GetPublicationsCount());
            Assert.True(res1);
            Assert.False(res2);
        }

        [Fact]
        public void TryGetByID_ShouldReturnPublicationOrNull()
        {
            // Arrange
            Book a = new();
            int aID = a.ID;
            AudioBookCD b = new();
            int bID = b.ID;
            Encyclopedia c = new(null);
            int cID = c.ID;
            Periodical d = new();
            int dID = d.ID;
            VideoStreaming e = new();
            int eID = e.ID;
            VideoDVD f = new();
            int fID = f.ID;
            int gID = 12345;
            Publication[] testArr = [a, b, c, d, e, f];

            InMemoryPubRepo repo = new();

            foreach (Publication pubObject in testArr)
            {
                repo.TryAddPublication(pubObject);
            }

            // Act
            bool resA = repo.TryGetById(aID, out Publication? outputA);
            bool resB = repo.TryGetById(bID, out Publication? outputB);
            bool resC = repo.TryGetById(cID, out Publication? outputC);
            bool resD = repo.TryGetById(dID, out Publication? outputD);
            bool resE = repo.TryGetById(eID, out Publication? outputE);
            bool resF = repo.TryGetById(fID, out Publication? outputF);
            bool resG = repo.TryGetById(gID, out Publication? outputG);

            // Assert
            Assert.Equal(a, outputA);
            Assert.True(resA);

            Assert.Equal(b, outputB);
            Assert.True(resB);

            Assert.Equal(c, outputC);
            Assert.True(resC);

            Assert.Equal(d, outputD);
            Assert.True(resD);

            Assert.Equal(e, outputE);
            Assert.True(resE);

            Assert.Equal(f, outputF);
            Assert.True(resF);

            Assert.Null(outputG);
            Assert.False(resG);

        }

        [Fact]
        public void TryGetByIsbn_ShouldReturnPublicationOrNull()
        {
            // Arrange
            Book a = new(isbn: "a");
            string aIsbn = a.Isbn;
            AudioBookCD b = new(isbn: "b");
            string bIsbn = b.Isbn;
            Encyclopedia c = new(null, isbn: "c");
            string cIsbn = c.Isbn;
            Periodical d = new(isbn: "d");
            string dIsbn = d.Isbn;
            VideoStreaming e = new(isbn: "e");
            string eIsbn = e.Isbn;
            VideoDVD f = new(isbn: "f");
            string fIsbn = f.Isbn;
            string gIsbn = "12345";
            Publication[] testArr = [a, b, c, d, e, f];

            InMemoryPubRepo repo = new();

            foreach (Publication pubObject in testArr)
            {
                repo.TryAddPublication(pubObject);
            }

            // Act
            bool resA = repo.TryGetByIsbn(aIsbn, out Publication? outputA);
            bool resB = repo.TryGetByIsbn(bIsbn, out Publication? outputB);
            bool resC = repo.TryGetByIsbn(cIsbn, out Publication? outputC);
            bool resD = repo.TryGetByIsbn(dIsbn, out Publication? outputD);
            bool resE = repo.TryGetByIsbn(eIsbn, out Publication? outputE);
            bool resF = repo.TryGetByIsbn(fIsbn, out Publication? outputF);
            bool resG = repo.TryGetByIsbn(gIsbn, out Publication? outputG);

            // Assert
            Assert.Equal(a, outputA);
            Assert.True(resA);

            Assert.Equal(b, outputB);
            Assert.True(resB);

            Assert.Equal(c, outputC);
            Assert.True(resC);

            Assert.Equal(d, outputD);
            Assert.True(resD);

            Assert.Equal(e, outputE);
            Assert.True(resE);

            Assert.Equal(f, outputF);
            Assert.True(resF);

            Assert.Null(outputG);
            Assert.False(resG);
        }

        [Fact]
        public void GetByTitle_ShouldReturnListOfPublicationsOrEmptyList()
        {
            // Arrange
            Book a = new(title: "sBug");
            AudioBookCD b = new(title: "Bugs");
            Encyclopedia c = new(null, title: "bugsbunny");
            Periodical d = new(title: "qwsetest");
            VideoStreaming e = new(title: "fsdfdsatest");
            VideoDVD f = new(title: "jhlkjTESTkdjfl");
            Publication[] testArr = [a, b, c, d, e, f];

            List<Publication> groupBug  = [a, b, c];
            List<Publication> groupTest = [d, e, f];
            List<Publication> groupS = [a, b, c, d, e, f];
            List<Publication> groupEmpty = [];

            InMemoryPubRepo repo = new();

            foreach (Publication pubObject in testArr)
            {
                repo.TryAddPublication(pubObject);
            }

            // Act
            List<Publication> testGroupBug = repo.GetByTitle("bUg");
            List<Publication> testGroupTest = repo.GetByTitle("Test");
            List<Publication> testGroupS = repo.GetByTitle("s");
            List<Publication> testGroupEmpty = repo.GetByTitle("Not In The List");

            // Assert
            Assert.Equal(groupBug, testGroupBug);
            Assert.Equal(groupTest, testGroupTest);
            Assert.Equal(groupS, testGroupS);
            Assert.Equal(groupEmpty, testGroupEmpty);
        }

        [Fact]
        public void GetByCreator_ShouldReturnListOfPublicationsOrEmptyList()
        {
            // Arrange
            Book a = new(author: "sBug");
            AudioBookCD b = new(author: "Bugs");
            Encyclopedia c = new(authors: new string[] { "bugsbunny", "djskbugkdjsl" });
            Periodical d = new(publisher: "qwsetest");
            VideoStreaming e = new(director: "fsdfdsatest");
            VideoDVD f = new(director: "jhlkjTESTkdjfl");
            Publication[] testArr = [a, b, c, d, e, f];

            List<Publication> groupBug = [a, b, c];
            List<Publication> groupTest = [d, e, f];
            List<Publication> groupS = [a, b, c, d, e, f];
            List<Publication> groupEmpty = [];

            InMemoryPubRepo repo = new();

            foreach (Publication pubObject in testArr)
            {
                repo.TryAddPublication(pubObject);
            }

            // Act
            List<Publication> testGroupBug = repo.GetByCreator("bUg");
            List<Publication> testGroupTest = repo.GetByCreator("Test");
            List<Publication> testGroupS = repo.GetByCreator("s");
            List<Publication> testGroupEmpty = repo.GetByCreator("Not In The List");

            // Assert
            Assert.Equal(groupBug, testGroupBug);
            Assert.Equal(groupTest, testGroupTest);
            Assert.Equal(groupS, testGroupS);
            Assert.Equal(groupEmpty, testGroupEmpty);
        }

        [Fact]
        public void GetByTopic_ShouldReturnListOfPublicationsOrEmptyList()
        {
            // Arrange
            Book a = new(topic: "sBug");
            AudioBookCD b = new(topic: "Bugs");
            Encyclopedia c = new(null, topic: "bugsbunny");
            Periodical d = new(topic: "qwsetest");
            VideoStreaming e = new(topic: "fsdfdsatest");
            VideoDVD f = new(topic: "jhlkjTESTkdjfl");
            Publication[] testArr = [a, b, c, d, e, f];

            List<Publication> groupBug = [a, b, c];
            List<Publication> groupTest = [d, e, f];
            List<Publication> groupS = [a, b, c, d, e, f];
            List<Publication> groupEmpty = [];

            InMemoryPubRepo repo = new();

            foreach (Publication pubObject in testArr)
            {
                repo.TryAddPublication(pubObject);
            }

            // Act
            List<Publication> testGroupBug = repo.GetByTopic("bUg");
            List<Publication> testGroupTest = repo.GetByTopic("Test");
            List<Publication> testGroupS = repo.GetByTopic("s");
            List<Publication> testGroupEmpty = repo.GetByTopic("Not In The List");

            // Assert
            Assert.Equal(groupBug, testGroupBug);
            Assert.Equal(groupTest, testGroupTest);
            Assert.Equal(groupS, testGroupS);
            Assert.Equal(groupEmpty, testGroupEmpty);
        }

    }
}
