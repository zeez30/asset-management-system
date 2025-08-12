// File: AssetManagementApi/Models/FileUploadRequest.cs

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementApi.Models
{
    public class FileUploadRequest
    {
        [Required]
        public required IFormFile File { get; set; }

        [Required]
        public required string TagNumber { get; set; }
    }
}