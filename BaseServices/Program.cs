using AutoMapper;
using Infrastructure.Interfaces;
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
using Base.API;
using Base.DataContractCore.Base;
using Newtonsoft.Json.Converters;
using Base.Common.Helpers;

var builder = WebApplication.CreateBuilder(args);
// init autofac services factory
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(op =>
{
    op.SerializerSettings.ContractResolver = new LowercaseContractResolver();
    op.SerializerSettings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = AppConfigHelper.AppSetting.DataSettings.DateTimeFormat });
    op.SerializerSettings.DateFormatString = AppConfigHelper.AppSetting.DataSettings.DateTimeFormat;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<BaseServicesContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'PMSDbContext' not found.")));


//builder.Services.AddDbContext<BaseServicesContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'PMSDbContext' not found.")));

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
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

startup.Configure(app, builder.Environment);

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
