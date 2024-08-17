using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TV.Domain.Models;

namespace TV.Infrastructure.Repositories
{
    public interface ITVShowRepository : IRepository<TVShow>
    {
        IEnumerable<TVShow> GetByLanguage(string language);
        TVShow GetByIdWithLanguage(int id);
    }
}
