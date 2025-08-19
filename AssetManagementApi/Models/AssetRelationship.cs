using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AssetManagementApi.Models
{
    public class AssetRelationship
    {
        [Key]
        public int Id { get; set; }
        public string? RelationshipType { get; set; }
        public string? AssociatedTagNumber { get; set; }

        // Foreign key to the primary Asset
        public string? PrimaryTagNumber { get; set; }
        
        [JsonIgnore]
        [ForeignKey("PrimaryTagNumber")]
        public virtual Asset? PrimaryAsset { get; set; }
    }
}