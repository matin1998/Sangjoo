using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Domain.Entities;

public class Province:BaseEntity
{
    public string Name { get; set; } = null!;

    public bool IsActive { get; set; } = true;
}
