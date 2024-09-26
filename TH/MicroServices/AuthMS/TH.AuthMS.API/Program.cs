using System.Reflection;
using MassTransit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using TH.AuthMS.API;
using TH.AuthMS.App;
using TH.AuthMS.Infra;
using TH.CompanyMS.API;
using TH.EventBus.Messages;
using TH.Common.Model;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllers();
//Tanvir
builder.Services.AddControllers(c => c.Filters.Add(new CommonCustomExceptionFilter()));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Tanvir
builder.Services.AddAuthAppDependencyInjection(builder.Configuration);
builder.Services.AddAuthInfraDependencyInjection(builder.Configuration);
builder.Services.AddAuthJwtTokenBasedAuthentication(builder.Configuration);
builder.AddLog4NetDependency(builder.Configuration);
//builder.Services.AddCookieBasedAuthentication(builder.Configuration);

//AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//builder.Services.AddAuthentication(options =>
//    {
//        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
//    })
//    .AddCookie()
//    .AddGoogle(opt =>
//    {
//        opt.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//        opt.ClientId = builder.Configuration["Google:ClientId"];
//        opt.ClientSecret = builder.Configuration["Google:ClientSecret"];
//    });


//RabbitMQ Config
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<UserCreateEventConsumer>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration.GetSection("EventBus:Host").Value);
        cfg.ReceiveEndpoint(EventBus.UserCreateQueue, c =>
        {
            c.ConfigureConsumer<UserCreateEventConsumer>(ctx);
        });
    });
});

const string CorsPolicy = "_corsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsPolicy,
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

//google auth
//builder.Services.AddAuthentication().AddGoogle(options =>
//    {
//        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
//        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
//    }
//);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//tanvir
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

//Tanvir
app.UseAuthentication();// MUST be before app.UseAuthorization();
app.UseCors(CorsPolicy);//tanvir
app.UseAuthorization();

app.MapControllers();
app.Run();
