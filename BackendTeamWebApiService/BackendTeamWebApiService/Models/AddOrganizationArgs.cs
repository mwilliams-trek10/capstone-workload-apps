namespace BackendTeamWebApiService.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Add Organization Args
/// </summary>
public sealed class AddOrganizationArgs
{
    /// <summary>
    /// Gets or sets the Name
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the Employee Count
    /// </summary>
    [Required]
    public int EmployeeCount { get; set; }

    /// <summary>
    /// Gets or sets the Scrum Team Count
    /// </summary>
    [Required]
    public int ScrumTeamCount { get; set; }
}