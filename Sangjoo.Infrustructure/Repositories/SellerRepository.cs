using Microsoft.EntityFrameworkCore;
using Sangjoo.Domain.Entities;
using Sangjoo.Domain.RepositoryInterfaces;
using Sangjoo.Infrustructure.SangjooStoreDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Infrustructure.Repositories;

public class SellerRepository : BaseRepository<Seller>, ISellerRepository
{
    public SellerRepository(SangjooDbContext context)
        : base(context)
    {
    }

    public async Task<Seller?> GetByUserIdAsync(long userId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }
}
