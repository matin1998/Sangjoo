using Sangjoo.Application.Services.Interfaces;
using Sangjoo.Domain.Entities;
using Sangjoo.Domain.RepositoryInterfaces;
using Sangjoo.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Application.Services.Implementations;

public class ProductService:IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Product?> GetByIdAsync(long id)
    {
        return await _productRepository.GetByIdAsync(id);
    }
}
