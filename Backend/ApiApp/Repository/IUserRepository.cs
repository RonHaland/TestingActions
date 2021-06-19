using ApiApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApp.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsersByName(string name);
        Task AddUser(User user);
    }
}
