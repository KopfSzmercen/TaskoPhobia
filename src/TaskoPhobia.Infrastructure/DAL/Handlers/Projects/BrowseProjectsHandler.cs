﻿using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.Projects;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.Projects;

internal sealed class BrowseProjectsHandler : IQueryHandler<BrowseProjects, IEnumerable<ProjectDto>>
{
    private readonly DbSet<ProjectReadModel> _projects;

    public BrowseProjectsHandler(TaskoPhobiaReadDbContext dbContext)
    {
        _projects = dbContext.Projects;
    }

    public async Task<IEnumerable<ProjectDto>> HandleAsync(BrowseProjects query)
    {
        var projects = _projects.AsNoTracking();

        if (query.Created) projects = projects.Where(x => x.OwnerId == query.UserId);

        else
            projects = projects.Where(x =>
                x.Participations.Any(p => p.ParticipantId == query.UserId));

        return await projects.Select(x => x.AsDto()).ToListAsync();
    }
}