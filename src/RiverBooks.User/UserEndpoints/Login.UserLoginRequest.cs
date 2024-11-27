namespace RiverBooks.User.UserEndpoints;

internal record UserLoginRequest(
  string Email,
  string Password
  );
