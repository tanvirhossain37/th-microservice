using System.Net.Mail;
using System.Reflection;
using MassTransit;
using TH.EmailMS.API;
using TH.EmailMS.API.Infra;
using TH.EventBus.Messages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Tanvir
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEmailRepo, EmailRepo>();
builder.Services.AddTransient<SmtpClient>();

//RabbitMQ Config
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<EmailEventConsumer>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration.GetSection("EventBus:Host").Value);
        cfg.ReceiveEndpoint(EventBus.EmailQueue, c =>
        {
            c.ConfigureConsumer<EmailEventConsumer>(ctx);
        });
    });
});

//AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


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

app.UseAuthorization();

app.MapControllers();

app.Run();
