// File: AssetManagementApi/Models/Asset2DModel.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagementApi.Models
{
    public class Asset2DModel
    {
        [Key]
        public int Model2DID { get; set; }

        [ForeignKey("Asset")]
        [StringLength(50)]
        [Required]
        public string TagNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string FilePath { get; set; } = string.Empty; // Relative path on the server's file system

        [StringLength(255)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? ModelFormat { get; set; } // e.g., "DWG", "DXF", "SVG"

        public int? FileSizeKB { get; set; }

        [StringLength(100)]
        public string? UploadedBy { get; set; }

        [Required]
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        [StringLength(20)]
        public string? Version { get; set; }

        public Asset? Asset { get; set; }

        public Asset2DModel()
        {
            UploadDate = DateTime.UtcNow;
        }
    }
}