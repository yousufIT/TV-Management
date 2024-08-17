using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TV.Domain.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public List<LanguageTVShow> LanguageTVShows { get; set; }
        public Language()
        {
            LanguageTVShows = new List<LanguageTVShow>();
        }
    }

}
