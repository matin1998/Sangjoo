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

    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ColorService(
            IColorRepository colorRepository,
            IUnitOfWork unitOfWork)
        {
            _colorRepository = colorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool Success, Color? Color, string? Error)> AddAsync(string name)
        {
            name = name.Trim();

            if (string.IsNullOrWhiteSpace(name))
                return (false, null, "نام رنگ الزامی است.");

            var exists =
                await _colorRepository.ExistsByNameAsync(name);

            if (exists)
                return (false, null, "این رنگ قبلاً ثبت شده است.");

            var color = new Color
            {
                Name = name,
                IsActive = true
            };

            await _colorRepository.AddAsync(color);
            await _unitOfWork.SaveChangesAsync();

            return (true, color, null);
        }
    }

