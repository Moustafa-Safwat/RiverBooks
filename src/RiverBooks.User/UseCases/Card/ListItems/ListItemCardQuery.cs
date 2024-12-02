using Ardalis.Result;
using MediatR;
using RiverBooks.User.Dto;

namespace RiverBooks.User.UseCases.Card.ListItems;

internal record ListItemCardQuery(string UserEmailAddress) : IRequest<Result<List<CardItemDto>>>;
