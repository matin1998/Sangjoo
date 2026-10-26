using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Application.Services.Interfaces
{
    public interface IProcessingTypeService
    {
        Task<(bool Success, string? Error, long? Id)> AddAsync(string name);
    }
}
