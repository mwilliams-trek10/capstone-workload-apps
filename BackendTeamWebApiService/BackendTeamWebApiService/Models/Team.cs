namespace ReportingTeamWebApiService.Models;

/// <summary>
/// Team
/// </summary>
public class Team
{
    /// <summary>
    /// Gets or sets the Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the Organization Id
    /// </summary>
    public int OrganizationId { get; set; }

    /// <summary>
    /// Gets or sets the Team Name
    /// </summary>
    public string TeamName { get; set; }
}