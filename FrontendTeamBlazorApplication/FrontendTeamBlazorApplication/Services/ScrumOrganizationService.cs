namespace FrontendTeamBlazorApplication.Services;

using Models;

public sealed class ScrumOrganizationService
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    public ScrumOrganizationService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task AddOrganization(Organization organization)
    {
        using HttpClient httpClient = _httpClientFactory.CreateClient("BackendService");
        var response = await httpClient.GetAsync("http://127.0.0.1:12400/api/organization/ping");
        response.EnsureSuccessStatusCode();
    }

    public async Task<Organization> GetScrumOrganizationsByIdAsync(Guid id)
    {
        return new Organization
        {
            Id = id,
            Name = "Get-Organization"
        };
    }
    
    public async Task<List<Organization>> GetAllScrumOrganizationsAsync()
    {
        // using HttpClient httpClient = _httpClientFactory.CreateClient("BackendService");
        // var response = await httpClient.GetAsync("http://127.0.0.1:12345/api/organization/getallscrumorganizations");
        // response.EnsureSuccessStatusCode();
        //
        // var results = await response.Content.ReadFromJsonAsync<List<Organization>>();
        //
        // return results ?? new List<Organization>();
        
        Organization organization = new Organization
        {
            Id = Guid.NewGuid(),
            Name = "Organization-1"
        };
        
        Organization organizationSecond = new Organization
        {
            Id = Guid.NewGuid(),
            Name = "Organization-2"
        };

        return await Task.FromResult(new List<Organization>
        {
            organization,
            organizationSecond
        });
    }
}