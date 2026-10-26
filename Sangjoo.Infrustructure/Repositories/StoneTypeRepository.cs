using Sangjoo.Domain.Entities;
using Sangjoo.Domain.RepositoryInterfaces;
using Sangjoo.Infrustructure.SangjooStoreDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sangjoo.Infrustructure.Repositories;

namespace BookStore.infrastructure.Repositories;

public class StoneTypeRepository
: BaseRepository<StoneType>, IStoneTypeRepository
{
    public StoneTypeRepository(SangjooDbContext context)
        : base(context)
    {
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbSet
            .AnyAsync(x => x.Name == name);
    }
    public async Task<bool> ExistsBySlugAsync(string slug)
    {
        return await _dbSet
            .AnyAsync(x => x.Slug == slug);
    }
}
