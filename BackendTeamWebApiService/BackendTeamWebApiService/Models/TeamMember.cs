namespace BackendTeamWebApiService.Models;

/// <summary>
/// Team Member
/// </summary>
public sealed class TeamMember : Person
{
    /// <summary>
    /// Gets or sets the Supervisor Id
    /// </summary>
    public Guid SupervisorId { get; set; }

    /// <summary>
    /// Gets or sets the Team Id
    /// </summary>
    public Guid TeamId { get; set; }
}