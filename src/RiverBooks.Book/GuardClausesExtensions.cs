using System.Runtime.CompilerServices;

#pragma warning disable IDE0130
namespace Ardalis.GuardClauses;
#pragma warning restore IDE0130

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
