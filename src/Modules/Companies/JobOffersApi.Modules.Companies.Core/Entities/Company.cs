using JobOffersApi.Abstractions.Core;
using JobOffersApi.Abstractions.DTO;
using JobOffersApi.Modules.Companies.Core.Exceptions;

namespace JobOffersApi.Modules.Companies.Core.Entities;

internal class Company : AggregateRoot<Guid>
{
    private List<CompanyEmployer> companiesEmployers = new();

    private Company()
    {
        // EF Core needs it
    }

    public Company(
        string name,
        string description,
        DateTimeOffset createdDate,
        Location location)
    {
        Name = name;
        Description = description;
        CreatedDate = createdDate;
        Location = location;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; }
    public Location Location { get; private set; }
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

    public void RemoveEmployer(Guid employerId)
    {
        var toRemove = companiesEmployers.FirstOrDefault(ce => ce.Employer.Id == employerId);
        if (toRemove is null)
        {
            throw new EmployeeNotBelongToCompanyException(employerId, Id);
        }
        companiesEmployers.Remove(toRemove);
    }

    public void Update(string name, string  description, AddLocationDto location)
    {
        if(string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name cannot be null or empty");
        }
        if (string.IsNullOrEmpty(description))
        {
            throw new ArgumentException("Name cannot be null or empty");
        }

        var newLocation = new Location(
            location.Country,
            location.City,
            location.HouseNumber,
            location.Street,
            location.ApartmentNumber,
            location.PostalCode);

        Name = name;
        Description = description;
        Location = newLocation;
    }
}
