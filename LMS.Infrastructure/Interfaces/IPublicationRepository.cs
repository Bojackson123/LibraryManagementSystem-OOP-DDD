using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Publication;

namespace LMS.Infrastructure.Interfaces;

public interface IPublicationRepository
{
    bool TryGetById(int ID, out Publication? result);

    bool TryGetByIsbn(string isbn, out Publication? result);

    List<Publication> GetByTitle(string title);

    List<Publication> GetByCreator(string creator);

    List<Publication> GetByTopic(string topic);

    bool TryAddPublication(Publication pubObject);

    bool TryRemovePublication(int ID);

    void Save(Publication pubObject);
}
