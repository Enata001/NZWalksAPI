using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO;

public class NewWalkDto
{
    
    [StringLength(maximumLength: 50)] public string Name { get; set; } = string.Empty;
    [StringLength(maximumLength: 1000)] public string Description { get; set; } = string.Empty;
    public double LengthInKm { get; set; }
    [StringLength(maximumLength: 250)] public string? WalkImageUrl { get; set; }
    public RegionDto? Region { get; set; }
    public DifficultyDto? Difficulty { get; set; }
}