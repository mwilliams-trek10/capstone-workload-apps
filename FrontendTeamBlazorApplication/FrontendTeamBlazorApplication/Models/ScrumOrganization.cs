namespace FrontendTeamBlazorApplication.Models;

using System.ComponentModel.DataAnnotations;

public sealed class ScrumOrganization
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the Name.
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    [Required]
    [Range(1, 1000, ErrorMessage = "Employee count must be between (1-1000).")]
    public int? TeamMemberCount { get; set; }
    
    [Required]
    [Range(1, 50, ErrorMessage = "Scrum team must be between (1-50).")]
    public int? ScrumTeamCount { get; set; }
}