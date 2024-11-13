using RiverBooks.Book;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Register Books Services
builder.Services.RegisterBookServices();
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


