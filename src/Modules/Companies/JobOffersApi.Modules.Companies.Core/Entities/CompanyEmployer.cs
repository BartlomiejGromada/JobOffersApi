using JobOffersApi.Abstractions.Core;

namespace JobOffersApi.Modules.Companies.Core.Entities;

internal class CompanyEmployer : Entity<Guid>
{
    private CompanyEmployer()
    {
        // EF Core needs it    
    }

    public CompanyEmployer(
        Company company,
        Employer employer,
        string position,
        DateTimeOffset createdDate)
    {
        Company = company;
        Employer = employer;
        Position = position;
        CreatedDate = createdDate;
    }

    public Company Company { get; private set; }
    public Guid CompanyId { get; private set; }

    public Employer Employer { get; private set; }
    public Guid EmployerId { get; private set; }

    public string Position { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; }
}
