using FrontendTeamBlazorApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<ScrumOrganizationService>();

string backendServiceUri = Environment.GetEnvironmentVariable("BACKEND_SERVICE_URI") ?? throw new ApplicationException("BACKEND_SERVICE_URI not found!");

builder.Services.AddHttpClient("AddScrumOrganization", httpClient =>
{
    httpClient.BaseAddress = new Uri($"{backendServiceUri}/api/organization/addneworganization");
});

builder.Services.AddHttpClient("GetAllScrumOrganizationsAsync", httpClient =>
{
    httpClient.BaseAddress = new Uri($"{backendServiceUri}/api/organization/getallscrumorganizations");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();