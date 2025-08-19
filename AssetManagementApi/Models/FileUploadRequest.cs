using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementApi.Models
{
    // A model class used for file upload requests.
    // This DTO (Data Transfer Object) defines the structure
    // of the data expected in an HTTP request for uploading a single file
    // and associating it with a tag number.
    public class FileUploadRequest
    {
        // [Required] is a data annotation for validation, ensuring that the 'File' property is not null.
        // 'required' keyword in C# 11 and later ensures that this property must be initialized.
        public required IFormFile File { get; set; }

        // [Required] ensures that the 'TagNumber' property is not null.
        // This links the uploaded file to a specific asset.
        public required string TagNumber { get; set; }
    }
}