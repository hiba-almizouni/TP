using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models;

public class School
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Sections { get; set; } = string.Empty;

    [Required]
    public string Director { get; set; } = string.Empty;

    [Range(0, 5)]
    public double Rating { get; set; }

    [Url]
    public string? WebSite { get; set; }
}
