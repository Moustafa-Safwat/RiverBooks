using FastEndpoints;
using RiverBooks.Book.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Register Books Services
builder.Services.RegisterBookServices(builder.Configuration);
builder.Services.AddFastEndpoints();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseFastEndpoints();
app.Run();

public partial class Program { } // For tesing only


