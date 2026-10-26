using Sangjoo.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Sangjoo.Domain.Entities;
using Sangjoo.Infrustructure.SangjooStoreDbContext;
using BookStore.infrastructure.Repositories;
using Sangjoo.Domain.RepositoryInterfaces;
using Sangjoo.Domain.UnitOfWork;
using Sangjoo.Infrustructure.Repositories;
using Sangjoo.Infrustructure.UnitOfWork;
using Sangjoo.Application.Services.Interfaces;
using Sangjoo.infrustructure.Services;
using Sangjoo.Application.Services.Implementations;
using Sangjoo.infrastructure.Identity;
using Sangjoo.Infrustructure.Data;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole<long>>(
            options =>
            {
                // Configure identity options here if needed
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            }

            ).AddEntityFrameworkStores<SangjooDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,
        CustomClaimsPrincipalFactory>();

        // Add services to the container.
        builder.Services.AddScoped<IStoneRepository, StoneRepository>();

        builder.Services.AddScoped<IStoneTypeRepository, StoneTypeRepository>();
        builder.Services.AddScoped<IStoneTypeService, StoneTypeService>();
        builder.Services.AddScoped<ICommercialNameRepository, CommercialNameRepository>();
        builder.Services.AddScoped<ICommercialNameService, CommercialNameService>();
        builder.Services.AddScoped<IColorRepository, ColorRepository>();
        builder.Services.AddScoped<IThicknessRepository, ThicknessRepository>();
        builder.Services.AddScoped<IThicknessService, ThicknessService>();
        builder.Services.AddScoped<IMineRepository, MineRepository>();
        builder.Services.AddScoped<IMineService, MineService>();
        builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();
        builder.Services.AddScoped<IProcessingTypeRepository, ProcessingTypeRepository>();
        builder.Services.AddScoped<IProcessingTypeService, ProcessingTypeService>();
        builder.Services.AddScoped<IQualityGradeRepository, QualityGradeRepository>();
        builder.Services.AddScoped<IQualityGradeService, QualityGradeService>();
        builder.Services.AddScoped<IStoneApplicationRepository, StoneApplicationRepository>();
        builder.Services.AddScoped<IStoneApplicationService, StoneApplicationService>();
        builder.Services.AddScoped<IImageRepository, ImageRepository>();
        builder.Services.AddScoped<IFileService, FileService>();
        builder.Services.AddScoped<ISellerService, SellerService>();
        builder.Services.AddScoped<ISellerRepository, SellerRepository>();
        builder.Services.AddScoped<IImageService, ImageService>();
        builder.Services.AddScoped<IStoneService, StoneService>();
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<SangjooDbContext>();
        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var roleManager =
                scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole<long>>>();
            var userManager =
       scope.ServiceProvider
           .GetRequiredService<UserManager<ApplicationUser>>();


            await IdentitySeeder.SeedRolesAsync(roleManager);
            await IdentitySeeder.SeedAdminAsync(userManager);
        }
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider
                .GetRequiredService<SangjooDbContext>();

            await DatabaseSeeder.SeedAsync(context);
            await DatabaseSeeder.SeedColorsAsync(context);
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        await app.RunAsync();
    }
}
