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

public class CommercialNameService : ICommercialNameService
{
    private readonly ICommercialNameRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CommercialNameService(
        ICommercialNameRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool Success, string? Error, long? Id)>
        AddAsync(string name)
    {
        name = name.Trim();

        if (string.IsNullOrWhiteSpace(name))
            return (false, "نام تجاری الزامی است.", null);

        if (await _repository.ExistsByNameAsync(name))
            return (false, "این نام تجاری قبلاً ثبت شده است.", null);

        var commercialName = new CommercialName
        {
            Name = name,
            IsActive = true
        };

        await _repository.AddAsync(commercialName);
        await _unitOfWork.SaveChangesAsync();

        return (true, null, commercialName.Id);
    }
}
