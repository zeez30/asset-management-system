// File: AssetManagementApi/Filters/SwaggerFileOperationFilter.cs

using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace AssetManagementApi.Filters
{
    public class SwaggerFileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var formFileParameters = context.ApiDescription.ActionDescriptor.Parameters
                .Where(p => p.ParameterType == typeof(IFormFile) || p.ParameterType == typeof(IFormFileCollection))
                .ToList();

            if (formFileParameters.Any())
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content = {
                        ["multipart/form-data"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties =
                                {
                                    // Add IFormFile parameters
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

                // Add other FromForm parameters that are not IFormFile
                var otherFormParameters = context.ApiDescription.ActionDescriptor.Parameters
                    .Where(p => p.BindingInfo?.BindingSource?.Id == "Form" &&
                                p.ParameterType != typeof(IFormFile) &&
                                p.ParameterType != typeof(IFormFileCollection));

                foreach (var param in otherFormParameters)
                {
                    if (operation.RequestBody.Content["multipart/form-data"].Schema.Properties.ContainsKey(param.Name))
                    {
                        continue; // Skip if already added
                    }

                    operation.RequestBody.Content["multipart/form-data"].Schema.Properties.Add(param.Name, new OpenApiSchema
                    {
                        Type = "string" // Assuming these are simple string/primitive types
                        // You might need more complex schema generation here for complex types
                    });
                }

                // Remove the parameters from the Parameters list as they are now in RequestBody
                operation.Parameters.Clear();
            }
        }
    }
}