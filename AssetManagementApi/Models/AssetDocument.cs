// File: AssetManagementApi/Models/AssetDocument.cs

using System; // For DateTime
using System.ComponentModel.DataAnnotations; // For [Key]
using System.ComponentModel.DataAnnotations.Schema; // For [ForeignKey] if needed for clarity

namespace AssetManagementApi.Models
{
    public class AssetDocument
    {
        [Key] // Designates DocumentId as the primary key
        public string DocumentId { get; set; } = Guid.NewGuid().ToString(); // Unique ID for the document entry

        // Foreign Key to Asset
        [Required]
        // This links to the Asset's TagNumber, which is its primary key.
        // It's good practice to explicitly state the foreign key relationship
        // if the property name doesn't follow EF Core conventions exactly (e.g., AssetTagNumber instead of TagNumber)
        // [ForeignKey("Asset")] // This would be if you named the navigation property 'Asset' and wanted to explicitly state the FK
        public string TagNumber { get; set; } // Links to the Asset's TagNumber

        [Required]
        [MaxLength(255)] // Max length for the original file name
        public string FileName { get; set; } // Original name of the uploaded file

        [Required]
        [MaxLength(500)] // Max length for the path where the file is stored (URL-friendly)
        public string FilePath { get; set; } // Relative URL path where the file can be accessed (e.g., /StaticFiles/uniqueid.pdf)

        [MaxLength(100)] // Max length for MIME type (e.g., "application/pdf")
        public string FileType { get; set; } // MIME type of the file

        public DateTime UploadDate { get; set; } // Date and time when the document was uploaded

        // Navigation property back to Asset (optional, but good for EF Core relationships)
        // This allows you to easily access the associated Asset from an AssetDocument
        public Asset Asset { get; set; }
    }
}