using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.Domain;

public class Region
{
    public Guid Id { get; set; }
    [StringLength(maximumLength: 5,  MinimumLength = 3)] public string Code { get; set; } = string.Empty;
    [StringLength(maximumLength: 50, MinimumLength = 3)] public string Name { get; set; } = string.Empty;
    [StringLength(maximumLength: 250)] public string? ImageUrl { get; set; }
}