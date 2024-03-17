using Microsoft.Extensions.DependencyInjection;
using TranslationManagement.Application.Contracts;
using TranslationManagement.Application.Services;

namespace TranslationManagement.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddScoped<ITranslationJobService, TranslationJobService>();
            services.AddScoped<ITranslatorManagementService, TranslatorManagementService>();

            return services;
        }
    }
}
