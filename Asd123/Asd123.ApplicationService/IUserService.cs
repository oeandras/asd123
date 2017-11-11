using Asd123.Domain;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Asd123.ApplicationService
{
    public interface IUserService
    {
        Task EnsureUser(IReadOnlyCollection<Claim> claims);
        Task<User> GetById(string id);
    }
}
