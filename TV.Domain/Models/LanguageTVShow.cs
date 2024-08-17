using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TV.Domain.Models
{
     public class LanguageTVShow
    {
        public int Id { get; set; } 
        public int TVShowId { get; set; }
        public TVShow TVShow { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
