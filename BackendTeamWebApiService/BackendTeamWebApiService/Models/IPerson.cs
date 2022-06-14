namespace BackendTeamWebApiService.Models;

/// <summary>
/// Person
/// </summary>
public interface IPerson
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
    /// Gets or sets the Organization Id
    /// </summary>
    Guid OrganizationId { get; set; }

    /// <summary>
    /// Gets or sets the First Name
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the Last Name
    /// </summary>
    public string LastName { get; set; }
}