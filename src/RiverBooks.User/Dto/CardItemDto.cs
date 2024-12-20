﻿namespace RiverBooks.User.Dto;

internal record CardItemDto(
  Guid Id,
  Guid BookId,
  string Description,
  int Quantity,
  decimal UnitPrice
  );
