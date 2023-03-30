using PremiumTravelService.Api.Models.Options;
using PremiumTravelService.Api.Services.DataStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .Configure<DataStorageOptions>(
        builder.Configuration.GetSection("DataStorage")
    );

builder.Services.AddTransient<IDataStorageService, DataStorageService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
