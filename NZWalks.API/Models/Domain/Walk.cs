using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.Domain;

public class Walk
{
    public Guid Id { get; set; }
    [StringLength(maximumLength: 50)] public string Name { get; set; } = string.Empty;
    [StringLength(maximumLength: 1000)] public string Description { get; set; } = string.Empty;
    public double LengthInKm { get; set; }
    [StringLength(maximumLength: 250)] public string? WalkImageUrl { get; set; }
    public Guid DifficultyId { get; set; }
    public Guid RegionId { get; set; }
    public Difficulty? Difficulty { get; set; }
    public Region? Region { get; set; }
    
    
}