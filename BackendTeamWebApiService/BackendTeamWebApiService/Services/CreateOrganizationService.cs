namespace BackendTeamWebApiService.Services;

using System.Text;
using Models;
using Utilities;

internal sealed class CreateOrganizationService : ICreateOrganizationService
{
    private readonly string _alphabet = "abcdefghijklmnopqrstuvwxyz";
    
    private AddOrganizationArgs _addOrganizationArgs;

    private readonly Organization _organization;

    private readonly Random _random;

    public CreateOrganizationService()
    {
        _random = Random.Shared;
        _organization = new Organization();
    }

    public async Task<IOrganization> CreateScrumTeamOrganizationAsync(AddOrganizationArgs addOrganizationArgs)
    {
        _addOrganizationArgs = addOrganizationArgs;
        _organization.Id = Guid.NewGuid();
        _organization.Name = addOrganizationArgs.Name;
        
        // Purposefully making these tasks to generate the data in parallel.
        // In a normal real world application, as long as these are independent calls, they can be written this way.
        Task createTeamsTask = CreateTeams();
        
        Task createTeamsMembersTask = CreateTeamMembers();

        Task createScrumMastersTask = CreateScrumMasters();
        
        Task createSupervisorsTask = CreateSupervisors();

        await Task.WhenAll(createTeamsTask, createTeamsMembersTask, createScrumMastersTask, createSupervisorsTask);

        return _organization;
    }

    private async Task CreateTeams()
    {
        _organization.Teams = new List<Team>(_addOrganizationArgs.ScrumTeamCount);

        await CreateT(_organization.Teams, this.CreateTeam);
    }

    private Team CreateTeam(int i)
    {
        return new Team
        {
            Id = Guid.NewGuid(),
            OrganizationId = _organization.Id,
            TeamNumber = $"TM{i.GetZeroLeftPaddedNumber(4)}",
            TeamName = GenerateName(2, 20)
        };
    }
    
    private async Task CreateTeamMembers()
    {
        int teamMemberCount = _addOrganizationArgs.TeamMemberCount;
        _organization.TeamMembers = new List<TeamMember>(teamMemberCount);

        await CreateT(_organization.TeamMembers, CreatePerson<TeamMember>);
    }
    
    private async Task CreateSupervisors()
    {
        int supervisorCount = _addOrganizationArgs.SupervisorCount;
        _organization.Supervisors = new List<Supervisor>(supervisorCount);

        await CreateT(_organization.Supervisors, CreatePerson<Supervisor>);
    }

    private async Task CreateScrumMasters()
    {
        _organization.ScrumMasters = new List<ScrumMaster>(_addOrganizationArgs.ScrumTeamCount);

        await CreateT(_organization.ScrumMasters, CreatePerson<ScrumMaster>);
    }

    private T CreatePerson<T>(int i) where T: IPerson, new()
    {
        return new T
        {
            OrganizationEmployeeNumber = $"EMP{i.GetZeroLeftPaddedNumber(6)}",
            FirstName = GenerateName(3, 7),
            LastName = GenerateName(2, 12)
        };
    }

    private static async Task CreateT<T>(ICollection<T> list, Func<int, T> func) where T : class
    {
        await Task.Run(() =>
        {
            for (int i = 0; i < list.Count; i++)
            {
                T item = func(i);
                list.Add(item);
            }
        });
    }

    private string GenerateName(int minLength, int maxLength)
    {
        int nameLength = _random.Next(minLength, maxLength);
        StringBuilder stringBuilder = new StringBuilder(nameLength);
        
        for (int i = 0; i < nameLength; i++)
        {
            int charPos = _random.Next(0, 25);

            string val = i == 0 ? _alphabet[charPos].ToString().ToUpper() : _alphabet[charPos].ToString();

            stringBuilder.Append(val);
        }

        return stringBuilder.ToString();
    }
    
}