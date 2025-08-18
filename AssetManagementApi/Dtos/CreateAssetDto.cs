namespace AssetManagementApi.Dtos
{
    public class CreateAssetDto
    {
        // Primary Asset Properties
        public string? TagNumber { get; set; }
        public string? AssetName { get; set; }
        public string? Description { get; set; }
        public string? AssetType { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? SerialNumber { get; set; }
        public string? Size { get; set; }
        public DateTime InstallationDate { get; set; }
        public string? Status { get; set; }
        public string? ValidationStatus { get; set; }
        public DateTime LastMaintenance { get; set; }
        public string? Site { get; set; }
        public string? DeckPlatform { get; set; }
        public string? AreaCode { get; set; }
        public string? System { get; set; }
        public string? FacilitySection { get; set; }
        public string? FunctionalClassID { get; set; }
        public string? Subsystem { get; set; }
        public string? CMMMSRequired { get; set; }
        public string? TagFormatID { get; set; }

        // Associated Data
        public List<CreateAssetRelationshipDto> AssetRelationships { get; set; } = new List<CreateAssetRelationshipDto>();
        public List<IFormFile> AssetDocuments { get; set; } = new List<IFormFile>();
        public List<IFormFile> Asset2DModels { get; set; } = new List<IFormFile>();
    }

    public class CreateAssetRelationshipDto
    {
        public string? AssociatedTagNumber { get; set; }
        public string? RelationshipType { get; set; }
    }
}