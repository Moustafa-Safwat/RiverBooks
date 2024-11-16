#pragma warning disable IDE0130
using FastEndpoints;
using FluentValidation;

namespace RiverBooks.Book;

internal class GetBooksRequestValidator : Validator<GetBooksRequest>
{
    public GetBooksRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0.");
        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .WithMessage("PageSize must be between 1 and 100.");
    }
}
