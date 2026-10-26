using Sangjoo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Domain.RepositoryInterfaces;

public interface IStoneRepository:IBaseRepository<Stone>
{
    Task<Stone?> GetAStoneByIdAsync(long stoneId);

    Task<Stone?> GetByIdWithApplicationsAsync(long stoneId);

    Task<List<Stone>> GetListOfStonesAsync();

    Task DeleteAStone(long stoneId);

    Task<List<Stone>> GetListOfStonesBySellerIdAsync(long sellerId);

    Task<Stone?> GetByIdAndSellerIdAsync(
        long stoneId,
        long sellerId);

    Task<Stone?> GetStoneDetailsByIdAsync(long stoneId);
}
