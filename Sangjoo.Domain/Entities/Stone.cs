using Sangjoo.Domain.Entities;
using Sangjoo.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace Sangjoo.Domain.Entities;

public class Stone:Product
{

        // نوع سنگ
        public long StoneTypeId { get; set; }
        public StoneType StoneType { get; set; } = null!;

        // مجموعه فروشنده
        public long SellerId { get; set; }
        public Seller Seller { get; set; } = null!;

        // اسم تجاری
        public long CommercialNameId { get; set; }
        public CommercialName? CommercialName { get; set; }

    // رنگ‌ها
    public ICollection<Color> Colors { get; set; }
    = new List<Color>();

    // ضخامت‌ها
    public long ThicknessId { get; set; }
    public Thickness Thickness { get; set; } = null!;

    // ابعاد
    public int MinLength { get; set; }

    public int MaxLength { get; set; }

    public int MinWidth { get; set; }

    public int MaxWidth { get; set; }

    // معدن
    public long MineId { get; set; }
        public Mine? Mine { get; set; }

        // استان محل استخراج
        public long ExtractionProvinceId { get; set; }
        public Province? ExtractionProvince { get; set; }

    // نوع فرآوری
    public ICollection<ProcessingType> ProcessingTypes { get; set; }
    = new List<ProcessingType>();

    // استان محل استقرار
    public long StorageProvinceId { get; set; }
        public Province StorageProvince { get; set; } = null!;

        // انبار / سنگبری
        public StoneLocation Location { get; set; }

        // موجودی
        public int InventoryMin { get; set; }
        public int InventoryMax { get; set; }

        // درجه کیفی
        public long QualityGradeId { get; set; }
        public QualityGrade? QualityGrade { get; set; }

        // مناسب برای
        public ICollection<StoneApplication> Applications { get; set; }
            = new List<StoneApplication>();

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
}
