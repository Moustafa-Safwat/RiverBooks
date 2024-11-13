﻿using Ardalis.GuardClauses;
using RiverBooks.Book.Configurations;
namespace RiverBooks.Book.Models;
internal class Book(Guid id, string title, string author, decimal price)
{
  public Guid Id { get; private set; } = Guard.Against.Default(id);
  public string Title { get; private set; } = Guard.Against.NullOrEmpty(title);
  public string Author { get; private set; } = Guard.Against.NullOrEmpty(author);
  public decimal Price { get; private set; } = Guard.Against.Negative(price);

  public void UpdatePrice(decimal price)
  {
    Price = Guard.Against.UpdatePrice(price);
  }
}
