using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using Newtonsoft.Json;

namespace RiverBooks.Book.Tests.Endpoints;
public class List(Fixture fixture) : TestBase<Fixture>
{
  [Fact]
  public async Task ReturnsFiveBooksAsync()
  {
    var result = await fixture.Client.GETAsync<BookEndpoints.List, GetBooksRequest, GetBooksDto>(new GetBooksRequest(1, 10));
    var responseBody = (await result.Response.Content.ReadAsStringAsync());
    var books = JsonConvert.DeserializeObject<GetBooksDto>(responseBody);
    result.Response.EnsureSuccessStatusCode();
    books!.Books.Count.Should().Be(7);
    books!.Should().NotBeNull();
  }
}
