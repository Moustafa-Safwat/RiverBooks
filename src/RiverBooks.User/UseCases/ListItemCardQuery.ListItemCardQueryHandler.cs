using Ardalis.Result;
using MediatR;
using RiverBooks.User.Dto;
using RiverBooks.User.Repository;

namespace RiverBooks.User.UseCases;

// this handelrar is responsible for get the email address of the user and return the list of cards for this user
internal class ListItemCardQueryHandler(
  IApplicationUserRepository applicationUserRepository
  )
  : IRequestHandler<ListItemCardQuery, Result<List<CardItemDto>>>
{

  public async Task<Result<List<CardItemDto>>> Handle(ListItemCardQuery request, CancellationToken cancellationToken)
  {
    var user = await applicationUserRepository.GetApplicationUSerByEmailAsync(request.UserEmailAddress, cancellationToken);
    if (user is null)
    {
      return Result.Unauthorized(["User doesn't exists"]);
    }
    return user.CardItems.Select(item => new CardItemDto(
      item.Id,
      item.BookId,
      item.Description,
      item.Quantity,
      item.Price
      )).ToList();
  }
}
