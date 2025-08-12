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
    }
}