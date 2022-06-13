namespace BackendTeamWebApiService.Models;

/// <summary>
/// Team
/// </summary>
public class Team
{
    /// <summary>
    /// Gets or sets the Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the team number
    /// </summary>
    public string TeamNumber { get; set; }

    /// <summary>
    /// Gets or sets the Organization Id
    /// </summary>
    public Guid OrganizationId { get; set; }

    /// <summary>
    /// Gets or sets the Scrum Master Id
    /// </summary>
    public Guid ScrumMasterId { get; set; }

    /// <summary>
    /// Gets or sets the Team Name
    /// </summary>
    public string TeamName { get; set; }
}