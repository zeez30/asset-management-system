// File: AssetManagementApi/Controllers/AssetsController.cs

using AssetManagementApi.Data;
using AssetManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementApi.Controllers
{
    [Route("api/[controller]")] // Defines the base route for this controller (e.g., /api/assets)
    [ApiController] // Indicates that this class is an API controller
    public class AssetsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Constructor: Dependency Injection to get an instance of ApplicationDbContext
        public AssetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Assets
        // Retrieves all assets from the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            if (_context.Assets == null)
            {
                return NotFound(); // Return 404 if the DbSet is null (unlikely if DB is set up)
            }
            return await _context.Assets.ToListAsync(); // Return all assets as a list
        }

        // GET: api/Assets/{tagNumber}
        // Retrieves a single asset by its TagNumber
        [HttpGet("{tagNumber}")]
        public async Task<ActionResult<Asset>> GetAsset(string tagNumber)
        {
            if (_context.Assets == null)
            {
                return NotFound();
            }
            var asset = await _context.Assets.FindAsync(tagNumber); // Find asset by primary key

            if (asset == null)
            {
                return NotFound(); // Return 404 if asset not found
            }

            return asset; // Return the found asset
        }

        // POST: api/Assets
        // Creates a new asset
        [HttpPost]
        public async Task<ActionResult<Asset>> PostAsset(Asset asset)
        {
            if (_context.Assets == null)
            {
                // This scenario means DbContext is not initialized properly, or DB issue
                return Problem("Entity set 'ApplicationDbContext.Assets' is null.");
            }

            // Ensure CreatedAt and UpdatedAt are set on creation
            asset.CreatedAt = DateTime.UtcNow;
            asset.UpdatedAt = DateTime.UtcNow;

            _context.Assets.Add(asset); // Add the new asset to the DbSet

            try
            {
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            catch (DbUpdateException) // Catch potential database update errors (e.g., duplicate TagNumber)
            {
                if (AssetExists(asset.TagNumber))
                {
                    return Conflict(); // Return 409 Conflict if an asset with this TagNumber already exists
                }
                else
                {
                    throw; // Re-throw other database update exceptions
                }
            }

            // Return 201 Created status with the newly created asset and a link to it
            return CreatedAtAction(nameof(GetAsset), new { tagNumber = asset.TagNumber }, asset);
        }

        // PUT: api/Assets/{tagNumber}
        // Updates an existing asset
        [HttpPut("{tagNumber}")]
        public async Task<IActionResult> PutAsset(string tagNumber, Asset asset)
        {
            if (tagNumber != asset.TagNumber)
            {
                return BadRequest(); // Return 400 Bad Request if URL tagNumber doesn't match asset object's tagNumber
            }

            _context.Entry(asset).State = EntityState.Modified; // Mark the entity as modified

            // Ensure UpdatedAt is refreshed on update
            asset.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            catch (DbUpdateConcurrencyException) // Catch concurrency issues (e.g., asset deleted by another process)
            {
                if (!AssetExists(tagNumber))
                {
                    return NotFound(); // Return 404 if asset not found during update
                }
                else
                {
                    throw; // Re-throw other concurrency exceptions
                }
            }

            return NoContent(); // Return 204 No Content for a successful update
        }

        // DELETE: api/Assets/{tagNumber}
        // Deletes an asset
        [HttpDelete("{tagNumber}")]
        public async Task<IActionResult> DeleteAsset(string tagNumber)
        {
            if (_context.Assets == null)
            {
                return NotFound();
            }
            var asset = await _context.Assets.FindAsync(tagNumber); // Find asset to delete
            if (asset == null)
            {
                return NotFound(); // Return 404 if asset not found
            }

            _context.Assets.Remove(asset); // Remove the asset from the DbSet
            await _context.SaveChangesAsync(); // Save changes to the database

            return NoContent(); // Return 204 No Content for a successful deletion
        }

        // Helper method to check if an asset exists
        private bool AssetExists(string tagNumber)
        {
            return (_context.Assets?.Any(e => e.TagNumber == tagNumber)).GetValueOrDefault();
        }
    }
}
