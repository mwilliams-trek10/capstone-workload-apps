namespace FrontendTeamBlazorApplication;

public static class HttpClientRegistrations
{
    private static readonly string BackendServiceUri = Environment.GetEnvironmentVariable("BACKEND_SERVICE_URI")
                                                       ?? throw new ApplicationException("BACKEND_SERVICE_URI not found!");
    
    // private static readonly string ReportingServiceUri = Environment.GetEnvironmentVariable("REPORTING_SERVICE_URI")
    //                                                    ?? throw new ApplicationException("REPORTING_SERVICE_URI not found!");
    
    public static void RegisterHttpClients(WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient("AddScrumOrganization", httpClient =>
        {
            httpClient.BaseAddress = new Uri($"{BackendServiceUri}/api/scrumorganization/addnewscrumorganization");
        });
        
        builder.Services.AddHttpClient("GetAllScrumOrganizationsAsync", httpClient =>
        {
            httpClient.BaseAddress = new Uri($"{BackendServiceUri}/api/scrumorganization/getallscrumorganizations");
        });
        
        builder.Services.AddHttpClient("GetScrumOrganizationByIdAsync", httpClient =>
        {
            httpClient.BaseAddress = new Uri($"{BackendServiceUri}/api/scrumorganization/getscrumorganizationbyid");
        });
        
        builder.Services.AddHttpClient("UpdateScrumOrganizationAsync", httpClient =>
        {
            httpClient.BaseAddress = new Uri($"{BackendServiceUri}/api/scrumorganization/updatescrumorganization");
        });
    }
}