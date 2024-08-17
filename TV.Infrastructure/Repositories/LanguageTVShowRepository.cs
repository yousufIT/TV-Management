using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TV.Domain.Models;

namespace TV.Infrastructure.Repositories
{
    public class LanguageTVShowRepository : GenericRepository<LanguageTVShow>,ILanguageTVShowRepository
    {
        public LanguageTVShowRepository(ApplicationDbContext context) : base(context)
        {
        }

        public bool IsFoundFromLanguageAndTVShow(int languageId, int tVShowId)
        {
            return _context.LanguageTVShows.Any(lt => lt.LanguageId == languageId && lt.TVShowId == tVShowId);
        }

    }
}
