namespace BackendTeamWebApiService.Models;

/// <summary>
/// Person class
/// </summary>
public abstract class Person : IPerson
{
    /// <summary>
    /// Gets or sets the Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the Organization Employee Number
    /// </summary>
    public string OrganizationEmployeeNumber { get; set; }

    /// <summary>
    /// Gets or sets the First Name
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the Last Name
    /// </summary>
    public string LastName { get; set; }
}