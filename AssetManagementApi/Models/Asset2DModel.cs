using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AssetManagementApi.Models
{
    public class Asset2DModels
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Asset")]
        public string? TagNumber { get; set; }
        public Asset? Asset { get; set; }

        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? DrawingType { get; set; }
    }
}