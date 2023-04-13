using PremiumTravelService.Api.Models.Options;
using PremiumTravelService.Api.Services.DataStorage;
using PremiumTravelService.Api.Services.Singleton;
using PremiumTravelService.Api.Services.StateMachine;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .Configure<DataStorageOptions>(
        builder.Configuration.GetSection("DataStorage")
    );

builder.Services.AddTransient<IDataStorageService, DataStorageService>();
builder.Services.AddTransient<ISingletonService, SingletonService>();
builder.Services.AddScoped<IStateMachineService, StateMachineService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("*");
    }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
