using Sangjoo.Domain.Entities;
using Sangjoo.Domain.RepositoryInterfaces;
using Sangjoo.Infrustructure.SangjooStoreDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Infrustructure.Repositories;

public class StoneRepository : BaseRepository<Stone>,IStoneRepository
{
    #region Ctor
    public StoneRepository(SangjooDbContext context)
        : base(context)
    {

    }
    #endregion

    public async Task<Stone?> GetAStoneByIdAsync(long stoneId)
    {
        return await _dbSet
            .Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == stoneId);
    }

    public async Task<Stone?> GetByIdWithApplicationsAsync(long stoneId)
    {
        return await _dbSet
            .Include(x => x.Applications)
            .FirstOrDefaultAsync(x => x.Id == stoneId);
    }

    public async Task<List<Stone>> GetListOfStonesAsync()
    {
        return await _dbSet
            .Include(x => x.StoneType)
            .Include(x => x.Images)
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task DeleteAStone(long stoneId)
    {
        var stone = await _dbSet
            .FirstOrDefaultAsync(x => x.Id == stoneId);

        if (stone == null)
            return;

        _dbSet.Remove(stone);
    }
    public async Task<List<Stone>> GetListOfStonesBySellerIdAsync(long sellerId)
    {
        return await _dbSet
            .Include(x => x.StoneType)
            .Include(x => x.Images)
            .Include(x => x.ProcessingTypes)
            .Where(x => x.SellerId == sellerId)
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<Stone?> GetByIdAndSellerIdAsync(
    long stoneId,
    long sellerId)
    {
        return await _dbSet
            .Include(x => x.Images)
            .Include(x => x.Applications)
            .Include(x => x.Colors)
            .Include(x => x.ProcessingTypes)
            .FirstOrDefaultAsync(x =>
                x.Id == stoneId &&
                x.SellerId == sellerId);
    }
    public async Task<Stone?> GetStoneDetailsByIdAsync(long stoneId)
    {
        return await _dbSet
            .Include(x => x.Seller)
            .Include(x => x.StoneType)
            .Include(x => x.CommercialName)
            .Include(x => x.Colors)
            .Include(x => x.Thickness)
            .Include(x => x.Mine)
            .Include(x => x.ExtractionProvince)
            .Include(x => x.ProcessingTypes)
            .Include(x => x.StorageProvince)
            .Include(x => x.QualityGrade)
            .Include(x => x.Applications)
            .Include(x => x.Images)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == stoneId);
    }

}
