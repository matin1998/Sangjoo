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

public class MineService : IMineService
{
    private readonly IMineRepository _mineRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MineService(
        IMineRepository mineRepository,
        IUnitOfWork unitOfWork)
    {
        _mineRepository = mineRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool Success, string? Error, long? Id)> AddAsync(
        string name)
    {
        name = name?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(name))
        {
            return (false, "نام معدن الزامی است.", null);
        }

        var exists = await _mineRepository.ExistsByNameAsync(name);

        if (exists)
        {
            return (false, "این معدن قبلاً ثبت شده است.", null);
        }

        var mine = new Mine
        {
            Name = name,
            IsActive = true
        };

        await _mineRepository.AddAsync(mine);
        await _unitOfWork.SaveChangesAsync();

        return (true, null, mine.Id);
    }
}
