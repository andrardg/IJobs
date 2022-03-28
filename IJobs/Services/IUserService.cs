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
        void Create(User user);
        void Update(Guid? id, UserRequestDTO model);
        void Update(User entity);
        //get all
        IEnumerable<UserResponseDTO> GetAllUsers();
        //get by id
        UserResponseDTO GetById(Guid? id);
        Task<UserResponseDTO> GetByIdAsinc(Guid? id);
        bool Save();
        Task<bool> SaveAsync();
        void Delete(User entity);
    }
}
