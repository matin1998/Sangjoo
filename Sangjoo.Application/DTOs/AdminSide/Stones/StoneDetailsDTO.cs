using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Application.DTOs.AdminSide.Stones
{
    public class StoneDetailsDTO
    {
        public long Id { get; set; }

        // اطلاعات اصلی
        public string CommercialName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }

        // مشخصات سنگ
        public string StoneType { get; set; } = string.Empty;
        public List<string> Colors { get; set; } = new();
        public string Thickness { get; set; } = string.Empty;

        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public int MinWidth { get; set; }
        public int MaxWidth { get; set; }

        public string Mine { get; set; } = string.Empty;
        public string ExtractionProvince { get; set; } = string.Empty;
        public List<string> ProcessingTypes { get; set; } = new();

        public string StorageProvince { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public int InventoryMin { get; set; }
        public int InventoryMax { get; set; }

        public string QualityGrade { get; set; } = string.Empty;
        public List<string> Applications { get; set; } = new();

        // تصاویر
        public List<StoneDetailsImageDTO> Images { get; set; } = new();

        // فروشنده
        public string SellerName { get; set; } = string.Empty;
    }

    public class StoneDetailsImageDTO
    {
        public long Id { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public bool IsMainImage { get; set; }
    }
}
