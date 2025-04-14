using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.Companies.Application.Commands;

internal class RemoveEmployerFromCompanyCommand : ICommand
{
    public RemoveEmployerFromCompanyCommand(
        Guid companyId,
        Guid employerId,
        Guid invokerId,
        string invokerRole)
    {
        CompanyId = companyId;
        EmployerId = employerId;
        InvokerId = invokerId;
        InvokerRole = invokerRole;
    }

    public Guid CompanyId { get; init; }
    public Guid EmployerId { get; init; }
    public Guid InvokerId { get; init; }
    public string InvokerRole { get; init; }
}
