using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using RiverBooks.Books.Contracts.Queries;
using RiverBooks.User.Repository;

namespace RiverBooks.User.UseCases;
internal class AddCardItemCommandHandler(
  IApplicationUserRepository applicationUserRepository,
  ISender sender
  )
  : IRequestHandler<AddCardItemCommand, Result>
{
  public async Task<Result> Handle(AddCardItemCommand request, CancellationToken cancellationToken)
  {
    // check the user is exists in the db
    var user = await applicationUserRepository.GetApplicationUSerByEmailAsync(request.EmailAddress, cancellationToken);
    if (user is null)
    {
      return Result.Unauthorized();
    }
    // get the books details from the book module
    var book = await sender.Send(new BookDetailsQuery(request.BookId), cancellationToken);
    if (book.Status == ResultStatus.NotFound)
    {
      return Result.NotFound();
    }
    var bookDetails = book.Value;
    string description = $"{bookDetails.Title} by {bookDetails.Author}";

    user.AddCardItem(new Data.CardItem(bookDetails.BookId, description, bookDetails.UnitPrice, request.Quantity));
    await applicationUserRepository.SaveChangesAsync(cancellationToken);
    return Result.Success();
  }
}


