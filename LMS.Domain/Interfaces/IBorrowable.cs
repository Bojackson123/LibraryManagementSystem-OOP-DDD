using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LMS.Domain
{
    public interface IBorrowable
    {
        // Properties
        int CurrCount { get; }
        int MaxCount { get; }
        // Methods
        bool TryBorrow();
        bool TryReturn();

    }
}
