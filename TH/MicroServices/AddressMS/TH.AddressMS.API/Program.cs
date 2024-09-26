using System.Reflection;
using TH.AddressMS.API;
using TH.AddressMS.App;
using TH.AddressMS.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//tanvir
//builder.Services.AddScoped<AddressDbContextSeed>();
builder.Services.AddAddressApplicationServices(builder.Configuration);
builder.Services.AddAddressAppEventBus(builder.Configuration);
builder.Services.AddAddressInfrastructureServices(builder.Configuration);
builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddHostedService<AppHostedService>();
/////////////////////////////////////////////

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
