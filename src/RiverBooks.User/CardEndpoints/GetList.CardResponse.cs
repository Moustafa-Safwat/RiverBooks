using RiverBooks.User.Dto;

namespace RiverBooks.User.CardEndpoints;

internal record CardResponse(List<CardItemDto> CardItems);
