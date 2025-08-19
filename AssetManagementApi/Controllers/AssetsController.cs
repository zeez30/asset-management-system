using AssetManagementApi.Data;
using AssetManagementApi.DTOs;
using AssetManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AssetManagementApi.Controllers
{
    // Sets the base route for the API controller
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        // Dependency injection for the database context and web hosting environment
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AssetsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: api/Assets
        // Retrieves a list of all assets from the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            return await _context.Assets.ToListAsync();
        }

        // GET: api/Assets/{tagNumber}
        // Retrieves a single asset by its unique tag number.
        // Eager loads related data (relationships, documents, 2D/3D models)
        [HttpGet("{tagNumber}")]
        public async Task<ActionResult> GetAsset(string tagNumber)
        {
            // Query for the main asset and include its relationships and models
            var asset = await _context.Assets
                .Include(a => a.AssetRelationships)
                .Include(a => a.Asset2DModels)
                .Include(a => a.Asset3DModels)
                .FirstOrDefaultAsync(a => a.TagNumber == tagNumber);

            if (asset != null)
            {
                // Query for related documents separately
                var documents = await _context.AssetDocuments
                    .Where(d => d.AssetTagNumber == tagNumber)
                    .Select(d => new
                    {
                        d.TagNumber,
                        d.Title,
                        d.FilePath,
                        d.OriginalFileName
                    })
                    .ToListAsync();

                // Query for 2D models
                var models2D = await _context.Asset2DModels
                    .Where(m => m.AssetTagNumber == tagNumber)
                    .Select(m => new 
                    {
                        m.FilePath,
                        m.OriginalFileName
                    })
                    .ToListAsync();

                // Query for 3D models
                var models3D = await _context.Asset3DModels
                    .Where(m => m.AssetTagNumber == tagNumber)
                    .Select(m => new
                    {
                        m.FilePath,
                        m.OriginalFileName
                    })
                    .ToListAsync();
                
                // Create an anonymous object to return the asset with all its associated data
                var assetWithDocs = new
                {
                    asset.TagNumber,
                    asset.AssetName,
                    asset.Description,
                    asset.AssetType,
                    asset.Manufacturer,
                    asset.Model,
                    asset.SerialNumber,
                    asset.Size,
                    asset.InstallationDate,
                    asset.Status,
                    asset.ValidationStatus,
                    asset.LastMaintenance,
                    asset.Site,
                    asset.DeckPlatform,
                    asset.AreaCode,
                    asset.System,
                    asset.FacilitySection,
                    asset.FunctionalClassID,
                    asset.Subsystem,
                    asset.CMMMSRequired,
                    asset.TagFormatID,
                    asset.CreatedAt,
                    asset.UpdatedAt,
                    asset.AssetRelationships,
                    AssetDocuments = documents,
                    Asset2DModels = models2D,
                    Asset3DModels = models3D
                };
                
                return Ok(assetWithDocs);
            }

            // If a primary asset is not found, check if the tag belongs to a document
            var document = await _context.AssetDocuments
                .FirstOrDefaultAsync(d => d.TagNumber == tagNumber);

            if (document != null)
            {
                return Ok(document);
            }

            return NotFound();
        }

        // GET: api/Assets/ByPartialTag/TAG
        // Retrieves a list of assets and documents that match a partial tag number.
        // This is used for the search autocomplete functionality.
        [HttpGet("ByPartialTag/{partialTag}")]
        public async Task<ActionResult<IEnumerable<SearchResultDto>>> GetAssetsByPartialTag(string partialTag)
        {
            if (string.IsNullOrWhiteSpace(partialTag))
            {
                return new List<SearchResultDto>();
            }

            var lowercasePartialTag = partialTag.ToLower();

            // Search for matching assets
            var assetResults = await _context.Assets
                .Where(a => a.TagNumber != null && a.TagNumber.ToLower().Contains(lowercasePartialTag))
                .Select(a => new SearchResultDto
                {
                    TagNumber = a.TagNumber,
                    AssetName = a.AssetName,
                    Type = "Asset"
                })
                .ToListAsync();

            // Search for matching documents
            var documentResults = await _context.AssetDocuments
                .Where(d => d.TagNumber != null && d.TagNumber.ToLower().Contains(lowercasePartialTag))
                .Select(d => new SearchResultDto
                {
                    TagNumber = d.TagNumber,
                    AssetName = d.Title, // Use the document's title for the name
                    Type = "Document"
                })
                .ToListAsync();

            // Combine and order the results
            var combinedResults = assetResults
                .Union(documentResults)
                .OrderBy(r => r.TagNumber)
                .ToList();

            return combinedResults;
        }

        // POST: api/Assets
        // Creates a new asset and handles file uploads (documents, 2D/3D models).
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> PostAsset([FromForm] CreateAssetDto createAssetDto)
        {
            // Validate the incoming model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check for a duplicate asset tag number
            if (await _context.Assets.AnyAsync(a => a.TagNumber == createAssetDto.TagNumber))
            {
                ModelState.AddModelError("TagNumber", "An asset with this tag number already exists.");
                return Conflict(ModelState);
            }

            // Create a new Asset object from the DTO
            var newAsset = new Asset
            {
                TagNumber = createAssetDto.TagNumber!, 
                AssetName = createAssetDto.AssetName!,
                Description = createAssetDto.Description,
                AssetType = createAssetDto.AssetType,
                Manufacturer = createAssetDto.Manufacturer,
                Model = createAssetDto.Model,
                SerialNumber = createAssetDto.SerialNumber,
                Size = createAssetDto.Size,
                InstallationDate = createAssetDto.InstallationDate,
                Status = createAssetDto.Status,
                ValidationStatus = createAssetDto.ValidationStatus,
                LastMaintenance = createAssetDto.LastMaintenance,
                Site = createAssetDto.Site,
                DeckPlatform = createAssetDto.DeckPlatform,
                AreaCode = createAssetDto.AreaCode,
                System = createAssetDto.System,
                FacilitySection = createAssetDto.FacilitySection,
                FunctionalClassID = createAssetDto.FunctionalClassID,
                Subsystem = createAssetDto.Subsystem,
                CMMMSRequired = createAssetDto.CMMMSRequired,
                TagFormatID = createAssetDto.TagFormatID,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Assets.Add(newAsset);
            await _context.SaveChangesAsync();

            var webRootPath = _hostEnvironment.WebRootPath;

            // Handle Document file uploads
            if (createAssetDto.AssetDocuments != null && createAssetDto.AssetDocuments.Count > 0)
            {
                var documentsDirectory = Path.Combine(webRootPath, "documents");
                if (!Directory.Exists(documentsDirectory))
                {
                    Directory.CreateDirectory(documentsDirectory);
                }

                foreach (var file in createAssetDto.AssetDocuments)
                {
                    var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                    var filePath = Path.Combine(documentsDirectory, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    
                    _context.AssetDocuments.Add(new AssetDocuments
                    {
                        TagNumber = $"{newAsset.TagNumber}-DOC-{Guid.NewGuid().ToString().Substring(0, 4)}",
                        Title = file.FileName,
                        FilePath = $"/documents/{uniqueFileName}",
                        AssetTagNumber = newAsset.TagNumber,
                        OriginalFileName = file.FileName
                    });
                }
            }
            
            // Handle 2D Model file uploads
            if (createAssetDto.Asset2DModels != null && createAssetDto.Asset2DModels.Count > 0)
            {
                var models2DDirectory = Path.Combine(webRootPath, "2dmodels");
                if (!Directory.Exists(models2DDirectory))
                {
                    Directory.CreateDirectory(models2DDirectory);
                }

                foreach (var file in createAssetDto.Asset2DModels)
                {
                    var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                    var filePath = Path.Combine(models2DDirectory, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    _context.Asset2DModels.Add(new Asset2DModels
                    {
                        FilePath = $"/2dmodels/{uniqueFileName}",
                        AssetTagNumber = newAsset.TagNumber,
                        OriginalFileName = file.FileName
                    });
                }
            }

            // Handle 3D Model file uploads
            if (createAssetDto.Asset3DModels != null && createAssetDto.Asset3DModels.Count > 0)
            {
                var models3DDirectory = Path.Combine(webRootPath, "3dmodels");
                if (!Directory.Exists(models3DDirectory))
                {
                    Directory.CreateDirectory(models3DDirectory);
                }

                foreach (var file in createAssetDto.Asset3DModels)
                {
                    var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                    var filePath = Path.Combine(models3DDirectory, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    _context.Asset3DModels.Add(new Asset3DModels
                    {
                        FilePath = $"/3dmodels/{uniqueFileName}",
                        AssetTagNumber = newAsset.TagNumber,
                        OriginalFileName = file.FileName
                    });
                }
            }
            
            // Save all changes to the database
            await _context.SaveChangesAsync();

            // Return a 201 Created response with the newly created asset
            return CreatedAtAction("GetAsset", new { tagNumber = newAsset.TagNumber }, newAsset);
        }

        // DELETE: api/Assets/{tagNumber}
        // Deletes an asset by its tag number
        [HttpDelete("{tagNumber}")]
        public async Task<IActionResult> DeleteAsset(string tagNumber)
        {
            var asset = await _context.Assets.FindAsync(tagNumber);
            if (asset == null)
            {
                return NotFound();
            }

            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}