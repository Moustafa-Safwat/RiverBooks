namespace RiverBooks.User.CardEndpoints;

internal record OrderCheckoutRequest(
  Guid ShippingAddressId,
  Guid BillingAddressId
  );
