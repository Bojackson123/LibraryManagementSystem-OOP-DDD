using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Publication
{
    public class Encyclopedia : Publication
    {
        public string[] Authors { get; set; }
        public int Edition { get; set; }


        public Encyclopedia(
          string[]? authors,  // Will decide later if no user input will return null or emptry []
          string title = "Unknown",
          string description = "No description added",
          string topic = "Unknown",
          string isbn = "Unknown",
          int edition = 1)
          : base(title, description, isbn, topic)

        {
            this.Authors = (authors == null || authors.Length == 0) // This will handle both cases.
              ? new string[] { "Unknown" }
              : authors;
            this.Edition = edition;
        }

        public override string Describe()
        {
            string res = ParseAuthors(this.Authors);
            string ordNum = CardinalToOrdinal(this.Edition);
            return $"{this.Title} by: " + res + $" is an encyclopedia about {this.Topic} and has an ID of {this.ID}, an ISBN of {this.Isbn}, and is the " + ordNum + $" edition.\nDescription: {this.Description}.";
        }

        public override string ToString()
        {
            string res = ParseAuthors(this.Authors);
            return $"Title: {this.Title}\nAuthors: {res}\nTopic: {this.Topic}\nISBN: {this.Isbn}\nEdition: {this.Edition}\nDescription {this.Description}";
        }

        public string ParseAuthors(string[] authors)
        {
            if (authors == null || authors.Length == 0)
                return "";

            if (authors.Length == 1)
                return authors[0];

            if (authors.Length == 2)
                return $"{authors[0]} and {authors[1]}";

            // For 3 or more
            var allExceptLast = string.Join(", ", authors[..^1]); // everything except last
            return $"{allExceptLast}, and {authors[^1]}";
        }

        public string CardinalToOrdinal(int number)
        {
            int lastTwoDigits = number % 100;

            switch (lastTwoDigits)
            {
                case 11: // Special cases for 11th to 13th.
                case 12:
                case 13:
                    return $"{number:N0}th";
                default:
                    int lastDigit = number % 10;

                    string suffix = lastDigit switch
                    {
                        1 => "st",
                        2 => "nd",
                        3 => "rd",
                        _ => "th"
                    };
                    return $"{number:N0}{suffix}";
            }
        }

    }
}
