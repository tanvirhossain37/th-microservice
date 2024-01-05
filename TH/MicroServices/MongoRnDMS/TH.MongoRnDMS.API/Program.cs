using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Globalization;
using TH.MongoRnDMS.API;
using TH.MongoRnDMS.App;
using TH.MongoRnDMS.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

////////////////////// Tanvir /////////////////////////////

//builder.Services.AddControllers();
builder.Services.AddControllers(c => c.Filters.Add(new CustomExceptionFilter()));

////////////////////// End //////////////////////////////

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


////////////////////// Tanvir /////////////////////////////

//builder.Services.AddLocalization(opt => opt.ResourcesPath = "Languages");
//var localizationOptions = builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    var supportedCultures = new[] { "en-US", "bn-BD" };
//    options.SetDefaultCulture(supportedCultures[0])
//             .AddSupportedCultures(supportedCultures)
//             .AddSupportedUICultures(supportedCultures);
//});


builder.Services.AddApplicationServices();
builder.Services.AddInfraServices(builder.Configuration);

////////////////////// End //////////////////////////////

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

/******************************* Tanvir *******************************/

//app.UseRequestLocalization();

/******************************* End *********************************/

app.Run();
