using System.ComponentModel.DataAnnotations;

namespace FrontendTeamBlazorApplication.Models;

public class EditScrumOrganization
{
    public EditScrumOrganization(ScrumOrganization scrumOrganization)
    {
        Id = scrumOrganization.Id;
        Name = scrumOrganization.Name;
    }
    
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the Name.
    /// </summary>
    [Required]
    public string Name { get; set; }
}