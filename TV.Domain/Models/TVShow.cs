using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TV.Domain.Models
{
    public class TVShow
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Rating { get; set; }
        public string URL { get; set; }
        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
        public bool IsDeleted { get; set; }

        // علاقات
        public List<LanguageTVShow> LanguageTVShows { get; set; }
        public TVShow() 
        {
            LanguageTVShows = new List<LanguageTVShow>();
        }
    }

}
