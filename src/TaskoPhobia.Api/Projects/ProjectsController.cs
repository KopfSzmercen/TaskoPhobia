using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskoPhobia.Application.Commands;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries;
using TaskoPhobia.Application.Security;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Exceptions.Errors;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Api.Projects;

[ApiController]
[Route("projects")]
public class ProjectsController : ControllerBase 
{
    private readonly ICommandDispatcher _commandDispatcher;
    // #CR nieużywana interfejs, do wyrzucenia
    private readonly ITokenStorage _tokenStorage;
    private readonly IQueryDispatcher _queryDispatcher;

    public ProjectsController(IQueryDispatcher queryDispatcher, ITokenStorage tokenStorage, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _tokenStorage = tokenStorage;
        _commandDispatcher = commandDispatcher;
    }

    [Authorize]
    [HttpPost]
    [SwaggerOperation("Create a project")]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    // #CR wskazywanie typu tupeof(void) lub typeof(error) jest zbędne
    // #CR nie używamy raczej bazowej klasy Error, dobrze jest mieć zbudowany własny middleware z swoją własną klasą  
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateProjectRequest request)
    {
        // #CR to można wydzielić sobie to interfejsu, gdzie jego implementacja pobierze sobie dane z tokenu, i użyć tego np już w handlerze
        var currentUserIdStr = User.Identity?.Name;
        
        // #CR jw. wydzielić sobie do metody w interfejsie, i jeśli nie ma userId zapisanego to raczej wyrzucić wyjątek, że user jest np niezalogowany
        if (string.IsNullOrWhiteSpace(currentUserIdStr))   return NotFound();

        var command = request.ToCommand(Guid.Parse(currentUserIdStr));
        await _commandDispatcher.DispatchAsync(command);
        
        return CreatedAtAction(nameof(Get), new {command.ProjectId}, null);
    }

    [Authorize]
    [HttpGet]
    [SwaggerOperation("Get all owned projects")]
    [ProducesResponseType(typeof(IEnumerable<ProjectDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> Get()
    {
        // #CR jw
        var currentUserIdStr = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(currentUserIdStr))   return NotFound();

        var query = new BrowseProjects(Guid.Parse(currentUserIdStr));
        var results = await _queryDispatcher.QueryAsync(query);

        return Ok(results);
    }

    [Authorize]
    [HttpGet("{projectId:guid}")]
    [SwaggerOperation("Get single owned project")]
    [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectDto>> Get([FromRoute]Guid projectId)
    {
        // #CR jw
        var currentUserIdStr = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(currentUserIdStr))   return NotFound();

        var query = new GetProject(Guid.Parse(currentUserIdStr), projectId);
        var project = await _queryDispatcher.QueryAsync(query);

        if (project is null) return NotFound();

        return Ok(project);
    }
}