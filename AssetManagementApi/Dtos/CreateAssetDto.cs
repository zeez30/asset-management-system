using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;

namespace AssetManagementApi.DTOs
{
    // Data Transfer Object (DTO) for creating a new asset.
    // This class is used to receive data from a client when they want to create a new asset.
    public class CreateAssetDto
    {
        // Data annotations are used for model validation.
        [Required]
        // The TagNumber property is required for creating an asset.
        public string? TagNumber { get; set; }

        [Required]
        // The AssetName property is also required.
        public string? AssetName { get; set; }

        // All other properties are optional, allowing for partial data entry.
        public string? Description { get; set; }
        public string? AssetType { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? SerialNumber { get; set; }
        public string? Size { get; set; }
        public DateTime? InstallationDate { get; set; }
        public string? Status { get; set; }
        public string? ValidationStatus { get; set; }
        public DateTime? LastMaintenance { get; set; }
        public string? Site { get; set; }
        public string? DeckPlatform { get; set; }
        public string? AreaCode { get; set; }
        public string? System { get; set; }
        public string? FacilitySection { get; set; }
        public string? FunctionalClassID { get; set; }
        public string? Subsystem { get; set; }
        public bool CMMMSRequired { get; set; }
        public string? TagFormatID { get; set; }

        // IFormFile is used to handle file uploads from an HTTP request.
        // These properties represent collections of files that can be uploaded
        // along with the asset's metadata.
        public List<IFormFile>? AssetDocuments { get; set; }
        public List<IFormFile>? Asset2DModels { get; set; }
        public List<IFormFile>? Asset3DModels { get; set; }
    }
}