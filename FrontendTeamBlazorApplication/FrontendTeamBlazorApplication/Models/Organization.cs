namespace FrontendTeamBlazorApplication.Models;

using System.ComponentModel.DataAnnotations;

public sealed record Organization
{
    [Required]
    public string Name { get; set; }

    [Required]
    [Range(1, 1000, ErrorMessage = "Employee count must be between (1-1000).")]
    public int? EmployeeCount { get; set; }
    
    [Required]
    [Range(1, 50, ErrorMessage = "Scrum team must be between (1-50).")]
    public int? ScrumTeamCount { get; set; }
}