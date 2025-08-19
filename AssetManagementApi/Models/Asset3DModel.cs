// Asset3DModels.cs
using System.ComponentModel.DataAnnotations.Schema;
using AssetManagementApi.Models; // <-- Add this line

public class Asset3DModels
{
    public int Id { get; set; }
    public string? FilePath { get; set; }

    public string? AssetTagNumber { get; set; }
    
    public string? OriginalFileName { get; set; }

    [ForeignKey("AssetTagNumber")]
    public Asset? Asset { get; set; }
}