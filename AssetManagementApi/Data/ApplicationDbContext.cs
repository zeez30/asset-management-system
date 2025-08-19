using AssetManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementApi.Data
{
    // The main database context class, inheriting from DbContext
    public class ApplicationDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions, required for dependency injection
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet properties for each of the models. These represent tables in the database.
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetRelationship> AssetRelationships { get; set; }
        public DbSet<AssetDocuments> AssetDocuments { get; set; }
        public DbSet<Asset2DModels> Asset2DModels { get; set; }
        public DbSet<Asset3DModels> Asset3DModels { get; set; }

        // This method is used to configure the model and its relationships using the Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the primary key for the 'Asset' entity.
            // The 'TagNumber' property is set as the primary key, as it's a unique identifier.
            modelBuilder.Entity<Asset>()
                .HasKey(a => a.TagNumber);

            // Configure the primary key for the 'AssetRelationship' entity.
            // 'Id' is used as the primary key.
            modelBuilder.Entity<AssetRelationship>()
                .HasKey(ar => ar.Id);

            // Configure the one-to-many relationship between 'Asset' and 'AssetRelationship'.
            // An 'AssetRelationship' has one 'PrimaryAsset'.
            // The 'PrimaryAsset' can have many 'AssetRelationships'.
            // The foreign key is 'PrimaryTagNumber'.
            modelBuilder.Entity<AssetRelationship>()
                .HasOne(ar => ar.PrimaryAsset)
                .WithMany() // .WithMany() here means there is no navigation property on the other side of the relationship.
                .HasForeignKey(ar => ar.PrimaryTagNumber);

            // Configure the primary key and relationship for 'Asset2DModels'.
            // 'Id' is the primary key for this entity.
            modelBuilder.Entity<Asset2DModels>()
                .HasKey(a2d => a2d.Id);

            // Configure the relationship between 'Asset' and 'Asset2DModels'.
            // An 'Asset2DModel' has one 'Asset'.
            // The 'Asset' can have many 'Asset2DModels'.
            // The foreign key is 'AssetTagNumber'.
            modelBuilder.Entity<Asset2DModels>()
                .HasOne(a2d => a2d.Asset)
                .WithMany() // .WithMany() here means there is no navigation property on the other side of the relationship.
                .HasForeignKey(a2d => a2d.AssetTagNumber);

            // Configure the primary key for 'Asset3DModels'.
            // 'Id' is the primary key for this entity.
            modelBuilder.Entity<Asset3DModels>()
                .HasKey(a3d => a3d.Id);
            
            // Note: The relationship for Asset3DModels is not explicitly configured here,
            // but it would follow a similar pattern to Asset2DModels if a navigation
            // property were added to the Asset model.
        }
    }
}