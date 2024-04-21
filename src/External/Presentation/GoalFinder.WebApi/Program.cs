using Microsoft.AspNetCore.Builder;
using System.Text;
using System;
using FastEndpoints;
using GoalFinder.WebApi;
using Microsoft.IdentityModel.JsonWebTokens;
using GoalFinder.MySqlRelationalDb;
using GoalFinder.Application;
using GoalFinder.Application.Shared.FIleObjectStorage;
using GoalFinder.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using GoalFinder.MySqlRelationalDb.Data;
using GoalFinder.ImageCloudinary;
using GoalFinder.AppJsonWebToken;
using FastEndpoints.Swagger;
using GoalFinder.RedisCachingDb;

// Default setting.
AppContext.SetSwitch(
    switchName: "Npgsql.DisableDateTimeInfinityConversions",
    isEnabled: true);
Console.OutputEncoding = Encoding.UTF8;
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

// Add services to the container.
var builder = WebApplication.CreateBuilder(args: args);

var services = builder.Services;
var config = builder.Configuration;

services.ConfigWebApi(configuration: config);
services.ConfigApplication();
services.ConfigMySqlRelationalDatabase(configuration: config);
services.ConfigCloudinaryImageStorage();
services.ConfigAppJwtIdentityService();
services.AddRedisCachingDatabase(configuration: config);

var app = builder.Build();

// Data seeding.
await using (var scope = app.Services.CreateAsyncScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GoalFinderContext>();

    // Can database be connected.
    var canConnect = await context.Database.CanConnectAsync();

    // Database cannot be connected.
    if (!canConnect)
    {
        throw new HostAbortedException(message: "Cannot connect database.");
    }

    // Try seed data.
    var seedResult = await GoalFinderDataSeeding.SeedAsync(
        context: context,
        userManager: scope.ServiceProvider.GetRequiredService<UserManager<User>>(),
        roleManager: scope.ServiceProvider.GetRequiredService<RoleManager<Role>>(),
        defaultUserAvatarAsUrlHandler: scope.ServiceProvider.GetRequiredService<IDefaultUserAvatarAsUrlHandler>(),
        cancellationToken: CancellationToken.None);

    // Data cannot be seed.
    if (!seedResult)
    {
        throw new HostAbortedException(message: "Database seeding is false.");
    }
}

// Configure the HTTP request pipeline.
app
    .UseResponseCaching()
    .UseFastEndpoints()
    .UseSwaggerGen()
    .UseSwaggerUi(configure: options =>
    {
        options.Path = string.Empty;
        options.DefaultModelsExpandDepth = -1;
    });

app.Run();
