using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackItApi.Data;
using TrackItApi.Models;

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
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            return await _context.Assets.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAsset(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null) return NotFound();
            return asset;
        }

        [HttpPost]
        public async Task<ActionResult<Asset>> CreateAsset(Asset asset)
        {
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAsset), new { id = asset.Id }, asset);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Asset>> UpdateAsset(Asset asset, int id)
        {
            var oldAsset = await _context.Assets.FindAsync(id);
            if (oldAsset == null) return NotFound();
            oldAsset.Name = asset.Name;
            oldAsset.IsAssigned = asset.IsAssigned;
            oldAsset.SerialNumber = asset.SerialNumber;
            oldAsset.Category = asset.Category;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Asset>> DeleteAsset(int id)        
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null) return NotFound();
            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}