namespace ReportingTeamWebApiService.Models;

/// <summary>
/// Team Member
/// </summary>
public sealed class TeamMember : Person
{
    /// <summary>
    /// Gets or sets the Supervisor Id
    /// </summary>
    public int SupervisorId { get; set; }

    /// <summary>
    /// Gets or sets the Team Id
    /// </summary>
    public int TeamId { get; set; }
}