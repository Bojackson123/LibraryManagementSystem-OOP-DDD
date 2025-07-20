using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Publication;
using LMS.Domain;

namespace LMS.Infrastructure.Interfaces
{
    internal interface IBorrowableRepository
    {
        IBorrowable? GetById(int ID);

        bool AddBorrowablePublication(IBorrowable pubObject);

        bool RemoveBorrowablePublication(int ID);

        void Save(Publication pubObject);


    }
}
