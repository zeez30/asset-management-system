using System.ComponentModel.DataAnnotations.Schema;
using AssetManagementApi.Models; // <-- Add this line

// Represents a 3D model file associated with an asset.
public class Asset3DModels
{
    // Primary key for the Asset3DModels table.
    public int Id { get; set; }
    
    // The path where the 3D model file is stored on the server.
    public string? FilePath { get; set; }

    // Foreign key that links this 3D model to an Asset.
    public string? AssetTagNumber { get; set; }
    
    // The original file name of the uploaded model.
    public string? OriginalFileName { get; set; }

    // Navigation property to the parent Asset.
    // The [ForeignKey] attribute specifies which property is the foreign key.
    [ForeignKey("AssetTagNumber")]
    public Asset? Asset { get; set; }
}