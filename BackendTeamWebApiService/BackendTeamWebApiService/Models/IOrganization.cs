namespace BackendTeamWebApiService.Models;

/// <summary>
/// Organization
/// </summary>
public interface IOrganization
{
    /// <summary>
    /// Gets or sets the Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the Name
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets the Teams
    /// </summary>
    List<Team> Teams { get; set; }

    /// <summary>
    /// Gets or sets the Team Members
    /// </summary>
    List<TeamMember> TeamMembers { get; set; }

    /// <summary>
    /// Gets or sets the Supervisors
    /// </summary>
    List<Supervisor> Supervisors { get; set; }
    
    /// <summary>
    /// Gets or sets the Scrum Masters
    /// </summary>
    List<ScrumMaster> ScrumMasters { get; set; }
}