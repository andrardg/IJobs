﻿using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.UserRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        List<User> GetByFirstName(string FirstName);
        List<User> GetByLastName(string LastName);
        List<User> GetAllWithEmploymentInclude();
        List<User> GetAllEmployedLINQ();
    }
}