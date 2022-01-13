using IJobs.Data;
using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Utilities.JWTUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services
{
    public class UserService: IUserService
    {
        public projectContext context;
        private IJWTUtils ijwtUtils;
        public UserResponseDTO Authenticate(UserRequestDTO model)
        {
            var user = context.Users.FirstOrDefault(x => x.Email == model.Email);
            if (user == null || BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return null;
            }
            //JWT generation (JSON WEB TOKEN)
            var jwtToken = ijwtUtils.GenerateJWTToken(user);
            return new UserResponseDTO(user, jwtToken);
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
