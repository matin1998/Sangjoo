using Microsoft.AspNetCore.Mvc;
using Sangjoo.Application.Services.Interfaces;

namespace Sangjoo.Presentation.Areas.Seller.Controllers;

public class LookupController : SellerBaseController
{
    private readonly ICommercialNameService _commercialNameService;
    private readonly IStoneTypeService _stoneTypeService;
    private readonly IThicknessService _thicknessService;
    private readonly IMineService _mineService;
    private readonly IProcessingTypeService _processingTypeService;
    private readonly IQualityGradeService _qualityGradeService;
    private readonly IStoneApplicationService _stoneApplicationService;
    public LookupController(
    ICommercialNameService commercialNameService,
    IStoneTypeService stoneTypeService,
    IThicknessService thicknessService,
    IMineService mineService,
    IProcessingTypeService processingTypeService,
    IQualityGradeService qualityGradeService,
    IStoneApplicationService stoneApplicationService
    )
    {
        _commercialNameService = commercialNameService;
        _stoneTypeService = stoneTypeService;
        _thicknessService = thicknessService;
        _mineService = mineService;
        _processingTypeService = processingTypeService;
        _qualityGradeService = qualityGradeService;
        _stoneApplicationService = stoneApplicationService;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddCommercialName(
        string name)
    {
        var result =
            await _commercialNameService.AddAsync(name);

        if (!result.Success)
        {
            return BadRequest(new
            {
                success = false,
                message = result.Error
            });
        }

        return Json(new
        {
            success = true,
            id = result.Id,
            name = name.Trim()
        });
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddStoneType(string name)
    {
        var result =
            await _stoneTypeService.AddAsync(name);

        if (!result.Success)
        {
            return BadRequest(new
            {
                success = false,
                message = result.Error
            });
        }

        return Json(new
        {
            success = true,
            id = result.Id,
            name = name.Trim()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddThickness(decimal value)
    {
        var result =
            await _thicknessService.AddAsync(value);

        if (!result.Success)
        {
            return BadRequest(new
            {
                success = false,
                message = result.Error
            });
        }

        return Json(new
        {
            success = true,
            id = result.Id,
            value = value
        });
    }
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddMine(string name)
    {
        var result = await _mineService.AddAsync(name);

        if (!result.Success)
        {
            return BadRequest(new
            {
                success = false,
                message = result.Error
            });
        }

        return Json(new
        {
            success = true,
            id = result.Id,
            name = name.Trim()
        });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddProcessingType(string name)
    {
        var result = await _processingTypeService.AddAsync(name);

        if (!result.Success)
        {
            return BadRequest(new
            {
                success = false,
                message = result.Error
            });
        }

        return Json(new
        {
            success = true,
            id = result.Id,
            name = name.Trim()
        });
    }
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> AddQualityGrade(string name)
    {
        var result = await _qualityGradeService.AddAsync(name);

        if (!result.Success)
        {
            return BadRequest(new
            {
                success = false,
                message = result.Error
            });
        }

        return Json(new
        {
            success = true,
            id = result.Id,
            name = name.Trim()
        });
    }

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> AddStoneApplication(string name)
    {
        var result = await _stoneApplicationService.AddAsync(name);

        if (!result.Success)
        {
            return BadRequest(new
            {
                success = false,
                message = result.Error
            });
        }

        return Json(new
        {
            success = true,
            id = result.Id,
            name = name.Trim()
        });
    }

}
