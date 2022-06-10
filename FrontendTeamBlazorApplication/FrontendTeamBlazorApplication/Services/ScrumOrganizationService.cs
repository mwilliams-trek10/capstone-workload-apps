namespace FrontendTeamBlazorApplication.Services;

using Models;

public sealed class ScrumOrganizationService
{
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly ILogger<ScrumOrganizationService> _logger;

    public ScrumOrganizationService(IHttpClientFactory httpClientFactory, ILogger<ScrumOrganizationService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }
    
    public async Task AddScrumOrganization(Organization organization)
    {
        using HttpClient httpClient = _httpClientFactory.CreateClient("AddScrumOrganization");
        //var response = await httpClient.GetAsync("http://127.0.0.1:12400/api/organization/ping");

        var httpContent = JsonContent.Create(organization); //GetOrganizationToAddAsHttpContent(organization);

        var response = await httpClient.PostAsync(httpClient.BaseAddress, httpContent);
        
        //var response = await httpClient.GetAsync("http://127.0.0.1:12400/api/organization/ping");
        response.EnsureSuccessStatusCode();
    }

    private HttpContent GetOrganizationToAddAsHttpContent(Organization organization)
    {
        return JsonContent.Create(organization);
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
        using HttpClient httpClient = _httpClientFactory.CreateClient("GetAllScrumOrganizationsAsync");

        _logger.LogInformation(httpClient.BaseAddress.AbsoluteUri);
        
        var response = await httpClient.GetAsync(httpClient.BaseAddress);
        response.EnsureSuccessStatusCode();
        
        var results = await response.Content.ReadFromJsonAsync<List<Organization>>();
        
        return results ?? new List<Organization>();
        
        // Organization organization = new Organization
        // {
        //     Id = Guid.NewGuid(),
        //     Name = "Organization-1"
        // };
        //
        // Organization organizationSecond = new Organization
        // {
        //     Id = Guid.NewGuid(),
        //     Name = "Organization-2"
        // };
        //
        // return await Task.FromResult(new List<Organization>
        // {
        //     organization,
        //     organizationSecond
        // });
    }
}