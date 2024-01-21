using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Repositories.Class;

public class WalkRepository : IWalkRepository
{
    private readonly AppDbContext _context;

    public WalkRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Walk> CreateAsync(Walk walk)
    {
        await _context.Walks.AddAsync(walk);
        await _context.SaveChangesAsync();
        return walk;
    }

    public async Task<List<Walk>> GetAll(string? filterOn = null, string? filterQuery = null, string? sortBy = null,
        bool isAscending = true, int pageNumber = 1, int pageSize = 100)
    {
        // var walks = await _context.Walks.Include("Difficulty").Include("Region").ToListAsync();

        var walks = _context.Walks.Include("Difficulty").Include("Region").AsQueryable();

        //filtering
        if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
        {
            if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = walks.Where(x => x.Name.ToLower().Contains(filterQuery.ToLower()));
            }

            if (filterOn.Equals("Region", StringComparison.OrdinalIgnoreCase))
            {
                walks = walks.Where(x => x.Region != null && x.Region.Name.ToLower().Contains(filterQuery.ToLower()));
            }

            if (filterOn.Equals("Difficulty", StringComparison.OrdinalIgnoreCase))
            {
                walks = walks.Where(x =>
                    x.Difficulty != null && x.Difficulty.Name.ToLower().Contains(filterQuery.ToLower()));
            }
        }

        //sorting
        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
            }

            if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
            {
                walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
            }
        }
        
        //pagination
        var skipResults = (pageNumber - 1) * pageSize;

        return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
    }

    public async Task<Walk?> GetById(Guid id)
    {
        var walk = await _context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        if (walk is null)
        {
            return null;
        }

        return walk;
    }

    public async Task<Walk?> Update(Guid id, Walk walk)
    {
        var existingWalk = await _context.Walks.Include("Difficulty").Include("Region")
            .FirstOrDefaultAsync(x => x.Id == id);
        if (existingWalk is null)
        {
            return null;
        }

        existingWalk.Name = walk.Name;
        existingWalk.Description = walk.Description;
        existingWalk.WalkImageUrl = walk.WalkImageUrl;
        existingWalk.LengthInKm = walk.LengthInKm;
        existingWalk.DifficultyId = walk.DifficultyId;
        existingWalk.RegionId = walk.RegionId;

        await _context.SaveChangesAsync();
        return existingWalk;
    }

    public async Task<Walk?> Remove(Guid id)
    {
        var existingWalk = await _context.Walks.FindAsync(id);
        if (existingWalk is null)
        {
            return null;
        }

        _context.Walks.Remove(existingWalk);
        await _context.SaveChangesAsync();
        return existingWalk;
    }
}