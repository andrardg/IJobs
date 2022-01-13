using IJobs.Models;
using IJobs.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services
{
    public interface IUserService
    {
        //autentificare
        UserResponseDTO Authenticate(UserRequestDTO model);
        //get all
        IEnumerable<User> GetAllUsers();
        //get by id
        User GetById(Guid id);
    }
}
