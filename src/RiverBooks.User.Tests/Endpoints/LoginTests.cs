using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.User.UserEndpoints;
using RiverBooks.USer.Tests.Endpoints;

namespace RiverBooks.User.Tests.Endpoints;
public class LoginTests(Fixture fixture) : TestBase<Fixture>
{
    [Fact]
    public async Task LoginUser_SuccessAsync()
    {
        // Arrange
        var userManager = fixture.Services.GetRequiredService<UserManager<IdentityUser>>();
        var existingUser = await userManager.FindByEmailAsync("loginuser@example.com");
        if (existingUser != null)
        {
            await userManager.DeleteAsync(existingUser);
        }

        var user = new IdentityUser { UserName = "loginuser", Email = "loginuser@example.com" };
        await userManager.CreateAsync(user, "Password123!");

        var request = new UserLoginRequest("loginuser@example.com", "Password123!");

        // Act
        var result = await fixture.Client.POSTAsync<UserEndpoints.Login, UserLoginRequest, TokenDto>(request);

        // Assert
        result.Response.EnsureSuccessStatusCode();
        result.Result.Token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task LoginUser_Failure_InvalidPasswordAsync()
    {
        // Arrange
        var userManager = fixture.Services.GetRequiredService<UserManager<IdentityUser>>();
        var existingUser = await userManager.FindByEmailAsync("loginuser@example.com");
        if (existingUser != null)
        {
            await userManager.DeleteAsync(existingUser);
        }

        var user = new IdentityUser { UserName = "loginuser", Email = "loginuser@example.com" };
        await userManager.CreateAsync(user, "Password123!");

        var request = new UserLoginRequest("loginuser@example.com", "WrongPassword!");

        // Act
        var result = await fixture.Client.POSTAsync<UserEndpoints.Login, UserLoginRequest, object>(request);

        // Assert
        result.Response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task LoginUser_Failure_UserNotFoundAsync()
    {
        // Arrange
        var request = new UserLoginRequest("nonexistentuser@example.com", "Password123!");

        // Act
        var result = await fixture.Client.POSTAsync<UserEndpoints.Login, UserLoginRequest, object>(request);

        // Assert
        result.Response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}
