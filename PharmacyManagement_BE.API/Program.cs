using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Application.Extentions;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conn database 
builder.Services.AddDbContext<PharmacyManagementContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));


// Respositories
builder.Services.AddRepositoryExtension();

// MediaR
builder.Services.AddMediatRExtention();

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfileExtention).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
