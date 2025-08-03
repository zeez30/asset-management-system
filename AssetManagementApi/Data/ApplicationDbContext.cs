using Microsoft.EntityFrameworkCore;
using AssetManagementApi.Models; 

namespace AssetManagementApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets  tables
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetDocument> AssetDocuments { get; set; }
        public DbSet<Asset3DModel> Asset3DModels { get; set; }
        public DbSet<Asset2DModel> Asset2DModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<AssetDocument>()
                .HasOne(ad => ad.Asset)
                .WithMany(a => a.Documents)
                .HasForeignKey(ad => ad.TagNumber);

            modelBuilder.Entity<Asset3DModel>()
                .HasOne(m3d => m3d.Asset)
                .WithMany(a => a.ThreeDModels)
                .HasForeignKey(m3d => m3d.TagNumber);

            modelBuilder.Entity<Asset2DModel>()
                .HasOne(m2d => m2d.Asset)
                .WithMany(a => a.TwoDModels)
                .HasForeignKey(m2d => m2d.TagNumber);
        }
    }
}