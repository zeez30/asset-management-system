// File: AssetManagementApi/Controllers/AssetsController.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetManagementApi.Data;
using AssetManagementApi.Models;
using Microsoft.Extensions.Hosting;


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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            if (_context.Assets == null)
            {
                return NotFound();
            }
            return await _context.Assets
                                 .Include(a => a.Documents)
                                 .Include(a => a.ThreeDModels)
                                 .Include(a => a.TwoDModels)
                                 .ToListAsync();
        }

        [HttpGet("ByTagNumber/{tagNumber}")]
        public async Task<ActionResult<Asset>> GetAssetByTagNumber(string tagNumber)
        {
            if (_context.Assets == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                                      .Include(a => a.Documents)
                                      .Include(a => a.ThreeDModels)
                                      .Include(a => a.TwoDModels)
                                      .FirstOrDefaultAsync(a => a.TagNumber == tagNumber);

            if (asset == null)
            {
                return NotFound();
            }

            return asset;
        }

        [HttpPost("UploadDocument")]
        public async Task<IActionResult> UploadDocument([FromForm] FileUploadRequest request)
        {
            if (request.File == null || request.File.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            if (string.IsNullOrEmpty(request.TagNumber))
            {
                return BadRequest("Tag Number must be provided.");
            }

            string contentRootPath = _env.ContentRootPath;
            string storagePath = Path.Combine(contentRootPath, "..", "AssetManagementStorage");

            string assetFolderPath = Path.Combine(storagePath, request.TagNumber);
            if (!Directory.Exists(assetFolderPath))
            {
                Directory.CreateDirectory(assetFolderPath);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(request.File.FileName);
            string filePath = Path.Combine(assetFolderPath, uniqueFileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream);
                }

                return Ok(new { Message = "File uploaded successfully", FileName = uniqueFileName, Path = filePath });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Internal server error: Could not save file. {ex.Message}");
                return StatusCode(500, $"Internal server error: Could not save file. {ex.Message}");
            }
        }

        // Add other CRUD methods here if they exist in your original controller.
    }
}