using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementApi.Models
{
    // Represents a 2D model file associated with an asset.
    public class Asset2DModels
    {
        // The [Key] attribute designates this property as the primary key.
        public int Id { get; set; }
        
        // The path where the 2D model file is stored.
        public string? FilePath { get; set; }
        
        // The original name of the file uploaded by the user.
        public string? OriginalFileName { get; set; }

        // Foreign key to link this 2D model back to its main Asset.
        public string? AssetTagNumber { get; set; }

        // [ForeignKey] attribute specifies the foreign key property name.
        // The 'virtual' keyword enables lazy loading, which is a feature of Entity Framework.
        public virtual Asset? Asset { get; set; }
    }
}