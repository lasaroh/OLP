using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OLP_API.Data;
using System.Net;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OLP_DbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("OLP_DbContext") ?? throw new InvalidOperationException("Connection string 'OLP_DbContext' not found.")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
