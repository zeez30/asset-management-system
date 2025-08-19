using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AssetManagementApi.Models
{
    // Represents the main 'Asset' entity in the database.
    public class Asset
    {
        // The [Key] attribute designates this property as the primary key.
        // The [StringLength] attribute sets a maximum length for the string data.
        [Key]
        [StringLength(50)]
        public string? TagNumber { get; set; }

        // The [Required] attribute indicates that this field must have a value.
        [Required]
        [StringLength(255)]
        public string? AssetName { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [StringLength(100)]
        public string? AssetType { get; set; }

        [StringLength(100)]
        public string? Manufacturer { get; set; }

        [StringLength(100)]
        public string? Model { get; set; }

        [StringLength(100)]
        public string? SerialNumber { get; set; }

        [StringLength(50)]
        public string? Size { get; set; }

        public DateTime? InstallationDate { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(50)]
        public string? ValidationStatus { get; set; }

        [StringLength(50)]
        public string? Site { get; set; }

        [StringLength(50)]
        public string? DeckPlatform { get; set; }

        [StringLength(50)]
        public string? AreaCode { get; set; }

        [StringLength(20)]
        public string? System { get; set; }

        [StringLength(50)]
        public string? FacilitySection { get; set; }

        [StringLength(100)]
        public string? FunctionalClassID { get; set; }

        [StringLength(20)]
        public string? Subsystem { get; set; }

        // Nullable boolean for CMMMSRequired
        public bool? CMMMSRequired { get; set; }

        [StringLength(100)]
        public string? TagFormatID { get; set; }

        public DateTime? LastMaintenance { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties represent relationships with other entities.
        // ICollection is used for a one-to-many relationship.
        // This allows an Asset to have multiple associated relationships, documents, and models.
        public ICollection<AssetRelationship> AssetRelationships { get; set; } = new List<AssetRelationship>();
        public ICollection<AssetDocuments> AssetDocuments { get; set; } = new List<AssetDocuments>();
        public ICollection<Asset2DModels> Asset2DModels { get; set; } = new List<Asset2DModels>();
        public ICollection<Asset3DModels> Asset3DModels { get; set; } = new List<Asset3DModels>();
    }
}