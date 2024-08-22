using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using PharmacyManagement_BE.API.Configs;
using PharmacyManagement_BE.Application.Extentions;
using PharmacyManagement_BE.Application.Filters;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.DBContext.Dapper;
using PharmacyManagement_BE.Infrastructure.Extentions;
using StackExchange.Redis;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("http://localhost:4200", "http://localhost:4201") // Thay đổi theo origin của bạn
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Swagger Config
builder.Services.AddSwaggerSetup();

// Conn Database By EntityFramewoek
builder.Services.AddDbContext<PharmacyManagementContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

// Dapper
builder.Services.AddScoped<PMDapperContext>();

// Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration["Redis:ConnectionString"]));

// Identity Config
builder.Services.AddIdentityConfig(builder.Configuration);

// Respositories Extention
builder.Services.AddRepositoryExtension();

// MediaR Extention
builder.Services.AddMediatRExtention();

// AutoMapper Extention
builder.Services.AddAutoMapper(typeof(AutoMapperProfileExtention).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/Customer/swagger.json", "Customer API");
        options.SwaggerEndpoint("/swagger/Admin/swagger.json", "Admin API");
    });

    app.UseCors("AllowSpecificOrigin"); // Sử dụng CORS
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    app.UseMiddleware<TokenValidationExtention>();
    app.MapControllers();
});

// Setup upload image
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Photos")),
//    RequestPath = "/Photos"
//});

app.Run();
