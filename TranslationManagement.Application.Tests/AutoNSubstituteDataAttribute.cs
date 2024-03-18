using AutoFixture.AutoNSubstitute;
using AutoFixture.NUnit3;
using AutoFixture;
using TranslationManagement.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace TranslationManagement.Application.Tests;

public class AutoNSubstituteDataAttribute : AutoDataAttribute
{
    public AutoNSubstituteDataAttribute()
        : base(CreateFixture)
    {
    }

    private static IFixture CreateFixture()
    {
        var customization = new AutoNSubstituteCustomization();
        var fixture = new Fixture();
        fixture.Customize(customization);
        return fixture;
    }
}
