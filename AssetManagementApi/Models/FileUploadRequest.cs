// File: AssetManagementApi/Models/FileUploadRequest.cs

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementApi.Models
{
    public class FileUploadRequest
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string TagNumber { get; set; }
    }
}