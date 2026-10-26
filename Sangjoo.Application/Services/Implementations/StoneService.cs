using Sangjoo.Application.DTOs.AdminSide.Stones;
using Sangjoo.Application.Services.Interfaces;
using Sangjoo.Domain.Entities;
using Sangjoo.Domain.RepositoryInterfaces;
using Sangjoo.Domain.UnitOfWork;

namespace Sangjoo.Application.Services.Implementations;

public class StoneService : IStoneService
{
    private readonly IStoneRepository _stoneRepository;
    private readonly IStoneTypeRepository _stoneTypeRepository;
    private readonly ICommercialNameRepository _commercialNameRepository;
    private readonly IColorRepository _colorRepository;
    private readonly IThicknessRepository _thicknessRepository;
    private readonly IMineRepository _mineRepository;
    private readonly IProvinceRepository _provinceRepository;
    private readonly IProcessingTypeRepository _processingTypeRepository;
    private readonly IQualityGradeRepository _qualityGradeRepository;
    private readonly IStoneApplicationRepository _stoneApplicationRepository;
    private readonly IImageService _imageService;
    private readonly IUnitOfWork _unitOfWork;

    public StoneService(
        IStoneRepository stoneRepository,
        IStoneTypeRepository stoneTypeRepository,
        ICommercialNameRepository commercialNameRepository,
        IColorRepository colorRepository,
        IThicknessRepository thicknessRepository,
        IMineRepository mineRepository,
        IProvinceRepository provinceRepository,
        IProcessingTypeRepository processingTypeRepository,
        IQualityGradeRepository qualityGradeRepository,
        IStoneApplicationRepository stoneApplicationRepository,
        IImageService imageService,
        IUnitOfWork unitOfWork)
    {
        _stoneRepository = stoneRepository;
        _stoneTypeRepository = stoneTypeRepository;
        _commercialNameRepository = commercialNameRepository;
        _colorRepository = colorRepository;
        _thicknessRepository = thicknessRepository;
        _mineRepository = mineRepository;
        _provinceRepository = provinceRepository;
        _processingTypeRepository = processingTypeRepository;
        _qualityGradeRepository = qualityGradeRepository;
        _stoneApplicationRepository = stoneApplicationRepository;
        _imageService = imageService;
        _unitOfWork = unitOfWork;
    }


    // =========================================================
    // CREATE
    // =========================================================

    public async Task<StoneDTO> GetCreateModelAsync()
    {
        var model = new StoneDTO();

        await FillStoneListsAsync(model);

        return model;
    }


    public async Task CreateAsync(StoneDTO model)
    {
        var stone = new Stone
        {
            Description = model.Description,
            Price = model.Price,

            StoneTypeId = model.StoneTypeId,
            /*SellerId = model.SellerId,*/
            CommercialNameId = model.CommercialNameId,
            ThicknessId = model.ThicknessId,
            MinLength = model.MinLength,
            MaxLength = model.MaxLength,
            MinWidth = model.MinWidth,
            MaxWidth = model.MaxWidth,
            MineId = model.MineId,
            ExtractionProvinceId = model.ExtractionProvinceId,
            StorageProvinceId = model.StorageProvinceId,

            Location = model.Location,

            InventoryMin = model.InventoryMin,
            InventoryMax = model.InventoryMax,

            QualityGradeId = model.QualityGradeId
        };


        // Applications
        if (model.ApplicationIds != null &&
            model.ApplicationIds.Any())
        {
            var applications =
                await _stoneApplicationRepository.GetAllAsync();

            var selectedApplications = applications
                .Where(x => model.ApplicationIds.Contains(x.Id))
                .ToList();

            foreach (var application in selectedApplications)
            {
                stone.Applications.Add(application);
            }
        }

        //colors
        if (model.ColorIds.Any())
        {
            var colors = await _colorRepository.GetAllAsync();

            var selectedColors = colors
                .Where(x => model.ColorIds.Contains(x.Id))
                .ToList();

            foreach (var color in selectedColors)
            {
                stone.Colors.Add(color);
            }
        }

        // Processing Types
        if (model.ProcessingTypeIds != null &&
            model.ProcessingTypeIds.Any())
        {
            var processingTypes =
                await _processingTypeRepository.GetAllAsync();

            var selectedProcessingTypes = processingTypes
                .Where(x => model.ProcessingTypeIds.Contains(x.Id))
                .ToList();

            foreach (var processingType in selectedProcessingTypes)
            {
                stone.ProcessingTypes.Add(processingType);
            }
        }

        // Stone
        await _stoneRepository.AddAsync(stone);

        await _unitOfWork.SaveChangesAsync();


        // Images
        if (model.Images != null &&
            model.Images.Any())
        {
            foreach (var image in model.Images)
            {
                await _imageService.AddImageAsync(
                    image,
                    stone.Id);
            }
        }
    }


    // =========================================================
    // SELLER STONES
    // =========================================================

    public async Task<List<Stone>> GetSellerStonesAsync(
        long sellerId)
    {
        return await _stoneRepository
            .GetListOfStonesBySellerIdAsync(sellerId);
    }


    public async Task<Stone?> GetSellerStoneAsync(
        long stoneId,
        long sellerId)
    {
        return await _stoneRepository
            .GetByIdAndSellerIdAsync(
                stoneId,
                sellerId);
    }


    // =========================================================
    // CREATE FOR SELLER
    // =========================================================

    public async Task CreateForSellerAsync(
        StoneDTO model,
        long sellerId)
    {
        // Validate selected colors
        if (model.ColorIds != null && model.ColorIds.Count > 3)
        {
            throw new ArgumentException(
                "حداکثر می‌توانید ۳ رنگ برای سنگ انتخاب کنید.");
        }

        var stone = new Stone
        {
            Description = model.Description,
            Price = model.Price,

            StoneTypeId = model.StoneTypeId,

            // SellerId از CurrentUser تعیین می‌شود
            SellerId = sellerId,

            CommercialNameId = model.CommercialNameId,
            /*ColorId = model.ColorId,*/
            ThicknessId = model.ThicknessId,
            MinLength = model.MinLength,
            MaxLength = model.MaxLength,
            MinWidth = model.MinWidth,
            MaxWidth = model.MaxWidth,
            MineId = model.MineId,
            ExtractionProvinceId = model.ExtractionProvinceId,
            StorageProvinceId = model.StorageProvinceId,

            Location = model.Location,

            InventoryMin = model.InventoryMin,
            InventoryMax = model.InventoryMax,

            QualityGradeId = model.QualityGradeId
        };


        // Applications
        if (model.ApplicationIds != null &&
              model.ApplicationIds.Any())
        {
            var applications =
            await _stoneApplicationRepository.GetAllAsync();

            foreach (var applicationId in model.ApplicationIds)
            {
                var application = applications
                    .FirstOrDefault(x => x.Id == applicationId);

                if (application != null)
                {
                    stone.Applications.Add(application);
                }
            }
        }
            //colors
            if (model.ColorIds.Any())
            {
                var colors = await _colorRepository.GetAllAsync();

                var selectedColors = colors
                    .Where(x => model.ColorIds.Contains(x.Id))
                    .ToList();

                foreach (var color in selectedColors)
                {
                    stone.Colors.Add(color);
                }
            }

        // Processing Types
        if (model.ProcessingTypeIds != null &&
            model.ProcessingTypeIds.Any())
        {
            var processingTypes =
                await _processingTypeRepository.GetAllAsync();

            var selectedProcessingTypes = processingTypes
                .Where(x => model.ProcessingTypeIds.Contains(x.Id))
                .ToList();

            foreach (var processingType in selectedProcessingTypes)
            {
                stone.ProcessingTypes.Add(processingType);
            }
        }

        // Stone
        await _stoneRepository.AddAsync(stone);

        await _unitOfWork.SaveChangesAsync();


        // Images
        if (model.Images != null &&
            model.Images.Any())
        {
            foreach (var image in model.Images)
            {
                await _imageService.AddImageAsync(
                    image,
                    stone.Id);
            }
        }
    }


    // =========================================================
    // EDIT
    // =========================================================

    public async Task<EditStoneDTO?> GetEditModelAsync(
        long stoneId,
        long sellerId)
    {
        // گرفتن سنگ فقط در صورتی که متعلق به همین Seller باشد
        var stone = await _stoneRepository
            .GetByIdAndSellerIdAsync(
                stoneId,
                sellerId);

        if (stone == null)
            return null;


        var model = new EditStoneDTO();


        // -----------------------------------------------------
        // Dropdown Lists
        // -----------------------------------------------------

        await FillEditStoneListsAsync(model);


        // -----------------------------------------------------
        // Stone Information
        // -----------------------------------------------------

        model.Description = stone.Description;
        model.Price = stone.Price;

        model.StoneTypeId = stone.StoneTypeId;

        /*model.SellerId = stone.SellerId;*/

        model.CommercialNameId =
            stone.CommercialNameId;

        model.ThicknessId =
            stone.ThicknessId;

        model.MinLength = stone.MinLength;
        model.MaxLength = stone.MaxLength;
        model.MinWidth = stone.MinWidth;
        model.MaxWidth = stone.MaxWidth;

        model.MineId =
            stone.MineId;

        model.ExtractionProvinceId =
            stone.ExtractionProvinceId;


        model.StorageProvinceId =
            stone.StorageProvinceId;

        model.Location =
            stone.Location;

        model.InventoryMin =
            stone.InventoryMin;

        model.InventoryMax =
            stone.InventoryMax;

        model.QualityGradeId =
            stone.QualityGradeId;


        // -----------------------------------------------------
        // Applications
        // -----------------------------------------------------

        model.ApplicationIds = stone.Applications
            .Select(x => x.Id)
            .ToList();

        // -----------------------------------------------------
        // Colors
        // -----------------------------------------------------
        model.ColorIds = stone.Colors
           .Select(x => x.Id)
           .ToList();

        // -----------------------------------------------------
        // ProcessingTypes
        // -----------------------------------------------------
        model.ProcessingTypeIds = stone.ProcessingTypes
    .Select(x => x.Id)
    .ToList();

        // -----------------------------------------------------
        // Existing Images
        // -----------------------------------------------------

        model.ExistingImages = stone.Images
            .Select(x => new StoneImageDTO
            {
                Id = x.Id,
                ImageName = x.ImageName,
                IsMainImage = x.IsMainImage
            })
            .ToList();


        return model;
    }


    // =========================================================
    // UPDATE FOR SELLER
    // =========================================================

    public async Task<bool> UpdateForSellerAsync(
        long stoneId,
        EditStoneDTO model,
        long sellerId)
    {
        // Validate selected colors
        if (model.ColorIds != null && model.ColorIds.Count > 3)
        {
            throw new ArgumentException(
                "حداکثر می‌توانید ۳ رنگ برای سنگ انتخاب کنید.");
        }
        // گرفتن سنگ با کنترل مالکیت
        var stone = await _stoneRepository
            .GetByIdAndSellerIdAsync(
                stoneId,
                sellerId);

        if (stone == null)
            return false;


        // -----------------------------------------------------
        // Stone Information
        // -----------------------------------------------------

        stone.Description =
            model.Description;

        stone.Price =
            model.Price;

        stone.StoneTypeId =
            model.StoneTypeId;

        stone.CommercialNameId =
            model.CommercialNameId;

        stone.ThicknessId =
            model.ThicknessId;

        stone.MinLength = model.MinLength;
        stone.MaxLength = model.MaxLength;
        stone.MinWidth = model.MinWidth;
        stone.MaxWidth = model.MaxWidth;

        stone.MineId =
            model.MineId;

        stone.ExtractionProvinceId =
            model.ExtractionProvinceId;

        stone.StorageProvinceId =
            model.StorageProvinceId;

        stone.Location =
            model.Location;

        stone.InventoryMin =
            model.InventoryMin;

        stone.InventoryMax =
            model.InventoryMax;

        stone.QualityGradeId =
            model.QualityGradeId;

        
        // -----------------------------------------------------
        // Applications
        // -----------------------------------------------------

        stone.Applications.Clear();

        var applications =
            await _stoneApplicationRepository.GetAllAsync();
        if (model.ApplicationIds != null &&
                   model.ApplicationIds.Any())
        {
            foreach (var applicationId in model.ApplicationIds)
        {
            var application = applications
                .FirstOrDefault(x => x.Id == applicationId);

            if (application != null)
            {
                stone.Applications.Add(application);
            }
          }
        }

        // -----------------------------------------------------
        // Colors
        // -----------------------------------------------------
        stone.Colors.Clear();

        if (model.ColorIds.Any())
        {
            var colors = await _colorRepository.GetAllAsync();

            var selectedColors = colors
                .Where(x => model.ColorIds.Contains(x.Id))
                .ToList();

            foreach (var color in selectedColors)
            {
                stone.Colors.Add(color);
            }
        }

        // -----------------------------------------------------
        // Processing Types
        // -----------------------------------------------------

        stone.ProcessingTypes.Clear();

        if (model.ProcessingTypeIds != null &&
            model.ProcessingTypeIds.Any())
        {
            var processingTypes =
                await _processingTypeRepository.GetAllAsync();

            var selectedProcessingTypes = processingTypes
                .Where(x => model.ProcessingTypeIds.Contains(x.Id))
                .ToList();

            foreach (var processingType in selectedProcessingTypes)
            {
                stone.ProcessingTypes.Add(processingType);
            }
        }
        // -----------------------------------------------------
        // Update Stone
        // -----------------------------------------------------

        await _stoneRepository.UpdateAsync(stone);

        await _unitOfWork.SaveChangesAsync();


        // -----------------------------------------------------
        // Add New Images
        // -----------------------------------------------------

        if (model.NewImages != null &&
            model.NewImages.Any())
        {
            foreach (var image in model.NewImages)
            {
                await _imageService.AddImageAsync(
                    image,
                    stone.Id);
            }
        }

        // حذف تصاویر انتخاب‌شده
        if (model.DeletedImageIds != null &&
        model.DeletedImageIds.Any())
        {
            foreach (var imageId in model.DeletedImageIds)
            {
                await _imageService.DeleteImageAsync(
                    imageId,
                    stone.Id);
            }
        }


        return true;
    }


    // =========================================================
    // DELETE STONE FOR SELLER
    // =========================================================

    public async Task<bool> DeleteForSellerAsync(
        long stoneId,
        long sellerId)
    {
        var stone = await _stoneRepository
            .GetByIdAndSellerIdAsync(
                stoneId,
                sellerId);

        if (stone == null)
            return false;


        // حذف تمام تصاویر مربوط به محصول
        await _imageService
            .DeleteImagesByProductIdAsync(stone.Id);


        // حذف Stone
        await _stoneRepository.DeleteAsync(stone);

        await _unitOfWork.SaveChangesAsync();


        return true;
    }


    // =========================================================
    // PRIVATE METHODS
    // =========================================================

    private async Task FillStoneListsAsync(
        StoneDTO model)
    {
        model.Types =
            await _stoneTypeRepository.GetAllAsync();

        model.CommercialNames =
            await _commercialNameRepository.GetAllAsync();

        model.Colors =
            await _colorRepository.GetAllAsync();

        model.Thicknesses =
            await _thicknessRepository.GetAllAsync();

        model.Mines =
            await _mineRepository.GetAllAsync();

        var provinces = await _provinceRepository.GetAllAsync();

        model.Provinces = provinces
            .Where(p => p.IsActive)
            .OrderBy(p => p.Name)
            .ToList();

        model.ProcessingTypes =
            await _processingTypeRepository.GetAllAsync();

        model.QualityGrades =
            (await _qualityGradeRepository.GetAllAsync())
               .Where(x => x.IsActive)
               .OrderBy(x => x.SortOrder)
               .ToList();

        model.StoneApplications =
            await _stoneApplicationRepository.GetAllAsync();
    }


    private async Task FillEditStoneListsAsync(
        EditStoneDTO model)
    {
        model.Types =
            await _stoneTypeRepository.GetAllAsync();

        model.CommercialNames =
            await _commercialNameRepository.GetAllAsync();

        model.Colors =
            await _colorRepository.GetAllAsync();

        model.Thicknesses =
            await _thicknessRepository.GetAllAsync();

        model.Mines =
            await _mineRepository.GetAllAsync();

        var provinces = await _provinceRepository.GetAllAsync();

        model.Provinces = provinces
            .Where(p => p.IsActive)
            .OrderBy(p => p.Name)
            .ToList();

        model.ProcessingTypes =
            await _processingTypeRepository.GetAllAsync();

        model.QualityGrades =
            (await _qualityGradeRepository.GetAllAsync())
                  .Where(x => x.IsActive)
                  .OrderBy(x => x.SortOrder)
                  .ToList();

        model.StoneApplications =
            await _stoneApplicationRepository.GetAllAsync();
    }

    public async Task<StoneDetailsDTO?> GetStoneDetailsAsync(long stoneId)
    {
        var stone = await _stoneRepository
            .GetStoneDetailsByIdAsync(stoneId);

        if (stone == null)
            return null;

        return new StoneDetailsDTO
        {
            Id = stone.Id,

            // اطلاعات اصلی
            CommercialName = stone.CommercialName?.Name ?? string.Empty,
            Description = stone.Description,
            Price = stone.Price,

            // مشخصات سنگ
            StoneType = stone.StoneType?.Name ?? string.Empty,

            Colors = stone.Colors
                .Select(x => x.Name)
                .ToList(),

            Thickness = stone.Thickness.Value.ToString(),

            MinLength = stone.MinLength,
            MaxLength = stone.MaxLength,
            MinWidth = stone.MinWidth,
            MaxWidth = stone.MaxWidth,

            Mine = stone.Mine?.Name ?? string.Empty,
            ExtractionProvince = stone.ExtractionProvince?.Name ?? string.Empty,

            ProcessingTypes = stone.ProcessingTypes
                .Select(x => x.Name)
                .ToList(),

            StorageProvince = stone.StorageProvince?.Name ?? string.Empty,
            Location = stone.Location.ToString(),

            InventoryMin = stone.InventoryMin,
            InventoryMax = stone.InventoryMax,

            QualityGrade = stone.QualityGrade?.Name ?? string.Empty,

            Applications = stone.Applications
                .Select(x => x.Name)
                .ToList(),

            // تصاویر
            Images = stone.Images
                .Select(x => new StoneDetailsImageDTO
                {
                    Id = x.Id,
                    ImageName = x.ImageName,
                    IsMainImage = x.IsMainImage
                })
                .ToList(),

            // فروشنده
            SellerName = stone.Seller?.Name ?? string.Empty
        };
    }
}