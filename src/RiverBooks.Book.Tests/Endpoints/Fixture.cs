using FastEndpoints.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit.Abstractions;

namespace RiverBooks.Book.Tests.Endpoints;
/// <summary>
/// Represents a fixture for setting up and tearing down test resources for the RiverBooks.Book endpoints.
/// </summary>
public class Fixture : AppFixture<Program>
{
  /// <summary>
  /// Sets up the HTTP client and any other necessary resources before tests are executed.
  /// </summary>
  /// <returns>A task representing the asynchronous operation.</returns>
  protected override Task SetupAsync()
  {
    Client = CreateClient();
    return Task.CompletedTask;
  }

  /// <summary>
  /// Disposes the HTTP client and performs any necessary cleanup after tests are executed.
  /// </summary>
  /// <returns>A task representing the asynchronous operation.</returns>
  protected override Task TearDownAsync()
  {
    Client.Dispose();
    return base.TearDownAsync();
  }
}
