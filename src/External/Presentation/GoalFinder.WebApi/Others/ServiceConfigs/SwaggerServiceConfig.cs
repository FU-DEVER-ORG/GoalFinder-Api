using FastEndpoints.Swagger;
using FuDever.Configuration.Presentation.WebApi.Swagger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.WebApi.Others.ServiceConfigs;

/// <summary>
///     Swagger service config.
/// </summary>
internal static class SwaggerServiceConfig
{
    internal static void ConfigSwagger(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        var option = configuration
            .GetRequiredSection(key: "Swagger")
            .GetRequiredSection(key: "NSwag")
            .Get<NSwagOption>();

        services.SwaggerDocument(documentOption =>
        {
            documentOption.DocumentSettings = setting =>
            {
                setting.PostProcess = document =>
                {
                    document.Info = new()
                    {
                        Version = option.Doc.Info.Version,
                        Title = option.Doc.Info.Title,
                        Description = option.Doc.Info.Description,
                        Contact = new()
                        {
                            Name = option.Doc.Info.Contact.Name,
                            Email = option.Doc.Info.Contact.Email
                        },
                        License = new()
                        {
                            Name = option.Doc.Info.License.Name,
                            Url = new(value: option.Doc.Info.License.Url)
                        }
                    };
                };
            };

            documentOption.EnableJWTBearerAuth = true;
        });
    }
}
