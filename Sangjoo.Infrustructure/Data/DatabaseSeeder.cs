using Microsoft.EntityFrameworkCore;
using Sangjoo.Domain.Entities;
using Sangjoo.Infrustructure.SangjooStoreDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Infrustructure.Data;

public static class DatabaseSeeder
{
    private static readonly string[] ProvinceNames =
    {
    "آذربایجان شرقی",
    "آذربایجان غربی",
    "اردبیل",
    "اصفهان",
    "البرز",
    "ایلام",
    "بوشهر",
    "تهران",
    "چهارمحال و بختیاری",
    "خراسان جنوبی",
    "خراسان رضوی",
    "خراسان شمالی",
    "خوزستان",
    "زنجان",
    "سمنان",
    "سیستان و بلوچستان",
    "فارس",
    "قزوین",
    "قم",
    "کردستان",
    "کرمان",
    "کرمانشاه",
    "کهگیلویه و بویراحمد",
    "گلستان",
    "گیلان",
    "لرستان",
    "مازندران",
    "مرکزی",
    "هرمزگان",
    "همدان",
    "یزد"
};
    private static readonly string[] colorNames =
        {
        "سفید",
        "استخوانی",
        "صدفی",
        "کرم",
        "بژ",
        "نسکافه‌ای",
        "قهوه‌ای",
        "شکلاتی",
        "طوسی",
        "خاکستری",
        "دودی",
        "مشکی",
        "قرمز",
        "صورتی",
        "گلبهی",
        "زرد",
        "طلایی",
        "نارنجی",
        "سبز",
        "آبی",
        "فیروزه‌ای",
        "بنفش",
        "نقره‌ای"
    };

    public static async Task SeedAsync(SangjooDbContext context)
    {
        var existingNames = await context.Provinces
            .Select(p => p.Name)
            .ToListAsync();

        var existingNamesSet = new HashSet<string>(
            existingNames,
            StringComparer.OrdinalIgnoreCase);

        var provincesToAdd = ProvinceNames
            .Where(name => !existingNamesSet.Contains(name))
            .Select(name => new Province
            {
                Name = name,
                IsActive = true
            })
            .ToList();

        if (provincesToAdd.Count == 0)
            return;

        await context.Provinces.AddRangeAsync(provincesToAdd);
        await context.SaveChangesAsync();
    }
    public static async Task SeedColorsAsync(SangjooDbContext context)
    {
       /* string[] colorNames =
        {
        "سفید",
        "استخوانی",
        "صدفی",
        "کرم",
        "بژ",
        "نسکافه‌ای",
        "قهوه‌ای",
        "شکلاتی",
        "طوسی",
        "خاکستری",
        "دودی",
        "مشکی",
        "قرمز",
        "صورتی",
        "گلبهی",
        "زرد",
        "طلایی",
        "نارنجی",
        "سبز",
        "آبی",
        "فیروزه‌ای",
        "بنفش",
        "نقره‌ای"
    };*/

        var existingColors = await context.Colors
            .Select(c => c.Name)
            .ToListAsync();

        var existingNames = new HashSet<string>(
            existingColors,
            StringComparer.OrdinalIgnoreCase);

        var newColors = colorNames
            .Where(name => !existingNames.Contains(name))
            .Select(name => new Color
            {
                Name = name,
                IsActive = true
            })
            .ToList();

        if (newColors.Count == 0)
            return;

        await context.Colors.AddRangeAsync(newColors);
        await context.SaveChangesAsync();
    }
}
