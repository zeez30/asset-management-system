using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AssetManagementApi.Models
{
    public class Asset
    {
        [Key]
        public string? TagNumber { get; set; }
        public string? AssetName { get; set; }
        public string? Description { get; set; }
        public string? AssetType { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? SerialNumber { get; set; }
        public string? Size { get; set; }
        public int? Age { get; set; }
        public DateTime? InstallationDate { get; set; }
        public string? Status { get; set; }
        public string? ValidationStatus { get; set; }
        public DateTime? LastMaintenance { get; set; }
        public string? Site { get; set; }
        public string? DeckPlatform { get; set; }
        public string? AreaCode { get; set; }
        public string? System { get; set; }
        public string? FacilitySection { get; set; }
        public string? FunctionalClassID { get; set; }
        public string? Subsystem { get; set; }
        public string? CMMMSRequired { get; set; }
        public string? TagFormatID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties for related data
        [JsonIgnore]
        public virtual ICollection<AssetDocuments> AssetDocuments { get; set; } = new List<AssetDocuments>();

        [JsonIgnore]
        public virtual ICollection<Asset2DModels> Asset2DModels { get; set; } = new List<Asset2DModels>();

        [JsonIgnore]
        public virtual ICollection<Asset3DModels> Asset3DModels { get; set; } = new List<Asset3DModels>();

        [JsonIgnore]
        public virtual ICollection<AssetRelationship> AssetRelationships { get; set; } = new List<AssetRelationship>();
    }
}