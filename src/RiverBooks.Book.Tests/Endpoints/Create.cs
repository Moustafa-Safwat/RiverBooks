using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using Newtonsoft.Json;
using RiverBooks.Book.Dtos;

namespace RiverBooks.Book.Tests.Endpoints;
public class Create(Fixture fixture) : TestBase<Fixture>
{
    [Fact]
    public async Task CreatesBookSuccessfullyAsync()
    {
        var newBook = new BookDto(Guid.NewGuid(), "New Book", "Author", 19.99m);
        var result = await fixture.Client.POSTAsync<BookEndpoints.Create, BookDto, BookDto>(newBook);
        var responseBody = await result.Response.Content.ReadAsStringAsync();
        var createdBook = JsonConvert.DeserializeObject<BookDto>(responseBody);
        result.Response.EnsureSuccessStatusCode();
        createdBook!.Should().NotBeNull();
        createdBook!.Title.Should().Be(newBook.Title);
        createdBook!.Author.Should().Be(newBook.Author);
        createdBook!.Price.Should().Be(newBook.Price);
    }

    [Fact]
    public async Task ReturnsBadRequestForInvalidBookAsync()
    {
        var invalidBook = new BookDto(Guid.NewGuid(), "", "", -1);
        var result = await fixture.Client.POSTAsync<BookEndpoints.Create, BookDto, BookDto>(invalidBook);
        result.Response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ReturnsCreatedBookWithCorrectDetailsAsync()
    {
        var newBook = new BookDto(Guid.NewGuid(), "Detailed Book", "Author", 29.99m);
        var result = await fixture.Client.POSTAsync<BookEndpoints.Create, BookDto, BookDto>(newBook);
        var responseBody = await result.Response.Content.ReadAsStringAsync();
        var createdBook = JsonConvert.DeserializeObject<BookDto>(responseBody);
        result.Response.EnsureSuccessStatusCode();
        createdBook!.Should().NotBeNull();
        createdBook!.Title.Should().Be(newBook.Title);
        createdBook!.Author.Should().Be(newBook.Author);
        createdBook!.Price.Should().Be(newBook.Price);
    }
}
