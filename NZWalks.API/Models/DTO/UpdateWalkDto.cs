using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO;

public class UpdateWalkDto
{
    [Required]
    [StringLength(maximumLength: 50, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(maximumLength: 1000,MinimumLength = 10)]
    public string Description { get; set; } = string.Empty;

    [Required] [Range(0, 50),] public double LengthInKm { get; set; }

    [StringLength(maximumLength: 250)] public string? WalkImageUrl { get; set; }

    [Required] public Guid DifficultyId { get; set; }

    [Required] public Guid RegionId { get; set; }
}