using FrontendTeamBlazorApplication.Models;

namespace FrontendTeamBlazorApplication.Services;

public sealed class TeamMemberService
{
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly ILogger<ScrumOrganizationService> _logger;

    public TeamMemberService(IHttpClientFactory httpClientFactory, ILogger<ScrumOrganizationService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }
    
    public async Task AddScrumOrganization(ScrumOrganization scrumOrganization)
    {
        using HttpClient httpClient = _httpClientFactory.CreateClient("AddScrumOrganization");

        var httpContent = JsonContent.Create(scrumOrganization);

        var response = await httpClient.PostAsync(httpClient.BaseAddress, httpContent);
        
        response.EnsureSuccessStatusCode();
    }

    public async Task<ScrumOrganization> GetScrumOrganizationByIdAsync(Guid id)
    {
        using HttpClient httpClient = _httpClientFactory.CreateClient("GetScrumOrganizationByIdAsync");
        httpClient.DefaultRequestHeaders.Add("guid", id.ToString());

        var response = await httpClient.GetAsync(httpClient.BaseAddress);
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<ScrumOrganization>();

        return result ?? new ScrumOrganization();
    }
    
    public async Task<List<ScrumOrganization>> GetAllScrumOrganizationsAsync()
    {
        using HttpClient httpClient = _httpClientFactory.CreateClient("GetAllScrumOrganizationsAsync");

        _logger.LogInformation(httpClient.BaseAddress.AbsoluteUri);
        
        var response = await httpClient.GetAsync(httpClient.BaseAddress);
        response.EnsureSuccessStatusCode();
        
        var results = await response.Content.ReadFromJsonAsync<List<ScrumOrganization>>();
        
        return results ?? new List<ScrumOrganization>();
    }
    
    public async Task UpdateScrumOrganization(EditScrumOrganization scrumOrganization)
    {
        using HttpClient httpClient = _httpClientFactory.CreateClient("UpdateScrumOrganizationAsync");

        var httpContent = JsonContent.Create(scrumOrganization);

        var response = await httpClient.PostAsync(httpClient.BaseAddress, httpContent);
        
        response.EnsureSuccessStatusCode();
    }
}