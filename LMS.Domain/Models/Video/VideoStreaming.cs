using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Publication;

public class VideoStreaming : Publication
{
    public string Director { get; set; }
    public int RunTime { get; set; }

    public VideoStreaming(
      string title = "Unknown",
      string description = "Description not added.",
      string director = "Unknown",
      string topic = "Unknown",
      string isbn = "Unknown",
      int runTime = 0)
      : base(title, description, isbn, topic)
    {
        this.Director = director;
        this.RunTime = runTime;
    }

    public override string Describe()
    {
        return $"{this.Title} is a {this.Topic} movie directed by {this.Director}. It has an ID of {this.ID} and ISBN of {this.Isbn}.\nDescription: {this.Description}";
    }

    public override string ToString()
    {
        return $"ID: {this.ID}\nTitle: {this.Title}\nDirector: {this.Director}\nGenre: {this.Topic}\nRun Time: {this.RunTime}\nISBN: {this.Isbn}\nDescription: {this.Description}";
    }


}
