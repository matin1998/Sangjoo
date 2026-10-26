using Sangjoo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Domain.RepositoryInterfaces;

public interface ICommercialNameRepository
    : IBaseRepository<CommercialName>
{
    Task<bool> ExistsByNameAsync(string name);
}