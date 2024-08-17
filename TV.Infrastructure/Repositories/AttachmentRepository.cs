using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Attachment = TV.Domain.Models.Attachment;

namespace TV.Infrastructure.Repositories
{
    public class AttachmentRepository : GenericRepository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
