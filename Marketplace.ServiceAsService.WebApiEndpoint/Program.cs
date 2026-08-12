using Marketplace.Application.Services;
using Marketplace.Application.Services.Contracts;
using Marketplace.EfCore;
using Marketplace.RepositoryDesignPattern.Services.Contracts;
using Marketplace.RepositoryDesignPattern.Services.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region [- AddDbContext<>() -]
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<MarketplaceDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<MarketplaceDbContext>();
#endregion

#region [- AddScoped<>() -]

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryApplicationService, CategoryApplicationService>();

#endregion

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
