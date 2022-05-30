namespace ReportingTeamWebApiService.Models;

/// <summary>
/// Scrum Master
/// </summary>
public sealed class ScrumMaster : Person
{
    /// <summary>
    /// Gets or sets the Team Id
    /// </summary>
    public int TeamId { get; set; }
}