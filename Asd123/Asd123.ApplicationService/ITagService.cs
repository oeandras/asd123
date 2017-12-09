using Asd123.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asd123.ApplicationService
{
    public interface ITagService
    {
        Task<Tag> GetByText(string text);
        Task<Tag> GetById(Guid id);
        Task<IReadOnlyCollection<Tag>> GetByPicture(Guid pictureId);
        Task AddToPicture(IEnumerable<string> texts, IEnumerable<Guid> pictureIds);
        Task RemoveFromPicture(IEnumerable<string> texts, IEnumerable<Guid> pictureIds);
        Task<Tag> CreateTag(string text);
        Task DeleteTag(Tag tag);
    }
}
