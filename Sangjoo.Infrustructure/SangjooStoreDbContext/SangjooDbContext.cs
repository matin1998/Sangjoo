using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sangjoo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sangjoo.Infrustructure.SangjooStoreDbContext;
public class SangjooDbContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
{
    #region Ctor
    public SangjooDbContext(DbContextOptions<SangjooDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-DOFAEJ3;Initial Catalog=Sangjoo;Integrated Security=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
        base.OnConfiguring(optionsBuilder);
    }
    #endregion
    public DbSet<Stone> Stones { get; set; }
    public DbSet<StoneType> StoneTypes { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Thickness> Thicknesses { get; set; }
    public DbSet<ProcessingType> ProcessingTypes { get; set; }
    public DbSet<CommercialName> CommercialNames { get; set; }
    public DbSet<Mine> Mines { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<QualityGrade> QualityGrades { get; set; }
    public DbSet<StoneApplication> StoneApplications { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================
        // Stone
        // =========================

        modelBuilder.Entity<Stone>(entity =>
        {

            entity.Property(x => x.Price)
                .HasPrecision(18, 2);

            entity.Property(x => x.InventoryMin)
                .HasPrecision(18, 2);

            entity.Property(x => x.InventoryMax)
                .HasPrecision(18, 2);

            // Stone -> StoneType
            entity.HasOne(x => x.StoneType)
                .WithMany(x => x.Stones)
                .HasForeignKey(x => x.StoneTypeId)
                .OnDelete(DeleteBehavior.Restrict);


            // Stone -> Seller
            entity.HasOne(x => x.Seller)
                .WithMany(x => x.Stones)
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Stone -> CommercialName
            entity.HasOne(x => x.CommercialName)
                .WithMany(x => x.Stones)
                .HasForeignKey(x => x.CommercialNameId)
                .OnDelete(DeleteBehavior.SetNull);

            // Stone -> Mine
            entity.HasOne(x => x.Mine)
                .WithMany(x => x.Stones)
                .HasForeignKey(x => x.MineId)
                .OnDelete(DeleteBehavior.SetNull);

            // Stone -> ExtractionProvince
            entity.HasOne(x => x.ExtractionProvince)
                .WithMany()
                .HasForeignKey(x => x.ExtractionProvinceId)
                .OnDelete(DeleteBehavior.SetNull);

            // Stone -> StorageProvince
            entity.HasOne(x => x.StorageProvince)
                .WithMany()
                .HasForeignKey(x => x.StorageProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Stone -> QualityGrade
            entity.HasOne(x => x.QualityGrade)
                .WithMany(x => x.Stones)
                .HasForeignKey(x => x.QualityGradeId)
                .OnDelete(DeleteBehavior.SetNull);
        });


        // =========================
        // StoneType
        // =========================

        modelBuilder.Entity<StoneType>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(x => x.Name)
                .IsUnique();
            entity.HasIndex(x => x.Slug)
                .IsUnique();
        });


        // =========================
        // Color
        // =========================

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(x => x.Name)
                .IsUnique();
        });


        // =========================
        // Thickness
        // =========================

        modelBuilder.Entity<Thickness>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Value)
                .HasPrecision(10, 1);

            entity.HasIndex(x => x.Value)
                .IsUnique();
        });


        // =========================
        // ProcessingType
        // =========================

        modelBuilder.Entity<ProcessingType>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(x => x.Name)
                .IsUnique();
        });


        // =========================
        // CommercialName
        // =========================

        modelBuilder.Entity<CommercialName>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasIndex(x => x.Name)
                .IsUnique();
        });


        // =========================
        // Mine
        // =========================

        modelBuilder.Entity<Mine>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasIndex(x => x.Name)
                .IsUnique();
        });


        // =========================
        // Province
        // =========================

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(x => x.Name)
                .IsUnique();
        });


        // =========================
        // Seller
        // =========================

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
            entity.HasOne(x => x.User)
        .WithOne(x => x.Seller)
        .HasForeignKey<Seller>(x => x.UserId)
        .OnDelete(DeleteBehavior.Restrict);
        });

        // =========================
        // QualityGrade
        // =========================

        modelBuilder.Entity<QualityGrade>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(x => x.Name)
                .IsUnique();
        });


        // =========================
        // StoneApplication
        // =========================

        modelBuilder.Entity<StoneApplication>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.HasIndex(x => x.Name)
                .IsUnique();
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.Property(x => x.DiscountAmount)
                .HasPrecision(18, 2);
        });


        modelBuilder.Entity<Stone>()
            .HasOne(x => x.Thickness)
            .WithMany(x => x.Stones);



        modelBuilder.Entity<Stone>()
    .HasMany(x => x.ProcessingTypes)
    .WithMany(x => x.Stones)
    .UsingEntity(j => j.ToTable("StoneProcessingTypesMap"));

        // =========================
        // Many-to-Many
        // =========================
        modelBuilder.Entity<Stone>()
            .HasMany(x => x.Applications)
            .WithMany(x => x.Stones)
            .UsingEntity(x => x.ToTable("StoneApplicationsMap"));

        

        modelBuilder.Entity<Image>()
    .ToTable("Image");
    }
}
