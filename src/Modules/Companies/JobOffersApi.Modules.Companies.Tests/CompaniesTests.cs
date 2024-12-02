using JobOffersApi.Modules.Companies.Core.Entities;
using JobOffersApi.Modules.Companies.Core.Exceptions;
using Microsoft.AspNetCore.Builder;

namespace JobOffersApi.Modules.Companies.Tests.Unit;

public sealed class CompaniesTests
{
    public CompaniesTests()
    {
    }

    [Fact]
    public void Should_End_With_Success_When_Added_Employer_To_Company()
    {
        // Act
        var company = new Company(
            "Company 1",
            string.Empty,
            DateTimeOffset.Now);

        var employer = new Employer(
            Guid.NewGuid(),
            "Jan",
            "Kowalski",
            DateOnly.FromDateTime(DateTime.Now),
            DateTimeOffset.Now);

        // Arrange
        company.AddEmployer(employer, "Employer", DateTimeOffset.Now);

        // Assert
        var employersCount = company.CompaniesEmployers
            .ToList()
            .Count();

        Assert.Equal(1, employersCount);
    }

    [Fact]
    public void Should_Throw_Exception_When_Employer_Already_Added_To_Company()
    {
        // Arrange
        var company = new Company(
            "Company 1",
            string.Empty,
            DateTimeOffset.Now);

        var employer = new Employer(
            Guid.NewGuid(),
            "Jan",
            "Kowalski",
            DateOnly.FromDateTime(DateTime.Now),
            DateTimeOffset.Now);

        company.AddEmployer(employer, "Employer", DateTimeOffset.Now);

        // Act & Assert
        var exception = Assert.Throws<EmployerAlreadyAddedException>(() =>
          company.AddEmployer(employer, "Employer", DateTimeOffset.Now));
    }
}