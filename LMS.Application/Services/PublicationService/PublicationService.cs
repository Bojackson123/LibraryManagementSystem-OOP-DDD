using LMS.Domain;
using LMS.Domain.Publication;
using LMS.Infrastructure.Interfaces;

namespace LMS.Application.Services;

public class PublicationService
{
    private readonly IPublicationRepository _repo;

    public PublicationService(IPublicationRepository repo)
    {
        _repo = repo;
    }

    public bool TryAddPublication(Publication publication)
    {
        return _repo.TryAddPublication(publication);
    }

    public bool TryRemovePublication(int pubID)
    {
        return _repo.TryRemovePublication(pubID);
    }
    
    public bool TryBorrowPub(IBorrowable pubObject)
    {
        if (pubObject is not IBorrowable)
        {
            return false;
        }
        return pubObject.TryBorrow();
    }

    public bool TryReturnPub(IBorrowable pubObject)
    {
        if (pubObject is not IBorrowable)
        {
            return false;
        }
        return pubObject.TryReturn();
    }

    public Publication? GetByID(int ID)
    {
        _repo.TryGetById(ID, out Publication? result);
        
        return result;
    }

    

    
}