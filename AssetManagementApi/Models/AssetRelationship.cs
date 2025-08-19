using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AssetManagementApi.Models
{
    // Represents a relationship between a primary asset and another asset.
    public class AssetRelationship
    {
        // The primary key for this relationship entry.
        [Key]
        public int Id { get; set; }
        
        // Describes the type of relationship (e.g., "Parent", "Child", "Component").
        public string? RelationshipType { get; set; }
        
        // The tag number of the asset that is related to the primary asset.
        public string? AssociatedTagNumber { get; set; }

        // The foreign key that links this relationship back to the main (primary) asset.
        public string? PrimaryTagNumber { get; set; }
        
        // [JsonIgnore] prevents a circular reference in the JSON output.
        // It's used here to stop the PrimaryAsset navigation property from being
        // serialized when an Asset object is returned.
        [JsonIgnore]
        // [ForeignKey] specifies the property that acts as the foreign key.
        // The 'virtual' keyword is used for lazy loading in Entity Framework.
        [ForeignKey("PrimaryTagNumber")]
        public virtual Asset? PrimaryAsset { get; set; }
    }
}