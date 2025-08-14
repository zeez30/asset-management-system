using Microsoft.EntityFrameworkCore;
using AssetManagementApi.Models;

namespace AssetManagementApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetDocuments> AssetDocuments { get; set; }
        public DbSet<Asset3DModels> Asset3DModels { get; set; }
        public DbSet<Asset2DModels> Asset2DModels { get; set; }
        public DbSet<AssetRelationship> AssetRelationships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetRelationship>()
                .HasOne(ar => ar.ParentAsset)
                .WithMany(a => a.AssetRelationships)
                .HasForeignKey(ar => ar.ParentTagNumber);
        }
    }
}