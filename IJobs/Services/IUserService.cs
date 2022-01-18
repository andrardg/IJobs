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
        void Register(UserRequestDTO model);
        void Update(Guid id, UserRequestDTO model);
        //get all
        IEnumerable<UserResponseDTO> GetAllUsers();
        //get by id
        User GetById(Guid? id);

        void Create(User user);
        Task<User> FindByIdAsinc(Guid? id);
        void Update(User entity);
        Task<bool> SaveAsync();
        bool Save();
        void Delete(User entity);
    }
}
