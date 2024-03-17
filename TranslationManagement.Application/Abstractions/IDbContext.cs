using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using TranslationManagement.Domain.Entities;

namespace TranslationManagement.Application.Abstractions
{
    public interface IDbContext
    {
        DbSet<TranslationJob> TranslationJobs { get; set; }
        DbSet<Translator> Translators { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
