using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetManagementApi.Data;
using AssetManagementApi.Models;
using AssetManagementApi.Dtos;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AssetManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AssetsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Existing method to get a single asset by its full tag number
        [HttpGet("{tagNumber}")]
        public async Task<ActionResult<Asset>> GetAssetByTagNumber(string tagNumber)
        {
            if (string.IsNullOrEmpty(tagNumber))
            {
                return NotFound();
            }

            var normalizedTag = tagNumber.ToUpper().Trim();

            var asset = await _context.Assets
                .Include(a => a.AssetDocuments)
                .Include(a => a.Asset2DModels)
                .Include(a => a.Asset3DModels)
                .Include(a => a.AssetRelationships)
                .FirstOrDefaultAsync(a => a.TagNumber.ToUpper().Contains(normalizedTag));

            if (asset == null)
            {
                return NotFound();
            }

            return asset;
        }

        // NEW: Method to get a list of assets by a partial tag number
        [HttpGet("ByPartialTag/{partialTag}")]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssetsByPartialTag(string partialTag)
        {
            if (string.IsNullOrEmpty(partialTag))
            {
                return new List<Asset>(); // Return an empty list if the search string is empty
            }

            var normalizedTag = partialTag.ToUpper().Trim();

            var assets = await _context.Assets
                .Where(a => a.TagNumber.ToUpper().Contains(normalizedTag))
                .OrderBy(a => a.TagNumber) // Order the results for a better user experience
                .Take(10) // Limit the number of results to prevent performance issues
                .Select(a => new Asset
                {
                    TagNumber = a.TagNumber,
                    AssetName = a.AssetName
                })
                .ToListAsync();

            if (assets == null)
            {
                return new List<Asset>();
            }

            return assets;
        }

        // NEW: Method to create a new asset
        [HttpPost]
        public async Task<ActionResult<Asset>> PostAsset([FromForm] CreateAssetDto createAssetDto)
        {
            // 1. Create the new Asset entity from the DTO
            var asset = new Asset
            {
                TagNumber = createAssetDto.TagNumber,
                AssetName = createAssetDto.AssetName,
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
                UpdatedAt = DateTime.UtcNow,
            };

            _context.Assets.Add(asset);

            // 2. Process and save associated data
            if (createAssetDto.AssetRelationships != null)
            {
                foreach (var rel in createAssetDto.AssetRelationships)
                {
                    asset.AssetRelationships.Add(new AssetRelationship
                    {
                        AssociatedTagNumber = rel.AssociatedTagNumber,
                        RelationshipType = rel.RelationshipType
                    });
                }
            }
            
            // 3. Process and save uploaded documents
            if (createAssetDto.AssetDocuments != null && createAssetDto.AssetDocuments.Any())
            {
                var documentsDirectory = Path.Combine(_env.WebRootPath, "documents", asset.TagNumber);
                if (!Directory.Exists(documentsDirectory))
                {
                    Directory.CreateDirectory(documentsDirectory);
                }

                foreach (var file in createAssetDto.AssetDocuments)
                {
                    var filePath = Path.Combine(documentsDirectory, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    asset.AssetDocuments.Add(new AssetDocuments { FilePath = $"/documents/{asset.TagNumber}/{file.FileName}" });
                }
            }
            
            // 4. Process and save uploaded 2D Models
            if (createAssetDto.Asset2DModels != null && createAssetDto.Asset2DModels.Any())
            {
                var models2dDirectory = Path.Combine(_env.WebRootPath, "2dmodels", asset.TagNumber);
                if (!Directory.Exists(models2dDirectory))
                {
                    Directory.CreateDirectory(models2dDirectory);
                }

                foreach (var file in createAssetDto.Asset2DModels)
                {
                    var filePath = Path.Combine(models2dDirectory, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    asset.Asset2DModels.Add(new Asset2DModels { FilePath = $"/2dmodels/{asset.TagNumber}/{file.FileName}" });
                }
            }
            
            // 5. Save all changes to the database
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAssetByTagNumber), new { tagNumber = asset.TagNumber }, asset);
        }
    }
}