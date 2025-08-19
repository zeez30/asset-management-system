using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;

namespace AssetManagementApi.DTOs
{
    public class CreateAssetDto
    {
        [Required]
        public string? TagNumber { get; set; }

        [Required]
        public string? AssetName { get; set; }

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

        public List<IFormFile>? AssetDocuments { get; set; }
        public List<IFormFile>? Asset2DModels { get; set; }
        public List<IFormFile>? Asset3DModels { get; set; }
    }
}