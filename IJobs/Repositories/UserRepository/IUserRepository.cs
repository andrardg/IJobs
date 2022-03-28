using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.UserRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        List<User> GetByFirstName(string firstName);
        List<User> GetByLastName(string lastName);
        List<User> GetByEmail(string email);
        List<User> GetAllWithEmploymentInclude();
        List<User> GetAllEmployedLINQ();
    }
}
