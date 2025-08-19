namespace AssetManagementApi.DTOs
{
    // Data Transfer Object (DTO) for search results.
    // This class is used to return a simplified, consistent object
    // for both Asset and AssetDocument search results.
    public class SearchResultDto
    {
        // The unique tag number of the asset or document.
        public string? TagNumber { get; set; }
        
        // The name of the asset or the title of the document.
        public string? AssetName { get; set; }
        
        // The type of the result, e.g., "Asset" or "Document".
        // This helps the client differentiate between the two types of results.
        public string? Type { get; set; }
    }
}