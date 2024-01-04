using CallCenterApi.Infrastructure.DB;
using CallCenterApi.Infrastructure.DB.Repositories;
using CallCenterApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAgentInfoManagementService, AgentInfoManagementService>();
builder.Services.AddScoped<IAgentInfoRepository, AgentInfoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var database = builder.Configuration.GetValue<string>("Database");

switch (database)
{
    case "PostgreSql":
        builder.Services.AddScoped<IDatabaseService, PostgresDatabaseService>();
        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("PostgreSQL"));
        });
        break;
    case "MongoDb":
        builder.Services.AddScoped<IDatabaseService, MongoDBDatabaseService>();
        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("MongoDB"));
        });
        break;
}

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
