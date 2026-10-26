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

public class ImageRepository : BaseRepository<Image>,IImageRepository
{
    #region Ctor
    public ImageRepository(SangjooDbContext context)
        :base(context)
    {
    }

    #endregion
    public async Task AddImageToDataBase(Image image)
    {
        await AddAsync(image);
    }

    public async Task DeleteAnImage(Image image)
    {
         await DeleteAsync(image);
    }

    public async Task EditAnImage(Image image)
    {
        await UpdateAsync(image);
    }

    public async Task<Image?> GetAnImageByIdAsync(long imageId)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.Id == imageId);
    }

    public async Task<Image?> GetAnImageByIdAndProductIdAsync(
    long imageId,
    long productId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(x =>
                x.Id == imageId &&
                x.ProductId == productId);
    }

    public async Task<List<Image>> GetListOfImages()
    {
        return await GetAllAsync();
    }
    public async Task<List<Image>> GetImagesByProductIdAsync(long productId)
    {
        return await _dbSet
            .Where(x => x.ProductId == productId)
            .ToListAsync();
    }


}
