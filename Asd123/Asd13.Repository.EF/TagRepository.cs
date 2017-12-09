using Asd123.Domain;
using Asd123.Repository.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;

namespace Asd13.Repository.EF
{
    public class TagRepository : GenericCrudRepository<Tag>, ITagRepository
    {
        public TagRepository(ApplicationDbContext dbContext)
        {
            Context = dbContext;
        }
        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Tag> FindByIdentifier(Guid id)
        {
            var tasks = await FindAll(x => x.Id == id);
            return tasks.FirstOrDefault();
        }

    }
}
