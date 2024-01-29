using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Repositories.Class;

public class RegionRepository : IRegionRepository
{
    private readonly AppDbContext _context;

    public RegionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Region>> GetAllAsync()
    {
        try
        {
            var regions = await _context.Regions.ToListAsync();

            return regions;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<Region?> GetById(Guid id)
    {
        var region = await _context.Regions.FindAsync(id);
        return region;
    }

    public async Task<Region?> Remove(Guid id)
    {
        var existingRegion = await _context.Regions.FindAsync(id);
        if (existingRegion is null)
        {
            return null;
        }

        _context.Regions.Remove(existingRegion);
        await _context.SaveChangesAsync();
        return existingRegion;
    }

    public async Task<Region?> Update(Guid id, Region region)
    {
        var existingRegion = await _context.Regions.FindAsync(id);
        if (existingRegion is null)
        {
            return null;
        }

        existingRegion.Code = region.Code;
        existingRegion.ImageUrl = region.ImageUrl;
        existingRegion.Name = region.Name;
        await _context.SaveChangesAsync();
        return existingRegion;
    }

    public async Task<Region> CreateAsync(Region region)
    {
        await _context.Regions.AddAsync(region);
        await _context.SaveChangesAsync();
        return region;
    }
    
}