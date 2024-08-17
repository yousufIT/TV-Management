using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TV.Domain.Models;

namespace TV.Infrastructure.Repositories
{
    public class TVShowRepository : GenericRepository<TVShow>, ITVShowRepository
    {
        public TVShowRepository(ApplicationDbContext context) : base(context)
        {
        }

        public TVShow GetByIdWithLanguage(int id)
        {
            return _context.TVShows
                  .Include(tv => tv.LanguageTVShows) 
                  .ThenInclude(ltv => ltv.Language) 
                  .FirstOrDefault(tv => tv.Id == id);
        }

        public IEnumerable<TVShow> GetByLanguage(string language)
        {
            return _context.TVShows
                .Include(t => t.LanguageTVShows) 
                .ThenInclude(lts => lts.Language) 
                .Where(t => t.LanguageTVShows.Any(lts => lts.Language.Name == language))
                .ToList();
        }

    }
}
