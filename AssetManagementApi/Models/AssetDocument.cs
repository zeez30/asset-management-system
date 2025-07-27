using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagementApi.Models
{
    public class AssetDocument
    {
        [Key] // Primary Key
        public int DocumentID { get; set; } // Auto-incrementing ID

        [ForeignKey("Asset")] // Links to the Asset TagNumber
        [StringLength(50)]
        [Required]
        public string TagNumber { get; set; } = string.Empty; // Foreign Key to Asset

        [Required]
        [StringLength(255)]
        public string FileName { get; set; } = string.Empty; // Original name of the file

        [Required]
        [StringLength(255)]
        public string FilePath { get; set; } = string.Empty; // Relative path on the server's file system

        [StringLength(255)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? DocumentType { get; set; } // e.g., "Manual", "Schematic"

        public int? FileSizeKB { get; set; } // Size of the file in kilobytes

        [StringLength(100)]
        public string? UploadedBy { get; set; }

        [Required]
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        [StringLength(20)]
        public string? Version { get; set; }

        // Navigation property back to the Asset
        public Asset? Asset { get; set; }

        // Constructor to ensure non-nullable properties are initialized
        public AssetDocument()
        {
            UploadDate = DateTime.UtcNow;
        }
    }
}