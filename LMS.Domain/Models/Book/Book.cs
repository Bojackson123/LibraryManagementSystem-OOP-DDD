using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Publication
{
    public class Book : Publication, IBorrowable
    {
        // Properties
        public string Author { get; set; }
        // IBorrowable Properties
        public int CurrCount { get; private set; }
        public int MaxCount { get; private set; }

        // Constructor
        public Book(
          string title = "Unknown",
          string description = "No description added.",
          string author = "Unknown",
          string topic = "Unknown",
          string isbn = "Unknown",
          int count = 1)
          : base(title, description, isbn, topic)
        {
            this.Author = author;
            this.CurrCount = count;
            this.MaxCount = count;
        }

        // Methods
        public override string Describe()
        {
            return $"{this.Title} by {this.Author} is a {this.Topic} book with ID: {this.ID} and ISBN: {this.Isbn}\nDescription: {this.Description}";
        }

        public override string ToString()
        {
            return $"ID: {this.ID}\nTitle: {this.Title}\nAuthor: {this.Author}\nGenre: {this.Topic}\nISBN: {this.Isbn}\nDescription: {this.Description}";
        }

        // IBorrowable methods.
        public bool TryBorrow()
        {
            if (this.CurrCount < 1)
            {
                return false;
            }
            this.CurrCount -= 1;
            return true;
        }

        public bool TryReturn()
        {
            if (this.CurrCount == this.MaxCount)
            {
                return false;
            }
            this.CurrCount += 1;
            return true;
        }
    }
}
