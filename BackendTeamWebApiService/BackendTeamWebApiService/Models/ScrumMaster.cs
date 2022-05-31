namespace BackendTeamWebApiService.Models;

/// <summary>
/// Scrum Master
/// </summary>
public sealed class ScrumMaster : Person
{
    /// <summary>
    /// Gets or sets the Team Id
    /// </summary>
    public Guid TeamId { get; set; }
}