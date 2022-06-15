using BackendTeamWebApiService.Models;

namespace BackendTeamWebApiService.Services;

/// <inheritdoc cref="IReportingService"/>
internal sealed class ReportingService : IReportingService
{
    private Organization Organization { get; set; } = null!;

    private readonly IScrumOrganizationService _scrumOrganizationService;
    
    private readonly ITeamService _teamService;
    
    private readonly ITeamMemberService _teamMemberService;
    
    private readonly IPersonService _personService;
    
    public ReportingService(IScrumOrganizationService scrumOrganizationService,
        ITeamService teamService,
        ITeamMemberService teamMemberService,
        IPersonService personService)
    {
        _scrumOrganizationService = scrumOrganizationService;
        _teamService = teamService;
        _teamMemberService = teamMemberService;
        _personService = personService;
    }
    
    public async Task WriteOrganizationReport(Guid organizationId)
    {
        await FetchOrganizationData(organizationId);
        FormReportStructure();
    }

    private async Task FetchOrganizationData(Guid organizationId)
    {
        Task<Organization> organizationTask = _scrumOrganizationService.GetScrumOrganizationByIdAsync(organizationId);
        Task<List<Team>> teamServiceTask = _teamService.GetTeamsByOrganizationId(organizationId);
        Task<List<TeamMember>> teamMemberServiceTask = _teamMemberService.GetTeamMembersByOrganizationId(organizationId);
        Task<List<Supervisor>> supervisorsTask = _personService.GetSupervisorsByOrganizationId(organizationId);
        Task<List<ScrumMaster>> scrumMastersTask = _personService.GetScrumMastersByOrganizationId(organizationId);

        await Task.WhenAll(organizationTask, teamServiceTask, teamMemberServiceTask, supervisorsTask, scrumMastersTask);

        Organization = organizationTask.Result;
        Organization.Teams = teamServiceTask.Result;
        Organization.TeamMembers = teamMemberServiceTask.Result;
        Organization.Supervisors = supervisorsTask.Result;
        Organization.ScrumMasters = scrumMastersTask.Result;
    }

    private void FormReportStructure()
    {
        var teamMembersReport = from team in Organization.Teams
            join teamMember in Organization.TeamMembers
                on team.OrganizationId equals teamMember.TeamId
                join sup in Organization.Supervisors
                on teamMember.SupervisorId equals  sup.Id
            select new
            {
                TeamMemberName = teamMember.LastName + ", " + teamMember.FirstName,
                TeamMemberEmpNum = teamMember.OrganizationEmployeeNumber,
                TeamName = team.TeamName,
                Supervisor = sup.LastName + ", " + sup.FirstName
            };

        var teamsReport = from team in Organization.Teams
            //join teamMember in Organization.TeamMembers
                //on team.OrganizationId equals teamMember.TeamId
            join scrumMaster in Organization.ScrumMasters
                on team.ScrumMasterId equals scrumMaster.Id
            select new
            {
                TeamName = team.TeamName,
                TeamNumber = team.TeamNumber,
                ScrumMasterName = scrumMaster.LastName + ", " + scrumMaster.FirstName,
                TeamMemberCount = Organization.TeamMembers.Count(p => p.TeamId == team.Id)
            };

        var combinedReport = from team in Organization.Teams
            join teamMember in Organization.TeamMembers
                on team.OrganizationId equals teamMember.TeamId
            join sup in Organization.Supervisors
                on teamMember.SupervisorId equals sup.Id
            join scrumMaster in Organization.ScrumMasters
                on team.ScrumMasterId equals scrumMaster.Id
            select new
            {
                OrgName = Organization.Name,
                TeamName = team.TeamName,
                TeamNumber = team.TeamNumber,
                ScrumMasterName = scrumMaster.LastName + ", " + scrumMaster.FirstName,
                TeamMemberName = teamMember.LastName + ", " + teamMember.FirstName,
                TeamMemberEmpNum = teamMember.OrganizationEmployeeNumber,
                Supervisor = sup.LastName + ", " + sup.FirstName
            };
    }
}