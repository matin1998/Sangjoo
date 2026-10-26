using Sangjoo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Domain.RepositoryInterfaces;


public interface IStoneTypeRepository : IBaseRepository<StoneType>
{
    Task<bool> ExistsByNameAsync(string name);
    Task<bool> ExistsBySlugAsync(string slug);
}

