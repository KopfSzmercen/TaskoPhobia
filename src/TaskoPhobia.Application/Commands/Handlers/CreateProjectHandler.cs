using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Handlers;

public sealed class CreateProjectHandler : ICommandHandler<CreateProject>
{
    private readonly IUserRepository _userRepository;

    public CreateProjectHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task HandleAsync(CreateProject command)
    {
        var ownerId = new UserId(command.OwnerId);
        var user = await _userRepository.GetByIdAsync(ownerId);

        if (user is null) throw new UserNotFoundException();
        // #CR po co w ogole wyjmowanie tego usera żeby sprawdzić czy istnieje, w systemie powinniśmy mieć pewną doze zaufania, jeśli nasz serwis autoryzacyjny wystawił token i jest tam userId to w takim razie bez sensu tu pobierać i sprawdzać czy istnieje
// #CR po za tym pobierasz całego usera żeby sprawdzić czy istnieje na bazie, a można np zrobić zapytanie Users.AnyAsync(x => x.id == id) i w ten sposób zwraca nam boola czy user istnieje lub nie
        
        var projectId = new ProjectId(command.ProjectId);
        var projectName = new ProjectName(command.ProjectName);
        var projectDescription = new ProjectDescription(command.ProjectDescription);
        var projectProgressStatus = ProgressStatus.InProgress();
// #CR usunąć puste linie
// #CR nie ma potrzeby wyżej tworzyć te nowe obiekty w taki sposób, jeśli konstruktor projektu przyjmuje np ProjectName a on ma nadpisane operatory to możesz bezpośrednio wstawić jakiegoś stringa a on zostanie przekonwertowany do tej klasy, w ten sposób na dobrą sprawe skrróci się handler o 4 linijki
        var project = new Project(projectId, projectName, projectDescription, projectProgressStatus, DateTime.UtcNow,
            ownerId);
        
        user.AddProject(project);

        await _userRepository.UpdateAsync(user);
    }
}