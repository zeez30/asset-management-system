using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementApi.Models
{
    public class Asset2DModels
    {
        [Key]
        public int Id { get; set; }
        public string? FilePath { get; set; }
        public string? OriginalFileName { get; set; }

        // Foreign key to link back to the main Asset
        public string? AssetTagNumber { get; set; }

        [ForeignKey("AssetTagNumber")]
        public virtual Asset? Asset { get; set; }
    }
}