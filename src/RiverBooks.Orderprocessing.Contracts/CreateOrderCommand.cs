using System;
using System.Collections.Generic;
using Ardalis.Result;
using MediatR;

namespace RiverBooks.Orderprocessing.Contracts;

public record CreateOrderCommand(
Guid UserId,
Guid ShippingAddressId,
Guid BillingAddressId,
IList<OrderItemDetails> Items)
: IRequest<Result<OrderDetailsResponse>>;
