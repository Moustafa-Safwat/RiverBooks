using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RiverBooks.Book.Data;

namespace RiverBooks.Book.Tests.Endpoints;
public class List(Fixture fixture) : TestBase<Fixture>
{
  [Fact]
  public async Task ReturnsBooksSuccessfullyAsync()
  {
    var result = await fixture.Client.GETAsync<BookEndpoints.List, GetBooksRequest, GetBooksDto>(new GetBooksRequest(1, 10));
    var responseBody = await result.Response.Content.ReadAsStringAsync();
    var books = JsonConvert.DeserializeObject<GetBooksDto>(responseBody);
    result.Response.EnsureSuccessStatusCode();
    books!.Should().NotBeNull();
    books!.Books.Should().NotBeEmpty();
  }

  [Fact]
    public async Task ReturnsEmptyListWhenNoBooksAsync()
    {
        using var scope = fixture.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookDbContext>();
        var allBooksInDb = dbContext.Books.ToList();

        // Remove all books from the database
        dbContext.Books.RemoveRange(allBooksInDb);
        await dbContext.SaveChangesAsync();

        // Perform the GET request
        var result = await fixture.Client.GETAsync<BookEndpoints.List, GetBooksRequest, GetBooksDto>(new GetBooksRequest(1, 10));
        var responseBody = await result.Response.Content.ReadAsStringAsync();
        var books = JsonConvert.DeserializeObject<GetBooksDto>(responseBody);

        // Ensure the response is successful and the list is empty
        result.Response.EnsureSuccessStatusCode();
        books!.Should().NotBeNull();
        books!.Books.Should().BeEmpty();

        // Restore the original books to the database
        dbContext.Books.AddRange(allBooksInDb);
        await dbContext.SaveChangesAsync();
    }

  [Fact]
  public async Task ReturnsCorrectNumberOfBooksAsync()
  {
    var result = await fixture.Client.GETAsync<BookEndpoints.List, GetBooksRequest, GetBooksDto>(new GetBooksRequest(1, 5));
    var responseBody = await result.Response.Content.ReadAsStringAsync();
    var books = JsonConvert.DeserializeObject<GetBooksDto>(responseBody);
    result.Response.EnsureSuccessStatusCode();
    books!.Should().NotBeNull();
    books!.Books.Count.Should().Be(5);
  }

  [Fact]
  public async Task ReturnsBooksWithCorrectDetailsAsync()
  {
    var result = await fixture.Client.GETAsync<BookEndpoints.List, GetBooksRequest, GetBooksDto>(new GetBooksRequest(1, 10));
    var responseBody = await result.Response.Content.ReadAsStringAsync();
    var books = JsonConvert.DeserializeObject<GetBooksDto>(responseBody);
    result.Response.EnsureSuccessStatusCode();
    books!.Should().NotBeNull();
    books!.Books.Should().AllSatisfy(book =>
    {
      book.Id.Should().NotBeEmpty();
      book.Title.Should().NotBeNullOrEmpty();
      book.Author.Should().NotBeNullOrEmpty();
      book.Price.Should().BeGreaterThan(0);
      book.RowVersion.Should().NotBeEmpty();
    });
  }

  [Theory]
  [InlineData(-1,-10)]
  [InlineData(-1,10)]
  [InlineData(1, -10)]
  [InlineData(1, 1000)]
  public async Task ReturnsBadRequestForInvalidRequestAsync(int pageNumber, int pageSize)
  {
    var result = await fixture.Client.GETAsync<BookEndpoints.List, GetBooksRequest, GetBooksDto>(new GetBooksRequest(pageNumber, pageSize));
    result.Response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
  }
}
