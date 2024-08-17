using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TV.Domain.Models;

namespace TV.Infrastructure.Repositories
{
    public class LanguagesRepository : GenericRepository<Language>, ILanguagesRepository
    {
        public LanguagesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Language GetByName(string name)
        {
            return _context.Languages.FirstOrDefault(l=>l.Name.ToLower()==name.ToLower());
        }
    }
}
