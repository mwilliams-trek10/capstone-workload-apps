namespace ReportingTeamWebApiService.Models;

/// <summary>
/// Person class
/// </summary>
public abstract class Person
{
    /// <summary>
    /// Gets or sets the Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the First Name
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the Last Name
    /// </summary>
    public string LastName { get; set; }
}