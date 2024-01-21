using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO;

public class NewRegionDto
{
    [Required]
    [StringLength(maximumLength: 5, MinimumLength = 3, ErrorMessage = "Code has to be between 3 and 5 characters")]
    public string Code { get; set; } = string.Empty;

    [Required]
    [StringLength(maximumLength: 100, MinimumLength = 3, ErrorMessage = "Name has to be between 3 and 100 characters")]
    public string Name { get; set; } = string.Empty;

    [StringLength(maximumLength: 250)] public string? ImageUrl { get; set; }
}