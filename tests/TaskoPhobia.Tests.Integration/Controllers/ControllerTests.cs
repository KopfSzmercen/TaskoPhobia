﻿using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Security;
using TaskoPhobia.Infrastructure.Auth;
using TaskoPhobia.Shared.Abstractions.Time;
using Xunit;

namespace TaskoPhobia.Tests.Integration.Controllers;

[Collection("api")]
public abstract class ControllerTests : IClassFixture<OptionsProvider>
{
    private readonly IAuthenticator _authenticator;

    protected ControllerTests(OptionsProvider optionsProvider, IClock clock)
    {
        var app = new TaskoPhobiaTestApp(ConfigureServices);
        HttpClient = app.Client;
        var authOptions = optionsProvider.Get<AuthOptions>("auth");

        _authenticator = new Authenticator(new OptionsWrapper<AuthOptions>(authOptions), clock);
    }

    protected HttpClient HttpClient { get; }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
    }

    protected JwtDto Authorize(Guid userId, string role)
    {
        var jwt = _authenticator.CreateToken(userId, role);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

        return jwt;
    }
}