using AutoMapper;
using Core.Interfaces;
using Infrastructure.Respository;
using Infrastructure.UOW;
using Microsoft.EntityFrameworkCore;
using Service.Automapper;
using Service.Service;
using Microsoft.Extensions.DependencyInjection;
using Service.Interface;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using BaseServices.DI;
using BaseServices.Prometheus;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
// init autofac services factory
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BaseServicesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'PMSDbContext' not found.")));


//builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
//builder.Services.AddTransient<IProjectService, ProjectService>();

// autofac container
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new RepositoryHandlerModule()));

builder.Host.UseMetricsWebTracking();

builder.Services.AddApiVersioning();

builder.Services.AddSingleton<MetricReporter>();
builder.Services.AddMetrics();
builder.Services.AddMetricsTrackingMiddleware();


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

app.UseMetricServer();
app.UseMiddleware<ResponseMetricMiddleware>();

app.MapControllers();

app.Run();
