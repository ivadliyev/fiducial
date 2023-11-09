using System.ComponentModel.DataAnnotations;

namespace fiducial.bll.Models;

public class BookAddDto
{
    [Required]
    public string? Title { get; set; }
    [Required]
    public string? Author { get; set; }
}