namespace BackendTeamWebApiService.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Update Organization Args
/// </summary>
public sealed class UpdateScrumOrganizationArgs
{
    /// <summary>
    /// Gets or sets the Id
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the Name
    /// </summary>
    [Required]
    public string Name { get; set; } = null!;
}