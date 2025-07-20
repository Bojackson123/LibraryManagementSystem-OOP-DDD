using LMS.Infrastructure.Interfaces;
using LMS.Domain.Publication;

namespace LMS.Infrastructure.Repositories
{
    public class InMemoryPubRepo : IPublicationRepository
    {
        private readonly Dictionary<int, Publication> borrowablePublications = new();

        public bool TryGetById(int ID, out Publication? result)
        {
            if (borrowablePublications.TryGetValue(ID, out Publication? value))
            {
                result = value;
                return true;
            }
            result = null;
            return false;
        }

        public bool TryGetByIsbn(string isbn, out Publication? result)
        {
            foreach (KeyValuePair<int, Publication> entry in borrowablePublications)
            {
                if (entry.Value.Isbn == isbn)
                {
                    result = entry.Value;
                    return true;
                }
            }
            result = null;
            return false;
        }

        public List<Publication> GetByTitle(string title)
        {
            List<Publication> pubObjectList = new();
            foreach (KeyValuePair<int, Publication> entry in borrowablePublications)
            {
                if (entry.Value.Title.ToLower().Contains(title.ToLower()))
                {
                    pubObjectList.Add(entry.Value);
                }
            }
            return pubObjectList;
        }

        public List<Publication> GetByCreator(string creator)
        {
            List<Publication> pubObjectList = new();
            string lowerCreator = creator.ToLower();

            foreach (var entry in borrowablePublications)
            {
                if (IsMatchingCreator(entry.Value, lowerCreator))
                {
                    pubObjectList.Add(entry.Value);
                }
            }

            return pubObjectList;
        }

        public List<Publication> GetByTopic(string topic)
        {
            List<Publication> pubObjectList = new();
            foreach (KeyValuePair<int, Publication> entry in borrowablePublications)
            {
                if (entry.Value.Topic.ToLower().Contains(topic.ToLower()))
                {
                    pubObjectList.Add(entry.Value);
                }
            }
            return pubObjectList;
        }

        public bool TryAddPublication(Publication pubObject)
        {
            return borrowablePublications.TryAdd(pubObject.ID, pubObject);
        }

        public bool TryRemovePublication(int ID)
        {
            return borrowablePublications.Remove(ID);
        }

        public void Save(Publication pubObject)
        {
            // Placeholder for when I implement EF.
            return;
        }


        // Helper Methods
        private bool IsMatchingCreator(Publication pub, string creatorQuery)
        {
            string normalizedQuery = creatorQuery.ToLower();

            // check most derived types first
            if (pub is AudioBookCD audioBook)
            {
                return audioBook.Author.ToLower().Contains(normalizedQuery);
            }

            if (pub is Book book)
            {
                return book.Author.ToLower().Contains(normalizedQuery);
            }

            if (pub is Encyclopedia encyclopedia)
            {
                return encyclopedia.Authors.Any(a => a.ToLower().Contains(normalizedQuery));
            }

            if (pub is Periodical periodical)
            {
                return periodical.Publisher.ToLower().Contains(normalizedQuery);
            }

            // check most derived types first
            if (pub is VideoDVD dvd)
            {
                return dvd.Director.ToLower().Contains(normalizedQuery);
            }

            if (pub is VideoStreaming stream)
            {
                return stream.Director.ToLower().Contains(normalizedQuery);
            }

            return false;
        }

    }
}
