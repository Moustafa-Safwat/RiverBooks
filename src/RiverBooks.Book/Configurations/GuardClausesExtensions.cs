using System.Runtime.CompilerServices;
using Ardalis.GuardClauses;


namespace RiverBooks.Book.Configurations;

internal static class GuardClausesExtensions
{
  public static decimal UpdatePrice(this IGuardClause _, decimal price
    , [CallerArgumentExpression(nameof(price))] string parameterName = "")
  {
    if (price < 0)
    {
      throw new ArgumentException($"{parameterName} should be greater than or equal to 0.");
    }
    return price;
  }
}
