using Microsoft.EntityFrameworkCore;
using Sangjoo.Domain.Entities;
using Sangjoo.Domain.RepositoryInterfaces;
using Sangjoo.Infrustructure.SangjooStoreDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Infrustructure.Repositories
{
    public class ThicknessRepository
    : BaseRepository<Thickness>, IThicknessRepository
    {
        public ThicknessRepository(SangjooDbContext context)
            : base(context)
        {
        }
        public async Task<bool> ExistsByValueAsync(decimal value)
        {
            return await _dbSet
                .AnyAsync(x => x.Value == value);
        }
    }
}
