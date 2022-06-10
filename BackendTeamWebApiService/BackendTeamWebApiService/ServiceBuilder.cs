using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;

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

        RegisterAmazonDynamoDbClient(serviceCollection, configurationOptions);
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

    private static void RegisterAmazonDynamoDbClient(IServiceCollection services, ConfigurationOptions configurationOptions)
    {
        if (configurationOptions.IsDevelopment)
        {
            RegisterAmazonDynamoDbClientFromEnvironmentVariables(services);
            return;
        }

        RegisterAmazonDynamoDbClientForRegion(services, configurationOptions);
    }
    
    private static void RegisterAmazonDynamoDbClientFromEnvironmentVariables(IServiceCollection services)
    {
        services.AddSingleton(p =>
        {
            // SessionAWSCredentials awsCredentials = new SessionAWSCredentials(Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID"),
            //     Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY"),
            //     Environment.GetEnvironmentVariable("AWS_SESSION_TOKEN"));

            BasicAWSCredentials awsCredentials = new BasicAWSCredentials(
                Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID"),
                Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY"));
            
            var client = new AmazonDynamoDBClient(awsCredentials, RegionEndpoint.USEast2);
            return Options.Create(options: client);
        });
    }

    private static void RegisterAmazonDynamoDbClientForRegion(IServiceCollection services, ConfigurationOptions configurationOptions)
    {
        services.AddSingleton(serviceProvider =>
        {
            var region = RegionEndpoint.EnumerableAllRegions.First(regionEndpoint =>
                regionEndpoint.DisplayName.Equals(configurationOptions.Region, StringComparison.InvariantCultureIgnoreCase));
            var client = new AmazonDynamoDBClient(region);
            return Options.Create(options: client);
        });
    }
}