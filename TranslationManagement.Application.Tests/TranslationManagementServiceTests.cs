using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Polly;
using TranslationManagement.Application.Abstractions;
using TranslationManagement.Application.Services;
using TranslationManagement.Domain.Entities;
using TranslationManagement.Infrastructure;

namespace TranslationManagement.Application.Tests
{
    internal class TranslationManagementServiceTests
    {
        private IDbContext _dbContext;
        private TranslatorManagementService sut = null!;

        [SetUp]
        public async Task Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite("Data Source=TestTranslationAppDatabase.db");
            });

            var provider = services.BuildServiceProvider();

            var dbContext = provider.GetRequiredService<AppDbContext>();

            dbContext.Database.Migrate();

            _dbContext = dbContext;

            _dbContext.Translators.AddRange(new List<Translator>
            {
                new Translator
                {
                    Status = Domain.Enums.TranslatorStatus.Certified,
                    CreditCardNumber = "123456789",
                    HourlyRate = 1,
                    Name = "John Doe",
                },
                new Translator
                {
                    Status = Domain.Enums.TranslatorStatus.Applicant,
                    CreditCardNumber = "21232181685",
                    HourlyRate = 150.15M,
                    Name = "Alice Smith",
                }
            });

            await _dbContext.SaveChangesAsync(CancellationToken.None);

            var logger = Substitute.For<ILogger<TranslatorManagementService>>();
            sut = Substitute.For<TranslatorManagementService>(_dbContext, logger);
        }

        [TearDown]
        public async Task TearDown()
        {
            _dbContext.Translators.RemoveRange(_dbContext.Translators);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        [Theory, AutoNSubstituteData]
        public async Task GetTranslatorsByNameAsync_TranslatorNotInDatabase_ReturnEmptyList()
        {
            var name = "random name";

            var result = await sut.GetTranslatorsByNameAsync(name, CancellationToken.None);

            result.Should().BeEmpty();
        }

        [Theory, AutoNSubstituteData]
        public async Task GetTranslatorsByNameAsync_TranslatorInDatabase_ReturnCorrectTranslator()
        {
            var name = "Alice Smith";

            var result = await sut.GetTranslatorsByNameAsync(name, CancellationToken.None);

            result.Should().HaveCount(1);
            result[0].Status.Should().Be(Domain.Enums.TranslatorStatus.Applicant);
            result[0].CreditCardNumber.Should().Be("21232181685");
            result[0].HourlyRate.Should().Be(150.15M);
            result[0].Name.Should().Be("Alice Smith");
        }
    }
}