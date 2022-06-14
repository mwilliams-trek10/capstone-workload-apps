namespace BackendTeamWebApiService.Models;

/// <inheritdoc cref="IOrganization"/>
public sealed class Organization : IOrganization
{
    /// <inheritdoc cref="IOrganization"/>
    public Guid Id { get; set; }

    /// <inheritdoc cref="IOrganization"/>
    public string Name { get; set; } = null!;

    /// <inheritdoc cref="IOrganization"/>
    public List<Team> Teams { get; set; } = null!;
    
    /// <inheritdoc cref="IOrganization"/>
    public List<TeamMember> TeamMembers { get; set; } = null!;
    
    /// <inheritdoc cref="IOrganization"/>
    public List<Supervisor> Supervisors { get; set; } = null!;
    
    /// <inheritdoc cref="IOrganization"/>
    public List<ScrumMaster> ScrumMasters { get; set; } = null!;
    
}