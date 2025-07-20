using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Publication
{
    public class Periodical : Publication, IBorrowable
    {
        // Private field
        private int issueNumber;
        // Properties
        public string Publisher { get; set; }
        public int IssueNumber // 1-12 for each month of a year.
        {
            get => issueNumber;
            set
            {
                if (value < 1 || value > 12)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                      "Issue number must be between 1 and 12.");
                }
                issueNumber = value;
            }
        }

        // IBarrowable properties.
        public int CurrCount { get; private set; }
        public int MaxCount { get; private set; }


        public Periodical(
          string title = "Unknown",
          string description = "No description added.",
          string isbn = "Unknown",
          string topic = "Unknown",
          string publisher = "Unknown",
          int issueNumber = 1,
          int count = 1)
          : base(title, description, isbn, topic)
        {
            this.Publisher = publisher;
            this.IssueNumber = issueNumber;
            this.CurrCount = count;
            this.MaxCount = count;
        }

        public override string ToString()
        {
            return $"Title: {this.Title}\nPublisher: {this.Publisher}\nTopic: {this.Topic}\nIssue Number: {this.IssueNumber}\nISBN: {this.Isbn}\nNumber Of Copies: {this.MaxCount}\n Avaliable Copies: {this.CurrCount}\nDescription: {this.Description}";
        }

        // IBarrowable methods.
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
