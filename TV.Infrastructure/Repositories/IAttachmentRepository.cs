using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TV.Domain.Models;
using Attachment = TV.Domain.Models.Attachment;

namespace TV.Infrastructure.Repositories
{
    public interface IAttachmentRepository : IRepository<Attachment>
    {

    }
}
