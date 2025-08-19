using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AssetManagementApi.Models
{
    // Represents a document (file) associated with an asset.
    public class AssetDocuments
    {
        // Primary key for the AssetDocuments table.
        // It's a string, likely a unique identifier for the document itself.
        [Key]
        public string? TagNumber { get; set; }
        
        // The title of the document.
        public string? Title { get; set; }
        
        // The path where the document is stored on the server.
        public string? FilePath { get; set; }

        // Foreign key to link this document back to the main Asset.
        public string? AssetTagNumber { get; set; }
        
        // The original file name of the uploaded document.
        public string? OriginalFileName { get; set; }

        // [JsonIgnore] prevents this property from being serialized to JSON.
        // This is important to avoid circular references when serializing the Asset object.
        [JsonIgnore]
        // [ForeignKey] specifies the foreign key property name.
        // The 'virtual' keyword enables lazy loading, which is a feature of Entity Framework.
        [ForeignKey("AssetTagNumber")]
        public virtual Asset? Asset { get; set; }
    }
}