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

public class StoneApplicationService : IStoneApplicationService
{
    private readonly IStoneApplicationRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public StoneApplicationService(
        IStoneApplicationRepository repository,
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
            return (false, "نام کاربرد سنگ الزامی است.", null);
        }

        if (await _repository.ExistsByNameAsync(name))
        {
            return (false, "این کاربرد قبلاً ثبت شده است.", null);
        }

        var application = new StoneApplication
        {
            Name = name,
            IsActive = true
        };

        await _repository.AddAsync(application);
        await _unitOfWork.SaveChangesAsync();

        return (true, null, application.Id);
    }
}

