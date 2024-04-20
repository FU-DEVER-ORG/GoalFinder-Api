using Microsoft.AspNetCore.Builder;
using System.Text;
using System;
using FastEndpoints;
using GoalFinder.WebApi;
using Microsoft.IdentityModel.JsonWebTokens;
using GoalFinder.MySqlRelationalDb;
using GoalFinder.Application;

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

services.ConfigWebApi();
services.ConfigApplication();
services.ConfigMySqlRelationalDatabase(configuration: config);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseFastEndpoints();

app.Run();
