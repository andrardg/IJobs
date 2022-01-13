using IJobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Utilities.JWTUtils
{
    public interface IJWTUtils
    {
        public string GenerateJWTToken(User user);
        public Guid ValidateJWTToken(string token);
    }
}
