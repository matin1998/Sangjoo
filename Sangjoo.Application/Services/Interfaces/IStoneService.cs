using Sangjoo.Application.DTOs.AdminSide.Stones;
using Sangjoo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Application.Services.Interfaces
{
    public interface IStoneService
    {
        Task<StoneDTO> GetCreateModelAsync();

        Task CreateAsync(StoneDTO model);

        Task<List<Stone>> GetSellerStonesAsync(long sellerId);

        Task<Stone?> GetSellerStoneAsync(
            long stoneId,
            long sellerId);

        Task CreateForSellerAsync(
            StoneDTO model,
            long sellerId);

        Task<EditStoneDTO?> GetEditModelAsync(
            long stoneId,
            long sellerId);

        Task<bool> UpdateForSellerAsync(
            long stoneId,
            EditStoneDTO model,
            long sellerId);

        Task<bool> DeleteForSellerAsync(
            long stoneId,
            long sellerId);

        Task<StoneDetailsDTO?> GetStoneDetailsAsync(long stoneId);
    }
}
