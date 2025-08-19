using AssetManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetRelationship> AssetRelationships { get; set; }
        public DbSet<AssetDocuments> AssetDocuments { get; set; }
        public DbSet<Asset2DModels> Asset2DModels { get; set; }
        public DbSet<Asset3DModels> Asset3DModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the primary key for Asset
            modelBuilder.Entity<Asset>()
                .HasKey(a => a.TagNumber);

            // Configure the primary key for AssetRelationship
            modelBuilder.Entity<AssetRelationship>()
                .HasKey(ar => ar.Id);

            // Configure the relationship between Asset and AssetRelationship
            modelBuilder.Entity<AssetRelationship>()
                .HasOne(ar => ar.PrimaryAsset)
                .WithMany()
                .HasForeignKey(ar => ar.PrimaryTagNumber);

            // Configure the primary key and relationship for Asset2DModels
            modelBuilder.Entity<Asset2DModels>()
                .HasKey(a2d => a2d.Id);

            modelBuilder.Entity<Asset2DModels>()
                .HasOne(a2d => a2d.Asset)
                .WithMany()
                .HasForeignKey(a2d => a2d.AssetTagNumber);

            // Configure the primary key for Asset3DModels
            modelBuilder.Entity<Asset3DModels>()
                .HasKey(a3d => a3d.Id);
        }
    }
}