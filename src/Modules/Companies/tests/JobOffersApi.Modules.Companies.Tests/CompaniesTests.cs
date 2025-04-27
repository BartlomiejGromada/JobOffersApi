using JobOffersApi.Abstractions.Exceptions;
using JobOffersApi.Modules.Companies.Core.Entities;
using JobOffersApi.Modules.Companies.Core.Exceptions;

namespace JobOffersApi.Modules.Companies.Tests.Unit;

public sealed class CompaniesTests
{
    public CompaniesTests()
    {
    }

    [Fact]
    public void Add_Employer_To_Company_Should_End_With_Success()
    {
        // Arrange
        var company = new Company(
            "Company 1",
            string.Empty,
            DateTimeOffset.Now,
            new Abstractions.Core.Location("Poland", "Kalisz", "Kaliska", "16"));

        var employer = new Employer(
            Guid.NewGuid(),
            "Jan",
            "Kowalski",
            DateOnly.FromDateTime(DateTime.Now),
            DateTimeOffset.Now);

        // Act
        company.AddEmployer(employer, "Employer", DateTimeOffset.Now);

        // Assert
        var employersCount = company.CompaniesEmployers
            .ToList()
            .Count();

        Assert.Equal(1, employersCount);
    }

    [Fact]
    public void Add_Employer_To_Company_Should_Throw_Exception_When_Employer_Already_Added()
    {
        // Arrange
        var company = new Company(
            "Company 1",
            string.Empty,
            DateTimeOffset.Now,
            new Abstractions.Core.Location("Poland", "Kalisz", "Kaliska", "16"));

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

    [Fact]
    public void Remove_Employer_From_Company_Should_End_With_Success()
    {
        // Arrange
        var company = new Company(
            "Company 1",
            string.Empty,
            DateTimeOffset.Now,
            new Abstractions.Core.Location("Poland", "Kalisz", "Kaliska", "16"));

        var employer = new Employer(
            Guid.NewGuid(),
            "Jan",
            "Kowalski",
            DateOnly.FromDateTime(DateTime.Now),
            DateTimeOffset.Now);

        // Act
        company.AddEmployer(employer, "Employer", DateTimeOffset.Now);
        company.RemoveEmployer(employer.Id);

        // Assert
        var employersCount = company.CompaniesEmployers
            .ToList()
            .Count();

        Assert.Equal(0, employersCount);
    }

    [Fact]
    public void Remove_Employer_From_Company_Should_Throw_Exception_When_Employer_Not_Belong_To_Company()
    {
        // Arrange
        var company = new Company(
            "Company 1",
            string.Empty,
            DateTimeOffset.Now,
            new Abstractions.Core.Location("Poland", "Kalisz", "Kaliska", "16"));

        var employer = new Employer(
            Guid.NewGuid(),
            "Jan",
            "Kowalski",
            DateOnly.FromDateTime(DateTime.Now),
            DateTimeOffset.Now);


        var employer2 = new Employer(
            Guid.NewGuid(),
            "Adam",
            "Nowak",
            DateOnly.FromDateTime(DateTime.Now),
            DateTimeOffset.Now);

        // Act & Assert
        company.AddEmployer(employer, "Employer", DateTimeOffset.Now);

        var exception = Assert.Throws<EmployeeNotBelongToCompanyException>(() =>
                 company.RemoveEmployer(employer2.Id));
    }

    [Fact]
    public void Update_Company_Should_End_With_Success()
    {
        // Arrange
        var company = new Company(
            "Company 1",
            string.Empty,
            DateTimeOffset.Now,
            new Abstractions.Core.Location("Poland", "Kalisz", "Kaliska", "16"));

        // Act
        company.Update("Company 1", "Firma produkcyjna", new Abstractions.DTO.AddLocationDto
        {
            Country = "Poland",
            City = "Kalisz",
            Street = "Kaliska",
            HouseNumber = "16",
            ApartmentNumber = "2"
        });

        // Assert
        Assert.Equal("2", company.Location.ApartmentNumber);
        Assert.Equal("Firma produkcyjna", company.Description);
    }

    [Fact]
    public void Update_Company_Should_End_With_Failure()
    {
        // Arrange
        var company = new Company(
            "Company 1",
            string.Empty,
            DateTimeOffset.Now,
            new Abstractions.Core.Location("Poland", "Kalisz", "Kaliska", "16"));

        
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
           company.Update(string.Empty, "Firma produkcyjna", new Abstractions.DTO.AddLocationDto
           {
               Country = "Poland",
               City = "Kalisz",
               Street = "Kaliska",
               HouseNumber = "16",
               ApartmentNumber = "2"
           }));
    }
}