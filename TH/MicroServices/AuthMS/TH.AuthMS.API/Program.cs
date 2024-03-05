using TH.AuthMS.API;
using TH.AuthMS.App;
using TH.AuthMS.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllers();
//Tanvir
builder.Services.AddControllers(c => c.Filters.Add(new CustomExceptionFilter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Tanvir
builder.Services.AddAppDependencyInjection(builder.Configuration);
builder.Services.AddInfraDependencyInjection(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Tanvir
app.UseAuthentication();// MUST be before app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
