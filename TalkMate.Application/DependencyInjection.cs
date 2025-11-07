using Microsoft.Extensions.DependencyInjection;
using TalkMate.Application.Interfaces.Services;
using TalkMate.Application.Services;

namespace TalkMate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IChatService, ChatService>();

        return services;
    }
}

