using Sangjoo.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Application.Services.Interfaces;

public interface IImageService
{
    Task AddImageAsync(IFormFile imageFile, long productId);

    Task DeleteImageAsync(long imageId, long productId);

    Task EditImageAsync(long imageId, IFormFile newImage);

    Task DeleteImagesByProductIdAsync(long productId);

    Task<Image> GetImageByIdAsync(long imageId);

    Task<List<Image>> GetImagesByProductIdAsync(long productId);

    Task<List<Image>> GetAllImages();

    Task SetMainImageAsync(long imageId);
}
