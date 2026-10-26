using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Domain.Entities;

public class QualityGrade: BaseEntity
{
    public string Name { get; set; } = null!;

    public int SortOrder { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<Stone> Stones { get; set; }
        = new List<Stone>();
}
