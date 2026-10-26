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


public class ProcessingTypeService : IProcessingTypeService
{
    private readonly IProcessingTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ProcessingTypeService(
        IProcessingTypeRepository repository,
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
            return (false, "نام نوع فرآوری الزامی است.", null);
        }

        var exists = await _repository.ExistsByNameAsync(name);

        if (exists)
        {
            return (false, "این نوع فرآوری قبلاً ثبت شده است.", null);
        }

        var processingType = new ProcessingType
        {
            Name = name,
            IsActive = true
        };

        await _repository.AddAsync(processingType);
        await _unitOfWork.SaveChangesAsync();

        return (true, null, processingType.Id);
    }
}
