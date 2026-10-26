using Sangjoo.Domain.Entities;
using Sangjoo.Domain.RepositoryInterfaces;
using Sangjoo.Infrustructure.SangjooStoreDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Infrustructure.Repositories;

public class ProductRepository: BaseRepository<Product>,IProductRepository
{
    #region Ctor
    public ProductRepository(SangjooDbContext context)
        : base(context)
    {

    }
    #endregion

}
