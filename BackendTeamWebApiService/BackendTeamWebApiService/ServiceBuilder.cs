using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using BackendTeamWebApiService.Repositories;

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
        serviceCollection.AddScoped<IScrumOrganizationService, ScrumOrganizationService>();
        serviceCollection.AddScoped<ICreateOrganizationService, CreateOrganizationService>();
        serviceCollection.AddScoped<IScrumOrganizationRepository, ScrumOrganizationRepository>();
        serviceCollection.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
        serviceCollection.AddScoped<IPersonRepository, PersonRepository>();
        serviceCollection.AddScoped<ITeamRepository, TeamRepository>();
        serviceCollection.AddScoped<ITeamService, TeamService>();
        serviceCollection.AddScoped<IPersonService, PersonService>();
        serviceCollection.AddScoped<ITeamMemberService, TeamMemberService>();
        serviceCollection.AddScoped<IReportingService, ReportingService>();
        
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
        services.AddSingleton(_ =>
            Options.Create(
                options: configurationOptions));
    }

    private static void RegisterAmazonDynamoDbClient(IServiceCollection services, ConfigurationOptions configurationOptions)
    {
        if (configurationOptions.UseWebToken)
        {
            RegisterAmazonDynamoDbClientUsingWebToken(services);
            return;
        }

        RegisterAmazonDynamoDbClientUsingBasicCredentials(services);
    }
    
    private static void RegisterAmazonDynamoDbClientUsingWebToken(IServiceCollection services)
    {
        AssumeRoleWithWebIdentityCredentials awsCredentials = AssumeRoleWithWebIdentityCredentials.FromEnvironmentVariables();
        CreateAmazonDynamoDbClient(services, awsCredentials);
    }

    private static void CreateAmazonDynamoDbClient(IServiceCollection services, AWSCredentials awsCredentials)
    {
        services.AddSingleton(_ =>
        {
            // SessionAWSCredentials awsCredentials = new SessionAWSCredentials(Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID"),
            //     Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY"),
            //     Environment.GetEnvironmentVariable("AWS_SESSION_TOKEN"));
            var client = new AmazonDynamoDBClient(awsCredentials, RegionEndpoint.USEast2);
            return Options.Create(options: client);
        });
    }

    private static void RegisterAmazonDynamoDbClientUsingBasicCredentials(IServiceCollection services)
    {
        BasicAWSCredentials awsCredentials = new BasicAWSCredentials(
            Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID"),
            Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY"));
        
        CreateAmazonDynamoDbClient(services, awsCredentials);
    }
}