using Sangjoo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Application.Services.Interfaces;

public interface IProductService
{
    Task<Product?> GetByIdAsync(long id);
}
