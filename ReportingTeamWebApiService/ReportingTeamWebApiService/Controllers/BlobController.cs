namespace ReportingTeamWebApiService.Controllers;

using Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

/// <summary>
/// Blob Controller.
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public sealed class BlobController : ControllerBase
{
    private readonly ILogger<BlobController> _logger;
    
    /// <summary>
    /// Initializes a new instance of <see cref="BlobController"/>
    /// </summary>
    /// <param name="logger">The logger.</param>
    public BlobController(ILogger<BlobController> logger)
    {
        _logger = logger;
    }
}