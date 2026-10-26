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
    public class ProvinceRepository
    : BaseRepository<Province>, IProvinceRepository
    {
        public ProvinceRepository(SangjooDbContext context)
            : base(context)
        {
        }
    }
}
