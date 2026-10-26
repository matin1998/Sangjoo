using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Domain.Entities;


public class Color:BaseEntity
{
    public string Name { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public ICollection<Stone> Stones { get; set; }
        = new List<Stone>();
}

