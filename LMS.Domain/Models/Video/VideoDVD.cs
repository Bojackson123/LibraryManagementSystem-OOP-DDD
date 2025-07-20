using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Publication
{
    public class VideoDVD : VideoStreaming, IBorrowable
    {
        // IBorrowable properties
        public int CurrCount { get; private set; }
        public int MaxCount { get; private set; }

        public VideoDVD(
          string title = "Unknown",
          string description = "Description not added.",
          string director = "Unknown",
          string topic = "Unknown",
          string isbn = "Unknown",
          int runTime = 0,
          int count = 1)
          : base(title, description, director, topic, isbn, runTime)
        {
            this.CurrCount = count;
            this.MaxCount = count;
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
