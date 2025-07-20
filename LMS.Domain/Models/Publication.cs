using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Publication
{
    public class Publication
    {
        // Static fields.
        private static int nextId;

        // Properties.
        public string Title { get; set; }
        public int ID { get; }
        public string Isbn { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }


        // Construtor.
        public Publication(string title, string description, string isbn, string topic)
        {
            this.Title = title;
            this.ID = nextId++;
            this.Description = description;
            this.Isbn = isbn;
            this.Topic = topic;
        }

        // Static constructor.
        static Publication()
        {
            nextId = 1;
        }

        // Methods
        public virtual string Describe()
        {
            return $"{this.Title} is a publication with ID: {this.ID}, ISBN: {this.Isbn}\nDescription: {this.Description}";
        }

        public override string ToString()
        {
            return $"Title: {this.Title}\nID: {this.ID}\nISBN: {this.Isbn}";
        }

    }
}
