using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.User.Data;
using RiverBooks.User.UserEndpoints;
using RiverBooks.USer.Tests.Endpoints;

namespace RiverBooks.User.Tests.Endpoints;

public class CreateTests(Fixture fixture) : TestBase<Fixture>
{
  [Fact]
  public async Task CreateUser_SuccessAsync()
  {
    // Arrange
    var userManager = fixture.Services.GetRequiredService<UserManager<ApplicationUser>>();
    var existingUser = await userManager.FindByNameAsync("testuser");
    if (existingUser != null)
    {
      await userManager.DeleteAsync(existingUser);
    }

    var request = new CreateUserRequest("testuser", "testuser@example.com", "Password123!", "User");

    // Act
    var result = await fixture.Client.POSTAsync<UserEndpoints.Create, CreateUserRequest, object>(request);
    var user = await userManager.FindByNameAsync(request.UserName);

    // Assert
    result.Response.EnsureSuccessStatusCode();
    user.Should().NotBeNull();
    (await userManager.CheckPasswordAsync(user!, request.Password)).Should().BeTrue();
    (await userManager.IsInRoleAsync(user!, request.Role)).Should().BeTrue();
  }

  [Fact]
  public async Task CreateUser_Failure_UserAlreadyExistsAsync()
  {
    // Arrange
    var userManager = fixture.Services.GetRequiredService<UserManager<ApplicationUser>>();
    var existingUser = new ApplicationUser { UserName = "existinguser", Email = "existinguser@example.com" };
    await userManager.CreateAsync(existingUser, "Password123!");

    var request = new CreateUserRequest("existinguser", "existinguser@example.com", "Password123!", "User");

    // Act
    var result = await fixture.Client.POSTAsync<UserEndpoints.Create, CreateUserRequest, object>(request);

    // Assert
    result.Response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
  }

  [Fact]
  public async Task CreateUser_Failure_InvalidRoleAsync()
  {
    // Arrange
    var request = new CreateUserRequest("newuser", "newuser@example.com", "Password123!", "InvalidRole");

    // Act
    var result = await fixture.Client.POSTAsync<UserEndpoints.Create, CreateUserRequest, object>(request);

    // Assert
    result.Response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
  }
}
