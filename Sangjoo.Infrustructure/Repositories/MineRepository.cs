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
    public class MineRepository
    : BaseRepository<Mine>, IMineRepository
    {
        public MineRepository(SangjooDbContext context)
            : base(context)
        {
        }
        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _dbSet.AnyAsync(
                x => x.Name == name
            );
        }
    }
}
