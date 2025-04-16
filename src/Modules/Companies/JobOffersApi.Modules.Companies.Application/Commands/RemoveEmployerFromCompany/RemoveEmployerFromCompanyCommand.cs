using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.Companies.Application.Commands.RemoveEmployerFromCompany;

internal class RemoveEmployerFromCompanyCommand : ICommand
{
    public RemoveEmployerFromCompanyCommand(
        Guid companyId,
        Guid employerId)
    {
        CompanyId = companyId;
        EmployerId = employerId;
    }

    public Guid CompanyId { get; init; }
    public Guid EmployerId { get; init; }
}
