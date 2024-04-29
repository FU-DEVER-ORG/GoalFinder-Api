using GoalFinder.Application.Shared.Mail;
using GoalFinder.Configuration.Infrastructure.Mail.GoogleGmail;
using GoalFinder.GoogleSmtpServerForMail.Handler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoalFinder.GoogleSmtpServerForMail.ServiceConfigs;

/// <summary>
///     Core service config.
/// </summary>
internal static class CoreServiceConfig
{
    internal static void ConfigCore(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services
            .AddSingleton<ISendingMailHandler, GoogleSendingMailHandler>()
            .AddSingleton(
                configuration
                    .GetRequiredSection(key: "SmtpServer")
                    .GetRequiredSection(key: "GoogleGmail")
                    .Get<GoogleGmailSendingOption>()
            );
    }
}
