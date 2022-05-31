namespace BackendTeamWebApiService;

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
        serviceCollection.AddScoped<IOrganizationService, OrganizationService>();
        serviceCollection.AddScoped<ICreateOrganizationService, CreateOrganizationService>();
        serviceCollection.AddScoped<IDynamoDbAccessService, DynamoDbAccessService>();
        
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