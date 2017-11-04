using Asd123.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asd123.Repository.EF
{
    public class UserRepository : GenericCrudRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
        {
            Context = dbContext;
        }

        public async Task<User> FindByIdentifier(string userIdentifier)
        {
            var users = await FindAll(u => u.UserIdentifier == userIdentifier);


            return users.FirstOrDefault();
        }
    }
}
