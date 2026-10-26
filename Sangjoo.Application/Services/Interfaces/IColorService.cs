using Sangjoo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Application.Services.Interfaces;

public interface IColorService
{
    Task<(bool Success, Color? Color, string? Error)> AddAsync(string name);
}
