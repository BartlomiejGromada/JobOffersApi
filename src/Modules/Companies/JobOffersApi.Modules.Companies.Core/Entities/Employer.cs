using JobOffersApi.Abstractions.Core;

namespace JobOffersApi.Modules.Companies.Core.Entities;

/// <summary>
/// Id in table Users in the same in Employer table
/// </summary>
internal class Employer : AggregateRoot<Guid>
{
    private List<CompanyEmployer> companiesEmployers = new();

    private Employer()
    {
        // EF Core needs it
    }

    public Employer(
        Guid id,
        string firstName,
        string lastName,
        DateOnly dateOfBirth,
        DateTimeOffset createdDate)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        CreatedDate = createdDate;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; }
    public IReadOnlyCollection<CompanyEmployer> CompaniesEmployers => companiesEmployers;
}
