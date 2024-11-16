using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using Newtonsoft.Json;
using RiverBooks.Book.BookEndpoints;
using RiverBooks.Book.Dtos;

namespace RiverBooks.Book.Tests.Endpoints;
public class GetById(Fixture fixture) : TestBase<Fixture>
{
  [Theory]
  [InlineData("7c5d510e-173a-48f5-83c0-131bd6670fa1")]
  public async Task ReturnsBookSuccessfullyAsync(Guid id)
  {
    var bookId = id;
    var result = await fixture.Client.GETAsync<BookEndpoints.GetById, GetByIdRequest, BookDto>(new GetByIdRequest(bookId));
    var responseBody = await result.Response.Content.ReadAsStringAsync();
    var book = JsonConvert.DeserializeObject<BookDto>(responseBody);
    result.Response.EnsureSuccessStatusCode();
    book!.Should().NotBeNull();
    book!.Id.Should().Be(bookId);
  }

  [Fact]
  public async Task ReturnsNotFoundForNonExistentBookAsync()
  {
    var nonExistentBookId = Guid.NewGuid();
    var result = await fixture.Client.GETAsync<BookEndpoints.GetById, GetByIdRequest, BookDto>(new GetByIdRequest(nonExistentBookId));
    result.Response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
  }

  [Fact]
  public async Task ReturnsNotFoundForInvalidIdAsync()
  {
    var invalidBookId = Guid.Empty;
    var result = await fixture.Client.GETAsync<BookEndpoints.GetById, GetByIdRequest, BookDto>(new GetByIdRequest(invalidBookId));
    result.Response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
  }

  [Theory]
  [InlineData("7c5d510e-173a-48f5-83c0-131bd6670fa1")]
  public async Task ReturnsBookWithCorrectDetailsAsync(Guid id)
  {
    var bookId = id;
    var result = await fixture.Client.GETAsync<BookEndpoints.GetById, GetByIdRequest, BookDto>(new GetByIdRequest(bookId));
    var responseBody = await result.Response.Content.ReadAsStringAsync();
    var book = JsonConvert.DeserializeObject<BookDto>(responseBody);
    result.Response.EnsureSuccessStatusCode();
    book!.Should().NotBeNull();
    book!.Id.Should().Be(bookId);
    book.Title.Should().NotBeNullOrEmpty();
    book.Author.Should().NotBeNullOrEmpty();
    book.Price.Should().BeGreaterThan(0);
  }
}

