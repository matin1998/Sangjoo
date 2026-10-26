using Sangjoo.Application.Services.Interfaces;
using Sangjoo.Domain.Entities;
using Sangjoo.Domain.RepositoryInterfaces;
using Sangjoo.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Application.Services.Implementations;

public class StoneTypeService : IStoneTypeService
{
    private readonly IStoneTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public StoneTypeService(
        IStoneTypeRepository StoneTypeRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = StoneTypeRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<(bool Success, string? Error, long? Id)>AddAsync(string name)
    {
        name = name.Trim();

        if (string.IsNullOrWhiteSpace(name))
            return (false, "نوع سنگ الزامی است.", null);

        if (await _repository.ExistsByNameAsync(name))
            return (false, "این نوع سنگ قبلاً ثبت شده است.", null);

        var slug = name
            .Replace(" ", "-");

        if (await _repository.ExistsBySlugAsync(slug))
            return (false, "این آدرس قبلاً استفاده شده است.", null);

        var stoneType = new StoneType
        {
            Name = name,
            Slug = slug,
            IsActive = true
        };

        await _repository.AddAsync(stoneType);
        await _unitOfWork.SaveChangesAsync();

        return (true, null, stoneType.Id);
    }
    /*
        public async Task DeleteAStoneType(StoneType StoneType)
        {
            await _StoneTypeRepository.DeleteAStoneType(StoneType);
        }

        public async Task EditAStoneType(StoneType StoneType)
        {
            await _StoneTypeRepository.EditAStoneType(StoneType);
        }

        public async Task<StoneType> GetAStoneTypeByIdAsync(int StoneTypeId)
        {
            StoneType StoneType = await _StoneTypeRepository.GetAStoneTypeByIdAsync(StoneTypeId);
            return StoneType;
        }

        public  List<StoneType> GetListOFCategories()
        {
            return _StoneTypeRepository.GetListOfStoneTypes();
        }

    */
}
