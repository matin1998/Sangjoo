using Sangjoo.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sangjoo.Domain.Enums;

namespace Sangjoo.Application.DTOs.AdminSide.Stones;

public class EditStoneDTO : IValidatableObject
{
    public long Id { get; set; }
    [Required(ErrorMessage = "وارد کردن توضیحات الزامی است.")]
    public string? Description { get; set; }

    [Range(1, double.MaxValue, ErrorMessage = "قیمت باید بیشتر از صفر باشد.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "انتخاب نام تجاری الزامی است.")]
    [Range(1, long.MaxValue, ErrorMessage = "انتخاب نام تجاری الزامی است.")]
    public CommercialName? CommercialName { get; set; }

    [Required(ErrorMessage = "انتخاب نوع سنگ الزامی است.")]
    [Range(1, long.MaxValue, ErrorMessage = "انتخاب نوع سنگ الزامی است.")]
    public long StoneTypeId { get; set; }

    /*public long SellerId { get; set; }*/
    [Required(ErrorMessage = "انتخاب نام تجاری الزامی است.")]
    public long CommercialNameId { get; set; }

    [Required(ErrorMessage = "حداقل یک رنگ را انتخاب کنید.")]
    [MinLength(1, ErrorMessage = "حداقل یک رنگ را انتخاب کنید.")]
    public List<long> ColorIds { get; set; }
= new List<long>();

    [Required(ErrorMessage = "انتخاب ضخامت الزامی است.")]
    [Range(1, long.MaxValue, ErrorMessage = "انتخاب ضخامت الزامی است.")]
    public long ThicknessId { get; set; }

    [Required(ErrorMessage = "وارد کردن حداقل طول الزامی است.")]
    [Range(1, int.MaxValue, ErrorMessage = "حداقل طول باید عددی مثبت باشد.")]
    public int MinLength { get; set; }

    [Required(ErrorMessage = "وارد کردن حداکثر طول الزامی است.")]
    [Range(1, int.MaxValue, ErrorMessage = "حداکثر طول باید عددی مثبت باشد.")]
    public int MaxLength { get; set; }

    [Required(ErrorMessage = "وارد کردن حداقل عرض الزامی است.")]
    [Range(1, int.MaxValue,
        ErrorMessage = "حداقل عرض باید عددی مثبت باشد.")]
    public int MinWidth { get; set; }

    [Required(ErrorMessage = "وارد کردن حداکثر عرض الزامی است.")]
    [Range(1, int.MaxValue, ErrorMessage = "حداکثر عرض باید عددی مثبت باشد.")]
    public int MaxWidth { get; set; }

    [Required(ErrorMessage = "انتخاب معدن الزامی است.")]
    [Range(1, long.MaxValue, ErrorMessage = "انتخاب معدن الزامی است.")]
    public long MineId { get; set; }
    [Required(ErrorMessage = "انتخاب استان استخراج الزامی است.")]
    [Range(1, long.MaxValue, ErrorMessage = "انتخاب استان استخراج الزامی است.")]
    public long ExtractionProvinceId { get; set; }

    [Required(ErrorMessage = "حداقل یک نوع فرآوری را انتخاب کنید.")]
    [MinLength(1, ErrorMessage = "حداقل یک نوع فرآوری را انتخاب کنید.")]
    public List<long> ProcessingTypeIds { get; set; }
    = new List<long>();
    [Required(ErrorMessage = "انتخاب استان محل نگهداری الزامی است.")]
    [Range(1, long.MaxValue, ErrorMessage = "انتخاب استان محل نگهداری الزامی است.")]
    public long StorageProvinceId { get; set; }
    [Required(ErrorMessage = "انتخاب محل نگهداری الزامی است.")]
    public StoneLocation Location { get; set; }

    [Range(1, int.MaxValue,
ErrorMessage = "حداقل موجودی باید عدد صحیح مثبت باشد.")]
    public int InventoryMin { get; set; }

    [Range(1, int.MaxValue,
        ErrorMessage = "حداکثر موجودی باید عدد صحیح مثبت باشد.")]
    public int InventoryMax { get; set; }

    [Required(ErrorMessage = "انتخاب درجه کیفیت الزامی است.")]
    [Range(1, long.MaxValue, ErrorMessage = "انتخاب درجه کیفیت الزامی است.")]
    public long QualityGradeId { get; set; }
    [Required(ErrorMessage = "حداقل یک مورد کاربرد انتخاب کنید.")]
    [MinLength(1, ErrorMessage = "حداقل یک مورد کاربرد انتخاب کنید.")]
    public List<long> ApplicationIds { get; set; }
        = new List<long>();
    // تصاویر فعلی
    public List<StoneImageDTO> ExistingImages { get; set; } = new();

    // تصاویر جدید
    public List<IFormFile> NewImages { get; set; } = new();

    // تصاویر انتخاب‌شده برای حذف
    public List<long> DeletedImageIds { get; set; } = new();
    public List<StoneType> Types { get; set; } = new List<StoneType>();
    public List<Color> Colors { get; set; } = new List<Color>();
    public List<CommercialName> CommercialNames { get; set; } = new List<CommercialName>();
    public List<Mine> Mines { get; set; } = new List<Mine>();
    public List<ProcessingType> ProcessingTypes { get; set; } = new List<ProcessingType>();
    public List<Province> Provinces { get; set; } = new List<Province>();
    public List<QualityGrade> QualityGrades { get; set; } = new List<QualityGrade>();
    /*public List<Seller> Sellers { get; set; } = new List<Seller>();*/
    public List<StoneApplication> StoneApplications { get; set; } = new List<StoneApplication>();
    public List<Thickness> Thicknesses { get; set; } = new List<Thickness>();

    public IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
    {
        if (MinLength > MaxLength &&
            MinLength > 0 &&
            MaxLength > 0)
        {
            yield return new ValidationResult(
                "حداقل طول نمی‌تواند از حداکثر طول بیشتر باشد.",
                new[] { nameof(MinLength), nameof(MaxLength) });
        }

        if (MinWidth > MaxWidth &&
            MinWidth > 0 &&
            MaxWidth > 0)
        {
            yield return new ValidationResult(
                "حداقل عرض نمی‌تواند از حداکثر عرض بیشتر باشد.",
                new[] { nameof(MinWidth), nameof(MaxWidth) });
        }

        if (InventoryMin > InventoryMax)
        {
            yield return new ValidationResult(
                "حداقل موجودی نمی‌تواند از حداکثر موجودی بیشتر باشد.",
                new[] { nameof(InventoryMin), nameof(InventoryMax) });
        }
    }
}
