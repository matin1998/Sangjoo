using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Application.DTOs.AdminSide.Stones;

public class StoneImageDTO
{
    public long Id { get; set; }
    public string ImageName { get; set; } = null!;
    public bool IsMainImage { get; set; }
}
