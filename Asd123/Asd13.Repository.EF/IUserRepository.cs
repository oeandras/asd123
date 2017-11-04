using Asd123.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asd123.Repository.EF
{
    public interface IUserRepository
    {
        Task Create(User entity);
        Task<User> FindByIdentifier(string userIdentifier);
    }
}
