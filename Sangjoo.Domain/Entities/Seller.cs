using Sangjoo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Domain.Entities;

public class Seller: BaseEntity
{
    public string Name { get; set; } = null!;

    public bool IsActive { get; set; } = true;
    public long? UserId { get; set; }
    public ApplicationUser? User { get; set; }

    public ICollection<Stone> Stones { get; set; }
        = new List<Stone>();
}
