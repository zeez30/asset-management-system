using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AssetManagementApi.Models
{
    public class AssetDocuments
    {
        [Key]
        public string? TagNumber { get; set; }
        public string? Title { get; set; }
        public string? FilePath { get; set; }

        // Foreign key to link back to the main Asset
        public string? AssetTagNumber { get; set; }
        public string? OriginalFileName { get; set; } 

        [JsonIgnore]
        [ForeignKey("AssetTagNumber")]
        public virtual Asset? Asset { get; set; }
    }
}