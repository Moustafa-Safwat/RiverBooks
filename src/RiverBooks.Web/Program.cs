using FastEndpoints;
using FastEndpoints.Security;
using RiverBooks.Book.Configurations;
using RiverBooks.User.Configurations;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// Register Books Services
builder.Services.RegisterBookServices(builder.Configuration)
                .RegisterUserServices(builder.Configuration)
                .AddAuthenticationJwtBearer(options =>
                {
                  options.SigningKey = builder.Configuration["Auth:JwtSecret"];
                })
                .AddAuthorization()
                .AddSwaggerGen()
                .AddFastEndpoints();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

if (app.Environment.EnvironmentName == "Testing")
{ // Ensure User secrets is seen in Testing Environment
  // To can access the user secrets in the test environment, you need to add the following line to the Program.cs file.
  builder.Configuration.AddUserSecrets<Program>();
}
//app.UseHttpsRedirection();
app.UseAuthentication()
  .UseAuthorization();

app.UseFastEndpoints();

app.Run();

public partial class Program { } // For tesing only


