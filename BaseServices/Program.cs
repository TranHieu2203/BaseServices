using AutoMapper;
using Core.Interfaces;
using Infrastructure.Respository;
using Infrastructure.UOW;
using Microsoft.EntityFrameworkCore;
using Service.Automapper;
using Service.Service;
using Microsoft.Extensions.DependencyInjection;
using Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PMSDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'PMSDbContext' not found.")));


builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<ICustommerService, CustommerService>();


builder.Services.AddApiVersioning();
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});

var mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);



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
