using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.Companies.Core.Exceptions;

namespace JobOffersApi.Modules.Companies.Core.Entities;

internal class Company : AggregateRoot<Guid>
{
    private List<CompanyEmployer> companiesEmployers = new();

    private Company()
    {
        // EF Core needs it
    }

    public Company(string name, string description, DateTimeOffset createdDate)
    {
        Name = name;
        Description = description;
        CreatedDate = createdDate;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; }
    public IReadOnlyCollection<CompanyEmployer> CompaniesEmployers => companiesEmployers;

    public void AddEmployer(Employer employer, string position, DateTimeOffset date)
    {
        var isAlreadyAdded = companiesEmployers.Any(ce => ce.Employer.Id == employer.Id);

        if(isAlreadyAdded)
        {
            throw new EmployerAlreadyAddedException(Id, employer.Id);
        }

        companiesEmployers.Add(new CompanyEmployer(
            company: this,
            employer,
            position,
            date));
    }
}
