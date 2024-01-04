using MongoDB.Driver;
using TH.MongoRnDMS.App;
using TH.MongoRnDMS.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


////////////////////// Tanvir /////////////////////////////

//builder.Services.AddSingleton<IMongoClient>();

builder.Services.AddApplicationServices();
builder.Services.AddInfraServices(builder.Configuration);

////////////////////// End /////////////////////////////

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
