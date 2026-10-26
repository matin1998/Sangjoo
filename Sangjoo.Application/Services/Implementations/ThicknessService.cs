using Sangjoo.Application.Services.Interfaces;
using Sangjoo.Domain.Entities;
using Sangjoo.Domain.RepositoryInterfaces;
using Sangjoo.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Application.Services.Implementations;

public class ThicknessService : IThicknessService
{
    private readonly IThicknessRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ThicknessService(
        IThicknessRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool Success, string? Error, long? Id)>
        AddAsync(decimal value)
    {
        if (value <= 0)
            return (
                false,
                "مقدار ضخامت باید بزرگ‌تر از صفر باشد.",
                null);

        if (await _repository.ExistsByValueAsync(value))
            return (
                false,
                "این ضخامت قبلاً ثبت شده است.",
                null);

        var thickness = new Thickness
        {
            Value = value,
            IsActive = true
        };

        await _repository.AddAsync(thickness);
        await _unitOfWork.SaveChangesAsync();

        return (true, null, thickness.Id);
    }
}
