using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sangjoo.Application.DTOs.AdminSide.Sellers;
using Sangjoo.Application.Services.Interfaces;

namespace Sangjoo.Presentation.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SellerController : AdminBaseController
{
    private readonly ISellerService _sellerService;

    public SellerController(ISellerService sellerService)
    {
        _sellerService = sellerService;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateSellerDTO model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _sellerService.CreateSellerAsync(model);

        if (!result)
        {
            ModelState.AddModelError(
                "",
                "ایمیل وارد شده قبلاً استفاده شده است یا ایجاد حساب با خطا مواجه شد.");

            return View(model);
        }

        TempData["SuccessMessage"] =
            "فروشنده و حساب کاربری با موفقیت ایجاد شدند.";

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Index()
    {
        // بعداً لیست Sellerها را اضافه می‌کنیم
        return Content("Seller management");
    }
}
