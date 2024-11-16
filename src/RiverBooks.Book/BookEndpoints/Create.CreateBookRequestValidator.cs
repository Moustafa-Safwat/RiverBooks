using FastEndpoints;
using FluentValidation;
using RiverBooks.Book.Data;

namespace RiverBooks.Book.BookEndpoints;
internal class CreateBookRequestValidator : Validator<CreateBookRequest>
{
  public CreateBookRequestValidator()
  {
    RuleFor(x => x.Author)
      .NotEmpty().WithMessage("Author is required.")
      .MaximumLength(DbEntitiesConfigurations.DEFAULT_MAX_LENGTH)
      .WithMessage($"Author cannot exceed {DbEntitiesConfigurations.DEFAULT_MAX_LENGTH} characters.");

    RuleFor(x => x.Title)
      .NotEmpty().WithMessage("Title is required.")
      .MaximumLength(DbEntitiesConfigurations.DEFAULT_MAX_LENGTH)
      .WithMessage($"Title cannot exceed {DbEntitiesConfigurations.DEFAULT_MAX_LENGTH} characters.");

    RuleFor(x => x.Price)
      .GreaterThanOrEqualTo(0)
      .WithMessage("Price must be greater than or equal to zero.");
  }
}
