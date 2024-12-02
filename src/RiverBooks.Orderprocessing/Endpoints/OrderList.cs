using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Orderprocessing.UseCases;

namespace RiverBooks.Orderprocessing.Endpoints;
internal class OrderList(
  ISender sender
  )
  : Endpoint<GetOrderListRequest, IList<OrderListDto>>
{
  public override void Configure()
  {
    Get("/orders");
    Claims(ClaimTypes.Email, ClaimTypes.NameIdentifier);
    Roles("User");
  }

  public override async Task HandleAsync(GetOrderListRequest request, CancellationToken ct)
  {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

    var query = new OrderListForUserQuery(Guid.Parse(userId!), request.PageNumber, request.PageSize);

    var result = await sender.Send(query);

    if (result.Status == ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync(ct);
    }
    else
    {
      await SendOkAsync(result.Value.ToList());
    }
  }
}
