using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.Domain;

public class Difficulty
{
    public Guid Id { get; set; }
    [StringLength(maximumLength:50)]
    public string Name { get; set; } = string.Empty;
}