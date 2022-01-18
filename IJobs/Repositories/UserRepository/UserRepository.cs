using IJobs.Data;
using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using IJobs.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(projectContext context) : base(context)
        {

        }
        public List<User> GetByFirstName(string FirstName)
        {
            return _table.Where(s => s.FirstName!.ToLower().Contains(FirstName.ToLower())).ToList();
        }
        public List<User> GetByLastName(string LastName)
        {
            return _table.Where(x => x.LastName!.ToLower().Contains(LastName.ToLower())).ToList();
        }
        public List<User> GetByEmail(string Email)
        {
            return _table.Where(x => x.Email!.ToLower().Equals(Email.ToLower())).ToList();
        }
        public List<User> GetAllWithEmploymentInclude()
        {
            var result = _table.ToList();
            //var result = _table.Include(x => x.Employment).ToList();
            return result;

        }
        public List<User> GetAllEmployedLINQ()
        {
            var results = (from m1 in _table
                         where m1.Employment.Status == "Employed"
                         select m1).ToList();
            return results;
        }
    }
}
