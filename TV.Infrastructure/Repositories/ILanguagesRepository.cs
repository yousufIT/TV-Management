using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TV.Domain.Models;

namespace TV.Infrastructure.Repositories
{
    public interface ILanguagesRepository : IRepository<Language>
    {
        Language GetByName(string name);
    }
}
