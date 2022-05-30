namespace FrontendTeamBlazorApplication.Services;

using Models;

public sealed class OrganizationService
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    public OrganizationService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task AddOrganization(Organization organization)
    {
        using HttpClient httpClient = _httpClientFactory.CreateClient("BackendService");
        var response = await httpClient.GetAsync("http://127.0.0.1:12345/api/sampleservice");
        response.EnsureSuccessStatusCode();
    }
}