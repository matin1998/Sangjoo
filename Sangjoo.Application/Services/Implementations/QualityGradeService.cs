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

public class QualityGradeService : IQualityGradeService
{
    private readonly IQualityGradeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public QualityGradeService(
        IQualityGradeRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool Success, string? Error, long? Id)> AddAsync(
        string name)
    {
        name = name?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(name))
        {
            return (false, "نام درجه کیفیت الزامی است.", null);
        }

        var exists = await _repository.ExistsByNameAsync(name);

        if (exists)
        {
            return (false, "این درجه کیفیت قبلاً ثبت شده است.", null);
        }

        var sortOrder = await _repository.GetMaxSortOrderAsync();

        var qualityGrade = new QualityGrade
        {
            Name = name,
            SortOrder = sortOrder + 1,
            IsActive = true
        };

        await _repository.AddAsync(qualityGrade);
        await _unitOfWork.SaveChangesAsync();

        return (true, null, qualityGrade.Id);
    }
}
