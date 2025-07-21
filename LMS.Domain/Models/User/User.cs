using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LMS.Domain
{
    public class User
    {
        private static int nextId;
        private HashSet<int> BorrowedPublications = new();
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }


        public User(string firstName, string lastName, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            ID = nextId++;
        }

        static User()
        {
            nextId = 1;
        }

        public bool TryAddPublicationID(int pubID)
        {
          
            return BorrowedPublications.Add(pubID);

        }

        public bool TryRemovePublicationID(int pubID)
        {
            return BorrowedPublications.Remove(pubID);
        }

        public int GetBorrowedCount()
        {
            return BorrowedPublications.Count;
        }
    }
}
