using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TV.Domain.Models;

namespace TV.Infrastructure.Repositories
{
    public interface ILanguageTVShowRepository : IRepository<LanguageTVShow>
    {
       bool IsFoundFromLanguageAndTVShow(int languageId,int tVShowId);
    }
}
