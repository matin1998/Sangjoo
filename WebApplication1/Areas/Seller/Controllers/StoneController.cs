using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sangjoo.Application.DTOs.AdminSide.Stones;
using Sangjoo.Application.Services.Implementations;
using Sangjoo.Application.Services.Interfaces;

namespace Sangjoo.Presentation.Areas.Seller.Controllers
{
    public class StoneController : SellerBaseController
    {
        private readonly IStoneService _stoneService;
        private readonly ISellerService _sellerService;


        public StoneController(IStoneService stoneService,
            ISellerService sellerService)
        {
            _stoneService = stoneService;
            _sellerService = sellerService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var sellerId =
        await _sellerService
            .GetSellerIdByUserIdAsync(CurrentUserId);

            if (sellerId == null)
                return Forbid();


            var model = await _stoneService.GetCreateModelAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoneDTO model)
        {
            var sellerId =
        await _sellerService
            .GetSellerIdByUserIdAsync(CurrentUserId);

            if (sellerId == null)
                return Forbid();


            if (!ModelState.IsValid)
            {
                ViewData["ValidationError"] = "اطلاعات واردشده ناقص یا نامعتبر است. لطفاً خطاهای فرم را بررسی کنید.";
                var newModel = await _stoneService.GetCreateModelAsync();
                newModel.Description = model.Description;
                newModel.Price = model.Price;
                newModel.StoneTypeId = model.StoneTypeId;
                /*newModel.SellerId = model.SellerId;*/
                newModel.CommercialNameId = model.CommercialNameId;
                newModel.ColorIds = model.ColorIds;
                newModel.ThicknessId = model.ThicknessId;
                newModel.MinLength = model.MinLength;
                newModel.MaxLength = model.MaxLength;
                newModel.MinWidth = model.MinWidth;
                newModel.MaxWidth = model.MaxWidth;
                newModel.MineId = model.MineId;
                newModel.ExtractionProvinceId = model.ExtractionProvinceId;
                newModel.ProcessingTypeIds = model.ProcessingTypeIds;
                newModel.StorageProvinceId = model.StorageProvinceId;
                newModel.Location = model.Location;
                newModel.InventoryMin = model.InventoryMin;
                newModel.InventoryMax = model.InventoryMax;
                newModel.QualityGradeId = model.QualityGradeId;
                newModel.ApplicationIds = model.ApplicationIds;
                newModel.Images = model.Images;

                return View(newModel);
            }

            await _stoneService.CreateForSellerAsync(
        model,
        sellerId.Value);

            TempData["SuccessMessage"] =
        "سنگ با موفقیت ثبت شد.";

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sellerId =
                await _sellerService
                    .GetSellerIdByUserIdAsync(CurrentUserId);

            if (sellerId == null)
                return Forbid();

            var stones =
                await _stoneService
                    .GetSellerStonesAsync(sellerId.Value);

            return View(stones);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var sellerId =
                await _sellerService
                    .GetSellerIdByUserIdAsync(CurrentUserId);

            if (sellerId == null)
                return Forbid();

            var model =
                await _stoneService.GetEditModelAsync(
                    id,
                    sellerId.Value);

            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
    long id,
    EditStoneDTO model)
        {
            var sellerId =
                await _sellerService
                    .GetSellerIdByUserIdAsync(CurrentUserId);

            if (sellerId == null)
                return Forbid();

            if (!ModelState.IsValid)
            {
                ViewData["ValidationError"] = "اطلاعات واردشده ناقص یا نامعتبر است. لطفاً خطاهای فرم را بررسی کنید.";

                var editModel =
                    await _stoneService.GetEditModelAsync(
                        id,
                        sellerId.Value);

                if (editModel == null)
                    return NotFound();

                editModel.Description = model.Description;
                editModel.Price = model.Price;

                editModel.StoneTypeId = model.StoneTypeId;

                editModel.CommercialNameId =
                    model.CommercialNameId;

                editModel.ColorIds = model.ColorIds;

                editModel.ThicknessId =
                    model.ThicknessId;

                editModel.MinLength = model.MinLength;
                editModel.MaxLength = model.MaxLength;
                editModel.MinWidth = model.MinWidth;
                editModel.MaxWidth = model.MaxWidth;

                editModel.MineId =
                    model.MineId;

                editModel.ExtractionProvinceId =
                    model.ExtractionProvinceId;

                editModel.ProcessingTypeIds =
                    model.ProcessingTypeIds;

                editModel.StorageProvinceId =
                    model.StorageProvinceId;

                editModel.Location =
                    model.Location;

                editModel.InventoryMin =
                    model.InventoryMin;

                editModel.InventoryMax =
                    model.InventoryMax;

                editModel.QualityGradeId =
                    model.QualityGradeId;

                editModel.ApplicationIds =
                    model.ApplicationIds;

                // تصاویر جدیدی که کاربر انتخاب کرده
                editModel.NewImages =
                    model.NewImages;

                // تصاویر موجود را از دیتابیس نگه می‌داریم
                // و جایگزین نمی‌کنیم.

                // تصاویری که کاربر برای حذف انتخاب کرده
                editModel.DeletedImageIds =
                    model.DeletedImageIds;

                return View(editModel);
            }

            var result =
                await _stoneService.UpdateForSellerAsync(
                    id,
                    model,
                    sellerId.Value);

            if (!result)
                return NotFound();

            TempData["SuccessMessage"] =
                "سنگ با موفقیت ویرایش شد.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var sellerId =
                await _sellerService
                    .GetSellerIdByUserIdAsync(CurrentUserId);

            if (sellerId == null)
                return Forbid();

            var result =
                await _stoneService.DeleteForSellerAsync(
                    id,
                    sellerId.Value);

            if (!result)
                return NotFound();

            TempData["SuccessMessage"] =
                "سنگ با موفقیت حذف شد.";

            return RedirectToAction(nameof(Index));
        }
    }
}
