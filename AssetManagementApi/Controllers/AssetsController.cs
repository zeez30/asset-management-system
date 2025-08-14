using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetManagementApi.Data;
using AssetManagementApi.Models;

namespace AssetManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AssetsController(ApplicationDbContext context)
        {
            _context = context;
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
    }
}