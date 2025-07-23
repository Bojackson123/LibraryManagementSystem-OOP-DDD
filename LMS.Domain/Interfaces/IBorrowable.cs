
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
