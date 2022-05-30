namespace ReportingTeamWebApiService;

using Models;
using Services;
using Microsoft.Extensions.Options;

/// <summary>
/// Service Builder class.
/// </summary>
public static class ServiceBuilder
{
    /// <summary>
    /// Adds the service registrations to the service collection.
    /// </summary>
    /// <param name="serviceCollection">The service collection.</param>
    /// <param name="webHostBuilderContext">The web host builder context.</param>
    public static void AddServices(IServiceCollection serviceCollection,
        WebHostBuilderContext webHostBuilderContext)
    {
        ConfigurationOptions configurationOptions = GetConfigurationOptions(webHostBuilderContext.Configuration);
        
        serviceCollection.AddOptions();
        serviceCollection.AddScoped<IDynamoDbAccessService, DynamoDbAccessService>();
        serviceCollection.AddScoped<IEfsAccessService, EfsAccessService>();
        serviceCollection.AddScoped<IOrganizationEfsAccessService, OrganizationEfsAccessService>();
        serviceCollection.AddScoped<IOrganizationReportService, OrganizationReportService>();
        serviceCollection.AddScoped<IOrganizationS3AccessService, OrganizationS3AccessService>();
        serviceCollection.AddScoped<IS3AccessService, S3AccessService>();
        
        
        AddConfigurationOptions(serviceCollection, configurationOptions);
    }
    
    private static ConfigurationOptions GetConfigurationOptions(IConfiguration configuration)
    {
        return configuration.GetSection("ConfigurationOptions")
            .Get<ConfigurationOptions>();
    }

    private static void AddConfigurationOptions(IServiceCollection services, ConfigurationOptions configurationOptions)
    {
        services.AddSingleton(p =>
            Options.Create(
                options: configurationOptions));
    }
}