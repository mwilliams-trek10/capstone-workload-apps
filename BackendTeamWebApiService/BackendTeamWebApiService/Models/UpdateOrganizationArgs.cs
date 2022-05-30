namespace BackendTeamWebApiService.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Update Organization Args
/// </summary>
public sealed class UpdateOrganizationArgs
{
    /// <summary>
    /// Gets or sets the Id
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the Name
    /// </summary>
    public string Name { get; set; }
}