using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Application.DTOs.AdminSide.Sellers;

public class CreateSellerDTO
{
    public string SellerName { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; } = true;
}
