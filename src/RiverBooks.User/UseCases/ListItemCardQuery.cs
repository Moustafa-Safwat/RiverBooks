using Ardalis.Result;
using MediatR;
using RiverBooks.User.Dto;

namespace RiverBooks.User.UseCases;

internal record ListItemCardQuery(string UserEmailAddress) : IRequest<Result<List<CardItemDto>>>;
