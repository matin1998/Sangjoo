using Sangjoo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Application.Services.Interfaces;

public interface IStoneTypeService
{
    Task<(bool Success, string? Error, long? Id)>
        AddAsync(string name);
/*    List<StoneType> GetListOFCategories();

    Task<StoneType> GetAStoneTypeByIdAsync(int stoneTypeId);

    Task EditAStoneType(StoneType stoneType);

    Task DeleteAStoneType(StoneType stoneType);*/


}
