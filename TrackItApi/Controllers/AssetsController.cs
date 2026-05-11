using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackItApi.Data;
using TrackItApi.Models;
using TrackItApi.DTOs;

namespace TrackItApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AssetsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetDto>>> GetAssets()
        {
            var assets = await _context.Assets
                .Include(a => a.AssignedToUser)
                .ToListAsync();

            return assets.Select(a => new AssetDto
            {
                Id = a.Id,
                Name = a.Name,
                Category = a.Category,
                SerialNumber = a.SerialNumber,
                IsAssigned = a.IsAssigned,
                AssignedToUserId = a.AssignedToUserId,
                AssignedToUserName = a.AssignedToUser != null
                    ? $"{a.AssignedToUser.FirstName} {a.AssignedToUser.LastName}"
                    : null
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssetDto>> GetAsset(int id)
        {
            var asset = await _context.Assets
                .Include(a => a.AssignedToUser)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (asset == null) return NotFound();

            return new AssetDto
            {
                Id = asset.Id,
                Name = asset.Name,
                Category = asset.Category,
                SerialNumber = asset.SerialNumber,
                IsAssigned = asset.IsAssigned,
                AssignedToUserId = asset.AssignedToUserId,
                AssignedToUserName = asset.AssignedToUser != null
                    ? $"{asset.AssignedToUser.FirstName} {asset.AssignedToUser.LastName}"
                    : null
            };
        }

        [HttpPost]
        public async Task<ActionResult<AssetDto>> CreateAsset(CreateAssetDto createDto)
        {
            var asset = new Asset
            {
                Name = createDto.Name,
                Category = createDto.Category,
                SerialNumber = createDto.SerialNumber
            };

            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAsset), new { id = asset.Id }, new AssetDto
            {
                Id = asset.Id,
                Name = asset.Name,
                Category = asset.Category,
                SerialNumber = asset.SerialNumber,
                IsAssigned = asset.IsAssigned
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsset(int id, CreateAssetDto updateDto)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null) return NotFound();
            asset.Name = updateDto.Name;
            asset.Category = updateDto.Category;
            asset.SerialNumber = updateDto.SerialNumber;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsset(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null) return NotFound();
            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}