using Microsoft.AspNetCore.Mvc;

namespace BackendTeamWebApiService.Controllers;

/// <summary>
/// Health Controller.
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public sealed class HealthController : ControllerBase
{
    /// <summary>
    /// Pings the dependency injected organization service.
    /// </summary>
    /// <returns><see cref="bool"/>True</returns>
    [HttpGet]
    [Produces(typeof(bool))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public bool Health()
    {
        return true;
    }
}