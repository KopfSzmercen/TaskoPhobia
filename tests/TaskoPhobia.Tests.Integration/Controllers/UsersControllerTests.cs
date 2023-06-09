﻿using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Shouldly;
using TaskoPhobia.Application.Commands.Users.SignIn;
using TaskoPhobia.Application.Commands.Users.SignUp;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Infrastructure.Security;
using TaskoPhobia.Shared.Abstractions.Exceptions.Errors;
using TaskoPhobia.Shared.Time;
using Xunit;

namespace TaskoPhobia.Tests.Integration.Controllers;

public class UsersControllerTests : ControllerTests, IDisposable
{
    [Fact]
    public async Task given_valid_command_sign_up_should_return_created_201_code()
    {
        var request = new SignUpRequest { Password = Password, Email = "test@t.pl", Username = "test" };

        var response = await HttpClient.PostAsJsonAsync("users/sign-up", request);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        response.Headers.Location.ShouldNotBeNull();
    }

    [Fact]
    public async Task given_valid_credentials_post_sign_in_should_return_200_ok_status_and_jwt()
    {
        var user = await CreateUserAsync();

        var request = new SignInRequest { Email = user.Email, Password = Password };
        var response = await HttpClient.PostAsJsonAsync("users/sign-in", request);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var jwt = await response.Content.ReadFromJsonAsync<JwtDto>();
        jwt.ShouldNotBeNull();
        jwt.AccessToken.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task given_valid_jwt_get_users_should_return_ok_200_and_user_data()
    {
        var user = await CreateUserAsync();
        Authorize(user.Id, user.Role);

        var response = await HttpClient.GetAsync("users/me");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var userDto = await response.Content.ReadFromJsonAsync<UserDetailsDto>();

        userDto.Id.ShouldBe(user.Id.Value);
        userDto.Email.ShouldBe(user.Email.Value);
    }

    [Fact]
    public async Task given_email_already_in_use_post_sign_up_should_return_400_bad_request_email_in_use_error()
    {
        var user = await CreateUserAsync();
        var request = new SignUpRequest { Email = user.Email, Password = Password, Username = "username" };
        var response = await HttpClient.PostAsJsonAsync("users/sign-up", request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errorResult = response.Content.ReadFromJsonAsync<Error>().Result;

        errorResult.ShouldBeOfType<Error>();
    }

    [Fact]
    public async Task given_invalid_credentials_post_sign_in_should_return_400_invalid_credentials_error()
    {
        var user = await CreateUserAsync();
        var request = new SignInRequest { Email = "dfs{user.Email.Value}", Password = user.Password.Value };
        var response = await HttpClient.PostAsJsonAsync("users/sign-in", request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errorResult = response.Content.ReadFromJsonAsync<Error>().Result;

        errorResult.ShouldBeOfType<Error>();
    }

    #region setup

    private const string Password = "secret";
    private readonly TestDatabase _testDatabase;

    public UsersControllerTests(OptionsProvider optionsProvider) : base(optionsProvider, new Clock())
    {
        _testDatabase = new TestDatabase();
    }

    public void Dispose()
    {
        _testDatabase.Dispose();
    }

    private async Task<User> CreateUserAsync()
    {
        var passwordManager = new PasswordManager(new PasswordHasher<object>());
        var securedPassword = passwordManager.Secure(Password);

        var user = new User(Guid.NewGuid(), "test@t.pl",
            "test", securedPassword, Role.User(), DateTime.UtcNow, AccountType.Free());

        await _testDatabase.WriteDbContext.Users.AddAsync(user);
        await _testDatabase.WriteDbContext.SaveChangesAsync();

        return user;
    }

    #endregion
}