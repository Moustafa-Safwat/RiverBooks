namespace RiverBooks.User.UserEndpoints;

internal record CreateUserRequest(
  string UserName,
  string Email,
  string Password,
  string Role
  );
