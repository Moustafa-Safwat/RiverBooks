using System.Reflection;
using System.Security.Claims;
using System.Text;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using RiverBooks.Book.Configurations;
using RiverBooks.User.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

IList<Assembly> assemblies = [typeof(Program).Assembly];
// Register Books Services
builder.Services.RegisterBookServices(builder.Configuration, assemblies)
                .RegisterUserServices(builder.Configuration, assemblies)
                .AddAuthenticationJwtBearer(options =>
                {
                  options.SigningKey = builder.Configuration["Auth:JwtSecret"];
                })
                .AddAuthorization()
                .AddFastEndpoints()
                .AddSwaggerGen();

// Have to define the Auth schema of the FastEndpoints in the Authentication
builder.Services.AddAuthentication(options =>
{
  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
});

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(assemblies.ToArray()));

builder.Host.UseSerilog((context, configuration) =>
  configuration.ReadFrom.Configuration(context.Configuration));


var app = builder.Build();

app.UseSerilogRequestLogging(); // Automatic HTTP requests logging

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
app.UseHttpsRedirection();
app.UseAuthentication()
  .UseAuthorization();

app.UseFastEndpoints();

app.Run();

public partial class Program { } // For tesing only


