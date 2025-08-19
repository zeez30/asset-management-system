using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace AssetManagementApi.Filters
{
    // A Swagger/OpenAPI operation filter to correctly handle file uploads (IFormFile)
    // and other form data in the Swagger UI.
    public class SwaggerFileOperationFilter : IOperationFilter
    {
        // The Apply method is called for each API operation in the Swagger generation process.
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Find all parameters in the action that are of type IFormFile or IFormFileCollection.
            var formFileParameters = context.ApiDescription.ActionDescriptor.Parameters
                .Where(p => p.ParameterType == typeof(IFormFile) || p.ParameterType == typeof(IFormFileCollection))
                .ToList();

            // Check if there are any file parameters in the operation.
            if (formFileParameters.Any())
            {
                // If files are present, define the request body as 'multipart/form-data'.
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content = {
                        ["multipart/form-data"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                // Define the properties for the multipart form data.
                                Properties =
                                {
                                    // Add the IFormFile parameters to the schema.
                                    // The type is "string" and the format is "binary" which represents a file upload.
                                    [formFileParameters.First().Name] = new OpenApiSchema
                                    {
                                        Type = "string",
                                        Format = "binary"
                                    }
                                }
                            }
                        }
                    }
                };

                // Find all other parameters that are bound from a form but are not files.
                var otherFormParameters = context.ApiDescription.ActionDescriptor.Parameters
                    .Where(p => p.BindingInfo?.BindingSource?.Id == "Form" &&
                                p.ParameterType != typeof(IFormFile) &&
                                p.ParameterType != typeof(IFormFileCollection));

                // Add the non-file form parameters to the 'multipart/form-data' schema.
                foreach (var param in otherFormParameters)
                {
                    // Skip if the parameter has already been added (e.g., in a DTO).
                    if (operation.RequestBody.Content["multipart/form-data"].Schema.Properties.ContainsKey(param.Name))
                    {
                        continue; 
                    }

                    // Add the parameter with a simple "string" type.
                    operation.RequestBody.Content["multipart/form-data"].Schema.Properties.Add(param.Name, new OpenApiSchema
                    {
                        Type = "string" // Assuming these are simple string/primitive types
                        // You might need more complex schema generation here for complex types
                    });
                }

                // Remove the parameters from the individual 'Parameters' list in the Swagger UI
                // because they are now part of the RequestBody schema. This prevents duplication.
                operation.Parameters.Clear();
            }
        }
    }
}