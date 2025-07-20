


namespace LMS.Domain.Publication
{
    public class AudioBookCD : Book, IBorrowable
    {
        // Properties
        public string Narrator { get; set; }
        public int RunTime { get; set; }

        // Constructor.
        public AudioBookCD(
        string title = "Unknown",
        string description = "No description added.",
        string author = "Unknown",
        string topic = "Unknown",
        string isbn = "Unknown",
        string narrator = "Unknown",
        int runTime = 0,
        int count = 1)
        : base(title, description, author, topic, isbn, count)
        {
            this.Narrator = narrator;
            this.RunTime = runTime;
        }

        // Methods
        public override string ToString()
        {
            return $"ID: {this.ID}\nTitle: {this.Title}\nAuthor: {this.Author}\nTopic: {this.Topic}\nISBN: {this.Isbn}\nNarrator: {this.Narrator}\nRun Time: {this.RunTime}\nDescription: {this.Description}";
        }


    }
}
