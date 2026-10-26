using Sangjoo.Application.DTOs.AdminSide.Sellers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Application.Services.Interfaces;

public interface ISellerService
{
    Task<bool> CreateSellerAsync(CreateSellerDTO model);
    Task<long?> GetSellerIdByUserIdAsync(long userId);
}
