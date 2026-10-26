using Sangjoo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Domain.RepositoryInterfaces
{
    public interface IQualityGradeRepository
    : IBaseRepository<QualityGrade>
    {
        Task<bool> ExistsByNameAsync(string name);
        Task<int> GetMaxSortOrderAsync();
    }
}
