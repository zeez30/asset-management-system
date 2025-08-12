using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AssetManagementApi.Models
{
    public class AssetRelationship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? ParentTagNumber { get; set; }
        [JsonIgnore]
        public Asset? ParentAsset { get; set; }

        public string? AssociatedTagNumber { get; set; }
        public string? RelationshipType { get; set; }

        [NotMapped]
        public Asset? AssociatedAsset { get; set; }
    }
}