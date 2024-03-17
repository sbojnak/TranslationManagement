using Microsoft.EntityFrameworkCore;
using TranslationManagement.Application.Abstractions;
using TranslationManagement.Domain.Entities;

namespace TranslationManagement.Infrastructure;

public class AppDbContext : DbContext, IDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TranslationJob> TranslationJobs { get; set; } = null!;
    public DbSet<Translator> Translators { get; set; } = null!;
}