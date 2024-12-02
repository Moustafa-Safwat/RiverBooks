namespace RiverBooks.Orderprocessing.Entities;

/// <summary>
/// Represents a physical address.
/// </summary>
/// <param name="Street1">The primary street address.</param>
/// <param name="Street2">The secondary street address (optional).</param>
/// <param name="City">The city of the address.</param>
/// <param name="State">The state or province of the address.</param>
/// <param name="PostalCode">The postal or ZIP code of the address.</param>
/// <param name="Country">The country of the address.</param>
internal record Address(
  string Street1,
  string Street2,
  string City,
  string State,
  string PostalCode,
  string Country
  );
