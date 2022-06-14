using System.Runtime.CompilerServices;

namespace BackendTeamWebApiService.Services;

using System.Text;
using Models;
using Utilities;

internal sealed class CreateOrganizationService : ICreateOrganizationService
{
    private readonly string _alphabet = "abcdefghijklmnopqrstuvwxyz";
    
    private AddScrumOrganizationArgs _addScrumOrganizationArgs = null!;

    private readonly Organization _organization;

    private readonly Random _random;

    private static int _universalCount;

    public CreateOrganizationService()
    {
        _random = Random.Shared;
        _organization = new();
    }

    public async Task<IOrganization> CreateScrumTeamOrganizationAsync(AddScrumOrganizationArgs addScrumOrganizationArgs)
    {
        _addScrumOrganizationArgs = addScrumOrganizationArgs;
        _organization.Id = Guid.NewGuid();
        _organization.Name = addScrumOrganizationArgs.Name;
        
        // Purposefully making these tasks to generate the data in parallel.
        // In a normal real world application, as long as these are independent calls, they can be written this way.
        Task createTeamsTask = CreateTeams();
        
        Task createTeamsMembersTask = CreateTeamMembers();

        Task createScrumMastersTask = CreateScrumMasters();
        
        Task createSupervisorsTask = CreateSupervisors();

        await Task.WhenAll(createTeamsTask, createTeamsMembersTask, createScrumMastersTask, createSupervisorsTask);

        CreateAssociations();

        return _organization;
    }

    private void CreateAssociations()
    {
        AssignTeamMembersToTeams();
        AssignTeamMembersToSupervisors();
        AssignTeamsToScrumMasters();
    }

    private void AssignTeamMembersToTeams()
    {
        foreach (var teamMember in _organization.TeamMembers)
        {
            var groupedTeamCounts = from t in _organization.Teams
                join tm in _organization.TeamMembers
                    on t.Id equals tm.TeamId into tmToT
                from tCount in tmToT.DefaultIfEmpty()
                group tCount by t.Id into groupedSmCounts
                select new
                {
                    team = groupedSmCounts.Key,
                    count = groupedSmCounts.Count()
                };

            teamMember.TeamId = groupedTeamCounts.OrderBy(p => p.count).First().team;
        }
    }

    private void AssignTeamMembersToSupervisors()
    {
        foreach (var teamMember in _organization.TeamMembers)
        {
            var groupedSupervisorCounts = from s in _organization.Supervisors
                join tm in _organization.TeamMembers
                    on s.Id equals tm.SupervisorId into tmToS
                from sCount in tmToS.DefaultIfEmpty()
                group sCount by s.Id into groupedSmCounts
                select new
                {
                    supervisor = groupedSmCounts.Key,
                    count = groupedSmCounts.Count()
                };

            teamMember.SupervisorId = groupedSupervisorCounts.OrderBy(p => p.count).First().supervisor;
        }
    }

    private void AssignTeamsToScrumMasters()
    {
        var results = _organization.Teams.Zip(_organization.ScrumMasters, (team, scrumMaster) => new { teamId = team.Id, scrumMasterId = scrumMaster.Id });

        foreach (var result in results)
        {
            _organization.Teams.First(p => p.Id == result.teamId).ScrumMasterId = result.scrumMasterId;
        }

        // foreach (var team in _organization.Teams)
        // {
        //     // var groupedScrumMasterCounts = from sm in _organization.ScrumMasters
        //     //     join tm in _organization.Teams
        //     //         on sm.Id equals tm.ScrumMasterId into tmToSm
        //     //     from smCount in tmToSm.DefaultIfEmpty()
        //     //     group smCount by sm.Id into groupedSmCounts
        //     //     select new
        //     //     {
        //     //         scrumMaster = groupedSmCounts.Key,
        //     //         count = groupedSmCounts.Count()
        //     //     };
        //     
        //     _organization.ScrumMasters
        //
        //     team.ScrumMasterId = groupedScrumMasterCounts.OrderBy(p => p.count).First().scrumMaster;
        // }
    }

    private async Task CreateTeams()
    {
        _organization.Teams = new List<Team>(_addScrumOrganizationArgs.ScrumTeamCount);

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
        int teamMemberCount = _addScrumOrganizationArgs.TeamMemberCount;
        _organization.TeamMembers = new List<TeamMember>(teamMemberCount);

        await CreateT(_organization.TeamMembers, CreatePerson<TeamMember>);
    }
    
    private async Task CreateSupervisors()
    {
        int supervisorCount = _addScrumOrganizationArgs.SupervisorCount;
        _organization.Supervisors = new List<Supervisor>(supervisorCount);

        await CreateT(_organization.Supervisors, CreatePerson<Supervisor>);
    }

    private async Task CreateScrumMasters()
    {
        _organization.ScrumMasters = new List<ScrumMaster>(_addScrumOrganizationArgs.ScrumTeamCount);

        await CreateT(_organization.ScrumMasters, CreatePerson<ScrumMaster>);
    }

    private T CreatePerson<T>(int i) where T: IPerson, new()
    {
        return new T
        {
            Id = Guid.NewGuid(),
            OrganizationEmployeeNumber = $"EMP{i.GetZeroLeftPaddedNumber(6)}",
            OrganizationId = _organization.Id,
            FirstName = GenerateName(3, 7),
            LastName = GenerateName(2, 12)
        };
    }

    private static async Task CreateT<T>(List<T> list, Func<int, T> func) where T : class
    {
        await Task.Run(() =>
        {
            int listCount = list.Capacity;
            for (int i = 0; i < listCount; i++)
            {
                int number = Interlocked.Add(ref _universalCount, 1);
                T item = func(number);
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