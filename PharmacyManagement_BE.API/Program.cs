using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PharmacyManagement_BE.API.Configs;
using PharmacyManagement_BE.Application.Extentions;
using PharmacyManagement_BE.Application.Filters;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.Extentions;
using StackExchange.Redis;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Swagger Config
builder.Services.AddSwaggerSetup();

// Conn database 
builder.Services.AddDbContext<PharmacyManagementContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

// Redis
//builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration["Redis:ConnectionString"]));

// Identity Config
builder.Services.AddIdentityConfig(builder.Configuration);

// Respositories Extention
builder.Services.AddRepositoryExtension();

// PMAuthorizeActionFilter
//builder.Services.AddScoped<PMAuthorizeActionFilter>();

// MediaR Extention
builder.Services.AddMediatRExtention();

// AutoMapper Extention
builder.Services.AddAutoMapper(typeof(AutoMapperProfileExtention).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
