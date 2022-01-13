using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models
{
    public class IBaseEntity //interfata
    {
        Guid Id { get; set; }
        DateTime? DateCreated { get; set; }
        DateTime? DateModified { get; set; }
    }
}
