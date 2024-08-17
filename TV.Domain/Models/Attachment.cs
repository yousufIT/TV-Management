using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TV.Domain.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string FileType { get; set; }
        public int TVShowId { get; set; }
        public TVShow TVShow { get; set; }
        public bool IsDeleted { get; set; }
    }

}
