
using LMS.Domain;
using LMS.Infrastructure.Interfaces;

namespace LMS.Infrastructure.Repositories
{
    public class InMemoryUserRepo : IUserRepository
    {
        private readonly Dictionary<int, User> allUsers = new();

        public bool TryAddUser(User userObject)
        {
            return allUsers.TryAdd(userObject.ID, userObject);
        }

        public bool TryRemoveUser(int ID)
        {
            return allUsers.Remove(ID);
        }

        public bool TryGetByID(int ID, out User? result)
        {
            if (allUsers.TryGetValue(ID, out User? value))
            {
                result = value;
                return true;
            }
            result = null;
            return false;
        }

        public List<User> GetByFirstName(string firstName)
        {
            List<User> result = new();
            foreach (User value in allUsers.Values)
            {
                if (value.FirstName.ToLower().Contains(firstName.ToLower()))
                {
                    result.Add(value);
                }
            }
            return result;
        }

        public List<User> GetByLastName(string lastName)
        {
            List<User> result = new();
            foreach (User value in allUsers.Values)
            {
                if (value.LastName.ToLower().Contains(lastName.ToLower()))
                {
                    result.Add(value);
                }
            }
            return result;
        }
    }   
}
