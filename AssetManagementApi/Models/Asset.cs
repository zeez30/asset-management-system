// File: AssetManagementApi/Models/Asset.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagementApi.Models
{
    public class Asset
    {
        [Key]
        [StringLength(50)]
        public string TagNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string AssetName { get; set; } = string.Empty;

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

        public int? Age { get; set; }

        public DateTime? InstallationDate { get; set; }

        [StringLength(50)]
        public string? CurrentStatus { get; set; }

        // --- New Fields from image_eb214e.png ---
        [StringLength(50)]
        public string? AreaCode { get; set; } // e.g., "AA PLATFORM"

        [StringLength(50)]
        public string? DeckPlatformCode { get; set; } // e.g., "ASAAA"

        [StringLength(50)]
        public string? FacilitySector { get; set; } // e.g., "ASA"

        [StringLength(255)]
        public string? ServiceDescription { get; set; } // e.g., "FIREWATER HOSE REEL"

        [StringLength(50)]
        public string? Site { get; set; } // e.g., "AS"

        [StringLength(20)]
        public string? Subsystem { get; set; } // e.g., "04"

        [StringLength(20)]
        public string? System { get; set; } // e.g., "SASY"

        [StringLength(100)]
        public string? FunctionalClassID { get; set; } // e.g., "TOTAL-F0000000338"

        [StringLength(100)]
        public string? TagFormatID { get; set; } // e.g., "Safety and Lifesaving Equipment"

        [StringLength(50)]
        public string? ValidationStatus { get; set; } // e.g., "Valid"

        public bool? CMIMSRequired { get; set; } // Using nullable bool for TRUE/FALSE

        // Boolean flags for existence in other systems (can be useful metadata)
        public bool? FoundInADiagrams { get; set; } //
        public bool? FoundInADL { get; set; } //
        public bool? FoundInAEngineering { get; set; } //
        public bool? FoundInAVEVAE3D { get; set; } //
        public bool? FoundInAVEVAElectricalAndInstrumentation { get; set; } //
        public bool? FoundInEDMS { get; set; } //
        public bool? FoundInPiVision { get; set; } //
        // --- End New Fields ---

        public DateTime? LastMaintenanceDate { get; set; }
        public DateTime? NextMaintenanceDate { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties for related documents/models
        public ICollection<AssetDocument>? Documents { get; set; }
        public ICollection<Asset3DModel>? ThreeDModels { get; set; }
        public ICollection<Asset2DModel>? TwoDModels { get; set; }

        public Asset()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}