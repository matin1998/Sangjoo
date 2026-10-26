using Sangjoo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Domain.Entities;

public abstract class Product : BaseEntity
{
    /*public string Title { get; set; }*/

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public ICollection<Image> Images { get; set; } = new List<Image>();

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
