using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationManagement.Application.Abstractions;
using TranslationManagement.Infrastructure.FileDeserializers;

namespace TranslationManagement.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=TranslationAppDatabase.db"));
            services.AddScoped<IRetryConfiguration, RetryConfiguration>();
            services.AddScoped<IMessagingService, MessagingService>();4
            services.AddScoped<IDbContext, AppDbContext>();
            services.AddScoped<IFileDeserializerTypeResolver, FileDeserializerTypeResolver>();

            return services;
        }
    }
}
